using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000901 RID: 2305
	[ComVisible(true)]
	public sealed class PdbTupleElementNamesCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x0600594C RID: 22860 RVA: 0x001B5B50 File Offset: 0x001B5B50
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.TupleElementNames;
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x0600594D RID: 22861 RVA: 0x001B5B54 File Offset: 0x001B5B54
		public override Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x0600594E RID: 22862 RVA: 0x001B5B5C File Offset: 0x001B5B5C
		public IList<PdbTupleElementNames> Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x001B5B64 File Offset: 0x001B5B64
		public PdbTupleElementNamesCustomDebugInfo()
		{
			this.names = new List<PdbTupleElementNames>();
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x001B5B78 File Offset: 0x001B5B78
		public PdbTupleElementNamesCustomDebugInfo(int capacity)
		{
			this.names = new List<PdbTupleElementNames>(capacity);
		}

		// Token: 0x04002B48 RID: 11080
		private readonly IList<PdbTupleElementNames> names;
	}
}
