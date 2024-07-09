using System;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x0200063B RID: 1595
	public abstract class OcspRespStatus
	{
		// Token: 0x04001D62 RID: 7522
		public const int Successful = 0;

		// Token: 0x04001D63 RID: 7523
		public const int MalformedRequest = 1;

		// Token: 0x04001D64 RID: 7524
		public const int InternalError = 2;

		// Token: 0x04001D65 RID: 7525
		public const int TryLater = 3;

		// Token: 0x04001D66 RID: 7526
		public const int SigRequired = 5;

		// Token: 0x04001D67 RID: 7527
		public const int Unauthorized = 6;
	}
}
