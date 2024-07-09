using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x020003F8 RID: 1016
	public class CbcBlockCipher : IBlockCipher
	{
		// Token: 0x0600203C RID: 8252 RVA: 0x000BBAB4 File Offset: 0x000BBAB4
		public CbcBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			this.IV = new byte[this.blockSize];
			this.cbcV = new byte[this.blockSize];
			this.cbcNextV = new byte[this.blockSize];
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x000BBB14 File Offset: 0x000BBB14
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000BBB1C File Offset: 0x000BBB1C
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			bool flag = this.encrypting;
			this.encrypting = forEncryption;
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				if (iv.Length != this.blockSize)
				{
					throw new ArgumentException("initialisation vector must be the same length as block size");
				}
				Array.Copy(iv, 0, this.IV, 0, iv.Length);
				parameters = parametersWithIV.Parameters;
			}
			this.Reset();
			if (parameters != null)
			{
				this.cipher.Init(this.encrypting, parameters);
				return;
			}
			if (flag != this.encrypting)
			{
				throw new ArgumentException("cannot change encrypting state without providing key.");
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x000BBBBC File Offset: 0x000BBBBC
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CBC";
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x000BBBD4 File Offset: 0x000BBBD4
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000BBBD8 File Offset: 0x000BBBD8
		public int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000BBBE8 File Offset: 0x000BBBE8
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.encrypting)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000BBC0C File Offset: 0x000BBC0C
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.cbcV, 0, this.IV.Length);
			Array.Clear(this.cbcNextV, 0, this.cbcNextV.Length);
			this.cipher.Reset();
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000BBC48 File Offset: 0x000BBC48
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			for (int i = 0; i < this.blockSize; i++)
			{
				byte[] array;
				IntPtr intPtr;
				(array = this.cbcV)[(int)(intPtr = (IntPtr)i)] = (array[(int)intPtr] ^ input[inOff + i]);
			}
			int result = this.cipher.ProcessBlock(this.cbcV, 0, outBytes, outOff);
			Array.Copy(outBytes, outOff, this.cbcV, 0, this.cbcV.Length);
			return result;
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x000BBCCC File Offset: 0x000BBCCC
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			Array.Copy(input, inOff, this.cbcNextV, 0, this.blockSize);
			int result = this.cipher.ProcessBlock(input, inOff, outBytes, outOff);
			for (int i = 0; i < this.blockSize; i++)
			{
				IntPtr intPtr;
				outBytes[(int)(intPtr = (IntPtr)(outOff + i))] = (outBytes[(int)intPtr] ^ this.cbcV[i]);
			}
			byte[] array = this.cbcV;
			this.cbcV = this.cbcNextV;
			this.cbcNextV = array;
			return result;
		}

		// Token: 0x04001512 RID: 5394
		private byte[] IV;

		// Token: 0x04001513 RID: 5395
		private byte[] cbcV;

		// Token: 0x04001514 RID: 5396
		private byte[] cbcNextV;

		// Token: 0x04001515 RID: 5397
		private int blockSize;

		// Token: 0x04001516 RID: 5398
		private IBlockCipher cipher;

		// Token: 0x04001517 RID: 5399
		private bool encrypting;
	}
}
