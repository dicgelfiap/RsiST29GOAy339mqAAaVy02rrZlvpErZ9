using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007C7 RID: 1991
	[ComVisible(true)]
	public interface IFullName
	{
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x0600487C RID: 18556
		string FullName { get; }

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600487D RID: 18557
		// (set) Token: 0x0600487E RID: 18558
		UTF8String Name { get; set; }
	}
}
