using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Docking
{
	// Token: 0x0200008A RID: 138
	[ToolboxItem(false)]
	public class DarkDockGroup : Panel
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00032B44 File Offset: 0x00032B44
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00032B4C File Offset: 0x00032B4C
		public DarkDockPanel DockPanel { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00032B58 File Offset: 0x00032B58
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x00032B60 File Offset: 0x00032B60
		public DarkDockRegion DockRegion { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00032B6C File Offset: 0x00032B6C
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x00032B74 File Offset: 0x00032B74
		public DarkDockArea DockArea { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00032B80 File Offset: 0x00032B80
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x00032B88 File Offset: 0x00032B88
		public DarkDockContent VisibleContent { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00032B94 File Offset: 0x00032B94
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00032B9C File Offset: 0x00032B9C
		public int Order { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00032BA8 File Offset: 0x00032BA8
		public int ContentCount
		{
			get
			{
				return this._contents.Count;
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00032BB8 File Offset: 0x00032BB8
		public DarkDockGroup(DarkDockPanel dockPanel, DarkDockRegion dockRegion, int order)
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			this.DockPanel = dockPanel;
			this.DockRegion = dockRegion;
			this.DockArea = dockRegion.DockArea;
			this.Order = order;
			this._tabArea = new DarkDockTabArea(this.DockArea);
			this.DockPanel.ActiveContentChanged += this.DockPanel_ActiveContentChanged;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00032C3C File Offset: 0x00032C3C
		public void AddContent(DarkDockContent dockContent)
		{
			dockContent.DockGroup = this;
			dockContent.Dock = DockStyle.Fill;
			dockContent.Order = 0;
			if (this._contents.Count > 0)
			{
				int num = -1;
				foreach (DarkDockContent darkDockContent in this._contents)
				{
					if (darkDockContent.Order >= num)
					{
						num = darkDockContent.Order + 1;
					}
				}
				dockContent.Order = num;
			}
			this._contents.Add(dockContent);
			base.Controls.Add(dockContent);
			dockContent.DockTextChanged += this.DockContent_DockTextChanged;
			this._tabs.Add(dockContent, new DarkDockTab(dockContent));
			if (this.VisibleContent == null)
			{
				dockContent.Visible = true;
				this.VisibleContent = dockContent;
			}
			else
			{
				dockContent.Visible = false;
			}
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(dockContent.DockText);
			toolStripMenuItem.Tag = dockContent;
			toolStripMenuItem.Click += this.TabMenuItem_Select;
			toolStripMenuItem.Image = dockContent.Icon;
			this._tabArea.AddMenuItem(toolStripMenuItem);
			this.UpdateTabArea();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00032D78 File Offset: 0x00032D78
		public void RemoveContent(DarkDockContent dockContent)
		{
			dockContent.DockGroup = null;
			int order = dockContent.Order;
			this._contents.Remove(dockContent);
			base.Controls.Remove(dockContent);
			foreach (DarkDockContent darkDockContent in this._contents)
			{
				if (darkDockContent.Order > order)
				{
					DarkDockContent darkDockContent2 = darkDockContent;
					int order2 = darkDockContent2.Order;
					darkDockContent2.Order = order2 - 1;
				}
			}
			dockContent.DockTextChanged -= this.DockContent_DockTextChanged;
			if (this._tabs.ContainsKey(dockContent))
			{
				this._tabs.Remove(dockContent);
			}
			if (this.VisibleContent == dockContent)
			{
				this.VisibleContent = null;
				if (this._contents.Count > 0)
				{
					DarkDockContent darkDockContent3 = this._contents[0];
					darkDockContent3.Visible = true;
					this.VisibleContent = darkDockContent3;
				}
			}
			ToolStripMenuItem menuItem = this._tabArea.GetMenuItem(dockContent);
			menuItem.Click -= this.TabMenuItem_Select;
			this._tabArea.RemoveMenuItem(menuItem);
			this.UpdateTabArea();
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00032EB4 File Offset: 0x00032EB4
		public List<DarkDockContent> GetContents()
		{
			return (from c in this._contents
			orderby c.Order
			select c).ToList<DarkDockContent>();
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00032EE8 File Offset: 0x00032EE8
		private void UpdateTabArea()
		{
			if (this.DockArea == DarkDockArea.Document)
			{
				this._tabArea.Visible = (this._contents.Count > 0);
			}
			else
			{
				this._tabArea.Visible = (this._contents.Count > 1);
			}
			switch (this.DockArea)
			{
			case DarkDockArea.Document:
			{
				int num = this._tabArea.Visible ? 24 : 0;
				base.Padding = new Padding(0, num, 0, 0);
				this._tabArea.ClientRectangle = new Rectangle(base.Padding.Left, 0, base.ClientRectangle.Width - base.Padding.Horizontal, num);
				break;
			}
			case DarkDockArea.Left:
			case DarkDockArea.Right:
			{
				int num = this._tabArea.Visible ? 21 : 0;
				base.Padding = new Padding(0, 0, 0, num);
				this._tabArea.ClientRectangle = new Rectangle(base.Padding.Left, base.ClientRectangle.Bottom - num, base.ClientRectangle.Width - base.Padding.Horizontal, num);
				break;
			}
			case DarkDockArea.Bottom:
			{
				int num = this._tabArea.Visible ? 21 : 0;
				base.Padding = new Padding(1, 0, 0, num);
				this._tabArea.ClientRectangle = new Rectangle(base.Padding.Left, base.ClientRectangle.Bottom - num, base.ClientRectangle.Width - base.Padding.Horizontal, num);
				break;
			}
			}
			if (this.DockArea == DarkDockArea.Document)
			{
				int num2 = 24;
				this._tabArea.DropdownRectangle = new Rectangle(this._tabArea.ClientRectangle.Right - num2, 0, num2, num2);
			}
			this.BuildTabs();
			this.EnsureVisible();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00033100 File Offset: 0x00033100
		private void BuildTabs()
		{
			if (!this._tabArea.Visible)
			{
				return;
			}
			base.SuspendLayout();
			int width = DockIcons.close.Width;
			int num = 0;
			IOrderedEnumerable<DarkDockContent> orderedEnumerable = from c in this._contents
			orderby c.Order
			select c;
			foreach (DarkDockContent key in orderedEnumerable)
			{
				DarkDockTab darkDockTab = this._tabs[key];
				int num2;
				using (Graphics graphics = base.CreateGraphics())
				{
					num2 = darkDockTab.CalculateWidth(graphics, this.Font);
				}
				if (this.DockArea == DarkDockArea.Document)
				{
					num2 += 5;
					num2 += width;
					if (darkDockTab.DockContent.Icon != null)
					{
						num2 += darkDockTab.DockContent.Icon.Width + 5;
					}
				}
				darkDockTab.ShowSeparator = true;
				num2++;
				int y = (this.DockArea == DarkDockArea.Document) ? 0 : (base.ClientRectangle.Height - 21);
				int height = (this.DockArea == DarkDockArea.Document) ? 24 : 21;
				Rectangle clientRectangle = new Rectangle(this._tabArea.ClientRectangle.Left + num, y, num2, height);
				darkDockTab.ClientRectangle = clientRectangle;
				num += num2;
			}
			if (this.DockArea != DarkDockArea.Document && num > this._tabArea.ClientRectangle.Width)
			{
				int num3 = num - this._tabArea.ClientRectangle.Width;
				DarkDockTab darkDockTab2 = this._tabs[orderedEnumerable.Last<DarkDockContent>()];
				Rectangle clientRectangle2 = darkDockTab2.ClientRectangle;
				darkDockTab2.ClientRectangle = new Rectangle(clientRectangle2.Left, clientRectangle2.Top, clientRectangle2.Width - 1, clientRectangle2.Height);
				darkDockTab2.ShowSeparator = false;
				int i = 1;
				while (i < num3)
				{
					int width2 = (from tab in this._tabs.Values
					orderby tab.ClientRectangle.Width descending
					select tab).First<DarkDockTab>().ClientRectangle.Width;
					foreach (DarkDockContent key2 in orderedEnumerable)
					{
						DarkDockTab darkDockTab3 = this._tabs[key2];
						if (i >= num3)
						{
							break;
						}
						if (darkDockTab3.ClientRectangle.Width >= width2)
						{
							Rectangle clientRectangle3 = darkDockTab3.ClientRectangle;
							darkDockTab3.ClientRectangle = new Rectangle(clientRectangle3.Left, clientRectangle3.Top, clientRectangle3.Width - 1, clientRectangle3.Height);
							i++;
						}
					}
				}
				int num4 = 0;
				foreach (DarkDockContent key3 in orderedEnumerable)
				{
					DarkDockTab darkDockTab4 = this._tabs[key3];
					Rectangle clientRectangle4 = darkDockTab4.ClientRectangle;
					darkDockTab4.ClientRectangle = new Rectangle(this._tabArea.ClientRectangle.Left + num4, clientRectangle4.Top, clientRectangle4.Width, clientRectangle4.Height);
					num4 += clientRectangle4.Width;
				}
			}
			if (this.DockArea == DarkDockArea.Document)
			{
				foreach (DarkDockContent key4 in orderedEnumerable)
				{
					DarkDockTab darkDockTab5 = this._tabs[key4];
					Rectangle closeButtonRectangle = new Rectangle(darkDockTab5.ClientRectangle.Right - 7 - width - 1, darkDockTab5.ClientRectangle.Top + darkDockTab5.ClientRectangle.Height / 2 - width / 2 - 1, width, width);
					darkDockTab5.CloseButtonRectangle = closeButtonRectangle;
				}
			}
			num = 0;
			foreach (DarkDockContent key5 in orderedEnumerable)
			{
				DarkDockTab darkDockTab6 = this._tabs[key5];
				num += darkDockTab6.ClientRectangle.Width;
			}
			this._tabArea.TotalTabSize = num;
			base.ResumeLayout();
			base.Invalidate();
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00033624 File Offset: 0x00033624
		public void EnsureVisible()
		{
			if (this.DockArea != DarkDockArea.Document)
			{
				return;
			}
			if (this.VisibleContent == null)
			{
				return;
			}
			int num = base.ClientRectangle.Width - base.Padding.Horizontal - this._tabArea.DropdownRectangle.Width;
			Rectangle rectangle = new Rectangle(base.Padding.Left, 0, num, 0);
			DarkDockTab darkDockTab = this._tabs[this.VisibleContent];
			if (darkDockTab.ClientRectangle.IsEmpty)
			{
				return;
			}
			if (this.RectangleToTabArea(darkDockTab.ClientRectangle).Left < rectangle.Left)
			{
				this._tabArea.Offset = darkDockTab.ClientRectangle.Left;
			}
			else if (this.RectangleToTabArea(darkDockTab.ClientRectangle).Right > rectangle.Right)
			{
				this._tabArea.Offset = darkDockTab.ClientRectangle.Right - num;
			}
			if (this._tabArea.TotalTabSize < rectangle.Width)
			{
				this._tabArea.Offset = 0;
			}
			if (this._tabArea.TotalTabSize > rectangle.Width)
			{
				IOrderedEnumerable<DarkDockContent> source = from x in this._contents
				orderby x.Order
				select x;
				DarkDockTab darkDockTab2 = this._tabs[source.Last<DarkDockContent>()];
				if (darkDockTab2 != null && this.RectangleToTabArea(darkDockTab2.ClientRectangle).Right < rectangle.Right)
				{
					this._tabArea.Offset = darkDockTab2.ClientRectangle.Right - num;
				}
			}
			base.Invalidate();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00033804 File Offset: 0x00033804
		public void SetVisibleContent(DarkDockContent content)
		{
			if (!this._contents.Contains(content))
			{
				return;
			}
			if (this.VisibleContent != content)
			{
				this.VisibleContent = content;
				content.Visible = true;
				foreach (DarkDockContent darkDockContent in this._contents)
				{
					if (darkDockContent != content)
					{
						darkDockContent.Visible = false;
					}
				}
				base.Invalidate();
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00033898 File Offset: 0x00033898
		private Point PointToTabArea(Point point)
		{
			return new Point(point.X - this._tabArea.Offset, point.Y);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000338BC File Offset: 0x000338BC
		private Rectangle RectangleToTabArea(Rectangle rectangle)
		{
			return new Rectangle(this.PointToTabArea(rectangle.Location), rectangle.Size);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000338D8 File Offset: 0x000338D8
		protected override void OnResize(EventArgs eventargs)
		{
			base.OnResize(eventargs);
			this.UpdateTabArea();
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000338E8 File Offset: 0x000338E8
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this._dragTab != null)
			{
				int num = e.Location.X + this._tabArea.Offset;
				if (num < this._dragTab.ClientRectangle.Left)
				{
					if (this._dragTab.DockContent.Order > 0)
					{
						List<DarkDockTab> list = (from t in this._tabs.Values
						where t.DockContent.Order == this._dragTab.DockContent.Order - 1
						select t).ToList<DarkDockTab>();
						if (list.Count == 0)
						{
							return;
						}
						DarkDockTab darkDockTab = list.First<DarkDockTab>();
						if (darkDockTab == null)
						{
							return;
						}
						int order = this._dragTab.DockContent.Order;
						this._dragTab.DockContent.Order = order - 1;
						darkDockTab.DockContent.Order = order;
						this.BuildTabs();
						this.EnsureVisible();
						this._tabArea.RebuildMenu();
						return;
					}
				}
				else if (num > this._dragTab.ClientRectangle.Right)
				{
					int count = this._contents.Count;
					if (this._dragTab.DockContent.Order < count)
					{
						List<DarkDockTab> list2 = (from t in this._tabs.Values
						where t.DockContent.Order == this._dragTab.DockContent.Order + 1
						select t).ToList<DarkDockTab>();
						if (list2.Count == 0)
						{
							return;
						}
						DarkDockTab darkDockTab2 = list2.First<DarkDockTab>();
						if (darkDockTab2 == null)
						{
							return;
						}
						int order2 = this._dragTab.DockContent.Order;
						this._dragTab.DockContent.Order = order2 + 1;
						darkDockTab2.DockContent.Order = order2;
						this.BuildTabs();
						this.EnsureVisible();
						this._tabArea.RebuildMenu();
						return;
					}
				}
				return;
			}
			if (this._tabArea.DropdownRectangle.Contains(e.Location))
			{
				this._tabArea.DropdownHot = true;
				foreach (DarkDockTab darkDockTab3 in this._tabs.Values)
				{
					darkDockTab3.Hot = false;
				}
				base.Invalidate();
				return;
			}
			this._tabArea.DropdownHot = false;
			foreach (DarkDockTab darkDockTab4 in this._tabs.Values)
			{
				bool flag = this.RectangleToTabArea(darkDockTab4.ClientRectangle).Contains(e.Location);
				if (darkDockTab4.Hot != flag)
				{
					darkDockTab4.Hot = flag;
					base.Invalidate();
				}
				bool flag2 = this.RectangleToTabArea(darkDockTab4.CloseButtonRectangle).Contains(e.Location);
				if (darkDockTab4.CloseButtonHot != flag2)
				{
					darkDockTab4.CloseButtonHot = flag2;
					base.Invalidate();
				}
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00033BF4 File Offset: 0x00033BF4
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this._tabArea.DropdownRectangle.Contains(e.Location))
			{
				this._tabArea.DropdownHot = true;
				return;
			}
			foreach (DarkDockTab darkDockTab in this._tabs.Values)
			{
				if (this.RectangleToTabArea(darkDockTab.ClientRectangle).Contains(e.Location))
				{
					if (e.Button == MouseButtons.Middle)
					{
						darkDockTab.DockContent.Close();
						return;
					}
					if (this.RectangleToTabArea(darkDockTab.CloseButtonRectangle).Contains(e.Location))
					{
						this._tabArea.ClickedCloseButton = darkDockTab;
						return;
					}
					this.DockPanel.ActiveContent = darkDockTab.DockContent;
					this.EnsureVisible();
					this._dragTab = darkDockTab;
					return;
				}
			}
			if (this.VisibleContent != null)
			{
				this.DockPanel.ActiveContent = this.VisibleContent;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00033D30 File Offset: 0x00033D30
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this._dragTab = null;
			if (this._tabArea.DropdownRectangle.Contains(e.Location))
			{
				if (this._tabArea.DropdownHot)
				{
					this._tabArea.ShowMenu(this, new Point(this._tabArea.DropdownRectangle.Left, this._tabArea.DropdownRectangle.Bottom - 2));
				}
				return;
			}
			if (this._tabArea.ClickedCloseButton == null)
			{
				return;
			}
			if (this.RectangleToTabArea(this._tabArea.ClickedCloseButton.CloseButtonRectangle).Contains(e.Location))
			{
				this._tabArea.ClickedCloseButton.DockContent.Close();
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00033E08 File Offset: 0x00033E08
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			foreach (DarkDockTab darkDockTab in this._tabs.Values)
			{
				darkDockTab.Hot = false;
			}
			base.Invalidate();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00033E74 File Offset: 0x00033E74
		private void TabMenuItem_Select(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			if (toolStripMenuItem == null)
			{
				return;
			}
			DarkDockContent darkDockContent = toolStripMenuItem.Tag as DarkDockContent;
			if (darkDockContent == null)
			{
				return;
			}
			this.DockPanel.ActiveContent = darkDockContent;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00033EB4 File Offset: 0x00033EB4
		private void DockPanel_ActiveContentChanged(object sender, DockContentEventArgs e)
		{
			if (!this._contents.Contains(e.Content))
			{
				return;
			}
			if (e.Content == this.VisibleContent)
			{
				this.VisibleContent.Focus();
				return;
			}
			this.VisibleContent = e.Content;
			foreach (DarkDockContent darkDockContent in this._contents)
			{
				darkDockContent.Visible = (darkDockContent == this.VisibleContent);
			}
			this.VisibleContent.Focus();
			this.EnsureVisible();
			base.Invalidate();
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00033F6C File Offset: 0x00033F6C
		private void DockContent_DockTextChanged(object sender, EventArgs e)
		{
			this.BuildTabs();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00033F74 File Offset: 0x00033F74
		public void Redraw()
		{
			base.Invalidate();
			foreach (DarkDockContent darkDockContent in this._contents)
			{
				darkDockContent.Invalidate();
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00033FD0 File Offset: 0x00033FD0
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			using (SolidBrush solidBrush = new SolidBrush(Colors.GreyBackground))
			{
				graphics.FillRectangle(solidBrush, base.ClientRectangle);
			}
			if (!this._tabArea.Visible)
			{
				return;
			}
			using (SolidBrush solidBrush2 = new SolidBrush(Colors.MediumBackground))
			{
				graphics.FillRectangle(solidBrush2, this._tabArea.ClientRectangle);
			}
			foreach (DarkDockTab tab in this._tabs.Values)
			{
				if (this.DockArea == DarkDockArea.Document)
				{
					this.PaintDocumentTab(graphics, tab);
				}
				else
				{
					this.PaintToolWindowTab(graphics, tab);
				}
			}
			if (this.DockArea == DarkDockArea.Document)
			{
				using (SolidBrush solidBrush3 = new SolidBrush((this.DockPanel.ActiveGroup == this) ? Colors.BlueSelection : Colors.GreySelection))
				{
					Rectangle rect = new Rectangle(this._tabArea.ClientRectangle.Left, this._tabArea.ClientRectangle.Bottom - 2, this._tabArea.ClientRectangle.Width, 2);
					graphics.FillRectangle(solidBrush3, rect);
				}
				Rectangle rect2 = new Rectangle(this._tabArea.DropdownRectangle.Left, this._tabArea.DropdownRectangle.Top, this._tabArea.DropdownRectangle.Width, this._tabArea.DropdownRectangle.Height - 2);
				using (SolidBrush solidBrush4 = new SolidBrush(Colors.MediumBackground))
				{
					graphics.FillRectangle(solidBrush4, rect2);
				}
				using (Bitmap arrow = DockIcons.arrow)
				{
					graphics.DrawImageUnscaled(arrow, rect2.Left + rect2.Width / 2 - arrow.Width / 2, rect2.Top + rect2.Height / 2 - arrow.Height / 2 + 1);
				}
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00034270 File Offset: 0x00034270
		private void PaintDocumentTab(Graphics g, DarkDockTab tab)
		{
			Rectangle rect = this.RectangleToTabArea(tab.ClientRectangle);
			bool flag = this.VisibleContent == tab.DockContent;
			bool flag2 = this.DockPanel.ActiveGroup == this;
			Color color = flag ? Colors.BlueSelection : Colors.DarkBackground;
			if (!flag2)
			{
				color = (flag ? Colors.GreySelection : Colors.DarkBackground);
			}
			if (tab.Hot && !flag)
			{
				color = Colors.MediumBackground;
			}
			using (SolidBrush solidBrush = new SolidBrush(color))
			{
				g.FillRectangle(solidBrush, rect);
			}
			if (tab.ShowSeparator)
			{
				using (Pen pen = new Pen(Colors.DarkBorder))
				{
					g.DrawLine(pen, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom);
				}
			}
			int num = 0;
			if (tab.DockContent.Icon != null)
			{
				g.DrawImageUnscaled(tab.DockContent.Icon, rect.Left + 5, rect.Top + 4);
				num += tab.DockContent.Icon.Width + 2;
			}
			StringFormat format = new StringFormat
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center,
				FormatFlags = StringFormatFlags.NoWrap,
				Trimming = StringTrimming.EllipsisCharacter
			};
			using (SolidBrush solidBrush2 = new SolidBrush(flag ? Colors.LightText : Colors.DisabledText))
			{
				Rectangle r = new Rectangle(rect.Left + 5 + num, rect.Top, rect.Width - tab.CloseButtonRectangle.Width - 7 - 5 - num, rect.Height);
				g.DrawString(tab.DockContent.DockText, this.Font, solidBrush2, r, format);
			}
			Bitmap image = tab.CloseButtonHot ? DockIcons.inactive_close_selected : DockIcons.inactive_close;
			if (flag)
			{
				if (flag2)
				{
					image = (tab.CloseButtonHot ? DockIcons.close_selected : DockIcons.close);
				}
				else
				{
					image = (tab.CloseButtonHot ? DockIcons.close_selected : DockIcons.active_inactive_close);
				}
			}
			Rectangle rectangle = this.RectangleToTabArea(tab.CloseButtonRectangle);
			g.DrawImageUnscaled(image, rectangle.Left, rectangle.Top);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00034520 File Offset: 0x00034520
		private void PaintToolWindowTab(Graphics g, DarkDockTab tab)
		{
			Rectangle clientRectangle = tab.ClientRectangle;
			bool flag = this.VisibleContent == tab.DockContent;
			Color color = flag ? Colors.GreyBackground : Colors.DarkBackground;
			if (tab.Hot && !flag)
			{
				color = Colors.MediumBackground;
			}
			using (SolidBrush solidBrush = new SolidBrush(color))
			{
				g.FillRectangle(solidBrush, clientRectangle);
			}
			if (tab.ShowSeparator)
			{
				using (Pen pen = new Pen(Colors.DarkBorder))
				{
					g.DrawLine(pen, clientRectangle.Right - 1, clientRectangle.Top, clientRectangle.Right - 1, clientRectangle.Bottom);
				}
			}
			StringFormat format = new StringFormat
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center,
				FormatFlags = StringFormatFlags.NoWrap,
				Trimming = StringTrimming.EllipsisCharacter
			};
			using (SolidBrush solidBrush2 = new SolidBrush(flag ? Colors.BlueHighlight : Colors.DisabledText))
			{
				Rectangle r = new Rectangle(clientRectangle.Left + 5, clientRectangle.Top, clientRectangle.Width - 5, clientRectangle.Height);
				g.DrawString(tab.DockContent.DockText, this.Font, solidBrush2, r, format);
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000346AC File Offset: 0x000346AC
		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		// Token: 0x04000468 RID: 1128
		private List<DarkDockContent> _contents = new List<DarkDockContent>();

		// Token: 0x04000469 RID: 1129
		private Dictionary<DarkDockContent, DarkDockTab> _tabs = new Dictionary<DarkDockContent, DarkDockTab>();

		// Token: 0x0400046A RID: 1130
		private DarkDockTabArea _tabArea;

		// Token: 0x0400046B RID: 1131
		private DarkDockTab _dragTab;
	}
}
