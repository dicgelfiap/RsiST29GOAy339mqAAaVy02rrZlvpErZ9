using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007A3 RID: 1955
	[Flags]
	[ComVisible(true)]
	public enum EventAttributes : ushort
	{
		// Token: 0x04002488 RID: 9352
		SpecialName = 512,
		// Token: 0x04002489 RID: 9353
		RTSpecialName = 1024
	}
}
