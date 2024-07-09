using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000404 RID: 1028
	public class GOfbBlockCipher : IBlockCipher
	{
		// Token: 0x060020F0 RID: 8432 RVA: 0x000BF3FC File Offset: 0x000BF3FC
		public GOfbBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			if (this.blockSize != 8)
			{
				throw new ArgumentException("GCTR only for 64 bit block ciphers");
			}
			this.IV = new byte[cipher.GetBlockSize()];
			this.ofbV = new byte[cipher.GetBlockSize()];
			this.ofbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000BF478 File Offset: 0x000BF478
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000BF480 File Offset: 0x000BF480
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.firstStep = true;
			this.N3 = 0;
			this.N4 = 0;
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

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x000BF54C File Offset: 0x000BF54C
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/GCTR";
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x000BF564 File Offset: 0x000BF564
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000BF568 File Offset: 0x000BF568
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000BF570 File Offset: 0x000BF570
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
			if (this.firstStep)
			{
				this.firstStep = false;
				this.cipher.ProcessBlock(this.ofbV, 0, this.ofbOutV, 0);
				this.N3 = this.bytesToint(this.ofbOutV, 0);
				this.N4 = this.bytesToint(this.ofbOutV, 4);
			}
			this.N3 += 16843009;
			this.N4 += 16843012;
			if (this.N4 < 16843012 && this.N4 > 0)
			{
				this.N4++;
			}
			this.intTobytes(this.N3, this.ofbV, 0);
			this.intTobytes(this.N4, this.ofbV, 4);
			this.cipher.ProcessBlock(this.ofbV, 0, this.ofbOutV, 0);
			for (int i = 0; i < this.blockSize; i++)
			{
				output[outOff + i] = (this.ofbOutV[i] ^ input[inOff + i]);
			}
			Array.Copy(this.ofbV, this.blockSize, this.ofbV, 0, this.ofbV.Length - this.blockSize);
			Array.Copy(this.ofbOutV, 0, this.ofbV, this.ofbV.Length - this.blockSize, this.blockSize);
			return this.blockSize;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000BF718 File Offset: 0x000BF718
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.ofbV, 0, this.IV.Length);
			this.cipher.Reset();
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000BF740 File Offset: 0x000BF740
		private int bytesToint(byte[] inBytes, int inOff)
		{
			return (int)((long)((long)inBytes[inOff + 3] << 24) & (long)((ulong)-16777216)) + ((int)inBytes[inOff + 2] << 16 & 16711680) + ((int)inBytes[inOff + 1] << 8 & 65280) + (int)(inBytes[inOff] & byte.MaxValue);
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000BF77C File Offset: 0x000BF77C
		private void intTobytes(int num, byte[] outBytes, int outOff)
		{
			outBytes[outOff + 3] = (byte)(num >> 24);
			outBytes[outOff + 2] = (byte)(num >> 16);
			outBytes[outOff + 1] = (byte)(num >> 8);
			outBytes[outOff] = (byte)num;
		}

		// Token: 0x04001565 RID: 5477
		private const int C1 = 16843012;

		// Token: 0x04001566 RID: 5478
		private const int C2 = 16843009;

		// Token: 0x04001567 RID: 5479
		private byte[] IV;

		// Token: 0x04001568 RID: 5480
		private byte[] ofbV;

		// Token: 0x04001569 RID: 5481
		private byte[] ofbOutV;

		// Token: 0x0400156A RID: 5482
		private readonly int blockSize;

		// Token: 0x0400156B RID: 5483
		private readonly IBlockCipher cipher;

		// Token: 0x0400156C RID: 5484
		private bool firstStep = true;

		// Token: 0x0400156D RID: 5485
		private int N3;

		// Token: 0x0400156E RID: 5486
		private int N4;
	}
}
