using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Writer;

namespace dnlib.DotNet.Pdb.WindowsPdb
{
	// Token: 0x0200092E RID: 2350
	internal sealed class WindowsPdbWriter : IDisposable
	{
		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06005A91 RID: 23185 RVA: 0x001B91B8 File Offset: 0x001B91B8
		// (set) Token: 0x06005A92 RID: 23186 RVA: 0x001B91C0 File Offset: 0x001B91C0
		public ILogger Logger { get; set; }

		// Token: 0x06005A93 RID: 23187 RVA: 0x001B91CC File Offset: 0x001B91CC
		public WindowsPdbWriter(SymbolWriter writer, PdbState pdbState, dnlib.DotNet.Writer.Metadata metadata) : this(pdbState, metadata)
		{
			if (pdbState == null)
			{
				throw new ArgumentNullException("pdbState");
			}
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			writer.Initialize(metadata);
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x001B9228 File Offset: 0x001B9228
		private WindowsPdbWriter(PdbState pdbState, dnlib.DotNet.Writer.Metadata metadata)
		{
			this.pdbState = pdbState;
			this.metadata = metadata;
			this.module = metadata.Module;
			this.instrToOffset = new Dictionary<Instruction, uint>();
			this.customDebugInfoWriterContext = new PdbCustomDebugInfoWriterContext();
			this.localsEndScopeIncValue = (PdbUtils.IsEndInclusive(PdbFileKind.WindowsPDB, pdbState.Compiler) ? 1 : 0);
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x001B92A4 File Offset: 0x001B92A4
		private ISymbolDocumentWriter Add(PdbDocument pdbDoc)
		{
			ISymbolDocumentWriter symbolDocumentWriter;
			if (this.pdbDocs.TryGetValue(pdbDoc, out symbolDocumentWriter))
			{
				return symbolDocumentWriter;
			}
			symbolDocumentWriter = this.writer.DefineDocument(pdbDoc.Url, pdbDoc.Language, pdbDoc.LanguageVendor, pdbDoc.DocumentType);
			symbolDocumentWriter.SetCheckSum(pdbDoc.CheckSumAlgorithmId, pdbDoc.CheckSum);
			PdbEmbeddedSourceCustomDebugInfo pdbEmbeddedSourceCustomDebugInfo;
			if (WindowsPdbWriter.TryGetCustomDebugInfo<PdbEmbeddedSourceCustomDebugInfo>(pdbDoc, out pdbEmbeddedSourceCustomDebugInfo))
			{
				symbolDocumentWriter.SetSource(pdbEmbeddedSourceCustomDebugInfo.SourceCodeBlob);
			}
			this.pdbDocs.Add(pdbDoc, symbolDocumentWriter);
			return symbolDocumentWriter;
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x001B9328 File Offset: 0x001B9328
		private static bool TryGetCustomDebugInfo<TCDI>(IHasCustomDebugInformation hci, out TCDI cdi) where TCDI : PdbCustomDebugInfo
		{
			IList<PdbCustomDebugInfo> customDebugInfos = hci.CustomDebugInfos;
			int count = customDebugInfos.Count;
			for (int i = 0; i < count; i++)
			{
				TCDI tcdi = customDebugInfos[i] as TCDI;
				if (tcdi != null)
				{
					cdi = tcdi;
					return true;
				}
			}
			cdi = default(TCDI);
			return false;
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x001B9384 File Offset: 0x001B9384
		public void Write()
		{
			this.writer.SetUserEntryPoint(this.GetUserEntryPointToken());
			List<PdbCustomDebugInfo> cdiBuilder = new List<PdbCustomDebugInfo>();
			foreach (TypeDef typeDef in this.module.GetTypes())
			{
				if (typeDef != null)
				{
					IList<MethodDef> methods = typeDef.Methods;
					int count = methods.Count;
					for (int i = 0; i < count; i++)
					{
						MethodDef methodDef = methods[i];
						if (methodDef != null && this.ShouldAddMethod(methodDef))
						{
							this.Write(methodDef, cdiBuilder);
						}
					}
				}
			}
			PdbSourceLinkCustomDebugInfo pdbSourceLinkCustomDebugInfo;
			if (WindowsPdbWriter.TryGetCustomDebugInfo<PdbSourceLinkCustomDebugInfo>(this.module, out pdbSourceLinkCustomDebugInfo))
			{
				this.writer.SetSourceLinkData(pdbSourceLinkCustomDebugInfo.FileBlob);
			}
			PdbSourceServerCustomDebugInfo pdbSourceServerCustomDebugInfo;
			if (WindowsPdbWriter.TryGetCustomDebugInfo<PdbSourceServerCustomDebugInfo>(this.module, out pdbSourceServerCustomDebugInfo))
			{
				this.writer.SetSourceServerData(pdbSourceServerCustomDebugInfo.FileBlob);
			}
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x001B9488 File Offset: 0x001B9488
		private bool ShouldAddMethod(MethodDef method)
		{
			CilBody body = method.Body;
			if (body == null)
			{
				return false;
			}
			if (body.HasPdbMethod)
			{
				return true;
			}
			LocalList variables = body.Variables;
			int count = variables.Count;
			for (int i = 0; i < count; i++)
			{
				Local local = variables[i];
				if (local.Name != null)
				{
					return true;
				}
				if (local.Attributes != PdbLocalAttributes.None)
				{
					return true;
				}
			}
			IList<Instruction> instructions = body.Instructions;
			count = instructions.Count;
			for (int j = 0; j < count; j++)
			{
				if (instructions[j].SequencePoint != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x001B9530 File Offset: 0x001B9530
		private void Write(MethodDef method, List<PdbCustomDebugInfo> cdiBuilder)
		{
			uint rid = this.metadata.GetRid(method);
			if (rid == 0U)
			{
				this.Error("Method {0} ({1:X8}) is not defined in this module ({2})", new object[]
				{
					method,
					method.MDToken.Raw,
					this.module
				});
				return;
			}
			WindowsPdbWriter.CurrentMethod currentMethod = new WindowsPdbWriter.CurrentMethod(this, method, this.instrToOffset);
			CilBody body = method.Body;
			MDToken mdtoken = new MDToken(Table.Method, rid);
			this.writer.OpenMethod(mdtoken);
			this.seqPointsHelper.Write(this, currentMethod.Method.Body.Instructions);
			PdbMethod pdbMethod = body.PdbMethod;
			if (pdbMethod == null)
			{
				pdbMethod = (body.PdbMethod = new PdbMethod());
			}
			PdbScope pdbScope = pdbMethod.Scope;
			if (pdbScope == null)
			{
				pdbScope = (pdbMethod.Scope = new PdbScope());
			}
			if (pdbScope.Namespaces.Count == 0 && pdbScope.Variables.Count == 0 && pdbScope.Constants.Count == 0)
			{
				if (pdbScope.Scopes.Count == 0)
				{
					this.writer.OpenScope(0);
					this.writer.CloseScope((int)currentMethod.BodySize);
				}
				else
				{
					IList<PdbScope> scopes = pdbScope.Scopes;
					int count = scopes.Count;
					for (int i = 0; i < count; i++)
					{
						this.WriteScope(ref currentMethod, scopes[i], 0);
					}
				}
			}
			else
			{
				this.WriteScope(ref currentMethod, pdbScope, 0);
			}
			PdbAsyncMethodCustomDebugInfo pdbAsyncMethodCustomDebugInfo;
			this.GetPseudoCustomDebugInfos(method.CustomDebugInfos, cdiBuilder, out pdbAsyncMethodCustomDebugInfo);
			if (cdiBuilder.Count != 0)
			{
				this.customDebugInfoWriterContext.Logger = this.GetLogger();
				byte[] array = PdbCustomDebugInfoWriter.Write(this.metadata, method, this.customDebugInfoWriterContext, cdiBuilder);
				if (array != null)
				{
					this.writer.SetSymAttribute(mdtoken, "MD2", array);
				}
			}
			if (pdbAsyncMethodCustomDebugInfo != null)
			{
				if (!this.writer.SupportsAsyncMethods)
				{
					this.Error("PDB symbol writer doesn't support writing async methods", new object[0]);
				}
				else
				{
					this.WriteAsyncMethod(ref currentMethod, pdbAsyncMethodCustomDebugInfo);
				}
			}
			this.writer.CloseMethod();
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x001B975C File Offset: 0x001B975C
		private void GetPseudoCustomDebugInfos(IList<PdbCustomDebugInfo> customDebugInfos, List<PdbCustomDebugInfo> cdiBuilder, out PdbAsyncMethodCustomDebugInfo asyncMethod)
		{
			cdiBuilder.Clear();
			asyncMethod = null;
			int count = customDebugInfos.Count;
			for (int i = 0; i < count; i++)
			{
				PdbCustomDebugInfo pdbCustomDebugInfo = customDebugInfos[i];
				if (pdbCustomDebugInfo.Kind == PdbCustomDebugInfoKind.AsyncMethod)
				{
					if (asyncMethod != null)
					{
						this.Error("Duplicate async method custom debug info", new object[0]);
					}
					else
					{
						asyncMethod = (PdbAsyncMethodCustomDebugInfo)pdbCustomDebugInfo;
					}
				}
				else if (pdbCustomDebugInfo.Kind > (PdbCustomDebugInfoKind)255)
				{
					this.Error("Custom debug info {0} isn't supported by Windows PDB files", new object[]
					{
						pdbCustomDebugInfo.Kind
					});
				}
				else
				{
					cdiBuilder.Add(pdbCustomDebugInfo);
				}
			}
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x001B980C File Offset: 0x001B980C
		private uint GetMethodToken(MethodDef method)
		{
			uint rid = this.metadata.GetRid(method);
			if (rid == 0U)
			{
				this.Error("Method {0} ({1:X8}) is not defined in this module ({2})", new object[]
				{
					method,
					method.MDToken.Raw,
					this.module
				});
			}
			return new MDToken(Table.Method, rid).Raw;
		}

		// Token: 0x06005A9C RID: 23196 RVA: 0x001B9874 File Offset: 0x001B9874
		private void WriteAsyncMethod(ref WindowsPdbWriter.CurrentMethod info, PdbAsyncMethodCustomDebugInfo asyncMethod)
		{
			if (asyncMethod.KickoffMethod == null)
			{
				this.Error("KickoffMethod is null", new object[0]);
				return;
			}
			uint methodToken = this.GetMethodToken(asyncMethod.KickoffMethod);
			this.writer.DefineKickoffMethod(methodToken);
			if (asyncMethod.CatchHandlerInstruction != null)
			{
				int offset = info.GetOffset(asyncMethod.CatchHandlerInstruction);
				this.writer.DefineCatchHandlerILOffset((uint)offset);
			}
			IList<PdbAsyncStepInfo> stepInfos = asyncMethod.StepInfos;
			uint[] array = new uint[stepInfos.Count];
			uint[] array2 = new uint[stepInfos.Count];
			uint[] array3 = new uint[stepInfos.Count];
			for (int i = 0; i < array.Length; i++)
			{
				PdbAsyncStepInfo pdbAsyncStepInfo = stepInfos[i];
				if (pdbAsyncStepInfo.YieldInstruction == null)
				{
					this.Error("YieldInstruction is null", new object[0]);
					return;
				}
				if (pdbAsyncStepInfo.BreakpointMethod == null)
				{
					this.Error("BreakpointMethod is null", new object[0]);
					return;
				}
				if (pdbAsyncStepInfo.BreakpointInstruction == null)
				{
					this.Error("BreakpointInstruction is null", new object[0]);
					return;
				}
				array[i] = (uint)info.GetOffset(pdbAsyncStepInfo.YieldInstruction);
				array2[i] = (uint)this.GetExternalInstructionOffset(ref info, pdbAsyncStepInfo.BreakpointMethod, pdbAsyncStepInfo.BreakpointInstruction);
				array3[i] = this.GetMethodToken(pdbAsyncStepInfo.BreakpointMethod);
			}
			this.writer.DefineAsyncStepInfo(array, array2, array3);
		}

		// Token: 0x06005A9D RID: 23197 RVA: 0x001B99D8 File Offset: 0x001B99D8
		private int GetExternalInstructionOffset(ref WindowsPdbWriter.CurrentMethod info, MethodDef method, Instruction instr)
		{
			if (info.Method == method)
			{
				return info.GetOffset(instr);
			}
			CilBody body = method.Body;
			if (body == null)
			{
				this.Error("Method body is null", new object[0]);
				return 0;
			}
			IList<Instruction> instructions = body.Instructions;
			int num = 0;
			for (int i = 0; i < instructions.Count; i++)
			{
				Instruction instruction = instructions[i];
				if (instruction == instr)
				{
					return num;
				}
				num += instruction.GetSize();
			}
			if (instr == null)
			{
				return num;
			}
			this.Error("Async method instruction has been removed but it's still being referenced by PDB info: BP Instruction: {0}, BP Method: {1} (0x{2:X8}), Current Method: {3} (0x{4:X8})", new object[]
			{
				instr,
				method,
				method.MDToken.Raw,
				info.Method,
				info.Method.MDToken.Raw
			});
			return 0;
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x001B9AB8 File Offset: 0x001B9AB8
		private void WriteScope(ref WindowsPdbWriter.CurrentMethod info, PdbScope scope, int recursionCounter)
		{
			if (recursionCounter >= 1000)
			{
				this.Error("Too many PdbScopes", new object[0]);
				return;
			}
			int offset = info.GetOffset(scope.Start);
			int offset2 = info.GetOffset(scope.End);
			this.writer.OpenScope(offset);
			this.AddLocals(info.Method, scope.Variables, (uint)offset, (uint)offset2);
			if (scope.Constants.Count > 0)
			{
				IList<PdbConstant> constants = scope.Constants;
				FieldSig fieldSig = new FieldSig();
				for (int i = 0; i < constants.Count; i++)
				{
					PdbConstant pdbConstant = constants[i];
					fieldSig.Type = pdbConstant.Type;
					MDToken token = this.metadata.GetToken(fieldSig);
					this.writer.DefineConstant(pdbConstant.Name, pdbConstant.Value ?? WindowsPdbWriter.boxedZeroInt32, token.Raw);
				}
			}
			IList<string> namespaces = scope.Namespaces;
			int count = namespaces.Count;
			for (int j = 0; j < count; j++)
			{
				this.writer.UsingNamespace(namespaces[j]);
			}
			IList<PdbScope> scopes = scope.Scopes;
			count = scopes.Count;
			for (int k = 0; k < count; k++)
			{
				this.WriteScope(ref info, scopes[k], recursionCounter + 1);
			}
			this.writer.CloseScope((offset == 0 && (long)offset2 == (long)((ulong)info.BodySize)) ? offset2 : (offset2 - this.localsEndScopeIncValue));
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x001B9C48 File Offset: 0x001B9C48
		private void AddLocals(MethodDef method, IList<PdbLocal> locals, uint startOffset, uint endOffset)
		{
			if (locals.Count == 0)
			{
				return;
			}
			uint localVarSigToken = this.metadata.GetLocalVarSigToken(method);
			if (localVarSigToken == 0U)
			{
				this.Error("Method {0} ({1:X8}) has no local signature token", new object[]
				{
					method,
					method.MDToken.Raw
				});
				return;
			}
			int count = locals.Count;
			for (int i = 0; i < count; i++)
			{
				PdbLocal pdbLocal = locals[i];
				uint pdbLocalFlags = WindowsPdbWriter.GetPdbLocalFlags(pdbLocal.Attributes);
				if (pdbLocalFlags != 0U || pdbLocal.Name != null)
				{
					this.writer.DefineLocalVariable(pdbLocal.Name ?? string.Empty, pdbLocalFlags, localVarSigToken, 1U, (uint)pdbLocal.Index, 0U, 0U, startOffset, endOffset);
				}
			}
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x001B9D14 File Offset: 0x001B9D14
		private static uint GetPdbLocalFlags(PdbLocalAttributes attributes)
		{
			if ((attributes & PdbLocalAttributes.DebuggerHidden) != PdbLocalAttributes.None)
			{
				return 1U;
			}
			return 0U;
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x001B9D24 File Offset: 0x001B9D24
		private MDToken GetUserEntryPointToken()
		{
			MethodDef userEntryPoint = this.pdbState.UserEntryPoint;
			if (userEntryPoint == null)
			{
				return default(MDToken);
			}
			uint rid = this.metadata.GetRid(userEntryPoint);
			if (rid == 0U)
			{
				this.Error("PDB user entry point method {0} ({1:X8}) is not defined in this module ({2})", new object[]
				{
					userEntryPoint,
					userEntryPoint.MDToken.Raw,
					this.module
				});
				return default(MDToken);
			}
			return new MDToken(Table.Method, rid);
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x001B9DAC File Offset: 0x001B9DAC
		public bool GetDebugInfo(ChecksumAlgorithm pdbChecksumAlgorithm, ref uint pdbAge, out Guid guid, out uint stamp, out IMAGE_DEBUG_DIRECTORY idd, out byte[] codeViewData)
		{
			return this.writer.GetDebugInfo(pdbChecksumAlgorithm, ref pdbAge, out guid, out stamp, out idd, out codeViewData);
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x001B9DC4 File Offset: 0x001B9DC4
		public void Close()
		{
			this.writer.Close();
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x001B9DD4 File Offset: 0x001B9DD4
		private ILogger GetLogger()
		{
			return this.Logger ?? DummyLogger.ThrowModuleWriterExceptionOnErrorInstance;
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x001B9DE8 File Offset: 0x001B9DE8
		private void Error(string message, params object[] args)
		{
			this.GetLogger().Log(this, LoggerEvent.Error, message, args);
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x001B9DFC File Offset: 0x001B9DFC
		public void Dispose()
		{
			if (this.writer != null)
			{
				this.Close();
			}
			SymbolWriter symbolWriter = this.writer;
			if (symbolWriter != null)
			{
				symbolWriter.Dispose();
			}
			this.writer = null;
		}

		// Token: 0x04002BCB RID: 11211
		private SymbolWriter writer;

		// Token: 0x04002BCC RID: 11212
		private readonly PdbState pdbState;

		// Token: 0x04002BCD RID: 11213
		private readonly ModuleDef module;

		// Token: 0x04002BCE RID: 11214
		private readonly dnlib.DotNet.Writer.Metadata metadata;

		// Token: 0x04002BCF RID: 11215
		private readonly Dictionary<PdbDocument, ISymbolDocumentWriter> pdbDocs = new Dictionary<PdbDocument, ISymbolDocumentWriter>();

		// Token: 0x04002BD0 RID: 11216
		private readonly WindowsPdbWriter.SequencePointHelper seqPointsHelper = new WindowsPdbWriter.SequencePointHelper();

		// Token: 0x04002BD1 RID: 11217
		private readonly Dictionary<Instruction, uint> instrToOffset;

		// Token: 0x04002BD2 RID: 11218
		private readonly PdbCustomDebugInfoWriterContext customDebugInfoWriterContext;

		// Token: 0x04002BD3 RID: 11219
		private readonly int localsEndScopeIncValue;

		// Token: 0x04002BD5 RID: 11221
		private static readonly object boxedZeroInt32 = 0;

		// Token: 0x02001033 RID: 4147
		private sealed class SequencePointHelper
		{
			// Token: 0x06008FB9 RID: 36793 RVA: 0x002AD12C File Offset: 0x002AD12C
			public void Write(WindowsPdbWriter pdbWriter, IList<Instruction> instrs)
			{
				this.checkedPdbDocs.Clear();
				for (;;)
				{
					PdbDocument pdbDocument = null;
					bool flag = false;
					int num = 0;
					int num2 = 0;
					int i = 0;
					while (i < instrs.Count)
					{
						Instruction instruction = instrs[i];
						SequencePoint sequencePoint = instruction.SequencePoint;
						if (sequencePoint != null && sequencePoint.Document != null && !this.checkedPdbDocs.ContainsKey(sequencePoint.Document))
						{
							if (pdbDocument == null)
							{
								pdbDocument = sequencePoint.Document;
							}
							else if (pdbDocument != sequencePoint.Document)
							{
								flag = true;
								goto IL_12C;
							}
							if (num >= this.instrOffsets.Length)
							{
								int num3 = num * 2;
								if (num3 < 64)
								{
									num3 = 64;
								}
								Array.Resize<int>(ref this.instrOffsets, num3);
								Array.Resize<int>(ref this.startLines, num3);
								Array.Resize<int>(ref this.startColumns, num3);
								Array.Resize<int>(ref this.endLines, num3);
								Array.Resize<int>(ref this.endColumns, num3);
							}
							this.instrOffsets[num] = num2;
							this.startLines[num] = sequencePoint.StartLine;
							this.startColumns[num] = sequencePoint.StartColumn;
							this.endLines[num] = sequencePoint.EndLine;
							this.endColumns[num] = sequencePoint.EndColumn;
							num++;
						}
						IL_12C:
						i++;
						num2 += instruction.GetSize();
					}
					if (num != 0)
					{
						pdbWriter.writer.DefineSequencePoints(pdbWriter.Add(pdbDocument), (uint)num, this.instrOffsets, this.startLines, this.startColumns, this.endLines, this.endColumns);
					}
					if (!flag)
					{
						break;
					}
					if (pdbDocument != null)
					{
						this.checkedPdbDocs.Add(pdbDocument, true);
					}
				}
			}

			// Token: 0x04004500 RID: 17664
			private readonly Dictionary<PdbDocument, bool> checkedPdbDocs = new Dictionary<PdbDocument, bool>();

			// Token: 0x04004501 RID: 17665
			private int[] instrOffsets = Array2.Empty<int>();

			// Token: 0x04004502 RID: 17666
			private int[] startLines;

			// Token: 0x04004503 RID: 17667
			private int[] startColumns;

			// Token: 0x04004504 RID: 17668
			private int[] endLines;

			// Token: 0x04004505 RID: 17669
			private int[] endColumns;
		}

		// Token: 0x02001034 RID: 4148
		private struct CurrentMethod
		{
			// Token: 0x06008FBB RID: 36795 RVA: 0x002AD2FC File Offset: 0x002AD2FC
			public CurrentMethod(WindowsPdbWriter pdbWriter, MethodDef method, Dictionary<Instruction, uint> toOffset)
			{
				this.pdbWriter = pdbWriter;
				this.Method = method;
				this.toOffset = toOffset;
				toOffset.Clear();
				uint num = 0U;
				IList<Instruction> instructions = method.Body.Instructions;
				int count = instructions.Count;
				for (int i = 0; i < count; i++)
				{
					Instruction instruction = instructions[i];
					toOffset[instruction] = num;
					num += (uint)instruction.GetSize();
				}
				this.BodySize = num;
			}

			// Token: 0x06008FBC RID: 36796 RVA: 0x002AD370 File Offset: 0x002AD370
			public readonly int GetOffset(Instruction instr)
			{
				if (instr == null)
				{
					return (int)this.BodySize;
				}
				uint result;
				if (this.toOffset.TryGetValue(instr, out result))
				{
					return (int)result;
				}
				this.pdbWriter.Error("Instruction was removed from the body but is referenced from PdbScope: {0}", new object[]
				{
					instr
				});
				return (int)this.BodySize;
			}

			// Token: 0x04004506 RID: 17670
			private readonly WindowsPdbWriter pdbWriter;

			// Token: 0x04004507 RID: 17671
			public readonly MethodDef Method;

			// Token: 0x04004508 RID: 17672
			private readonly Dictionary<Instruction, uint> toOffset;

			// Token: 0x04004509 RID: 17673
			public readonly uint BodySize;
		}
	}
}
