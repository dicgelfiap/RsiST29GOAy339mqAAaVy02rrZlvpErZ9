using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009FA RID: 2554
	public class SelectingEventArgs : EventArgs
	{
		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x0600623B RID: 25147 RVA: 0x001D51CC File Offset: 0x001D51CC
		// (set) Token: 0x0600623C RID: 25148 RVA: 0x001D51D4 File Offset: 0x001D51D4
		public AutocompleteItem Item { get; internal set; }

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x0600623D RID: 25149 RVA: 0x001D51E0 File Offset: 0x001D51E0
		// (set) Token: 0x0600623E RID: 25150 RVA: 0x001D51E8 File Offset: 0x001D51E8
		public bool Cancel { get; set; }

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x0600623F RID: 25151 RVA: 0x001D51F4 File Offset: 0x001D51F4
		// (set) Token: 0x06006240 RID: 25152 RVA: 0x001D51FC File Offset: 0x001D51FC
		public int SelectedIndex { get; set; }

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x06006241 RID: 25153 RVA: 0x001D5208 File Offset: 0x001D5208
		// (set) Token: 0x06006242 RID: 25154 RVA: 0x001D5210 File Offset: 0x001D5210
		public bool Handled { get; set; }
	}
}
