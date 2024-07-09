using System;
using System.Security.Cryptography;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000486 RID: 1158
	public class CryptoApiEntropySourceProvider : IEntropySourceProvider
	{
		// Token: 0x060023C0 RID: 9152 RVA: 0x000C8604 File Offset: 0x000C8604
		public CryptoApiEntropySourceProvider() : this(RandomNumberGenerator.Create(), true)
		{
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000C8614 File Offset: 0x000C8614
		public CryptoApiEntropySourceProvider(RandomNumberGenerator rng, bool isPredictionResistant)
		{
			if (rng == null)
			{
				throw new ArgumentNullException("rng");
			}
			this.mRng = rng;
			this.mPredictionResistant = isPredictionResistant;
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000C863C File Offset: 0x000C863C
		public IEntropySource Get(int bitsRequired)
		{
			return new CryptoApiEntropySourceProvider.CryptoApiEntropySource(this.mRng, this.mPredictionResistant, bitsRequired);
		}

		// Token: 0x040016B5 RID: 5813
		private readonly RandomNumberGenerator mRng;

		// Token: 0x040016B6 RID: 5814
		private readonly bool mPredictionResistant;

		// Token: 0x02000E10 RID: 3600
		private class CryptoApiEntropySource : IEntropySource
		{
			// Token: 0x06008C24 RID: 35876 RVA: 0x002A1B74 File Offset: 0x002A1B74
			internal CryptoApiEntropySource(RandomNumberGenerator rng, bool predictionResistant, int entropySize)
			{
				this.mRng = rng;
				this.mPredictionResistant = predictionResistant;
				this.mEntropySize = entropySize;
			}

			// Token: 0x17001D79 RID: 7545
			// (get) Token: 0x06008C25 RID: 35877 RVA: 0x002A1B94 File Offset: 0x002A1B94
			bool IEntropySource.IsPredictionResistant
			{
				get
				{
					return this.mPredictionResistant;
				}
			}

			// Token: 0x06008C26 RID: 35878 RVA: 0x002A1B9C File Offset: 0x002A1B9C
			byte[] IEntropySource.GetEntropy()
			{
				byte[] array = new byte[(this.mEntropySize + 7) / 8];
				this.mRng.GetBytes(array);
				return array;
			}

			// Token: 0x17001D7A RID: 7546
			// (get) Token: 0x06008C27 RID: 35879 RVA: 0x002A1BCC File Offset: 0x002A1BCC
			int IEntropySource.EntropySize
			{
				get
				{
					return this.mEntropySize;
				}
			}

			// Token: 0x04004149 RID: 16713
			private readonly RandomNumberGenerator mRng;

			// Token: 0x0400414A RID: 16714
			private readonly bool mPredictionResistant;

			// Token: 0x0400414B RID: 16715
			private readonly int mEntropySize;
		}
	}
}
