using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007C6 RID: 1990
	[ComVisible(true)]
	public interface IScope
	{
		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x0600487A RID: 18554
		ScopeType ScopeType { get; }

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x0600487B RID: 18555
		string ScopeName { get; }
	}
}
