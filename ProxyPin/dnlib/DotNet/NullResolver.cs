using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000826 RID: 2086
	[ComVisible(true)]
	public sealed class NullResolver : IAssemblyResolver, IResolver, ITypeResolver, IMemberRefResolver
	{
		// Token: 0x06004DC8 RID: 19912 RVA: 0x00184DBC File Offset: 0x00184DBC
		private NullResolver()
		{
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x00184DC4 File Offset: 0x00184DC4
		public AssemblyDef Resolve(IAssembly assembly, ModuleDef sourceModule)
		{
			return null;
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x00184DC8 File Offset: 0x00184DC8
		public TypeDef Resolve(TypeRef typeRef, ModuleDef sourceModule)
		{
			return null;
		}

		// Token: 0x06004DCB RID: 19915 RVA: 0x00184DCC File Offset: 0x00184DCC
		public IMemberForwarded Resolve(MemberRef memberRef)
		{
			return null;
		}

		// Token: 0x04002672 RID: 9842
		public static readonly NullResolver Instance = new NullResolver();
	}
}
