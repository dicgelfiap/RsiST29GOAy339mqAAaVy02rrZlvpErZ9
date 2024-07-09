using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;

namespace dnlib.DotNet.Pdb.WindowsPdb
{
	// Token: 0x0200092B RID: 2347
	internal struct PdbCustomDebugInfoWriter
	{
		// Token: 0x06005A6E RID: 23150 RVA: 0x001B83A4 File Offset: 0x001B83A4
		public static byte[] Write(Metadata metadata, MethodDef method, PdbCustomDebugInfoWriterContext context, IList<PdbCustomDebugInfo> customDebugInfos)
		{
			PdbCustomDebugInfoWriter pdbCustomDebugInfoWriter = new PdbCustomDebugInfoWriter(metadata, method, context);
			return pdbCustomDebugInfoWriter.Write(customDebugInfos);
		}

		// Token: 0x06005A6F RID: 23151 RVA: 0x001B83C8 File Offset: 0x001B83C8
		private PdbCustomDebugInfoWriter(Metadata metadata, MethodDef method, PdbCustomDebugInfoWriterContext context)
		{
			this.metadata = metadata;
			this.method = method;
			this.logger = context.Logger;
			this.memoryStream = context.MemoryStream;
			this.writer = context.Writer;
			this.instructionToOffsetDict = context.InstructionToOffsetDict;
			this.bodySize = 0U;
			this.instructionToOffsetDictInitd = false;
			this.memoryStream.SetLength(0L);
			this.memoryStream.Position = 0L;
		}

