using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007C9 RID: 1993
	[ComVisible(true)]
	public interface IIsTypeOrMethod
	{
		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06004880 RID: 18560
		bool IsType { get; }

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06004881 RID: 18561
		bool IsMethod { get; }
	}
}
