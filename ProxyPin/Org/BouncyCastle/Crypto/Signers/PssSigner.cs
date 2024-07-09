using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004AD RID: 1197
	public class PssSigner : ISigner
	{
		// Token: 0x060024D0 RID: 9424 RVA: 0x000CD1D8 File Offset: 0x000CD1D8
		public static PssSigner CreateRawSigner(IAsymmetricBlockCipher cipher, IDigest digest)
		{
			return new PssSigner(cipher, new NullDigest(), digest, digest, digest.GetDigestSize(), null, 188);
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000CD204 File Offset: 0x000CD204
		public static PssSigner CreateRawSigner(IAsymmetricBlockCipher cipher, IDigest contentDigest, IDigest mgfDigest, int saltLen, byte trailer)
		{
			return new PssSigner(cipher, new NullDigest(), contentDigest, mgfDigest, saltLen, null, trailer);
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000CD218 File Offset: 0x000CD218
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest digest) : this(cipher, digest, digest.GetDigestSize())
		{
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000CD228 File Offset: 0x000CD228
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLen) : this(cipher, digest, saltLen, 188)
		{
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000CD238 File Offset: 0x000CD238
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, byte[] salt) : this(cipher, digest, digest, digest, salt.Length, salt, 188)
		{
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000CD25C File Offset: 0x000CD25C
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest contentDigest, IDigest mgfDigest, int saltLen) : this(cipher, contentDigest, mgfDigest, saltLen, 188)
		{
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000CD270 File Offset: 0x000CD270
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest contentDigest, IDigest mgfDigest, byte[] salt) : this(cipher, contentDigest, contentDigest, mgfDigest, salt.Length, salt, 188)
		{
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000CD298 File Offset: 0x000CD298
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest digest, int saltLen, byte trailer) : this(cipher, digest, digest, saltLen, trailer)
		{
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000CD2A8 File Offset: 0x000CD2A8
		public PssSigner(IAsymmetricBlockCipher cipher, IDigest contentDigest, IDigest mgfDigest, int saltLen, byte trailer) : this(cipher, contentDigest, contentDigest, mgfDigest, saltLen, null, trailer)
		{
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000CD2C8 File Offset: 0x000CD2C8
		private PssSigner(IAsymmetricBlockCipher cipher, IDigest contentDigest1, IDigest contentDigest2, IDigest mgfDigest, int saltLen, byte[] salt, byte trailer)
		{
			this.cipher = cipher;
			this.contentDigest1 = contentDigest1;
			this.contentDigest2 = contentDigest2;
			this.mgfDigest = mgfDigest;
			this.hLen = contentDigest2.GetDigestSize();
			this.mgfhLen = mgfDigest.GetDigestSize();
			this.sLen = saltLen;
			this.sSet = (salt != null);
			if (this.sSet)
			{
				this.salt = salt;
			}
			else
			{
				this.salt = new byte[saltLen];
			}
			this.mDash = new byte[8 + saltLen + this.hLen];
			this.trailer = trailer;
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x000CD370 File Offset: 0x000CD370
		public virtual string AlgorithmName
		{
			get
			{
				return this.mgfDigest.AlgorithmName + "withRSAandMGF1";
			}
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000CD388 File Offset: 0x000CD388
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				parameters = parametersWithRandom.Parameters;
				this.random = parametersWithRandom.Random;
			}
			else if (forSigning)
			{
				this.random = new SecureRandom();
			}
			this.cipher.Init(forSigning, parameters);
			RsaKeyParameters rsaKeyParameters;
			if (parameters is RsaBlindingParameters)
			{
				rsaKeyParameters = ((RsaBlindingParameters)parameters).PublicKey;
			}
			else
			{
				rsaKeyParameters = (RsaKeyParameters)parameters;
			}
			this.emBits = rsaKeyParameters.Modulus.BitLength - 1;
			if (this.emBits < 8 * this.hLen + 8 * this.sLen + 9)
			{
				throw new ArgumentException("key too small for specified hash and salt lengths");
			}
			this.block = new byte[(this.emBits + 7) / 8];
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000CD458 File Offset: 0x000CD458
		private void ClearBlock(byte[] block)
		{
			Array.Clear(block, 0, block.Length);
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000CD464 File Offset: 0x000CD464
		public virtual void Update(byte input)
		{
			this.contentDigest1.Update(input);
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000CD474 File Offset: 0x000CD474
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.contentDigest1.BlockUpdate(input, inOff, length);
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000CD484 File Offset: 0x000CD484
		public virtual void Reset()
		{
			this.contentDigest1.Reset();
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000CD494 File Offset: 0x000CD494
		public virtual byte[] GenerateSignature()
		{
			this.contentDigest1.DoFinal(this.mDash, this.mDash.Length - this.hLen - this.sLen);
			if (this.sLen != 0)
			{
				if (!this.sSet)
				{
					this.random.NextBytes(this.salt);
				}
				this.salt.CopyTo(this.mDash, this.mDash.Length - this.sLen);
			}
			byte[] array = new byte[this.hLen];
			this.contentDigest2.BlockUpdate(this.mDash, 0, this.mDash.Length);
			this.contentDigest2.DoFinal(array, 0);
			this.block[this.block.Length - this.sLen - 1 - this.hLen - 1] = 1;
			this.salt.CopyTo(this.block, this.block.Length - this.sLen - this.hLen - 1);
			byte[] array2 = this.MaskGeneratorFunction1(array, 0, array.Length, this.block.Length - this.hLen - 1);
			byte[] array3;
			for (int num = 0; num != array2.Length; num++)
			{
				IntPtr intPtr;
				(array3 = this.block)[(int)(intPtr = (IntPtr)num)] = (array3[(int)intPtr] ^ array2[num]);
			}
			array.CopyTo(this.block, this.block.Length - this.hLen - 1);
			uint num2 = 255U >> this.block.Length * 8 - this.emBits;
			(array3 = this.block)[0] = (array3[0] & (byte)num2);
			this.block[this.block.Length - 1] = this.trailer;
			byte[] result = this.cipher.ProcessBlock(this.block, 0, this.block.Length);
			this.ClearBlock(this.block);
			return result;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000CD664 File Offset: 0x000CD664
		public virtual bool VerifySignature(byte[] signature)
		{
			this.contentDigest1.DoFinal(this.mDash, this.mDash.Length - this.hLen - this.sLen);
			byte[] array = this.cipher.ProcessBlock(signature, 0, signature.Length);
			Arrays.Fill(this.block, 0, this.block.Length - array.Length, 0);
			array.CopyTo(this.block, this.block.Length - array.Length);
			uint num = 255U >> this.block.Length * 8 - this.emBits;
			if (this.block[0] != (byte)((uint)this.block[0] & num) || this.block[this.block.Length - 1] != this.trailer)
			{
				this.ClearBlock(this.block);
				return false;
			}
			byte[] array2 = this.MaskGeneratorFunction1(this.block, this.block.Length - this.hLen - 1, this.hLen, this.block.Length - this.hLen - 1);
			byte[] array3;
			for (int num2 = 0; num2 != array2.Length; num2++)
			{
				IntPtr intPtr;
				(array3 = this.block)[(int)(intPtr = (IntPtr)num2)] = (array3[(int)intPtr] ^ array2[num2]);
			}
			(array3 = this.block)[0] = (array3[0] & (byte)num);
			for (int num3 = 0; num3 != this.block.Length - this.hLen - this.sLen - 2; num3++)
			{
				if (this.block[num3] != 0)
				{
					this.ClearBlock(this.block);
					return false;
				}
			}
			if (this.block[this.block.Length - this.hLen - this.sLen - 2] != 1)
			{
				this.ClearBlock(this.block);
				return false;
			}
			if (this.sSet)
			{
				Array.Copy(this.salt, 0, this.mDash, this.mDash.Length - this.sLen, this.sLen);
			}
			else
			{
				Array.Copy(this.block, this.block.Length - this.sLen - this.hLen - 1, this.mDash, this.mDash.Length - this.sLen, this.sLen);
			}
			this.contentDigest2.BlockUpdate(this.mDash, 0, this.mDash.Length);
			this.contentDigest2.DoFinal(this.mDash, this.mDash.Length - this.hLen);
			int num4 = this.block.Length - this.hLen - 1;
			for (int num5 = this.mDash.Length - this.hLen; num5 != this.mDash.Length; num5++)
			{
				if ((this.block[num4] ^ this.mDash[num5]) != 0)
				{
					this.ClearBlock(this.mDash);
					this.ClearBlock(this.block);
					return false;
				}
				num4++;
			}
			this.ClearBlock(this.mDash);
			this.ClearBlock(this.block);
			return true;
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000CD964 File Offset: 0x000CD964
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000CD984 File Offset: 0x000CD984
		private byte[] MaskGeneratorFunction1(byte[] Z, int zOff, int zLen, int length)
		{
			byte[] array = new byte[length];
			byte[] array2 = new byte[this.mgfhLen];
			byte[] array3 = new byte[4];
			int i = 0;
			this.mgfDigest.Reset();
			while (i < length / this.mgfhLen)
			{
				this.ItoOSP(i, array3);
				this.mgfDigest.BlockUpdate(Z, zOff, zLen);
				this.mgfDigest.BlockUpdate(array3, 0, array3.Length);
				this.mgfDigest.DoFinal(array2, 0);
				array2.CopyTo(array, i * this.mgfhLen);
				i++;
			}
			if (i * this.mgfhLen < length)
			{
				this.ItoOSP(i, array3);
				this.mgfDigest.BlockUpdate(Z, zOff, zLen);
				this.mgfDigest.BlockUpdate(array3, 0, array3.Length);
				this.mgfDigest.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, i * this.mgfhLen, array.Length - i * this.mgfhLen);
			}
			return array;
		}

		// Token: 0x04001753 RID: 5971
		public const byte TrailerImplicit = 188;

		// Token: 0x04001754 RID: 5972
		private readonly IDigest contentDigest1;

		// Token: 0x04001755 RID: 5973
		private readonly IDigest contentDigest2;

		// Token: 0x04001756 RID: 5974
		private readonly IDigest mgfDigest;

		// Token: 0x04001757 RID: 5975
		private readonly IAsymmetricBlockCipher cipher;

		// Token: 0x04001758 RID: 5976
		private SecureRandom random;

		// Token: 0x04001759 RID: 5977
		private int hLen;

		// Token: 0x0400175A RID: 5978
		private int mgfhLen;

		// Token: 0x0400175B RID: 5979
		private int sLen;

		// Token: 0x0400175C RID: 5980
		private bool sSet;

		// Token: 0x0400175D RID: 5981
		private int emBits;

		// Token: 0x0400175E RID: 5982
		private byte[] salt;

		// Token: 0x0400175F RID: 5983
		private byte[] mDash;

		// Token: 0x04001760 RID: 5984
		private byte[] block;

		// Token: 0x04001761 RID: 5985
		private byte trailer;
	}
}
