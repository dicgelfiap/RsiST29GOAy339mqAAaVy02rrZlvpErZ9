using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000811 RID: 2065
	[Flags]
	[ComVisible(true)]
	public enum MethodExportInfoOptions
	{
		// Token: 0x040025AB RID: 9643
		None = 0,
		// Token: 0x040025AC RID: 9644
		FromUnmanaged = 1,
		// Token: 0x040025AD RID: 9645
		FromUnmanagedRetainAppDomain = 2,
		// Token: 0x040025AE RID: 9646
		CallMostDerived = 4
	}
}
