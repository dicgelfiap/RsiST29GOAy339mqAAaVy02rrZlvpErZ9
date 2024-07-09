using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000290 RID: 656
	public enum RevocationReasonTag : byte
	{
		// Token: 0x04000DE5 RID: 3557
		NoReason,
		// Token: 0x04000DE6 RID: 3558
		KeySuperseded,
		// Token: 0x04000DE7 RID: 3559
		KeyCompromised,
		// Token: 0x04000DE8 RID: 3560
		KeyRetired,
		// Token: 0x04000DE9 RID: 3561
		UserNoLongerValid = 32
	}
}
