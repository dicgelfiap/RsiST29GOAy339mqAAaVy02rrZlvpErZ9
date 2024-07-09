using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007D2 RID: 2002
	[ComVisible(true)]
	public interface ITypeDefOrRef : ICodedToken, IMDTokenProvider, IHasCustomAttribute, IMemberRefParent, IFullName, IType, IOwnerModule, IGenericParameterProvider, IIsTypeOrMethod, IContainsGenericParameter, ITokenOperand, IMemberRef
	{
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06004894 RID: 18580
		int TypeDefOrRefTag { get; }
	}
}
