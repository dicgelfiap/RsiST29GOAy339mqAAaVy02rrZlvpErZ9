using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007AE RID: 1966
	[Flags]
	[ComVisible(true)]
	public enum FileAttributes : uint
	{
		// Token: 0x040024D3 RID: 9427
		ContainsMetadata = 0U,
		// Token: 0x040024D4 RID: 9428
		ContainsNoMetadata = 1U
	}
}
