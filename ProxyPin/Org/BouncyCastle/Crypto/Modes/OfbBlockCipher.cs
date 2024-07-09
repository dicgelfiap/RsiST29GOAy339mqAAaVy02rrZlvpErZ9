using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000408 RID: 1032
	public class OfbBlockCipher : IBlockCipher
	{
		// Token: 0x06002138 RID: 8504 RVA: 0x000C13BC File Offset: 0x000C13BC
		public OfbBlockCipher(IBlockCipher cipher, int blockSize)
		{
			this.cipher = cipher;
			this.blockSize = blockSize / 8;
			this.IV = new byte[cipher.GetBlockSize()];
			this.ofbV = new byte[cipher.GetBlockSize()];
			this.ofbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000C1418 File Offset: 0x000C1418
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000C1420 File Offset: 0x000C1420
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				if (iv.Length < this.IV.Length)
				{
					Array.Copy(iv, 0, this.IV, this.IV.Length - iv.Length, iv.Length);
					for (int i = 0; i < this.IV.Length - iv.Length; i++)
					{
						this.IV[i] = 0;
					}
				}
				else
				{
					Array.Copy(iv, 0, this.IV, 0, this.IV.Length);
				}
				parameters = parametersWithIV.Parameters;
			}
			this.Reset();
			if (parameters != null)
			{
				this.cipher.Init(true, parameters);
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x000C14D8 File Offset: 0x000C14D8
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/OFB" + this.blockSize * 8;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x000C14FC File Offset: 0x000C14FC
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000C1500 File Offset: 0x000C1500
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000C1508 File Offset: 0x000C1508
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + this.blockSize > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			this.cipher.ProcessBlock(this.ofbV, 0, this.ofbOutV, 0);
			for (int i = 0; i < this.blockSize; i++)
			{
				output[outOff + i] = (this.ofbOutV[i] ^ input[inOff + i]);
			}
			Array.Copy(this.ofbV, this.blockSize, this.ofbV, 0, this.ofbV.Length - this.blockSize);
			Array.Copy(this.ofbOutV, 0, this.ofbV, this.ofbV.Length - this.blockSize, this.blockSize);
			return this.blockSize;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000C15E8 File Offset: 0x000C15E8
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.ofbV, 0, this.IV.Length);
			this.cipher.Reset();
		}

		// Token: 0x0400159F RID: 5535
		private byte[] IV;

		// Token: 0x040015A0 RID: 5536
		private byte[] ofbV;

		// Token: 0x040015A1 RID: 5537
		private byte[] ofbOutV;

		// Token: 0x040015A2 RID: 5538
		private readonly int blockSize;

		// Token: 0x040015A3 RID: 5539
		private readonly IBlockCipher cipher;
	}
}
