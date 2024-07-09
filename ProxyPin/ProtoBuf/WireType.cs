using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C40 RID: 3136
	[ComVisible(true)]
	public enum WireType
	{
		// Token: 0x04003C24 RID: 15396
		None = -1,
		// Token: 0x04003C25 RID: 15397
		Variant,
		// Token: 0x04003C26 RID: 15398
		Fixed64,
		// Token: 0x04003C27 RID: 15399
		String,
		// Token: 0x04003C28 RID: 15400
		StartGroup,
		// Token: 0x04003C29 RID: 15401
		EndGroup,
		// Token: 0x04003C2A RID: 15402
		Fixed32,
		// Token: 0x04003C2B RID: 15403
		SignedVariant = 8
	}
}
