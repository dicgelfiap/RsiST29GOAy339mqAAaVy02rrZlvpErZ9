using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;

namespace DarkUI.Docking
{
	// Token: 0x02000092 RID: 146
	internal class DarkDockTabArea
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000365BC File Offset: 0x000365BC
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x000365C4 File Offset: 0x000365C4
		public DarkDockArea DockArea { get; private set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x000365D0 File Offset: 0x000365D0
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x000365D8 File Offset: 0x000365D8
		public Rectangle ClientRectangle { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x000365E4 File Offset: 0x000365E4
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x000365EC File Offset: 0x000365EC
		public Rectangle DropdownRectangle { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000365F8 File Offset: 0x000365F8
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00036600 File Offset: 0x00036600
		public bool DropdownHot { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0003660C File Offset: 0x0003660C
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00036614 File Offset: 0x00036614
		public int Offset { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00036620 File Offset: 0x00036620
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00036628 File Offset: 0x00036628
		public int TotalTabSize { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00036634 File Offset: 0x00036634
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0003663C File Offset: 0x0003663C
		public bool Visible { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00036648 File Offset: 0x00036648
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x00036650 File Offset: 0x00036650
		public DarkDockTab ClickedCloseButton { get; set; }

		// Token: 0x06000598 RID: 1432 RVA: 0x0003665C File Offset: 0x0003665C
		public DarkDockTabArea(DarkDockArea dockArea)
		{
			this.DockArea = dockArea;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0003668C File Offset: 0x0003668C
		public void ShowMenu(Control control, Point location)
		{
			this._tabMenu.Show(control, location);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0003669C File Offset: 0x0003669C
		public void AddMenuItem(ToolStripMenuItem menuItem)
		{
			this._menuItems.Add(menuItem);
			this.RebuildMenu();
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000366B0 File Offset: 0x000366B0
		public void RemoveMenuItem(ToolStripMenuItem menuItem)
		{
			this._menuItems.Remove(menuItem);
			this.RebuildMenu();
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000366C8 File Offset: 0x000366C8
		public ToolStripMenuItem GetMenuItem(DarkDockContent content)
		{
			ToolStripMenuItem result = null;
			foreach (ToolStripMenuItem toolStripMenuItem in this._menuItems)
			{
				DarkDockContent darkDockContent = toolStripMenuItem.Tag as DarkDockContent;
				if (darkDockContent != null && darkDockContent == content)
				{
					result = toolStripMenuItem;
				}
			}
			return result;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00036738 File Offset: 0x00036738
		public void RebuildMenu()
		{
			this._tabMenu.Items.Clear();
			List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
			int num = 0;
			for (int i = 0; i < this._menuItems.Count; i++)
			{
				foreach (ToolStripMenuItem toolStripMenuItem in this._menuItems)
				{
					if (((DarkDockContent)toolStripMenuItem.Tag).Order == num)
					{
						list.Add(toolStripMenuItem);
					}
				}
				num++;
			}
			foreach (ToolStripMenuItem value in list)
			{
				this._tabMenu.Items.Add(value);
			}
		}

		// Token: 0x0400049A RID: 1178
		private Dictionary<DarkDockContent, DarkDockTab> _tabs = new Dictionary<DarkDockContent, DarkDockTab>();

		// Token: 0x0400049B RID: 1179
		private List<ToolStripMenuItem> _menuItems = new List<ToolStripMenuItem>();

		// Token: 0x0400049C RID: 1180
		private DarkContextMenu _tabMenu = new DarkContextMenu();
	}
}
