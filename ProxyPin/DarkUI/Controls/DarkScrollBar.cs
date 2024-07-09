using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000AE RID: 174
	public class DarkScrollBar : Control
	{
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600071F RID: 1823 RVA: 0x0003DE08 File Offset: 0x0003DE08
		// (remove) Token: 0x06000720 RID: 1824 RVA: 0x0003DE44 File Offset: 0x0003DE44
		public event EventHandler<ScrollValueEventArgs> ValueChanged;

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0003DE80 File Offset: 0x0003DE80
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0003DE88 File Offset: 0x0003DE88
		[Category("Behavior")]
		[Description("The orientation type of the scrollbar.")]
		[DefaultValue(DarkScrollOrientation.Vertical)]
		public DarkScrollOrientation ScrollOrientation
		{
			get
			{
				return this._scrollOrientation;
			}
			set
			{
				this._scrollOrientation = value;
				this.UpdateScrollBar();
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0003DE98 File Offset: 0x0003DE98
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0003DEA0 File Offset: 0x0003DEA0
		[Category("Behavior")]
		[Description("The value that the scroll thumb position represents.")]
		[DefaultValue(0)]
		public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (value < this.Minimum)
				{
					value = this.Minimum;
				}
				int num = this.Maximum - this.ViewSize;
				if (value > num)
				{
					value = num;
				}
				if (this._value == value)
				{
					return;
				}
				this._value = value;
				this.UpdateThumb(true);
				if (this.ValueChanged != null)
				{
					this.ValueChanged(this, new ScrollValueEventArgs(this.Value));
				}
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0003DF1C File Offset: 0x0003DF1C
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0003DF24 File Offset: 0x0003DF24
		[Category("Behavior")]
		[Description("The lower limit value of the scrollable range.")]
		[DefaultValue(0)]
		public int Minimum
		{
			get
			{
				return this._minimum;
			}
			set
			{
				this._minimum = value;
				this.UpdateScrollBar();
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0003DF34 File Offset: 0x0003DF34
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0003DF3C File Offset: 0x0003DF3C
		[Category("Behavior")]
		[Description("The upper limit value of the scrollable range.")]
		[DefaultValue(100)]
		public int Maximum
		{
			get
			{
				return this._maximum;
			}
			set
			{
				this._maximum = value;
				this.UpdateScrollBar();
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0003DF4C File Offset: 0x0003DF4C
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0003DF54 File Offset: 0x0003DF54
		[Category("Behavior")]
		[Description("The view size for the scrollable area.")]
		[DefaultValue(0)]
		public int ViewSize
		{
			get
			{
				return this._viewSize;
			}
			set
			{
				this._viewSize = value;
				this.UpdateScrollBar();
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0003DF64 File Offset: 0x0003DF64
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0003DF6C File Offset: 0x0003DF6C
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				if (base.Visible == value)
				{
					return;
				}
				base.Visible = value;
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0003DF84 File Offset: 0x0003DF84
		public DarkScrollBar()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.Selectable, false);
			this._scrollTimer = new Timer();
			this._scrollTimer.Interval = 1;
			this._scrollTimer.Tick += this.ScrollTimerTick;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0003DFEC File Offset: 0x0003DFEC
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.UpdateScrollBar();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0003DFFC File Offset: 0x0003DFFC
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this._thumbArea.Contains(e.Location) && e.Button == MouseButtons.Left)
			{
				this._isScrolling = true;
				this._initialContact = e.Location;
				if (this._scrollOrientation == DarkScrollOrientation.Vertical)
				{
					this._initialValue = this._thumbArea.Top;
				}
				else
				{
					this._initialValue = this._thumbArea.Left;
				}
				base.Invalidate();
				return;
			}
			if (this._upArrowArea.Contains(e.Location) && e.Button == MouseButtons.Left)
			{
				this._upArrowClicked = true;
				this._scrollTimer.Enabled = true;
				base.Invalidate();
				return;
			}
			if (this._downArrowArea.Contains(e.Location) && e.Button == MouseButtons.Left)
			{
				this._downArrowClicked = true;
				this._scrollTimer.Enabled = true;
				base.Invalidate();
				return;
			}
			if (this._trackArea.Contains(e.Location) && e.Button == MouseButtons.Left)
			{
				if (this._scrollOrientation == DarkScrollOrientation.Vertical)
				{
					Rectangle rectangle = new Rectangle(this._thumbArea.Left, this._trackArea.Top, this._thumbArea.Width, this._trackArea.Height);
					if (!rectangle.Contains(e.Location))
					{
						return;
					}
				}
				else if (this._scrollOrientation == DarkScrollOrientation.Horizontal)
				{
					Rectangle rectangle2 = new Rectangle(this._trackArea.Left, this._thumbArea.Top, this._trackArea.Width, this._thumbArea.Height);
					if (!rectangle2.Contains(e.Location))
					{
						return;
					}
				}
				if (this._scrollOrientation == DarkScrollOrientation.Vertical)
				{
					int num = e.Location.Y;
					num -= this._upArrowArea.Bottom - 1;
					num -= this._thumbArea.Height / 2;
					this.ScrollToPhysical(num);
				}
				else
				{
					int num2 = e.Location.X;
					num2 -= this._upArrowArea.Right - 1;
					num2 -= this._thumbArea.Width / 2;
					this.ScrollToPhysical(num2);
				}
				this._isScrolling = true;
				this._initialContact = e.Location;
				this._thumbHot = true;
				if (this._scrollOrientation == DarkScrollOrientation.Vertical)
				{
					this._initialValue = this._thumbArea.Top;
				}
				else
				{
					this._initialValue = this._thumbArea.Left;
				}
				base.Invalidate();
				return;
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0003E2A4 File Offset: 0x0003E2A4
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this._isScrolling = false;
			this._thumbClicked = false;
			this._upArrowClicked = false;
			this._downArrowClicked = false;
			base.Invalidate();
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0003E2D0 File Offset: 0x0003E2D0
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (!this._isScrolling)
			{
				bool flag = this._thumbArea.Contains(e.Location);
				if (this._thumbHot != flag)
				{
					this._thumbHot = flag;
					base.Invalidate();
				}
				bool flag2 = this._upArrowArea.Contains(e.Location);
				if (this._upArrowHot != flag2)
				{
					this._upArrowHot = flag2;
					base.Invalidate();
				}
				bool flag3 = this._downArrowArea.Contains(e.Location);
				if (this._downArrowHot != flag3)
				{
					this._downArrowHot = flag3;
					base.Invalidate();
				}
			}
			if (this._isScrolling)
			{
				if (e.Button != MouseButtons.Left)
				{
					this.OnMouseUp(null);
					return;
				}
				Point point = new Point(e.Location.X - this._initialContact.X, e.Location.Y - this._initialContact.Y);
				if (this._scrollOrientation == DarkScrollOrientation.Vertical)
				{
					int positionInPixels = this._initialValue - this._trackArea.Top + point.Y;
					this.ScrollToPhysical(positionInPixels);
				}
				else if (this._scrollOrientation == DarkScrollOrientation.Horizontal)
				{
					int positionInPixels2 = this._initialValue - this._trackArea.Left + point.X;
					this.ScrollToPhysical(positionInPixels2);
				}
				this.UpdateScrollBar();
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0003E440 File Offset: 0x0003E440
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this._thumbHot = false;
			this._upArrowHot = false;
			this._downArrowHot = false;
			base.Invalidate();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0003E464 File Offset: 0x0003E464
		private void ScrollTimerTick(object sender, EventArgs e)
		{
			if (!this._upArrowClicked && !this._downArrowClicked)
			{
				this._scrollTimer.Enabled = false;
				return;
			}
			if (this._upArrowClicked)
			{
				this.ScrollBy(-1);
				return;
			}
			if (this._downArrowClicked)
			{
				this.ScrollBy(1);
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0003E4C0 File Offset: 0x0003E4C0
		public void ScrollTo(int position)
		{
			this.Value = position;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0003E4CC File Offset: 0x0003E4CC
		public void ScrollToPhysical(int positionInPixels)
		{
			int num = (this._scrollOrientation == DarkScrollOrientation.Vertical) ? (this._trackArea.Height - this._thumbArea.Height) : (this._trackArea.Width - this._thumbArea.Width);
			float num2 = (float)positionInPixels / (float)num;
			int num3 = this.Maximum - this.ViewSize;
			int value = (int)(num2 * (float)num3);
			this.Value = value;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0003E540 File Offset: 0x0003E540
		public void ScrollBy(int offset)
		{
			int position = this.Value + offset;
			this.ScrollTo(position);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0003E564 File Offset: 0x0003E564
		public void ScrollByPhysical(int offsetInPixels)
		{
			int positionInPixels = ((this._scrollOrientation == DarkScrollOrientation.Vertical) ? (this._thumbArea.Top - this._trackArea.Top) : (this._thumbArea.Left - this._trackArea.Left)) - offsetInPixels;
			this.ScrollToPhysical(positionInPixels);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0003E5C0 File Offset: 0x0003E5C0
		public void UpdateScrollBar()
		{
			Rectangle clientRectangle = base.ClientRectangle;
			if (this._scrollOrientation == DarkScrollOrientation.Vertical)
			{
				this._upArrowArea = new Rectangle(clientRectangle.Left, clientRectangle.Top, Consts.ArrowButtonSize, Consts.ArrowButtonSize);
				this._downArrowArea = new Rectangle(clientRectangle.Left, clientRectangle.Bottom - Consts.ArrowButtonSize, Consts.ArrowButtonSize, Consts.ArrowButtonSize);
			}
			else if (this._scrollOrientation == DarkScrollOrientation.Horizontal)
			{
				this._upArrowArea = new Rectangle(clientRectangle.Left, clientRectangle.Top, Consts.ArrowButtonSize, Consts.ArrowButtonSize);
				this._downArrowArea = new Rectangle(clientRectangle.Right - Consts.ArrowButtonSize, clientRectangle.Top, Consts.ArrowButtonSize, Consts.ArrowButtonSize);
			}
			if (this._scrollOrientation == DarkScrollOrientation.Vertical)
			{
				this._trackArea = new Rectangle(clientRectangle.Left, clientRectangle.Top + Consts.ArrowButtonSize, clientRectangle.Width, clientRectangle.Height - Consts.ArrowButtonSize * 2);
			}
			else if (this._scrollOrientation == DarkScrollOrientation.Horizontal)
			{
				this._trackArea = new Rectangle(clientRectangle.Left + Consts.ArrowButtonSize, clientRectangle.Top, clientRectangle.Width - Consts.ArrowButtonSize * 2, clientRectangle.Height);
			}
			this.UpdateThumb(false);
			base.Invalidate();
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0003E720 File Offset: 0x0003E720
		private void UpdateThumb(bool forceRefresh = false)
		{
			if (this.ViewSize >= this.Maximum)
			{
				return;
			}
			int num = this.Maximum - this.ViewSize;
			if (this.Value > num)
			{
				this.Value = num;
			}
			this._viewContentRatio = (float)this.ViewSize / (float)this.Maximum;
			int num2 = this.Maximum - this.ViewSize;
			float num3 = (float)this.Value / (float)num2;
			if (this._scrollOrientation == DarkScrollOrientation.Vertical)
			{
				int num4 = (int)((float)this._trackArea.Height * this._viewContentRatio);
				if (num4 < Consts.MinimumThumbSize)
				{
					num4 = Consts.MinimumThumbSize;
				}
				int num5 = (int)((float)(this._trackArea.Height - num4) * num3);
				this._thumbArea = new Rectangle(this._trackArea.Left + 3, this._trackArea.Top + num5, Consts.ScrollBarSize - 6, num4);
			}
			else if (this._scrollOrientation == DarkScrollOrientation.Horizontal)
			{
				int num6 = (int)((float)this._trackArea.Width * this._viewContentRatio);
				if (num6 < Consts.MinimumThumbSize)
				{
					num6 = Consts.MinimumThumbSize;
				}
				int num7 = (int)((float)(this._trackArea.Width - num6) * num3);
				this._thumbArea = new Rectangle(this._trackArea.Left + num7, this._trackArea.Top + 3, num6, Consts.ScrollBarSize - 6);
			}
			if (forceRefresh)
			{
				base.Invalidate();
				base.Update();
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0003E894 File Offset: 0x0003E894
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Bitmap bitmap = this._upArrowHot ? ScrollIcons.scrollbar_arrow_hot : ScrollIcons.scrollbar_arrow_standard;
			if (this._upArrowClicked)
			{
				bitmap = ScrollIcons.scrollbar_arrow_clicked;
			}
			if (!base.Enabled)
			{
				bitmap = ScrollIcons.scrollbar_arrow_disabled;
			}
			if (this._scrollOrientation == DarkScrollOrientation.Vertical)
			{
				bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
			}
			else if (this._scrollOrientation == DarkScrollOrientation.Horizontal)
			{
				bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
			}
			graphics.DrawImageUnscaled(bitmap, this._upArrowArea.Left + this._upArrowArea.Width / 2 - bitmap.Width / 2, this._upArrowArea.Top + this._upArrowArea.Height / 2 - bitmap.Height / 2);
			Bitmap bitmap2 = this._downArrowHot ? ScrollIcons.scrollbar_arrow_hot : ScrollIcons.scrollbar_arrow_standard;
			if (this._downArrowClicked)
			{
				bitmap2 = ScrollIcons.scrollbar_arrow_clicked;
			}
			if (!base.Enabled)
			{
				bitmap2 = ScrollIcons.scrollbar_arrow_disabled;
			}
			if (this._scrollOrientation == DarkScrollOrientation.Horizontal)
			{
				bitmap2.RotateFlip(RotateFlipType.Rotate270FlipNone);
			}
			graphics.DrawImageUnscaled(bitmap2, this._downArrowArea.Left + this._downArrowArea.Width / 2 - bitmap2.Width / 2, this._downArrowArea.Top + this._downArrowArea.Height / 2 - bitmap2.Height / 2);
			if (base.Enabled)
			{
				Color color = this._thumbHot ? Colors.GreyHighlight : Colors.GreySelection;
				if (this._isScrolling)
				{
					color = Colors.ActiveControl;
				}
				using (SolidBrush solidBrush = new SolidBrush(color))
				{
					graphics.FillRectangle(solidBrush, this._thumbArea);
				}
			}
		}

		// Token: 0x04000522 RID: 1314
		private DarkScrollOrientation _scrollOrientation;

		// Token: 0x04000523 RID: 1315
		private int _value;

		// Token: 0x04000524 RID: 1316
		private int _minimum;

		// Token: 0x04000525 RID: 1317
		private int _maximum = 100;

		// Token: 0x04000526 RID: 1318
		private int _viewSize;

		// Token: 0x04000527 RID: 1319
		private Rectangle _trackArea;

		// Token: 0x04000528 RID: 1320
		private float _viewContentRatio;

		// Token: 0x04000529 RID: 1321
		private Rectangle _thumbArea;

		// Token: 0x0400052A RID: 1322
		private Rectangle _upArrowArea;

		// Token: 0x0400052B RID: 1323
		private Rectangle _downArrowArea;

		// Token: 0x0400052C RID: 1324
		private bool _thumbHot;

		// Token: 0x0400052D RID: 1325
		private bool _upArrowHot;

		// Token: 0x0400052E RID: 1326
		private bool _downArrowHot;

		// Token: 0x0400052F RID: 1327
		private bool _thumbClicked;

		// Token: 0x04000530 RID: 1328
		private bool _upArrowClicked;

		// Token: 0x04000531 RID: 1329
		private bool _downArrowClicked;

		// Token: 0x04000532 RID: 1330
		private bool _isScrolling;

		// Token: 0x04000533 RID: 1331
		private int _initialValue;

		// Token: 0x04000534 RID: 1332
		private Point _initialContact;

		// Token: 0x04000535 RID: 1333
		private Timer _scrollTimer;
	}
}
