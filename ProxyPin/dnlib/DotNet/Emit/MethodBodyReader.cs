using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009E9 RID: 2537
	[ComVisible(true)]
	public sealed class MethodBodyReader : MethodBodyReaderBase
	{
		// Token: 0x06006141 RID: 24897 RVA: 0x001CEE5C File Offset: 0x001CEE5C
		public static CilBody CreateCilBody(IInstructionOperandResolver opResolver, DataReader reader, MethodDef method)
		{
			return MethodBodyReader.CreateCilBody(opResolver, reader, null, method.Parameters, default(GenericParamContext));
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x001CEE8C File Offset: 0x001CEE8C
		public static CilBody CreateCilBody(IInstructionOperandResolver opResolver, DataReader reader, MethodDef method, GenericParamContext gpContext)
		{
			return MethodBodyReader.CreateCilBody(opResolver, reader, null, method.Parameters, gpContext);
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x001CEEB4 File Offset: 0x001CEEB4
		public static CilBody CreateCilBody(IInstructionOperandResolver opResolver, DataReader reader, IList<Parameter> parameters)
		{
			return MethodBodyReader.CreateCilBody(opResolver, reader, null, parameters, default(GenericParamContext));
		}

		// Token: 0x06006144 RID: 24900 RVA: 0x001CEEE0 File Offset: 0x001CEEE0
		public static CilBody CreateCilBody(IInstructionOperandResolver opResolver, DataReader reader, IList<Parameter> parameters, GenericParamContext gpContext)
		{
			return MethodBodyReader.CreateCilBody(opResolver, reader, null, parameters, gpContext);
		}

		// Token: 0x06006145 RID: 24901 RVA: 0x001CEF04 File Offset: 0x001CEF04
		public static CilBody CreateCilBody(IInstructionOperandResolver opResolver, byte[] code, byte[] exceptions, IList<Parameter> parameters)
		{
			return MethodBodyReader.CreateCilBody(opResolver, ByteArrayDataReaderFactory.CreateReader(code), (exceptions == null) ? null : new DataReader?(ByteArrayDataReaderFactory.CreateReader(exceptions)), parameters, default(GenericParamContext));
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x001CEF4C File Offset: 0x001CEF4C
		public static CilBody CreateCilBody(IInstructionOperandResolver opResolver, byte[] code, byte[] exceptions, IList<Parameter> parameters, GenericParamContext gpContext)
		{
			return MethodBodyReader.CreateCilBody(opResolver, ByteArrayDataReaderFactory.CreateReader(code), (exceptions == null) ? null : new DataReader?(ByteArrayDataReaderFactory.CreateReader(exceptions)), parameters, gpContext);
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x001CEF8C File Offset: 0x001CEF8C
		public static CilBody CreateCilBody(IInstructionOperandResolver opResolver, DataReader codeReader, DataReader? ehReader, IList<Parameter> parameters)
		{
			return MethodBodyReader.CreateCilBody(opResolver, codeReader, ehReader, parameters, default(GenericParamContext));
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x001CEFB0 File Offset: 0x001CEFB0
		public static CilBody CreateCilBody(IInstructionOperandResolver opResolver, DataReader codeReader, DataReader? ehReader, IList<Parameter> parameters, GenericParamContext gpContext)
		{
			MethodBodyReader methodBodyReader = new MethodBodyReader(opResolver, codeReader, ehReader, parameters, gpContext);
			if (!methodBodyReader.Read())
			{
				return new CilBody();
			}
			return methodBodyReader.CreateCilBody();
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x001CEFE4 File Offset: 0x001CEFE4
		public static CilBody CreateCilBody(IInstructionOperandResolver opResolver, byte[] code, byte[] exceptions, IList<Parameter> parameters, ushort flags, ushort maxStack, uint codeSize, uint localVarSigTok)
		{
			return MethodBodyReader.CreateCilBody(opResolver, code, exceptions, parameters, flags, maxStack, codeSize, localVarSigTok, default(GenericParamContext));
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x001CF010 File Offset: 0x001CF010
		public static CilBody CreateCilBody(IInstructionOperandResolver opResolver, byte[] code, byte[] exceptions, IList<Parameter> parameters, ushort flags, ushort maxStack, uint codeSize, uint localVarSigTok, GenericParamContext gpContext)
		{
			DataReader codeReader = ByteArrayDataReaderFactory.CreateReader(code);
			DataReader? ehReader = (exceptions == null) ? null : new DataReader?(ByteArrayDataReaderFactory.CreateReader(exceptions));
			MethodBodyReader methodBodyReader = new MethodBodyReader(opResolver, codeReader, ehReader, parameters, gpContext);
			methodBodyReader.SetHeader(flags, maxStack, codeSize, localVarSigTok);
			if (!methodBodyReader.Read())
			{
				return new CilBody();
			}
			return methodBodyReader.CreateCilBody();
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x001CF07C File Offset: 0x001CF07C
		public MethodBodyReader(IInstructionOperandResolver opResolver, DataReader reader, MethodDef method) : this(opResolver, reader, null, method.Parameters, default(GenericParamContext))
		{
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x001CF0B0 File Offset: 0x001CF0B0
		public MethodBodyReader(IInstructionOperandResolver opResolver, DataReader reader, MethodDef method, GenericParamContext gpContext) : this(opResolver, reader, null, method.Parameters, gpContext)
		{
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x001CF0DC File Offset: 0x001CF0DC
		public MethodBodyReader(IInstructionOperandResolver opResolver, DataReader reader, IList<Parameter> parameters) : this(opResolver, reader, null, parameters, default(GenericParamContext))
		{
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x001CF108 File Offset: 0x001CF108
		public MethodBodyReader(IInstructionOperandResolver opResolver, DataReader reader, IList<Parameter> parameters, GenericParamContext gpContext) : this(opResolver, reader, null, parameters, gpContext)
		{
		}

		// Token: 0x0600614F RID: 24911 RVA: 0x001CF130 File Offset: 0x001CF130
		public MethodBodyReader(IInstructionOperandResolver opResolver, DataReader codeReader, DataReader? ehReader, IList<Parameter> parameters) : this(opResolver, codeReader, ehReader, parameters, default(GenericParamContext))
		{
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x001CF158 File Offset: 0x001CF158
		public MethodBodyReader(IInstructionOperandResolver opResolver, DataReader codeReader, DataReader? ehReader, IList<Parameter> parameters, GenericParamContext gpContext) : base(codeReader, parameters)
		{
			this.opResolver = opResolver;
			this.exceptionsReader = ehReader;
			this.gpContext = gpContext;
			this.startOfHeader = uint.MaxValue;
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x001CF180 File Offset: 0x001CF180
		private void SetHeader(ushort flags, ushort maxStack, uint codeSize, uint localVarSigTok)
		{
			this.hasReadHeader = true;
			this.flags = flags;
			this.maxStack = maxStack;
			this.codeSize = codeSize;
			this.localVarSigTok = localVarSigTok;
		}

		// Token: 0x06006152 RID: 24914 RVA: 0x001CF1A8 File Offset: 0x001CF1A8
		public bool Read()
		{
			bool result;
			try
			{
				if (!this.ReadHeader())
				{
					result = false;
				}
				else
				{
					base.SetLocals(this.ReadLocals());
					this.ReadInstructions();
					this.ReadExceptionHandlers(out this.totalBodySize);
					result = true;
				}
			}
			catch (InvalidMethodException)
			{
				result = false;
			}
			catch (IOException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06006153 RID: 24915 RVA: 0x001CF21C File Offset: 0x001CF21C
		private bool ReadHeader()
		{
			if (this.hasReadHeader)
			{
				return true;
			}
			this.hasReadHeader = true;
			this.startOfHeader = this.reader.Position;
			byte b = this.reader.ReadByte();
			switch (b & 7)
			{
			case 2:
			case 6:
				this.flags = 2;
				this.maxStack = 8;
				this.codeSize = (uint)(b >> 2);
				this.localVarSigTok = 0U;
				this.headerSize = 1;
				goto IL_130;
			case 3:
				this.flags = (ushort)((int)this.reader.ReadByte() << 8 | (int)b);
				this.headerSize = (byte)(this.flags >> 12);
				this.maxStack = this.reader.ReadUInt16();
				this.codeSize = this.reader.ReadUInt32();
				this.localVarSigTok = this.reader.ReadUInt32();
				this.reader.Position = this.reader.Position - 12U + (uint)(this.headerSize * 4);
				if (this.headerSize < 3)
				{
					this.flags &= 65527;
				}
				this.headerSize *= 4;
				goto IL_130;
			}
			return false;
			IL_130:
			return (ulong)this.reader.Position + (ulong)this.codeSize <= (ulong)this.reader.Length;
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x001CF388 File Offset: 0x001CF388
		private IList<TypeSig> ReadLocals()
		{
			StandAloneSig standAloneSig = this.opResolver.ResolveToken(this.localVarSigTok, this.gpContext) as StandAloneSig;
			if (standAloneSig == null)
			{
				return null;
			}
			LocalSig localSig = standAloneSig.LocalSig;
			if (localSig == null)
			{
				return null;
			}
			return localSig.Locals;
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x001CF3D4 File Offset: 0x001CF3D4
		private void ReadInstructions()
		{
			base.ReadInstructionsNumBytes(this.codeSize);
		}

		// Token: 0x06006156 RID: 24918 RVA: 0x001CF3E4 File Offset: 0x001CF3E4
		protected override IField ReadInlineField(Instruction instr)
		{
			return this.opResolver.ResolveToken(this.reader.ReadUInt32(), this.gpContext) as IField;
		}

		// Token: 0x06006157 RID: 24919 RVA: 0x001CF408 File Offset: 0x001CF408
		protected override IMethod ReadInlineMethod(Instruction instr)
		{
			return this.opResolver.ResolveToken(this.reader.ReadUInt32(), this.gpContext) as IMethod;
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x001CF42C File Offset: 0x001CF42C
		protected override MethodSig ReadInlineSig(Instruction instr)
		{
			StandAloneSig standAloneSig = this.opResolver.ResolveToken(this.reader.ReadUInt32(), this.gpContext) as StandAloneSig;
			if (standAloneSig == null)
			{
				return null;
			}
			MethodSig methodSig = standAloneSig.MethodSig;
			if (methodSig != null)
			{
				methodSig.OriginalToken = standAloneSig.MDToken.Raw;
			}
			return methodSig;
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x001CF48C File Offset: 0x001CF48C
		protected override string ReadInlineString(Instruction instr)
		{
			return this.opResolver.ReadUserString(this.reader.ReadUInt32()) ?? string.Empty;
		}

		// Token: 0x0600615A RID: 24922 RVA: 0x001CF4B0 File Offset: 0x001CF4B0
		protected override ITokenOperand ReadInlineTok(Instruction instr)
		{
			return this.opResolver.ResolveToken(this.reader.ReadUInt32(), this.gpContext) as ITokenOperand;
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x001CF4D4 File Offset: 0x001CF4D4
		protected override ITypeDefOrRef ReadInlineType(Instruction instr)
		{
			return this.opResolver.ResolveToken(this.reader.ReadUInt32(), this.gpContext) as ITypeDefOrRef;
		}

		// Token: 0x0600615C RID: 24924 RVA: 0x001CF4F8 File Offset: 0x001CF4F8
		private void ReadExceptionHandlers(out uint totalBodySize)
		{
			if ((this.flags & 8) == 0)
			{
				totalBodySize = ((this.startOfHeader == uint.MaxValue) ? 0U : (this.reader.Position - this.startOfHeader));
				return;
			}
			DataReader? dataReader = this.exceptionsReader;
			bool flag;
			DataReader dataReader2;
			if (dataReader != null)
			{
				flag = false;
				dataReader2 = this.exceptionsReader.Value;
			}
			else
			{
				flag = true;
				dataReader2 = this.reader;
				dataReader2.Position = (dataReader2.Position + 3U & 4294967292U);
			}
			byte b = dataReader2.ReadByte();
			if ((b & 63) != 1)
			{
				totalBodySize = ((this.startOfHeader == uint.MaxValue) ? 0U : (this.reader.Position - this.startOfHeader));
				return;
			}
			if ((b & 64) != 0)
			{
				this.ReadFatExceptionHandlers(ref dataReader2);
			}
			else
			{
				this.ReadSmallExceptionHandlers(ref dataReader2);
			}
			if (flag)
			{
				totalBodySize = ((this.startOfHeader == uint.MaxValue) ? 0U : (dataReader2.Position - this.startOfHeader));
				return;
			}
			totalBodySize = 0U;
		}

		// Token: 0x0600615D RID: 24925 RVA: 0x001CF604 File Offset: 0x001CF604
		private static ushort GetNumberOfExceptionHandlers(uint num)
		{
			return (ushort)num;
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x001CF608 File Offset: 0x001CF608
		private void ReadFatExceptionHandlers(ref DataReader ehReader)
		{
			uint position = ehReader.Position;
			ehReader.Position = position - 1U;
			int numberOfExceptionHandlers = (int)MethodBodyReader.GetNumberOfExceptionHandlers((ehReader.ReadUInt32() >> 8) / 24U);
			for (int i = 0; i < numberOfExceptionHandlers; i++)
			{
				ExceptionHandler exceptionHandler = new ExceptionHandler((ExceptionHandlerType)ehReader.ReadUInt32());
				uint num = ehReader.ReadUInt32();
				exceptionHandler.TryStart = base.GetInstruction(num);
				exceptionHandler.TryEnd = base.GetInstruction(num + ehReader.ReadUInt32());
				num = ehReader.ReadUInt32();
				exceptionHandler.HandlerStart = base.GetInstruction(num);
				exceptionHandler.HandlerEnd = base.GetInstruction(num + ehReader.ReadUInt32());
				if (exceptionHandler.HandlerType == ExceptionHandlerType.Catch)
				{
					exceptionHandler.CatchType = (this.opResolver.ResolveToken(ehReader.ReadUInt32(), this.gpContext) as ITypeDefOrRef);
				}
				else if (exceptionHandler.HandlerType == ExceptionHandlerType.Filter)
				{
					exceptionHandler.FilterStart = base.GetInstruction(ehReader.ReadUInt32());
				}
				else
				{
					ehReader.ReadUInt32();
				}
				base.Add(exceptionHandler);
			}
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x001CF714 File Offset: 0x001CF714
		private void ReadSmallExceptionHandlers(ref DataReader ehReader)
		{
			int numberOfExceptionHandlers = (int)MethodBodyReader.GetNumberOfExceptionHandlers((uint)(ehReader.ReadByte() / 12));
			ehReader.Position += 2U;
			for (int i = 0; i < numberOfExceptionHandlers; i++)
			{
				ExceptionHandler exceptionHandler = new ExceptionHandler((ExceptionHandlerType)ehReader.ReadUInt16());
				uint num = (uint)ehReader.ReadUInt16();
				exceptionHandler.TryStart = base.GetInstruction(num);
				exceptionHandler.TryEnd = base.GetInstruction(num + (uint)ehReader.ReadByte());
				num = (uint)ehReader.ReadUInt16();
				exceptionHandler.HandlerStart = base.GetInstruction(num);
				exceptionHandler.HandlerEnd = base.GetInstruction(num + (uint)ehReader.ReadByte());
				if (exceptionHandler.HandlerType == ExceptionHandlerType.Catch)
				{
					exceptionHandler.CatchType = (this.opResolver.ResolveToken(ehReader.ReadUInt32(), this.gpContext) as ITypeDefOrRef);
				}
				else if (exceptionHandler.HandlerType == ExceptionHandlerType.Filter)
				{
					exceptionHandler.FilterStart = base.GetInstruction(ehReader.ReadUInt32());
				}
				else
				{
					ehReader.ReadUInt32();
				}
				base.Add(exceptionHandler);
			}
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x001CF814 File Offset: 0x001CF814
		public CilBody CreateCilBody()
		{
			CilBody cilBody = new CilBody(this.flags == 2 || (this.flags & 16) > 0, this.instructions, this.exceptionHandlers, this.locals);
			cilBody.HeaderSize = this.headerSize;
			cilBody.MaxStack = this.maxStack;
			cilBody.LocalVarSigTok = this.localVarSigTok;
			cilBody.MetadataBodySize = this.totalBodySize;
			this.instructions = null;
			this.exceptionHandlers = null;
			this.locals = null;
			return cilBody;
		}

		// Token: 0x040030B1 RID: 12465
		private readonly IInstructionOperandResolver opResolver;

		// Token: 0x040030B2 RID: 12466
		private bool hasReadHeader;

		// Token: 0x040030B3 RID: 12467
		private byte headerSize;

		// Token: 0x040030B4 RID: 12468
		private ushort flags;

		// Token: 0x040030B5 RID: 12469
		private ushort maxStack;

		// Token: 0x040030B6 RID: 12470
		private uint codeSize;

		// Token: 0x040030B7 RID: 12471
		private uint localVarSigTok;

		// Token: 0x040030B8 RID: 12472
		private uint startOfHeader;

		// Token: 0x040030B9 RID: 12473
		private uint totalBodySize;

		// Token: 0x040030BA RID: 12474
		private DataReader? exceptionsReader;

		// Token: 0x040030BB RID: 12475
		private readonly GenericParamContext gpContext;
	}
}
