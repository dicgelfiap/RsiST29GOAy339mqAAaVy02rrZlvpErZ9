using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007DB RID: 2011
	[ComVisible(true)]
	public interface IImplementation : ICodedToken, IMDTokenProvider, IHasCustomAttribute, IFullName
	{
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x060048A9 RID: 18601
		int ImplementationTag { get; }
	}
}
