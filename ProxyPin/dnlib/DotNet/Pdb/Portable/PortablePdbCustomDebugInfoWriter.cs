using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x02000940 RID: 2368
	internal readonly struct PortablePdbCustomDebugInfoWriter
	{
		// Token: 0x06005B03 RID: 23299 RVA: 0x001BC0EC File Offset: 0x001BC0EC
		public static byte[] Write(IPortablePdbCustomDebugInfoWriterHelper helper, SerializerMethodContext methodContext, Metadata systemMetadata, PdbCustomDebugInfo cdi, DataWriterContext context)
		{
			PortablePdbCustomDebugInfoWriter portablePdbCustomDebugInfoWriter = new PortablePdbCustomDebugInfoWriter(helper, methodContext, systemMetadata, context);
			return portablePdbCustomDebugInfoWriter.Write(cdi);
		}

		// Token: 0x06005B04 RID: 23300 RVA: 0x001BC114 File Offset: 0x001BC114
		private PortablePdbCustomDebugInfoWriter(IPortablePdbCustomDebugInfoWriterHelper helper, SerializerMethodContext methodContext, Metadata systemMetadata, DataWriterContext context)
		{
			this.helper = helper;
			this.methodContext = methodContext;
			this.systemMetadata = systemMetadata;
			this.outStream = context.OutStream;
			this.writer = context.Writer;
			this.outStream.SetLength(0L);
			this.outStream.Position = 0L;
		}

		// Token: 0x06005B05 RID: 23301 RVA: 0x001BC170 File Offset: 0x001BC170
		private byte[] Write(PdbCustomDebugInfo cdi)
		{
			PdbCustomDebugInfoKind kind = cdi.Kind;
			switch (kind)
			{
			case PdbCustomDebugInfoKind.Unknown:
				this.WriteUnknown((PdbUnknownCustomDebugInfo)cdi);
				goto IL_118;
			case PdbCustomDebugInfoKind.TupleElementNames_PortablePdb:
				this.WriteTupleElementNames((PortablePdbTupleElementNamesCustomDebugInfo)cdi);
				goto IL_118;
			case PdbCustomDebugInfoKind.DefaultNamespace:
				this.WriteDefaultNamespace((PdbDefaultNamespaceCustomDebugInfo)cdi);
				goto IL_118;
			case PdbCustomDebugInfoKind.DynamicLocalVariables:
				this.WriteDynamicLocalVariables((PdbDynamicLocalVariablesCustomDebugInfo)cdi);
				goto IL_118;
			case PdbCustomDebugInfoKind.EmbeddedSource:
				this.WriteEmbeddedSource((PdbEmbeddedSourceCustomDebugInfo)cdi);
				goto IL_118;
			case PdbCustomDebugInfoKind.SourceLink:
				this.WriteSourceLink((PdbSourceLinkCustomDebugInfo)cdi);
				goto IL_118;
			case PdbCustomDebugInfoKind.SourceServer:
			case PdbCustomDebugInfoKind.IteratorMethod:
				break;
			case PdbCustomDebugInfoKind.AsyncMethod:
				this.WriteAsyncMethodSteppingInformation((PdbAsyncMethodCustomDebugInfo)cdi);
				goto IL_118;
			default:
				switch (kind)
				{
				case PdbCustomDebugInfoKind.StateMachineHoistedLocalScopes:
					this.WriteStateMachineHoistedLocalScopes((PdbStateMachineHoistedLocalScopesCustomDebugInfo)cdi);
					goto IL_118;
				case PdbCustomDebugInfoKind.EditAndContinueLocalSlotMap:
					this.WriteEditAndContinueLocalSlotMap((PdbEditAndContinueLocalSlotMapCustomDebugInfo)cdi);
					goto IL_118;
				case PdbCustomDebugInfoKind.EditAndContinueLambdaMap:
					this.WriteEditAndContinueLambdaMap((PdbEditAndContinueLambdaMapCustomDebugInfo)cdi);
					goto IL_118;
				}
				break;
			}
			this.helper.Error("Unreachable code, caller should filter these out");
			return null;
			IL_118:
			return this.outStream.ToArray();
		}

		// Token: 0x06005B06 RID: 23302 RVA: 0x001BC2A4 File Offset: 0x001BC2A4
		private void WriteUTF8Z(string s)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			this.writer.WriteBytes(bytes);
			this.writer.WriteByte(0);
		}

		// Token: 0x06005B07 RID: 23303 RVA: 0x001BC2DC File Offset: 0x001BC2DC
		private void WriteStateMachineHoistedLocalScopes(PdbStateMachineHoistedLocalScopesCustomDebugInfo cdi)
		{
			if (!this.methodContext.HasBody)
			{
				this.helper.Error("Method has no body, can't write custom debug info: " + cdi.Kind.ToString());
				return;
			}
			IList<StateMachineHoistedLocalScope> scopes = cdi.Scopes;
			int count = scopes.Count;
			for (int i = 0; i < count; i++)
			{
				StateMachineHoistedLocalScope stateMachineHoistedLocalScope = scopes[i];
				uint num;
				uint num2;
				if (stateMachineHoistedLocalScope.IsSynthesizedLocal)
				{
					num = 0U;
					num2 = 0U;
				}
				else
				{
					Instruction start = stateMachineHoistedLocalScope.Start;
					if (start == null)
					{
						this.helper.Error("Instruction is null");
						return;
					}
					num = this.methodContext.GetOffset(start);
					num2 = this.methodContext.GetOffset(stateMachineHoistedLocalScope.End);
				}
				if (num > num2)
				{
					this.helper.Error("End instruction is before start instruction");
					return;
				}
				this.writer.WriteUInt32(num);
				this.writer.WriteUInt32(num2 - num);
			}
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x001BC3E4 File Offset: 0x001BC3E4
		private void WriteEditAndContinueLocalSlotMap(PdbEditAndContinueLocalSlotMapCustomDebugInfo cdi)
		{
			byte[] data = cdi.Data;
			if (data == null)
			{
				this.helper.Error("Data blob is null");
				return;
			}
			this.writer.WriteBytes(data);
		}

		// Token: 0x06005B09 RID: 23305 RVA: 0x001BC420 File Offset: 0x001BC420
		private void WriteEditAndContinueLambdaMap(PdbEditAndContinueLambdaMapCustomDebugInfo cdi)
		{
			byte[] data = cdi.Data;
			if (data == null)
			{
				this.helper.Error("Data blob is null");
				return;
			}
			this.writer.WriteBytes(data);
		}

		// Token: 0x06005B0A RID: 23306 RVA: 0x001BC45C File Offset: 0x001BC45C
		private void WriteUnknown(PdbUnknownCustomDebugInfo cdi)
		{
			byte[] data = cdi.Data;
			if (data == null)
			{
				this.helper.Error("Data blob is null");
				return;
			}
			this.writer.WriteBytes(data);
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x001BC498 File Offset: 0x001BC498
		private void WriteTupleElementNames(PortablePdbTupleElementNamesCustomDebugInfo cdi)
		{
			IList<string> names = cdi.Names;
			int count = names.Count;
			for (int i = 0; i < count; i++)
			{
				string text = names[i];
				if (text == null)
				{
					this.helper.Error("Tuple name is null");
					return;
				}
				this.WriteUTF8Z(text);
			}
		}

		// Token: 0x06005B0C RID: 23308 RVA: 0x001BC4EC File Offset: 0x001BC4EC
		private void WriteDefaultNamespace(PdbDefaultNamespaceCustomDebugInfo cdi)
		{
			string @namespace = cdi.Namespace;
			if (@namespace == null)
			{
				this.helper.Error("Default namespace is null");
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(@namespace);
			this.writer.WriteBytes(bytes);
		}

		// Token: 0x06005B0D RID: 23309 RVA: 0x001BC534 File Offset: 0x001BC534
		private void WriteDynamicLocalVariables(PdbDynamicLocalVariablesCustomDebugInfo cdi)
		{
			bool[] flags = cdi.Flags;
			for (int i = 0; i < flags.Length; i += 8)
			{
				this.writer.WriteByte(PortablePdbCustomDebugInfoWriter.ToByte(flags, i));
			}
		}

		// Token: 0x06005B0E RID: 23310 RVA: 0x001BC570 File Offset: 0x001BC570
		private static byte ToByte(bool[] flags, int index)
		{
			int num = 0;
			int num2 = 1;
			int i = index;
			while (i < flags.Length)
			{
				if (flags[i])
				{
					num |= num2;
				}
				i++;
				num2 <<= 1;
			}
			return (byte)num;
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x001BC5A8 File Offset: 0x001BC5A8
		private void WriteEmbeddedSource(PdbEmbeddedSourceCustomDebugInfo cdi)
		{
			byte[] sourceCodeBlob = cdi.SourceCodeBlob;
			if (sourceCodeBlob == null)
			{
				this.helper.Error("Source code blob is null");
				return;
			}
			this.writer.WriteBytes(sourceCodeBlob);
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x001BC5E4 File Offset: 0x001BC5E4
		private void WriteSourceLink(PdbSourceLinkCustomDebugInfo cdi)
		{
			byte[] fileBlob = cdi.FileBlob;
			if (fileBlob == null)
			{
				this.helper.Error("Source link blob is null");
				return;
			}
			this.writer.WriteBytes(fileBlob);
		}

		// Token: 0x06005B11 RID: 23313 RVA: 0x001BC620 File Offset: 0x001BC620
		private void WriteAsyncMethodSteppingInformation(PdbAsyncMethodCustomDebugInfo cdi)
		{
			if (!this.methodContext.HasBody)
			{
				this.helper.Error("Method has no body, can't write custom debug info: " + cdi.Kind.ToString());
				return;
			}
			uint value;
			if (cdi.CatchHandlerInstruction == null)
			{
				value = 0U;
			}
			else
			{
				value = this.methodContext.GetOffset(cdi.CatchHandlerInstruction) + 1U;
			}
			this.writer.WriteUInt32(value);
			IList<PdbAsyncStepInfo> stepInfos = cdi.StepInfos;
			int count = stepInfos.Count;
			for (int i = 0; i < count; i++)
			{
				PdbAsyncStepInfo pdbAsyncStepInfo = stepInfos[i];
				if (pdbAsyncStepInfo.YieldInstruction == null)
				{
					this.helper.Error("YieldInstruction is null");
					return;
				}
				if (pdbAsyncStepInfo.BreakpointMethod == null)
				{
					this.helper.Error("BreakpointMethod is null");
					return;
				}
				if (pdbAsyncStepInfo.BreakpointInstruction == null)
				{
					this.helper.Error("BreakpointInstruction is null");
					return;
				}
				uint offset = this.methodContext.GetOffset(pdbAsyncStepInfo.YieldInstruction);
				uint value2;
				if (this.methodContext.IsSameMethod(pdbAsyncStepInfo.BreakpointMethod))
				{
					value2 = this.methodContext.GetOffset(pdbAsyncStepInfo.BreakpointInstruction);
				}
				else
				{
					value2 = this.GetOffsetSlow(pdbAsyncStepInfo.BreakpointMethod, pdbAsyncStepInfo.BreakpointInstruction);
				}
				uint rid = this.systemMetadata.GetRid(pdbAsyncStepInfo.BreakpointMethod);
				this.writer.WriteUInt32(offset);
				this.writer.WriteUInt32(value2);
				this.writer.WriteCompressedUInt32(rid);
			}
		}

		// Token: 0x06005B12 RID: 23314 RVA: 0x001BC7B8 File Offset: 0x001BC7B8
		private uint GetOffsetSlow(MethodDef method, Instruction instr)
		{
			CilBody body = method.Body;
			if (body == null)
			{
				this.helper.Error("Method has no body");
				return uint.MaxValue;
			}
			IList<Instruction> instructions = body.Instructions;
			uint num = 0U;
			for (int i = 0; i < instructions.Count; i++)
			{
				Instruction instruction = instructions[i];
				if (instruction == instr)
				{
					return num;
				}
				num += (uint)instruction.GetSize();
			}
			this.helper.Error("Couldn't find an instruction, maybe it was removed. It's still being referenced by some code or by the PDB");
			return uint.MaxValue;
		}

		// Token: 0x04002BFF RID: 11263
		private readonly IPortablePdbCustomDebugInfoWriterHelper helper;

		// Token: 0x04002C00 RID: 11264
		private readonly SerializerMethodContext methodContext;

		// Token: 0x04002C01 RID: 11265
		private readonly Metadata systemMetadata;

		// Token: 0x04002C02 RID: 11266
		private readonly MemoryStream outStream;

		// Token: 0x04002C03 RID: 11267
		private readonly DataWriter writer;
	}
}
