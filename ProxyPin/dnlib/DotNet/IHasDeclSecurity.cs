using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007D6 RID: 2006
	[ComVisible(true)]
	public interface IHasDeclSecurity : ICodedToken, IMDTokenProvider, IHasCustomAttribute, IFullName
	{
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x0600489F RID: 18591
		int HasDeclSecurityTag { get; }

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x060048A0 RID: 18592
		IList<DeclSecurity> DeclSecurities { get; }

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x060048A1 RID: 18593
		bool HasDeclSecurities { get; }
	}
}
