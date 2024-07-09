using System;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000EC RID: 236
	public enum PkiStatus
	{
		// Token: 0x04000679 RID: 1657
		Granted,
		// Token: 0x0400067A RID: 1658
		GrantedWithMods,
		// Token: 0x0400067B RID: 1659
		Rejection,
		// Token: 0x0400067C RID: 1660
		Waiting,
		// Token: 0x0400067D RID: 1661
		RevocationWarning,
		// Token: 0x0400067E RID: 1662
		RevocationNotification,
		// Token: 0x0400067F RID: 1663
		KeyUpdateWarning
	}
}
