using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007F1 RID: 2033
	[ComVisible(true)]
	public interface ITypeResolver
	{
		// Token: 0x06004961 RID: 18785
		TypeDef Resolve(TypeRef typeRef, ModuleDef sourceModule);
	}
}
