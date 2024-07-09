using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009DF RID: 2527
	[ComVisible(true)]
	public sealed class Instruction
	{
		// Token: 0x060060BD RID: 24765 RVA: 0x001CD908 File Offset: 0x001CD908
		public Instruction()
		{
		}

		// Token: 0x060060BE RID: 24766 RVA: 0x001CD910 File Offset: 0x001CD910
		public Instruction(OpCode opCode)
		{
			this.OpCode = opCode;
		}

		// Token: 0x060060BF RID: 24767 RVA: 0x001CD920 File Offset: 0x001CD920
		public Instruction(OpCode opCode, object operand)
		{
			this.OpCode = opCode;
			this.Operand = operand;
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x001CD938 File Offset: 0x001CD938
		public static Instruction Create(OpCode opCode)
		{
			if (opCode.OperandType != OperandType.InlineNone)
			{
				throw new ArgumentException("Must be a no-operand opcode", "opCode");
			}
			return new Instruction(opCode);
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x001CD95C File Offset: 0x001CD95C
		public static Instruction Create(OpCode opCode, byte value)
		{
			if (opCode.Code != Code.Unaligned)
			{
				throw new ArgumentException("Opcode does not have a byte operand", "opCode");
			}
			return new Instruction(opCode, value);
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x001CD98C File Offset: 0x001CD98C
		public static Instruction Create(OpCode opCode, sbyte value)
		{
			if (opCode.Code != Code.Ldc_I4_S)
			{
				throw new ArgumentException("Opcode does not have a sbyte operand", "opCode");
			}
			return new Instruction(opCode, value);
		}

		// Token: 0x060060C3 RID: 24771 RVA: 0x001CD9B8 File Offset: 0x001CD9B8
		public static Instruction Create(OpCode opCode, int value)
		{
			if (opCode.OperandType != OperandType.InlineI)
			{
				throw new ArgumentException("Opcode does not have an int32 operand", "opCode");
			}
			return new Instruction(opCode, value);
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x001CD9E4 File Offset: 0x001CD9E4
		public static Instruction Create(OpCode opCode, long value)
		{
			if (opCode.OperandType != OperandType.InlineI8)
			{
				throw new ArgumentException("Opcode does not have an int64 operand", "opCode");
			}
			return new Instruction(opCode, value);
		}

		// Token: 0x060060C5 RID: 24773 RVA: 0x001CDA10 File Offset: 0x001CDA10
		public static Instruction Create(OpCode opCode, float value)
		{
			if (opCode.OperandType != OperandType.ShortInlineR)
			{
				throw new ArgumentException("Opcode does not have a real4 operand", "opCode");
			}
			return new Instruction(opCode, value);
		}

		// Token: 0x060060C6 RID: 24774 RVA: 0x001CDA3C File Offset: 0x001CDA3C
		public static Instruction Create(OpCode opCode, double value)
		{
			if (opCode.OperandType != OperandType.InlineR)
			{
				throw new ArgumentException("Opcode does not have a real8 operand", "opCode");
			}
			return new Instruction(opCode, value);
		}

		// Token: 0x060060C7 RID: 24775 RVA: 0x001CDA68 File Offset: 0x001CDA68
		public static Instruction Create(OpCode opCode, string s)
		{
			if (opCode.OperandType != OperandType.InlineString)
			{
				throw new ArgumentException("Opcode does not have a string operand", "opCode");
			}
			return new Instruction(opCode, s);
		}

		// Token: 0x060060C8 RID: 24776 RVA: 0x001CDA90 File Offset: 0x001CDA90
		public static Instruction Create(OpCode opCode, Instruction target)
		{
			if (opCode.OperandType != OperandType.ShortInlineBrTarget && opCode.OperandType != OperandType.InlineBrTarget)
			{
				throw new ArgumentException("Opcode does not have an instruction operand", "opCode");
			}
			return new Instruction(opCode, target);
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x001CDAC4 File Offset: 0x001CDAC4
		public static Instruction Create(OpCode opCode, IList<Instruction> targets)
		{
			if (opCode.OperandType != OperandType.InlineSwitch)
			{
				throw new ArgumentException("Opcode does not have a targets array operand", "opCode");
			}
			return new Instruction(opCode, targets);
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x001CDAEC File Offset: 0x001CDAEC
		public static Instruction Create(OpCode opCode, ITypeDefOrRef type)
		{
			if (opCode.OperandType != OperandType.InlineType && opCode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("Opcode does not have a type operand", "opCode");
			}
			return new Instruction(opCode, type);
		}

		// Token: 0x060060CB RID: 24779 RVA: 0x001CDB20 File Offset: 0x001CDB20
		public static Instruction Create(OpCode opCode, CorLibTypeSig type)
		{
			return Instruction.Create(opCode, type.TypeDefOrRef);
		}

		// Token: 0x060060CC RID: 24780 RVA: 0x001CDB30 File Offset: 0x001CDB30
		public static Instruction Create(OpCode opCode, MemberRef mr)
		{
			if (opCode.OperandType != OperandType.InlineField && opCode.OperandType != OperandType.InlineMethod && opCode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("Opcode does not have a field operand", "opCode");
			}
			return new Instruction(opCode, mr);
		}

		// Token: 0x060060CD RID: 24781 RVA: 0x001CDB70 File Offset: 0x001CDB70
		public static Instruction Create(OpCode opCode, IField field)
		{
			if (opCode.OperandType != OperandType.InlineField && opCode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("Opcode does not have a field operand", "opCode");
			}
			return new Instruction(opCode, field);
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x001CDBA4 File Offset: 0x001CDBA4
		public static Instruction Create(OpCode opCode, IMethod method)
		{
			if (opCode.OperandType != OperandType.InlineMethod && opCode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("Opcode does not have a method operand", "opCode");
			}
			return new Instruction(opCode, method);
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x001CDBD8 File Offset: 0x001CDBD8
		public static Instruction Create(OpCode opCode, ITokenOperand token)
		{
			if (opCode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("Opcode does not have a token operand", "opCode");
			}
			return new Instruction(opCode, token);
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x001CDC00 File Offset: 0x001CDC00
		public static Instruction Create(OpCode opCode, MethodSig methodSig)
		{
			if (opCode.OperandType != OperandType.InlineSig)
			{
				throw new ArgumentException("Opcode does not have a method sig operand", "opCode");
			}
			return new Instruction(opCode, methodSig);
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x001CDC28 File Offset: 0x001CDC28
		public static Instruction Create(OpCode opCode, Parameter parameter)
		{
			if (opCode.OperandType != OperandType.ShortInlineVar && opCode.OperandType != OperandType.InlineVar)
			{
				throw new ArgumentException("Opcode does not have a method parameter operand", "opCode");
			}
			return new Instruction(opCode, parameter);
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x001CDC5C File Offset: 0x001CDC5C
		public static Instruction Create(OpCode opCode, Local local)
		{
			if (opCode.OperandType != OperandType.ShortInlineVar && opCode.OperandType != OperandType.InlineVar)
			{
				throw new ArgumentException("Opcode does not have a method local operand", "opCode");
			}
			return new Instruction(opCode, local);
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x001CDC90 File Offset: 0x001CDC90
		public static Instruction CreateLdcI4(int value)
		{
			switch (value)
			{
			case -1:
				return OpCodes.Ldc_I4_M1.ToInstruction();
			case 0:
				return OpCodes.Ldc_I4_0.ToInstruction();
			case 1:
				return OpCodes.Ldc_I4_1.ToInstruction();
			case 2:
				return OpCodes.Ldc_I4_2.ToInstruction();
			case 3:
				return OpCodes.Ldc_I4_3.ToInstruction();
			case 4:
				return OpCodes.Ldc_I4_4.ToInstruction();
			case 5:
				return OpCodes.Ldc_I4_5.ToInstruction();
			case 6:
				return OpCodes.Ldc_I4_6.ToInstruction();
			case 7:
				return OpCodes.Ldc_I4_7.ToInstruction();
			case 8:
				return OpCodes.Ldc_I4_8.ToInstruction();
			default:
				if (-128 <= value && value <= 127)
				{
					return new Instruction(OpCodes.Ldc_I4_S, (sbyte)value);
				}
				return new Instruction(OpCodes.Ldc_I4, value);
			}
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x001CDD78 File Offset: 0x001CDD78
		public int GetSize()
		{
			OpCode opCode = this.OpCode;
			switch (opCode.OperandType)
			{
			case OperandType.InlineBrTarget:
			case OperandType.InlineField:
			case OperandType.InlineI:
			case OperandType.InlineMethod:
			case OperandType.InlineSig:
			case OperandType.InlineString:
			case OperandType.InlineTok:
			case OperandType.InlineType:
			case OperandType.ShortInlineR:
				return opCode.Size + 4;
			case OperandType.InlineI8:
			case OperandType.InlineR:
				return opCode.Size + 8;
			case OperandType.InlineSwitch:
			{
				IList<Instruction> list = this.Operand as IList<Instruction>;
				return opCode.Size + 4 + ((list == null) ? 0 : (list.Count * 4));
			}
			case OperandType.InlineVar:
				return opCode.Size + 2;
			case OperandType.ShortInlineBrTarget:
			case OperandType.ShortInlineI:
			case OperandType.ShortInlineVar:
				return opCode.Size + 1;
			}
			return opCode.Size;
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x001CDE44 File Offset: 0x001CDE44
		private static bool IsSystemVoid(TypeSig type)
		{
			return type.RemovePinnedAndModifiers().GetElementType() == ElementType.Void;
		}

		// Token: 0x060060D6 RID: 24790 RVA: 0x001CDE54 File Offset: 0x001CDE54
		public void UpdateStack(ref int stack)
		{
			this.UpdateStack(ref stack, false);
		}

		// Token: 0x060060D7 RID: 24791 RVA: 0x001CDE60 File Offset: 0x001CDE60
		public void UpdateStack(ref int stack, bool methodHasReturnValue)
		{
			int num;
			int num2;
			this.CalculateStackUsage(methodHasReturnValue, out num, out num2);
			if (num2 == -1)
			{
				stack = 0;
				return;
			}
			stack += num - num2;
		}

		// Token: 0x060060D8 RID: 24792 RVA: 0x001CDE90 File Offset: 0x001CDE90
		public void CalculateStackUsage(out int pushes, out int pops)
		{
			this.CalculateStackUsage(false, out pushes, out pops);
		}

		// Token: 0x060060D9 RID: 24793 RVA: 0x001CDE9C File Offset: 0x001CDE9C
		public void CalculateStackUsage(bool methodHasReturnValue, out int pushes, out int pops)
		{
			OpCode opCode = this.OpCode;
			if (opCode.FlowControl == FlowControl.Call)
			{
				this.CalculateStackUsageCall(opCode.Code, out pushes, out pops);
				return;
			}
			this.CalculateStackUsageNonCall(opCode, methodHasReturnValue, out pushes, out pops);
		}

		// Token: 0x060060DA RID: 24794 RVA: 0x001CDEDC File Offset: 0x001CDEDC
		private void CalculateStackUsageCall(Code code, out int pushes, out int pops)
		{
			pushes = 0;
			pops = 0;
			if (code == Code.Jmp)
			{
				return;
			}
			object operand = this.Operand;
			IMethod method = operand as IMethod;
			MethodSig methodSig;
			if (method != null)
			{
				methodSig = method.MethodSig;
			}
			else
			{
				methodSig = (operand as MethodSig);
			}
			if (methodSig == null)
			{
				return;
			}
			bool implicitThis = methodSig.ImplicitThis;
			if (!Instruction.IsSystemVoid(methodSig.RetType) || (code == Code.Newobj && methodSig.HasThis))
			{
				pushes++;
			}
			pops += methodSig.Params.Count;
			IList<TypeSig> paramsAfterSentinel = methodSig.ParamsAfterSentinel;
			if (paramsAfterSentinel != null)
			{
				pops += paramsAfterSentinel.Count;
			}
			if (implicitThis && code != Code.Newobj)
			{
				pops++;
			}
			if (code == Code.Calli)
			{
				pops++;
			}
		}

		// Token: 0x060060DB RID: 24795 RVA: 0x001CDFA4 File Offset: 0x001CDFA4
		private void CalculateStackUsageNonCall(OpCode opCode, bool hasReturnValue, out int pushes, out int pops)
		{
			switch (opCode.StackBehaviourPush)
			{
			case StackBehaviour.Push0:
				pushes = 0;
				goto IL_58;
			case StackBehaviour.Push1:
			case StackBehaviour.Pushi:
			case StackBehaviour.Pushi8:
			case StackBehaviour.Pushr4:
			case StackBehaviour.Pushr8:
			case StackBehaviour.Pushref:
				pushes = 1;
				goto IL_58;
			case StackBehaviour.Push1_push1:
				pushes = 2;
				goto IL_58;
			}
			pushes = 0;
			IL_58:
			StackBehaviour stackBehaviourPop = opCode.StackBehaviourPop;
			switch (stackBehaviourPop)
			{
			case StackBehaviour.Pop0:
				pops = 0;
				return;
			case StackBehaviour.Pop1:
			case StackBehaviour.Popi:
			case StackBehaviour.Popref:
				pops = 1;
				return;
			case StackBehaviour.Pop1_pop1:
			case StackBehaviour.Popi_pop1:
			case StackBehaviour.Popi_popi:
			case StackBehaviour.Popi_popi8:
			case StackBehaviour.Popi_popr4:
			case StackBehaviour.Popi_popr8:
			case StackBehaviour.Popref_pop1:
			case StackBehaviour.Popref_popi:
				pops = 2;
				return;
			case StackBehaviour.Popi_popi_popi:
			case StackBehaviour.Popref_popi_popi:
			case StackBehaviour.Popref_popi_popi8:
			case StackBehaviour.Popref_popi_popr4:
			case StackBehaviour.Popref_popi_popr8:
			case StackBehaviour.Popref_popi_popref:
			case StackBehaviour.Popref_popi_pop1:
				pops = 3;
				return;
			case StackBehaviour.Push0:
			case StackBehaviour.Push1:
			case StackBehaviour.Push1_push1:
			case StackBehaviour.Pushi:
			case StackBehaviour.Pushi8:
			case StackBehaviour.Pushr4:
			case StackBehaviour.Pushr8:
			case StackBehaviour.Pushref:
			case StackBehaviour.Varpush:
				break;
			case StackBehaviour.Varpop:
				if (hasReturnValue)
				{
					pops = 1;
					return;
				}
				pops = 0;
				return;
			default:
				if (stackBehaviourPop == StackBehaviour.PopAll)
				{
					pops = -1;
					return;
				}
				break;
			}
			pops = 0;
		}

		// Token: 0x060060DC RID: 24796 RVA: 0x001CE0CC File Offset: 0x001CE0CC
		public bool IsLeave()
		{
			return this.OpCode == OpCodes.Leave || this.OpCode == OpCodes.Leave_S;
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x001CE0F0 File Offset: 0x001CE0F0
		public bool IsBr()
		{
			return this.OpCode == OpCodes.Br || this.OpCode == OpCodes.Br_S;
		}

		// Token: 0x060060DE RID: 24798 RVA: 0x001CE114 File Offset: 0x001CE114
		public bool IsBrfalse()
		{
			return this.OpCode == OpCodes.Brfalse || this.OpCode == OpCodes.Brfalse_S;
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x001CE138 File Offset: 0x001CE138
		public bool IsBrtrue()
		{
			return this.OpCode == OpCodes.Brtrue || this.OpCode == OpCodes.Brtrue_S;
		}

		// Token: 0x060060E0 RID: 24800 RVA: 0x001CE15C File Offset: 0x001CE15C
		public bool IsConditionalBranch()
		{
			Code code = this.OpCode.Code;
			return code - Code.Brfalse_S <= 11 || code - Code.Brfalse <= 11;
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x001CE194 File Offset: 0x001CE194
		public bool IsLdcI4()
		{
			Code code = this.OpCode.Code;
			return code - Code.Ldc_I4_M1 <= 11;
		}

		// Token: 0x060060E2 RID: 24802 RVA: 0x001CE1C0 File Offset: 0x001CE1C0
		public int GetLdcI4Value()
		{
			int result;
			switch (this.OpCode.Code)
			{
			case Code.Ldc_I4_M1:
				result = -1;
				break;
			case Code.Ldc_I4_0:
				result = 0;
				break;
			case Code.Ldc_I4_1:
				result = 1;
				break;
			case Code.Ldc_I4_2:
				result = 2;
				break;
			case Code.Ldc_I4_3:
				result = 3;
				break;
			case Code.Ldc_I4_4:
				result = 4;
				break;
			case Code.Ldc_I4_5:
				result = 5;
				break;
			case Code.Ldc_I4_6:
				result = 6;
				break;
			case Code.Ldc_I4_7:
				result = 7;
				break;
			case Code.Ldc_I4_8:
				result = 8;
				break;
			case Code.Ldc_I4_S:
				result = (int)((sbyte)this.Operand);
				break;
			case Code.Ldc_I4:
				result = (int)this.Operand;
				break;
			default:
				throw new InvalidOperationException(string.Format("Not a ldc.i4 instruction: {0}", this));
			}
			return result;
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x001CE298 File Offset: 0x001CE298
		public bool IsLdarg()
		{
			Code code = this.OpCode.Code;
			return code - Code.Ldarg_0 <= 3 || code == Code.Ldarg_S || code == Code.Ldarg;
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x001CE2D4 File Offset: 0x001CE2D4
		public bool IsLdloc()
		{
			Code code = this.OpCode.Code;
			return code - Code.Ldloc_0 <= 3 || code == Code.Ldloc_S || code == Code.Ldloc;
		}

		// Token: 0x060060E5 RID: 24805 RVA: 0x001CE310 File Offset: 0x001CE310
		public bool IsStarg()
		{
			Code code = this.OpCode.Code;
			return code == Code.Starg_S || code == Code.Starg;
		}

		// Token: 0x060060E6 RID: 24806 RVA: 0x001CE344 File Offset: 0x001CE344
		public bool IsStloc()
		{
			Code code = this.OpCode.Code;
			return code - Code.Stloc_0 <= 3 || code == Code.Stloc_S || code == Code.Stloc;
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x001CE384 File Offset: 0x001CE384
		public Local GetLocal(IList<Local> locals)
		{
			Code code = this.OpCode.Code;
			int num;
			switch (code)
			{
			case Code.Ldloc_0:
			case Code.Ldloc_1:
			case Code.Ldloc_2:
			case Code.Ldloc_3:
				num = (int)(code - Code.Ldloc_0);
				goto IL_7C;
			case Code.Stloc_0:
			case Code.Stloc_1:
			case Code.Stloc_2:
			case Code.Stloc_3:
				num = (int)(code - Code.Stloc_0);
				goto IL_7C;
			case Code.Ldarg_S:
			case Code.Ldarga_S:
			case Code.Starg_S:
				goto IL_7A;
			case Code.Ldloc_S:
			case Code.Ldloca_S:
			case Code.Stloc_S:
				break;
			default:
				if (code - Code.Ldloc > 2)
				{
					goto IL_7A;
				}
				break;
			}
			return this.Operand as Local;
			IL_7A:
			return null;
			IL_7C:
			if (num < locals.Count)
			{
				return locals[num];
			}
			return null;
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x001CE428 File Offset: 0x001CE428
		public int GetParameterIndex()
		{
			Code code = this.OpCode.Code;
			switch (code)
			{
			case Code.Ldarg_0:
				return 0;
			case Code.Ldarg_1:
				return 1;
			case Code.Ldarg_2:
				return 2;
			case Code.Ldarg_3:
				return 3;
			default:
				if (code - Code.Ldarg_S <= 2 || code - Code.Ldarg <= 2)
				{
					Parameter parameter = this.Operand as Parameter;
					if (parameter != null)
					{
						return parameter.Index;
					}
				}
				return -1;
			}
		}

		// Token: 0x060060E9 RID: 24809 RVA: 0x001CE49C File Offset: 0x001CE49C
		public Parameter GetParameter(IList<Parameter> parameters)
		{
			int parameterIndex = this.GetParameterIndex();
			if (parameterIndex < parameters.Count)
			{
				return parameters[parameterIndex];
			}
			return null;
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x001CE4CC File Offset: 0x001CE4CC
		public TypeSig GetArgumentType(MethodSig methodSig, ITypeDefOrRef declaringType)
		{
			if (methodSig == null)
			{
				return null;
			}
			int num = this.GetParameterIndex();
			if (num == 0 && methodSig.ImplicitThis)
			{
				if (declaringType == null || !declaringType.IsValueType)
				{
					return declaringType.ToTypeSig(true);
				}
				return new ByRefSig(declaringType.ToTypeSig(true));
			}
			else
			{
				if (methodSig.ImplicitThis)
				{
					num--;
				}
				if (num < methodSig.Params.Count)
				{
					return methodSig.Params[num];
				}
				return null;
			}
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x001CE554 File Offset: 0x001CE554
		public Instruction Clone()
		{
			return new Instruction
			{
				Offset = this.Offset,
				OpCode = this.OpCode,
				Operand = this.Operand,
				SequencePoint = this.SequencePoint
			};
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x001CE58C File Offset: 0x001CE58C
		public override string ToString()
		{
			return InstructionPrinter.ToString(this);
		}

		// Token: 0x0400309C RID: 12444
		public OpCode OpCode;

		// Token: 0x0400309D RID: 12445
		public object Operand;

		// Token: 0x0400309E RID: 12446
		public uint Offset;

		// Token: 0x0400309F RID: 12447
		public SequencePoint SequencePoint;
	}
}
