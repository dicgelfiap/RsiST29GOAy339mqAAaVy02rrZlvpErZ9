using System;
using System.Collections.Generic;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x02000939 RID: 2361
	internal readonly struct ImportScopeBlobReader
	{
		// Token: 0x06005AD7 RID: 23255 RVA: 0x001BA308 File Offset: 0x001BA308
		public ImportScopeBlobReader(ModuleDef module, BlobStream blobStream)
		{
			this.module = module;
			this.blobStream = blobStream;
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x001BA318 File Offset: 0x001BA318
		public void Read(uint imports, IList<PdbImport> result)
		{
			if (imports == 0U)
			{
				return;
			}
			DataReader dataReader;
			if (!this.blobStream.TryCreateReader(imports, out dataReader))
			{
				return;
			}
			while (dataReader.Position < dataReader.Length)
			{
				PdbImport pdbImport;
				switch (ImportDefinitionKindUtils.ToPdbImportDefinitionKind(dataReader.ReadCompressedUInt32()))
				{
				case (PdbImportDefinitionKind)(-1):
					pdbImport = null;
					break;
				case PdbImportDefinitionKind.ImportNamespace:
				{
					string targetNamespace = this.ReadUTF8(dataReader.ReadCompressedUInt32());
					pdbImport = new PdbImportNamespace(targetNamespace);
					break;
				}
				case PdbImportDefinitionKind.ImportAssemblyNamespace:
				{
					AssemblyRef targetAssembly = this.TryReadAssemblyRef(dataReader.ReadCompressedUInt32());
					string targetNamespace = this.ReadUTF8(dataReader.ReadCompressedUInt32());
					pdbImport = new PdbImportAssemblyNamespace(targetAssembly, targetNamespace);
					break;
				}
				case PdbImportDefinitionKind.ImportType:
				{
					ITypeDefOrRef targetType = this.TryReadType(dataReader.ReadCompressedUInt32());
					pdbImport = new PdbImportType(targetType);
					break;
				}
				case PdbImportDefinitionKind.ImportXmlNamespace:
				{
					string alias = this.ReadUTF8(dataReader.ReadCompressedUInt32());
					string targetNamespace = this.ReadUTF8(dataReader.ReadCompressedUInt32());
					pdbImport = new PdbImportXmlNamespace(alias, targetNamespace);
					break;
				}
				case PdbImportDefinitionKind.ImportAssemblyReferenceAlias:
					pdbImport = new PdbImportAssemblyReferenceAlias(this.ReadUTF8(dataReader.ReadCompressedUInt32()));
					break;
				case PdbImportDefinitionKind.AliasAssemblyReference:
				{
					string alias2 = this.ReadUTF8(dataReader.ReadCompressedUInt32());
					AssemblyRef targetAssembly = this.TryReadAssemblyRef(dataReader.ReadCompressedUInt32());
					pdbImport = new PdbAliasAssemblyReference(alias2, targetAssembly);
					break;
				}
				case PdbImportDefinitionKind.AliasNamespace:
				{
					string alias3 = this.ReadUTF8(dataReader.ReadCompressedUInt32());
					string targetNamespace = this.ReadUTF8(dataReader.ReadCompressedUInt32());
					pdbImport = new PdbAliasNamespace(alias3, targetNamespace);
					break;
				}
				case PdbImportDefinitionKind.AliasAssemblyNamespace:
				{
					string alias4 = this.ReadUTF8(dataReader.ReadCompressedUInt32());
					AssemblyRef targetAssembly = this.TryReadAssemblyRef(dataReader.ReadCompressedUInt32());
					string targetNamespace = this.ReadUTF8(dataReader.ReadCompressedUInt32());
					pdbImport = new PdbAliasAssemblyNamespace(alias4, targetAssembly, targetNamespace);
					break;
				}
				case PdbImportDefinitionKind.AliasType:
				{
					string alias5 = this.ReadUTF8(dataReader.ReadCompressedUInt32());
					ITypeDefOrRef targetType = this.TryReadType(dataReader.ReadCompressedUInt32());
					pdbImport = new PdbAliasType(alias5, targetType);
					break;
				}
				default:
					pdbImport = null;
					break;
				}
				if (pdbImport != null)
				{
					result.Add(pdbImport);
				}
			}
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x001BA500 File Offset: 0x001BA500
		private ITypeDefOrRef TryReadType(uint codedToken)
		{
			uint token;
			if (!CodedToken.TypeDefOrRef.Decode(codedToken, out token))
			{
				return null;
			}
			return this.module.ResolveToken(token) as ITypeDefOrRef;
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x001BA538 File Offset: 0x001BA538
		private AssemblyRef TryReadAssemblyRef(uint rid)
		{
			return this.module.ResolveToken(587202560U + rid) as AssemblyRef;
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x001BA554 File Offset: 0x001BA554
		private string ReadUTF8(uint offset)
		{
			DataReader dataReader;
			if (!this.blobStream.TryCreateReader(offset, out dataReader))
			{
				return string.Empty;
			}
			return dataReader.ReadUtf8String((int)dataReader.BytesLeft);
		}

		// Token: 0x04002BE8 RID: 11240
		private readonly ModuleDef module;

		// Token: 0x04002BE9 RID: 11241
		private readonly BlobStream blobStream;
	}
}
