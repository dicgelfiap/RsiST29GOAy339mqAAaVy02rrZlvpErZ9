using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007D3 RID: 2003
	[ComVisible(true)]
	public interface IHasConstant : ICodedToken, IMDTokenProvider, IHasCustomAttribute, IFullName
	{
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06004895 RID: 18581
		int HasConstantTag { get; }

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06004896 RID: 18582
		// (set) Token: 0x06004897 RID: 18583
		Constant Constant { get; set; }
	}
}
