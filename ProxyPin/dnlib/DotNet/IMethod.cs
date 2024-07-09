using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007CF RID: 1999
	[ComVisible(true)]
	public interface IMethod : ICodedToken, IMDTokenProvider, ITokenOperand, IFullName, IGenericParameterProvider, IIsTypeOrMethod, IMemberRef, IOwnerModule
	{
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06004892 RID: 18578
		// (set) Token: 0x06004893 RID: 18579
		MethodSig MethodSig { get; set; }
	}
}
