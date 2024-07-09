using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000920 RID: 2336
	[Flags]
	[ComVisible(true)]
	public enum PdbReaderOptions
	{
		// Token: 0x04002B98 RID: 11160
		None = 0,
		// Token: 0x04002B99 RID: 11161
		MicrosoftComReader = 1,
		// Token: 0x04002B9A RID: 11162
		NoDiaSymReader = 2,
		// Token: 0x04002B9B RID: 11163
		NoOldDiaSymReader = 4
	}
}
