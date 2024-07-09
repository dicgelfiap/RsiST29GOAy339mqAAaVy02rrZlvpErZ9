using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008BD RID: 2237
	[ComVisible(true)]
	public abstract class MethodBodyWriterBase
	{
		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06005675 RID: 22133 RVA: 0x001A6CC4 File Offset: 0x001A6CC4
		public bool ErrorDetected
		{
			get
			{
				return this.errors > 0;
			}
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x001A6CD0 File Offset: 0x001A6CD0
		internal MethodBodyWriterBase()
		{
		}

		// Token: 0x06005677 RID: 22135 RVA: 0x001A6CF0 File Offset: 0x001A6CF0
		protected MethodBodyWriterBase(IList<Instruction> instructions, IList<ExceptionHandler> exceptionHandlers)
		{
			this.instructions = instructions;
			this.exceptionHandlers = exceptionHandlers;
		}

		// Token: 0x06005678 RID: 22136 RVA: 0x001A6D1C File Offset: 0x001A6D1C
		internal void Reset(IList<Instruction> instructions, IList<ExceptionHandler> exceptionHandlers)
		{
			this.instructions = instructions;
			this.exceptionHandlers = exceptionHandlers;
			this.offsets.Clear();
			this.firstInstructionOffset = 0U;
			this.errors = 0;
		}

		// Token: 0x06005679 RID: 22137 RVA: 0x001A6D48 File Offset: 0x001A6D48
		protected void Error(string message)
		{
			this.errors++;
			this.ErrorImpl(message);
		}

		// Token: 0x0600567A RID: 22138 RVA: 0x001A6D60 File Offset: 0x001A6D60
		protected virtual void ErrorImpl(string message)
		{
		}

		// Token: 0x0600567B RID: 22139 RVA: 0x001A6D64 File Offset: 0x001A6D64
		protected uint GetMaxStack()
		{
			if (this.instructions.Count == 0)
			{
				return 0U;
			}
			this.maxStackCalculator.Reset(this.instructions, this.exceptionHandlers);
			uint num;
			if (!this.maxStackCalculator.Calculate(out num))
			{
				this.Error("Error calculating max stack value. If the method's obfuscated, set CilBody.KeepOldMaxStack or MetadataOptions.Flags (KeepOldMaxStack, global option) to ignore this error. Otherwise fix your generated CIL code so it conforms to the ECMA standard.");
				num += 8U;
			}
			return num;
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x001A6DC0 File Offset: 0x001A6DC0
		protected uint GetOffset(Instruction instr)
		{
			if (instr == null)
			{
				this.Error("Instruction is null");
				return 0U;
			}
			uint result;
			if (this.offsets.TryGetValue(instr, out result))
			{
				return result;
			}
			this.Error("Found some other method's instruction or a removed instruction. You probably removed an instruction that is the target of a branch instruction or an instruction that's the first/last instruction in an exception handler.");
			return 0U;
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x001A6E08 File Offset: 0x001A6E08
		protected uint InitializeInstructionOffsets()
		{
			uint num = 0U;
			IList<Instruction> list = this.instructions;
			for (int i = 0; i < list.Count; i++)
			{
				Instruction instruction = list[i];
				if (instruction != null)
				{
					this.offsets[instruction] = num;
					num += this.GetSizeOfInstruction(instruction);
				}
			}
			return num;
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x001A6E5C File Offset: 0x001A6E5C
		protected virtual uint GetSizeOfInstruction(Instruction instr)
		{
			return (uint)instr.GetSize();
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x001A6E64 File Offset: 0x001A6E64
		protected uint WriteInstructions(ref ArrayWriter writer)
		{
			this.firstInstructionOffset = (uint)writer.Position;
			IList<Instruction> list = this.instructions;
			for (int i = 0; i < list.Count; i++)
			{
				Instruction instruction = list[i];
				if (instruction != null)
				{
					this.WriteInstruction(ref writer, instruction);
				}
			}
			return this.ToInstructionOffset(ref writer);
		}

		// Token: 0x06005680 RID: 22144 RVA: 0x001A6EBC File Offset: 0x001A6EBC
		protected uint ToInstructionOffset(ref ArrayWriter writer)
		{
			return (uint)(writer.Position - (int)this.firstInstructionOffset);
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x001A6ECC File Offset: 0x001A6ECC
		protected virtual void WriteInstruction(ref ArrayWriter writer, Instruction instr)
		{
			this.WriteOpCode(ref writer, instr);
			this.WriteOperand(ref writer, instr);
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x001A6EE0 File Offset: 0x001A6EE0
		protected void WriteOpCode(ref ArrayWriter writer, Instruction instr)
		{
			Code code = instr.OpCode.Code;
			if (code <= Code.Prefixref)
			{
				writer.WriteByte((byte)code);
				return;
			}
			if (code >> 8 == Code.Prefix1)
			{
				writer.WriteByte((byte)(code >> 8));
				writer.WriteByte((byte)code);
				return;
			}
			if (code == Code.UNKNOWN1)
			{
				writer.WriteByte(0);
				return;
			}
			if (code == Code.UNKNOWN2)
			{
				writer.WriteUInt16(0);
				return;
			}
			this.Error("Unknown instruction");
			writer.WriteByte(0);
		}

		// Token: 0x06005683 RID: 22147 RVA: 0x001A6F6C File Offset: 0x001A6F6C
		protected void WriteOperand(ref ArrayWriter writer, Instruction instr)
		{
			switch (instr.OpCode.OperandType)
			{
			case OperandType.InlineBrTarget:
				this.WriteInlineBrTarget(ref writer, instr);
				return;
			case OperandType.InlineField:
				this.WriteInlineField(ref writer, instr);
				return;
			case OperandType.InlineI:
				this.WriteInlineI(ref writer, instr);
				return;
			case OperandType.InlineI8:
				this.WriteInlineI8(ref writer, instr);
				return;
			case OperandType.InlineMethod:
				this.WriteInlineMethod(ref writer, instr);
				return;
			case OperandType.InlineNone:
				this.WriteInlineNone(ref writer, instr);
				return;
			case OperandType.InlinePhi:
				this.WriteInlinePhi(ref writer, instr);
				return;
			case OperandType.InlineR:
				this.WriteInlineR(ref writer, instr);
				return;
			case OperandType.InlineSig:
				this.WriteInlineSig(ref writer, instr);
				return;
			case OperandType.InlineString:
				this.WriteInlineString(ref writer, instr);
				return;
			case OperandType.InlineSwitch:
				this.WriteInlineSwitch(ref writer, instr);
				return;
			case OperandType.InlineTok:
				this.WriteInlineTok(ref writer, instr);
				return;
			case OperandType.InlineType:
				this.WriteInlineType(ref writer, instr);
				return;
			case OperandType.InlineVar:
				this.WriteInlineVar(ref writer, instr);
				return;
			case OperandType.ShortInlineBrTarget:
				this.WriteShortInlineBrTarget(ref writer, instr);
				return;
			case OperandType.ShortInlineI:
				this.WriteShortInlineI(ref writer, instr);
				return;
			case OperandType.ShortInlineR:
				this.WriteShortInlineR(ref writer, instr);
				return;
			case OperandType.ShortInlineVar:
				this.WriteShortInlineVar(ref writer, instr);
				return;
			}
			this.Error("Unknown operand type");
		}

		// Token: 0x06005684 RID: 22148 RVA: 0x001A7090 File Offset: 0x001A7090
		protected virtual void WriteInlineBrTarget(ref ArrayWriter writer, Instruction instr)
		{
			uint value = this.GetOffset(instr.Operand as Instruction) - (this.ToInstructionOffset(ref writer) + 4U);
			writer.WriteUInt32(value);
		}

		// Token: 0x06005685 RID: 22149
		protected abstract void WriteInlineField(ref ArrayWriter writer, Instruction instr);

		// Token: 0x06005686 RID: 22150 RVA: 0x001A70C4 File Offset: 0x001A70C4
		protected virtual void WriteInlineI(ref ArrayWriter writer, Instruction instr)
		{
			if (instr.Operand is int)
			{
				writer.WriteInt32((int)instr.Operand);
				return;
			}
			this.Error("Operand is not an Int32");
			writer.WriteInt32(0);
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x001A70FC File Offset: 0x001A70FC
		protected virtual void WriteInlineI8(ref ArrayWriter writer, Instruction instr)
		{
			if (instr.Operand is long)
			{
				writer.WriteInt64((long)instr.Operand);
				return;
			}
			this.Error("Operand is not an Int64");
			writer.WriteInt64(0L);
		}

		// Token: 0x06005688 RID: 22152
		protected abstract void WriteInlineMethod(ref ArrayWriter writer, Instruction instr);

		// Token: 0x06005689 RID: 22153 RVA: 0x001A7134 File Offset: 0x001A7134
		protected virtual void WriteInlineNone(ref ArrayWriter writer, Instruction instr)
		{
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x001A7138 File Offset: 0x001A7138
		protected virtual void WriteInlinePhi(ref ArrayWriter writer, Instruction instr)
		{
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x001A713C File Offset: 0x001A713C
		protected virtual void WriteInlineR(ref ArrayWriter writer, Instruction instr)
		{
			if (instr.Operand is double)
			{
				writer.WriteDouble((double)instr.Operand);
				return;
			}
			this.Error("Operand is not a Double");
			writer.WriteDouble(0.0);
		}

		// Token: 0x0600568C RID: 22156
		protected abstract void WriteInlineSig(ref ArrayWriter writer, Instruction instr);

		// Token: 0x0600568D RID: 22157
		protected abstract void WriteInlineString(ref ArrayWriter writer, Instruction instr);

		// Token: 0x0600568E RID: 22158 RVA: 0x001A717C File Offset: 0x001A717C
		protected virtual void WriteInlineSwitch(ref ArrayWriter writer, Instruction instr)
		{
			IList<Instruction> list = instr.Operand as IList<Instruction>;
			if (list == null)
			{
				this.Error("switch operand is not a list of instructions");
				writer.WriteInt32(0);
				return;
			}
			uint num = (uint)((ulong)(this.ToInstructionOffset(ref writer) + 4U) + (ulong)((long)(list.Count * 4)));
			writer.WriteInt32(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				Instruction instr2 = list[i];
				writer.WriteUInt32(this.GetOffset(instr2) - num);
			}
		}

		// Token: 0x0600568F RID: 22159
		protected abstract void WriteInlineTok(ref ArrayWriter writer, Instruction instr);

		// Token: 0x06005690 RID: 22160
		protected abstract void WriteInlineType(ref ArrayWriter writer, Instruction instr);

		// Token: 0x06005691 RID: 22161 RVA: 0x001A7200 File Offset: 0x001A7200
		protected virtual void WriteInlineVar(ref ArrayWriter writer, Instruction instr)
		{
			IVariable variable = instr.Operand as IVariable;
			if (variable == null)
			{
				this.Error("Operand is not a local/arg");
				writer.WriteUInt16(0);
				return;
			}
			int index = variable.Index;
			if (0 <= index && index <= 65535)
			{
				writer.WriteUInt16((ushort)index);
				return;
			}
			this.Error("Local/arg index doesn't fit in a UInt16");
			writer.WriteUInt16(0);
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x001A726C File Offset: 0x001A726C
		protected virtual void WriteShortInlineBrTarget(ref ArrayWriter writer, Instruction instr)
		{
			int num = (int)(this.GetOffset(instr.Operand as Instruction) - (this.ToInstructionOffset(ref writer) + 1U));
			if (-128 <= num && num <= 127)
			{
				writer.WriteSByte((sbyte)num);
				return;
			}
			this.Error("Target instruction is too far away for a short branch. Use the long branch or call CilBody.SimplifyBranches() and CilBody.OptimizeBranches()");
			writer.WriteByte(0);
		}

		// Token: 0x06005693 RID: 22163 RVA: 0x001A72C4 File Offset: 0x001A72C4
		protected virtual void WriteShortInlineI(ref ArrayWriter writer, Instruction instr)
		{
			if (instr.Operand is sbyte)
			{
				writer.WriteSByte((sbyte)instr.Operand);
				return;
			}
			if (instr.Operand is byte)
			{
				writer.WriteByte((byte)instr.Operand);
				return;
			}
			this.Error("Operand is not a Byte or a SByte");
			writer.WriteByte(0);
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x001A732C File Offset: 0x001A732C
		protected virtual void WriteShortInlineR(ref ArrayWriter writer, Instruction instr)
		{
			if (instr.Operand is float)
			{
				writer.WriteSingle((float)instr.Operand);
				return;
			}
			this.Error("Operand is not a Single");
			writer.WriteSingle(0f);
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x001A7368 File Offset: 0x001A7368
		protected virtual void WriteShortInlineVar(ref ArrayWriter writer, Instruction instr)
		{
			IVariable variable = instr.Operand as IVariable;
			if (variable == null)
			{
				this.Error("Operand is not a local/arg");
				writer.WriteByte(0);
				return;
			}
			int index = variable.Index;
			if (0 <= index && index <= 255)
			{
				writer.WriteByte((byte)index);
				return;
			}
			this.Error("Local/arg index doesn't fit in a Byte. Use the longer ldloc/ldarg/stloc/starg instruction.");
			writer.WriteByte(0);
		}

		// Token: 0x04002973 RID: 10611
		protected IList<Instruction> instructions;

		// Token: 0x04002974 RID: 10612
		protected IList<ExceptionHandler> exceptionHandlers;

		// Token: 0x04002975 RID: 10613
		private readonly Dictionary<Instruction, uint> offsets = new Dictionary<Instruction, uint>();

		// Token: 0x04002976 RID: 10614
		private uint firstInstructionOffset;

		// Token: 0x04002977 RID: 10615
		private int errors;

		// Token: 0x04002978 RID: 10616
		private MaxStackCalculator maxStackCalculator = MaxStackCalculator.Create();
	}
}
