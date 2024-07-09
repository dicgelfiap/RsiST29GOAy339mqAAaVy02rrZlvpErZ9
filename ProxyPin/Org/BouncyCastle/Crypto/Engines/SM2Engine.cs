using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003A7 RID: 935
	public class SM2Engine
	{
		// Token: 0x06001DC6 RID: 7622 RVA: 0x000ABA64 File Offset: 0x000ABA64
		public SM2Engine() : this(new SM3Digest())
		{
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000ABA74 File Offset: 0x000ABA74
		public SM2Engine(IDigest digest)
		{
			this.mDigest = digest;
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x000ABA84 File Offset: 0x000ABA84
		public virtual void Init(bool forEncryption, ICipherParameters param)
		{
			this.mForEncryption = forEncryption;
			if (forEncryption)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.mECKey = (ECKeyParameters)parametersWithRandom.Parameters;
				this.mECParams = this.mECKey.Parameters;
				ECPoint ecpoint = ((ECPublicKeyParameters)this.mECKey).Q.Multiply(this.mECParams.H);
				if (ecpoint.IsInfinity)
				{
					throw new ArgumentException("invalid key: [h]Q at infinity");
				}
				this.mRandom = parametersWithRandom.Random;
			}
			else
			{
				this.mECKey = (ECKeyParameters)param;
				this.mECParams = this.mECKey.Parameters;
			}
			this.mCurveLength = (this.mECParams.Curve.FieldSize + 7) / 8;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000ABB4C File Offset: 0x000ABB4C
		public virtual byte[] ProcessBlock(byte[] input, int inOff, int inLen)
		{
			if (this.mForEncryption)
			{
				return this.Encrypt(input, inOff, inLen);
			}
			return this.Decrypt(input, inOff, inLen);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000ABB6C File Offset: 0x000ABB6C
		protected virtual ECMultiplier CreateBasePointMultiplier()
		{
			return new FixedPointCombMultiplier();
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000ABB74 File Offset: 0x000ABB74
		private byte[] Encrypt(byte[] input, int inOff, int inLen)
		{
			byte[] array = new byte[inLen];
			Array.Copy(input, inOff, array, 0, array.Length);
			ECMultiplier ecmultiplier = this.CreateBasePointMultiplier();
			byte[] encoded;
			ECPoint ecpoint2;
			do
			{
				BigInteger bigInteger = this.NextK();
				ECPoint ecpoint = ecmultiplier.Multiply(this.mECParams.G, bigInteger).Normalize();
				encoded = ecpoint.GetEncoded(false);
				ecpoint2 = ((ECPublicKeyParameters)this.mECKey).Q.Multiply(bigInteger).Normalize();
				this.Kdf(this.mDigest, ecpoint2, array);
			}
			while (this.NotEncrypted(array, input, inOff));
			this.AddFieldElement(this.mDigest, ecpoint2.AffineXCoord);
			this.mDigest.BlockUpdate(input, inOff, inLen);
			this.AddFieldElement(this.mDigest, ecpoint2.AffineYCoord);
			byte[] array2 = DigestUtilities.DoFinal(this.mDigest);
			return Arrays.ConcatenateAll(new byte[][]
			{
				encoded,
				array,
				array2
			});
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000ABC6C File Offset: 0x000ABC6C
		private byte[] Decrypt(byte[] input, int inOff, int inLen)
		{
			byte[] array = new byte[this.mCurveLength * 2 + 1];
			Array.Copy(input, inOff, array, 0, array.Length);
			ECPoint ecpoint = this.mECParams.Curve.DecodePoint(array);
			ECPoint ecpoint2 = ecpoint.Multiply(this.mECParams.H);
			if (ecpoint2.IsInfinity)
			{
				throw new InvalidCipherTextException("[h]C1 at infinity");
			}
			ecpoint = ecpoint.Multiply(((ECPrivateKeyParameters)this.mECKey).D).Normalize();
			byte[] array2 = new byte[inLen - array.Length - this.mDigest.GetDigestSize()];
			Array.Copy(input, inOff + array.Length, array2, 0, array2.Length);
			this.Kdf(this.mDigest, ecpoint, array2);
			this.AddFieldElement(this.mDigest, ecpoint.AffineXCoord);
			this.mDigest.BlockUpdate(array2, 0, array2.Length);
			this.AddFieldElement(this.mDigest, ecpoint.AffineYCoord);
			byte[] array3 = DigestUtilities.DoFinal(this.mDigest);
			int num = 0;
			for (int num2 = 0; num2 != array3.Length; num2++)
			{
				num |= (int)(array3[num2] ^ input[inOff + array.Length + array2.Length + num2]);
			}
			Arrays.Fill(array, 0);
			Arrays.Fill(array3, 0);
			if (num != 0)
			{
				Arrays.Fill(array2, 0);
				throw new InvalidCipherTextException("invalid cipher text");
			}
			return array2;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000ABDC0 File Offset: 0x000ABDC0
		private bool NotEncrypted(byte[] encData, byte[] input, int inOff)
		{
			for (int num = 0; num != encData.Length; num++)
			{
				if (encData[num] != input[inOff + num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000ABDF4 File Offset: 0x000ABDF4
		private void Kdf(IDigest digest, ECPoint c1, byte[] encData)
		{
			int digestSize = digest.GetDigestSize();
			byte[] array = new byte[Math.Max(4, digestSize)];
			int i = 0;
			IMemoable memoable = digest as IMemoable;
			IMemoable other = null;
			if (memoable != null)
			{
				this.AddFieldElement(digest, c1.AffineXCoord);
				this.AddFieldElement(digest, c1.AffineYCoord);
				other = memoable.Copy();
			}
			uint num = 0U;
			while (i < encData.Length)
			{
				if (memoable != null)
				{
					memoable.Reset(other);
				}
				else
				{
					this.AddFieldElement(digest, c1.AffineXCoord);
					this.AddFieldElement(digest, c1.AffineYCoord);
				}
				Pack.UInt32_To_BE(num += 1U, array, 0);
				digest.BlockUpdate(array, 0, 4);
				digest.DoFinal(array, 0);
				int num2 = Math.Min(digestSize, encData.Length - i);
				this.Xor(encData, array, i, num2);
				i += num2;
			}
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000ABEC8 File Offset: 0x000ABEC8
		private void Xor(byte[] data, byte[] kdfOut, int dOff, int dRemaining)
		{
			for (int num = 0; num != dRemaining; num++)
			{
				IntPtr intPtr;
				data[(int)(intPtr = (IntPtr)(dOff + num))] = (data[(int)intPtr] ^ kdfOut[num]);
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000ABEFC File Offset: 0x000ABEFC
		private BigInteger NextK()
		{
			int bitLength = this.mECParams.N.BitLength;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(bitLength, this.mRandom);
			}
			while (bigInteger.SignValue == 0 || bigInteger.CompareTo(this.mECParams.N) >= 0);
			return bigInteger;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000ABF48 File Offset: 0x000ABF48
		private void AddFieldElement(IDigest digest, ECFieldElement v)
		{
			byte[] encoded = v.GetEncoded();
			digest.BlockUpdate(encoded, 0, encoded.Length);
		}

		// Token: 0x0400139A RID: 5018
		private readonly IDigest mDigest;

		// Token: 0x0400139B RID: 5019
		private bool mForEncryption;

		// Token: 0x0400139C RID: 5020
		private ECKeyParameters mECKey;

		// Token: 0x0400139D RID: 5021
		private ECDomainParameters mECParams;

		// Token: 0x0400139E RID: 5022
		private int mCurveLength;

		// Token: 0x0400139F RID: 5023
		private SecureRandom mRandom;
	}
}
