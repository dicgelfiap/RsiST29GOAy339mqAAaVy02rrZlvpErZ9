using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007D7 RID: 2007
	[ComVisible(true)]
	public interface IMemberRefParent : ICodedToken, IMDTokenProvider, IHasCustomAttribute, IFullName
	{
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060048A2 RID: 18594
		int MemberRefParentTag { get; }
	}
}
