using System;
using System.Collections.Generic;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb.Symbols;

namespace dnlib.DotNet.Pdb.WindowsPdb
{
	// Token: 0x0200092C RID: 2348
	internal static class PseudoCustomDebugInfoFactory
	{
		// Token: 0x06005A78 RID: 23160 RVA: 0x001B8FF8 File Offset: 0x001B8FF8
		public static PdbAsyncMethodCustomDebugInfo TryCreateAsyncMethod(ModuleDef module, MethodDef method, CilBody body, int asyncKickoffMethod, IList<SymbolAsyncStepInfo> asyncStepInfos, uint? asyncCatchHandlerILOffset)
		{
			MDToken mdToken = new MDToken(asyncKickoffMethod);
			if (mdToken.Table != Table.Method)
			{
				return null;
			}
			MethodDef kickoffMethod = module.ResolveToken(mdToken) as MethodDef;
			PdbAsyncMethodCustomDebugInfo pdbAsyncMethodCustomDebugInfo = new PdbAsyncMethodCustomDebugInfo(asyncStepInfos.Count);
			pdbAsyncMethodCustomDebugInfo.KickoffMethod = kickoffMethod;
			if (asyncCatchHandlerILOffset != null)
			{
				pdbAsyncMethodCustomDebugInfo.CatchHandlerInstruction = PseudoCustomDebugInfoFactory.GetInstruction(body, asyncCatchHandlerILOffset.Value);
			}
			int count = asyncStepInfos.Count;
			for (int i = 0; i < count; i++)
			{
				SymbolAsyncStepInfo symbolAsyncStepInfo = asyncStepInfos[i];
				Instruction instruction = PseudoCustomDebugInfoFactory.GetInstruction(body, symbolAsyncStepInfo.YieldOffset);
				if (instruction != null)
				{
					MethodDef methodDef;
					Instruction instruction2;
					if (method.MDToken.Raw == symbolAsyncStepInfo.BreakpointMethod)
					{
						methodDef = method;
						instruction2 = PseudoCustomDebugInfoFactory.GetInstruction(body, symbolAsyncStepInfo.BreakpointOffset);
					}
					else
					{
						MDToken mdToken2 = new MDToken(symbolAsyncStepInfo.BreakpointMethod);
						if (mdToken2.Table != Table.Method)
						{
							goto IL_11D;
						}
						methodDef = (module.ResolveToken(mdToken2) as MethodDef);
						if (methodDef == null)
						{
							goto IL_11D;
						}
						instruction2 = PseudoCustomDebugInfoFactory.GetInstruction(methodDef.Body, symbolAsyncStepInfo.BreakpointOffset);
					}
					if (instruction2 != null)
					{
						pdbAsyncMethodCustomDebugInfo.StepInfos.Add(new PdbAsyncStepInfo(instruction, methodDef, instruction2));
					}
				}
				IL_11D:;
			}
			return pdbAsyncMethodCustomDebugInfo;
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x001B9138 File Offset: 0x001B9138
		private static Instruction GetInstruction(CilBody body, uint offset)
		{
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
	}
}
