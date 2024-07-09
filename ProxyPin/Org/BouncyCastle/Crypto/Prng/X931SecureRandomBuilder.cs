using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000494 RID: 1172
	public class X931SecureRandomBuilder
	{
		// Token: 0x0600241A RID: 9242 RVA: 0x000C9878 File Offset: 0x000C9878
		public X931SecureRandomBuilder() : this(new SecureRandom(), false)
		{
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000C9888 File Offset: 0x000C9888
		public X931SecureRandomBuilder(SecureRandom entropySource, bool predictionResistant)
		{
			this.mRandom = entropySource;
			this.mEntropySourceProvider = new BasicEntropySourceProvider(this.mRandom, predictionResistant);
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000C98AC File Offset: 0x000C98AC
		public X931SecureRandomBuilder(IEntropySourceProvider entropySourceProvider)
		{
			this.mRandom = null;
			this.mEntropySourceProvider = entropySourceProvider;
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000C98C4 File Offset: 0x000C98C4
		public X931SecureRandomBuilder SetDateTimeVector(byte[] dateTimeVector)
		{
			this.mDateTimeVector = dateTimeVector;
			return this;
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000C98D0 File Offset: 0x000C98D0
		public X931SecureRandom Build(IBlockCipher engine, KeyParameter key, bool predictionResistant)
		{
			if (this.mDateTimeVector == null)
			{
				this.mDateTimeVector = new byte[engine.GetBlockSize()];
				Pack.UInt64_To_BE((ulong)DateTimeUtilities.CurrentUnixMs(), this.mDateTimeVector, 0);
			}
			engine.Init(true, key);
			return new X931SecureRandom(this.mRandom, new X931Rng(engine, this.mDateTimeVector, this.mEntropySourceProvider.Get(engine.GetBlockSize() * 8)), predictionResistant);
		}

		// Token: 0x040016E0 RID: 5856
		private readonly SecureRandom mRandom;

		// Token: 0x040016E1 RID: 5857
		private IEntropySourceProvider mEntropySourceProvider;

		// Token: 0x040016E2 RID: 5858
		private byte[] mDateTimeVector;
	}
}
