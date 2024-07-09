using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009F1 RID: 2545
	[ComVisible(true)]
	public enum StackBehaviour : byte
	{
		// Token: 0x040031D8 RID: 12760
		Pop0,
		// Token: 0x040031D9 RID: 12761
		Pop1,
		// Token: 0x040031DA RID: 12762
		Pop1_pop1,
		// Token: 0x040031DB RID: 12763
		Popi,
		// Token: 0x040031DC RID: 12764
		Popi_pop1,
		// Token: 0x040031DD RID: 12765
		Popi_popi,
		// Token: 0x040031DE RID: 12766
		Popi_popi8,
		// Token: 0x040031DF RID: 12767
		Popi_popi_popi,
		// Token: 0x040031E0 RID: 12768
		Popi_popr4,
		// Token: 0x040031E1 RID: 12769
		Popi_popr8,
		// Token: 0x040031E2 RID: 12770
		Popref,
		// Token: 0x040031E3 RID: 12771
		Popref_pop1,
		// Token: 0x040031E4 RID: 12772
		Popref_popi,
		// Token: 0x040031E5 RID: 12773
		Popref_popi_popi,
		// Token: 0x040031E6 RID: 12774
		Popref_popi_popi8,
		// Token: 0x040031E7 RID: 12775
		Popref_popi_popr4,
		// Token: 0x040031E8 RID: 12776
		Popref_popi_popr8,
		// Token: 0x040031E9 RID: 12777
		Popref_popi_popref,
		// Token: 0x040031EA RID: 12778
		Push0,
		// Token: 0x040031EB RID: 12779
		Push1,
		// Token: 0x040031EC RID: 12780
		Push1_push1,
		// Token: 0x040031ED RID: 12781
		Pushi,
		// Token: 0x040031EE RID: 12782
		Pushi8,
		// Token: 0x040031EF RID: 12783
		Pushr4,
		// Token: 0x040031F0 RID: 12784
		Pushr8,
		// Token: 0x040031F1 RID: 12785
		Pushref,
		// Token: 0x040031F2 RID: 12786
		Varpop,
		// Token: 0x040031F3 RID: 12787
		Varpush,
		// Token: 0x040031F4 RID: 12788
		Popref_popi_pop1,
		// Token: 0x040031F5 RID: 12789
		PopAll = 255
	}
}
