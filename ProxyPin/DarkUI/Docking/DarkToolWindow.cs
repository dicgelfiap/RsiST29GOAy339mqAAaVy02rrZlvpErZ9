using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Docking
{
	// Token: 0x0200008F RID: 143
	[ToolboxItem(false)]
	public class DarkToolWindow : DarkDockContent
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00035A90 File Offset: 0x00035A90
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00035A98 File Offset: 0x00035A98
		public DarkToolWindow()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			this.BackColor = Colors.GreyBackground;
			base.Padding = new Padding(0, 25, 0, 0);
			this.UpdateCloseButton();
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00035ADC File Offset: 0x00035ADC
		private bool IsActive()
		{
			return base.DockPanel != null && base.DockPanel.ActiveContent == this;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00035AFC File Offset: 0x00035AFC
		private void UpdateCloseButton()
		{
			this._headerRect = new Rectangle
			{
				X = base.ClientRectangle.Left,
				Y = base.ClientRectangle.Top,
				Width = base.ClientRectangle.Width,
				Height = 25
			};
			this._closeButtonRect = new Rectangle
			{
				X = base.ClientRectangle.Right - DockIcons.tw_close.Width - 5 - 3,
				Y = base.ClientRectangle.Top + 12 - DockIcons.tw_close.Height / 2,
				Width = DockIcons.tw_close.Width,
				Height = DockIcons.tw_close.Height
			};
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00035BE0 File Offset: 0x00035BE0
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.UpdateCloseButton();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00035BF0 File Offset: 0x00035BF0
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this._closeButtonRect.Contains(e.Location) || this._closeButtonPressed)
			{
				if (!this._closeButtonHot)
				{
					this._closeButtonHot = true;
					base.Invalidate();
					return;
				}
			}
			else
			{
				if (this._closeButtonHot)
				{
					this._closeButtonHot = false;
					base.Invalidate();
				}
				if (this._shouldDrag)
				{
					base.DockPanel.DragContent(this);
					return;
				}
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00035C74 File Offset: 0x00035C74
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this._closeButtonRect.Contains(e.Location))
			{
				this._closeButtonPressed = true;
				this._closeButtonHot = true;
				base.Invalidate();
				return;
			}
			if (this._headerRect.Contains(e.Location))
			{
				this._shouldDrag = true;
				return;
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00035CD8 File Offset: 0x00035CD8
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this._closeButtonRect.Contains(e.Location) && this._closeButtonPressed)
			{
				this.Close();
			}
			this._closeButtonPressed = false;
			this._closeButtonHot = false;
			this._shouldDrag = false;
			base.Invalidate();
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00035D34 File Offset: 0x00035D34
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			using (SolidBrush solidBrush = new SolidBrush(Colors.GreyBackground))
			{
				graphics.FillRectangle(solidBrush, base.ClientRectangle);
			}
			bool flag = this.IsActive();
			Color color = flag ? Colors.BlueBackground : Colors.HeaderBackground;
			Color color2 = flag ? Colors.DarkBlueBorder : Colors.DarkBorder;
			Color color3 = flag ? Colors.LightBlueBorder : Colors.LightBorder;
			using (SolidBrush solidBrush2 = new SolidBrush(color))
			{
				Rectangle rect = new Rectangle(0, 0, base.ClientRectangle.Width, 25);
				graphics.FillRectangle(solidBrush2, rect);
			}
			using (Pen pen = new Pen(color2))
			{
				graphics.DrawLine(pen, base.ClientRectangle.Left, 0, base.ClientRectangle.Right, 0);
				graphics.DrawLine(pen, base.ClientRectangle.Left, 24, base.ClientRectangle.Right, 24);
			}
			using (Pen pen2 = new Pen(color3))
			{
				graphics.DrawLine(pen2, base.ClientRectangle.Left, 1, base.ClientRectangle.Right, 1);
			}
			int num = 2;
			if (base.Icon != null)
			{
				graphics.DrawImageUnscaled(base.Icon, base.ClientRectangle.Left + 5, base.ClientRectangle.Top + 12 - base.Icon.Height / 2 + 1);
				num = base.Icon.Width + 8;
			}
			using (SolidBrush solidBrush3 = new SolidBrush(Colors.LightText))
			{
				Rectangle r = new Rectangle(num, 0, base.ClientRectangle.Width - 4 - num, 25);
				StringFormat format = new StringFormat
				{
					Alignment = StringAlignment.Near,
					LineAlignment = StringAlignment.Center,
					FormatFlags = StringFormatFlags.NoWrap,
					Trimming = StringTrimming.EllipsisCharacter
				};
				graphics.DrawString(base.DockText, this.Font, solidBrush3, r, format);
			}
			Bitmap image = this._closeButtonHot ? DockIcons.tw_close_selected : DockIcons.tw_close;
			if (flag)
			{
				image = (this._closeButtonHot ? DockIcons.tw_active_close_selected : DockIcons.tw_active_close);
			}
			graphics.DrawImageUnscaled(image, this._closeButtonRect.Left, this._closeButtonRect.Top);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00036030 File Offset: 0x00036030
		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		// Token: 0x04000487 RID: 1159
		private Rectangle _closeButtonRect;

		// Token: 0x04000488 RID: 1160
		private bool _closeButtonHot;

		// Token: 0x04000489 RID: 1161
		private bool _closeButtonPressed;

		// Token: 0x0400048A RID: 1162
		private Rectangle _headerRect;

		// Token: 0x0400048B RID: 1163
		private bool _shouldDrag;
	}
}
