using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000903 RID: 2307
	[ComVisible(true)]
	public sealed class PortablePdbTupleElementNamesCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x0600595E RID: 22878 RVA: 0x001B5C54 File Offset: 0x001B5C54
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.TupleElementNames_PortablePdb;
			}
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x0600595F RID: 22879 RVA: 0x001B5C5C File Offset: 0x001B5C5C
		public override Guid Guid
		{
			get
			{
				return CustomDebugInfoGuids.TupleElementNames;
			}
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06005960 RID: 22880 RVA: 0x001B5C64 File Offset: 0x001B5C64
		public IList<string> Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x001B5C6C File Offset: 0x001B5C6C
		public PortablePdbTupleElementNamesCustomDebugInfo()
		{
			this.names = new List<string>();
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x001B5C80 File Offset: 0x001B5C80
		public PortablePdbTupleElementNamesCustomDebugInfo(int capacity)
		{
			this.names = new List<string>(capacity);
		}

		// Token: 0x04002B4E RID: 11086
		private readonly IList<string> names;
	}
}
