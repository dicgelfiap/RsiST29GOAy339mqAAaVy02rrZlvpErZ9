using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A21 RID: 2593
	public class AutoIndentEventArgs : EventArgs
	{
		// Token: 0x0600660B RID: 26123 RVA: 0x001EFB4C File Offset: 0x001EFB4C
		public AutoIndentEventArgs(int iLine, string lineText, string prevLineText, int tabLength, int currentIndentation)
		{
			this.iLine = iLine;
			this.LineText = lineText;
			this.PrevLineText = prevLineText;
			this.TabLength = tabLength;
			this.AbsoluteIndentation = currentIndentation;
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x0600660C RID: 26124 RVA: 0x001EFB90 File Offset: 0x001EFB90
		// (set) Token: 0x0600660D RID: 26125 RVA: 0x001EFB98 File Offset: 0x001EFB98
		public int iLine { get; internal set; }

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x0600660E RID: 26126 RVA: 0x001EFBA4 File Offset: 0x001EFBA4
		// (set) Token: 0x0600660F RID: 26127 RVA: 0x001EFBAC File Offset: 0x001EFBAC
		public int TabLength { get; internal set; }

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x06006610 RID: 26128 RVA: 0x001EFBB8 File Offset: 0x001EFBB8
		// (set) Token: 0x06006611 RID: 26129 RVA: 0x001EFBC0 File Offset: 0x001EFBC0
		public string LineText { get; internal set; }

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06006612 RID: 26130 RVA: 0x001EFBCC File Offset: 0x001EFBCC
		// (set) Token: 0x06006613 RID: 26131 RVA: 0x001EFBD4 File Offset: 0x001EFBD4
		public string PrevLineText { get; internal set; }

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06006614 RID: 26132 RVA: 0x001EFBE0 File Offset: 0x001EFBE0
		// (set) Token: 0x06006615 RID: 26133 RVA: 0x001EFBE8 File Offset: 0x001EFBE8
		public int Shift { get; set; }

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x06006616 RID: 26134 RVA: 0x001EFBF4 File Offset: 0x001EFBF4
		// (set) Token: 0x06006617 RID: 26135 RVA: 0x001EFBFC File Offset: 0x001EFBFC
		public int ShiftNextLines { get; set; }

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x06006618 RID: 26136 RVA: 0x001EFC08 File Offset: 0x001EFC08
		// (set) Token: 0x06006619 RID: 26137 RVA: 0x001EFC10 File Offset: 0x001EFC10
		public int AbsoluteIndentation { get; set; }
	}
}
