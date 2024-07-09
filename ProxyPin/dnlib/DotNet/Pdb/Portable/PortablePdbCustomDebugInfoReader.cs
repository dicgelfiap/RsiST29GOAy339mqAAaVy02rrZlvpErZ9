using System;
using System.Collections.Generic;
using System.IO;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x0200093E RID: 2366
	internal struct PortablePdbCustomDebugInfoReader
	{
		// Token: 0x06005AF4 RID: 23284 RVA: 0x001BBAC0 File Offset: 0x001BBAC0
		public static PdbCustomDebugInfo Read(ModuleDef module, TypeDef typeOpt, CilBody bodyOpt, GenericParamContext gpContext, Guid kind, ref DataReader reader)
		{
			try
			{
				return new PortablePdbCustomDebugInfoReader(module, typeOpt, bodyOpt, gpContext, ref reader).Read(kind);
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
			return null;
		}

		// Token: 0x06005AF5 RID: 23285 RVA: 0x001BBB28 File Offset: 0x001BBB28
		private PortablePdbCustomDebugInfoReader(ModuleDef module, TypeDef typeOpt, CilBody bodyOpt, GenericParamContext gpContext, ref DataReader reader)
		{
			this.module = module;
			this.typeOpt = typeOpt;
			this.bodyOpt = bodyOpt;
			this.gpContext = gpContext;
			this.reader = reader;
		}

		// Token: 0x06005AF6 RID: 23286 RVA: 0x001BBB54 File Offset: 0x001BBB54
		private PdbCustomDebugInfo Read(Guid kind)
		{
			if (kind == CustomDebugInfoGuids.AsyncMethodSteppingInformationBlob)
			{
				return this.ReadAsyncMethodSteppingInformationBlob();
			}
			if (kind == CustomDebugInfoGuids.DefaultNamespace)
			{
				return this.ReadDefaultNamespace();
			}
			if (kind == CustomDebugInfoGuids.DynamicLocalVariables)
			{
				return this.ReadDynamicLocalVariables((long)((ulong)this.reader.Length));
			}
			if (kind == CustomDebugInfoGuids.EmbeddedSource)
			{
				return this.ReadEmbeddedSource();
			}
			if (kind == CustomDebugInfoGuids.EncLambdaAndClosureMap)
			{
				return this.ReadEncLambdaAndClosureMap((long)((ulong)this.reader.Length));
			}
			if (kind == CustomDebugInfoGuids.EncLocalSlotMap)
			{
				return this.ReadEncLocalSlotMap((long)((ulong)this.reader.Length));
			}
			if (kind == CustomDebugInfoGuids.SourceLink)
			{
				return this.ReadSourceLink();
			}
			if (kind == CustomDebugInfoGuids.StateMachineHoistedLocalScopes)
			{
				return this.ReadStateMachineHoistedLocalScopes();
			}
			if (kind == CustomDebugInfoGuids.TupleElementNames)
			{
				return this.ReadTupleElementNames();
			}
			return new PdbUnknownCustomDebugInfo(kind, this.reader.ReadRemainingBytes());
		}

		// Token: 0x06005AF7 RID: 23287 RVA: 0x001BBC6C File Offset: 0x001BBC6C
		private PdbCustomDebugInfo ReadAsyncMethodSteppingInformationBlob()
		{
			if (this.bodyOpt == null)
			{
				return null;
			}
			uint num = this.reader.ReadUInt32() - 1U;
			Instruction instruction;
			if (num == 4294967295U)
			{
				instruction = null;
			}
			else
			{
				instruction = this.GetInstruction(num);
				if (instruction == null)
				{
					return null;
				}
			}
			PdbAsyncMethodSteppingInformationCustomDebugInfo pdbAsyncMethodSteppingInformationCustomDebugInfo = new PdbAsyncMethodSteppingInformationCustomDebugInfo();
			pdbAsyncMethodSteppingInformationCustomDebugInfo.CatchHandler = instruction;
			while (this.reader.Position < this.reader.Length)
			{
				Instruction instruction2 = this.GetInstruction(this.reader.ReadUInt32());
				if (instruction2 == null)
				{
					return null;
				}
				uint offset = this.reader.ReadUInt32();
				uint rid = this.reader.ReadCompressedUInt32();
				MDToken mdtoken = new MDToken(Table.Method, rid);
				MethodDef methodDef;
				Instruction instruction3;
				if (this.gpContext.Method != null && mdtoken == this.gpContext.Method.MDToken)
				{
					methodDef = this.gpContext.Method;
					instruction3 = this.GetInstruction(offset);
				}
				else
				{
					methodDef = (this.module.ResolveToken(mdtoken, this.gpContext) as MethodDef);
					if (methodDef == null)
					{
						return null;
					}
					instruction3 = PortablePdbCustomDebugInfoReader.GetInstruction(methodDef, offset);
				}
				if (instruction3 == null)
				{
					return null;
				}
				pdbAsyncMethodSteppingInformationCustomDebugInfo.AsyncStepInfos.Add(new PdbAsyncStepInfo(instruction2, methodDef, instruction3));
			}
			return pdbAsyncMethodSteppingInformationCustomDebugInfo;
		}

		// Token: 0x06005AF8 RID: 23288 RVA: 0x001BBDB8 File Offset: 0x001BBDB8
		private PdbCustomDebugInfo ReadDefaultNamespace()
		{
			return new PdbDefaultNamespaceCustomDebugInfo(this.reader.ReadUtf8String((int)this.reader.BytesLeft));
		}

		// Token: 0x06005AF9 RID: 23289 RVA: 0x001BBDD8 File Offset: 0x001BBDD8
		private PdbCustomDebugInfo ReadDynamicLocalVariables(long recPosEnd)
		{
			bool[] array = new bool[this.reader.Length * 8U];
			int num = 0;
			while (this.reader.Position < this.reader.Length)
			{
				int num2 = (int)this.reader.ReadByte();
				for (int i = 1; i < 256; i <<= 1)
				{
					array[num++] = ((num2 & i) != 0);
				}
			}
			return new PdbDynamicLocalVariablesCustomDebugInfo(array);
		}

		// Token: 0x06005AFA RID: 23290 RVA: 0x001BBE50 File Offset: 0x001BBE50
		private PdbCustomDebugInfo ReadEmbeddedSource()
		{
			return new PdbEmbeddedSourceCustomDebugInfo(this.reader.ReadRemainingBytes());
		}

		// Token: 0x06005AFB RID: 23291 RVA: 0x001BBE64 File Offset: 0x001BBE64
		private PdbCustomDebugInfo ReadEncLambdaAndClosureMap(long recPosEnd)
		{
			return new PdbEditAndContinueLambdaMapCustomDebugInfo(this.reader.ReadBytes((int)(recPosEnd - (long)((ulong)this.reader.Position))));
		}

		// Token: 0x06005AFC RID: 23292 RVA: 0x001BBE88 File Offset: 0x001BBE88
		private PdbCustomDebugInfo ReadEncLocalSlotMap(long recPosEnd)
		{
			return new PdbEditAndContinueLocalSlotMapCustomDebugInfo(this.reader.ReadBytes((int)(recPosEnd - (long)((ulong)this.reader.Position))));
		}

		// Token: 0x06005AFD RID: 23293 RVA: 0x001BBEAC File Offset: 0x001BBEAC
		private PdbCustomDebugInfo ReadSourceLink()
		{
			return new PdbSourceLinkCustomDebugInfo(this.reader.ReadRemainingBytes());
		}

		// Token: 0x06005AFE RID: 23294 RVA: 0x001BBEC0 File Offset: 0x001BBEC0
		private PdbCustomDebugInfo ReadStateMachineHoistedLocalScopes()
		{
			if (this.bodyOpt == null)
			{
				return null;
			}
			int num = (int)(this.reader.Length / 8U);
			PdbStateMachineHoistedLocalScopesCustomDebugInfo pdbStateMachineHoistedLocalScopesCustomDebugInfo = new PdbStateMachineHoistedLocalScopesCustomDebugInfo(num);
			for (int i = 0; i < num; i++)
			{
				uint num2 = this.reader.ReadUInt32();
				uint num3 = this.reader.ReadUInt32();
				if (num2 == 0U && num3 == 0U)
				{
					pdbStateMachineHoistedLocalScopesCustomDebugInfo.Scopes.Add(default(StateMachineHoistedLocalScope));
				}
				else
				{
					Instruction instruction = this.GetInstruction(num2);
					Instruction instruction2 = this.GetInstruction(num2 + num3);
					if (instruction == null)
					{
						return null;
					}
					pdbStateMachineHoistedLocalScopesCustomDebugInfo.Scopes.Add(new StateMachineHoistedLocalScope(instruction, instruction2));
				}
			}
			return pdbStateMachineHoistedLocalScopesCustomDebugInfo;
		}

		// Token: 0x06005AFF RID: 23295 RVA: 0x001BBF78 File Offset: 0x001BBF78
		private PdbCustomDebugInfo ReadTupleElementNames()
		{
			PortablePdbTupleElementNamesCustomDebugInfo portablePdbTupleElementNamesCustomDebugInfo = new PortablePdbTupleElementNamesCustomDebugInfo();
			while (this.reader.Position < this.reader.Length)
			{
				string item = this.ReadUTF8Z((long)((ulong)this.reader.Length));
				portablePdbTupleElementNamesCustomDebugInfo.Names.Add(item);
			}
			return portablePdbTupleElementNamesCustomDebugInfo;
		}

		// Token: 0x06005B00 RID: 23296 RVA: 0x001BBFCC File Offset: 0x001BBFCC
		private string ReadUTF8Z(long recPosEnd)
		{
			if ((ulong)this.reader.Position > (ulong)recPosEnd)
			{
				return null;
			}
			return this.reader.TryReadZeroTerminatedUtf8String();
		}

		// Token: 0x06005B01 RID: 23297 RVA: 0x001BBFF0 File Offset: 0x001BBFF0
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

		// Token: 0x06005B02 RID: 23298 RVA: 0x001BC064 File Offset: 0x001BC064
		private static Instruction GetInstruction(MethodDef method, uint offset)
		{
			if (method == null)
			{
				return null;
			}
			CilBody body = method.Body;
			if (body == null)
			{
				return null;
			}
			IList<Instruction> instructions = body.Instructions;
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

		// Token: 0x04002BFA RID: 11258
		private readonly ModuleDef module;

		// Token: 0x04002BFB RID: 11259
		private readonly TypeDef typeOpt;

		// Token: 0x04002BFC RID: 11260
		private readonly CilBody bodyOpt;

		// Token: 0x04002BFD RID: 11261
		private readonly GenericParamContext gpContext;

		// Token: 0x04002BFE RID: 11262
		private DataReader reader;
	}
}
