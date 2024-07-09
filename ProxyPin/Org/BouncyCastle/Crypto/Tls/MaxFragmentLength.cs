using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200050C RID: 1292
	public abstract class MaxFragmentLength
	{
		// Token: 0x0600276A RID: 10090 RVA: 0x000D5A28 File Offset: 0x000D5A28
		public static bool IsValid(byte maxFragmentLength)
		{
			return maxFragmentLength >= 1 && maxFragmentLength <= 4;
		}

		// Token: 0x040019E0 RID: 6624
		public const byte pow2_9 = 1;

		// Token: 0x040019E1 RID: 6625
		public const byte pow2_10 = 2;

		// Token: 0x040019E2 RID: 6626
		public const byte pow2_11 = 3;

		// Token: 0x040019E3 RID: 6627
		public const byte pow2_12 = 4;
	}
}
