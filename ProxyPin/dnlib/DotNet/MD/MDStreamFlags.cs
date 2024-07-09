using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000993 RID: 2451
	[Flags]
	[ComVisible(true)]
	public enum MDStreamFlags : byte
	{
		// Token: 0x04002E1C RID: 11804
		BigStrings = 1,
		// Token: 0x04002E1D RID: 11805
		BigGUID = 2,
		// Token: 0x04002E1E RID: 11806
		BigBlob = 4,
		// Token: 0x04002E1F RID: 11807
		Padding = 8,
		// Token: 0x04002E20 RID: 11808
		DeltaOnly = 32,
		// Token: 0x04002E21 RID: 11809
		ExtraData = 64,
		// Token: 0x04002E22 RID: 11810
		HasDelete = 128
	}
}
