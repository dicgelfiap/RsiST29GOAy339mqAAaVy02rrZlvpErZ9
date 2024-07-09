using System;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A25 RID: 2597
	public class ToolTipNeededEventArgs : EventArgs
	{
		// Token: 0x0600661A RID: 26138 RVA: 0x001EFC1C File Offset: 0x001EFC1C
		public ToolTipNeededEventArgs(Place place, string hoveredWord)
		{
			this.HoveredWord = hoveredWord;
			this.Place = place;
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x0600661B RID: 26139 RVA: 0x001EFC38 File Offset: 0x001EFC38
		// (set) Token: 0x0600661C RID: 26140 RVA: 0x001EFC40 File Offset: 0x001EFC40
		public Place Place { get; private set; }

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x0600661D RID: 26141 RVA: 0x001EFC4C File Offset: 0x001EFC4C
		// (set) Token: 0x0600661E RID: 26142 RVA: 0x001EFC54 File Offset: 0x001EFC54
		public string HoveredWord { get; private set; }

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x0600661F RID: 26143 RVA: 0x001EFC60 File Offset: 0x001EFC60
		// (set) Token: 0x06006620 RID: 26144 RVA: 0x001EFC68 File Offset: 0x001EFC68
		public string ToolTipTitle { get; set; }

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06006621 RID: 26145 RVA: 0x001EFC74 File Offset: 0x001EFC74
		// (set) Token: 0x06006622 RID: 26146 RVA: 0x001EFC7C File Offset: 0x001EFC7C
		public string ToolTipText { get; set; }

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06006623 RID: 26147 RVA: 0x001EFC88 File Offset: 0x001EFC88
		// (set) Token: 0x06006624 RID: 26148 RVA: 0x001EFC90 File Offset: 0x001EFC90
		public ToolTipIcon ToolTipIcon { get; set; }
	}
}
