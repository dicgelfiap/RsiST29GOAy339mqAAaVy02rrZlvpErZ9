using System;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x02000938 RID: 2360
	internal static class ImportDefinitionKindUtils
	{
		// Token: 0x06005AD5 RID: 23253 RVA: 0x001BA23C File Offset: 0x001BA23C
		public static PdbImportDefinitionKind ToPdbImportDefinitionKind(uint value)
		{
			switch (value)
			{
			case 1U:
				return PdbImportDefinitionKind.ImportNamespace;
			case 2U:
				return PdbImportDefinitionKind.ImportAssemblyNamespace;
			case 3U:
				return PdbImportDefinitionKind.ImportType;
			case 4U:
				return PdbImportDefinitionKind.ImportXmlNamespace;
			case 5U:
				return PdbImportDefinitionKind.ImportAssemblyReferenceAlias;
			case 6U:
				return PdbImportDefinitionKind.AliasAssemblyReference;
			case 7U:
				return PdbImportDefinitionKind.AliasNamespace;
			case 8U:
				return PdbImportDefinitionKind.AliasAssemblyNamespace;
			case 9U:
				return PdbImportDefinitionKind.AliasType;
			default:
				return (PdbImportDefinitionKind)(-1);
			}
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x001BA294 File Offset: 0x001BA294
		public static bool ToImportDefinitionKind(PdbImportDefinitionKind kind, out uint rawKind)
		{
			switch (kind)
			{
			case PdbImportDefinitionKind.ImportNamespace:
				rawKind = 1U;
				return true;
			case PdbImportDefinitionKind.ImportAssemblyNamespace:
				rawKind = 2U;
				return true;
			case PdbImportDefinitionKind.ImportType:
				rawKind = 3U;
				return true;
			case PdbImportDefinitionKind.ImportXmlNamespace:
				rawKind = 4U;
				return true;
			case PdbImportDefinitionKind.ImportAssemblyReferenceAlias:
				rawKind = 5U;
				return true;
			case PdbImportDefinitionKind.AliasAssemblyReference:
				rawKind = 6U;
				return true;
			case PdbImportDefinitionKind.AliasNamespace:
				rawKind = 7U;
				return true;
			case PdbImportDefinitionKind.AliasAssemblyNamespace:
				rawKind = 8U;
				return true;
			case PdbImportDefinitionKind.AliasType:
				rawKind = 9U;
				return true;
			default:
				rawKind = uint.MaxValue;
				return false;
			}
		}

		// Token: 0x04002BE7 RID: 11239
		public const PdbImportDefinitionKind UNKNOWN_IMPORT_KIND = (PdbImportDefinitionKind)(-1);
	}
}
