using System;
using System.Drawing;

namespace DarkUI.Docking
{
	// Token: 0x02000094 RID: 148
	internal class DockDropArea
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00036854 File Offset: 0x00036854
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0003685C File Offset: 0x0003685C
		internal DarkDockPanel DockPanel { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00036868 File Offset: 0x00036868
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00036870 File Offset: 0x00036870
		internal Rectangle DropArea { get; private set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0003687C File Offset: 0x0003687C
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00036884 File Offset: 0x00036884
		internal Rectangle HighlightArea { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00036890 File Offset: 0x00036890
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00036898 File Offset: 0x00036898
		internal DarkDockRegion DockRegion { get; private set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x000368A4 File Offset: 0x000368A4
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x000368AC File Offset: 0x000368AC
		internal DarkDockGroup DockGroup { get; private set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x000368B8 File Offset: 0x000368B8
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x000368C0 File Offset: 0x000368C0
		internal DockInsertType InsertType { get; private set; }

		// Token: 0x060005AD RID: 1453 RVA: 0x000368CC File Offset: 0x000368CC
		internal DockDropArea(DarkDockPanel dockPanel, DarkDockRegion region)
		{
			this.DockPanel = dockPanel;
			this.DockRegion = region;
			this.InsertType = DockInsertType.None;
			this.BuildAreas();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00036900 File Offset: 0x00036900
		internal DockDropArea(DarkDockPanel dockPanel, DarkDockGroup group, DockInsertType insertType)
		{
			this.DockPanel = dockPanel;
			this.DockGroup = group;
			this.InsertType = insertType;
			this.BuildAreas();
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00036934 File Offset: 0x00036934
		internal void BuildAreas()
		{
			if (this.DockRegion != null)
			{
				this.BuildRegionAreas();
				return;
			}
			if (this.DockGroup != null)
			{
				this.BuildGroupAreas();
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0003695C File Offset: 0x0003695C
		private void BuildRegionAreas()
		{
			switch (this.DockRegion.DockArea)
			{
			case DarkDockArea.Left:
			{
				Rectangle rectangle = new Rectangle
				{
					X = this.DockPanel.PointToScreen(Point.Empty).X,
					Y = this.DockPanel.PointToScreen(Point.Empty).Y,
					Width = 50,
					Height = this.DockPanel.Height
				};
				this.DropArea = rectangle;
				this.HighlightArea = rectangle;
				return;
			}
			case DarkDockArea.Right:
			{
				Rectangle rectangle2 = new Rectangle
				{
					X = this.DockPanel.PointToScreen(Point.Empty).X + this.DockPanel.Width - 50,
					Y = this.DockPanel.PointToScreen(Point.Empty).Y,
					Width = 50,
					Height = this.DockPanel.Height
				};
				this.DropArea = rectangle2;
				this.HighlightArea = rectangle2;
				return;
			}
			case DarkDockArea.Bottom:
			{
				int num = this.DockPanel.PointToScreen(Point.Empty).X;
				int num2 = this.DockPanel.Width;
				if (this.DockPanel.Regions[DarkDockArea.Left].Visible)
				{
					num += this.DockPanel.Regions[DarkDockArea.Left].Width;
					num2 -= this.DockPanel.Regions[DarkDockArea.Left].Width;
				}
				if (this.DockPanel.Regions[DarkDockArea.Right].Visible)
				{
					num2 -= this.DockPanel.Regions[DarkDockArea.Right].Width;
				}
				Rectangle rectangle3 = new Rectangle
				{
					X = num,
					Y = this.DockPanel.PointToScreen(Point.Empty).Y + this.DockPanel.Height - 50,
					Width = num2,
					Height = 50
				};
				this.DropArea = rectangle3;
				this.HighlightArea = rectangle3;
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00036B98 File Offset: 0x00036B98
		private void BuildGroupAreas()
		{
			switch (this.InsertType)
			{
			case DockInsertType.None:
			{
				Rectangle rectangle = new Rectangle
				{
					X = this.DockGroup.PointToScreen(Point.Empty).X,
					Y = this.DockGroup.PointToScreen(Point.Empty).Y,
					Width = this.DockGroup.Width,
					Height = this.DockGroup.Height
				};
				this.DropArea = rectangle;
				this.HighlightArea = rectangle;
				return;
			}
			case DockInsertType.Before:
			{
				int width = this.DockGroup.Width;
				int height = this.DockGroup.Height;
				DarkDockArea dockArea = this.DockGroup.DockArea;
				if (dockArea - DarkDockArea.Left > 1)
				{
					if (dockArea == DarkDockArea.Bottom)
					{
						width = this.DockGroup.Width / 4;
					}
				}
				else
				{
					height = this.DockGroup.Height / 4;
				}
				Rectangle rectangle2 = new Rectangle
				{
					X = this.DockGroup.PointToScreen(Point.Empty).X,
					Y = this.DockGroup.PointToScreen(Point.Empty).Y,
					Width = width,
					Height = height
				};
				this.DropArea = rectangle2;
				this.HighlightArea = rectangle2;
				return;
			}
			case DockInsertType.After:
			{
				int x = this.DockGroup.PointToScreen(Point.Empty).X;
				int y = this.DockGroup.PointToScreen(Point.Empty).Y;
				int num = this.DockGroup.Width;
				int num2 = this.DockGroup.Height;
				DarkDockArea dockArea = this.DockGroup.DockArea;
				if (dockArea - DarkDockArea.Left > 1)
				{
					if (dockArea == DarkDockArea.Bottom)
					{
						num = this.DockGroup.Width / 4;
						x = this.DockGroup.PointToScreen(Point.Empty).X + this.DockGroup.Width - num;
					}
				}
				else
				{
					num2 = this.DockGroup.Height / 4;
					y = this.DockGroup.PointToScreen(Point.Empty).Y + this.DockGroup.Height - num2;
				}
				Rectangle rectangle3 = new Rectangle
				{
					X = x,
					Y = y,
					Width = num,
					Height = num2
				};
				this.DropArea = rectangle3;
				this.HighlightArea = rectangle3;
				return;
			}
			default:
				return;
			}
		}
	}
}
