using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007D4 RID: 2004
	[ComVisible(true)]
	public interface IHasCustomAttribute : ICodedToken, IMDTokenProvider
	{
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06004898 RID: 18584
		int HasCustomAttributeTag { get; }

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06004899 RID: 18585
		CustomAttributeCollection CustomAttributes { get; }

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x0600489A RID: 18586
		bool HasCustomAttributes { get; }
	}
}
