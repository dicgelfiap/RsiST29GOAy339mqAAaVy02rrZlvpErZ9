using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004AA RID: 1194
	public class Iso9796d2Signer : ISignerWithRecovery, ISigner
	{
		// Token: 0x060024B5 RID: 9397 RVA: 0x000CC568 File Offset: 0x000CC568
		public byte[] GetRecoveredMessage()
		{
			return this.recoveredMessage;
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000CC570 File Offset: 0x000CC570
		public Iso9796d2Signer(IAsymmetricBlockCipher cipher, IDigest digest, bool isImplicit)
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

		// Token: 0x060024B7 RID: 9399 RVA: 0x000CC5D0 File Offset: 0x000CC5D0
		public Iso9796d2Signer(IAsymmetricBlockCipher cipher, IDigest digest) : this(cipher, digest, false)
		{
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x000CC5DC File Offset: 0x000CC5DC
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "withISO9796-2S1";
			}
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000CC5F4 File Offset: 0x000CC5F4
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)parameters;
			this.cipher.Init(forSigning, rsaKeyParameters);
			this.keyBits = rsaKeyParameters.Modulus.BitLength;
			this.block = new byte[(this.keyBits + 7) / 8];
			if (this.trailer == 188)
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - 2];
			}
			else
			{
				this.mBuf = new byte[this.block.Length - this.digest.GetDigestSize() - 3];
			}
			this.Reset();
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000CC69C File Offset: 0x000CC69C
		private bool IsSameAs(byte[] a, byte[] b)
		{
			int num;
			if (this.messageLength > this.mBuf.Length)
			{
				if (this.mBuf.Length > b.Length)
				{
					return false;
				}
				num = this.mBuf.Length;
			}
			else
			{
				if (this.messageLength != b.Length)
				{
					return false;
				}
				num = b.Length;
			}
			bool result = true;
			for (int num2 = 0; num2 != num; num2++)
			{
				if (a[num2] != b[num2])
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000CC714 File Offset: 0x000CC714
		private void ClearBlock(byte[] block)
		{
			Array.Clear(block, 0, block.Length);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000CC720 File Offset: 0x000CC720
		public virtual void UpdateWithRecoveredMessage(byte[] signature)
		{
			byte[] array = this.cipher.ProcessBlock(signature, 0, signature.Length);
			if (((array[0] & 192) ^ 64) != 0)
			{
				throw new InvalidCipherTextException("malformed signature");
			}
			if (((array[array.Length - 1] & 15) ^ 12) != 0)
			{
				throw new InvalidCipherTextException("malformed signature");
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
			int num3 = 0;
			while (num3 != array.Length && ((array[num3] & 15) ^ 10) != 0)
			{
				num3++;
			}
			num3++;
			int num4 = array.Length - num - this.digest.GetDigestSize();
			if (num4 - num3 <= 0)
			{
				throw new InvalidCipherTextException("malformed block");
			}
			if ((array[0] & 32) == 0)
			{
				this.fullMessage = true;
				this.recoveredMessage = new byte[num4 - num3];
				Array.Copy(array, num3, this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			else
			{
				this.fullMessage = false;
				this.recoveredMessage = new byte[num4 - num3];
				Array.Copy(array, num3, this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			this.preSig = signature;
			this.preBlock = array;
			this.digest.BlockUpdate(this.recoveredMessage, 0, this.recoveredMessage.Length);
			this.messageLength = this.recoveredMessage.Length;
			this.recoveredMessage.CopyTo(this.mBuf, 0);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000CC8FC File Offset: 0x000CC8FC
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
			if (this.messageLength < this.mBuf.Length)
			{
				this.mBuf[this.messageLength] = input;
			}
			this.messageLength++;
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000CC93C File Offset: 0x000CC93C
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			while (length > 0 && this.messageLength < this.mBuf.Length)
			{
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
			this.digest.BlockUpdate(input, inOff, length);
			this.messageLength += length;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000CC998 File Offset: 0x000CC998
		public virtual void Reset()
		{
			this.digest.Reset();
			this.messageLength = 0;
			this.ClearBlock(this.mBuf);
			if (this.recoveredMessage != null)
			{
				this.ClearBlock(this.recoveredMessage);
			}
			this.recoveredMessage = null;
			this.fullMessage = false;
			if (this.preSig != null)
			{
				this.preSig = null;
				this.ClearBlock(this.preBlock);
				this.preBlock = null;
			}
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000CCA14 File Offset: 0x000CCA14
		public virtual byte[] GenerateSignature()
		{
			int digestSize = this.digest.GetDigestSize();
			int num;
			int num2;
			if (this.trailer == 188)
			{
				num = 8;
				num2 = this.block.Length - digestSize - 1;
				this.digest.DoFinal(this.block, num2);
				this.block[this.block.Length - 1] = 188;
			}
			else
			{
				num = 16;
				num2 = this.block.Length - digestSize - 2;
				this.digest.DoFinal(this.block, num2);
				this.block[this.block.Length - 2] = (byte)((uint)this.trailer >> 8);
				this.block[this.block.Length - 1] = (byte)this.trailer;
			}
			int num3 = (digestSize + this.messageLength) * 8 + num + 4 - this.keyBits;
			byte b;
			if (num3 > 0)
			{
				int num4 = this.messageLength - (num3 + 7) / 8;
				b = 96;
				num2 -= num4;
				Array.Copy(this.mBuf, 0, this.block, num2, num4);
			}
			else
			{
				b = 64;
				num2 -= this.messageLength;
				Array.Copy(this.mBuf, 0, this.block, num2, this.messageLength);
			}
			if (num2 - 1 > 0)
			{
				for (int num5 = num2 - 1; num5 != 0; num5--)
				{
					this.block[num5] = 187;
				}
				byte[] array;
				IntPtr intPtr;
				(array = this.block)[(int)(intPtr = (IntPtr)(num2 - 1))] = (array[(int)intPtr] ^ 1);
				this.block[0] = 11;
				(array = this.block)[0] = (array[0] | b);
			}
			else
			{
				this.block[0] = 10;
				byte[] array;
				(array = this.block)[0] = (array[0] | b);
			}
			byte[] result = this.cipher.ProcessBlock(this.block, 0, this.block.Length);
			this.messageLength = 0;
			this.ClearBlock(this.mBuf);
			this.ClearBlock(this.block);
			return result;
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000CCC0C File Offset: 0x000CCC0C
		public virtual bool VerifySignature(byte[] signature)
		{
			byte[] array;
			if (this.preSig == null)
			{
				try
				{
					array = this.cipher.ProcessBlock(signature, 0, signature.Length);
					goto IL_5A;
				}
				catch (Exception)
				{
					return false;
				}
			}
			if (!Arrays.AreEqual(this.preSig, signature))
			{
				throw new InvalidOperationException("updateWithRecoveredMessage called on different signature");
			}
			array = this.preBlock;
			this.preSig = null;
			this.preBlock = null;
			IL_5A:
			if (((array[0] & 192) ^ 64) != 0)
			{
				return this.ReturnFalse(array);
			}
			if (((array[array.Length - 1] & 15) ^ 12) != 0)
			{
				return this.ReturnFalse(array);
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
			int num3 = 0;
			while (num3 != array.Length && ((array[num3] & 15) ^ 10) != 0)
			{
				num3++;
			}
			num3++;
			byte[] array2 = new byte[this.digest.GetDigestSize()];
			int num4 = array.Length - num - array2.Length;
			if (num4 - num3 <= 0)
			{
				return this.ReturnFalse(array);
			}
			if ((array[0] & 32) == 0)
			{
				this.fullMessage = true;
				if (this.messageLength > num4 - num3)
				{
					return this.ReturnFalse(array);
				}
				this.digest.Reset();
				this.digest.BlockUpdate(array, num3, num4 - num3);
				this.digest.DoFinal(array2, 0);
				bool flag = true;
				for (int num5 = 0; num5 != array2.Length; num5++)
				{
					byte[] array3;
					IntPtr intPtr;
					(array3 = array)[(int)(intPtr = (IntPtr)(num4 + num5))] = (array3[(int)intPtr] ^ array2[num5]);
					if (array[num4 + num5] != 0)
					{
						flag = false;
					}
				}
				if (!flag)
				{
					return this.ReturnFalse(array);
				}
				this.recoveredMessage = new byte[num4 - num3];
				Array.Copy(array, num3, this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			else
			{
				this.fullMessage = false;
				this.digest.DoFinal(array2, 0);
				bool flag2 = true;
				for (int num6 = 0; num6 != array2.Length; num6++)
				{
					byte[] array3;
					IntPtr intPtr;
					(array3 = array)[(int)(intPtr = (IntPtr)(num4 + num6))] = (array3[(int)intPtr] ^ array2[num6]);
					if (array[num4 + num6] != 0)
					{
						flag2 = false;
					}
				}
				if (!flag2)
				{
					return this.ReturnFalse(array);
				}
				this.recoveredMessage = new byte[num4 - num3];
				Array.Copy(array, num3, this.recoveredMessage, 0, this.recoveredMessage.Length);
			}
			if (this.messageLength != 0 && !this.IsSameAs(this.mBuf, this.recoveredMessage))
			{
				return this.ReturnFalse(array);
			}
			this.ClearBlock(this.mBuf);
			this.ClearBlock(array);
			this.messageLength = 0;
			return true;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000CCF3C File Offset: 0x000CCF3C
		private bool ReturnFalse(byte[] block)
		{
			this.messageLength = 0;
			this.ClearBlock(this.mBuf);
			this.ClearBlock(block);
			return false;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000CCF5C File Offset: 0x000CCF5C
		public virtual bool HasFullMessage()
		{
			return this.fullMessage;
		}

		// Token: 0x04001733 RID: 5939
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerImplicit = 188;

		// Token: 0x04001734 RID: 5940
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerRipeMD160 = 12748;

		// Token: 0x04001735 RID: 5941
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerRipeMD128 = 13004;

		// Token: 0x04001736 RID: 5942
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha1 = 13260;

		// Token: 0x04001737 RID: 5943
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha256 = 13516;

		// Token: 0x04001738 RID: 5944
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha512 = 13772;

		// Token: 0x04001739 RID: 5945
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerSha384 = 14028;

		// Token: 0x0400173A RID: 5946
		[Obsolete("Use 'IsoTrailers' instead")]
		public const int TrailerWhirlpool = 14284;

		// Token: 0x0400173B RID: 5947
		private IDigest digest;

		// Token: 0x0400173C RID: 5948
		private IAsymmetricBlockCipher cipher;

		// Token: 0x0400173D RID: 5949
		private int trailer;

		// Token: 0x0400173E RID: 5950
		private int keyBits;

		// Token: 0x0400173F RID: 5951
		private byte[] block;

		// Token: 0x04001740 RID: 5952
		private byte[] mBuf;

		// Token: 0x04001741 RID: 5953
		private int messageLength;

		// Token: 0x04001742 RID: 5954
		private bool fullMessage;

		// Token: 0x04001743 RID: 5955
		private byte[] recoveredMessage;

		// Token: 0x04001744 RID: 5956
		private byte[] preSig;

		// Token: 0x04001745 RID: 5957
		private byte[] preBlock;
	}
}
