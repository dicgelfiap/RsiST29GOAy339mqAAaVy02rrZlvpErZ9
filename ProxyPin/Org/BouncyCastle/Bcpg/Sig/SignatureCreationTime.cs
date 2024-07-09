using System;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000291 RID: 657
	public class SignatureCreationTime : SignatureSubpacket
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x0006DE64 File Offset: 0x0006DE64
		protected static byte[] TimeToBytes(DateTime time)
		{
			long num = DateTimeUtilities.DateTimeToUnixMs(time) / 1000L;
			return new byte[]
			{
				(byte)(num >> 24),
				(byte)(num >> 16),
				(byte)(num >> 8),
				(byte)num
			};
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0006DEA8 File Offset: 0x0006DEA8
		public SignatureCreationTime(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.CreationTime, critical, isLongLength, data)
		{
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0006DEB4 File Offset: 0x0006DEB4
		public SignatureCreationTime(bool critical, DateTime date) : base(SignatureSubpacketTag.CreationTime, critical, false, SignatureCreationTime.TimeToBytes(date))
		{
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0006DEC8 File Offset: 0x0006DEC8
		public DateTime GetTime()
		{
			long num = (long)((ulong)((int)this.data[0] << 24 | (int)this.data[1] << 16 | (int)this.data[2] << 8 | (int)this.data[3]));
			return DateTimeUtilities.UnixMsToDateTime(num * 1000L);
		}
	}
}
