using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x02000348 RID: 840
	public class SM2KeyExchange
	{
		// Token: 0x06001900 RID: 6400 RVA: 0x000807E0 File Offset: 0x000807E0
		public SM2KeyExchange() : this(new SM3Digest())
		{
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x000807F0 File Offset: 0x000807F0
		public SM2KeyExchange(IDigest digest)
		{
			this.mDigest = digest;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00080800 File Offset: 0x00080800
		public virtual void Init(ICipherParameters privParam)
		{
			SM2KeyExchangePrivateParameters sm2KeyExchangePrivateParameters;
			if (privParam is ParametersWithID)
			{
				sm2KeyExchangePrivateParameters = (SM2KeyExchangePrivateParameters)((ParametersWithID)privParam).Parameters;
				this.mUserID = ((ParametersWithID)privParam).GetID();
			}
			else
			{
				sm2KeyExchangePrivateParameters = (SM2KeyExchangePrivateParameters)privParam;
				this.mUserID = new byte[0];
			}
			this.mInitiator = sm2KeyExchangePrivateParameters.IsInitiator;
			this.mStaticKey = sm2KeyExchangePrivateParameters.StaticPrivateKey;
			this.mEphemeralKey = sm2KeyExchangePrivateParameters.EphemeralPrivateKey;
			this.mECParams = this.mStaticKey.Parameters;
			this.mStaticPubPoint = sm2KeyExchangePrivateParameters.StaticPublicPoint;
			this.mEphemeralPubPoint = sm2KeyExchangePrivateParameters.EphemeralPublicPoint;
			this.mW = this.mECParams.Curve.FieldSize / 2 - 1;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x000808C0 File Offset: 0x000808C0
		public virtual byte[] CalculateKey(int kLen, ICipherParameters pubParam)
		{
			SM2KeyExchangePublicParameters sm2KeyExchangePublicParameters;
			byte[] userID;
			if (pubParam is ParametersWithID)
			{
				sm2KeyExchangePublicParameters = (SM2KeyExchangePublicParameters)((ParametersWithID)pubParam).Parameters;
				userID = ((ParametersWithID)pubParam).GetID();
			}
			else
			{
				sm2KeyExchangePublicParameters = (SM2KeyExchangePublicParameters)pubParam;
				userID = new byte[0];
			}
			byte[] z = this.GetZ(this.mDigest, this.mUserID, this.mStaticPubPoint);
			byte[] z2 = this.GetZ(this.mDigest, userID, sm2KeyExchangePublicParameters.StaticPublicKey.Q);
			ECPoint u = this.CalculateU(sm2KeyExchangePublicParameters);
			byte[] result;
			if (this.mInitiator)
			{
				result = this.Kdf(u, z, z2, kLen);
			}
			else
			{
				result = this.Kdf(u, z2, z, kLen);
			}
			return result;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00080974 File Offset: 0x00080974
		public virtual byte[][] CalculateKeyWithConfirmation(int kLen, byte[] confirmationTag, ICipherParameters pubParam)
		{
			SM2KeyExchangePublicParameters sm2KeyExchangePublicParameters;
			byte[] userID;
			if (pubParam is ParametersWithID)
			{
				sm2KeyExchangePublicParameters = (SM2KeyExchangePublicParameters)((ParametersWithID)pubParam).Parameters;
				userID = ((ParametersWithID)pubParam).GetID();
			}
			else
			{
				sm2KeyExchangePublicParameters = (SM2KeyExchangePublicParameters)pubParam;
				userID = new byte[0];
			}
			if (this.mInitiator && confirmationTag == null)
			{
				throw new ArgumentException("if initiating, confirmationTag must be set");
			}
			byte[] z = this.GetZ(this.mDigest, this.mUserID, this.mStaticPubPoint);
			byte[] z2 = this.GetZ(this.mDigest, userID, sm2KeyExchangePublicParameters.StaticPublicKey.Q);
			ECPoint u = this.CalculateU(sm2KeyExchangePublicParameters);
			byte[] array;
			if (!this.mInitiator)
			{
				array = this.Kdf(u, z2, z, kLen);
				byte[] inner = this.CalculateInnerHash(this.mDigest, u, z2, z, sm2KeyExchangePublicParameters.EphemeralPublicKey.Q, this.mEphemeralPubPoint);
				return new byte[][]
				{
					array,
					this.S1(this.mDigest, u, inner),
					this.S2(this.mDigest, u, inner)
				};
			}
			array = this.Kdf(u, z, z2, kLen);
			byte[] inner2 = this.CalculateInnerHash(this.mDigest, u, z, z2, this.mEphemeralPubPoint, sm2KeyExchangePublicParameters.EphemeralPublicKey.Q);
			byte[] a = this.S1(this.mDigest, u, inner2);
			if (!Arrays.ConstantTimeAreEqual(a, confirmationTag))
			{
				throw new InvalidOperationException("confirmation tag mismatch");
			}
			return new byte[][]
			{
				array,
				this.S2(this.mDigest, u, inner2)
			};
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00080B20 File Offset: 0x00080B20
		protected virtual ECPoint CalculateU(SM2KeyExchangePublicParameters otherPub)
		{
			ECDomainParameters parameters = this.mStaticKey.Parameters;
			ECPoint p = ECAlgorithms.CleanPoint(parameters.Curve, otherPub.StaticPublicKey.Q);
			ECPoint ecpoint = ECAlgorithms.CleanPoint(parameters.Curve, otherPub.EphemeralPublicKey.Q);
			BigInteger bigInteger = this.Reduce(this.mEphemeralPubPoint.AffineXCoord.ToBigInteger());
			BigInteger val = this.Reduce(ecpoint.AffineXCoord.ToBigInteger());
			BigInteger val2 = this.mStaticKey.D.Add(bigInteger.Multiply(this.mEphemeralKey.D));
			BigInteger bigInteger2 = this.mECParams.H.Multiply(val2).Mod(this.mECParams.N);
			BigInteger b = bigInteger2.Multiply(val).Mod(this.mECParams.N);
			return ECAlgorithms.SumOfTwoMultiplies(p, bigInteger2, ecpoint, b).Normalize();
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00080C08 File Offset: 0x00080C08
		protected virtual byte[] Kdf(ECPoint u, byte[] za, byte[] zb, int klen)
		{
			int digestSize = this.mDigest.GetDigestSize();
			byte[] array = new byte[Math.Max(4, digestSize)];
			byte[] array2 = new byte[(klen + 7) / 8];
			int i = 0;
			IMemoable memoable = this.mDigest as IMemoable;
			IMemoable other = null;
			if (memoable != null)
			{
				this.AddFieldElement(this.mDigest, u.AffineXCoord);
				this.AddFieldElement(this.mDigest, u.AffineYCoord);
				this.mDigest.BlockUpdate(za, 0, za.Length);
				this.mDigest.BlockUpdate(zb, 0, zb.Length);
				other = memoable.Copy();
			}
			uint num = 0U;
			while (i < array2.Length)
			{
				if (memoable != null)
				{
					memoable.Reset(other);
				}
				else
				{
					this.AddFieldElement(this.mDigest, u.AffineXCoord);
					this.AddFieldElement(this.mDigest, u.AffineYCoord);
					this.mDigest.BlockUpdate(za, 0, za.Length);
					this.mDigest.BlockUpdate(zb, 0, zb.Length);
				}
				Pack.UInt32_To_BE(num += 1U, array, 0);
				this.mDigest.BlockUpdate(array, 0, 4);
				this.mDigest.DoFinal(array, 0);
				int num2 = Math.Min(digestSize, array2.Length - i);
				Array.Copy(array, 0, array2, i, num2);
				i += num2;
			}
			return array2;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00080D58 File Offset: 0x00080D58
		private BigInteger Reduce(BigInteger x)
		{
			return x.And(BigInteger.One.ShiftLeft(this.mW).Subtract(BigInteger.One)).SetBit(this.mW);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00080D94 File Offset: 0x00080D94
		private byte[] S1(IDigest digest, ECPoint u, byte[] inner)
		{
			digest.Update(2);
			this.AddFieldElement(digest, u.AffineYCoord);
			digest.BlockUpdate(inner, 0, inner.Length);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00080DCC File Offset: 0x00080DCC
		private byte[] CalculateInnerHash(IDigest digest, ECPoint u, byte[] za, byte[] zb, ECPoint p1, ECPoint p2)
		{
			this.AddFieldElement(digest, u.AffineXCoord);
			digest.BlockUpdate(za, 0, za.Length);
			digest.BlockUpdate(zb, 0, zb.Length);
			this.AddFieldElement(digest, p1.AffineXCoord);
			this.AddFieldElement(digest, p1.AffineYCoord);
			this.AddFieldElement(digest, p2.AffineXCoord);
			this.AddFieldElement(digest, p2.AffineYCoord);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00080E40 File Offset: 0x00080E40
		private byte[] S2(IDigest digest, ECPoint u, byte[] inner)
		{
			digest.Update(3);
			this.AddFieldElement(digest, u.AffineYCoord);
			digest.BlockUpdate(inner, 0, inner.Length);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00080E78 File Offset: 0x00080E78
		private byte[] GetZ(IDigest digest, byte[] userID, ECPoint pubPoint)
		{
			this.AddUserID(digest, userID);
			this.AddFieldElement(digest, this.mECParams.Curve.A);
			this.AddFieldElement(digest, this.mECParams.Curve.B);
			this.AddFieldElement(digest, this.mECParams.G.AffineXCoord);
			this.AddFieldElement(digest, this.mECParams.G.AffineYCoord);
			this.AddFieldElement(digest, pubPoint.AffineXCoord);
			this.AddFieldElement(digest, pubPoint.AffineYCoord);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00080F10 File Offset: 0x00080F10
		private void AddUserID(IDigest digest, byte[] userID)
		{
			uint num = (uint)(userID.Length * 8);
			digest.Update((byte)(num >> 8));
			digest.Update((byte)num);
			digest.BlockUpdate(userID, 0, userID.Length);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00080F44 File Offset: 0x00080F44
		private void AddFieldElement(IDigest digest, ECFieldElement v)
		{
			byte[] encoded = v.GetEncoded();
			digest.BlockUpdate(encoded, 0, encoded.Length);
		}

		// Token: 0x040010CE RID: 4302
		private readonly IDigest mDigest;

		// Token: 0x040010CF RID: 4303
		private byte[] mUserID;

		// Token: 0x040010D0 RID: 4304
		private ECPrivateKeyParameters mStaticKey;

		// Token: 0x040010D1 RID: 4305
		private ECPoint mStaticPubPoint;

		// Token: 0x040010D2 RID: 4306
		private ECPoint mEphemeralPubPoint;

		// Token: 0x040010D3 RID: 4307
		private ECDomainParameters mECParams;

		// Token: 0x040010D4 RID: 4308
		private int mW;

		// Token: 0x040010D5 RID: 4309
		private ECPrivateKeyParameters mEphemeralKey;

		// Token: 0x040010D6 RID: 4310
		private bool mInitiator;
	}
}
