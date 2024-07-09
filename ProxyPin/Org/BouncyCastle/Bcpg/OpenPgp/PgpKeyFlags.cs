using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200064F RID: 1615
	public abstract class PgpKeyFlags
	{
		// Token: 0x04001DAE RID: 7598
		public const int CanCertify = 1;

		// Token: 0x04001DAF RID: 7599
		public const int CanSign = 2;

		// Token: 0x04001DB0 RID: 7600
		public const int CanEncryptCommunications = 4;

		// Token: 0x04001DB1 RID: 7601
		public const int CanEncryptStorage = 8;

		// Token: 0x04001DB2 RID: 7602
		public const int MaybeSplit = 16;

		// Token: 0x04001DB3 RID: 7603
		public const int MaybeShared = 128;
	}
}
