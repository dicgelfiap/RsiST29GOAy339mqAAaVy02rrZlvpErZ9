using System;
using System.Collections.Generic;
using System.Text;
using dnlib.DotNet.MD;
using dnlib.DotNet.Writer;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x0200093A RID: 2362
	internal readonly struct ImportScopeBlobWriter
	{
		// Token: 0x06005ADC RID: 23260 RVA: 0x001BA58C File Offset: 0x001BA58C
		private ImportScopeBlobWriter(IWriterError helper, dnlib.DotNet.Writer.Metadata systemMetadata, BlobHeap blobHeap)
		{
			this.helper = helper;
			this.systemMetadata = systemMetadata;
			this.blobHeap = blobHeap;
		}

		// Token: 0x06005ADD RID: 23261 RVA: 0x001BA5A4 File Offset: 0x001BA5A4
		public static void Write(IWriterError helper, dnlib.DotNet.Writer.Metadata systemMetadata, DataWriter writer, BlobHeap blobHeap, IList<PdbImport> imports)
		{
			ImportScopeBlobWriter importScopeBlobWriter = new ImportScopeBlobWriter(helper, systemMetadata, blobHeap);
			importScopeBlobWriter.Write(writer, imports);
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x001BA5CC File Offset: 0x001BA5CC
		private uint WriteUTF8(string s)
		{
			if (s == null)
			{
				this.helper.Error("String is null");
				s = string.Empty;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			return this.blobHeap.Add(bytes);
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x001BA614 File Offset: 0x001BA614
		private void Write(DataWriter writer, IList<PdbImport> imports)
		{
			int count = imports.Count;
			for (int i = 0; i < count; i++)
			{
				PdbImport pdbImport = imports[i];
				uint value;
				if (!ImportDefinitionKindUtils.ToImportDefinitionKind(pdbImport.Kind, out value))
				{
					this.helper.Error("Unknown import definition kind: " + pdbImport.Kind.ToString());
					return;
				}
				writer.WriteCompressedUInt32(value);
				switch (pdbImport.Kind)
				{
				case PdbImportDefinitionKind.ImportNamespace:
					writer.WriteCompressedUInt32(this.WriteUTF8(((PdbImportNamespace)pdbImport).TargetNamespace));
					break;
				case PdbImportDefinitionKind.ImportAssemblyNamespace:
					writer.WriteCompressedUInt32(this.systemMetadata.GetToken(((PdbImportAssemblyNamespace)pdbImport).TargetAssembly).Rid);
					writer.WriteCompressedUInt32(this.WriteUTF8(((PdbImportAssemblyNamespace)pdbImport).TargetNamespace));
					break;
				case PdbImportDefinitionKind.ImportType:
					writer.WriteCompressedUInt32(this.GetTypeDefOrRefEncodedToken(((PdbImportType)pdbImport).TargetType));
					break;
				case PdbImportDefinitionKind.ImportXmlNamespace:
					writer.WriteCompressedUInt32(this.WriteUTF8(((PdbImportXmlNamespace)pdbImport).Alias));
					writer.WriteCompressedUInt32(this.WriteUTF8(((PdbImportXmlNamespace)pdbImport).TargetNamespace));
					break;
				case PdbImportDefinitionKind.ImportAssemblyReferenceAlias:
					writer.WriteCompressedUInt32(this.WriteUTF8(((PdbImportAssemblyReferenceAlias)pdbImport).Alias));
					break;
				case PdbImportDefinitionKind.AliasAssemblyReference:
					writer.WriteCompressedUInt32(this.WriteUTF8(((PdbAliasAssemblyReference)pdbImport).Alias));
					writer.WriteCompressedUInt32(this.systemMetadata.GetToken(((PdbAliasAssemblyReference)pdbImport).TargetAssembly).Rid);
					break;
				case PdbImportDefinitionKind.AliasNamespace:
					writer.WriteCompressedUInt32(this.WriteUTF8(((PdbAliasNamespace)pdbImport).Alias));
					writer.WriteCompressedUInt32(this.WriteUTF8(((PdbAliasNamespace)pdbImport).TargetNamespace));
					break;
				case PdbImportDefinitionKind.AliasAssemblyNamespace:
					writer.WriteCompressedUInt32(this.WriteUTF8(((PdbAliasAssemblyNamespace)pdbImport).Alias));
					writer.WriteCompressedUInt32(this.systemMetadata.GetToken(((PdbAliasAssemblyNamespace)pdbImport).TargetAssembly).Rid);
					writer.WriteCompressedUInt32(this.WriteUTF8(((PdbAliasAssemblyNamespace)pdbImport).TargetNamespace));
					break;
				case PdbImportDefinitionKind.AliasType:
					writer.WriteCompressedUInt32(this.WriteUTF8(((PdbAliasType)pdbImport).Alias));
					writer.WriteCompressedUInt32(this.GetTypeDefOrRefEncodedToken(((PdbAliasType)pdbImport).TargetType));
					break;
				default:
					this.helper.Error("Unknown import definition kind: " + pdbImport.Kind.ToString());
					return;
				}
			}
		}

		// Token: 0x06005AE0 RID: 23264 RVA: 0x001BA8B4 File Offset: 0x001BA8B4
		private uint GetTypeDefOrRefEncodedToken(ITypeDefOrRef tdr)
		{
			if (tdr == null)
			{
				this.helper.Error("ITypeDefOrRef is null");
				return 0U;
			}
			MDToken token = this.systemMetadata.GetToken(tdr);
			uint result;
			if (CodedToken.TypeDefOrRef.Encode(token, out result))
			{
				return result;
			}
			this.helper.Error(string.Format("Could not encode token 0x{0:X8}", token.Raw));
			return 0U;
		}

		// Token: 0x04002BEA RID: 11242
		private readonly IWriterError helper;

		// Token: 0x04002BEB RID: 11243
		private readonly dnlib.DotNet.Writer.Metadata systemMetadata;

		// Token: 0x04002BEC RID: 11244
		private readonly BlobHeap blobHeap;
	}
}
