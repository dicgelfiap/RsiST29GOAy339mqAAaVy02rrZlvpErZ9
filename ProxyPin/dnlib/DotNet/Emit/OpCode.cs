using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009ED RID: 2541
	[ComVisible(true)]
	public sealed class OpCode
	{
		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x0600619C RID: 24988 RVA: 0x001D15B0 File Offset: 0x001D15B0
		public short Value
		{
			get
			{
				return (short)this.Code;
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x0600619D RID: 24989 RVA: 0x001D15BC File Offset: 0x001D15BC
		public int Size
		{
			get
			{
				if (this.Code >= Code.UNKNOWN1 && this.Code != Code.UNKNOWN1)
				{
					return 2;
				}
				return 1;
			}
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x001D15E4 File Offset: 0x001D15E4
		internal OpCode(string name, Code code, OperandType operandType, FlowControl flowControl, OpCodeType opCodeType, StackBehaviour push, StackBehaviour pop)
		{
			this.Name = name;
			this.Code = code;
			this.OperandType = operandType;
			this.FlowControl = flowControl;
			this.OpCodeType = opCodeType;
			this.StackBehaviourPush = push;
			this.StackBehaviourPop = pop;
			if (code >> 8 == Code.Nop)
			{
				OpCodes.OneByteOpCodes[(int)((byte)code)] = this;
				return;
			}
			if (code >> 8 == Code.Prefix1)
			{
				OpCodes.TwoByteOpCodes[(int)((byte)code)] = this;
			}
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x001D1660 File Offset: 0x001D1660
		public Instruction ToInstruction()
		{
			return Instruction.Create(this);
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x001D1668 File Offset: 0x001D1668
		public Instruction ToInstruction(byte value)
		{
			return Instruction.Create(this, value);
		}

		// Token: 0x060061A1 RID: 24993 RVA: 0x001D1674 File Offset: 0x001D1674
		public Instruction ToInstruction(sbyte value)
		{
			return Instruction.Create(this, value);
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x001D1680 File Offset: 0x001D1680
		public Instruction ToInstruction(int value)
		{
			return Instruction.Create(this, value);
		}

		// Token: 0x060061A3 RID: 24995 RVA: 0x001D168C File Offset: 0x001D168C
		public Instruction ToInstruction(long value)
		{
			return Instruction.Create(this, value);
		}

		// Token: 0x060061A4 RID: 24996 RVA: 0x001D1698 File Offset: 0x001D1698
		public Instruction ToInstruction(float value)
		{
			return Instruction.Create(this, value);
		}

		// Token: 0x060061A5 RID: 24997 RVA: 0x001D16A4 File Offset: 0x001D16A4
		public Instruction ToInstruction(double value)
		{
			return Instruction.Create(this, value);
		}

		// Token: 0x060061A6 RID: 24998 RVA: 0x001D16B0 File Offset: 0x001D16B0
		public Instruction ToInstruction(string s)
		{
			return Instruction.Create(this, s);
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x001D16BC File Offset: 0x001D16BC
		public Instruction ToInstruction(Instruction target)
		{
			return Instruction.Create(this, target);
		}

		// Token: 0x060061A8 RID: 25000 RVA: 0x001D16C8 File Offset: 0x001D16C8
		public Instruction ToInstruction(IList<Instruction> targets)
		{
			return Instruction.Create(this, targets);
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x001D16D4 File Offset: 0x001D16D4
		public Instruction ToInstruction(ITypeDefOrRef type)
		{
			return Instruction.Create(this, type);
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x001D16E0 File Offset: 0x001D16E0
		public Instruction ToInstruction(CorLibTypeSig type)
		{
			return Instruction.Create(this, type.TypeDefOrRef);
		}

		// Token: 0x060061AB RID: 25003 RVA: 0x001D16F0 File Offset: 0x001D16F0
		public Instruction ToInstruction(MemberRef mr)
		{
			return Instruction.Create(this, mr);
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x001D16FC File Offset: 0x001D16FC
		public Instruction ToInstruction(IField field)
		{
			return Instruction.Create(this, field);
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x001D1708 File Offset: 0x001D1708
		public Instruction ToInstruction(IMethod method)
		{
			return Instruction.Create(this, method);
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x001D1714 File Offset: 0x001D1714
		public Instruction ToInstruction(ITokenOperand token)
		{
			return Instruction.Create(this, token);
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x001D1720 File Offset: 0x001D1720
		public Instruction ToInstruction(MethodSig methodSig)
		{
			return Instruction.Create(this, methodSig);
		}

		// Token: 0x060061B0 RID: 25008 RVA: 0x001D172C File Offset: 0x001D172C
		public Instruction ToInstruction(Parameter parameter)
		{
			return Instruction.Create(this, parameter);
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x001D1738 File Offset: 0x001D1738
		public Instruction ToInstruction(Local local)
		{
			return Instruction.Create(this, local);
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x001D1744 File Offset: 0x001D1744
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x040030CF RID: 12495
		public readonly string Name;

		// Token: 0x040030D0 RID: 12496
		public readonly Code Code;

		// Token: 0x040030D1 RID: 12497
		public readonly OperandType OperandType;

		// Token: 0x040030D2 RID: 12498
		public readonly FlowControl FlowControl;

		// Token: 0x040030D3 RID: 12499
		public readonly OpCodeType OpCodeType;

		// Token: 0x040030D4 RID: 12500
		public readonly StackBehaviour StackBehaviourPush;

		// Token: 0x040030D5 RID: 12501
		public readonly StackBehaviour StackBehaviourPop;
	}
}
