using System;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A43 RID: 2627
	public class FoldedBlockStyle : TextStyle
	{
		// Token: 0x06006744 RID: 26436 RVA: 0x001F7AE0 File Offset: 0x001F7AE0
		public FoldedBlockStyle(Brush foreBrush, Brush backgroundBrush, FontStyle fontStyle) : base(foreBrush, backgroundBrush, fontStyle)
		{
		}

		// Token: 0x06006745 RID: 26437 RVA: 0x001F7AF0 File Offset: 0x001F7AF0
		public override void Draw(Graphics gr, Point position, Range range)
		{
			bool flag = range.End.iChar > range.Start.iChar;
			if (flag)
			{
				base.Draw(gr, position, range);
				int num = position.X;
				for (int i = range.Start.iChar; i < range.End.iChar; i++)
				{
					bool flag2 = range.tb[range.Start.iLine][i].c != ' ';
					if (flag2)
					{
						break;
					}
					num += range.tb.CharWidth;
				}
				range.tb.AddVisualMarker(new FoldedAreaMarker(range.Start.iLine, new Rectangle(num, position.Y, position.X + (range.End.iChar - range.Start.iChar) * range.tb.CharWidth - num, range.tb.CharHeight)));
			}
			else
			{
				using (Font font = new Font(range.tb.Font, base.FontStyle))
				{
					gr.DrawString("...", font, base.ForeBrush, (float)range.tb.LeftIndent, (float)(position.Y - 2));
				}
				range.tb.AddVisualMarker(new FoldedAreaMarker(range.Start.iLine, new Rectangle(range.tb.LeftIndent + 2, position.Y, 2 * range.tb.CharHeight, range.tb.CharHeight)));
			}
		}
	}
}
