using System;
using System.Collections.Generic;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x02000941 RID: 2369
	internal sealed class PortablePdbReader : SymbolReader
	{
		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06005B13 RID: 23315 RVA: 0x001BC834 File Offset: 0x001BC834
		public override PdbFileKind PdbFileKind
		{
			get
			{
				return this.pdbFileKind;
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06005B14 RID: 23316 RVA: 0x001BC83C File Offset: 0x001BC83C
		public override int UserEntryPoint
		{
			get
			{
				return this.pdbMetadata.PdbStream.EntryPoint.ToInt32();
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06005B15 RID: 23317 RVA: 0x001BC868 File Offset: 0x001BC868
		public override IList<SymbolDocument> Documents
		{
			get
			{
				return this.documents;
			}
		}

		// Token: 0x06005B16 RID: 23318 RVA: 0x001BC870 File Offset: 0x001BC870
		public PortablePdbReader(DataReaderFactory pdbStream, PdbFileKind pdbFileKind)
		{
			this.pdbFileKind = pdbFileKind;
			this.pdbMetadata = MetadataFactory.CreateStandalonePortablePDB(pdbStream, true);
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x001BC88C File Offset: 0x001BC88C
		internal bool MatchesModule(Guid pdbGuid, uint timestamp, uint age)
		{
			PdbStream pdbStream = this.pdbMetadata.PdbStream;
			if (pdbStream != null)
			{
				byte[] id = pdbStream.Id;
				Array.Resize<byte>(ref id, 16);
				return !(new Guid(id) != pdbGuid) && BitConverter.ToUInt32(pdbStream.Id, 16) == timestamp && age == 1U;
			}
			return false;
		}

		// Token: 0x06005B18 RID: 23320 RVA: 0x001BC8F4 File Offset: 0x001BC8F4
		public override void Initialize(ModuleDef module)
		{
			this.module = module;
			this.documents = this.ReadDocuments();
		}

		// Token: 0x06005B19 RID: 23321 RVA: 0x001BC90C File Offset: 0x001BC90C
		private static Guid GetLanguageVendor(Guid language)
		{
			if (language == PdbDocumentConstants.LanguageCSharp || language == PdbDocumentConstants.LanguageVisualBasic || language == PdbDocumentConstants.LanguageFSharp)
			{
				return PdbDocumentConstants.LanguageVendorMicrosoft;
			}
			return Guid.Empty;
		}

		// Token: 0x06005B1A RID: 23322 RVA: 0x001BC94C File Offset: 0x001BC94C
		private SymbolDocument[] ReadDocuments()
		{
			SymbolDocument[] array = new SymbolDocument[this.pdbMetadata.TablesStream.DocumentTable.Rows];
			DocumentNameReader documentNameReader = new DocumentNameReader(this.pdbMetadata.BlobStream);
			List<PdbCustomDebugInfo> list = ListCache<PdbCustomDebugInfo>.AllocList();
			GenericParamContext gpContext = default(GenericParamContext);
			for (int i = 0; i < array.Length; i++)
			{
				RawDocumentRow rawDocumentRow;
				this.pdbMetadata.TablesStream.TryReadDocumentRow((uint)(i + 1), out rawDocumentRow);
				string url = documentNameReader.ReadDocumentName(rawDocumentRow.Name);
				Guid language = this.pdbMetadata.GuidStream.Read(rawDocumentRow.Language) ?? Guid.Empty;
				Guid languageVendor = PortablePdbReader.GetLanguageVendor(language);
				Guid documentTypeText = PdbDocumentConstants.DocumentTypeText;
				Guid checkSumAlgorithmId = this.pdbMetadata.GuidStream.Read(rawDocumentRow.HashAlgorithm) ?? Guid.Empty;
				byte[] checkSum = this.pdbMetadata.BlobStream.ReadNoNull(rawDocumentRow.Hash);
				int token = new MDToken(Table.Document, i + 1).ToInt32();
				list.Clear();
				this.GetCustomDebugInfos(token, gpContext, list);
				PdbCustomDebugInfo[] customDebugInfos = (list.Count == 0) ? Array2.Empty<PdbCustomDebugInfo>() : list.ToArray();
				array[i] = new SymbolDocumentImpl(url, language, languageVendor, documentTypeText, checkSumAlgorithmId, checkSum, customDebugInfos);
			}
			ListCache<PdbCustomDebugInfo>.Free(ref list);
			return array;
		}

		// Token: 0x06005B1B RID: 23323 RVA: 0x001BCAD8 File Offset: 0x001BCAD8
		private bool TryGetSymbolDocument(uint rid, out SymbolDocument document)
		{
			int num = (int)(rid - 1U);
			if (num >= this.documents.Length)
			{
				document = null;
				return false;
			}
			document = this.documents[num];
			return true;
		}

		// Token: 0x06005B1C RID: 23324 RVA: 0x001BCB10 File Offset: 0x001BCB10
		public override SymbolMethod GetMethod(MethodDef method, int version)
		{
			if (version != 1)
			{
				return null;
			}
			MDTable methodDebugInformationTable = this.pdbMetadata.TablesStream.MethodDebugInformationTable;
			uint rid = method.Rid;
			if (!methodDebugInformationTable.IsValidRID(rid))
			{
				return null;
			}
			SymbolSequencePoint[] sequencePoints = this.ReadSequencePoints(rid) ?? Array2.Empty<SymbolSequencePoint>();
			GenericParamContext gpContext = GenericParamContext.Create(method);
			SymbolScopeImpl symbolScopeImpl = this.ReadScope(rid, gpContext);
			int kickoffMethod = this.GetKickoffMethod(rid);
			SymbolMethodImpl symbolMethodImpl = new SymbolMethodImpl(this, method.MDToken.ToInt32(), symbolScopeImpl, sequencePoints, kickoffMethod);
			symbolScopeImpl.method = symbolMethodImpl;
			return symbolMethodImpl;
		}

		// Token: 0x06005B1D RID: 23325 RVA: 0x001BCBA0 File Offset: 0x001BCBA0
		private int GetKickoffMethod(uint methodRid)
		{
			uint stateMachineMethodRid = this.pdbMetadata.GetStateMachineMethodRid(methodRid);
			if (stateMachineMethodRid == 0U)
			{
				return 0;
			}
			RawStateMachineMethodRow rawStateMachineMethodRow;
			if (!this.pdbMetadata.TablesStream.TryReadStateMachineMethodRow(stateMachineMethodRid, out rawStateMachineMethodRow))
			{
				return 0;
			}
			return (int)(100663296U + rawStateMachineMethodRow.KickoffMethod);
		}

		// Token: 0x06005B1E RID: 23326 RVA: 0x001BCBEC File Offset: 0x001BCBEC
		private SymbolSequencePoint[] ReadSequencePoints(uint methodRid)
		{
			if (!this.pdbMetadata.TablesStream.MethodDebugInformationTable.IsValidRID(methodRid))
			{
				return null;
			}
			RawMethodDebugInformationRow rawMethodDebugInformationRow;
			if (!this.pdbMetadata.TablesStream.TryReadMethodDebugInformationRow(methodRid, out rawMethodDebugInformationRow))
			{
				return null;
			}
			if (rawMethodDebugInformationRow.SequencePoints == 0U)
			{
				return null;
			}
			uint num = rawMethodDebugInformationRow.Document;
			DataReader dataReader;
			if (!this.pdbMetadata.BlobStream.TryCreateReader(rawMethodDebugInformationRow.SequencePoints, out dataReader))
			{
				return null;
			}
			List<SymbolSequencePoint> list = ListCache<SymbolSequencePoint>.AllocList();
			dataReader.ReadCompressedUInt32();
			if (num == 0U)
			{
				num = dataReader.ReadCompressedUInt32();
			}
			SymbolDocument symbolDocument;
			this.TryGetSymbolDocument(num, out symbolDocument);
			uint num2 = uint.MaxValue;
			int num3 = -1;
			int num4 = 0;
			bool flag = false;
			while (dataReader.Position < dataReader.Length)
			{
				uint num5 = dataReader.ReadCompressedUInt32();
				if (num5 == 0U && flag)
				{
					num = dataReader.ReadCompressedUInt32();
					this.TryGetSymbolDocument(num, out symbolDocument);
				}
				else
				{
					if (symbolDocument == null)
					{
						return null;
					}
					SymbolSequencePoint item = new SymbolSequencePoint
					{
						Document = symbolDocument
					};
					if (num2 == 4294967295U)
					{
						num2 = num5;
					}
					else
					{
						if (num5 == 0U)
						{
							return null;
						}
						num2 += num5;
					}
					item.Offset = (int)num2;
					uint num6 = dataReader.ReadCompressedUInt32();
					int num7 = (int)((num6 == 0U) ? dataReader.ReadCompressedUInt32() : ((uint)dataReader.ReadCompressedInt32()));
					if (num6 == 0U && num7 == 0)
					{
						item.Line = 16707566;
						item.EndLine = 16707566;
						item.Column = 0;
						item.EndColumn = 0;
					}
					else
					{
						if (num3 < 0)
						{
							num3 = (int)dataReader.ReadCompressedUInt32();
							num4 = (int)dataReader.ReadCompressedUInt32();
						}
						else
						{
							num3 += dataReader.ReadCompressedInt32();
							num4 += dataReader.ReadCompressedInt32();
						}
						item.Line = num3;
						item.EndLine = num3 + (int)num6;
						item.Column = num4;
						item.EndColumn = num4 + num7;
					}
					list.Add(item);
				}
				flag = true;
			}
			return ListCache<SymbolSequencePoint>.FreeAndToArray(ref list);
		}

		// Token: 0x06005B1F RID: 23327 RVA: 0x001BCDFC File Offset: 0x001BCDFC
		private SymbolScopeImpl ReadScope(uint methodRid, GenericParamContext gpContext)
		{
			RidList localScopeRidList = this.pdbMetadata.GetLocalScopeRidList(methodRid);
			SymbolScopeImpl symbolScopeImpl = null;
			if (localScopeRidList.Count != 0)
			{
				List<PdbCustomDebugInfo> list = ListCache<PdbCustomDebugInfo>.AllocList();
				List<SymbolScopeImpl> list2 = ListCache<SymbolScopeImpl>.AllocList();
				ImportScopeBlobReader importScopeBlobReader = new ImportScopeBlobReader(this.module, this.pdbMetadata.BlobStream);
				for (int i = 0; i < localScopeRidList.Count; i++)
				{
					uint num = localScopeRidList[i];
					int token = new MDToken(Table.LocalScope, num).ToInt32();
					RawLocalScopeRow rawLocalScopeRow;
					this.pdbMetadata.TablesStream.TryReadLocalScopeRow(num, out rawLocalScopeRow);
					uint startOffset = rawLocalScopeRow.StartOffset;
					uint num2 = startOffset + rawLocalScopeRow.Length;
					SymbolScopeImpl symbolScopeImpl2 = null;
					while (list2.Count > 0)
					{
						SymbolScopeImpl symbolScopeImpl3 = list2[list2.Count - 1];
						if ((ulong)startOffset >= (ulong)((long)symbolScopeImpl3.StartOffset) && (ulong)num2 <= (ulong)((long)symbolScopeImpl3.EndOffset))
						{
							symbolScopeImpl2 = symbolScopeImpl3;
							break;
						}
						list2.RemoveAt(list2.Count - 1);
					}
					list.Clear();
					this.GetCustomDebugInfos(token, gpContext, list);
					PdbCustomDebugInfo[] customDebugInfos = (list.Count == 0) ? Array2.Empty<PdbCustomDebugInfo>() : list.ToArray();
					SymbolScopeImpl symbolScopeImpl4 = new SymbolScopeImpl(this, symbolScopeImpl2, (int)startOffset, (int)num2, customDebugInfos);
					if (symbolScopeImpl == null)
					{
						symbolScopeImpl = symbolScopeImpl4;
					}
					list2.Add(symbolScopeImpl4);
					if (symbolScopeImpl2 != null)
					{
						symbolScopeImpl2.childrenList.Add(symbolScopeImpl4);
					}
					symbolScopeImpl4.importScope = this.ReadPdbImportScope(ref importScopeBlobReader, rawLocalScopeRow.ImportScope, gpContext);
					uint variableListEnd;
					uint constantListEnd;
					this.GetEndOfLists(num, out variableListEnd, out constantListEnd);
					this.ReadVariables(symbolScopeImpl4, gpContext, rawLocalScopeRow.VariableList, variableListEnd);
					this.ReadConstants(symbolScopeImpl4, rawLocalScopeRow.ConstantList, constantListEnd);
				}
				ListCache<SymbolScopeImpl>.Free(ref list2);
				ListCache<PdbCustomDebugInfo>.Free(ref list);
			}
			return symbolScopeImpl ?? new SymbolScopeImpl(this, null, 0, int.MaxValue, Array2.Empty<PdbCustomDebugInfo>());
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x001BCFDC File Offset: 0x001BCFDC
		private void GetEndOfLists(uint scopeRid, out uint variableListEnd, out uint constantListEnd)
		{
			uint rid = scopeRid + 1U;
			RawLocalScopeRow rawLocalScopeRow;
			if (!this.pdbMetadata.TablesStream.TryReadLocalScopeRow(rid, out rawLocalScopeRow))
			{
				variableListEnd = this.pdbMetadata.TablesStream.LocalVariableTable.Rows + 1U;
				constantListEnd = this.pdbMetadata.TablesStream.LocalConstantTable.Rows + 1U;
				return;
			}
			variableListEnd = rawLocalScopeRow.VariableList;
			constantListEnd = rawLocalScopeRow.ConstantList;
		}

		// Token: 0x06005B21 RID: 23329 RVA: 0x001BD04C File Offset: 0x001BD04C
		private PdbImportScope ReadPdbImportScope(ref ImportScopeBlobReader importScopeBlobReader, uint importScope, GenericParamContext gpContext)
		{
			if (importScope == 0U)
			{
				return null;
			}
			PdbImportScope pdbImportScope = null;
			PdbImportScope pdbImportScope2 = null;
			int num = 0;
			while (importScope != 0U)
			{
				if (num >= 1000)
				{
					return null;
				}
				int token = new MDToken(Table.ImportScope, importScope).ToInt32();
				RawImportScopeRow rawImportScopeRow;
				if (!this.pdbMetadata.TablesStream.TryReadImportScopeRow(importScope, out rawImportScopeRow))
				{
					return null;
				}
				PdbImportScope pdbImportScope3 = new PdbImportScope();
				this.GetCustomDebugInfos(token, gpContext, pdbImportScope3.CustomDebugInfos);
				if (pdbImportScope == null)
				{
					pdbImportScope = pdbImportScope3;
				}
				if (pdbImportScope2 != null)
				{
					pdbImportScope2.Parent = pdbImportScope3;
				}
				importScopeBlobReader.Read(rawImportScopeRow.Imports, pdbImportScope3.Imports);
				pdbImportScope2 = pdbImportScope3;
				importScope = rawImportScopeRow.Parent;
				num++;
			}
			return pdbImportScope;
		}

		// Token: 0x06005B22 RID: 23330 RVA: 0x001BD104 File Offset: 0x001BD104
		private void ReadVariables(SymbolScopeImpl scope, GenericParamContext gpContext, uint variableList, uint variableListEnd)
		{
			if (variableList == 0U)
			{
				return;
			}
			if (variableList >= variableListEnd)
			{
				return;
			}
			MDTable localVariableTable = this.pdbMetadata.TablesStream.LocalVariableTable;
			if (!localVariableTable.IsValidRID(variableListEnd - 1U))
			{
				return;
			}
			if (!localVariableTable.IsValidRID(variableList))
			{
				return;
			}
			List<PdbCustomDebugInfo> list = ListCache<PdbCustomDebugInfo>.AllocList();
			for (uint num = variableList; num < variableListEnd; num += 1U)
			{
				int token = new MDToken(Table.LocalVariable, num).ToInt32();
				list.Clear();
				this.GetCustomDebugInfos(token, gpContext, list);
				PdbCustomDebugInfo[] customDebugInfos = (list.Count == 0) ? Array2.Empty<PdbCustomDebugInfo>() : list.ToArray();
				RawLocalVariableRow rawLocalVariableRow;
				this.pdbMetadata.TablesStream.TryReadLocalVariableRow(num, out rawLocalVariableRow);
				UTF8String s = this.pdbMetadata.StringsStream.Read(rawLocalVariableRow.Name);
				scope.localsList.Add(new SymbolVariableImpl(s, PortablePdbReader.ToSymbolVariableAttributes(rawLocalVariableRow.Attributes), (int)rawLocalVariableRow.Index, customDebugInfos));
			}
			ListCache<PdbCustomDebugInfo>.Free(ref list);
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x001BD20C File Offset: 0x001BD20C
		private static PdbLocalAttributes ToSymbolVariableAttributes(ushort attributes)
		{
			PdbLocalAttributes pdbLocalAttributes = PdbLocalAttributes.None;
			if ((attributes & 1) != 0)
			{
				pdbLocalAttributes |= PdbLocalAttributes.DebuggerHidden;
			}
			return pdbLocalAttributes;
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x001BD22C File Offset: 0x001BD22C
		private void ReadConstants(SymbolScopeImpl scope, uint constantList, uint constantListEnd)
		{
			if (constantList == 0U)
			{
				return;
			}
			if (constantList >= constantListEnd)
			{
				return;
			}
			MDTable localConstantTable = this.pdbMetadata.TablesStream.LocalConstantTable;
			if (!localConstantTable.IsValidRID(constantListEnd - 1U))
			{
				return;
			}
			if (!localConstantTable.IsValidRID(constantList))
			{
				return;
			}
			scope.SetConstants(this.pdbMetadata, constantList, constantListEnd);
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x001BD288 File Offset: 0x001BD288
		internal void GetCustomDebugInfos(SymbolMethodImpl symMethod, MethodDef method, CilBody body, IList<PdbCustomDebugInfo> result)
		{
			PdbAsyncMethodSteppingInformationCustomDebugInfo pdbAsyncMethodSteppingInformationCustomDebugInfo;
			this.GetCustomDebugInfos(method.MDToken.ToInt32(), GenericParamContext.Create(method), result, method, body, out pdbAsyncMethodSteppingInformationCustomDebugInfo);
			if (pdbAsyncMethodSteppingInformationCustomDebugInfo != null)
			{
				PdbAsyncMethodCustomDebugInfo pdbAsyncMethodCustomDebugInfo = this.TryCreateAsyncMethod(this.module, symMethod.KickoffMethod, pdbAsyncMethodSteppingInformationCustomDebugInfo.AsyncStepInfos, pdbAsyncMethodSteppingInformationCustomDebugInfo.CatchHandler);
				if (pdbAsyncMethodCustomDebugInfo != null)
				{
					result.Add(pdbAsyncMethodCustomDebugInfo);
					return;
				}
			}
			else if (symMethod.KickoffMethod != 0)
			{
				PdbIteratorMethodCustomDebugInfo pdbIteratorMethodCustomDebugInfo = this.TryCreateIteratorMethod(this.module, symMethod.KickoffMethod);
				if (pdbIteratorMethodCustomDebugInfo != null)
				{
					result.Add(pdbIteratorMethodCustomDebugInfo);
				}
			}
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x001BD31C File Offset: 0x001BD31C
		private PdbAsyncMethodCustomDebugInfo TryCreateAsyncMethod(ModuleDef module, int asyncKickoffMethod, IList<PdbAsyncStepInfo> asyncStepInfos, Instruction asyncCatchHandler)
		{
			MDToken mdToken = new MDToken(asyncKickoffMethod);
			if (mdToken.Table != Table.Method)
			{
				return null;
			}
			PdbAsyncMethodCustomDebugInfo pdbAsyncMethodCustomDebugInfo = new PdbAsyncMethodCustomDebugInfo(asyncStepInfos.Count);
			pdbAsyncMethodCustomDebugInfo.KickoffMethod = (module.ResolveToken(mdToken) as MethodDef);
			pdbAsyncMethodCustomDebugInfo.CatchHandlerInstruction = asyncCatchHandler;
			int count = asyncStepInfos.Count;
			for (int i = 0; i < count; i++)
			{
				pdbAsyncMethodCustomDebugInfo.StepInfos.Add(asyncStepInfos[i]);
			}
			return pdbAsyncMethodCustomDebugInfo;
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x001BD394 File Offset: 0x001BD394
		private PdbIteratorMethodCustomDebugInfo TryCreateIteratorMethod(ModuleDef module, int iteratorKickoffMethod)
		{
			MDToken mdToken = new MDToken(iteratorKickoffMethod);
			if (mdToken.Table != Table.Method)
			{
				return null;
			}
			return new PdbIteratorMethodCustomDebugInfo(module.ResolveToken(mdToken) as MethodDef);
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x001BD3D0 File Offset: 0x001BD3D0
		public override void GetCustomDebugInfos(int token, GenericParamContext gpContext, IList<PdbCustomDebugInfo> result)
		{
			PdbAsyncMethodSteppingInformationCustomDebugInfo pdbAsyncMethodSteppingInformationCustomDebugInfo;
			this.GetCustomDebugInfos(token, gpContext, result, null, null, out pdbAsyncMethodSteppingInformationCustomDebugInfo);
		}

		// Token: 0x06005B29 RID: 23337 RVA: 0x001BD3F0 File Offset: 0x001BD3F0
		private void GetCustomDebugInfos(int token, GenericParamContext gpContext, IList<PdbCustomDebugInfo> result, MethodDef methodOpt, CilBody bodyOpt, out PdbAsyncMethodSteppingInformationCustomDebugInfo asyncStepInfo)
		{
			asyncStepInfo = null;
			MDToken mdtoken = new MDToken(token);
			RidList customDebugInformationRidList = this.pdbMetadata.GetCustomDebugInformationRidList(mdtoken.Table, mdtoken.Rid);
			if (customDebugInformationRidList.Count == 0)
			{
				return;
			}
			TypeDef typeOpt = (methodOpt != null) ? methodOpt.DeclaringType : null;
			for (int i = 0; i < customDebugInformationRidList.Count; i++)
			{
				uint rid = customDebugInformationRidList[i];
				RawCustomDebugInformationRow rawCustomDebugInformationRow;
				if (this.pdbMetadata.TablesStream.TryReadCustomDebugInformationRow(rid, out rawCustomDebugInformationRow))
				{
					Guid? guid = this.pdbMetadata.GuidStream.Read(rawCustomDebugInformationRow.Kind);
					DataReader dataReader;
					if (this.pdbMetadata.BlobStream.TryCreateReader(rawCustomDebugInformationRow.Value, out dataReader) && guid != null)
					{
						PdbCustomDebugInfo pdbCustomDebugInfo = PortablePdbCustomDebugInfoReader.Read(this.module, typeOpt, bodyOpt, gpContext, guid.Value, ref dataReader);
						if (pdbCustomDebugInfo != null)
						{
							PdbAsyncMethodSteppingInformationCustomDebugInfo pdbAsyncMethodSteppingInformationCustomDebugInfo = pdbCustomDebugInfo as PdbAsyncMethodSteppingInformationCustomDebugInfo;
							if (pdbAsyncMethodSteppingInformationCustomDebugInfo != null)
							{
								asyncStepInfo = pdbAsyncMethodSteppingInformationCustomDebugInfo;
							}
							else
							{
								result.Add(pdbCustomDebugInfo);
							}
						}
					}
				}
			}
		}

		// Token: 0x06005B2A RID: 23338 RVA: 0x001BD50C File Offset: 0x001BD50C
		public override void Dispose()
		{
			this.pdbMetadata.Dispose();
		}

		// Token: 0x04002C04 RID: 11268
		private readonly PdbFileKind pdbFileKind;

		// Token: 0x04002C05 RID: 11269
		private ModuleDef module;

		// Token: 0x04002C06 RID: 11270
		private readonly Metadata pdbMetadata;

		// Token: 0x04002C07 RID: 11271
		private SymbolDocument[] documents;
	}
}
