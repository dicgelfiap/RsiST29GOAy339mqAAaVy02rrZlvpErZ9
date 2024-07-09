using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A1E RID: 2590
	public class WordWrapNeededEventArgs : EventArgs
	{
		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x060065F5 RID: 26101 RVA: 0x001EFA08 File Offset: 0x001EFA08
		// (set) Token: 0x060065F6 RID: 26102 RVA: 0x001EFA10 File Offset: 0x001EFA10
		public List<int> CutOffPositions { get; private set; }

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x060065F7 RID: 26103 RVA: 0x001EFA1C File Offset: 0x001EFA1C
		// (set) Token: 0x060065F8 RID: 26104 RVA: 0x001EFA24 File Offset: 0x001EFA24
		public bool ImeAllowed { get; private set; }

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x060065F9 RID: 26105 RVA: 0x001EFA30 File Offset: 0x001EFA30
		// (set) Token: 0x060065FA RID: 26106 RVA: 0x001EFA38 File Offset: 0x001EFA38
		public Line Line { get; private set; }

		// Token: 0x060065FB RID: 26107 RVA: 0x001EFA44 File Offset: 0x001EFA44
		public WordWrapNeededEventArgs(List<int> cutOffPositions, bool imeAllowed, Line line)
		{
			this.CutOffPositions = cutOffPositions;
			this.ImeAllowed = imeAllowed;
			this.Line = line;
		}
	}
}
