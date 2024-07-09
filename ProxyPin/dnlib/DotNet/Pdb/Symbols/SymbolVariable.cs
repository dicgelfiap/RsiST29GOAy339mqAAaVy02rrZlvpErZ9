using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Symbols
{
	// Token: 0x02000936 RID: 2358
	[ComVisible(true)]
	public abstract class SymbolVariable
	{
		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06005ACC RID: 23244
		public abstract string Name { get; }

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06005ACD RID: 23245
		public abstract PdbLocalAttributes Attributes { get; }

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06005ACE RID: 23246
		public abstract int Index { get; }

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06005ACF RID: 23247
		public abstract PdbCustomDebugInfo[] CustomDebugInfos { get; }
	}
}
