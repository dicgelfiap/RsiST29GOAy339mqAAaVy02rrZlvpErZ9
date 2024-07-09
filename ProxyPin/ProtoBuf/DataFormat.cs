using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C1A RID: 3098
	[ComVisible(true)]
	public enum DataFormat
	{
		// Token: 0x04003B62 RID: 15202
		Default,
		// Token: 0x04003B63 RID: 15203
		ZigZag,
		// Token: 0x04003B64 RID: 15204
		TwosComplement,
		// Token: 0x04003B65 RID: 15205
		FixedSize,
		// Token: 0x04003B66 RID: 15206
		Group,
		// Token: 0x04003B67 RID: 15207
		WellKnown
	}
}
