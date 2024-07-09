using System;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A45 RID: 2629
	public class MarkerStyle : Style
	{
		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x0600674E RID: 26446 RVA: 0x001F7E74 File Offset: 0x001F7E74
		// (set) Token: 0x0600674F RID: 26447 RVA: 0x001F7E7C File Offset: 0x001F7E7C
		public Brush BackgroundBrush { get; set; }

		// Token: 0x06006750 RID: 26448 RVA: 0x001F7E88 File Offset: 0x001F7E88
		public MarkerStyle(Brush backgroundBrush)
		{
			this.BackgroundBrush = backgroundBrush;
			this.IsExportable = true;
		}

		// Token: 0x06006751 RID: 26449 RVA: 0x001F7EA4 File Offset: 0x001F7EA4
		public override void Draw(Graphics gr, Point position, Range range)
		{
			bool flag = this.BackgroundBrush != null;
			if (flag)
			{
				Rectangle rect = new Rectangle(position.X, position.Y, (range.End.iChar - range.Start.iChar) * range.tb.CharWidth, range.tb.CharHeight);
				bool flag2 = rect.Width == 0;
				if (!flag2)
				{
					gr.FillRectangle(this.BackgroundBrush, rect);
				}
			}
		}

		// Token: 0x06006752 RID: 26450 RVA: 0x001F7F30 File Offset: 0x001F7F30
		public override string GetCSS()
		{
			string text = "";
			bool flag = this.BackgroundBrush is SolidBrush;
			if (flag)
			{
				string colorAsString = ExportToHTML.GetColorAsString((this.BackgroundBrush as SolidBrush).Color);
				bool flag2 = colorAsString != "";
				if (flag2)
				{
					text = text + "background-color:" + colorAsString + ";";
				}
			}
			return text;
		}
	}
}
