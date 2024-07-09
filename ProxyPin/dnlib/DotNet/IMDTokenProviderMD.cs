using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007C1 RID: 1985
	[ComVisible(true)]
	public interface IMDTokenProviderMD : IMDTokenProvider
	{
		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06004858 RID: 18520
		uint OrigRid { get; }
	}
}
