using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007D8 RID: 2008
	[ComVisible(true)]
	public interface IHasSemantic : ICodedToken, IMDTokenProvider, IHasCustomAttribute, IFullName, IMemberRef, IOwnerModule, IIsTypeOrMethod
	{
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060048A3 RID: 18595
		int HasSemanticTag { get; }
	}
}