		// Token: 0x06005A70 RID: 23152 RVA: 0x001B8440 File Offset: 0x001B8440
		private void InitializeInstructionDictionary()
		{
			this.instructionToOffsetDict.Clear();
			CilBody body = this.method.Body;
			if (body == null)
			{
				return;
			}
			IList<Instruction> instructions = body.Instructions;
			uint num = 0U;
			for (int i = 0; i < instructions.Count; i++)
			{
				Instruction instruction = instructions[i];
				this.instructionToOffsetDict[instruction] = num;
				num += (uint)instruction.GetSize();
			}
			this.bodySize = num;
			this.instructionToOffsetDictInitd = true;
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x001B84BC File Offset: 0x001B84BC
		private uint GetInstructionOffset(Instruction instr, bool nullIsEndOfMethod)
		{
			if (!this.instructionToOffsetDictInitd)
			{
				this.InitializeInstructionDictionary();
			}
			if (instr == null)
			{
				if (nullIsEndOfMethod)
				{
					return this.bodySize;
				}
				this.Error("Instruction is null", new object[0]);
				return uint.MaxValue;
			}
			else
			{
				uint result;
				if (this.instructionToOffsetDict.TryGetValue(instr, out result))
				{
					return result;
				}
				this.Error("Instruction is missing in body but it's still being referenced by PDB data. Method {0} (0x{1:X8}), instruction: {2}", new object[]
				{
					this.method,
					this.method.MDToken.Raw,
					instr
				});
				return uint.MaxValue;
			}
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x001B8554 File Offset: 0x001B8554
		private void Error(string message, params object[] args)
		{
			this.logger.Log(this, LoggerEvent.Error, message, args);
		}

		// Token: 0x06005A73 RID: 23155 RVA: 0x001B8570 File Offset: 0x001B8570
		private byte[] Write(IList<PdbCustomDebugInfo> customDebugInfos)
		{
			if (customDebugInfos.Count == 0)
			{
				return null;
			}
			if (customDebugInfos.Count > 255)
			{
				this.Error("Too many custom debug infos. Count must be <= 255", new object[0]);
				return null;
			}
			this.writer.WriteByte(4);
			this.writer.WriteByte((byte)customDebugInfos.Count);
			this.writer.WriteUInt16(0);
			for (int i = 0; i < customDebugInfos.Count; i++)
			{
				PdbCustomDebugInfo pdbCustomDebugInfo = customDebugInfos[i];
				if (pdbCustomDebugInfo == null)
				{
					this.Error("Custom debug info is null", new object[0]);
					return null;
				}
				if (pdbCustomDebugInfo.Kind > (PdbCustomDebugInfoKind)255)
				{
					this.Error("Invalid custom debug info kind", new object[0]);
					return null;
				}
				long position = this.writer.Position;
				this.writer.WriteByte(4);
				this.writer.WriteByte((byte)pdbCustomDebugInfo.Kind);
				this.writer.WriteUInt16(0);
				this.writer.WriteUInt32(0U);
				switch (pdbCustomDebugInfo.Kind)
				{
				case PdbCustomDebugInfoKind.UsingGroups:
				{
					PdbUsingGroupsCustomDebugInfo pdbUsingGroupsCustomDebugInfo = pdbCustomDebugInfo as PdbUsingGroupsCustomDebugInfo;
					if (pdbUsingGroupsCustomDebugInfo == null)
					{
						this.Error("Unsupported custom debug info type {0}", new object[]
						{
							pdbCustomDebugInfo.GetType()
						});
						return null;
					}
					int count = pdbUsingGroupsCustomDebugInfo.UsingCounts.Count;
					if (count > 65535)
					{
						this.Error("UsingCounts contains more than 0xFFFF elements", new object[0]);
						return null;
					}
					this.writer.WriteUInt16((ushort)count);
					for (int j = 0; j < count; j++)
					{
						this.writer.WriteUInt16(pdbUsingGroupsCustomDebugInfo.UsingCounts[j]);
					}
					break;
				}
				case PdbCustomDebugInfoKind.ForwardMethodInfo:
				{
					PdbForwardMethodInfoCustomDebugInfo pdbForwardMethodInfoCustomDebugInfo = pdbCustomDebugInfo as PdbForwardMethodInfoCustomDebugInfo;
					if (pdbForwardMethodInfoCustomDebugInfo == null)
					{
						this.Error("Unsupported custom debug info type {0}", new object[]
						{
							pdbCustomDebugInfo.GetType()
						});
						return null;
					}
					uint methodToken = this.GetMethodToken(pdbForwardMethodInfoCustomDebugInfo.Method);
					if (methodToken == 0U)
					{
						return null;
					}
					this.writer.WriteUInt32(methodToken);
					break;
				}
				case PdbCustomDebugInfoKind.ForwardModuleInfo:
				{
					PdbForwardModuleInfoCustomDebugInfo pdbForwardModuleInfoCustomDebugInfo = pdbCustomDebugInfo as PdbForwardModuleInfoCustomDebugInfo;
					if (pdbForwardModuleInfoCustomDebugInfo == null)
					{
						this.Error("Unsupported custom debug info type {0}", new object[]
						{
							pdbCustomDebugInfo.GetType()
						});
						return null;
					}
					uint methodToken = this.GetMethodToken(pdbForwardModuleInfoCustomDebugInfo.Method);
					if (methodToken == 0U)
					{
						return null;
					}
					this.writer.WriteUInt32(methodToken);
					break;
				}
				case PdbCustomDebugInfoKind.StateMachineHoistedLocalScopes:
				{
					PdbStateMachineHoistedLocalScopesCustomDebugInfo pdbStateMachineHoistedLocalScopesCustomDebugInfo = pdbCustomDebugInfo as PdbStateMachineHoistedLocalScopesCustomDebugInfo;
					if (pdbStateMachineHoistedLocalScopesCustomDebugInfo == null)
					{
						this.Error("Unsupported custom debug info type {0}", new object[]
						{
							pdbCustomDebugInfo.GetType()
						});
						return null;
					}
					int count = pdbStateMachineHoistedLocalScopesCustomDebugInfo.Scopes.Count;
					this.writer.WriteInt32(count);
					for (int j = 0; j < count; j++)
					{
						StateMachineHoistedLocalScope stateMachineHoistedLocalScope = pdbStateMachineHoistedLocalScopesCustomDebugInfo.Scopes[j];
						if (stateMachineHoistedLocalScope.IsSynthesizedLocal)
						{
							this.writer.WriteInt32(0);
							this.writer.WriteInt32(0);
						}
						else
						{
							this.writer.WriteUInt32(this.GetInstructionOffset(stateMachineHoistedLocalScope.Start, false));
							this.writer.WriteUInt32(this.GetInstructionOffset(stateMachineHoistedLocalScope.End, true) - 1U);
						}
					}
					break;
				}
				case PdbCustomDebugInfoKind.StateMachineTypeName:
				{
					PdbStateMachineTypeNameCustomDebugInfo pdbStateMachineTypeNameCustomDebugInfo = pdbCustomDebugInfo as PdbStateMachineTypeNameCustomDebugInfo;
					if (pdbStateMachineTypeNameCustomDebugInfo == null)
					{
						this.Error("Unsupported custom debug info type {0}", new object[]
						{
							pdbCustomDebugInfo.GetType()
						});
						return null;
					}
					TypeDef type = pdbStateMachineTypeNameCustomDebugInfo.Type;
					if (type == null)
					{
						this.Error("State machine type is null", new object[0]);
						return null;
					}
					this.WriteUnicodeZ(this.MetadataNameToRoslynName(type.Name));
					break;
				}
				case PdbCustomDebugInfoKind.DynamicLocals:
				{
					PdbDynamicLocalsCustomDebugInfo pdbDynamicLocalsCustomDebugInfo = pdbCustomDebugInfo as PdbDynamicLocalsCustomDebugInfo;
					if (pdbDynamicLocalsCustomDebugInfo == null)
					{
						this.Error("Unsupported custom debug info type {0}", new object[]
						{
							pdbCustomDebugInfo.GetType()
						});
						return null;
					}
					int count = pdbDynamicLocalsCustomDebugInfo.Locals.Count;
					this.writer.WriteInt32(count);
					for (int j = 0; j < count; j++)
					{
						PdbDynamicLocal pdbDynamicLocal = pdbDynamicLocalsCustomDebugInfo.Locals[j];
						if (pdbDynamicLocal == null)
						{
							this.Error("Dynamic local is null", new object[0]);
							return null;
						}
						if (pdbDynamicLocal.Flags.Count > 64)
						{
							this.Error("Dynamic local flags is longer than 64 bytes", new object[0]);
							return null;
						}
						string text = pdbDynamicLocal.Name;
						if (text == null)
						{
							text = string.Empty;
						}
						if (text.Length > 64)
						{
							this.Error("Dynamic local name is longer than 64 chars", new object[0]);
							return null;
						}
						if (text.IndexOf('\0') >= 0)
						{
							this.Error("Dynamic local name contains a NUL char", new object[0]);
							return null;
						}
						int k;
						for (k = 0; k < pdbDynamicLocal.Flags.Count; k++)
						{
							this.writer.WriteByte(pdbDynamicLocal.Flags[k]);
						}
						while (k++ < 64)
						{
							this.writer.WriteByte(0);
						}
						this.writer.WriteInt32(pdbDynamicLocal.Flags.Count);
						if (pdbDynamicLocal.Local == null)
						{
							this.writer.WriteInt32(0);
						}
						else
						{
							this.writer.WriteInt32(pdbDynamicLocal.Local.Index);
						}
						for (k = 0; k < text.Length; k++)
						{
							this.writer.WriteUInt16((ushort)text[k]);
						}
						while (k++ < 64)
						{
							this.writer.WriteUInt16(0);
						}
					}
					break;
				}
				case PdbCustomDebugInfoKind.EditAndContinueLocalSlotMap:
				{
					PdbEditAndContinueLocalSlotMapCustomDebugInfo pdbEditAndContinueLocalSlotMapCustomDebugInfo = pdbCustomDebugInfo as PdbEditAndContinueLocalSlotMapCustomDebugInfo;
					if (pdbEditAndContinueLocalSlotMapCustomDebugInfo == null)
					{
						this.Error("Unsupported custom debug info type {0}", new object[]
						{
							pdbCustomDebugInfo.GetType()
						});
						return null;
					}
					this.writer.WriteBytes(pdbEditAndContinueLocalSlotMapCustomDebugInfo.Data);
					break;
				}
				case PdbCustomDebugInfoKind.EditAndContinueLambdaMap:
				{
					PdbEditAndContinueLambdaMapCustomDebugInfo pdbEditAndContinueLambdaMapCustomDebugInfo = pdbCustomDebugInfo as PdbEditAndContinueLambdaMapCustomDebugInfo;
					if (pdbEditAndContinueLambdaMapCustomDebugInfo == null)
					{
						this.Error("Unsupported custom debug info type {0}", new object[]
						{
							pdbCustomDebugInfo.GetType()
						});
						return null;
					}
					this.writer.WriteBytes(pdbEditAndContinueLambdaMapCustomDebugInfo.Data);
					break;
				}
				case PdbCustomDebugInfoKind.TupleElementNames:
				{
					PdbTupleElementNamesCustomDebugInfo pdbTupleElementNamesCustomDebugInfo = pdbCustomDebugInfo as PdbTupleElementNamesCustomDebugInfo;
					if (pdbTupleElementNamesCustomDebugInfo == null)
					{
						this.Error("Unsupported custom debug info type {0}", new object[]
						{
							pdbCustomDebugInfo.GetType()
						});
						return null;
					}
					int count = pdbTupleElementNamesCustomDebugInfo.Names.Count;
					this.writer.WriteInt32(count);
					for (int j = 0; j < count; j++)
					{
						PdbTupleElementNames pdbTupleElementNames = pdbTupleElementNamesCustomDebugInfo.Names[j];
						if (pdbTupleElementNames == null)
						{
							this.Error("Tuple name info is null", new object[0]);
							return null;
						}
						this.writer.WriteInt32(pdbTupleElementNames.TupleElementNames.Count);
						for (int k = 0; k < pdbTupleElementNames.TupleElementNames.Count; k++)
						{
							this.WriteUTF8Z(pdbTupleElementNames.TupleElementNames[k]);
						}
						if (pdbTupleElementNames.Local == null)
						{
							this.writer.WriteInt32(-1);
							this.writer.WriteUInt32(this.GetInstructionOffset(pdbTupleElementNames.ScopeStart, false));
							this.writer.WriteUInt32(this.GetInstructionOffset(pdbTupleElementNames.ScopeEnd, true));
						}
						else
						{
							this.writer.WriteInt32(pdbTupleElementNames.Local.Index);
							this.writer.WriteInt64(0L);
						}
						this.WriteUTF8Z(pdbTupleElementNames.Name);
					}
					break;
				}
				default:
				{
					PdbUnknownCustomDebugInfo pdbUnknownCustomDebugInfo = pdbCustomDebugInfo as PdbUnknownCustomDebugInfo;
					if (pdbUnknownCustomDebugInfo == null)
					{
						this.Error("Unsupported custom debug info class {0}", new object[]
						{
							pdbCustomDebugInfo.GetType()
						});
						return null;
					}
					this.writer.WriteBytes(pdbUnknownCustomDebugInfo.Data);
					break;
				}
				}
				long position2 = this.writer.Position;
				long num = position2 - position;
				long num2 = num + 3L & -4L;
				if (num2 > (long)((ulong)-1))
				{
					this.Error("Custom debug info record is too big", new object[0]);
					return null;
				}
				this.writer.Position = position + 3L;
				if (pdbCustomDebugInfo.Kind <= PdbCustomDebugInfoKind.DynamicLocals)
				{
					this.writer.WriteByte(0);
				}
				else
				{
					this.writer.WriteByte((byte)(num2 - num));
				}
				this.writer.WriteUInt32((uint)num2);
				this.writer.Position = position2;
				while (this.writer.Position < position + num2)
				{
					this.writer.WriteByte(0);
				}
			}
			return this.memoryStream.ToArray();
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x001B8DF0 File Offset: 0x001B8DF0
		private string MetadataNameToRoslynName(string name)
		{
			if (name == null)
			{
				return name;
			}
			int num = name.LastIndexOf('`');
			if (num < 0)
			{
				return name;
			}
			return name.Substring(0, num);
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x001B8E24 File Offset: 0x001B8E24
		private void WriteUnicodeZ(string s)
		{
			if (s == null)
			{
				this.Error("String is null", new object[0]);
				return;
			}
			if (s.IndexOf('\0') >= 0)
			{
				this.Error("String contains a NUL char: {0}", new object[]
				{
					s
				});
				return;
			}
			for (int i = 0; i < s.Length; i++)
			{
				this.writer.WriteUInt16((ushort)s[i]);
			}
			this.writer.WriteUInt16(0);
		}

		// Token: 0x06005A76 RID: 23158 RVA: 0x001B8EA4 File Offset: 0x001B8EA4
		private void WriteUTF8Z(string s)
		{
			if (s == null)
			{
				this.Error("String is null", new object[0]);
				return;
			}
			if (s.IndexOf('\0') >= 0)
			{
				this.Error("String contains a NUL char: {0}", new object[]
				{
					s
				});
				return;
			}
			this.writer.WriteBytes(Encoding.UTF8.GetBytes(s));
			this.writer.WriteByte(0);
		}

		// Token: 0x06005A77 RID: 23159 RVA: 0x001B8F14 File Offset: 0x001B8F14
		private uint GetMethodToken(IMethodDefOrRef method)
		{
			if (method == null)
			{
				this.Error("Method is null", new object[0]);
				return 0U;
			}
			MethodDef methodDef = method as MethodDef;
			if (methodDef != null)
			{
				uint rid = this.metadata.GetRid(methodDef);
				if (rid == 0U)
				{
					this.Error("Method {0} ({1:X8}) is not defined in this module ({2})", new object[]
					{
						method,
						method.MDToken.Raw,
						this.metadata.Module
					});
					return 0U;
				}
				return new MDToken(methodDef.MDToken.Table, rid).Raw;
			}
			else
			{
				MemberRef memberRef = method as MemberRef;
				if (memberRef != null && memberRef.IsMethodRef)
				{
					return this.metadata.GetToken(memberRef).Raw;
				}
				this.Error("Not a method", new object[0]);
				return 0U;
			}
		}

		// Token: 0x04002BC3 RID: 11203
		private readonly Metadata metadata;

		// Token: 0x04002BC4 RID: 11204
		private readonly MethodDef method;

		// Token: 0x04002BC5 RID: 11205
		private readonly ILogger logger;

		// Token: 0x04002BC6 RID: 11206
		private readonly MemoryStream memoryStream;

		// Token: 0x04002BC7 RID: 11207
		private readonly DataWriter writer;

		// Token: 0x04002BC8 RID: 11208
		private readonly Dictionary<Instruction, uint> instructionToOffsetDict;

		// Token: 0x04002BC9 RID: 11209
		private uint bodySize;

		// Token: 0x04002BCA RID: 11210
		private bool instructionToOffsetDictInitd;
	}
}
