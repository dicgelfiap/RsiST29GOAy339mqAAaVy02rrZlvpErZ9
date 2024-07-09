using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Security;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000A2 RID: 162
	public class DarkNumericUpDown : NumericUpDown
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000389C8 File Offset: 0x000389C8
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x000389D0 File Offset: 0x000389D0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color ForeColor { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000389DC File Offset: 0x000389DC
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x000389E4 File Offset: 0x000389E4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color BackColor { get; set; }

		// Token: 0x0600062D RID: 1581 RVA: 0x000389F0 File Offset: 0x000389F0
		public DarkNumericUpDown()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			base.ForeColor = Color.Gainsboro;
			base.BackColor = Colors.LightBackground;
			base.Controls[0].Paint += this.DarkNumericUpDown_Paint;
			try
			{
				Type type = base.Controls[0].GetType();
				BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
				MethodInfo method = type.GetMethod("SetStyle", bindingAttr);
				if (method != null)
				{
					object[] parameters = new object[]
					{
						ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer,
						true
					};
					method.Invoke(base.Controls[0], parameters);
				}
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00038ABC File Offset: 0x00038ABC
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.Invalidate();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00038AC4 File Offset: 0x00038AC4
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this._mouseDown = true;
			base.Invalidate();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00038AD4 File Offset: 0x00038AD4
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			this._mouseDown = false;
			base.Invalidate();
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00038AE4 File Offset: 0x00038AE4
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			base.Invalidate();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00038AF4 File Offset: 0x00038AF4
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			base.Invalidate();
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00038B04 File Offset: 0x00038B04
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00038B14 File Offset: 0x00038B14
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			base.Invalidate();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00038B24 File Offset: 0x00038B24
		protected override void OnTextBoxLostFocus(object source, EventArgs e)
		{
			base.OnTextBoxLostFocus(source, e);
			base.Invalidate();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00038B34 File Offset: 0x00038B34
		private void DarkNumericUpDown_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle clipRectangle = e.ClipRectangle;
			using (SolidBrush solidBrush = new SolidBrush(Colors.HeaderBackground))
			{
				graphics.FillRectangle(solidBrush, clipRectangle);
			}
			Point pt = base.Controls[0].PointToClient(Cursor.Position);
			Rectangle rectangle = new Rectangle(0, 0, clipRectangle.Width, clipRectangle.Height / 2);
			bool flag = rectangle.Contains(pt);
			Bitmap bitmap = flag ? ScrollIcons.scrollbar_arrow_small_hot : ScrollIcons.scrollbar_arrow_small_standard;
			if (flag && this._mouseDown)
			{
				bitmap = ScrollIcons.scrollbar_arrow_small_clicked;
			}
			bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
			graphics.DrawImageUnscaled(bitmap, rectangle.Width / 2 - bitmap.Width / 2, rectangle.Height / 2 - bitmap.Height / 2);
			Rectangle rectangle2 = new Rectangle(0, clipRectangle.Height / 2, clipRectangle.Width, clipRectangle.Height / 2);
			bool flag2 = rectangle2.Contains(pt);
			Bitmap bitmap2 = flag2 ? ScrollIcons.scrollbar_arrow_small_hot : ScrollIcons.scrollbar_arrow_small_standard;
			if (flag2 && this._mouseDown)
			{
				bitmap2 = ScrollIcons.scrollbar_arrow_small_clicked;
			}
			graphics.DrawImageUnscaled(bitmap2, rectangle2.Width / 2 - bitmap2.Width / 2, rectangle2.Top + rectangle2.Height / 2 - bitmap2.Height / 2);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00038CB0 File Offset: 0x00038CB0
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
			Color color = Colors.GreySelection;
			if (this.Focused && base.TabStop)
			{
				color = Colors.BlueHighlight;
			}
			using (Pen pen = new Pen(color, 1f))
			{
				Rectangle rect = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width - 1, rectangle.Height - 1);
				graphics.DrawRectangle(pen, rect);
			}
		}

		// Token: 0x040004DB RID: 1243
		private bool _mouseDown;
	}
}
