using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004B2 RID: 1202
	public class X931Signer : ISigner
	{
		// Token: 0x06002510 RID: 9488 RVA: 0x000CE618 File Offset: 0x000CE618
		public X931Signer(IAsymmetricBlockCipher cipher, IDigest digest, bool isImplicit)
		{
			this.cipher = cipher;
			this.digest = digest;
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

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x000CE678 File Offset: 0x000CE678
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "with" + this.cipher.AlgorithmName + "/X9.31";
			}
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000CE6A0 File Offset: 0x000CE6A0
		public X931Signer(IAsymmetricBlockCipher cipher, IDigest digest) : this(cipher, digest, false)
		{
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000CE6AC File Offset: 0x000CE6AC
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.kParam = (RsaKeyParameters)parameters;
			this.cipher.Init(forSigning, this.kParam);
			this.keyBits = this.kParam.Modulus.BitLength;
			this.block = new byte[(this.keyBits + 7) / 8];
			this.Reset();
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000CE70C File Offset: 0x000CE70C
		private void ClearBlock(byte[] block)
		{
			Array.Clear(block, 0, block.Length);
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000CE718 File Offset: 0x000CE718
		public virtual void Update(byte b)
		{
			this.digest.Update(b);
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000CE728 File Offset: 0x000CE728
		public virtual void BlockUpdate(byte[] input, int off, int len)
		{
			this.digest.BlockUpdate(input, off, len);
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000CE738 File Offset: 0x000CE738
		public virtual void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000CE748 File Offset: 0x000CE748
		public virtual byte[] GenerateSignature()
		{
			this.CreateSignatureBlock();
			BigInteger bigInteger = new BigInteger(1, this.cipher.ProcessBlock(this.block, 0, this.block.Length));
			this.ClearBlock(this.block);
			bigInteger = bigInteger.Min(this.kParam.Modulus.Subtract(bigInteger));
			int unsignedByteLength = BigIntegers.GetUnsignedByteLength(this.kParam.Modulus);
			return BigIntegers.AsUnsignedByteArray(unsignedByteLength, bigInteger);
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000CE7BC File Offset: 0x000CE7BC
		private void CreateSignatureBlock()
		{
			int digestSize = this.digest.GetDigestSize();
			int num;
			if (this.trailer == 188)
			{
				num = this.block.Length - digestSize - 1;
				this.digest.DoFinal(this.block, num);
				this.block[this.block.Length - 1] = 188;
			}
			else
			{
				num = this.block.Length - digestSize - 2;
				this.digest.DoFinal(this.block, num);
				this.block[this.block.Length - 2] = (byte)(this.trailer >> 8);
				this.block[this.block.Length - 1] = (byte)this.trailer;
			}
			this.block[0] = 107;
			for (int num2 = num - 2; num2 != 0; num2--)
			{
				this.block[num2] = 187;
			}
			this.block[num - 1] = 186;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000CE8AC File Offset: 0x000CE8AC
		public virtual bool VerifySignature(byte[] signature)
		{
			try
			{
				this.block = this.cipher.ProcessBlock(signature, 0, signature.Length);
			}
			catch (Exception)
			{
				return false;
			}
			BigInteger bigInteger = new BigInteger(1, this.block);
			BigInteger n;
			if ((bigInteger.IntValue & 15) == 12)
			{
				n = bigInteger;
			}
			else
			{
				bigInteger = this.kParam.Modulus.Subtract(bigInteger);
				if ((bigInteger.IntValue & 15) != 12)
				{
					return false;
				}
				n = bigInteger;
			}
			this.CreateSignatureBlock();
			byte[] b = BigIntegers.AsUnsignedByteArray(this.block.Length, n);
			bool result = Arrays.ConstantTimeAreEqual(this.block, b);
			this.ClearBlock(this.block);
			this.ClearBlock(b);
			return result;
		}

		// Token: 0x04001771 RID: 6001
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_IMPLICIT = 188;

		// Token: 0x04001772 RID: 6002
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_RIPEMD160 = 12748;

		// Token: 0x04001773 RID: 6003
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_RIPEMD128 = 13004;

		// Token: 0x04001774 RID: 6004
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_SHA1 = 13260;

		// Token: 0x04001775 RID: 6005
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_SHA256 = 13516;

		// Token: 0x04001776 RID: 6006
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_SHA512 = 13772;

		// Token: 0x04001777 RID: 6007
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_SHA384 = 14028;

		// Token: 0x04001778 RID: 6008
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_WHIRLPOOL = 14284;

		// Token: 0x04001779 RID: 6009
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TRAILER_SHA224 = 14540;

		// Token: 0x0400177A RID: 6010
		private IDigest digest;

		// Token: 0x0400177B RID: 6011
		private IAsymmetricBlockCipher cipher;

		// Token: 0x0400177C RID: 6012
		private RsaKeyParameters kParam;

		// Token: 0x0400177D RID: 6013
		private int trailer;

		// Token: 0x0400177E RID: 6014
		private int keyBits;

		// Token: 0x0400177F RID: 6015
		private byte[] block;
	}
}
