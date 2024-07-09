using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000A1 RID: 161
	public class DarkGroupBox : GroupBox
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00038774 File Offset: 0x00038774
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0003877C File Offset: 0x0003877C
		[Category("Appearance")]
		[Description("Determines the color of the border.")]
		public Color BorderColor
		{
			get
			{
				return this._borderColor;
			}
			set
			{
				this._borderColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0003878C File Offset: 0x0003878C
		public DarkGroupBox()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			base.ResizeRedraw = true;
			this.DoubleBuffered = true;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000387C8 File Offset: 0x000387C8
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rect = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
			SizeF sizeF = graphics.MeasureString(this.Text, this.Font);
			Color lightText = Colors.LightText;
			Color greyBackground = Colors.GreyBackground;
			using (SolidBrush solidBrush = new SolidBrush(greyBackground))
			{
				graphics.FillRectangle(solidBrush, rect);
			}
			using (Pen pen = new Pen(this.BorderColor, 1f))
			{
				Rectangle rect2 = new Rectangle(0, (int)sizeF.Height / 2, rect.Width - 1, rect.Height - (int)sizeF.Height / 2 - 1);
				graphics.DrawRectangle(pen, rect2);
			}
			Rectangle r = new Rectangle(rect.Left + Consts.Padding, rect.Top, rect.Width - Consts.Padding * 2, (int)sizeF.Height);
			using (SolidBrush solidBrush2 = new SolidBrush(greyBackground))
			{
				Rectangle rect3 = new Rectangle(r.Left, r.Top, Math.Min(r.Width, (int)sizeF.Width), r.Height);
				graphics.FillRectangle(solidBrush2, rect3);
			}
			using (SolidBrush solidBrush3 = new SolidBrush(lightText))
			{
				StringFormat format = new StringFormat
				{
					LineAlignment = StringAlignment.Center,
					Alignment = StringAlignment.Near,
					FormatFlags = StringFormatFlags.NoWrap,
					Trimming = StringTrimming.EllipsisCharacter
				};
				graphics.DrawString(this.Text, this.Font, solidBrush3, r, format);
			}
		}

		// Token: 0x040004D8 RID: 1240
		private Color _borderColor = Colors.DarkBorder;
	}
}
