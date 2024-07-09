using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Docking
{
	// Token: 0x0200008C RID: 140
	[ToolboxItem(false)]
	public class DarkDockRegion : Panel
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0003505C File Offset: 0x0003505C
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x00035064 File Offset: 0x00035064
		public DarkDockPanel DockPanel { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00035070 File Offset: 0x00035070
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00035078 File Offset: 0x00035078
		public DarkDockArea DockArea { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00035084 File Offset: 0x00035084
		public DarkDockContent ActiveDocument
		{
			get
			{
				if (this.DockArea != DarkDockArea.Document || this._groups.Count == 0)
				{
					return null;
				}
				return this._groups[0].VisibleContent;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000350B8 File Offset: 0x000350B8
		public List<DarkDockGroup> Groups
		{
			get
			{
				return this._groups.ToList<DarkDockGroup>();
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000350C8 File Offset: 0x000350C8
		public DarkDockRegion(DarkDockPanel dockPanel, DarkDockArea dockArea)
		{
			this._groups = new List<DarkDockGroup>();
			this.DockPanel = dockPanel;
			this.DockArea = dockArea;
			this.BuildProperties();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000350F0 File Offset: 0x000350F0
		internal void AddContent(DarkDockContent dockContent)
		{
			this.AddContent(dockContent, null);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000350FC File Offset: 0x000350FC
		internal void AddContent(DarkDockContent dockContent, DarkDockGroup dockGroup)
		{
			if (dockGroup == null)
			{
				if (this.DockArea == DarkDockArea.Document && this._groups.Count > 0)
				{
					dockGroup = this._groups[0];
				}
				else
				{
					dockGroup = this.CreateGroup();
				}
			}
			dockContent.DockRegion = this;
			dockGroup.AddContent(dockContent);
			if (!base.Visible)
			{
				base.Visible = true;
				this.CreateSplitter();
			}
			this.PositionGroups();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00035178 File Offset: 0x00035178
		internal void InsertContent(DarkDockContent dockContent, DarkDockGroup dockGroup, DockInsertType insertType)
		{
			int num = dockGroup.Order;
			if (insertType == DockInsertType.After)
			{
				num++;
			}
			DarkDockGroup darkDockGroup = this.InsertGroup(num);
			dockContent.DockRegion = this;
			darkDockGroup.AddContent(dockContent);
			if (!base.Visible)
			{
				base.Visible = true;
				this.CreateSplitter();
			}
			this.PositionGroups();
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000351D0 File Offset: 0x000351D0
		internal void RemoveContent(DarkDockContent dockContent)
		{
			dockContent.DockRegion = null;
			DarkDockGroup dockGroup = dockContent.DockGroup;
			dockGroup.RemoveContent(dockContent);
			dockContent.DockArea = DarkDockArea.None;
			if (dockGroup.ContentCount == 0)
			{
				this.RemoveGroup(dockGroup);
			}
			if (this._groups.Count == 0 && this.DockArea != DarkDockArea.Document)
			{
				base.Visible = false;
				this.RemoveSplitter();
			}
			this.PositionGroups();
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00035240 File Offset: 0x00035240
		public List<DarkDockContent> GetContents()
		{
			List<DarkDockContent> list = new List<DarkDockContent>();
			foreach (DarkDockGroup darkDockGroup in this._groups)
			{
				list.AddRange(darkDockGroup.GetContents());
			}
			return list;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000352A8 File Offset: 0x000352A8
		private DarkDockGroup CreateGroup()
		{
			int num = 0;
			if (this._groups.Count >= 1)
			{
				num = -1;
				foreach (DarkDockGroup darkDockGroup in this._groups)
				{
					if (darkDockGroup.Order >= num)
					{
						num = darkDockGroup.Order + 1;
					}
				}
			}
			DarkDockGroup darkDockGroup2 = new DarkDockGroup(this.DockPanel, this, num);
			this._groups.Add(darkDockGroup2);
			base.Controls.Add(darkDockGroup2);
			return darkDockGroup2;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0003534C File Offset: 0x0003534C
		private DarkDockGroup InsertGroup(int order)
		{
			foreach (DarkDockGroup darkDockGroup in this._groups)
			{
				if (darkDockGroup.Order >= order)
				{
					DarkDockGroup darkDockGroup2 = darkDockGroup;
					int order2 = darkDockGroup2.Order;
					darkDockGroup2.Order = order2 + 1;
				}
			}
			DarkDockGroup darkDockGroup3 = new DarkDockGroup(this.DockPanel, this, order);
			this._groups.Add(darkDockGroup3);
			base.Controls.Add(darkDockGroup3);
			return darkDockGroup3;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000353E4 File Offset: 0x000353E4
		private void RemoveGroup(DarkDockGroup group)
		{
			int order = group.Order;
			this._groups.Remove(group);
			base.Controls.Remove(group);
			foreach (DarkDockGroup darkDockGroup in this._groups)
			{
				if (darkDockGroup.Order > order)
				{
					DarkDockGroup darkDockGroup2 = darkDockGroup;
					int order2 = darkDockGroup2.Order;
					darkDockGroup2.Order = order2 - 1;
				}
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00035474 File Offset: 0x00035474
		private void PositionGroups()
		{
			DockStyle dock;
			switch (this.DockArea)
			{
			default:
				dock = DockStyle.Fill;
				break;
			case DarkDockArea.Left:
			case DarkDockArea.Right:
				dock = DockStyle.Top;
				break;
			case DarkDockArea.Bottom:
				dock = DockStyle.Left;
				break;
			}
			if (this._groups.Count == 1)
			{
				this._groups[0].Dock = DockStyle.Fill;
				return;
			}
			if (this._groups.Count > 1)
			{
				DarkDockGroup darkDockGroup = (from g in this._groups
				orderby g.Order descending
				select g).First<DarkDockGroup>();
				foreach (DarkDockGroup darkDockGroup2 in from g in this._groups
				orderby g.Order descending
				select g)
				{
					darkDockGroup2.SendToBack();
					if (darkDockGroup2.Order == darkDockGroup.Order)
					{
						darkDockGroup2.Dock = DockStyle.Fill;
					}
					else
					{
						darkDockGroup2.Dock = dock;
					}
				}
				this.SizeGroups();
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000355B8 File Offset: 0x000355B8
		private void SizeGroups()
		{
			if (this._groups.Count <= 1)
			{
				return;
			}
			Size size = new Size(0, 0);
			switch (this.DockArea)
			{
			default:
				return;
			case DarkDockArea.Left:
			case DarkDockArea.Right:
				size = new Size(base.ClientRectangle.Width, base.ClientRectangle.Height / this._groups.Count);
				break;
			case DarkDockArea.Bottom:
				size = new Size(base.ClientRectangle.Width / this._groups.Count, base.ClientRectangle.Height);
				break;
			}
			foreach (DarkDockGroup darkDockGroup in this._groups)
			{
				darkDockGroup.Size = size;
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000356B0 File Offset: 0x000356B0
		private void BuildProperties()
		{
			this.MinimumSize = new Size(50, 50);
			switch (this.DockArea)
			{
			default:
				this.Dock = DockStyle.Fill;
				base.Padding = new Padding(0, 1, 0, 0);
				return;
			case DarkDockArea.Left:
				this.Dock = DockStyle.Left;
				base.Padding = new Padding(0, 0, 1, 0);
				base.Visible = false;
				return;
			case DarkDockArea.Right:
				this.Dock = DockStyle.Right;
				base.Padding = new Padding(1, 0, 0, 0);
				base.Visible = false;
				return;
			case DarkDockArea.Bottom:
				this.Dock = DockStyle.Bottom;
				base.Padding = new Padding(0, 0, 0, 0);
				base.Visible = false;
				return;
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00035760 File Offset: 0x00035760
		private void CreateSplitter()
		{
			if (this._splitter != null && this.DockPanel.Splitters.Contains(this._splitter))
			{
				this.DockPanel.Splitters.Remove(this._splitter);
			}
			switch (this.DockArea)
			{
			case DarkDockArea.Left:
				this._splitter = new DarkDockSplitter(this.DockPanel, this, DarkSplitterType.Right);
				break;
			case DarkDockArea.Right:
				this._splitter = new DarkDockSplitter(this.DockPanel, this, DarkSplitterType.Left);
				break;
			case DarkDockArea.Bottom:
				this._splitter = new DarkDockSplitter(this.DockPanel, this, DarkSplitterType.Top);
				break;
			default:
				return;
			}
			this.DockPanel.Splitters.Add(this._splitter);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00035824 File Offset: 0x00035824
		private void RemoveSplitter()
		{
			if (this.DockPanel.Splitters.Contains(this._splitter))
			{
				this.DockPanel.Splitters.Remove(this._splitter);
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00035868 File Offset: 0x00035868
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			this._parentForm = base.FindForm();
			this._parentForm.ResizeEnd += this.ParentForm_ResizeEnd;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00035894 File Offset: 0x00035894
		protected override void OnResize(EventArgs eventargs)
		{
			base.OnResize(eventargs);
			this.SizeGroups();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000358A4 File Offset: 0x000358A4
		private void ParentForm_ResizeEnd(object sender, EventArgs e)
		{
			if (this._splitter != null)
			{
				this._splitter.UpdateBounds();
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000358BC File Offset: 0x000358BC
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			if (this._splitter != null)
			{
				this._splitter.UpdateBounds();
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000358DC File Offset: 0x000358DC
		public void Redraw()
		{
			base.Invalidate();
			foreach (DarkDockGroup darkDockGroup in this._groups)
			{
				darkDockGroup.Redraw();
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00035938 File Offset: 0x00035938
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			if (!base.Visible)
			{
				return;
			}
			using (SolidBrush solidBrush = new SolidBrush(Colors.GreyBackground))
			{
				graphics.FillRectangle(solidBrush, base.ClientRectangle);
			}
			using (Pen pen = new Pen(Colors.DarkBorder))
			{
				if (this.DockArea == DarkDockArea.Document)
				{
					graphics.DrawLine(pen, base.ClientRectangle.Left, 0, base.ClientRectangle.Right, 0);
				}
				if (this.DockArea == DarkDockArea.Right)
				{
					graphics.DrawLine(pen, base.ClientRectangle.Left, 0, base.ClientRectangle.Left, base.ClientRectangle.Height);
				}
				if (this.DockArea == DarkDockArea.Left)
				{
					graphics.DrawLine(pen, base.ClientRectangle.Right - 1, 0, base.ClientRectangle.Right - 1, base.ClientRectangle.Height);
				}
			}
		}

		// Token: 0x0400047D RID: 1149
		private List<DarkDockGroup> _groups;

		// Token: 0x0400047E RID: 1150
		private Form _parentForm;

		// Token: 0x0400047F RID: 1151
		private DarkDockSplitter _splitter;
	}
}
