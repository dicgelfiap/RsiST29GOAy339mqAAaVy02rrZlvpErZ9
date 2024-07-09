using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x0200090B RID: 2315
	[ComVisible(true)]
	public struct PdbAsyncStepInfo
	{
		// Token: 0x06005990 RID: 22928 RVA: 0x001B5E68 File Offset: 0x001B5E68
		public PdbAsyncStepInfo(Instruction yieldInstruction, MethodDef breakpointMethod, Instruction breakpointInstruction)
		{
			this.YieldInstruction = yieldInstruction;
			this.BreakpointMethod = breakpointMethod;
			this.BreakpointInstruction = breakpointInstruction;
		}

		// Token: 0x04002B59 RID: 11097
		public Instruction YieldInstruction;

		// Token: 0x04002B5A RID: 11098
		public MethodDef BreakpointMethod;

		// Token: 0x04002B5B RID: 11099
		public Instruction BreakpointInstruction;
	}
}
