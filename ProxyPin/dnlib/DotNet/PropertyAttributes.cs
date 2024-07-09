using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200082E RID: 2094
	[Flags]
	[ComVisible(true)]
	public enum PropertyAttributes : ushort
	{
		// Token: 0x040026AE RID: 9902
		SpecialName = 512,
		// Token: 0x040026AF RID: 9903
		RTSpecialName = 1024,
		// Token: 0x040026B0 RID: 9904
		HasDefault = 4096
	}
}
