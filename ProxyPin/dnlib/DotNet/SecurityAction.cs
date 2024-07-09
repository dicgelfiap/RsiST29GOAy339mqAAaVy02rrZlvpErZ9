using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000842 RID: 2114
	[ComVisible(true)]
	public enum SecurityAction : short
	{
		// Token: 0x040026D6 RID: 9942
		ActionMask = 31,
		// Token: 0x040026D7 RID: 9943
		ActionNil = 0,
		// Token: 0x040026D8 RID: 9944
		Request,
		// Token: 0x040026D9 RID: 9945
		Demand,
		// Token: 0x040026DA RID: 9946
		Assert,
		// Token: 0x040026DB RID: 9947
		Deny,
		// Token: 0x040026DC RID: 9948
		PermitOnly,
		// Token: 0x040026DD RID: 9949
		LinktimeCheck,
		// Token: 0x040026DE RID: 9950
		LinkDemand = 6,
		// Token: 0x040026DF RID: 9951
		InheritanceCheck,
		// Token: 0x040026E0 RID: 9952
		InheritDemand = 7,
		// Token: 0x040026E1 RID: 9953
		RequestMinimum,
		// Token: 0x040026E2 RID: 9954
		RequestOptional,
		// Token: 0x040026E3 RID: 9955
		RequestRefuse,
		// Token: 0x040026E4 RID: 9956
		PrejitGrant,
		// Token: 0x040026E5 RID: 9957
		PreJitGrant = 11,
		// Token: 0x040026E6 RID: 9958
		PrejitDenied,
		// Token: 0x040026E7 RID: 9959
		PreJitDeny = 12,
		// Token: 0x040026E8 RID: 9960
		NonCasDemand,
		// Token: 0x040026E9 RID: 9961
		NonCasLinkDemand,
		// Token: 0x040026EA RID: 9962
		NonCasInheritance,
		// Token: 0x040026EB RID: 9963
		MaximumValue = 15
	}
}
