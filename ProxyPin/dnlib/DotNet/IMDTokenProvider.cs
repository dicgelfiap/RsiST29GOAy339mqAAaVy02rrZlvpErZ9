using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007C0 RID: 1984
	[ComVisible(true)]
	public interface IMDTokenProvider
	{
		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06004855 RID: 18517
		MDToken MDToken { get; }

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06004856 RID: 18518
		// (set) Token: 0x06004857 RID: 18519
		uint Rid { get; set; }
	}
}
