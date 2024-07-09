using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A4E RID: 2638
	public class LineNeededEventArgs : EventArgs
	{
		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x060067C0 RID: 26560 RVA: 0x001F9CF0 File Offset: 0x001F9CF0
		// (set) Token: 0x060067C1 RID: 26561 RVA: 0x001F9CF8 File Offset: 0x001F9CF8
		public string SourceLineText { get; private set; }

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x060067C2 RID: 26562 RVA: 0x001F9D04 File Offset: 0x001F9D04
		// (set) Token: 0x060067C3 RID: 26563 RVA: 0x001F9D0C File Offset: 0x001F9D0C
		public int DisplayedLineIndex { get; private set; }

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x060067C4 RID: 26564 RVA: 0x001F9D18 File Offset: 0x001F9D18
		// (set) Token: 0x060067C5 RID: 26565 RVA: 0x001F9D20 File Offset: 0x001F9D20
		public string DisplayedLineText { get; set; }

		// Token: 0x060067C6 RID: 26566 RVA: 0x001F9D2C File Offset: 0x001F9D2C
		public LineNeededEventArgs(string sourceLineText, int displayedLineIndex)
		{
			this.SourceLineText = sourceLineText;
			this.DisplayedLineIndex = displayedLineIndex;
			this.DisplayedLineText = sourceLineText;
		}
	}
}
