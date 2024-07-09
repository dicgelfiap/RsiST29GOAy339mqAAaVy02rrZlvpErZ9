using System;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x02000702 RID: 1794
	public sealed class Times
	{
		// Token: 0x06003EDB RID: 16091 RVA: 0x00159E6C File Offset: 0x00159E6C
		public static long NanoTime()
		{
			return DateTime.UtcNow.Ticks * Times.NanosecondsPerTick;
		}

		// Token: 0x0400207D RID: 8317
		private static long NanosecondsPerTick = 100L;
	}
}
