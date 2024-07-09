using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007F4 RID: 2036
	[ComVisible(true)]
	public interface IType : IFullName, IOwnerModule, ICodedToken, IMDTokenProvider, IGenericParameterProvider, IIsTypeOrMethod, IContainsGenericParameter
	{
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06004964 RID: 18788
		bool IsValueType { get; }

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06004965 RID: 18789
		string TypeName { get; }

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06004966 RID: 18790
		string ReflectionName { get; }

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06004967 RID: 18791
		string Namespace { get; }

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06004968 RID: 18792
		string ReflectionNamespace { get; }

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06004969 RID: 18793
		string ReflectionFullName { get; }

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x0600496A RID: 18794
		string AssemblyQualifiedName { get; }

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x0600496B RID: 18795
		IAssembly DefinitionAssembly { get; }

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x0600496C RID: 18796
		IScope Scope { get; }

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x0600496D RID: 18797
		ITypeDefOrRef ScopeType { get; }

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x0600496E RID: 18798
		bool IsPrimitive { get; }
	}
}
