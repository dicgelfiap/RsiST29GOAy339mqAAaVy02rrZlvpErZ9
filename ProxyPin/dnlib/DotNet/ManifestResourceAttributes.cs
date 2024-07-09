using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007FC RID: 2044
	[Flags]
	[ComVisible(true)]
	public enum ManifestResourceAttributes : uint
	{
		// Token: 0x0400253C RID: 9532
		VisibilityMask = 7U,
		// Token: 0x0400253D RID: 9533
		Public = 1U,
		// Token: 0x0400253E RID: 9534
		Private = 2U
	}
}
