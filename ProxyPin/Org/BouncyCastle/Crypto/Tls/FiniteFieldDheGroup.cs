using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000503 RID: 1283
	public abstract class FiniteFieldDheGroup
	{
		// Token: 0x06002755 RID: 10069 RVA: 0x000D56D4 File Offset: 0x000D56D4
		public static bool IsValid(byte group)
		{
			return group >= 0 && group <= 4;
		}

		// Token: 0x0400199C RID: 6556
		public const byte ffdhe2432 = 0;

		// Token: 0x0400199D RID: 6557
		public const byte ffdhe3072 = 1;

		// Token: 0x0400199E RID: 6558
		public const byte ffdhe4096 = 2;

		// Token: 0x0400199F RID: 6559
		public const byte ffdhe6144 = 3;

		// Token: 0x040019A0 RID: 6560
		public const byte ffdhe8192 = 4;
	}
}
