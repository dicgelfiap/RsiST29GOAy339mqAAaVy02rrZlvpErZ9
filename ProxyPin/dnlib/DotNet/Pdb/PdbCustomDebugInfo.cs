using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008F5 RID: 2293
	[ComVisible(true)]
	public abstract class PdbCustomDebugInfo
	{
		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06005910 RID: 22800
		public abstract PdbCustomDebugInfoKind Kind { get; }

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06005911 RID: 22801
		public abstract Guid Guid { get; }
	}
}
