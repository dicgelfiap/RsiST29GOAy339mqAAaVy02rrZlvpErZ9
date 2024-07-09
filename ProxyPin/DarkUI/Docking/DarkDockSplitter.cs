using System;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Forms;

namespace DarkUI.Docking
{
	// Token: 0x02000090 RID: 144
	public class DarkDockSplitter
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00036034 File Offset: 0x00036034
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0003603C File Offset: 0x0003603C
		public Rectangle Bounds { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00036048 File Offset: 0x00036048
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00036050 File Offset: 0x00036050
		public Cursor ResizeCursor { get; private set; }

		// Token: 0x06000574 RID: 1396 RVA: 0x0003605C File Offset: 0x0003605C
		public DarkDockSplitter(Control parentControl, Control control, DarkSplitterType splitterType)
		{
			this._parentControl = parentControl;
			this._control = control;
			this._splitterType = splitterType;
			DarkSplitterType splitterType2 = this._splitterType;
			if (splitterType2 <= DarkSplitterType.Right)
			{
				this.ResizeCursor = Cursors.SizeWE;
				return;
			}
			if (splitterType2 - DarkSplitterType.Top > 1)
			{
				return;
			}
			this.ResizeCursor = Cursors.SizeNS;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000360B8 File Offset: 0x000360B8
		public void ShowOverlay()
		{
			this._overlayForm = new DarkTranslucentForm(Color.Black, 0.6);
			this._overlayForm.Visible = true;
			this.UpdateOverlay(new Point(0, 0));
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000360EC File Offset: 0x000360EC
		public void HideOverlay()
		{
			this._overlayForm.Visible = false;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000360FC File Offset: 0x000360FC
		public void UpdateOverlay(Point difference)
		{
			Rectangle bounds = new Rectangle(this.Bounds.Location, this.Bounds.Size);
			switch (this._splitterType)
			{
			case DarkSplitterType.Left:
			{
				int num = Math.Max(bounds.Location.X - difference.X, this._minimum);
				if (this._maximum != 0 && num > this._maximum)
				{
					num = this._maximum;
				}
				bounds.Location = new Point(num, bounds.Location.Y);
				break;
			}
			case DarkSplitterType.Right:
			{
				int num2 = Math.Max(bounds.Location.X - difference.X, this._minimum);
				if (this._maximum != 0 && num2 > this._maximum)
				{
					num2 = this._maximum;
				}
				bounds.Location = new Point(num2, bounds.Location.Y);
				break;
			}
			case DarkSplitterType.Top:
			{
				int num3 = Math.Max(bounds.Location.Y - difference.Y, this._minimum);
				if (this._maximum != 0 && num3 > this._maximum)
				{
					num3 = this._maximum;
				}
				bounds.Location = new Point(bounds.Location.X, num3);
				break;
			}
			case DarkSplitterType.Bottom:
			{
				int num4 = Math.Max(bounds.Location.Y - difference.Y, this._minimum);
				if (this._maximum != 0 && num4 > this._maximum)
				{
					int num3 = this._maximum;
				}
				bounds.Location = new Point(bounds.Location.X, num4);
				break;
			}
			}
			this._overlayForm.Bounds = bounds;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000362F4 File Offset: 0x000362F4
		public void Move(Point difference)
		{
			switch (this._splitterType)
			{
			case DarkSplitterType.Left:
				this._control.Width += difference.X;
				break;
			case DarkSplitterType.Right:
				this._control.Width -= difference.X;
				break;
			case DarkSplitterType.Top:
				this._control.Height += difference.Y;
				break;
			case DarkSplitterType.Bottom:
				this._control.Height -= difference.Y;
				break;
			}
			this.UpdateBounds();
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000363A0 File Offset: 0x000363A0
		public void UpdateBounds()
		{
			Rectangle rectangle = this._parentControl.RectangleToScreen(this._control.Bounds);
			switch (this._splitterType)
			{
			case DarkSplitterType.Left:
				this.Bounds = new Rectangle(rectangle.Left - 2, rectangle.Top, 5, rectangle.Height);
				this._maximum = rectangle.Right - 2 - this._control.MinimumSize.Width;
				return;
			case DarkSplitterType.Right:
				this.Bounds = new Rectangle(rectangle.Right - 2, rectangle.Top, 5, rectangle.Height);
				this._minimum = rectangle.Left - 2 + this._control.MinimumSize.Width;
				return;
			case DarkSplitterType.Top:
				this.Bounds = new Rectangle(rectangle.Left, rectangle.Top - 2, rectangle.Width, 5);
				this._maximum = rectangle.Bottom - 2 - this._control.MinimumSize.Height;
				return;
			case DarkSplitterType.Bottom:
				this.Bounds = new Rectangle(rectangle.Left, rectangle.Bottom - 2, rectangle.Width, 5);
				this._minimum = rectangle.Top - 2 + this._control.MinimumSize.Height;
				return;
			default:
				return;
			}
		}

		// Token: 0x0400048C RID: 1164
		private Control _parentControl;

		// Token: 0x0400048D RID: 1165
		private Control _control;

		// Token: 0x0400048E RID: 1166
		private DarkSplitterType _splitterType;

		// Token: 0x0400048F RID: 1167
		private int _minimum;

		// Token: 0x04000490 RID: 1168
		private int _maximum;

		// Token: 0x04000491 RID: 1169
		private DarkTranslucentForm _overlayForm;
	}
}
