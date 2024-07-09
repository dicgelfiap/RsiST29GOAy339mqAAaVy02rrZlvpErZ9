using System;
using System.Drawing;

namespace DarkUI.Docking
{
	// Token: 0x02000091 RID: 145
	internal class DarkDockTab
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00036504 File Offset: 0x00036504
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0003650C File Offset: 0x0003650C
		public DarkDockContent DockContent { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00036518 File Offset: 0x00036518
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00036520 File Offset: 0x00036520
		public Rectangle ClientRectangle { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0003652C File Offset: 0x0003652C
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00036534 File Offset: 0x00036534
		public Rectangle CloseButtonRectangle { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00036540 File Offset: 0x00036540
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x00036548 File Offset: 0x00036548
		public bool Hot { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00036554 File Offset: 0x00036554
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0003655C File Offset: 0x0003655C
		public bool CloseButtonHot { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00036568 File Offset: 0x00036568
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x00036570 File Offset: 0x00036570
		public bool ShowSeparator { get; set; }

		// Token: 0x06000586 RID: 1414 RVA: 0x0003657C File Offset: 0x0003657C
		public DarkDockTab(DarkDockContent content)
		{
			this.DockContent = content;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0003658C File Offset: 0x0003658C
		public int CalculateWidth(Graphics g, Font font)
		{
			return (int)g.MeasureString(this.DockContent.DockText, font).Width + 10;
		}
	}
}
