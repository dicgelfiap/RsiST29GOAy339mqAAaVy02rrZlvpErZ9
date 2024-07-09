using System;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Docking;

namespace DarkUI.Win32
{
	// Token: 0x02000079 RID: 121
	public class DockResizeFilter : IMessageFilter
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x00030BC0 File Offset: 0x00030BC0
		public DockResizeFilter(DarkDockPanel dockPanel)
		{
			this._dockPanel = dockPanel;
			this._dragTimer = new Timer();
			this._dragTimer.Interval = 1;
			this._dragTimer.Tick += this.DragTimer_Tick;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00030C00 File Offset: 0x00030C00
		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg != 512 && m.Msg != 513 && m.Msg != 514 && m.Msg != 515 && m.Msg != 516 && m.Msg != 517 && m.Msg != 518)
			{
				return false;
			}
			if (m.Msg == 514 && this._isDragging)
			{
				this.StopDrag();
				return true;
			}
			if (m.Msg == 514 && !this._isDragging)
			{
				return false;
			}
			if (this._isDragging)
			{
				Cursor.Current = this._activeSplitter.ResizeCursor;
			}
			if (m.Msg == 512 && !this._isDragging && this._dockPanel.MouseButtonState != MouseButtons.None)
			{
				return false;
			}
			Control control = Control.FromHandle(m.HWnd);
			if (control == null)
			{
				return false;
			}
			if (control != this._dockPanel && !this._dockPanel.Contains(control))
			{
				return false;
			}
			this.CheckCursor();
			if (m.Msg == 513)
			{
				DarkDockSplitter darkDockSplitter = this.HotSplitter();
				if (darkDockSplitter != null)
				{
					this.StartDrag(darkDockSplitter);
					return true;
				}
			}
			return this.HotSplitter() != null || this._isDragging;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00030D88 File Offset: 0x00030D88
		private void DragTimer_Tick(object sender, EventArgs e)
		{
			if (this._dockPanel.MouseButtonState != MouseButtons.Left)
			{
				this.StopDrag();
				return;
			}
			Point difference = new Point(this._initialContact.X - Cursor.Position.X, this._initialContact.Y - Cursor.Position.Y);
			this._activeSplitter.UpdateOverlay(difference);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00030DFC File Offset: 0x00030DFC
		private void StartDrag(DarkDockSplitter splitter)
		{
			this._activeSplitter = splitter;
			Cursor.Current = this._activeSplitter.ResizeCursor;
			this._initialContact = Cursor.Position;
			this._isDragging = true;
			this._activeSplitter.ShowOverlay();
			this._dragTimer.Start();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00030E4C File Offset: 0x00030E4C
		private void StopDrag()
		{
			this._dragTimer.Stop();
			this._activeSplitter.HideOverlay();
			Point difference = new Point(this._initialContact.X - Cursor.Position.X, this._initialContact.Y - Cursor.Position.Y);
			this._activeSplitter.Move(difference);
			this._isDragging = false;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00030EC0 File Offset: 0x00030EC0
		private DarkDockSplitter HotSplitter()
		{
			foreach (DarkDockSplitter darkDockSplitter in this._dockPanel.Splitters)
			{
				if (darkDockSplitter.Bounds.Contains(Cursor.Position))
				{
					return darkDockSplitter;
				}
			}
			return null;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00030F3C File Offset: 0x00030F3C
		private void CheckCursor()
		{
			if (this._isDragging)
			{
				return;
			}
			DarkDockSplitter darkDockSplitter = this.HotSplitter();
			if (darkDockSplitter != null)
			{
				Cursor.Current = darkDockSplitter.ResizeCursor;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00030F74 File Offset: 0x00030F74
		private void ResetCursor()
		{
			Cursor.Current = Cursors.Default;
			this.CheckCursor();
		}

		// Token: 0x04000330 RID: 816
		private DarkDockPanel _dockPanel;

		// Token: 0x04000331 RID: 817
		private Timer _dragTimer;

		// Token: 0x04000332 RID: 818
		private bool _isDragging;

		// Token: 0x04000333 RID: 819
		private Point _initialContact;

		// Token: 0x04000334 RID: 820
		private DarkDockSplitter _activeSplitter;
	}
}
