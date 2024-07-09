using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.Threading;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000922 RID: 2338
	[ComVisible(true)]
	public sealed class PdbState
	{
		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06005A34 RID: 23092 RVA: 0x001B69D8 File Offset: 0x001B69D8
		// (set) Token: 0x06005A35 RID: 23093 RVA: 0x001B69E0 File Offset: 0x001B69E0
		public PdbFileKind PdbFileKind { get; set; }

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06005A36 RID: 23094 RVA: 0x001B69EC File Offset: 0x001B69EC
		// (set) Token: 0x06005A37 RID: 23095 RVA: 0x001B69F4 File Offset: 0x001B69F4
		public MethodDef UserEntryPoint
		{
			get
			{
				return this.userEntryPoint;
			}
			set
			{
				this.userEntryPoint = value;
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06005A38 RID: 23096 RVA: 0x001B6A00 File Offset: 0x001B6A00
		public IEnumerable<PdbDocument> Documents
		{
			get
			{
				this.theLock.EnterWriteLock();
				IEnumerable<PdbDocument> result;
				try
				{
					result = new List<PdbDocument>(this.docDict.Values);
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
				return result;
			}
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06005A39 RID: 23097 RVA: 0x001B6A4C File Offset: 0x001B6A4C
		public bool HasDocuments
		{
			get
			{
				this.theLock.EnterWriteLock();
				bool result;
				try
				{
					result = (this.docDict.Count > 0);
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
				return result;
			}
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x001B6A98 File Offset: 0x001B6A98
		public PdbState(ModuleDef module, PdbFileKind pdbFileKind)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			this.compiler = PdbState.CalculateCompiler(module);
			this.PdbFileKind = pdbFileKind;
			this.originalPdbFileKind = pdbFileKind;
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x001B6AF0 File Offset: 0x001B6AF0
		public PdbState(SymbolReader reader, ModuleDefMD module)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.reader = reader;
			reader.Initialize(module);
			this.PdbFileKind = reader.PdbFileKind;
			this.originalPdbFileKind = reader.PdbFileKind;
			this.compiler = PdbState.CalculateCompiler(module);
			this.userEntryPoint = (module.ResolveToken(reader.UserEntryPoint) as MethodDef);
			IList<SymbolDocument> documents = reader.Documents;
			int count = documents.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add_NoLock(documents[i]);
			}
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x001B6BB4 File Offset: 0x001B6BB4
		public PdbDocument Add(PdbDocument doc)
		{
			this.theLock.EnterWriteLock();
			PdbDocument result;
			try
			{
				result = this.Add_NoLock(doc);
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x001B6BF8 File Offset: 0x001B6BF8
		private PdbDocument Add_NoLock(PdbDocument doc)
		{
			PdbDocument result;
			if (this.docDict.TryGetValue(doc, out result))
			{
				return result;
			}
			this.docDict.Add(doc, doc);
			return doc;
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x001B6C2C File Offset: 0x001B6C2C
		private PdbDocument Add_NoLock(SymbolDocument symDoc)
		{
			PdbDocument pdbDocument = PdbDocument.CreatePartialForCompare(symDoc);
			PdbDocument result;
			if (this.docDict.TryGetValue(pdbDocument, out result))
			{
				return result;
			}
			pdbDocument.Initialize(symDoc);
			this.docDict.Add(pdbDocument, pdbDocument);
			return pdbDocument;
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x001B6C70 File Offset: 0x001B6C70
		public bool Remove(PdbDocument doc)
		{
			this.theLock.EnterWriteLock();
			bool result;
			try
			{
				result = this.docDict.Remove(doc);
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x001B6CB8 File Offset: 0x001B6CB8
		public PdbDocument GetExisting(PdbDocument doc)
		{
			this.theLock.EnterWriteLock();
			PdbDocument result;
			try
			{
				PdbDocument pdbDocument;
				this.docDict.TryGetValue(doc, out pdbDocument);
				result = pdbDocument;
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x001B6D04 File Offset: 0x001B6D04
		public void RemoveAllDocuments()
		{
			this.RemoveAllDocuments(false);
		}

		// Token: 0x06005A42 RID: 23106 RVA: 0x001B6D10 File Offset: 0x001B6D10
		public List<PdbDocument> RemoveAllDocuments(bool returnDocs)
		{
			this.theLock.EnterWriteLock();
			List<PdbDocument> result;
			try
			{
				List<PdbDocument> list = returnDocs ? new List<PdbDocument>(this.docDict.Values) : null;
				this.docDict.Clear();
				result = list;
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06005A43 RID: 23107 RVA: 0x001B6D74 File Offset: 0x001B6D74
		internal Compiler Compiler
		{
			get
			{
				return this.compiler;
			}
		}

		// Token: 0x06005A44 RID: 23108 RVA: 0x001B6D7C File Offset: 0x001B6D7C
		internal void InitializeMethodBody(ModuleDefMD module, MethodDef ownerMethod, CilBody body)
		{
			if (this.reader == null)
			{
				return;
			}
			SymbolMethod method = this.reader.GetMethod(ownerMethod, 1);
			if (method != null)
			{
				PdbMethod pdbMethod = new PdbMethod();
				pdbMethod.Scope = this.CreateScope(module, GenericParamContext.Create(ownerMethod), body, method.RootScope);
				this.AddSequencePoints(body, method);
				body.PdbMethod = pdbMethod;
			}
		}

		// Token: 0x06005A45 RID: 23109 RVA: 0x001B6DDC File Offset: 0x001B6DDC
		internal void InitializeCustomDebugInfos(MethodDef ownerMethod, CilBody body, IList<PdbCustomDebugInfo> customDebugInfos)
		{
			if (this.reader == null)
			{
				return;
			}
			SymbolMethod method = this.reader.GetMethod(ownerMethod, 1);
			if (method != null)
			{
				method.GetCustomDebugInfos(ownerMethod, body, customDebugInfos);
			}
		}

		// Token: 0x06005A46 RID: 23110 RVA: 0x001B6E18 File Offset: 0x001B6E18
		private static Compiler CalculateCompiler(ModuleDef module)
		{
			if (module == null)
			{
				return Compiler.Other;
			}
			foreach (AssemblyRef assemblyRef in module.GetAssemblyRefs())
			{
				if (assemblyRef.Name == PdbState.nameAssemblyVisualBasic || assemblyRef.Name == PdbState.nameAssemblyVisualBasicCore)
				{
					return Compiler.VisualBasic;
				}
			}
			return Compiler.Other;
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x001B6EA4 File Offset: 0x001B6EA4
		private void AddSequencePoints(CilBody body, SymbolMethod method)
		{
			int num = 0;
			IList<SymbolSequencePoint> sequencePoints = method.SequencePoints;
			int count = sequencePoints.Count;
			for (int i = 0; i < count; i++)
			{
				SymbolSequencePoint symbolSequencePoint = sequencePoints[i];
				Instruction instruction = PdbState.GetInstruction(body.Instructions, symbolSequencePoint.Offset, ref num);
				if (instruction != null)
				{
					SequencePoint sequencePoint = new SequencePoint
					{
						Document = this.Add_NoLock(symbolSequencePoint.Document),
						StartLine = symbolSequencePoint.Line,
						StartColumn = symbolSequencePoint.Column,
						EndLine = symbolSequencePoint.EndLine,
						EndColumn = symbolSequencePoint.EndColumn
					};
					instruction.SequencePoint = sequencePoint;
				}
			}
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x001B6F54 File Offset: 0x001B6F54
		private PdbScope CreateScope(ModuleDefMD module, GenericParamContext gpContext, CilBody body, SymbolScope symScope)
		{
			if (symScope == null)
			{
				return null;
			}
			Stack<PdbState.CreateScopeState> stack = new Stack<PdbState.CreateScopeState>();
			PdbState.CreateScopeState createScopeState = new PdbState.CreateScopeState
			{
				SymScope = symScope
			};
			int num = PdbUtils.IsEndInclusive(this.originalPdbFileKind, this.Compiler) ? 1 : 0;
			for (;;)
			{
				int num2 = 0;
				createScopeState.PdbScope = new PdbScope
				{
					Start = PdbState.GetInstruction(body.Instructions, createScopeState.SymScope.StartOffset, ref num2),
					End = PdbState.GetInstruction(body.Instructions, createScopeState.SymScope.EndOffset + num, ref num2)
				};
				IList<PdbCustomDebugInfo> customDebugInfos = createScopeState.SymScope.CustomDebugInfos;
				int count = customDebugInfos.Count;
				for (int i = 0; i < count; i++)
				{
					createScopeState.PdbScope.CustomDebugInfos.Add(customDebugInfos[i]);
				}
				IList<SymbolVariable> locals = createScopeState.SymScope.Locals;
				count = locals.Count;
				for (int j = 0; j < count; j++)
				{
					SymbolVariable symbolVariable = locals[j];
					int index = symbolVariable.Index;
					if (index < body.Variables.Count)
					{
						Local local = body.Variables[index];
						string name = symbolVariable.Name;
						local.SetName(name);
						PdbLocalAttributes attributes = symbolVariable.Attributes;
						local.SetAttributes(attributes);
						PdbLocal pdbLocal = new PdbLocal(local, name, attributes);
						customDebugInfos = symbolVariable.CustomDebugInfos;
						int count2 = customDebugInfos.Count;
						for (int k = 0; k < count2; k++)
						{
							pdbLocal.CustomDebugInfos.Add(customDebugInfos[k]);
						}
						createScopeState.PdbScope.Variables.Add(pdbLocal);
					}
				}
				IList<SymbolNamespace> namespaces = createScopeState.SymScope.Namespaces;
				count = namespaces.Count;
				for (int l = 0; l < count; l++)
				{
					createScopeState.PdbScope.Namespaces.Add(namespaces[l].Name);
				}
				createScopeState.PdbScope.ImportScope = createScopeState.SymScope.ImportScope;
				IList<PdbConstant> constants = createScopeState.SymScope.GetConstants(module, gpContext);
				for (int m = 0; m < constants.Count; m++)
				{
					PdbConstant pdbConstant = constants[m];
					TypeSig typeSig = pdbConstant.Type.RemovePinnedAndModifiers();
					if (typeSig != null)
					{
						switch (typeSig.ElementType)
						{
						case ElementType.Void:
						case ElementType.I2:
						case ElementType.U2:
						case ElementType.I4:
						case ElementType.U4:
						case ElementType.I8:
						case ElementType.U8:
						case ElementType.R4:
						case ElementType.R8:
						case ElementType.Ptr:
						case ElementType.ByRef:
						case ElementType.ValueType:
						case ElementType.TypedByRef:
						case ElementType.I:
						case ElementType.U:
						case ElementType.FnPtr:
							goto IL_453;
						case ElementType.Boolean:
							if (pdbConstant.Value is short)
							{
								pdbConstant.Value = ((short)pdbConstant.Value != 0);
								goto IL_453;
							}
							goto IL_453;
						case ElementType.Char:
							if (pdbConstant.Value is ushort)
							{
								pdbConstant.Value = (char)((ushort)pdbConstant.Value);
								goto IL_453;
							}
							goto IL_453;
						case ElementType.I1:
							if (pdbConstant.Value is short)
							{
								pdbConstant.Value = (sbyte)((short)pdbConstant.Value);
								goto IL_453;
							}
							goto IL_453;
						case ElementType.U1:
							if (pdbConstant.Value is short)
							{
								pdbConstant.Value = (byte)((short)pdbConstant.Value);
								goto IL_453;
							}
							goto IL_453;
						case ElementType.String:
							if (this.PdbFileKind != PdbFileKind.WindowsPDB)
							{
								goto IL_453;
							}
							if (pdbConstant.Value is int && (int)pdbConstant.Value == 0)
							{
								pdbConstant.Value = null;
								goto IL_453;
							}
							if (pdbConstant.Value == null)
							{
								pdbConstant.Value = string.Empty;
								goto IL_453;
							}
							goto IL_453;
						case ElementType.Var:
						case ElementType.MVar:
						{
							GenericParam genericParam = ((GenericSig)typeSig).GenericParam;
							if (genericParam == null || genericParam.HasNotNullableValueTypeConstraint || !genericParam.HasReferenceTypeConstraint)
							{
								goto IL_453;
							}
							break;
						}
						case ElementType.GenericInst:
							if (((GenericInstSig)typeSig).GenericType is ValueTypeSig)
							{
								goto IL_453;
							}
							break;
						}
						if (pdbConstant.Value is int && (int)pdbConstant.Value == 0)
						{
							pdbConstant.Value = null;
						}
					}
					IL_453:
					createScopeState.PdbScope.Constants.Add(pdbConstant);
				}
				createScopeState.ChildrenIndex = 0;
				createScopeState.Children = createScopeState.SymScope.Children;
				while (createScopeState.ChildrenIndex >= createScopeState.Children.Count)
				{
					if (stack.Count == 0)
					{
						goto Block_25;
					}
					PdbScope pdbScope = createScopeState.PdbScope;
					createScopeState = stack.Pop();
					createScopeState.PdbScope.Scopes.Add(pdbScope);
					createScopeState.ChildrenIndex++;
				}
				SymbolScope symScope2 = createScopeState.Children[createScopeState.ChildrenIndex];
				stack.Push(createScopeState);
				createScopeState = new PdbState.CreateScopeState
				{
					SymScope = symScope2
				};
			}
			Block_25:
			return createScopeState.PdbScope;
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x001B7484 File Offset: 0x001B7484
		private static Instruction GetInstruction(IList<Instruction> instrs, int offset, ref int index)
		{
			if (instrs.Count > 0 && (long)offset > (long)((ulong)instrs[instrs.Count - 1].Offset))
			{
				return null;
			}
			int i = index;
			while (i < instrs.Count)
			{
				Instruction instruction = instrs[i];
				if ((ulong)instruction.Offset >= (ulong)((long)offset))
				{
					if ((ulong)instruction.Offset == (ulong)((long)offset))
					{
						index = i;
						return instruction;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			int j = 0;
			while (j < index)
			{
				Instruction instruction2 = instrs[j];
				if ((ulong)instruction2.Offset >= (ulong)((long)offset))
				{
					if ((ulong)instruction2.Offset == (ulong)((long)offset))
					{
						index = j;
						return instruction2;
					}
					break;
				}
				else
				{
					j++;
				}
			}
			return null;
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x001B7538 File Offset: 0x001B7538
		internal void InitializeCustomDebugInfos(MDToken token, GenericParamContext gpContext, IList<PdbCustomDebugInfo> result)
		{
			SymbolReader symbolReader = this.reader;
			if (symbolReader == null)
			{
				return;
			}
			symbolReader.GetCustomDebugInfos(token.ToInt32(), gpContext, result);
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x001B7558 File Offset: 0x001B7558
		internal void Dispose()
		{
			SymbolReader symbolReader = this.reader;
			if (symbolReader == null)
			{
				return;
			}
			symbolReader.Dispose();
		}

		// Token: 0x04002BA4 RID: 11172
		private readonly SymbolReader reader;

		// Token: 0x04002BA5 RID: 11173
		private readonly Dictionary<PdbDocument, PdbDocument> docDict = new Dictionary<PdbDocument, PdbDocument>();

		// Token: 0x04002BA6 RID: 11174
		private MethodDef userEntryPoint;

		// Token: 0x04002BA7 RID: 11175
		private readonly Compiler compiler;

		// Token: 0x04002BA8 RID: 11176
		private readonly PdbFileKind originalPdbFileKind;

		// Token: 0x04002BA9 RID: 11177
		private readonly Lock theLock = Lock.Create();

		// Token: 0x04002BAB RID: 11179
		private static readonly UTF8String nameAssemblyVisualBasic = new UTF8String("Microsoft.VisualBasic");

		// Token: 0x04002BAC RID: 11180
		private static readonly UTF8String nameAssemblyVisualBasicCore = new UTF8String("Microsoft.VisualBasic.Core");

		// Token: 0x02001032 RID: 4146
		private struct CreateScopeState
		{
			// Token: 0x040044FC RID: 17660
			public SymbolScope SymScope;

			// Token: 0x040044FD RID: 17661
			public PdbScope PdbScope;

			// Token: 0x040044FE RID: 17662
			public IList<SymbolScope> Children;

			// Token: 0x040044FF RID: 17663
			public int ChildrenIndex;
		}
	}
}
