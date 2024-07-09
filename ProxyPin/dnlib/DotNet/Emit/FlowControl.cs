using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009DE RID: 2526
	[ComVisible(true)]
	public enum FlowControl
	{
		// Token: 0x04003093 RID: 12435
		Branch,
		// Token: 0x04003094 RID: 12436
		Break,
		// Token: 0x04003095 RID: 12437
		Call,
		// Token: 0x04003096 RID: 12438
		Cond_Branch,
		// Token: 0x04003097 RID: 12439
		Meta,
		// Token: 0x04003098 RID: 12440
		Next,
		// Token: 0x04003099 RID: 12441
		Phi,
		// Token: 0x0400309A RID: 12442
		Return,
		// Token: 0x0400309B RID: 12443
		Throw
	}
}
