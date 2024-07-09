using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A1D RID: 2589
	public class TextChangingEventArgs : EventArgs
	{
		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x060065F0 RID: 26096 RVA: 0x001EF9D4 File Offset: 0x001EF9D4
		// (set) Token: 0x060065F1 RID: 26097 RVA: 0x001EF9DC File Offset: 0x001EF9DC
		public string InsertingText { get; set; }

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x060065F2 RID: 26098 RVA: 0x001EF9E8 File Offset: 0x001EF9E8
		// (set) Token: 0x060065F3 RID: 26099 RVA: 0x001EF9F0 File Offset: 0x001EF9F0
		public bool Cancel { get; set; }
	}
}
