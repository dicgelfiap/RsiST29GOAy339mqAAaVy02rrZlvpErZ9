using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008BC RID: 2236
	[ComVisible(true)]
	public sealed class MethodBodyWriter : MethodBodyWriterBase
	{
		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x0600565B RID: 22107 RVA: 0x001A63A8 File Offset: 0x001A63A8
		public byte[] Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x0600565C RID: 22108 RVA: 0x001A63B0 File Offset: 0x001A63B0
		public byte[] ExtraSections
		{
			get
			{
				return this.extraSections;
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x0600565D RID: 22109 RVA: 0x001A63B8 File Offset: 0x001A63B8
		public uint LocalVarSigTok
		{
			get
			{
				return this.localVarSigTok;
			}
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x001A63C0 File Offset: 0x001A63C0
		public MethodBodyWriter(ITokenProvider helper, CilBody cilBody) : this(helper, cilBody, false)
		{
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x001A63CC File Offset: 0x001A63CC
		public MethodBodyWriter(ITokenProvider helper, CilBody cilBody, bool keepMaxStack) : base(cilBody.Instructions, cilBody.ExceptionHandlers)
		{
			this.helper = helper;
			this.cilBody = cilBody;
			this.keepMaxStack = keepMaxStack;
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x001A63F8 File Offset: 0x001A63F8
		internal MethodBodyWriter(ITokenProvider helper)
		{
			this.helper = helper;
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x001A6408 File Offset: 0x001A6408
		internal void Reset(CilBody cilBody, bool keepMaxStack)
		{
			base.Reset(cilBody.Instructions, cilBody.ExceptionHandlers);
			this.cilBody = cilBody;
			this.keepMaxStack = keepMaxStack;
			this.codeSize = 0U;
			this.maxStack = 0U;
			this.code = null;
			this.extraSections = null;
			this.localVarSigTok = 0U;
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x001A645C File Offset: 0x001A645C
		public void Write()
		{
			this.codeSize = base.InitializeInstructionOffsets();
			this.maxStack = (this.keepMaxStack ? ((uint)this.cilBody.MaxStack) : base.GetMaxStack());
			if (this.NeedFatHeader())
			{
				this.WriteFatHeader();
			}
			else
			{
				this.WriteTinyHeader();
			}
			if (this.exceptionHandlers.Count > 0)
			{
				this.WriteExceptionHandlers();
			}
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x001A64D4 File Offset: 0x001A64D4
		public byte[] GetFullMethodBody()
		{
			int num = Utils.AlignUp(this.code.Length, 4U) - this.code.Length;
			byte[] array = new byte[this.code.Length + ((this.extraSections == null) ? 0 : (num + this.extraSections.Length))];
			Array.Copy(this.code, 0, array, 0, this.code.Length);
			if (this.extraSections != null)
			{
				Array.Copy(this.extraSections, 0, array, this.code.Length + num, this.extraSections.Length);
			}
			return array;
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x001A6568 File Offset: 0x001A6568
		private bool NeedFatHeader()
		{
			return this.codeSize > 63U || this.exceptionHandlers.Count > 0 || this.cilBody.HasVariables || this.maxStack > 8U;
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x001A65A4 File Offset: 0x001A65A4
		private void WriteFatHeader()
		{
			if (this.maxStack > 65535U)
			{
				base.Error("MaxStack is too big");
				this.maxStack = 65535U;
			}
			ushort num = 12291;
			if (this.exceptionHandlers.Count > 0)
			{
				num |= 8;
			}
			if (this.cilBody.InitLocals)
			{
				num |= 16;
			}
			this.code = new byte[12U + this.codeSize];
			ArrayWriter arrayWriter = new ArrayWriter(this.code);
			arrayWriter.WriteUInt16(num);
			arrayWriter.WriteUInt16((ushort)this.maxStack);
			arrayWriter.WriteUInt32(this.codeSize);
			arrayWriter.WriteUInt32(this.localVarSigTok = this.helper.GetToken(this.GetLocals(), this.cilBody.LocalVarSigTok).Raw);
			if (base.WriteInstructions(ref arrayWriter) != this.codeSize)
			{
				base.Error("Didn't write all code bytes");
			}
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x001A66A4 File Offset: 0x001A66A4
		private IList<TypeSig> GetLocals()
		{
			TypeSig[] array = new TypeSig[this.cilBody.Variables.Count];
			for (int i = 0; i < this.cilBody.Variables.Count; i++)
			{
				array[i] = this.cilBody.Variables[i].Type;
			}
			return array;
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x001A6708 File Offset: 0x001A6708
		private void WriteTinyHeader()
		{
			this.localVarSigTok = 0U;
			this.code = new byte[1U + this.codeSize];
			ArrayWriter arrayWriter = new ArrayWriter(this.code);
			arrayWriter.WriteByte((byte)(this.codeSize << 2 | 2U));
			if (base.WriteInstructions(ref arrayWriter) != this.codeSize)
			{
				base.Error("Didn't write all code bytes");
			}
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x001A6770 File Offset: 0x001A6770
		private void WriteExceptionHandlers()
		{
			if (this.NeedFatExceptionClauses())
			{
				this.extraSections = this.WriteFatExceptionClauses();
				return;
			}
			this.extraSections = this.WriteSmallExceptionClauses();
		}

		// Token: 0x06005669 RID: 22121 RVA: 0x001A6798 File Offset: 0x001A6798
		private bool NeedFatExceptionClauses()
		{
			IList<ExceptionHandler> exceptionHandlers = this.exceptionHandlers;
			if (exceptionHandlers.Count > 20)
			{
				return true;
			}
			for (int i = 0; i < exceptionHandlers.Count; i++)
			{
				ExceptionHandler exceptionHandler = exceptionHandlers[i];
				if (!this.FitsInSmallExceptionClause(exceptionHandler.TryStart, exceptionHandler.TryEnd))
				{
					return true;
				}
				if (!this.FitsInSmallExceptionClause(exceptionHandler.HandlerStart, exceptionHandler.HandlerEnd))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x001A6810 File Offset: 0x001A6810
		private bool FitsInSmallExceptionClause(Instruction start, Instruction end)
		{
			uint offset = this.GetOffset2(start);
			uint offset2 = this.GetOffset2(end);
			return offset2 >= offset && offset <= 65535U && offset2 - offset <= 255U;
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x001A6854 File Offset: 0x001A6854
		private uint GetOffset2(Instruction instr)
		{
			if (instr == null)
			{
				return this.codeSize;
			}
			return base.GetOffset(instr);
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x001A686C File Offset: 0x001A686C
		private byte[] WriteFatExceptionClauses()
		{
			IList<ExceptionHandler> exceptionHandlers = this.exceptionHandlers;
			int num = exceptionHandlers.Count;
			if (num > 699050)
			{
				base.Error("Too many exception handlers");
				num = 699050;
			}
			byte[] array = new byte[num * 24 + 4];
			ArrayWriter arrayWriter = new ArrayWriter(array);
			arrayWriter.WriteUInt32((uint)(num * 24 + 4 << 8 | 65));
			for (int i = 0; i < num; i++)
			{
				ExceptionHandler exceptionHandler = exceptionHandlers[i];
				arrayWriter.WriteUInt32((uint)exceptionHandler.HandlerType);
				uint offset = this.GetOffset2(exceptionHandler.TryStart);
				uint offset2 = this.GetOffset2(exceptionHandler.TryEnd);
				if (offset2 <= offset)
				{
					base.Error("Exception handler: TryEnd <= TryStart");
				}
				arrayWriter.WriteUInt32(offset);
				arrayWriter.WriteUInt32(offset2 - offset);
				offset = this.GetOffset2(exceptionHandler.HandlerStart);
				offset2 = this.GetOffset2(exceptionHandler.HandlerEnd);
				if (offset2 <= offset)
				{
					base.Error("Exception handler: HandlerEnd <= HandlerStart");
				}
				arrayWriter.WriteUInt32(offset);
				arrayWriter.WriteUInt32(offset2 - offset);
				if (exceptionHandler.HandlerType == ExceptionHandlerType.Catch)
				{
					arrayWriter.WriteUInt32(this.helper.GetToken(exceptionHandler.CatchType).Raw);
				}
				else if (exceptionHandler.HandlerType == ExceptionHandlerType.Filter)
				{
					arrayWriter.WriteUInt32(this.GetOffset2(exceptionHandler.FilterStart));
				}
				else
				{
					arrayWriter.WriteInt32(0);
				}
			}
			if (arrayWriter.Position != array.Length)
			{
				throw new InvalidOperationException();
			}
			return array;
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x001A6A00 File Offset: 0x001A6A00
		private byte[] WriteSmallExceptionClauses()
		{
			IList<ExceptionHandler> exceptionHandlers = this.exceptionHandlers;
			int num = exceptionHandlers.Count;
			if (num > 20)
			{
				base.Error("Too many exception handlers");
				num = 20;
			}
			byte[] array = new byte[num * 12 + 4];
			ArrayWriter arrayWriter = new ArrayWriter(array);
			arrayWriter.WriteUInt32((uint)(num * 12 + 4 << 8 | 1));
			for (int i = 0; i < num; i++)
			{
				ExceptionHandler exceptionHandler = exceptionHandlers[i];
				arrayWriter.WriteUInt16((ushort)exceptionHandler.HandlerType);
				uint offset = this.GetOffset2(exceptionHandler.TryStart);
				uint offset2 = this.GetOffset2(exceptionHandler.TryEnd);
				if (offset2 <= offset)
				{
					base.Error("Exception handler: TryEnd <= TryStart");
				}
				arrayWriter.WriteUInt16((ushort)offset);
				arrayWriter.WriteByte((byte)(offset2 - offset));
				offset = this.GetOffset2(exceptionHandler.HandlerStart);
				offset2 = this.GetOffset2(exceptionHandler.HandlerEnd);
				if (offset2 <= offset)
				{
					base.Error("Exception handler: HandlerEnd <= HandlerStart");
				}
				arrayWriter.WriteUInt16((ushort)offset);
				arrayWriter.WriteByte((byte)(offset2 - offset));
				if (exceptionHandler.HandlerType == ExceptionHandlerType.Catch)
				{
					arrayWriter.WriteUInt32(this.helper.GetToken(exceptionHandler.CatchType).Raw);
				}
				else if (exceptionHandler.HandlerType == ExceptionHandlerType.Filter)
				{
					arrayWriter.WriteUInt32(this.GetOffset2(exceptionHandler.FilterStart));
				}
				else
				{
					arrayWriter.WriteInt32(0);
				}
			}
			if (arrayWriter.Position != array.Length)
			{
				throw new InvalidOperationException();
			}
			return array;
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x001A6B94 File Offset: 0x001A6B94
		protected override void ErrorImpl(string message)
		{
			this.helper.Error(message);
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x001A6BA4 File Offset: 0x001A6BA4
		protected override void WriteInlineField(ref ArrayWriter writer, Instruction instr)
		{
			writer.WriteUInt32(this.helper.GetToken(instr.Operand).Raw);
		}

		// Token: 0x06005670 RID: 22128 RVA: 0x001A6BD4 File Offset: 0x001A6BD4
		protected override void WriteInlineMethod(ref ArrayWriter writer, Instruction instr)
		{
			writer.WriteUInt32(this.helper.GetToken(instr.Operand).Raw);
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x001A6C04 File Offset: 0x001A6C04
		protected override void WriteInlineSig(ref ArrayWriter writer, Instruction instr)
		{
			writer.WriteUInt32(this.helper.GetToken(instr.Operand).Raw);
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x001A6C34 File Offset: 0x001A6C34
		protected override void WriteInlineString(ref ArrayWriter writer, Instruction instr)
		{
			writer.WriteUInt32(this.helper.GetToken(instr.Operand).Raw);
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x001A6C64 File Offset: 0x001A6C64
		protected override void WriteInlineTok(ref ArrayWriter writer, Instruction instr)
		{
			writer.WriteUInt32(this.helper.GetToken(instr.Operand).Raw);
		}

		// Token: 0x06005674 RID: 22132 RVA: 0x001A6C94 File Offset: 0x001A6C94
		protected override void WriteInlineType(ref ArrayWriter writer, Instruction instr)
		{
			writer.WriteUInt32(this.helper.GetToken(instr.Operand).Raw);
		}

		// Token: 0x0400296B RID: 10603
		private readonly ITokenProvider helper;

		// Token: 0x0400296C RID: 10604
		private CilBody cilBody;

		// Token: 0x0400296D RID: 10605
		private bool keepMaxStack;

		// Token: 0x0400296E RID: 10606
		private uint codeSize;

		// Token: 0x0400296F RID: 10607
		private uint maxStack;

		// Token: 0x04002970 RID: 10608
		private byte[] code;

		// Token: 0x04002971 RID: 10609
		private byte[] extraSections;

		// Token: 0x04002972 RID: 10610
		private uint localVarSigTok;
	}
}
