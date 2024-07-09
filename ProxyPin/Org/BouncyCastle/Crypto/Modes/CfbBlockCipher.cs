using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x020003FC RID: 1020
	public class CfbBlockCipher : IBlockCipher
	{
		// Token: 0x06002068 RID: 8296 RVA: 0x000BC594 File Offset: 0x000BC594
		public CfbBlockCipher(IBlockCipher cipher, int bitBlockSize)
		{
			this.cipher = cipher;
			this.blockSize = bitBlockSize / 8;
			this.IV = new byte[cipher.GetBlockSize()];
			this.cfbV = new byte[cipher.GetBlockSize()];
			this.cfbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x000BC5F0 File Offset: 0x000BC5F0
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x000BC5F8 File Offset: 0x000BC5F8
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.encrypting = forEncryption;
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				int num = this.IV.Length - iv.Length;
				Array.Copy(iv, 0, this.IV, num, iv.Length);
				Array.Clear(this.IV, 0, num);
				parameters = parametersWithIV.Parameters;
			}
			this.Reset();
			if (parameters != null)
			{
				this.cipher.Init(true, parameters);
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x000BC678 File Offset: 0x000BC678
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CFB" + this.blockSize * 8;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x000BC69C File Offset: 0x000BC69C
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000BC6A0 File Offset: 0x000BC6A0
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x000BC6A8 File Offset: 0x000BC6A8
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.encrypting)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x000BC6CC File Offset: 0x000BC6CC
		public int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
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

		// Token: 0x06002070 RID: 8304 RVA: 0x000BC7A8 File Offset: 0x000BC7A8
		public int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
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
			Array.Copy(this.cfbV, this.blockSize, this.cfbV, 0, this.cfbV.Length - this.blockSize);
			Array.Copy(input, inOff, this.cfbV, this.cfbV.Length - this.blockSize, this.blockSize);
			for (int i = 0; i < this.blockSize; i++)
			{
				outBytes[outOff + i] = (this.cfbOutV[i] ^ input[inOff + i]);
			}
			return this.blockSize;
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000BC884 File Offset: 0x000BC884
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.cfbV, 0, this.IV.Length);
			this.cipher.Reset();
		}

		// Token: 0x04001522 RID: 5410
		private byte[] IV;

		// Token: 0x04001523 RID: 5411
		private byte[] cfbV;

		// Token: 0x04001524 RID: 5412
		private byte[] cfbOutV;

		// Token: 0x04001525 RID: 5413
		private bool encrypting;

		// Token: 0x04001526 RID: 5414
		private readonly int blockSize;

		// Token: 0x04001527 RID: 5415
		private readonly IBlockCipher cipher;
	}
}
