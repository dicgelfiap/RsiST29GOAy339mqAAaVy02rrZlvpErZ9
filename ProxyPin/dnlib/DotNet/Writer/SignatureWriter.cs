using System;
using System.IO;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008D9 RID: 2265
	[ComVisible(true)]
	public struct SignatureWriter : IDisposable
	{
		// Token: 0x06005817 RID: 22551 RVA: 0x001B07EC File Offset: 0x001B07EC
		public static byte[] Write(ISignatureWriterHelper helper, TypeSig typeSig)
		{
			byte[] result;
			using (SignatureWriter signatureWriter = new SignatureWriter(helper))
			{
				signatureWriter.Write(typeSig);
				result = signatureWriter.GetResult();
			}
			return result;
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x001B0838 File Offset: 0x001B0838
		internal static byte[] Write(ISignatureWriterHelper helper, TypeSig typeSig, DataWriterContext context)
		{
			byte[] result;
			using (SignatureWriter signatureWriter = new SignatureWriter(helper, context))
			{
				signatureWriter.Write(typeSig);
				result = signatureWriter.GetResult();
			}
			return result;
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x001B0884 File Offset: 0x001B0884
		public static byte[] Write(ISignatureWriterHelper helper, CallingConventionSig sig)
		{
			byte[] result;
			using (SignatureWriter signatureWriter = new SignatureWriter(helper))
			{
				signatureWriter.Write(sig);
				result = signatureWriter.GetResult();
			}
			return result;
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x001B08D0 File Offset: 0x001B08D0
		internal static byte[] Write(ISignatureWriterHelper helper, CallingConventionSig sig, DataWriterContext context)
		{
			byte[] result;
			using (SignatureWriter signatureWriter = new SignatureWriter(helper, context))
			{
				signatureWriter.Write(sig);
				result = signatureWriter.GetResult();
			}
			return result;
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x001B091C File Offset: 0x001B091C
		private SignatureWriter(ISignatureWriterHelper helper)
		{
			this.helper = helper;
			this.recursionCounter = default(RecursionCounter);
			this.outStream = new MemoryStream();
			this.writer = new DataWriter(this.outStream);
			this.disposeStream = true;
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x001B0954 File Offset: 0x001B0954
		private SignatureWriter(ISignatureWriterHelper helper, DataWriterContext context)
		{
			this.helper = helper;
			this.recursionCounter = default(RecursionCounter);
			this.outStream = context.OutStream;
			this.writer = context.Writer;
			this.disposeStream = false;
			this.outStream.SetLength(0L);
			this.outStream.Position = 0L;
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x001B09B4 File Offset: 0x001B09B4
		private byte[] GetResult()
		{
			return this.outStream.ToArray();
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x001B09C4 File Offset: 0x001B09C4
		private uint WriteCompressedUInt32(uint value)
		{
			return this.writer.WriteCompressedUInt32(this.helper, value);
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x001B09D8 File Offset: 0x001B09D8
		private int WriteCompressedInt32(int value)
		{
			return this.writer.WriteCompressedInt32(this.helper, value);
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x001B09EC File Offset: 0x001B09EC
		private void Write(TypeSig typeSig)
		{
			if (typeSig == null)
			{
				this.helper.Error("TypeSig is null");
				this.writer.WriteByte(2);
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.helper.Error("Infinite recursion");
				this.writer.WriteByte(2);
				return;
			}
			switch (typeSig.ElementType)
			{
			case ElementType.Void:
			case ElementType.Boolean:
			case ElementType.Char:
			case ElementType.I1:
			case ElementType.U1:
			case ElementType.I2:
			case ElementType.U2:
			case ElementType.I4:
			case ElementType.U4:
			case ElementType.I8:
			case ElementType.U8:
			case ElementType.R4:
			case ElementType.R8:
			case ElementType.String:
			case ElementType.TypedByRef:
			case ElementType.I:
			case ElementType.U:
			case ElementType.Object:
			case ElementType.Sentinel:
				this.writer.WriteByte((byte)typeSig.ElementType);
				goto IL_3F2;
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.SZArray:
			case ElementType.Pinned:
				this.writer.WriteByte((byte)typeSig.ElementType);
				this.Write(typeSig.Next);
				goto IL_3F2;
			case ElementType.ValueType:
			case ElementType.Class:
				this.writer.WriteByte((byte)typeSig.ElementType);
				this.Write(((TypeDefOrRefSig)typeSig).TypeDefOrRef);
				goto IL_3F2;
			case ElementType.Var:
			case ElementType.MVar:
				this.writer.WriteByte((byte)typeSig.ElementType);
				this.WriteCompressedUInt32(((GenericSig)typeSig).Number);
				goto IL_3F2;
			case ElementType.Array:
			{
				this.writer.WriteByte((byte)typeSig.ElementType);
				ArraySig arraySig = (ArraySig)typeSig;
				this.Write(arraySig.Next);
				this.WriteCompressedUInt32(arraySig.Rank);
				if (arraySig.Rank != 0U)
				{
					uint num = this.WriteCompressedUInt32((uint)arraySig.Sizes.Count);
					for (uint num2 = 0U; num2 < num; num2 += 1U)
					{
						this.WriteCompressedUInt32(arraySig.Sizes[(int)num2]);
					}
					num = this.WriteCompressedUInt32((uint)arraySig.LowerBounds.Count);
					for (uint num3 = 0U; num3 < num; num3 += 1U)
					{
						this.WriteCompressedInt32(arraySig.LowerBounds[(int)num3]);
					}
					goto IL_3F2;
				}
				goto IL_3F2;
			}
			case ElementType.GenericInst:
			{
				this.writer.WriteByte((byte)typeSig.ElementType);
				GenericInstSig genericInstSig = (GenericInstSig)typeSig;
				this.Write(genericInstSig.GenericType);
				uint num = this.WriteCompressedUInt32((uint)genericInstSig.GenericArguments.Count);
				for (uint num4 = 0U; num4 < num; num4 += 1U)
				{
					this.Write(genericInstSig.GenericArguments[(int)num4]);
				}
				goto IL_3F2;
			}
			case ElementType.ValueArray:
				this.writer.WriteByte((byte)typeSig.ElementType);
				this.Write(typeSig.Next);
				this.WriteCompressedUInt32((typeSig as ValueArraySig).Size);
				goto IL_3F2;
			case ElementType.FnPtr:
				this.writer.WriteByte((byte)typeSig.ElementType);
				this.Write((typeSig as FnPtrSig).Signature);
				goto IL_3F2;
			case ElementType.CModReqd:
			case ElementType.CModOpt:
				this.writer.WriteByte((byte)typeSig.ElementType);
				this.Write((typeSig as ModifierSig).Modifier);
				this.Write(typeSig.Next);
				goto IL_3F2;
			case ElementType.Module:
				this.writer.WriteByte((byte)typeSig.ElementType);
				this.WriteCompressedUInt32((typeSig as ModuleSig).Index);
				this.Write(typeSig.Next);
				goto IL_3F2;
			}
			this.helper.Error("Unknown or unsupported element type");
			this.writer.WriteByte(2);
			IL_3F2:
			this.recursionCounter.Decrement();
		}

		// Token: 0x06005821 RID: 22561 RVA: 0x001B0DFC File Offset: 0x001B0DFC
		private void Write(ITypeDefOrRef tdr)
		{
			if (tdr == null)
			{
				this.helper.Error("TypeDefOrRef is null");
				this.WriteCompressedUInt32(0U);
				return;
			}
			uint num = this.helper.ToEncodedToken(tdr);
			if (num > 536870911U)
			{
				this.helper.Error("Encoded token doesn't fit in 29 bits");
				num = 0U;
			}
			this.WriteCompressedUInt32(num);
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x001B0E60 File Offset: 0x001B0E60
		private void Write(CallingConventionSig sig)
		{
			if (sig == null)
			{
				this.helper.Error("sig is null");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.helper.Error("Infinite recursion");
				return;
			}
			MethodBaseSig sig2;
			FieldSig sig3;
			LocalSig sig4;
			GenericInstMethodSig sig5;
			if ((sig2 = (sig as MethodBaseSig)) != null)
			{
				this.Write(sig2);
			}
			else if ((sig3 = (sig as FieldSig)) != null)
			{
				this.Write(sig3);
			}
			else if ((sig4 = (sig as LocalSig)) != null)
			{
				this.Write(sig4);
			}
			else if ((sig5 = (sig as GenericInstMethodSig)) != null)
			{
				this.Write(sig5);
			}
			else
			{
				this.helper.Error("Unknown calling convention sig");
				this.writer.WriteByte((byte)sig.GetCallingConvention());
			}
			this.recursionCounter.Decrement();
		}

		// Token: 0x06005823 RID: 22563 RVA: 0x001B0F3C File Offset: 0x001B0F3C
		private void Write(MethodBaseSig sig)
		{
			if (sig == null)
			{
				this.helper.Error("sig is null");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.helper.Error("Infinite recursion");
				return;
			}
			this.writer.WriteByte((byte)sig.GetCallingConvention());
			if (sig.Generic)
			{
				this.WriteCompressedUInt32(sig.GenParamCount);
			}
			uint num = (uint)sig.Params.Count;
			if (sig.ParamsAfterSentinel != null)
			{
				num += (uint)sig.ParamsAfterSentinel.Count;
			}
			uint num2 = this.WriteCompressedUInt32(num);
			this.Write(sig.RetType);
			uint num3 = 0U;
			while (num3 < num2 && num3 < (uint)sig.Params.Count)
			{
				this.Write(sig.Params[(int)num3]);
				num3 += 1U;
			}
			if (sig.ParamsAfterSentinel != null && sig.ParamsAfterSentinel.Count > 0)
			{
				this.writer.WriteByte(65);
				uint num4 = 0U;
				uint num5 = (uint)sig.Params.Count;
				while (num4 < (uint)sig.ParamsAfterSentinel.Count && num5 < num2)
				{
					this.Write(sig.ParamsAfterSentinel[(int)num4]);
					num4 += 1U;
					num5 += 1U;
				}
			}
			this.recursionCounter.Decrement();
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x001B1094 File Offset: 0x001B1094
		private void Write(FieldSig sig)
		{
			if (sig == null)
			{
				this.helper.Error("sig is null");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.helper.Error("Infinite recursion");
				return;
			}
			this.writer.WriteByte((byte)sig.GetCallingConvention());
			this.Write(sig.Type);
			this.recursionCounter.Decrement();
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x001B1108 File Offset: 0x001B1108
		private void Write(LocalSig sig)
		{
			if (sig == null)
			{
				this.helper.Error("sig is null");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.helper.Error("Infinite recursion");
				return;
			}
			this.writer.WriteByte((byte)sig.GetCallingConvention());
			uint num = this.WriteCompressedUInt32((uint)sig.Locals.Count);
			if (num >= 65536U)
			{
				this.helper.Error("Too many locals, max number of locals is 65535 (0xFFFF)");
			}
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				this.Write(sig.Locals[(int)num2]);
			}
			this.recursionCounter.Decrement();
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x001B11BC File Offset: 0x001B11BC
		private void Write(GenericInstMethodSig sig)
		{
			if (sig == null)
			{
				this.helper.Error("sig is null");
				return;
			}
			if (!this.recursionCounter.Increment())
			{
				this.helper.Error("Infinite recursion");
				return;
			}
			this.writer.WriteByte((byte)sig.GetCallingConvention());
			uint num = this.WriteCompressedUInt32((uint)sig.GenericArguments.Count);
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				this.Write(sig.GenericArguments[(int)num2]);
			}
			this.recursionCounter.Decrement();
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x001B1254 File Offset: 0x001B1254
		public void Dispose()
		{
			if (!this.disposeStream)
			{
				return;
			}
			if (this.outStream != null)
			{
				this.outStream.Dispose();
			}
		}

		// Token: 0x04002A66 RID: 10854
		private readonly ISignatureWriterHelper helper;

		// Token: 0x04002A67 RID: 10855
		private RecursionCounter recursionCounter;

		// Token: 0x04002A68 RID: 10856
		private readonly MemoryStream outStream;

		// Token: 0x04002A69 RID: 10857
		private readonly DataWriter writer;

		// Token: 0x04002A6A RID: 10858
		private readonly bool disposeStream;
	}
}
