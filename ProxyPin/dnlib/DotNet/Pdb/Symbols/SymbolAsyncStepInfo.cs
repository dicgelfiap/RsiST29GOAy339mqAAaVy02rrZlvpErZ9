using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Symbols
{
	// Token: 0x0200092F RID: 2351
	[ComVisible(true)]
	public struct SymbolAsyncStepInfo
	{
		// Token: 0x06005AA8 RID: 23208 RVA: 0x001B9E40 File Offset: 0x001B9E40
		public SymbolAsyncStepInfo(uint yieldOffset, uint breakpointOffset, uint breakpointMethod)
		{
			this.YieldOffset = yieldOffset;
			this.BreakpointOffset = breakpointOffset;
			this.BreakpointMethod = breakpointMethod;
		}

		// Token: 0x04002BD6 RID: 11222
		public uint YieldOffset;

		// Token: 0x04002BD7 RID: 11223
		public uint BreakpointOffset;

		// Token: 0x04002BD8 RID: 11224
		public uint BreakpointMethod;
	}
}
