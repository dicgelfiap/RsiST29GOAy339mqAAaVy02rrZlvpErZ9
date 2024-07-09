using System;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A46 RID: 2630
	public class ShortcutStyle : Style
	{
		// Token: 0x06006753 RID: 26451 RVA: 0x001F7FA4 File Offset: 0x001F7FA4
		public ShortcutStyle(Pen borderPen)
		{
			this.borderPen = borderPen;
		}

		// Token: 0x06006754 RID: 26452 RVA: 0x001F7FB8 File Offset: 0x001F7FB8
		public override void Draw(Graphics gr, Point position, Range range)
		{
			Point point = range.tb.PlaceToPoint(range.End);
			Rectangle rect = new Rectangle(point.X - 5, point.Y + range.tb.CharHeight - 2, 4, 3);
			gr.FillPath(Brushes.White, Style.GetRoundedRectangle(rect, 1));
			gr.DrawPath(this.borderPen, Style.GetRoundedRectangle(rect, 1));
			this.AddVisualMarker(range.tb, new StyleVisualMarker(new Rectangle(point.X - range.tb.CharWidth, point.Y, range.tb.CharWidth, range.tb.CharHeight), this));
		}

		// Token: 0x040034BB RID: 13499
		public Pen borderPen;
	}
}
