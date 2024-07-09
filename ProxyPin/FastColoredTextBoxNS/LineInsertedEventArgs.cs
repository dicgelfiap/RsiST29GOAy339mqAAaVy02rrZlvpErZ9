using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A1A RID: 2586
	public class LineInsertedEventArgs : EventArgs
	{
		// Token: 0x060065E1 RID: 26081 RVA: 0x001EF8F8 File Offset: 0x001EF8F8
		public LineInsertedEventArgs(int index, int count)
		{
			this.Index = index;
			this.Count = count;
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x060065E2 RID: 26082 RVA: 0x001EF914 File Offset: 0x001EF914
		// (set) Token: 0x060065E3 RID: 26083 RVA: 0x001EF91C File Offset: 0x001EF91C
		public int Index { get; private set; }

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x060065E4 RID: 26084 RVA: 0x001EF928 File Offset: 0x001EF928
		// (set) Token: 0x060065E5 RID: 26085 RVA: 0x001EF930 File Offset: 0x001EF930
		public int Count { get; private set; }
	}
}
