using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009EC RID: 2540
	[ComVisible(true)]
	public static class MethodUtils
	{
		// Token: 0x06006196 RID: 24982 RVA: 0x001D07B0 File Offset: 0x001D07B0
		public static void SimplifyMacros(this IList<Instruction> instructions, IList<Local> locals, IList<Parameter> parameters)
		{
			int count = instructions.Count;
			for (int i = 0; i < count; i++)
			{
				Instruction instruction = instructions[i];
				Code code = instruction.OpCode.Code;
				switch (code)
				{
				case Code.Ldarg_0:
					instruction.OpCode = OpCodes.Ldarg;
					instruction.Operand = MethodUtils.ReadList<Parameter>(parameters, 0);
					break;
				case Code.Ldarg_1:
					instruction.OpCode = OpCodes.Ldarg;
					instruction.Operand = MethodUtils.ReadList<Parameter>(parameters, 1);
					break;
				case Code.Ldarg_2:
					instruction.OpCode = OpCodes.Ldarg;
					instruction.Operand = MethodUtils.ReadList<Parameter>(parameters, 2);
					break;
				case Code.Ldarg_3:
					instruction.OpCode = OpCodes.Ldarg;
					instruction.Operand = MethodUtils.ReadList<Parameter>(parameters, 3);
					break;
				case Code.Ldloc_0:
					instruction.OpCode = OpCodes.Ldloc;
					instruction.Operand = MethodUtils.ReadList<Local>(locals, 0);
					break;
				case Code.Ldloc_1:
					instruction.OpCode = OpCodes.Ldloc;
					instruction.Operand = MethodUtils.ReadList<Local>(locals, 1);
					break;
				case Code.Ldloc_2:
					instruction.OpCode = OpCodes.Ldloc;
					instruction.Operand = MethodUtils.ReadList<Local>(locals, 2);
					break;
				case Code.Ldloc_3:
					instruction.OpCode = OpCodes.Ldloc;
					instruction.Operand = MethodUtils.ReadList<Local>(locals, 3);
					break;
				case Code.Stloc_0:
					instruction.OpCode = OpCodes.Stloc;
					instruction.Operand = MethodUtils.ReadList<Local>(locals, 0);
					break;
				case Code.Stloc_1:
					instruction.OpCode = OpCodes.Stloc;
					instruction.Operand = MethodUtils.ReadList<Local>(locals, 1);
					break;
				case Code.Stloc_2:
					instruction.OpCode = OpCodes.Stloc;
					instruction.Operand = MethodUtils.ReadList<Local>(locals, 2);
					break;
				case Code.Stloc_3:
					instruction.OpCode = OpCodes.Stloc;
					instruction.Operand = MethodUtils.ReadList<Local>(locals, 3);
					break;
				case Code.Ldarg_S:
					instruction.OpCode = OpCodes.Ldarg;
					break;
				case Code.Ldarga_S:
					instruction.OpCode = OpCodes.Ldarga;
					break;
				case Code.Starg_S:
					instruction.OpCode = OpCodes.Starg;
					break;
				case Code.Ldloc_S:
					instruction.OpCode = OpCodes.Ldloc;
					break;
				case Code.Ldloca_S:
					instruction.OpCode = OpCodes.Ldloca;
					break;
				case Code.Stloc_S:
					instruction.OpCode = OpCodes.Stloc;
					break;
				case Code.Ldnull:
				case Code.Ldc_I4:
				case Code.Ldc_I8:
				case Code.Ldc_R4:
				case Code.Ldc_R8:
				case (Code)36:
				case Code.Dup:
				case Code.Pop:
				case Code.Jmp:
				case Code.Call:
				case Code.Calli:
				case Code.Ret:
					break;
				case Code.Ldc_I4_M1:
					instruction.OpCode = OpCodes.Ldc_I4;
					instruction.Operand = -1;
					break;
				case Code.Ldc_I4_0:
					instruction.OpCode = OpCodes.Ldc_I4;
					instruction.Operand = 0;
					break;
				case Code.Ldc_I4_1:
					instruction.OpCode = OpCodes.Ldc_I4;
					instruction.Operand = 1;
					break;
				case Code.Ldc_I4_2:
					instruction.OpCode = OpCodes.Ldc_I4;
					instruction.Operand = 2;
					break;
				case Code.Ldc_I4_3:
					instruction.OpCode = OpCodes.Ldc_I4;
					instruction.Operand = 3;
					break;
				case Code.Ldc_I4_4:
					instruction.OpCode = OpCodes.Ldc_I4;
					instruction.Operand = 4;
					break;
				case Code.Ldc_I4_5:
					instruction.OpCode = OpCodes.Ldc_I4;
					instruction.Operand = 5;
					break;
				case Code.Ldc_I4_6:
					instruction.OpCode = OpCodes.Ldc_I4;
					instruction.Operand = 6;
					break;
				case Code.Ldc_I4_7:
					instruction.OpCode = OpCodes.Ldc_I4;
					instruction.Operand = 7;
					break;
				case Code.Ldc_I4_8:
					instruction.OpCode = OpCodes.Ldc_I4;
					instruction.Operand = 8;
					break;
				case Code.Ldc_I4_S:
					instruction.OpCode = OpCodes.Ldc_I4;
					instruction.Operand = (int)((sbyte)instruction.Operand);
					break;
				case Code.Br_S:
					instruction.OpCode = OpCodes.Br;
					break;
				case Code.Brfalse_S:
					instruction.OpCode = OpCodes.Brfalse;
					break;
				case Code.Brtrue_S:
					instruction.OpCode = OpCodes.Brtrue;
					break;
				case Code.Beq_S:
					instruction.OpCode = OpCodes.Beq;
					break;
				case Code.Bge_S:
					instruction.OpCode = OpCodes.Bge;
					break;
				case Code.Bgt_S:
					instruction.OpCode = OpCodes.Bgt;
					break;
				case Code.Ble_S:
					instruction.OpCode = OpCodes.Ble;
					break;
				case Code.Blt_S:
					instruction.OpCode = OpCodes.Blt;
					break;
				case Code.Bne_Un_S:
					instruction.OpCode = OpCodes.Bne_Un;
					break;
				case Code.Bge_Un_S:
					instruction.OpCode = OpCodes.Bge_Un;
					break;
				case Code.Bgt_Un_S:
					instruction.OpCode = OpCodes.Bgt_Un;
					break;
				case Code.Ble_Un_S:
					instruction.OpCode = OpCodes.Ble_Un;
					break;
				case Code.Blt_Un_S:
					instruction.OpCode = OpCodes.Blt_Un;
					break;
				default:
					if (code == Code.Leave_S)
					{
						instruction.OpCode = OpCodes.Leave;
					}
					break;
				}
			}
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x001D0CB4 File Offset: 0x001D0CB4
		private static T ReadList<T>(IList<T> list, int index)
		{
			if (list == null)
			{
				return default(T);
			}
			if (index < list.Count)
			{
				return list[index];
			}
			return default(T);
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x001D0CF4 File Offset: 0x001D0CF4
		public static void OptimizeMacros(this IList<Instruction> instructions)
		{
			int count = instructions.Count;
			int i = 0;
			while (i < count)
			{
				Instruction instruction = instructions[i];
				Code code = instruction.OpCode.Code;
				Parameter parameter;
				Local local;
				if (code <= Code.Ldloc_S)
				{
					if (code == Code.Ldarg_S)
					{
						goto IL_7A;
					}
					if (code == Code.Ldloc_S)
					{
						goto IL_318;
					}
				}
				else
				{
					if (code != Code.Stloc_S)
					{
						if (code - Code.Ldc_I4_S > 1)
						{
							switch (code)
							{
							case Code.Ldarg:
								goto IL_7A;
							case Code.Ldarga:
								parameter = (instruction.Operand as Parameter);
								if (parameter != null && 0 <= parameter.Index && parameter.Index <= 255)
								{
									instruction.OpCode = OpCodes.Ldarga_S;
									goto IL_539;
								}
								goto IL_539;
							case Code.Starg:
								parameter = (instruction.Operand as Parameter);
								if (parameter != null && 0 <= parameter.Index && parameter.Index <= 255)
								{
									instruction.OpCode = OpCodes.Starg_S;
									goto IL_539;
								}
								goto IL_539;
							case Code.Ldloc:
								goto IL_318;
							case Code.Ldloca:
								local = (instruction.Operand as Local);
								if (local != null && 0 <= local.Index && local.Index <= 255)
								{
									instruction.OpCode = OpCodes.Ldloca_S;
									goto IL_539;
								}
								goto IL_539;
							case Code.Stloc:
								break;
							default:
								goto IL_539;
							}
						}
						else
						{
							int num;
							if (instruction.Operand is int)
							{
								num = (int)instruction.Operand;
							}
							else
							{
								if (!(instruction.Operand is sbyte))
								{
									goto IL_539;
								}
								num = (int)((sbyte)instruction.Operand);
							}
							switch (num)
							{
							case -1:
								instruction.OpCode = OpCodes.Ldc_I4_M1;
								instruction.Operand = null;
								goto IL_539;
							case 0:
								instruction.OpCode = OpCodes.Ldc_I4_0;
								instruction.Operand = null;
								goto IL_539;
							case 1:
								instruction.OpCode = OpCodes.Ldc_I4_1;
								instruction.Operand = null;
								goto IL_539;
							case 2:
								instruction.OpCode = OpCodes.Ldc_I4_2;
								instruction.Operand = null;
								goto IL_539;
							case 3:
								instruction.OpCode = OpCodes.Ldc_I4_3;
								instruction.Operand = null;
								goto IL_539;
							case 4:
								instruction.OpCode = OpCodes.Ldc_I4_4;
								instruction.Operand = null;
								goto IL_539;
							case 5:
								instruction.OpCode = OpCodes.Ldc_I4_5;
								instruction.Operand = null;
								goto IL_539;
							case 6:
								instruction.OpCode = OpCodes.Ldc_I4_6;
								instruction.Operand = null;
								goto IL_539;
							case 7:
								instruction.OpCode = OpCodes.Ldc_I4_7;
								instruction.Operand = null;
								goto IL_539;
							case 8:
								instruction.OpCode = OpCodes.Ldc_I4_8;
								instruction.Operand = null;
								goto IL_539;
							default:
								if (-128 <= num && num <= 127)
								{
									instruction.OpCode = OpCodes.Ldc_I4_S;
									instruction.Operand = (sbyte)num;
									goto IL_539;
								}
								goto IL_539;
							}
						}
					}
					local = (instruction.Operand as Local);
					if (local != null)
					{
						if (local.Index == 0)
						{
							instruction.OpCode = OpCodes.Stloc_0;
							instruction.Operand = null;
						}
						else if (local.Index == 1)
						{
							instruction.OpCode = OpCodes.Stloc_1;
							instruction.Operand = null;
						}
						else if (local.Index == 2)
						{
							instruction.OpCode = OpCodes.Stloc_2;
							instruction.Operand = null;
						}
						else if (local.Index == 3)
						{
							instruction.OpCode = OpCodes.Stloc_3;
							instruction.Operand = null;
						}
						else if (0 <= local.Index && local.Index <= 255)
						{
							instruction.OpCode = OpCodes.Stloc_S;
						}
					}
				}
				IL_539:
				i++;
				continue;
				IL_7A:
				parameter = (instruction.Operand as Parameter);
				if (parameter == null)
				{
					goto IL_539;
				}
				if (parameter.Index == 0)
				{
					instruction.OpCode = OpCodes.Ldarg_0;
					instruction.Operand = null;
					goto IL_539;
				}
				if (parameter.Index == 1)
				{
					instruction.OpCode = OpCodes.Ldarg_1;
					instruction.Operand = null;
					goto IL_539;
				}
				if (parameter.Index == 2)
				{
					instruction.OpCode = OpCodes.Ldarg_2;
					instruction.Operand = null;
					goto IL_539;
				}
				if (parameter.Index == 3)
				{
					instruction.OpCode = OpCodes.Ldarg_3;
					instruction.Operand = null;
					goto IL_539;
				}
				if (0 <= parameter.Index && parameter.Index <= 255)
				{
					instruction.OpCode = OpCodes.Ldarg_S;
					goto IL_539;
				}
				goto IL_539;
				IL_318:
				local = (instruction.Operand as Local);
				if (local == null)
				{
					goto IL_539;
				}
				if (local.Index == 0)
				{
					instruction.OpCode = OpCodes.Ldloc_0;
					instruction.Operand = null;
					goto IL_539;
				}
				if (local.Index == 1)
				{
					instruction.OpCode = OpCodes.Ldloc_1;
					instruction.Operand = null;
					goto IL_539;
				}
				if (local.Index == 2)
				{
					instruction.OpCode = OpCodes.Ldloc_2;
					instruction.Operand = null;
					goto IL_539;
				}
				if (local.Index == 3)
				{
					instruction.OpCode = OpCodes.Ldloc_3;
					instruction.Operand = null;
					goto IL_539;
				}
				if (0 <= local.Index && local.Index <= 255)
				{
					instruction.OpCode = OpCodes.Ldloc_S;
					goto IL_539;
				}
				goto IL_539;
			}
			instructions.OptimizeBranches();
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x001D1250 File Offset: 0x001D1250
		public static void SimplifyBranches(this IList<Instruction> instructions)
		{
			int count = instructions.Count;
			for (int i = 0; i < count; i++)
			{
				Instruction instruction = instructions[i];
				Code code = instruction.OpCode.Code;
				switch (code)
				{
				case Code.Br_S:
					instruction.OpCode = OpCodes.Br;
					break;
				case Code.Brfalse_S:
					instruction.OpCode = OpCodes.Brfalse;
					break;
				case Code.Brtrue_S:
					instruction.OpCode = OpCodes.Brtrue;
					break;
				case Code.Beq_S:
					instruction.OpCode = OpCodes.Beq;
					break;
				case Code.Bge_S:
					instruction.OpCode = OpCodes.Bge;
					break;
				case Code.Bgt_S:
					instruction.OpCode = OpCodes.Bgt;
					break;
				case Code.Ble_S:
					instruction.OpCode = OpCodes.Ble;
					break;
				case Code.Blt_S:
					instruction.OpCode = OpCodes.Blt;
					break;
				case Code.Bne_Un_S:
					instruction.OpCode = OpCodes.Bne_Un;
					break;
				case Code.Bge_Un_S:
					instruction.OpCode = OpCodes.Bge_Un;
					break;
				case Code.Bgt_Un_S:
					instruction.OpCode = OpCodes.Bgt_Un;
					break;
				case Code.Ble_Un_S:
					instruction.OpCode = OpCodes.Ble_Un;
					break;
				case Code.Blt_Un_S:
					instruction.OpCode = OpCodes.Blt_Un;
					break;
				default:
					if (code == Code.Leave_S)
					{
						instruction.OpCode = OpCodes.Leave;
					}
					break;
				}
			}
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x001D13B8 File Offset: 0x001D13B8
		public static void OptimizeBranches(this IList<Instruction> instructions)
		{
			bool flag;
			do
			{
				instructions.UpdateInstructionOffsets();
				flag = false;
				int count = instructions.Count;
				int i = 0;
				while (i < count)
				{
					Instruction instruction = instructions[i];
					Code code = instruction.OpCode.Code;
					OpCode opCode;
					switch (code)
					{
					case Code.Br:
						opCode = OpCodes.Br_S;
						goto IL_11E;
					case Code.Brfalse:
						opCode = OpCodes.Brfalse_S;
						goto IL_11E;
					case Code.Brtrue:
						opCode = OpCodes.Brtrue_S;
						goto IL_11E;
					case Code.Beq:
						opCode = OpCodes.Beq_S;
						goto IL_11E;
					case Code.Bge:
						opCode = OpCodes.Bge_S;
						goto IL_11E;
					case Code.Bgt:
						opCode = OpCodes.Bgt_S;
						goto IL_11E;
					case Code.Ble:
						opCode = OpCodes.Ble_S;
						goto IL_11E;
					case Code.Blt:
						opCode = OpCodes.Blt_S;
						goto IL_11E;
					case Code.Bne_Un:
						opCode = OpCodes.Bne_Un_S;
						goto IL_11E;
					case Code.Bge_Un:
						opCode = OpCodes.Bge_Un_S;
						goto IL_11E;
					case Code.Bgt_Un:
						opCode = OpCodes.Bgt_Un_S;
						goto IL_11E;
					case Code.Ble_Un:
						opCode = OpCodes.Ble_Un_S;
						goto IL_11E;
					case Code.Blt_Un:
						opCode = OpCodes.Blt_Un_S;
						goto IL_11E;
					default:
						if (code == Code.Leave)
						{
							opCode = OpCodes.Leave_S;
							goto IL_11E;
						}
						break;
					}
					IL_192:
					i++;
					continue;
					IL_11E:
					Instruction instruction2 = instruction.Operand as Instruction;
					if (instruction2 == null)
					{
						goto IL_192;
					}
					int num;
					if (instruction2.Offset >= instruction.Offset)
					{
						num = (int)(instruction.Offset + (uint)instruction.GetSize());
					}
					else
					{
						num = (int)(instruction.Offset + (uint)opCode.Size + 1U);
					}
					int num2 = (int)(instruction2.Offset - (uint)num);
					if (-128 <= num2 && num2 <= 127)
					{
						instruction.OpCode = opCode;
						flag = true;
						goto IL_192;
					}
					goto IL_192;
				}
			}
			while (flag);
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x001D156C File Offset: 0x001D156C
		public static uint UpdateInstructionOffsets(this IList<Instruction> instructions)
		{
			uint num = 0U;
			int count = instructions.Count;
			for (int i = 0; i < count; i++)
			{
				Instruction instruction = instructions[i];
				instruction.Offset = num;
				num += (uint)instruction.GetSize();
			}
			return num;
		}
	}
}
