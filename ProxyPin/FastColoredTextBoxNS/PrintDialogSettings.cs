using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A20 RID: 2592
	public class PrintDialogSettings
	{
		// Token: 0x060065FC RID: 26108 RVA: 0x001EFA78 File Offset: 0x001EFA78
		public PrintDialogSettings()
		{
			this.ShowPrintPreviewDialog = true;
			this.Title = "";
			this.Footer = "";
			this.Header = "";
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x060065FD RID: 26109 RVA: 0x001EFAC0 File Offset: 0x001EFAC0
		// (set) Token: 0x060065FE RID: 26110 RVA: 0x001EFAC8 File Offset: 0x001EFAC8
		public bool ShowPageSetupDialog { get; set; }

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x060065FF RID: 26111 RVA: 0x001EFAD4 File Offset: 0x001EFAD4
		// (set) Token: 0x06006600 RID: 26112 RVA: 0x001EFADC File Offset: 0x001EFADC
		public bool ShowPrintDialog { get; set; }

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06006601 RID: 26113 RVA: 0x001EFAE8 File Offset: 0x001EFAE8
		// (set) Token: 0x06006602 RID: 26114 RVA: 0x001EFAF0 File Offset: 0x001EFAF0
		public bool ShowPrintPreviewDialog { get; set; }

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06006603 RID: 26115 RVA: 0x001EFAFC File Offset: 0x001EFAFC
		// (set) Token: 0x06006604 RID: 26116 RVA: 0x001EFB04 File Offset: 0x001EFB04
		public string Title { get; set; }

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06006605 RID: 26117 RVA: 0x001EFB10 File Offset: 0x001EFB10
		// (set) Token: 0x06006606 RID: 26118 RVA: 0x001EFB18 File Offset: 0x001EFB18
		public string Footer { get; set; }

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06006607 RID: 26119 RVA: 0x001EFB24 File Offset: 0x001EFB24
		// (set) Token: 0x06006608 RID: 26120 RVA: 0x001EFB2C File Offset: 0x001EFB2C
		public string Header { get; set; }

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06006609 RID: 26121 RVA: 0x001EFB38 File Offset: 0x001EFB38
		// (set) Token: 0x0600660A RID: 26122 RVA: 0x001EFB40 File Offset: 0x001EFB40
		public bool IncludeLineNumbers { get; set; }
	}
}
