using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A1B RID: 2587
	public class LineRemovedEventArgs : EventArgs
	{
		// Token: 0x060065E6 RID: 26086 RVA: 0x001EF93C File Offset: 0x001EF93C
		public LineRemovedEventArgs(int index, int count, List<int> removedLineIds)
		{
			this.Index = index;
			this.Count = count;
			this.RemovedLineUniqueIds = removedLineIds;
		}

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x060065E7 RID: 26087 RVA: 0x001EF970 File Offset: 0x001EF970
		// (set) Token: 0x060065E8 RID: 26088 RVA: 0x001EF978 File Offset: 0x001EF978
		public int Index { get; private set; }

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x060065E9 RID: 26089 RVA: 0x001EF984 File Offset: 0x001EF984
		// (set) Token: 0x060065EA RID: 26090 RVA: 0x001EF98C File Offset: 0x001EF98C
		public int Count { get; private set; }

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x060065EB RID: 26091 RVA: 0x001EF998 File Offset: 0x001EF998
		// (set) Token: 0x060065EC RID: 26092 RVA: 0x001EF9A0 File Offset: 0x001EF9A0
		public List<int> RemovedLineUniqueIds { get; private set; }
	}
}
