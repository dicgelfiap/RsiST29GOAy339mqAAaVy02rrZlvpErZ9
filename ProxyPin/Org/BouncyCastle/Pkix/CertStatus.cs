using System;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x0200068A RID: 1674
	public class CertStatus
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06003A5F RID: 14943 RVA: 0x0013A9DC File Offset: 0x0013A9DC
		// (set) Token: 0x06003A60 RID: 14944 RVA: 0x0013A9E4 File Offset: 0x0013A9E4
		public DateTimeObject RevocationDate
		{
			get
			{
				return this.revocationDate;
			}
			set
			{
				this.revocationDate = value;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06003A61 RID: 14945 RVA: 0x0013A9F0 File Offset: 0x0013A9F0
		// (set) Token: 0x06003A62 RID: 14946 RVA: 0x0013A9F8 File Offset: 0x0013A9F8
		public int Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x04001E48 RID: 7752
		public const int Unrevoked = 11;

		// Token: 0x04001E49 RID: 7753
		public const int Undetermined = 12;

		// Token: 0x04001E4A RID: 7754
		private int status = 11;

		// Token: 0x04001E4B RID: 7755
		private DateTimeObject revocationDate = null;
	}
}
