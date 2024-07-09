using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009EA RID: 2538
	[ComVisible(true)]
	public abstract class MethodBodyReaderBase
	{
		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06006161 RID: 24929 RVA: 0x001CF8A0 File Offset: 0x001CF8A0
		public IList<Parameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06006162 RID: 24930 RVA: 0x001CF8A8 File Offset: 0x001CF8A8
		public IList<Local> Locals
		{
			get
			{
				return this.locals;
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06006163 RID: 24931 RVA: 0x001CF8B0 File Offset: 0x001CF8B0
		public IList<Instruction> Instructions
		{
			get
			{
				return this.instructions;
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06006164 RID: 24932 RVA: 0x001CF8B8 File Offset: 0x001CF8B8
		public IList<ExceptionHandler> ExceptionHandlers
		{
			get
			{
				return this.exceptionHandlers;
			}
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x001CF8C0 File Offset: 0x001CF8C0
		protected MethodBodyReaderBase()
		{
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x001CF8E0 File Offset: 0x001CF8E0
		protected MethodBodyReaderBase(DataReader reader) : this(reader, null)
		{
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x001CF8EC File Offset: 0x001CF8EC
		protected MethodBodyReaderBase(DataReader reader, IList<Parameter> parameters)
		{
			this.reader = reader;
			this.parameters = parameters;
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x001CF918 File Offset: 0x001CF918
		protected void SetLocals(IList<TypeSig> newLocals)
		{
			IList<Local> list = this.locals;
			list.Clear();
			if (newLocals == null)
			{
				return;
			}
			int count = newLocals.Count;
			for (int i = 0; i < count; i++)
			{
				list.Add(new Local(newLocals[i]));
			}
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x001CF968 File Offset: 0x001CF968
		protected void SetLocals(IList<Local> newLocals)
		{
			IList<Local> list = this.locals;
			list.Clear();
			if (newLocals == null)
			{
				return;
			}
			int count = newLocals.Count;
			for (int i = 0; i < count; i++)
			{
				list.Add(new Local(newLocals[i].Type));
			}
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x001CF9BC File Offset: 0x001CF9BC
		protected void ReadInstructions(int numInstrs)
		{
			this.codeStartOffs = this.reader.Position;
			this.codeEndOffs = this.reader.Length;
			this.instructions = new List<Instruction>(numInstrs);
			this.currentOffset = 0U;
			IList<Instruction> list = this.instructions;
			int num = 0;
			while (num < numInstrs && this.reader.Position < this.codeEndOffs)
			{
				list.Add(this.ReadOneInstruction());
				num++;
			}
			this.FixBranches();
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x001CFA40 File Offset: 0x001CFA40
		protected void ReadInstructionsNumBytes(uint codeSize)
		{
			this.codeStartOffs = this.reader.Position;
			this.codeEndOffs = this.reader.Position + codeSize;
			if (this.codeEndOffs < this.codeStartOffs || this.codeEndOffs > this.reader.Length)
			{
				throw new InvalidMethodException("Invalid code size");
			}
			this.instructions = new List<Instruction>();
			this.currentOffset = 0U;
			IList<Instruction> list = this.instructions;
			while (this.reader.Position < this.codeEndOffs)
			{
				list.Add(this.ReadOneInstruction());
			}
			this.reader.Position = this.codeEndOffs;
			this.FixBranches();
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x001CFAFC File Offset: 0x001CFAFC
		private void FixBranches()
		{
			IList<Instruction> list = this.instructions;
			int count = list.Count;
			int i = 0;
			while (i < count)
			{
				Instruction instruction = list[i];
				OperandType operandType = instruction.OpCode.OperandType;
				if (operandType == OperandType.InlineBrTarget)
				{
					goto IL_43;
				}
				if (operandType != OperandType.InlineSwitch)
				{
					if (operandType == OperandType.ShortInlineBrTarget)
					{
						goto IL_43;
					}
				}
				else
				{
					IList<uint> list2 = (IList<uint>)instruction.Operand;
					Instruction[] array = new Instruction[list2.Count];
					for (int j = 0; j < list2.Count; j++)
					{
						array[j] = this.GetInstruction(list2[j]);
					}
					instruction.Operand = array;
				}
				IL_B3:
				i++;
				continue;
				IL_43:
				instruction.Operand = this.GetInstruction((uint)instruction.Operand);
				goto IL_B3;
			}
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x001CFBCC File Offset: 0x001CFBCC
		protected Instruction GetInstruction(uint offset)
		{
			IList<Instruction> list = this.instructions;
			int num = 0;
			int num2 = list.Count - 1;
			while (num <= num2 && num2 != -1)
			{
				int num3 = (num + num2) / 2;
				Instruction instruction = list[num3];
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

		// Token: 0x0600616E RID: 24942 RVA: 0x001CFC3C File Offset: 0x001CFC3C
		protected Instruction GetInstructionThrow(uint offset)
		{
			Instruction instruction = this.GetInstruction(offset);
			if (instruction != null)
			{
				return instruction;
			}
			throw new InvalidOperationException(string.Format("There's no instruction @ {0:X4}", offset));
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x001CFC74 File Offset: 0x001CFC74
		private Instruction ReadOneInstruction()
		{
			Instruction instruction = new Instruction();
			instruction.Offset = this.currentOffset;
			instruction.OpCode = this.ReadOpCode();
			instruction.Operand = this.ReadOperand(instruction);
			if (instruction.OpCode.Code == Code.Switch)
			{
				IList<uint> list = (IList<uint>)instruction.Operand;
				this.currentOffset += (uint)(instruction.OpCode.Size + 4 + 4 * list.Count);
			}
			else
			{
				this.currentOffset += (uint)instruction.GetSize();
			}
			if (this.currentOffset < instruction.Offset)
			{
				this.reader.Position = this.codeEndOffs;
			}
			return instruction;
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x001CFD2C File Offset: 0x001CFD2C
		private OpCode ReadOpCode()
		{
			byte b = this.reader.ReadByte();
			if (b != 254)
			{
				return OpCodes.OneByteOpCodes[(int)b];
			}
			return OpCodes.TwoByteOpCodes[(int)this.reader.ReadByte()];
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x001CFD78 File Offset: 0x001CFD78
		private object ReadOperand(Instruction instr)
		{
			switch (instr.OpCode.OperandType)
			{
			case OperandType.InlineBrTarget:
				return this.ReadInlineBrTarget(instr);
			case OperandType.InlineField:
				return this.ReadInlineField(instr);
			case OperandType.InlineI:
				return this.ReadInlineI(instr);
			case OperandType.InlineI8:
				return this.ReadInlineI8(instr);
			case OperandType.InlineMethod:
				return this.ReadInlineMethod(instr);
			case OperandType.InlineNone:
				return this.ReadInlineNone(instr);
			case OperandType.InlinePhi:
				return this.ReadInlinePhi(instr);
			case OperandType.InlineR:
				return this.ReadInlineR(instr);
			case OperandType.InlineSig:
				return this.ReadInlineSig(instr);
			case OperandType.InlineString:
				return this.ReadInlineString(instr);
			case OperandType.InlineSwitch:
				return this.ReadInlineSwitch(instr);
			case OperandType.InlineTok:
				return this.ReadInlineTok(instr);
			case OperandType.InlineType:
				return this.ReadInlineType(instr);
			case OperandType.InlineVar:
				return this.ReadInlineVar(instr);
			case OperandType.ShortInlineBrTarget:
				return this.ReadShortInlineBrTarget(instr);
			case OperandType.ShortInlineI:
				return this.ReadShortInlineI(instr);
			case OperandType.ShortInlineR:
				return this.ReadShortInlineR(instr);
			case OperandType.ShortInlineVar:
				return this.ReadShortInlineVar(instr);
			}
			throw new InvalidOperationException("Invalid OpCode.OperandType");
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x001CFF00 File Offset: 0x001CFF00
		protected virtual uint ReadInlineBrTarget(Instruction instr)
		{
			return instr.Offset + (uint)instr.GetSize() + this.reader.ReadUInt32();
		}

		// Token: 0x06006173 RID: 24947
		protected abstract IField ReadInlineField(Instruction instr);

		// Token: 0x06006174 RID: 24948 RVA: 0x001CFF1C File Offset: 0x001CFF1C
		protected virtual int ReadInlineI(Instruction instr)
		{
			return this.reader.ReadInt32();
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x001CFF2C File Offset: 0x001CFF2C
		protected virtual long ReadInlineI8(Instruction instr)
		{
			return this.reader.ReadInt64();
		}

		// Token: 0x06006176 RID: 24950
		protected abstract IMethod ReadInlineMethod(Instruction instr);

		// Token: 0x06006177 RID: 24951 RVA: 0x001CFF3C File Offset: 0x001CFF3C
		protected virtual object ReadInlineNone(Instruction instr)
		{
			return null;
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x001CFF40 File Offset: 0x001CFF40
		protected virtual object ReadInlinePhi(Instruction instr)
		{
			return null;
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x001CFF44 File Offset: 0x001CFF44
		protected virtual double ReadInlineR(Instruction instr)
		{
			return this.reader.ReadDouble();
		}

		// Token: 0x0600617A RID: 24954
		protected abstract MethodSig ReadInlineSig(Instruction instr);

		// Token: 0x0600617B RID: 24955
		protected abstract string ReadInlineString(Instruction instr);

		// Token: 0x0600617C RID: 24956 RVA: 0x001CFF54 File Offset: 0x001CFF54
		protected virtual IList<uint> ReadInlineSwitch(Instruction instr)
		{
			uint num = this.reader.ReadUInt32();
			long num2 = (long)((ulong)instr.Offset + (ulong)((long)instr.OpCode.Size) + 4UL + (ulong)num * 4UL);
			if (num2 > (long)((ulong)-1) || (ulong)this.codeStartOffs + (ulong)num2 > (ulong)this.codeEndOffs)
			{
				this.reader.Position = this.codeEndOffs;
				return Array2.Empty<uint>();
			}
			uint[] array = new uint[num];
			uint num3 = (uint)num2;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = num3 + this.reader.ReadUInt32();
			}
			return array;
		}

		// Token: 0x0600617D RID: 24957
		protected abstract ITokenOperand ReadInlineTok(Instruction instr);

		// Token: 0x0600617E RID: 24958
		protected abstract ITypeDefOrRef ReadInlineType(Instruction instr);

		// Token: 0x0600617F RID: 24959 RVA: 0x001CFFF4 File Offset: 0x001CFFF4
		protected virtual IVariable ReadInlineVar(Instruction instr)
		{
			if (MethodBodyReaderBase.IsArgOperandInstruction(instr))
			{
				return this.ReadInlineVarArg(instr);
			}
			return this.ReadInlineVarLocal(instr);
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x001D0010 File Offset: 0x001D0010
		protected virtual Parameter ReadInlineVarArg(Instruction instr)
		{
			return this.GetParameter((int)this.reader.ReadUInt16());
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x001D0024 File Offset: 0x001D0024
		protected virtual Local ReadInlineVarLocal(Instruction instr)
		{
			return this.GetLocal((int)this.reader.ReadUInt16());
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x001D0038 File Offset: 0x001D0038
		protected virtual uint ReadShortInlineBrTarget(Instruction instr)
		{
			return instr.Offset + (uint)instr.GetSize() + (uint)this.reader.ReadSByte();
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x001D0054 File Offset: 0x001D0054
		protected virtual object ReadShortInlineI(Instruction instr)
		{
			if (instr.OpCode.Code == Code.Ldc_I4_S)
			{
				return this.reader.ReadSByte();
			}
			return this.reader.ReadByte();
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x001D008C File Offset: 0x001D008C
		protected virtual float ReadShortInlineR(Instruction instr)
		{
			return this.reader.ReadSingle();
		}

		// Token: 0x06006185 RID: 24965 RVA: 0x001D009C File Offset: 0x001D009C
		protected virtual IVariable ReadShortInlineVar(Instruction instr)
		{
			if (MethodBodyReaderBase.IsArgOperandInstruction(instr))
			{
				return this.ReadShortInlineVarArg(instr);
			}
			return this.ReadShortInlineVarLocal(instr);
		}

		// Token: 0x06006186 RID: 24966 RVA: 0x001D00B8 File Offset: 0x001D00B8
		protected virtual Parameter ReadShortInlineVarArg(Instruction instr)
		{
			return this.GetParameter((int)this.reader.ReadByte());
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x001D00CC File Offset: 0x001D00CC
		protected virtual Local ReadShortInlineVarLocal(Instruction instr)
		{
			return this.GetLocal((int)this.reader.ReadByte());
		}

		// Token: 0x06006188 RID: 24968 RVA: 0x001D00E0 File Offset: 0x001D00E0
		protected static bool IsArgOperandInstruction(Instruction instr)
		{
			Code code = instr.OpCode.Code;
			return code - Code.Ldarg_S <= 2 || code - Code.Ldarg <= 2;
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x001D0118 File Offset: 0x001D0118
		protected Parameter GetParameter(int index)
		{
			IList<Parameter> list = this.parameters;
			if (index < list.Count)
			{
				return list[index];
			}
			return null;
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x001D0148 File Offset: 0x001D0148
		protected Local GetLocal(int index)
		{
			IList<Local> list = this.locals;
			if (index < list.Count)
			{
				return list[index];
			}
			return null;
		}

		// Token: 0x0600618B RID: 24971 RVA: 0x001D0178 File Offset: 0x001D0178
		protected bool Add(ExceptionHandler eh)
		{
			uint offset = this.GetOffset(eh.TryStart);
			uint offset2 = this.GetOffset(eh.TryEnd);
			if (offset2 <= offset)
			{
				return false;
			}
			uint offset3 = this.GetOffset(eh.HandlerStart);
			uint offset4 = this.GetOffset(eh.HandlerEnd);
			if (offset4 <= offset3)
			{
				return false;
			}
			if (eh.HandlerType == ExceptionHandlerType.Filter)
			{
				if (eh.FilterStart == null)
				{
					return false;
				}
				if (eh.FilterStart.Offset >= offset3)
				{
					return false;
				}
			}
			if (offset3 <= offset && offset < offset4)
			{
				return false;
			}
			if (offset3 < offset2 && offset2 <= offset4)
			{
				return false;
			}
			if (offset <= offset3 && offset3 < offset2)
			{
				return false;
			}
			if (offset < offset4 && offset4 <= offset2)
			{
				return false;
			}
			this.exceptionHandlers.Add(eh);
			return true;
		}

		// Token: 0x0600618C RID: 24972 RVA: 0x001D0248 File Offset: 0x001D0248
		private uint GetOffset(Instruction instr)
		{
			if (instr != null)
			{
				return instr.Offset;
			}
			IList<Instruction> list = this.instructions;
			if (list.Count == 0)
			{
				return 0U;
			}
			return list[list.Count - 1].Offset;
		}

		// Token: 0x0600618D RID: 24973 RVA: 0x001D0290 File Offset: 0x001D0290
		public virtual void RestoreMethod(MethodDef method)
		{
			CilBody body = method.Body;
			body.Variables.Clear();
			IList<Local> list = this.locals;
			if (list != null)
			{
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					body.Variables.Add(list[i]);
				}
			}
			body.Instructions.Clear();
			IList<Instruction> list2 = this.instructions;
			if (list2 != null)
			{
				int count2 = list2.Count;
				for (int j = 0; j < count2; j++)
				{
					body.Instructions.Add(list2[j]);
				}
			}
			body.ExceptionHandlers.Clear();
			IList<ExceptionHandler> list3 = this.exceptionHandlers;
			if (list3 != null)
			{
				int count3 = list3.Count;
				for (int k = 0; k < count3; k++)
				{
					body.ExceptionHandlers.Add(list3[k]);
				}
			}
		}

		// Token: 0x040030BC RID: 12476
		protected DataReader reader;

		// Token: 0x040030BD RID: 12477
		protected IList<Parameter> parameters;

		// Token: 0x040030BE RID: 12478
		protected IList<Local> locals = new List<Local>();

		// Token: 0x040030BF RID: 12479
		protected IList<Instruction> instructions;

		// Token: 0x040030C0 RID: 12480
		protected IList<ExceptionHandler> exceptionHandlers = new List<ExceptionHandler>();

		// Token: 0x040030C1 RID: 12481
		private uint currentOffset;

		// Token: 0x040030C2 RID: 12482
		protected uint codeEndOffs;

		// Token: 0x040030C3 RID: 12483
		protected uint codeStartOffs;
	}
}
