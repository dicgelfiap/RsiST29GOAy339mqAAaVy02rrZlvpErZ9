using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007D9 RID: 2009
	[ComVisible(true)]
	public interface IMethodDefOrRef : ICodedToken, IMDTokenProvider, IHasCustomAttribute, ICustomAttributeType, IMethod, ITokenOperand, IFullName, IGenericParameterProvider, IIsTypeOrMethod, IMemberRef, IOwnerModule
	{
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060048A4 RID: 18596
		int MethodDefOrRefTag { get; }
	}
}
