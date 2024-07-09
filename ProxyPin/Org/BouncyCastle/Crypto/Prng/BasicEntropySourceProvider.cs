using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000484 RID: 1156
	public class BasicEntropySourceProvider : IEntropySourceProvider
	{
		// Token: 0x060023BB RID: 9147 RVA: 0x000C85D8 File Offset: 0x000C85D8
		public BasicEntropySourceProvider(SecureRandom secureRandom, bool isPredictionResistant)
		{
			this.mSecureRandom = secureRandom;
			this.mPredictionResistant = isPredictionResistant;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000C85F0 File Offset: 0x000C85F0
		public IEntropySource Get(int bitsRequired)
		{
			return new BasicEntropySourceProvider.BasicEntropySource(this.mSecureRandom, this.mPredictionResistant, bitsRequired);
		}

		// Token: 0x040016B3 RID: 5811
		private readonly SecureRandom mSecureRandom;

		// Token: 0x040016B4 RID: 5812
		private readonly bool mPredictionResistant;

		// Token: 0x02000E0F RID: 3599
		private class BasicEntropySource : IEntropySource
		{
			// Token: 0x06008C20 RID: 35872 RVA: 0x002A1B2C File Offset: 0x002A1B2C
			internal BasicEntropySource(SecureRandom secureRandom, bool predictionResistant, int entropySize)
			{
				this.mSecureRandom = secureRandom;
				this.mPredictionResistant = predictionResistant;
				this.mEntropySize = entropySize;
			}

			// Token: 0x17001D77 RID: 7543
			// (get) Token: 0x06008C21 RID: 35873 RVA: 0x002A1B4C File Offset: 0x002A1B4C
			bool IEntropySource.IsPredictionResistant
			{
				get
				{
					return this.mPredictionResistant;
				}
			}

			// Token: 0x06008C22 RID: 35874 RVA: 0x002A1B54 File Offset: 0x002A1B54
			byte[] IEntropySource.GetEntropy()
			{
				return SecureRandom.GetNextBytes(this.mSecureRandom, (this.mEntropySize + 7) / 8);
			}

			// Token: 0x17001D78 RID: 7544
			// (get) Token: 0x06008C23 RID: 35875 RVA: 0x002A1B6C File Offset: 0x002A1B6C
			int IEntropySource.EntropySize
			{
				get
				{
					return this.mEntropySize;
				}
			}

			// Token: 0x04004146 RID: 16710
			private readonly SecureRandom mSecureRandom;

			// Token: 0x04004147 RID: 16711
			private readonly bool mPredictionResistant;

			// Token: 0x04004148 RID: 16712
			private readonly int mEntropySize;
		}
	}
}
