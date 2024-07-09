using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000888 RID: 2184
	[Flags]
	[ComVisible(true)]
	public enum VTableFlags : ushort
	{
		// Token: 0x04002824 RID: 10276
		Bit32 = 1,
		// Token: 0x04002825 RID: 10277
		Bit64 = 2,
		// Token: 0x04002826 RID: 10278
		FromUnmanaged = 4,
		// Token: 0x04002827 RID: 10279
		FromUnmanagedRetainAppDomain = 8,
		// Token: 0x04002828 RID: 10280
		CallMostDerived = 16
	}
}
