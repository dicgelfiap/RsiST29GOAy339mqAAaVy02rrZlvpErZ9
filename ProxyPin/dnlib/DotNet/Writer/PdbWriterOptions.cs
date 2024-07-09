using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008C4 RID: 2244
	[Flags]
	[ComVisible(true)]
	public enum PdbWriterOptions
	{
		// Token: 0x04002992 RID: 10642
		None = 0,
		// Token: 0x04002993 RID: 10643
		NoDiaSymReader = 1,
		// Token: 0x04002994 RID: 10644
		NoOldDiaSymReader = 2,
		// Token: 0x04002995 RID: 10645
		Deterministic = 4,
		// Token: 0x04002996 RID: 10646
		PdbChecksum = 8
	}
}
