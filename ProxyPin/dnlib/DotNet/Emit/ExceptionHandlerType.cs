using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009DD RID: 2525
	[Flags]
	[ComVisible(true)]
	public enum ExceptionHandlerType
	{
		// Token: 0x0400308D RID: 12429
		Catch = 0,
		// Token: 0x0400308E RID: 12430
		Filter = 1,
		// Token: 0x0400308F RID: 12431
		Finally = 2,
		// Token: 0x04003090 RID: 12432
		Fault = 4,
		// Token: 0x04003091 RID: 12433
		Duplicated = 8
	}
}
