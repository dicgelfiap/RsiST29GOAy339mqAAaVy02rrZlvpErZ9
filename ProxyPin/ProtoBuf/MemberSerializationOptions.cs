using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C39 RID: 3129
	[Flags]
	[ComVisible(true)]
	public enum MemberSerializationOptions
	{
		// Token: 0x04003BEE RID: 15342
		None = 0,
		// Token: 0x04003BEF RID: 15343
		Packed = 1,
		// Token: 0x04003BF0 RID: 15344
		Required = 2,
		// Token: 0x04003BF1 RID: 15345
		AsReference = 4,
		// Token: 0x04003BF2 RID: 15346
		DynamicType = 8,
		// Token: 0x04003BF3 RID: 15347
		OverwriteList = 16,
		// Token: 0x04003BF4 RID: 15348
		AsReferenceHasValue = 32
	}
}
