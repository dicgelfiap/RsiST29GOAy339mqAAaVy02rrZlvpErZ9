using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007DC RID: 2012
	[ComVisible(true)]
	public interface ICustomAttributeType : ICodedToken, IMDTokenProvider, IHasCustomAttribute, IMethod, ITokenOperand, IFullName, IGenericParameterProvider, IIsTypeOrMethod, IMemberRef, IOwnerModule
	{
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x060048AA RID: 18602
		int CustomAttributeTypeTag { get; }
	}
}
