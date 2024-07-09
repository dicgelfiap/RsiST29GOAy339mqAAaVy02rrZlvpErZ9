using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007DD RID: 2013
	[ComVisible(true)]
	public interface IResolutionScope : ICodedToken, IMDTokenProvider, IHasCustomAttribute, IFullName
	{
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x060048AB RID: 18603
		int ResolutionScopeTag { get; }
	}
}
