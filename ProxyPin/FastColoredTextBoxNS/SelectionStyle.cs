using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A44 RID: 2628
	public class SelectionStyle : Style
	{
		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06006746 RID: 26438 RVA: 0x001F7CB8 File Offset: 0x001F7CB8
		// (set) Token: 0x06006747 RID: 26439 RVA: 0x001F7CC0 File Offset: 0x001F7CC0
		public Brush BackgroundBrush { get; set; }

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06006748 RID: 26440 RVA: 0x001F7CCC File Offset: 0x001F7CCC
		// (set) Token: 0x06006749 RID: 26441 RVA: 0x001F7CD4 File Offset: 0x001F7CD4
		public Brush ForegroundBrush { get; private set; }

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x0600674A RID: 26442 RVA: 0x001F7CE0 File Offset: 0x001F7CE0
		// (set) Token: 0x0600674B RID: 26443 RVA: 0x001F7CFC File Offset: 0x001F7CFC
		public override bool IsExportable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x001F7D00 File Offset: 0x001F7D00
		public SelectionStyle(Brush backgroundBrush, Brush foregroundBrush = null)
		{
			this.BackgroundBrush = backgroundBrush;
			this.ForegroundBrush = foregroundBrush;
		}

		// Token: 0x0600674D RID: 26445 RVA: 0x001F7D1C File Offset: 0x001F7D1C
		public override void Draw(Graphics gr, Point position, Range range)
		{
			bool flag = this.BackgroundBrush != null;
			if (flag)
			{
				gr.SmoothingMode = SmoothingMode.None;
				Rectangle rect = new Rectangle(position.X, position.Y, (range.End.iChar - range.Start.iChar) * range.tb.CharWidth, range.tb.CharHeight);
				bool flag2 = rect.Width == 0;
				if (!flag2)
				{
					gr.FillRectangle(this.BackgroundBrush, rect);
					bool flag3 = this.ForegroundBrush != null;
					if (flag3)
					{
						gr.SmoothingMode = SmoothingMode.AntiAlias;
						Range range2 = new Range(range.tb, range.Start.iChar, range.Start.iLine, Math.Min(range.tb[range.End.iLine].Count, range.End.iChar), range.End.iLine);
						using (TextStyle textStyle = new TextStyle(this.ForegroundBrush, null, FontStyle.Regular))
						{
							textStyle.Draw(gr, new Point(position.X, position.Y - 1), range2);
						}
					}
				}
			}
		}
	}
}
