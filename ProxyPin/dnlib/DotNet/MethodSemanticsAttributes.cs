using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000815 RID: 2069
	[Flags]
	[ComVisible(true)]
	public enum MethodSemanticsAttributes : ushort
	{
		// Token: 0x040025C5 RID: 9669
		None = 0,
		// Token: 0x040025C6 RID: 9670
		Setter = 1,
		// Token: 0x040025C7 RID: 9671
		Getter = 2,
		// Token: 0x040025C8 RID: 9672
		Other = 4,
		// Token: 0x040025C9 RID: 9673
		AddOn = 8,
		// Token: 0x040025CA RID: 9674
		RemoveOn = 16,
		// Token: 0x040025CB RID: 9675
		Fire = 32
	}
}
