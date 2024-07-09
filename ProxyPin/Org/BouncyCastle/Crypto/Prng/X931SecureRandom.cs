using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000493 RID: 1171
	public class X931SecureRandom : SecureRandom
	{
		// Token: 0x06002414 RID: 9236 RVA: 0x000C9720 File Offset: 0x000C9720
		internal X931SecureRandom(SecureRandom randomSource, X931Rng drbg, bool predictionResistant) : base(null)
		{
			this.mRandomSource = randomSource;
			this.mDrbg = drbg;
			this.mPredictionResistant = predictionResistant;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000C9740 File Offset: 0x000C9740
		public override void SetSeed(byte[] seed)
		{
			lock (this)
			{
				if (this.mRandomSource != null)
				{
					this.mRandomSource.SetSeed(seed);
				}
			}
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000C9788 File Offset: 0x000C9788
		public override void SetSeed(long seed)
		{
			lock (this)
			{
				if (this.mRandomSource != null)
				{
					this.mRandomSource.SetSeed(seed);
				}
			}
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000C97D0 File Offset: 0x000C97D0
		public override void NextBytes(byte[] bytes)
		{
			lock (this)
			{
				if (this.mDrbg.Generate(bytes, this.mPredictionResistant) < 0)
				{
					this.mDrbg.Reseed();
					this.mDrbg.Generate(bytes, this.mPredictionResistant);
				}
			}
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000C9838 File Offset: 0x000C9838
		public override void NextBytes(byte[] buf, int off, int len)
		{
			byte[] array = new byte[len];
			this.NextBytes(array);
			Array.Copy(array, 0, buf, off, len);
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000C9864 File Offset: 0x000C9864
		public override byte[] GenerateSeed(int numBytes)
		{
			return EntropyUtilities.GenerateSeed(this.mDrbg.EntropySource, numBytes);
		}

		// Token: 0x040016DD RID: 5853
		private readonly bool mPredictionResistant;

		// Token: 0x040016DE RID: 5854
		private readonly SecureRandom mRandomSource;

		// Token: 0x040016DF RID: 5855
		private readonly X931Rng mDrbg;
	}
}
