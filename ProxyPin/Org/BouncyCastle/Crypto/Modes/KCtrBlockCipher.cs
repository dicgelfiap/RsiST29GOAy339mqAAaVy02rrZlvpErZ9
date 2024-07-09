using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000406 RID: 1030
	public class KCtrBlockCipher : IStreamCipher, IBlockCipher
	{
		// Token: 0x06002111 RID: 8465 RVA: 0x000C03D0 File Offset: 0x000C03D0
		public KCtrBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.IV = new byte[cipher.GetBlockSize()];
			this.blockSize = cipher.GetBlockSize();
			this.ofbV = new byte[cipher.GetBlockSize()];
			this.ofbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x000C0430 File Offset: 0x000C0430
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x000C0438 File Offset: 0x000C0438
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.initialised = true;
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				int destinationIndex = this.IV.Length - iv.Length;
				Array.Clear(this.IV, 0, this.IV.Length);
				Array.Copy(iv, 0, this.IV, destinationIndex, iv.Length);
				parameters = parametersWithIV.Parameters;
				if (parameters != null)
				{
					this.cipher.Init(true, parameters);
				}
				this.Reset();
				return;
			}
			throw new ArgumentException("Invalid parameter passed");
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x000C04CC File Offset: 0x000C04CC
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/KCTR";
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000C04E4 File Offset: 0x000C04E4
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000C04E8 File Offset: 0x000C04E8
		public int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x000C04F8 File Offset: 0x000C04F8
		public byte ReturnByte(byte input)
		{
			return this.CalculateByte(input);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000C0504 File Offset: 0x000C0504
		public void ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			if (outOff + len > output.Length)
			{
				throw new DataLengthException("Output buffer too short");
			}
			if (inOff + len > input.Length)
			{
				throw new DataLengthException("Input buffer too small");
			}
			int i = inOff;
			int num = inOff + len;
			int num2 = outOff;
			while (i < num)
			{
				output[num2++] = this.CalculateByte(input[i++]);
			}
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000C056C File Offset: 0x000C056C
		protected byte CalculateByte(byte b)
		{
			if (this.byteCount == 0)
			{
				this.incrementCounterAt(0);
				this.checkCounter();
				this.cipher.ProcessBlock(this.ofbV, 0, this.ofbOutV, 0);
				return this.ofbOutV[this.byteCount++] ^ b;
			}
			byte result = this.ofbOutV[this.byteCount++] ^ b;
			if (this.byteCount == this.ofbV.Length)
			{
				this.byteCount = 0;
			}
			return result;
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x000C0604 File Offset: 0x000C0604
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (input.Length - inOff < this.GetBlockSize())
			{
				throw new DataLengthException("Input buffer too short");
			}
			if (output.Length - outOff < this.GetBlockSize())
			{
				throw new DataLengthException("Output buffer too short");
			}
			this.ProcessBytes(input, inOff, this.GetBlockSize(), output, outOff);
			return this.GetBlockSize();
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000C0664 File Offset: 0x000C0664
		public void Reset()
		{
			if (this.initialised)
			{
				this.cipher.ProcessBlock(this.IV, 0, this.ofbV, 0);
			}
			this.cipher.Reset();
			this.byteCount = 0;
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000C06A0 File Offset: 0x000C06A0
		private void incrementCounterAt(int pos)
		{
			int i = pos;
			while (i < this.ofbV.Length)
			{
				byte[] array;
				IntPtr intPtr;
				if (((array = this.ofbV)[(int)(intPtr = (IntPtr)(i++))] = array[(int)intPtr] + 1) != 0)
				{
					return;
				}
			}
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000C06E4 File Offset: 0x000C06E4
		private void checkCounter()
		{
		}

		// Token: 0x04001581 RID: 5505
		private byte[] IV;

		// Token: 0x04001582 RID: 5506
		private byte[] ofbV;

		// Token: 0x04001583 RID: 5507
		private byte[] ofbOutV;

		// Token: 0x04001584 RID: 5508
		private bool initialised;

		// Token: 0x04001585 RID: 5509
		private int byteCount;

		// Token: 0x04001586 RID: 5510
		private readonly int blockSize;

		// Token: 0x04001587 RID: 5511
		private readonly IBlockCipher cipher;
	}
}
