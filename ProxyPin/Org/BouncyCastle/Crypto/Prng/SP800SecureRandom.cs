using System;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200048E RID: 1166
	public class SP800SecureRandom : SecureRandom
	{
		// Token: 0x060023F6 RID: 9206 RVA: 0x000C8E58 File Offset: 0x000C8E58
		internal SP800SecureRandom(SecureRandom randomSource, IEntropySource entropySource, IDrbgProvider drbgProvider, bool predictionResistant) : base(null)
		{
			this.mRandomSource = randomSource;
			this.mEntropySource = entropySource;
			this.mDrbgProvider = drbgProvider;
			this.mPredictionResistant = predictionResistant;
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000C8E80 File Offset: 0x000C8E80
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

		// Token: 0x060023F8 RID: 9208 RVA: 0x000C8EC8 File Offset: 0x000C8EC8
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

		// Token: 0x060023F9 RID: 9209 RVA: 0x000C8F10 File Offset: 0x000C8F10
		public override void NextBytes(byte[] bytes)
		{
			lock (this)
			{
				if (this.mDrbg == null)
				{
					this.mDrbg = this.mDrbgProvider.Get(this.mEntropySource);
				}
				if (this.mDrbg.Generate(bytes, null, this.mPredictionResistant) < 0)
				{
					this.mDrbg.Reseed(null);
					this.mDrbg.Generate(bytes, null, this.mPredictionResistant);
				}
			}
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000C8F9C File Offset: 0x000C8F9C
		public override void NextBytes(byte[] buf, int off, int len)
		{
			byte[] array = new byte[len];
			this.NextBytes(array);
			Array.Copy(array, 0, buf, off, len);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000C8FC8 File Offset: 0x000C8FC8
		public override byte[] GenerateSeed(int numBytes)
		{
			return EntropyUtilities.GenerateSeed(this.mEntropySource, numBytes);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000C8FD8 File Offset: 0x000C8FD8
		public virtual void Reseed(byte[] additionalInput)
		{
			lock (this)
			{
				if (this.mDrbg == null)
				{
					this.mDrbg = this.mDrbgProvider.Get(this.mEntropySource);
				}
				this.mDrbg.Reseed(additionalInput);
			}
		}

		// Token: 0x040016C5 RID: 5829
		private readonly IDrbgProvider mDrbgProvider;

		// Token: 0x040016C6 RID: 5830
		private readonly bool mPredictionResistant;

		// Token: 0x040016C7 RID: 5831
		private readonly SecureRandom mRandomSource;

		// Token: 0x040016C8 RID: 5832
		private readonly IEntropySource mEntropySource;

		// Token: 0x040016C9 RID: 5833
		private ISP80090Drbg mDrbg;
	}
}
