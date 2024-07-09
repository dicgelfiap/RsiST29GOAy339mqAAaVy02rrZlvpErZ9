using System;
using Org.BouncyCastle.Crypto.Paddings;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003E4 RID: 996
	public class CfbBlockCipherMac : IMac
	{
		// Token: 0x06001F84 RID: 8068 RVA: 0x000B7578 File Offset: 0x000B7578
		public CfbBlockCipherMac(IBlockCipher cipher) : this(cipher, 8, cipher.GetBlockSize() * 8 / 2, null)
		{
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000B7590 File Offset: 0x000B7590
		public CfbBlockCipherMac(IBlockCipher cipher, IBlockCipherPadding padding) : this(cipher, 8, cipher.GetBlockSize() * 8 / 2, padding)
		{
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000B75A8 File Offset: 0x000B75A8
		public CfbBlockCipherMac(IBlockCipher cipher, int cfbBitSize, int macSizeInBits) : this(cipher, cfbBitSize, macSizeInBits, null)
		{
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000B75B4 File Offset: 0x000B75B4
		public CfbBlockCipherMac(IBlockCipher cipher, int cfbBitSize, int macSizeInBits, IBlockCipherPadding padding)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			this.mac = new byte[cipher.GetBlockSize()];
			this.cipher = new MacCFBBlockCipher(cipher, cfbBitSize);
			this.padding = padding;
			this.macSize = macSizeInBits / 8;
			this.Buffer = new byte[this.cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x000B762C File Offset: 0x000B762C
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x000B763C File Offset: 0x000B763C
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			this.cipher.Init(true, parameters);
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000B7654 File Offset: 0x000B7654
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000B765C File Offset: 0x000B765C
		public void Update(byte input)
		{
			if (this.bufOff == this.Buffer.Length)
			{
				this.cipher.ProcessBlock(this.Buffer, 0, this.mac, 0);
				this.bufOff = 0;
			}
			this.Buffer[this.bufOff++] = input;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000B76BC File Offset: 0x000B76BC
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = 0;
			int num2 = blockSize - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.Buffer, this.bufOff, num2);
				num += this.cipher.ProcessBlock(this.Buffer, 0, this.mac, 0);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				while (len > blockSize)
				{
					num += this.cipher.ProcessBlock(input, inOff, this.mac, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(input, inOff, this.Buffer, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x000B778C File Offset: 0x000B778C
		public int DoFinal(byte[] output, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			if (this.padding == null)
			{
				while (this.bufOff < blockSize)
				{
					this.Buffer[this.bufOff++] = 0;
				}
			}
			else
			{
				this.padding.AddPadding(this.Buffer, this.bufOff);
			}
			this.cipher.ProcessBlock(this.Buffer, 0, this.mac, 0);
			this.cipher.GetMacBlock(this.mac);
			Array.Copy(this.mac, 0, output, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000B7844 File Offset: 0x000B7844
		public void Reset()
		{
			Array.Clear(this.Buffer, 0, this.Buffer.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x0400149D RID: 5277
		private byte[] mac;

		// Token: 0x0400149E RID: 5278
		private byte[] Buffer;

		// Token: 0x0400149F RID: 5279
		private int bufOff;

		// Token: 0x040014A0 RID: 5280
		private MacCFBBlockCipher cipher;

		// Token: 0x040014A1 RID: 5281
		private IBlockCipherPadding padding;

		// Token: 0x040014A2 RID: 5282
		private int macSize;
	}
}
