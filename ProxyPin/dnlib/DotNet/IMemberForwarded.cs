using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007DA RID: 2010
	[ComVisible(true)]
	public interface IMemberForwarded : ICodedToken, IMDTokenProvider, IHasCustomAttribute, IFullName, IMemberRef, IOwnerModule, IIsTypeOrMethod
	{
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060048A5 RID: 18597
		int MemberForwardedTag { get; }

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060048A6 RID: 18598
		// (set) Token: 0x060048A7 RID: 18599
		ImplMap ImplMap { get; set; }

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x060048A8 RID: 18600
		bool HasImplMap { get; }
	}
}
