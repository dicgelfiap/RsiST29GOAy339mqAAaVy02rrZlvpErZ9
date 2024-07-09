using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009FB RID: 2555
	public class SelectedEventArgs : EventArgs
	{
		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x06006244 RID: 25156 RVA: 0x001D5228 File Offset: 0x001D5228
		// (set) Token: 0x06006245 RID: 25157 RVA: 0x001D5230 File Offset: 0x001D5230
		public AutocompleteItem Item { get; internal set; }

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x06006246 RID: 25158 RVA: 0x001D523C File Offset: 0x001D523C
		// (set) Token: 0x06006247 RID: 25159 RVA: 0x001D5244 File Offset: 0x001D5244
		public FastColoredTextBox Tb { get; set; }
	}
}
