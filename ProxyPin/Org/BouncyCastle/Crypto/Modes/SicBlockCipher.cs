using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200040A RID: 1034
	public class SicBlockCipher : IBlockCipher
	{
		// Token: 0x0600214B RID: 8523 RVA: 0x000C1C80 File Offset: 0x000C1C80
		public SicBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			this.counter = new byte[this.blockSize];
			this.counterOut = new byte[this.blockSize];
			this.IV = new byte[this.blockSize];
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000C1CE0 File Offset: 0x000C1CE0
		public virtual IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000C1CE8 File Offset: 0x000C1CE8
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			ParametersWithIV parametersWithIV = parameters as ParametersWithIV;
			if (parametersWithIV == null)
			{
				throw new ArgumentException("CTR/SIC mode requires ParametersWithIV", "parameters");
			}
			this.IV = Arrays.Clone(parametersWithIV.GetIV());
			if (this.blockSize < this.IV.Length)
			{
				throw new ArgumentException("CTR/SIC mode requires IV no greater than: " + this.blockSize + " bytes.");
			}
			int num = Math.Min(8, this.blockSize / 2);
			if (this.blockSize - this.IV.Length > num)
			{
				throw new ArgumentException("CTR/SIC mode requires IV of at least: " + (this.blockSize - num) + " bytes.");
			}
			if (parametersWithIV.Parameters != null)
			{
				this.cipher.Init(true, parametersWithIV.Parameters);
			}
			this.Reset();
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x000C1DC4 File Offset: 0x000C1DC4
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/SIC";
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x000C1DDC File Offset: 0x000C1DDC
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x000C1DE0 File Offset: 0x000C1DE0
		public virtual int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000C1DF0 File Offset: 0x000C1DF0
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			this.cipher.ProcessBlock(this.counter, 0, this.counterOut, 0);
			for (int i = 0; i < this.counterOut.Length; i++)
			{
				output[outOff + i] = (this.counterOut[i] ^ input[inOff + i]);
			}
			int num = this.counter.Length;
			byte[] array;
			IntPtr intPtr;
			while (--num >= 0 && ((array = this.counter)[(int)(intPtr = (IntPtr)num)] = array[(int)intPtr] + 1) == 0)
			{
			}
			return this.counter.Length;
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000C1E7C File Offset: 0x000C1E7C
		public virtual void Reset()
		{
			Arrays.Fill(this.counter, 0);
			Array.Copy(this.IV, 0, this.counter, 0, this.IV.Length);
			this.cipher.Reset();
		}

		// Token: 0x040015AB RID: 5547
		private readonly IBlockCipher cipher;

		// Token: 0x040015AC RID: 5548
		private readonly int blockSize;

		// Token: 0x040015AD RID: 5549
		private readonly byte[] counter;

		// Token: 0x040015AE RID: 5550
		private readonly byte[] counterOut;

		// Token: 0x040015AF RID: 5551
		private byte[] IV;
	}
}
