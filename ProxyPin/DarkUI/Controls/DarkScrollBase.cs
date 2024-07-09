using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000A9 RID: 169
	public abstract class DarkScrollBase : Control
	{
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060006CE RID: 1742 RVA: 0x0003C9D0 File Offset: 0x0003C9D0
		// (remove) Token: 0x060006CF RID: 1743 RVA: 0x0003CA0C File Offset: 0x0003CA0C
		public event EventHandler ViewportChanged;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060006D0 RID: 1744 RVA: 0x0003CA48 File Offset: 0x0003CA48
		// (remove) Token: 0x060006D1 RID: 1745 RVA: 0x0003CA84 File Offset: 0x0003CA84
		public event EventHandler ContentSizeChanged;

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0003CAC0 File Offset: 0x0003CAC0
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0003CAC8 File Offset: 0x0003CAC8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Rectangle Viewport
		{
			get
			{
				return this._viewport;
			}
			private set
			{
				this._viewport = value;
				if (this.ViewportChanged != null)
				{
					this.ViewportChanged(this, null);
				}
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0003CAEC File Offset: 0x0003CAEC
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x0003CAF4 File Offset: 0x0003CAF4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Size ContentSize
		{
			get
			{
				return this._contentSize;
			}
			set
			{
				this._contentSize = value;
				this.UpdateScrollBars();
				if (this.ContentSizeChanged != null)
				{
					this.ContentSizeChanged(this, null);
				}
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0003CB1C File Offset: 0x0003CB1C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Point OffsetMousePosition
		{
			get
			{
				return this._offsetMousePosition;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0003CB24 File Offset: 0x0003CB24
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0003CB2C File Offset: 0x0003CB2C
		[Category("Behavior")]
		[Description("Determines the maximum scroll change when dragging.")]
		[DefaultValue(0)]
		public int MaxDragChange
		{
			get
			{
				return this._maxDragChange;
			}
			set
			{
				this._maxDragChange = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0003CB3C File Offset: 0x0003CB3C
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0003CB44 File Offset: 0x0003CB44
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsDragging { get; private set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0003CB50 File Offset: 0x0003CB50
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0003CB58 File Offset: 0x0003CB58
		[Category("Behavior")]
		[Description("Determines whether scrollbars will remain visible when disabled.")]
		[DefaultValue(true)]
		public bool HideScrollBars
		{
			get
			{
				return this._hideScrollBars;
			}
			set
			{
				this._hideScrollBars = value;
				this.UpdateScrollBars();
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0003CB68 File Offset: 0x0003CB68
		protected DarkScrollBase()
		{
			base.SetStyle(ControlStyles.Selectable | ControlStyles.UserMouse, true);
			this._vScrollBar = new DarkScrollBar
			{
				ScrollOrientation = DarkScrollOrientation.Vertical
			};
			this._hScrollBar = new DarkScrollBar
			{
				ScrollOrientation = DarkScrollOrientation.Horizontal
			};
			base.Controls.Add(this._vScrollBar);
			base.Controls.Add(this._hScrollBar);
			this._vScrollBar.ValueChanged += delegate(object <p0>, ScrollValueEventArgs <p1>)
			{
				this.UpdateViewport();
			};
			this._hScrollBar.ValueChanged += delegate(object <p0>, ScrollValueEventArgs <p1>)
			{
				this.UpdateViewport();
			};
			this._vScrollBar.MouseDown += delegate(object <p0>, MouseEventArgs <p1>)
			{
				base.Select();
			};
			this._hScrollBar.MouseDown += delegate(object <p0>, MouseEventArgs <p1>)
			{
				base.Select();
			};
			this._dragTimer = new Timer();
			this._dragTimer.Interval = 1;
			this._dragTimer.Tick += this.DragTimer_Tick;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0003CC64 File Offset: 0x0003CC64
		private void UpdateScrollBars()
		{
			if (this._vScrollBar.Maximum != this.ContentSize.Height)
			{
				this._vScrollBar.Maximum = this.ContentSize.Height;
			}
			if (this._hScrollBar.Maximum != this.ContentSize.Width)
			{
				this._hScrollBar.Maximum = this.ContentSize.Width;
			}
			int scrollBarSize = Consts.ScrollBarSize;
			this._vScrollBar.Location = new Point(base.ClientSize.Width - scrollBarSize, 0);
			this._vScrollBar.Size = new Size(scrollBarSize, base.ClientSize.Height);
			this._hScrollBar.Location = new Point(0, base.ClientSize.Height - scrollBarSize);
			this._hScrollBar.Size = new Size(base.ClientSize.Width, scrollBarSize);
			if (base.DesignMode)
			{
				return;
			}
			this.SetVisibleSize();
			this.SetScrollBarVisibility();
			this.SetVisibleSize();
			this.SetScrollBarVisibility();
			if (this._vScrollBar.Visible)
			{
				this._hScrollBar.Width -= scrollBarSize;
			}
			if (this._hScrollBar.Visible)
			{
				this._vScrollBar.Height -= scrollBarSize;
			}
			this._vScrollBar.ViewSize = this._visibleSize.Height;
			this._hScrollBar.ViewSize = this._visibleSize.Width;
			this.UpdateViewport();
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0003CE08 File Offset: 0x0003CE08
		private void SetScrollBarVisibility()
		{
			this._vScrollBar.Enabled = (this._visibleSize.Height < this.ContentSize.Height);
			this._hScrollBar.Enabled = (this._visibleSize.Width < this.ContentSize.Width);
			if (this._hideScrollBars)
			{
				this._vScrollBar.Visible = this._vScrollBar.Enabled;
				this._hScrollBar.Visible = this._hScrollBar.Enabled;
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0003CE9C File Offset: 0x0003CE9C
		private void SetVisibleSize()
		{
			int scrollBarSize = Consts.ScrollBarSize;
			this._visibleSize = new Size(base.ClientSize.Width, base.ClientSize.Height);
			if (this._vScrollBar.Visible)
			{
				this._visibleSize.Width = this._visibleSize.Width - scrollBarSize;
			}
			if (this._hScrollBar.Visible)
			{
				this._visibleSize.Height = this._visibleSize.Height - scrollBarSize;
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0003CF20 File Offset: 0x0003CF20
		private void UpdateViewport()
		{
			int x = 0;
			int y = 0;
			int num = base.ClientSize.Width;
			int num2 = base.ClientSize.Height;
			if (this._hScrollBar.Visible)
			{
				x = this._hScrollBar.Value;
				num2 -= this._hScrollBar.Height;
			}
			if (this._vScrollBar.Visible)
			{
				y = this._vScrollBar.Value;
				num -= this._vScrollBar.Width;
			}
			this.Viewport = new Rectangle(x, y, num, num2);
			Point point = base.PointToClient(Control.MousePosition);
			this._offsetMousePosition = new Point(point.X + this.Viewport.Left, point.Y + this.Viewport.Top);
			base.Invalidate();
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0003D008 File Offset: 0x0003D008
		public void ScrollTo(Point point)
		{
			this.HScrollTo(point.X);
			this.VScrollTo(point.Y);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0003D034 File Offset: 0x0003D034
		public void VScrollTo(int value)
		{
			if (this._vScrollBar.Visible)
			{
				this._vScrollBar.Value = value;
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0003D054 File Offset: 0x0003D054
		public void HScrollTo(int value)
		{
			if (this._hScrollBar.Visible)
			{
				this._hScrollBar.Value = value;
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0003D074 File Offset: 0x0003D074
		protected virtual void StartDrag()
		{
			this.IsDragging = true;
			this._dragTimer.Start();
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0003D088 File Offset: 0x0003D088
		protected virtual void StopDrag()
		{
			this.IsDragging = false;
			this._dragTimer.Stop();
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0003D09C File Offset: 0x0003D09C
		public Point PointToView(Point point)
		{
			return new Point(point.X - this.Viewport.Left, point.Y - this.Viewport.Top);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0003D0E0 File Offset: 0x0003D0E0
		public Rectangle RectangleToView(Rectangle rect)
		{
			return new Rectangle(new Point(rect.Left - this.Viewport.Left, rect.Top - this.Viewport.Top), rect.Size);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0003D130 File Offset: 0x0003D130
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			this.UpdateScrollBars();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0003D140 File Offset: 0x0003D140
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0003D150 File Offset: 0x0003D150
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			base.Invalidate();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0003D160 File Offset: 0x0003D160
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.UpdateScrollBars();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0003D170 File Offset: 0x0003D170
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this._offsetMousePosition = new Point(e.X + this.Viewport.Left, e.Y + this.Viewport.Top);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0003D1C0 File Offset: 0x0003D1C0
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Right)
			{
				base.Select();
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0003D1E0 File Offset: 0x0003D1E0
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			bool flag = false;
			if (this._hScrollBar.Visible && Control.ModifierKeys == Keys.Control)
			{
				flag = true;
			}
			if (this._hScrollBar.Visible && !this._vScrollBar.Visible)
			{
				flag = true;
			}
			if (!flag)
			{
				if (e.Delta > 0)
				{
					this._vScrollBar.ScrollByPhysical(3);
					return;
				}
				if (e.Delta < 0)
				{
					this._vScrollBar.ScrollByPhysical(-3);
					return;
				}
			}
			else
			{
				if (e.Delta > 0)
				{
					this._hScrollBar.ScrollByPhysical(3);
					return;
				}
				if (e.Delta < 0)
				{
					this._hScrollBar.ScrollByPhysical(-3);
				}
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0003D2A8 File Offset: 0x0003D2A8
		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			base.OnPreviewKeyDown(e);
			Keys keyCode = e.KeyCode;
			if (keyCode - Keys.Left <= 3)
			{
				e.IsInputKey = true;
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0003D2D8 File Offset: 0x0003D2D8
		private void DragTimer_Tick(object sender, EventArgs e)
		{
			Point point = base.PointToClient(Control.MousePosition);
			int num = base.ClientRectangle.Right;
			int num2 = base.ClientRectangle.Bottom;
			if (this._vScrollBar.Visible)
			{
				num = this._vScrollBar.Left;
			}
			if (this._hScrollBar.Visible)
			{
				num2 = this._hScrollBar.Top;
			}
			if (this._vScrollBar.Visible)
			{
				if (point.Y < base.ClientRectangle.Top)
				{
					int num3 = (point.Y - base.ClientRectangle.Top) * -1;
					if (this.MaxDragChange > 0 && num3 > this.MaxDragChange)
					{
						num3 = this.MaxDragChange;
					}
					this._vScrollBar.Value = this._vScrollBar.Value - num3;
				}
				if (point.Y > num2)
				{
					int num4 = point.Y - num2;
					if (this.MaxDragChange > 0 && num4 > this.MaxDragChange)
					{
						num4 = this.MaxDragChange;
					}
					this._vScrollBar.Value = this._vScrollBar.Value + num4;
				}
			}
			if (this._hScrollBar.Visible)
			{
				if (point.X < base.ClientRectangle.Left)
				{
					int num5 = (point.X - base.ClientRectangle.Left) * -1;
					if (this.MaxDragChange > 0 && num5 > this.MaxDragChange)
					{
						num5 = this.MaxDragChange;
					}
					this._hScrollBar.Value = this._hScrollBar.Value - num5;
				}
				if (point.X > num)
				{
					int num6 = point.X - num;
					if (this.MaxDragChange > 0 && num6 > this.MaxDragChange)
					{
						num6 = this.MaxDragChange;
					}
					this._hScrollBar.Value = this._hScrollBar.Value + num6;
				}
			}
		}

		// Token: 0x0400050F RID: 1295
		protected readonly DarkScrollBar _vScrollBar;

		// Token: 0x04000510 RID: 1296
		protected readonly DarkScrollBar _hScrollBar;

		// Token: 0x04000511 RID: 1297
		private Size _visibleSize;

		// Token: 0x04000512 RID: 1298
		private Size _contentSize;

		// Token: 0x04000513 RID: 1299
		private Rectangle _viewport;

		// Token: 0x04000514 RID: 1300
		private Point _offsetMousePosition;

		// Token: 0x04000515 RID: 1301
		private int _maxDragChange;

		// Token: 0x04000516 RID: 1302
		private Timer _dragTimer;

		// Token: 0x04000517 RID: 1303
		private bool _hideScrollBars = true;
	}
}
