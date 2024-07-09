using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A1C RID: 2588
	public class TextChangedEventArgs : EventArgs
	{
		// Token: 0x060065ED RID: 26093 RVA: 0x001EF9AC File Offset: 0x001EF9AC
		public TextChangedEventArgs(Range changedRange)
		{
			this.ChangedRange = changedRange;
		}

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x060065EE RID: 26094 RVA: 0x001EF9C0 File Offset: 0x001EF9C0
		// (set) Token: 0x060065EF RID: 26095 RVA: 0x001EF9C8 File Offset: 0x001EF9C8
		public Range ChangedRange { get; set; }
	}
}
