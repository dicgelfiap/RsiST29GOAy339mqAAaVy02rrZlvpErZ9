using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000911 RID: 2321
	[ComVisible(true)]
	public enum PdbImportDefinitionKind
	{
		// Token: 0x04002B73 RID: 11123
		ImportNamespace,
		// Token: 0x04002B74 RID: 11124
		ImportAssemblyNamespace,
		// Token: 0x04002B75 RID: 11125
		ImportType,
		// Token: 0x04002B76 RID: 11126
		ImportXmlNamespace,
		// Token: 0x04002B77 RID: 11127
		ImportAssemblyReferenceAlias,
		// Token: 0x04002B78 RID: 11128
		AliasAssemblyReference,
		// Token: 0x04002B79 RID: 11129
		AliasNamespace,
		// Token: 0x04002B7A RID: 11130
		AliasAssemblyNamespace,
		// Token: 0x04002B7B RID: 11131
		AliasType
	}
}
