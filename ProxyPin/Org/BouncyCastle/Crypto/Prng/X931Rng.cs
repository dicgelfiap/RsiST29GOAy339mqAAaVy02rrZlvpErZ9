using System;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000492 RID: 1170
	internal class X931Rng
	{
		// Token: 0x0600240D RID: 9229 RVA: 0x000C93C4 File Offset: 0x000C93C4
		internal X931Rng(IBlockCipher engine, byte[] dateTimeVector, IEntropySource entropySource)
		{
			this.mEngine = engine;
			this.mEntropySource = entropySource;
			this.mDT = new byte[engine.GetBlockSize()];
			Array.Copy(dateTimeVector, 0, this.mDT, 0, this.mDT.Length);
			this.mI = new byte[engine.GetBlockSize()];
			this.mR = new byte[engine.GetBlockSize()];
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000C943C File Offset: 0x000C943C
		internal int Generate(byte[] output, bool predictionResistant)
		{
			if (this.mR.Length == 8)
			{
				if (this.mReseedCounter > 32768L)
				{
					return -1;
				}
				if (X931Rng.IsTooLarge(output, 512))
				{
					throw new ArgumentException("Number of bits per request limited to " + 4096, "output");
				}
			}
			else
			{
				if (this.mReseedCounter > 8388608L)
				{
					return -1;
				}
				if (X931Rng.IsTooLarge(output, 32768))
				{
					throw new ArgumentException("Number of bits per request limited to " + 262144, "output");
				}
			}
			if (predictionResistant || this.mV == null)
			{
				this.mV = this.mEntropySource.GetEntropy();
				if (this.mV.Length != this.mEngine.GetBlockSize())
				{
					throw new InvalidOperationException("Insufficient entropy returned");
				}
			}
			int num = output.Length / this.mR.Length;
			for (int i = 0; i < num; i++)
			{
				this.mEngine.ProcessBlock(this.mDT, 0, this.mI, 0);
				this.Process(this.mR, this.mI, this.mV);
				this.Process(this.mV, this.mR, this.mI);
				Array.Copy(this.mR, 0, output, i * this.mR.Length, this.mR.Length);
				this.Increment(this.mDT);
			}
			int num2 = output.Length - num * this.mR.Length;
			if (num2 > 0)
			{
				this.mEngine.ProcessBlock(this.mDT, 0, this.mI, 0);
				this.Process(this.mR, this.mI, this.mV);
				this.Process(this.mV, this.mR, this.mI);
				Array.Copy(this.mR, 0, output, num * this.mR.Length, num2);
				this.Increment(this.mDT);
			}
			this.mReseedCounter += 1L;
			return output.Length;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000C964C File Offset: 0x000C964C
		internal void Reseed()
		{
			this.mV = this.mEntropySource.GetEntropy();
			if (this.mV.Length != this.mEngine.GetBlockSize())
			{
				throw new InvalidOperationException("Insufficient entropy returned");
			}
			this.mReseedCounter = 1L;
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x000C968C File Offset: 0x000C968C
		internal IEntropySource EntropySource
		{
			get
			{
				return this.mEntropySource;
			}
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000C9694 File Offset: 0x000C9694
		private void Process(byte[] res, byte[] a, byte[] b)
		{
			for (int num = 0; num != res.Length; num++)
			{
				res[num] = (a[num] ^ b[num]);
			}
			this.mEngine.ProcessBlock(res, 0, res, 0);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000C96D4 File Offset: 0x000C96D4
		private void Increment(byte[] val)
		{
			for (int i = val.Length - 1; i >= 0; i--)
			{
				IntPtr intPtr;
				if ((val[(int)(intPtr = (IntPtr)i)] = val[(int)intPtr] + 1) != 0)
				{
					return;
				}
			}
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000C9710 File Offset: 0x000C9710
		private static bool IsTooLarge(byte[] bytes, int maxBytes)
		{
			return bytes != null && bytes.Length > maxBytes;
		}

		// Token: 0x040016D2 RID: 5842
		private const long BLOCK64_RESEED_MAX = 32768L;

		// Token: 0x040016D3 RID: 5843
		private const long BLOCK128_RESEED_MAX = 8388608L;

		// Token: 0x040016D4 RID: 5844
		private const int BLOCK64_MAX_BITS_REQUEST = 4096;

		// Token: 0x040016D5 RID: 5845
		private const int BLOCK128_MAX_BITS_REQUEST = 262144;

		// Token: 0x040016D6 RID: 5846
		private readonly IBlockCipher mEngine;

		// Token: 0x040016D7 RID: 5847
		private readonly IEntropySource mEntropySource;

		// Token: 0x040016D8 RID: 5848
		private readonly byte[] mDT;

		// Token: 0x040016D9 RID: 5849
		private readonly byte[] mI;

		// Token: 0x040016DA RID: 5850
		private readonly byte[] mR;

		// Token: 0x040016DB RID: 5851
		private byte[] mV;

		// Token: 0x040016DC RID: 5852
		private long mReseedCounter = 1L;
	}
}
