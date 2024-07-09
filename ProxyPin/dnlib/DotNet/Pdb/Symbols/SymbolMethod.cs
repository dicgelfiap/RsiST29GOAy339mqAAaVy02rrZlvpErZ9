using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Pdb.Symbols
{
	// Token: 0x02000931 RID: 2353
	[ComVisible(true)]
	public abstract class SymbolMethod
	{
		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06005AB1 RID: 23217
		public abstract int Token { get; }

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06005AB2 RID: 23218
		public abstract SymbolScope RootScope { get; }

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06005AB3 RID: 23219
		public abstract IList<SymbolSequencePoint> SequencePoints { get; }

		// Token: 0x06005AB4 RID: 23220
		public abstract void GetCustomDebugInfos(MethodDef method, CilBody body, IList<PdbCustomDebugInfo> result);
	}
}
