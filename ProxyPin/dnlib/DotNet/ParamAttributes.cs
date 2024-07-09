using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000827 RID: 2087
	[Flags]
	[ComVisible(true)]
	public enum ParamAttributes : ushort
	{
		// Token: 0x04002674 RID: 9844
		In = 1,
		// Token: 0x04002675 RID: 9845
		Out = 2,
		// Token: 0x04002676 RID: 9846
		Lcid = 4,
		// Token: 0x04002677 RID: 9847
		Retval = 8,
		// Token: 0x04002678 RID: 9848
		Optional = 16,
		// Token: 0x04002679 RID: 9849
		HasDefault = 4096,
		// Token: 0x0400267A RID: 9850
		HasFieldMarshal = 8192
	}
}
