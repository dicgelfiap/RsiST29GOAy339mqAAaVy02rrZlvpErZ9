using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using dnlib.DotNet.Emit;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.WindowsPdb
{
	// Token: 0x02000929 RID: 2345
	internal struct PdbCustomDebugInfoReader
	{
		// Token: 0x06005A65 RID: 23141 RVA: 0x001B7A04 File Offset: 0x001B7A04
		public static void Read(MethodDef method, CilBody body, IList<PdbCustomDebugInfo> result, byte[] data)
		{
			try
			{
				DataReader dataReader = ByteArrayDataReaderFactory.CreateReader(data);
				new PdbCustomDebugInfoReader(method, body, ref dataReader).Read(result);
			}
			catch (ArgumentException)
			{
			}
			catch (OutOfMemoryException)
			{
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x001B7A6C File Offset: 0x001B7A6C
		private PdbCustomDebugInfoReader(MethodDef method, CilBody body, ref DataReader reader)
		{
			this.module = method.Module;
			this.typeOpt = method.DeclaringType;
			this.bodyOpt = body;
			this.gpContext = GenericParamContext.Create(method);
			this.reader = reader;
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x001B7AA8 File Offset: 0x001B7AA8
		private void Read(IList<PdbCustomDebugInfo> result)
		{
			if (this.reader.Length < 4U)
			{
				return;
			}
			if (this.reader.ReadByte() != 4)
			{
				return;
			}
			this.reader.ReadByte();
			this.reader.Position = this.reader.Position + 2U;
			while (this.reader.CanRead(8U))
			{
				int num = (int)this.reader.ReadByte();
				PdbCustomDebugInfoKind pdbCustomDebugInfoKind = (PdbCustomDebugInfoKind)this.reader.ReadByte();
				uint position = this.reader.Position;
				this.reader.Position = position + 1U;
				int num2 = (int)this.reader.ReadByte();
				int num3 = this.reader.ReadInt32();
				if (num3 < 8 || (ulong)this.reader.Position - 8UL + (ulong)num3 > (ulong)this.reader.Length)
				{
					return;
				}
				if (pdbCustomDebugInfoKind <= PdbCustomDebugInfoKind.DynamicLocals)
				{
					num2 = 0;
				}
				if (num2 > 3)
				{
					return;
				}
				uint position2 = this.reader.Position - 8U + (uint)num3;
				if (num == 4)
				{
					ulong num4 = (ulong)this.reader.Position - 8UL + (ulong)num3 - (ulong)num2;
					PdbCustomDebugInfo pdbCustomDebugInfo = this.ReadRecord(pdbCustomDebugInfoKind, num4);
					if ((ulong)this.reader.Position > num4)
					{
						return;
					}
					if (pdbCustomDebugInfo != null)
					{
						result.Add(pdbCustomDebugInfo);
					}
				}
				this.reader.Position = position2;
			}
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x001B7C04 File Offset: 0x001B7C04
		private PdbCustomDebugInfo ReadRecord(PdbCustomDebugInfoKind recKind, ulong recPosEnd)
		{
			switch (recKind)
			{
			case PdbCustomDebugInfoKind.UsingGroups:
			{
				int num = (int)this.reader.ReadUInt16();
				if (num < 0)
				{
					return null;
				}
				PdbUsingGroupsCustomDebugInfo pdbUsingGroupsCustomDebugInfo = new PdbUsingGroupsCustomDebugInfo(num);
				for (int i = 0; i < num; i++)
				{
					pdbUsingGroupsCustomDebugInfo.UsingCounts.Add(this.reader.ReadUInt16());
				}
				return pdbUsingGroupsCustomDebugInfo;
			}
			case PdbCustomDebugInfoKind.ForwardMethodInfo:
			{
				IMethodDefOrRef methodDefOrRef = this.module.ResolveToken(this.reader.ReadUInt32(), this.gpContext) as IMethodDefOrRef;
				if (methodDefOrRef == null)
				{
					return null;
				}
				return new PdbForwardMethodInfoCustomDebugInfo(methodDefOrRef);
			}
			case PdbCustomDebugInfoKind.ForwardModuleInfo:
			{
				IMethodDefOrRef methodDefOrRef = this.module.ResolveToken(this.reader.ReadUInt32(), this.gpContext) as IMethodDefOrRef;
				if (methodDefOrRef == null)
				{
					return null;
				}
				return new PdbForwardModuleInfoCustomDebugInfo(methodDefOrRef);
			}
			case PdbCustomDebugInfoKind.StateMachineHoistedLocalScopes:
			{
				if (this.bodyOpt == null)
				{
					return null;
				}
				int num = this.reader.ReadInt32();
				if (num < 0)
				{
					return null;
				}
				PdbStateMachineHoistedLocalScopesCustomDebugInfo pdbStateMachineHoistedLocalScopesCustomDebugInfo = new PdbStateMachineHoistedLocalScopesCustomDebugInfo(num);
				for (int j = 0; j < num; j++)
				{
					uint num2 = this.reader.ReadUInt32();
					uint num3 = this.reader.ReadUInt32();
					if (num2 > num3)
					{
						return null;
					}
					if (num3 == 0U)
					{
						pdbStateMachineHoistedLocalScopesCustomDebugInfo.Scopes.Add(default(StateMachineHoistedLocalScope));
					}
					else
					{
						Instruction instruction = this.GetInstruction(num2);
						Instruction instruction2 = this.GetInstruction(num3 + 1U);
						if (instruction == null)
						{
							return null;
						}
						pdbStateMachineHoistedLocalScopesCustomDebugInfo.Scopes.Add(new StateMachineHoistedLocalScope(instruction, instruction2));
					}
				}
				return pdbStateMachineHoistedLocalScopesCustomDebugInfo;
			}
			case PdbCustomDebugInfoKind.StateMachineTypeName:
			{
				string text = this.ReadUnicodeZ(recPosEnd, true);
				if (text == null)
				{
					return null;
				}
				TypeDef nestedType = this.GetNestedType(text);
				if (nestedType == null)
				{
					return null;
				}
				return new PdbStateMachineTypeNameCustomDebugInfo(nestedType);
			}
			case PdbCustomDebugInfoKind.DynamicLocals:
			{
				if (this.bodyOpt == null)
				{
					return null;
				}
				int num = this.reader.ReadInt32();
				if ((ulong)this.reader.Position + (ulong)num * 200UL > recPosEnd)
				{
					return null;
				}
				PdbDynamicLocalsCustomDebugInfo pdbDynamicLocalsCustomDebugInfo = new PdbDynamicLocalsCustomDebugInfo(num);
				for (int k = 0; k < num; k++)
				{
					this.reader.Position = this.reader.Position + 64U;
					int num4 = this.reader.ReadInt32();
					if (num4 > 64)
					{
						return null;
					}
					PdbDynamicLocal pdbDynamicLocal = new PdbDynamicLocal(num4);
					uint position = this.reader.Position;
					this.reader.Position = this.reader.Position - 68U;
					for (int l = 0; l < num4; l++)
					{
						pdbDynamicLocal.Flags.Add(this.reader.ReadByte());
					}
					this.reader.Position = position;
					int num5 = this.reader.ReadInt32();
					if (num5 != 0 && num5 >= this.bodyOpt.Variables.Count)
					{
						return null;
					}
					uint num6 = this.reader.Position + 128U;
					string text = this.ReadUnicodeZ((ulong)num6, false);
					this.reader.Position = num6;
					Local local = (num5 < this.bodyOpt.Variables.Count) ? this.bodyOpt.Variables[num5] : null;
					if (num5 == 0 && local != null && local.Name != text)
					{
						local = null;
					}
					if (local != null && local.Name == text)
					{
						text = null;
					}
					pdbDynamicLocal.Name = text;
					pdbDynamicLocal.Local = local;
					pdbDynamicLocalsCustomDebugInfo.Locals.Add(pdbDynamicLocal);
				}
				return pdbDynamicLocalsCustomDebugInfo;
			}
			case PdbCustomDebugInfoKind.EditAndContinueLocalSlotMap:
			{
				byte[] data = this.reader.ReadBytes((int)(recPosEnd - (ulong)this.reader.Position));
				return new PdbEditAndContinueLocalSlotMapCustomDebugInfo(data);
			}
			case PdbCustomDebugInfoKind.EditAndContinueLambdaMap:
			{
				byte[] data = this.reader.ReadBytes((int)(recPosEnd - (ulong)this.reader.Position));
				return new PdbEditAndContinueLambdaMapCustomDebugInfo(data);
			}
			case PdbCustomDebugInfoKind.TupleElementNames:
			{
				if (this.bodyOpt == null)
				{
					return null;
				}
				int num = this.reader.ReadInt32();
				if (num < 0)
				{
					return null;
				}
				PdbTupleElementNamesCustomDebugInfo pdbTupleElementNamesCustomDebugInfo = new PdbTupleElementNamesCustomDebugInfo(num);
				for (int m = 0; m < num; m++)
				{
					int num7 = this.reader.ReadInt32();
					if (num7 >= 10000)
					{
						return null;
					}
					PdbTupleElementNames pdbTupleElementNames = new PdbTupleElementNames(num7);
					for (int n = 0; n < num7; n++)
					{
						string text2 = this.ReadUTF8Z(recPosEnd);
						if (text2 == null)
						{
							return null;
						}
						pdbTupleElementNames.TupleElementNames.Add(text2);
					}
					int num5 = this.reader.ReadInt32();
					uint offset = this.reader.ReadUInt32();
					uint offset2 = this.reader.ReadUInt32();
					string text = this.ReadUTF8Z(recPosEnd);
					if (text == null)
					{
						return null;
					}
					Local local;
					if (num5 == -1)
					{
						local = null;
						pdbTupleElementNames.ScopeStart = this.GetInstruction(offset);
						pdbTupleElementNames.ScopeEnd = this.GetInstruction(offset2);
						if (pdbTupleElementNames.ScopeStart == null)
						{
							return null;
						}
					}
					else
					{
						if (num5 >= this.bodyOpt.Variables.Count)
						{
							return null;
						}
						local = this.bodyOpt.Variables[num5];
					}
					if (local != null && local.Name == text)
					{
						text = null;
					}
					pdbTupleElementNames.Local = local;
					pdbTupleElementNames.Name = text;
					pdbTupleElementNamesCustomDebugInfo.Names.Add(pdbTupleElementNames);
				}
				return pdbTupleElementNamesCustomDebugInfo;
			}
			default:
			{
				byte[] data = this.reader.ReadBytes((int)(recPosEnd - (ulong)this.reader.Position));
				return new PdbUnknownCustomDebugInfo(recKind, data);
			}
			}
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x001B8188 File Offset: 0x001B8188
		private TypeDef GetNestedType(string name)
		{
			if (this.typeOpt == null)
			{
				return null;
			}
			IList<TypeDef> nestedTypes = this.typeOpt.NestedTypes;
			int count = nestedTypes.Count;
			for (int i = 0; i < count; i++)
			{
				TypeDef typeDef = nestedTypes[i];
				if (UTF8String.IsNullOrEmpty(typeDef.Namespace))
				{
					if (typeDef.Name == name)
					{
						return typeDef;
					}
					string @string = typeDef.Name.String;
					if (@string.StartsWith(name) && @string.Length >= name.Length + 2)
					{
						int j = name.Length;
						if (@string[j] == '`')
						{
							bool flag = true;
							for (j++; j < @string.Length; j++)
							{
								if (!char.IsDigit(@string[j]))
								{
									flag = false;
									break;
								}
							}
							if (flag)
							{
								return typeDef;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x001B8280 File Offset: 0x001B8280
		private string ReadUnicodeZ(ulong recPosEnd, bool needZeroChar)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while ((ulong)this.reader.Position < recPosEnd)
			{
				char c = this.reader.ReadChar();
				if (c == '\0')
				{
					return stringBuilder.ToString();
				}
				stringBuilder.Append(c);
			}
			if (!needZeroChar)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x001B82DC File Offset: 0x001B82DC
		private string ReadUTF8Z(ulong recPosEnd)
		{
			if ((ulong)this.reader.Position > recPosEnd)
			{
				return null;
			}
			return this.reader.TryReadZeroTerminatedUtf8String();
		}

		// Token: 0x06005A6C RID: 23148 RVA: 0x001B8300 File Offset: 0x001B8300
		private Instruction GetInstruction(uint offset)
		{
			IList<Instruction> instructions = this.bodyOpt.Instructions;
			int num = 0;
			int num2 = instructions.Count - 1;
			while (num <= num2 && num2 != -1)
			{
				int num3 = (num + num2) / 2;
				Instruction instruction = instructions[num3];
				if (instruction.Offset == offset)
				{
					return instruction;
				}
				if (offset < instruction.Offset)
				{
					num2 = num3 - 1;
				}
				else
				{
					num = num3 + 1;
				}
			}
			return null;
		}

		// Token: 0x04002BBA RID: 11194
		private readonly ModuleDef module;

		// Token: 0x04002BBB RID: 11195
		private readonly TypeDef typeOpt;

		// Token: 0x04002BBC RID: 11196
		private readonly CilBody bodyOpt;

		// Token: 0x04002BBD RID: 11197
		private readonly GenericParamContext gpContext;

		// Token: 0x04002BBE RID: 11198
		private DataReader reader;
	}
}
