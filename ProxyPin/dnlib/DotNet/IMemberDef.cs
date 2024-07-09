using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007CB RID: 1995
	[ComVisible(true)]
	public interface IMemberDef : IDnlibDef, ICodedToken, IMDTokenProvider, IFullName, IHasCustomAttribute, IMemberRef, IOwnerModule, IIsTypeOrMethod
	{
		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x0600488E RID: 18574
		TypeDef DeclaringType { get; }
	}
}
