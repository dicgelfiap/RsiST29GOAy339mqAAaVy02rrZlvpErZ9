using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007EA RID: 2026
	[Flags]
	[ComVisible(true)]
	public enum ImporterOptions
	{
		// Token: 0x04002521 RID: 9505
		TryToUseTypeDefs = 1,
		// Token: 0x04002522 RID: 9506
		TryToUseMethodDefs = 2,
		// Token: 0x04002523 RID: 9507
		TryToUseFieldDefs = 4,
		// Token: 0x04002524 RID: 9508
		TryToUseDefs = 7,
		// Token: 0x04002525 RID: 9509
		FixSignature = -2147483648
	}
}
