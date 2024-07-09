using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004A9 RID: 1193
	public class Iso9796d2PssSigner : ISignerWithRecovery, ISigner
	{
		// Token: 0x060024A4 RID: 9380 RVA: 0x000CB9EC File Offset: 0x000CB9EC
		public byte[] GetRecoveredMessage()
		{
			return this.recoveredMessage;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000CB9F4 File Offset: 0x000CB9F4
		public Iso9796d2PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLength, bool isImplicit)
		{
			this.cipher = cipher;
			this.digest = digest;
			this.hLen = digest.GetDigestSize();
			this.saltLength = saltLength;
			if (isImplicit)
			{
				this.trailer = 188;
				return;
			}
			if (IsoTrailers.NoTrailerAvailable(digest))
			{
				throw new ArgumentException("no valid trailer", "digest");
			}
			this.trailer = IsoTrailers.GetTrailer(digest);
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000CBA68 File Offset: 0x000CBA68
		public Iso9796d2PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLength) : this(cipher, digest, saltLength, false)
		{
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x000CBA74 File Offset: 0x000CBA74
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "withISO9796-2S2";
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000CBA8C File Offset: 0x000CBA8C
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			RsaKeyParameters rsaKeyParameters;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				rsaKeyParameters = (RsaKeyParameters)parametersWithRandom.Parameters;
				if (forSigning)
				{
					this.random = parametersWithRandom.Random;
				}
			}
			else if (parameters is ParametersWithSalt)
			{
				if (!forSigning)
				{
					throw new ArgumentException("ParametersWithSalt only valid for signing", "parameters");
				}
				ParametersWithSalt parametersWithSalt = (ParametersWithSalt)parameters;
				rsaKeyParameters = (RsaKeyParameters)parametersWithSalt.Parameters;
				this.standardSalt = parametersWithSalt.GetSalt();
				if (this.standardSalt.Length != this.saltLength)
				{
					throw new ArgumentException("Fixed salt is of wrong length");
				}
			}
			else
			{
				rsaKeyParameters = (RsaKeyParameters)parameters;
				if (forSigning)
				{
					this.random = new SecureRandom();
				}
			}
			this.cipher.Init(forSigning, rsaKeyParameters);
			this.keyBits = rsaKeyParameters.Modulus.BitLength;
			this.block = new byte[(this.keyBits + 7) / 8];
			if (this.trailer == 188)
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - this.saltLength - 1 - 1];
			}
			else
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - this.saltLength - 1 - 2];
			}
			this.Reset();
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000CBBEC File Offset: 0x000CBBEC
		private bool IsSameAs(byte[] a, byte[] b)
		{
			if (this.messageLength != b.Length)
			{
				return false;
			}
			bool result = true;
			for (int num = 0; num != b.Length; num++)
			{
				if (a[num] != b[num])
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000CBC30 File Offset: 0x000CBC30
		private void ClearBlock(byte[] block)
		{
			Array.Clear(block, 0, block.Length);
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000CBC3C File Offset: 0x000CBC3C
		public virtual void UpdateWithRecoveredMessage(byte[] signature)
		{
			byte[] array = this.cipher.ProcessBlock(signature, 0, signature.Length);
			if (array.Length < (this.keyBits + 7) / 8)
			{
				byte[] array2 = new byte[(this.keyBits + 7) / 8];
				Array.Copy(array, 0, array2, array2.Length - array.Length, array.Length);
				this.ClearBlock(array);
				array = array2;
			}
			int num;
			if (((array[array.Length - 1] & 255) ^ 188) == 0)
			{
				num = 1;
			}
			else
			{
				int num2 = (int)(array[array.Length - 2] & byte.MaxValue) << 8 | (int)(array[array.Length - 1] & byte.MaxValue);
				if (IsoTrailers.NoTrailerAvailable(this.digest))
				{
					throw new ArgumentException("unrecognised hash in signature");
				}
				if (num2 != IsoTrailers.GetTrailer(this.digest))
				{
					throw new InvalidOperationException("signer initialised with wrong digest for trailer " + num2);
				}
				num = 2;
			}
			byte[] output = new byte[this.hLen];
			this.digest.DoFinal(output, 0);
			byte[] array3 = this.MaskGeneratorFunction1(array, array.Length - this.hLen - num, this.hLen, array.Length - this.hLen - num);
			byte[] array4;
			for (int num3 = 0; num3 != array3.Length; num3++)
			{
				IntPtr intPtr;
				(array4 = array)[(int)(intPtr = (IntPtr)num3)] = (array4[(int)intPtr] ^ array3[num3]);
			}
			(array4 = array)[0] = (array4[0] & 127);
			int num4 = 0;
			while (num4 < array.Length && array[num4++] != 1)
			{
			}
			if (num4 >= array.Length)
			{
				this.ClearBlock(array);
			}
			this.fullMessage = (num4 > 1);
			this.recoveredMessage = new byte[array3.Length - num4 - this.saltLength];
			Array.Copy(array, num4, this.recoveredMessage, 0, this.recoveredMessage.Length);
			this.recoveredMessage.CopyTo(this.mBuf, 0);
			this.preSig = signature;
			this.preBlock = array;
			this.preMStart = num4;
			this.preTLength = num;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000CBE34 File Offset: 0x000CBE34
		public virtual void Update(byte input)
		{
			if (this.preSig == null && this.messageLength < this.mBuf.Length)
			{
				this.mBuf[this.messageLength++] = input;
				return;
			}
			this.digest.Update(input);
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000CBE8C File Offset: 0x000CBE8C
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			if (this.preSig == null)
			{
				while (length > 0 && this.messageLength < this.mBuf.Length)
				{
					this.Update(input[inOff]);
					inOff++;
					length--;
				}
			}
			if (length > 0)
			{
				this.digest.BlockUpdate(input, inOff, length);
			}
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000CBEEC File Offset: 0x000CBEEC
		public virtual void Reset()
		{
			this.digest.Reset();
			this.messageLength = 0;
			if (this.mBuf != null)
			{
				this.ClearBlock(this.mBuf);
			}
			if (this.recoveredMessage != null)
			{
				this.ClearBlock(this.recoveredMessage);
				this.recoveredMessage = null;
			}
			this.fullMessage = false;
			if (this.preSig != null)
			{
				this.preSig = null;
				this.ClearBlock(this.preBlock);
				this.preBlock = null;
			}
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000CBF70 File Offset: 0x000CBF70
		public virtual byte[] GenerateSignature()
		{
			int digestSize = this.digest.GetDigestSize();
			byte[] array = new byte[digestSize];
			this.digest.DoFinal(array, 0);
			byte[] array2 = new byte[8];
			this.LtoOSP((long)(this.messageLength * 8), array2);
			this.digest.BlockUpdate(array2, 0, array2.Length);
			this.digest.BlockUpdate(this.mBuf, 0, this.messageLength);
			this.digest.BlockUpdate(array, 0, array.Length);
			byte[] array3;
			if (this.standardSalt != null)
			{
				array3 = this.standardSalt;
			}
			else
			{
				array3 = new byte[this.saltLength];
				this.random.NextBytes(array3);
			}
			this.digest.BlockUpdate(array3, 0, array3.Length);
			byte[] array4 = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array4, 0);
			int num = 2;
			if (this.trailer == 188)
			{
				num = 1;
			}
			int num2 = this.block.Length - this.messageLength - array3.Length - this.hLen - num - 1;
			this.block[num2] = 1;
			Array.Copy(this.mBuf, 0, this.block, num2 + 1, this.messageLength);
			Array.Copy(array3, 0, this.block, num2 + 1 + this.messageLength, array3.Length);
			byte[] array5 = this.MaskGeneratorFunction1(array4, 0, array4.Length, this.block.Length - this.hLen - num);
			byte[] array6;
			for (int num3 = 0; num3 != array5.Length; num3++)
			{
				IntPtr intPtr;
				(array6 = this.block)[(int)(intPtr = (IntPtr)num3)] = (array6[(int)intPtr] ^ array5[num3]);
			}
			Array.Copy(array4, 0, this.block, this.block.Length - this.hLen - num, this.hLen);
			if (this.trailer == 188)
			{
				this.block[this.block.Length - 1] = 188;
			}
			else
			{
				this.block[this.block.Length - 2] = (byte)((uint)this.trailer >> 8);
				this.block[this.block.Length - 1] = (byte)this.trailer;
			}
			(array6 = this.block)[0] = (array6[0] & 127);
			byte[] result = this.cipher.ProcessBlock(this.block, 0, this.block.Length);
			this.ClearBlock(this.mBuf);
			this.ClearBlock(this.block);
			this.messageLength = 0;
			return result;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000CC1F0 File Offset: 0x000CC1F0
		public virtual bool VerifySignature(byte[] signature)
		{
			byte[] array = new byte[this.hLen];
			this.digest.DoFinal(array, 0);
			if (this.preSig == null)
			{
				try
				{
					this.UpdateWithRecoveredMessage(signature);
					goto IL_57;
				}
				catch (Exception)
				{
					return false;
				}
			}
			if (!Arrays.AreEqual(this.preSig, signature))
			{
				throw new InvalidOperationException("UpdateWithRecoveredMessage called on different signature");
			}
			IL_57:
			byte[] array2 = this.preBlock;
			int num = this.preMStart;
			int num2 = this.preTLength;
			this.preSig = null;
			this.preBlock = null;
			byte[] array3 = new byte[8];
			this.LtoOSP((long)(this.recoveredMessage.Length * 8), array3);
			this.digest.BlockUpdate(array3, 0, array3.Length);
			if (this.recoveredMessage.Length != 0)
			{
				this.digest.BlockUpdate(this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			this.digest.BlockUpdate(array, 0, array.Length);
			if (this.standardSalt != null)
			{
				this.digest.BlockUpdate(this.standardSalt, 0, this.standardSalt.Length);
			}
			else
			{
				this.digest.BlockUpdate(array2, num + this.recoveredMessage.Length, this.saltLength);
			}
			byte[] array4 = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array4, 0);
			int num3 = array2.Length - num2 - array4.Length;
			bool flag = true;
			for (int num4 = 0; num4 != array4.Length; num4++)
			{
				if (array4[num4] != array2[num3 + num4])
				{
					flag = false;
				}
			}
			this.ClearBlock(array2);
			this.ClearBlock(array4);
			if (!flag)
			{
				this.fullMessage = false;
				this.messageLength = 0;
				this.ClearBlock(this.recoveredMessage);
				return false;
			}
			if (this.messageLength != 0 && !this.IsSameAs(this.mBuf, this.recoveredMessage))
			{
				this.messageLength = 0;
				this.ClearBlock(this.mBuf);
				return false;
			}
			this.messageLength = 0;
			this.ClearBlock(this.mBuf);
			return true;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000CC40C File Offset: 0x000CC40C
		public virtual bool HasFullMessage()
		{
			return this.fullMessage;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000CC414 File Offset: 0x000CC414
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000CC434 File Offset: 0x000CC434
		private void LtoOSP(long l, byte[] sp)
		{
			sp[0] = (byte)((ulong)l >> 56);
			sp[1] = (byte)((ulong)l >> 48);
			sp[2] = (byte)((ulong)l >> 40);
			sp[3] = (byte)((ulong)l >> 32);
			sp[4] = (byte)((ulong)l >> 24);
			sp[5] = (byte)((ulong)l >> 16);
			sp[6] = (byte)((ulong)l >> 8);
			sp[7] = (byte)l;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000CC474 File Offset: 0x000CC474
		private byte[] MaskGeneratorFunction1(byte[] Z, int zOff, int zLen, int length)
		{
			byte[] array = new byte[length];
			byte[] array2 = new byte[this.hLen];
			byte[] array3 = new byte[4];
			int num = 0;
			this.digest.Reset();
			do
			{
				this.ItoOSP(num, array3);
				this.digest.BlockUpdate(Z, zOff, zLen);
				this.digest.BlockUpdate(array3, 0, array3.Length);
				this.digest.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, num * this.hLen, this.hLen);
			}
			while (++num < length / this.hLen);
			if (num * this.hLen < length)
			{
				this.ItoOSP(num, array3);
				this.digest.BlockUpdate(Z, zOff, zLen);
				this.digest.BlockUpdate(array3, 0, array3.Length);
				this.digest.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, num * this.hLen, array.Length - num * this.hLen);
			}
			return array;
		}

		// Token: 0x0400171A RID: 5914
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerImplicit = 188;

		// Token: 0x0400171B RID: 5915
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerRipeMD160 = 12748;

		// Token: 0x0400171C RID: 5916
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerRipeMD128 = 13004;

		// Token: 0x0400171D RID: 5917
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha1 = 13260;

		// Token: 0x0400171E RID: 5918
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha256 = 13516;

		// Token: 0x0400171F RID: 5919
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha512 = 13772;

		// Token: 0x04001720 RID: 5920
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha384 = 14028;

		// Token: 0x04001721 RID: 5921
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerWhirlpool = 14284;

		// Token: 0x04001722 RID: 5922
		private IDigest digest;

		// Token: 0x04001723 RID: 5923
		private IAsymmetricBlockCipher cipher;

		// Token: 0x04001724 RID: 5924
		private SecureRandom random;

		// Token: 0x04001725 RID: 5925
		private byte[] standardSalt;

		// Token: 0x04001726 RID: 5926
		private int hLen;

		// Token: 0x04001727 RID: 5927
		private int trailer;

		// Token: 0x04001728 RID: 5928
		private int keyBits;

		// Token: 0x04001729 RID: 5929
		private byte[] block;

		// Token: 0x0400172A RID: 5930
		private byte[] mBuf;

		// Token: 0x0400172B RID: 5931
		private int messageLength;

		// Token: 0x0400172C RID: 5932
		private readonly int saltLength;

		// Token: 0x0400172D RID: 5933
		private bool fullMessage;

		// Token: 0x0400172E RID: 5934
		private byte[] recoveredMessage;

		// Token: 0x0400172F RID: 5935
		private byte[] preSig;

		// Token: 0x04001730 RID: 5936
		private byte[] preBlock;

		// Token: 0x04001731 RID: 5937
		private int preMStart;

		// Token: 0x04001732 RID: 5938
		private int preTLength;
	}
}
