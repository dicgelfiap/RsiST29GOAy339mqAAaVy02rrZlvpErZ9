using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008FA RID: 2298
	[ComVisible(true)]
	public struct StateMachineHoistedLocalScope
	{
		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06005929 RID: 22825 RVA: 0x001B5968 File Offset: 0x001B5968
		public readonly bool IsSynthesizedLocal
		{
			get
			{
				return this.Start == null && this.End == null;
			}
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x001B5980 File Offset: 0x001B5980
		public StateMachineHoistedLocalScope(Instruction start, Instruction end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x04002B3E RID: 11070
		public Instruction Start;

		// Token: 0x04002B3F RID: 11071
		public Instruction End;
	}
}
