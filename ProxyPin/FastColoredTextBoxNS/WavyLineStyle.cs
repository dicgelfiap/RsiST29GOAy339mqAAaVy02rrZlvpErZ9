using System;
using System.Collections.Generic;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A47 RID: 2631
	public class WavyLineStyle : Style
	{
		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06006755 RID: 26453 RVA: 0x001F8074 File Offset: 0x001F8074
		// (set) Token: 0x06006756 RID: 26454 RVA: 0x001F807C File Offset: 0x001F807C
		private Pen Pen { get; set; }

		// Token: 0x06006757 RID: 26455 RVA: 0x001F8088 File Offset: 0x001F8088
		public WavyLineStyle(int alpha, Color color)
		{
			this.Pen = new Pen(Color.FromArgb(alpha, color));
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x001F80A8 File Offset: 0x001F80A8
		public override void Draw(Graphics gr, Point pos, Range range)
		{
			Size sizeOfRange = Style.GetSizeOfRange(range);
			Point start = new Point(pos.X, pos.Y + sizeOfRange.Height - 1);
			Point end = new Point(pos.X + sizeOfRange.Width, pos.Y + sizeOfRange.Height - 1);
			this.DrawWavyLine(gr, start, end);
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x001F8114 File Offset: 0x001F8114
		private void DrawWavyLine(Graphics graphics, Point start, Point end)
		{
			bool flag = end.X - start.X < 2;
			if (flag)
			{
				graphics.DrawLine(this.Pen, start, end);
			}
			else
			{
				int num = -1;
				List<Point> list = new List<Point>();
				for (int i = start.X; i <= end.X; i += 2)
				{
					list.Add(new Point(i, start.Y + num));
					num = -num;
				}
				graphics.DrawLines(this.Pen, list.ToArray());
			}
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x001F81B0 File Offset: 0x001F81B0
		public override void Dispose()
		{
			base.Dispose();
			bool flag = this.Pen != null;
			if (flag)
			{
				this.Pen.Dispose();
			}
		}
	}
}
