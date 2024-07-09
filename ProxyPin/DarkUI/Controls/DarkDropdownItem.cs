using System;
using System.Drawing;

namespace DarkUI.Controls
{
	// Token: 0x0200009C RID: 156
	public class DarkDropdownItem
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00037440 File Offset: 0x00037440
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00037448 File Offset: 0x00037448
		public string Text { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00037454 File Offset: 0x00037454
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0003745C File Offset: 0x0003745C
		public Bitmap Icon { get; set; }

		// Token: 0x060005EA RID: 1514 RVA: 0x00037468 File Offset: 0x00037468
		public DarkDropdownItem()
		{
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00037470 File Offset: 0x00037470
		public DarkDropdownItem(string text)
		{
			this.Text = text;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00037480 File Offset: 0x00037480
		public DarkDropdownItem(string text, Bitmap icon) : this(text)
		{
			this.Icon = icon;
		}
	}
}
