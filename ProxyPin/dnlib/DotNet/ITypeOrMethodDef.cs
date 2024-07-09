using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007DE RID: 2014
	[ComVisible(true)]
	public interface ITypeOrMethodDef : ICodedToken, IMDTokenProvider, IHasCustomAttribute, IHasDeclSecurity, IFullName, IMemberRefParent, IMemberRef, IOwnerModule, IIsTypeOrMethod, IGenericParameterProvider
	{
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x060048AC RID: 18604
		int TypeOrMethodDefTag { get; }

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x060048AD RID: 18605
		IList<GenericParam> GenericParameters { get; }

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x060048AE RID: 18606
		bool HasGenericParameters { get; }
	}
}
