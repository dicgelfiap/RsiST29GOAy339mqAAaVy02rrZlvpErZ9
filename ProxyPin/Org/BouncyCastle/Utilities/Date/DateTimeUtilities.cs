using System;

namespace Org.BouncyCastle.Utilities.Date
{
	// Token: 0x020006D5 RID: 1749
	public class DateTimeUtilities
	{
		// Token: 0x06003D45 RID: 15685 RVA: 0x0014FD74 File Offset: 0x0014FD74
		private DateTimeUtilities()
		{
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x0014FD7C File Offset: 0x0014FD7C
		public static long DateTimeToUnixMs(DateTime dateTime)
		{
			if (dateTime.CompareTo(DateTimeUtilities.UnixEpoch) < 0)
			{
				throw new ArgumentException("DateTime value may not be before the epoch", "dateTime");
			}
			return (dateTime.Ticks - DateTimeUtilities.UnixEpoch.Ticks) / 10000L;
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x0014FDD0 File Offset: 0x0014FDD0
		public static DateTime UnixMsToDateTime(long unixMs)
		{
			return new DateTime(unixMs * 10000L + DateTimeUtilities.UnixEpoch.Ticks);
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x0014FDFC File Offset: 0x0014FDFC
		public static long CurrentUnixMs()
		{
			return DateTimeUtilities.DateTimeToUnixMs(DateTime.UtcNow);
		}

		// Token: 0x04001EEB RID: 7915
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
	}
}
