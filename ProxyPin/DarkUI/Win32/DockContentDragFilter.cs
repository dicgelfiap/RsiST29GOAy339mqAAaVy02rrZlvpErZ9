using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;
using DarkUI.Docking;
using DarkUI.Forms;

namespace DarkUI.Win32
{
	// Token: 0x0200007A RID: 122
	public class DockContentDragFilter : IMessageFilter
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x00030F88 File Offset: 0x00030F88
		public DockContentDragFilter(DarkDockPanel dockPanel)
		{
			this._dockPanel = dockPanel;
			this._highlightForm = new DarkTranslucentForm(Colors.BlueSelection, 0.6);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00030FC8 File Offset: 0x00030FC8
		public bool PreFilterMessage(ref Message m)
		{
			if (!this._isDragging)
			{
				return false;
			}
			if (m.Msg != 512 && m.Msg != 513 && m.Msg != 514 && m.Msg != 515 && m.Msg != 516 && m.Msg != 517 && m.Msg != 518)
			{
				return false;
			}
			if (m.Msg == 512)
			{
				this.HandleDrag();
				return false;
			}
			if (m.Msg == 514)
			{
				if (this._targetRegion != null)
				{
					this._dockPanel.RemoveContent(this._dragContent);
					this._dragContent.DockArea = this._targetRegion.DockArea;
					this._dockPanel.AddContent(this._dragContent);
				}
				else if (this._targetGroup != null)
				{
					this._dockPanel.RemoveContent(this._dragContent);
					DockInsertType insertType = this._insertType;
					if (insertType != DockInsertType.None)
					{
						if (insertType - DockInsertType.Before <= 1)
						{
							this._dockPanel.InsertContent(this._dragContent, this._targetGroup, this._insertType);
						}
					}
					else
					{
						this._dockPanel.AddContent(this._dragContent, this._targetGroup);
					}
				}
				this.StopDrag();
				return false;
			}
			return true;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00031144 File Offset: 0x00031144
		public void StartDrag(DarkDockContent content)
		{
			this._regionDropAreas = new Dictionary<DarkDockRegion, DockDropArea>();
			this._groupDropAreas = new Dictionary<DarkDockGroup, DockDropCollection>();
			foreach (DarkDockRegion darkDockRegion in this._dockPanel.Regions.Values)
			{
				if (darkDockRegion.DockArea != DarkDockArea.Document)
				{
					if (darkDockRegion.Visible)
					{
						using (List<DarkDockGroup>.Enumerator enumerator2 = darkDockRegion.Groups.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								DarkDockGroup darkDockGroup = enumerator2.Current;
								DockDropCollection value = new DockDropCollection(this._dockPanel, darkDockGroup);
								this._groupDropAreas.Add(darkDockGroup, value);
							}
							continue;
						}
					}
					DockDropArea value2 = new DockDropArea(this._dockPanel, darkDockRegion);
					this._regionDropAreas.Add(darkDockRegion, value2);
				}
			}
			this._dragContent = content;
			this._isDragging = true;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00031258 File Offset: 0x00031258
		private void StopDrag()
		{
			Cursor.Current = Cursors.Default;
			this._highlightForm.Hide();
			this._dragContent = null;
			this._isDragging = false;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00031280 File Offset: 0x00031280
		private void UpdateHighlightForm(Rectangle rect)
		{
			Cursor.Current = Cursors.SizeAll;
			this._highlightForm.SuspendLayout();
			this._highlightForm.Size = new Size(rect.Width, rect.Height);
			this._highlightForm.Location = new Point(rect.X, rect.Y);
			this._highlightForm.ResumeLayout();
			if (!this._highlightForm.Visible)
			{
				this._highlightForm.Show();
				this._highlightForm.BringToFront();
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00031314 File Offset: 0x00031314
		private void HandleDrag()
		{
			Point position = Cursor.Position;
			this._insertType = DockInsertType.None;
			this._targetRegion = null;
			this._targetGroup = null;
			foreach (DockDropArea dockDropArea in this._regionDropAreas.Values)
			{
				if (dockDropArea.DropArea.Contains(position))
				{
					this._insertType = DockInsertType.None;
					this._targetRegion = dockDropArea.DockRegion;
					this.UpdateHighlightForm(dockDropArea.HighlightArea);
					return;
				}
			}
			foreach (DockDropCollection dockDropCollection in this._groupDropAreas.Values)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				if (dockDropCollection.DropArea.DockGroup == this._dragContent.DockGroup)
				{
					flag2 = true;
				}
				if (dockDropCollection.DropArea.DockGroup.DockRegion == this._dragContent.DockRegion)
				{
					flag = true;
				}
				if (this._dragContent.DockGroup.ContentCount > 1)
				{
					flag3 = true;
				}
				if (!flag2 || flag3)
				{
					bool flag4 = false;
					bool flag5 = false;
					if (flag && !flag3)
					{
						if (dockDropCollection.InsertBeforeArea.DockGroup.Order == this._dragContent.DockGroup.Order + 1)
						{
							flag4 = true;
						}
						if (dockDropCollection.InsertAfterArea.DockGroup.Order == this._dragContent.DockGroup.Order - 1)
						{
							flag5 = true;
						}
					}
					if (!flag4 && dockDropCollection.InsertBeforeArea.DropArea.Contains(position))
					{
						this._insertType = DockInsertType.Before;
						this._targetGroup = dockDropCollection.InsertBeforeArea.DockGroup;
						this.UpdateHighlightForm(dockDropCollection.InsertBeforeArea.HighlightArea);
						return;
					}
					if (!flag5 && dockDropCollection.InsertAfterArea.DropArea.Contains(position))
					{
						this._insertType = DockInsertType.After;
						this._targetGroup = dockDropCollection.InsertAfterArea.DockGroup;
						this.UpdateHighlightForm(dockDropCollection.InsertAfterArea.HighlightArea);
						return;
					}
				}
				if (!flag2 && dockDropCollection.DropArea.DropArea.Contains(position))
				{
					this._insertType = DockInsertType.None;
					this._targetGroup = dockDropCollection.DropArea.DockGroup;
					this.UpdateHighlightForm(dockDropCollection.DropArea.HighlightArea);
					return;
				}
			}
			if (this._highlightForm.Visible)
			{
				this._highlightForm.Hide();
			}
			Cursor.Current = Cursors.No;
		}

		// Token: 0x04000335 RID: 821
		private DarkDockPanel _dockPanel;

		// Token: 0x04000336 RID: 822
		private DarkDockContent _dragContent;

		// Token: 0x04000337 RID: 823
		private DarkTranslucentForm _highlightForm;

		// Token: 0x04000338 RID: 824
		private bool _isDragging;

		// Token: 0x04000339 RID: 825
		private DarkDockRegion _targetRegion;

		// Token: 0x0400033A RID: 826
		private DarkDockGroup _targetGroup;

		// Token: 0x0400033B RID: 827
		private DockInsertType _insertType;

		// Token: 0x0400033C RID: 828
		private Dictionary<DarkDockRegion, DockDropArea> _regionDropAreas = new Dictionary<DarkDockRegion, DockDropArea>();

		// Token: 0x0400033D RID: 829
		private Dictionary<DarkDockGroup, DockDropCollection> _groupDropAreas = new Dictionary<DarkDockGroup, DockDropCollection>();
	}
}
