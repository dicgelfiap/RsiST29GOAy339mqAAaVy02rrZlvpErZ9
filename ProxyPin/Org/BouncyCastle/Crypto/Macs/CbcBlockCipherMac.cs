using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003E2 RID: 994
	public class CbcBlockCipherMac : IMac
	{
		// Token: 0x06001F71 RID: 8049 RVA: 0x000B7064 File Offset: 0x000B7064
		public CbcBlockCipherMac(IBlockCipher cipher) : this(cipher, cipher.GetBlockSize() * 8 / 2, null)
		{
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000B7078 File Offset: 0x000B7078
		public CbcBlockCipherMac(IBlockCipher cipher, IBlockCipherPadding padding) : this(cipher, cipher.GetBlockSize() * 8 / 2, padding)
		{
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x000B708C File Offset: 0x000B708C
		public CbcBlockCipherMac(IBlockCipher cipher, int macSizeInBits) : this(cipher, macSizeInBits, null)
		{
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000B7098 File Offset: 0x000B7098
		public CbcBlockCipherMac(IBlockCipher cipher, int macSizeInBits, IBlockCipherPadding padding)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			this.cipher = new CbcBlockCipher(cipher);
			this.padding = padding;
			this.macSize = macSizeInBits / 8;
			this.buf = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x000B70F8 File Offset: 0x000B70F8
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000B7108 File Offset: 0x000B7108
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			this.cipher.Init(true, parameters);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x000B7120 File Offset: 0x000B7120
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000B7128 File Offset: 0x000B7128
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
				this.bufOff = 0;
			}
			this.buf[this.bufOff++] = input;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x000B7188 File Offset: 0x000B7188
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = blockSize - this.bufOff;
			if (len > num)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num);
				this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
				this.bufOff = 0;
				len -= num;
				inOff += num;
				while (len > blockSize)
				{
					this.cipher.ProcessBlock(input, inOff, this.buf, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000B7250 File Offset: 0x000B7250
		public int DoFinal(byte[] output, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			if (this.padding == null)
			{
				while (this.bufOff < blockSize)
				{
					this.buf[this.bufOff++] = 0;
				}
			}
			else
			{
				if (this.bufOff == blockSize)
				{
					this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
					this.bufOff = 0;
				}
				this.padding.AddPadding(this.buf, this.bufOff);
			}
			this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
			Array.Copy(this.buf, 0, output, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000B7324 File Offset: 0x000B7324
		public void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x04001493 RID: 5267
		private byte[] buf;

		// Token: 0x04001494 RID: 5268
		private int bufOff;

		// Token: 0x04001495 RID: 5269
		private IBlockCipher cipher;

		// Token: 0x04001496 RID: 5270
		private IBlockCipherPadding padding;

		// Token: 0x04001497 RID: 5271
		private int macSize;
	}
}
