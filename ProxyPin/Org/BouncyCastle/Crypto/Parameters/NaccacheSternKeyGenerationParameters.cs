using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000465 RID: 1125
	public class NaccacheSternKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002317 RID: 8983 RVA: 0x000C6264 File Offset: 0x000C6264
		public NaccacheSternKeyGenerationParameters(SecureRandom random, int strength, int certainty, int countSmallPrimes) : base(random, strength)
		{
			if (countSmallPrimes % 2 == 1)
			{
				throw new ArgumentException("countSmallPrimes must be a multiple of 2");
			}
			if (countSmallPrimes < 30)
			{
				throw new ArgumentException("countSmallPrimes must be >= 30 for security reasons");
			}
			this.certainty = certainty;
			this.countSmallPrimes = countSmallPrimes;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000C62B8 File Offset: 0x000C62B8
		[Obsolete("Use version without 'debug' parameter")]
		public NaccacheSternKeyGenerationParameters(SecureRandom random, int strength, int certainty, int countSmallPrimes, bool debug) : this(random, strength, certainty, countSmallPrimes)
		{
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x000C62C8 File Offset: 0x000C62C8
		public int Certainty
		{
			get
			{
				return this.certainty;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x000C62D0 File Offset: 0x000C62D0
		public int CountSmallPrimes
		{
			get
			{
				return this.countSmallPrimes;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x000C62D8 File Offset: 0x000C62D8
		[Obsolete("Remove: always false")]
		public bool IsDebug
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001653 RID: 5715
		private readonly int certainty;

		// Token: 0x04001654 RID: 5716
		private readonly int countSmallPrimes;
	}
}
