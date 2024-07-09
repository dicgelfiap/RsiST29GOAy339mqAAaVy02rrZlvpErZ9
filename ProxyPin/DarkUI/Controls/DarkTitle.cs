using System;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000A5 RID: 165
	public class DarkTitle : Label
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x000391C0 File Offset: 0x000391C0
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
			SizeF sizeF = graphics.MeasureString(this.Text, this.Font);
			using (SolidBrush solidBrush = new SolidBrush(Colors.LightText))
			{
				graphics.DrawString(this.Text, this.Font, solidBrush, new PointF(-2f, 0f));
			}
			using (Pen pen = new Pen(Colors.GreyHighlight))
			{
				PointF pt = new PointF(sizeF.Width + 5f, sizeF.Height / 2f);
				PointF pt2 = new PointF((float)rectangle.Width, sizeF.Height / 2f);
				graphics.DrawLine(pen, pt, pt2);
			}
		}
	}
}
