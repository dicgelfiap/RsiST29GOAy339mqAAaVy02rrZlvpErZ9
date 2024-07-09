using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A4F RID: 2639
	public class LinePushedEventArgs : EventArgs
	{
		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x060067C7 RID: 26567 RVA: 0x001F9D60 File Offset: 0x001F9D60
		// (set) Token: 0x060067C8 RID: 26568 RVA: 0x001F9D68 File Offset: 0x001F9D68
		public string SourceLineText { get; private set; }

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x060067C9 RID: 26569 RVA: 0x001F9D74 File Offset: 0x001F9D74
		// (set) Token: 0x060067CA RID: 26570 RVA: 0x001F9D7C File Offset: 0x001F9D7C
		public int DisplayedLineIndex { get; private set; }

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x060067CB RID: 26571 RVA: 0x001F9D88 File Offset: 0x001F9D88
		// (set) Token: 0x060067CC RID: 26572 RVA: 0x001F9D90 File Offset: 0x001F9D90
		public string DisplayedLineText { get; private set; }

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x060067CD RID: 26573 RVA: 0x001F9D9C File Offset: 0x001F9D9C
		// (set) Token: 0x060067CE RID: 26574 RVA: 0x001F9DA4 File Offset: 0x001F9DA4
		public string SavedText { get; set; }

		// Token: 0x060067CF RID: 26575 RVA: 0x001F9DB0 File Offset: 0x001F9DB0
		public LinePushedEventArgs(string sourceLineText, int displayedLineIndex, string displayedLineText)
		{
			this.SourceLineText = sourceLineText;
			this.DisplayedLineIndex = displayedLineIndex;
			this.DisplayedLineText = displayedLineText;
			this.SavedText = displayedLineText;
		}
	}
}
