using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003E3 RID: 995
	internal class MacCFBBlockCipher : IBlockCipher
	{
		// Token: 0x06001F7C RID: 8060 RVA: 0x000B734C File Offset: 0x000B734C
		public MacCFBBlockCipher(IBlockCipher cipher, int bitBlockSize)
		{
			this.cipher = cipher;
			this.blockSize = bitBlockSize / 8;
			this.IV = new byte[cipher.GetBlockSize()];
			this.cfbV = new byte[cipher.GetBlockSize()];
			this.cfbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x000B73A8 File Offset: 0x000B73A8
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				if (iv.Length < this.IV.Length)
				{
					Array.Copy(iv, 0, this.IV, this.IV.Length - iv.Length, iv.Length);
				}
				else
				{
					Array.Copy(iv, 0, this.IV, 0, this.IV.Length);
				}
				parameters = parametersWithIV.Parameters;
			}
			this.Reset();
			this.cipher.Init(true, parameters);
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x000B7434 File Offset: 0x000B7434
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CFB" + this.blockSize * 8;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x000B7458 File Offset: 0x000B7458
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000B745C File Offset: 0x000B745C
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x000B7464 File Offset: 0x000B7464
		public int ProcessBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + this.blockSize > outBytes.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			this.cipher.ProcessBlock(this.cfbV, 0, this.cfbOutV, 0);
			for (int i = 0; i < this.blockSize; i++)
			{
				outBytes[outOff + i] = (this.cfbOutV[i] ^ input[inOff + i]);
			}
			Array.Copy(this.cfbV, this.blockSize, this.cfbV, 0, this.cfbV.Length - this.blockSize);
			Array.Copy(outBytes, outOff, this.cfbV, this.cfbV.Length - this.blockSize, this.blockSize);
			return this.blockSize;
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x000B7540 File Offset: 0x000B7540
		public void Reset()
		{
			this.IV.CopyTo(this.cfbV, 0);
			this.cipher.Reset();
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x000B7560 File Offset: 0x000B7560
		public void GetMacBlock(byte[] mac)
		{
			this.cipher.ProcessBlock(this.cfbV, 0, mac, 0);
		}

		// Token: 0x04001498 RID: 5272
		private byte[] IV;

		// Token: 0x04001499 RID: 5273
		private byte[] cfbV;

		// Token: 0x0400149A RID: 5274
		private byte[] cfbOutV;

		// Token: 0x0400149B RID: 5275
		private readonly int blockSize;

		// Token: 0x0400149C RID: 5276
		private readonly IBlockCipher cipher;
	}
}
