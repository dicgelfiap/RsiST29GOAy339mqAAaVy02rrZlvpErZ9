using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A42 RID: 2626
	public class TextStyle : Style
	{
		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x0600673A RID: 26426 RVA: 0x001F753C File Offset: 0x001F753C
		// (set) Token: 0x0600673B RID: 26427 RVA: 0x001F7544 File Offset: 0x001F7544
		public Brush ForeBrush { get; set; }

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x0600673C RID: 26428 RVA: 0x001F7550 File Offset: 0x001F7550
		// (set) Token: 0x0600673D RID: 26429 RVA: 0x001F7558 File Offset: 0x001F7558
		public Brush BackgroundBrush { get; set; }

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x0600673E RID: 26430 RVA: 0x001F7564 File Offset: 0x001F7564
		// (set) Token: 0x0600673F RID: 26431 RVA: 0x001F756C File Offset: 0x001F756C
		public FontStyle FontStyle { get; set; }

		// Token: 0x06006740 RID: 26432 RVA: 0x001F7578 File Offset: 0x001F7578
		public TextStyle(Brush foreBrush, Brush backgroundBrush, FontStyle fontStyle)
		{
			this.ForeBrush = foreBrush;
			this.BackgroundBrush = backgroundBrush;
			this.FontStyle = fontStyle;
			this.stringFormat = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
		}

		// Token: 0x06006741 RID: 26433 RVA: 0x001F75BC File Offset: 0x001F75BC
		public override void Draw(Graphics gr, Point position, Range range)
		{
			bool flag = this.BackgroundBrush != null;
			if (flag)
			{
				gr.FillRectangle(this.BackgroundBrush, position.X, position.Y, (range.End.iChar - range.Start.iChar) * range.tb.CharWidth, range.tb.CharHeight);
			}
			using (Font font = new Font(range.tb.Font, this.FontStyle))
			{
				Line line = range.tb[range.Start.iLine];
				float num = (float)range.tb.CharWidth;
				float num2 = (float)(position.Y + range.tb.LineInterval / 2);
				float num3 = (float)(position.X - range.tb.CharWidth / 3);
				bool flag2 = this.ForeBrush == null;
				if (flag2)
				{
					this.ForeBrush = new SolidBrush(range.tb.ForeColor);
				}
				bool imeAllowed = range.tb.ImeAllowed;
				if (imeAllowed)
				{
					for (int i = range.Start.iChar; i < range.End.iChar; i++)
					{
						SizeF charSize = FastColoredTextBox.GetCharSize(font, line[i].c);
						GraphicsState gstate = gr.Save();
						float num4 = (charSize.Width > (float)(range.tb.CharWidth + 1)) ? ((float)range.tb.CharWidth / charSize.Width) : 1f;
						gr.TranslateTransform(num3, num2 + (1f - num4) * (float)range.tb.CharHeight / 2f);
						gr.ScaleTransform(num4, (float)Math.Sqrt((double)num4));
						Char @char = line[i];
						gr.DrawString(@char.c.ToString(), font, this.ForeBrush, 0f, 0f, this.stringFormat);
						gr.Restore(gstate);
						num3 += num;
					}
				}
				else
				{
					for (int j = range.Start.iChar; j < range.End.iChar; j++)
					{
						Char @char = line[j];
						gr.DrawString(@char.c.ToString(), font, this.ForeBrush, num3, num2, this.stringFormat);
						num3 += num;
					}
				}
			}
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x001F7870 File Offset: 0x001F7870
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
			bool flag3 = this.ForeBrush is SolidBrush;
			if (flag3)
			{
				string colorAsString2 = ExportToHTML.GetColorAsString((this.ForeBrush as SolidBrush).Color);
				bool flag4 = colorAsString2 != "";
				if (flag4)
				{
					text = text + "color:" + colorAsString2 + ";";
				}
			}
			bool flag5 = (this.FontStyle & FontStyle.Bold) > FontStyle.Regular;
			if (flag5)
			{
				text += "font-weight:bold;";
			}
			bool flag6 = (this.FontStyle & FontStyle.Italic) > FontStyle.Regular;
			if (flag6)
			{
				text += "font-style:oblique;";
			}
			bool flag7 = (this.FontStyle & FontStyle.Strikeout) > FontStyle.Regular;
			if (flag7)
			{
				text += "text-decoration:line-through;";
			}
			bool flag8 = (this.FontStyle & FontStyle.Underline) > FontStyle.Regular;
			if (flag8)
			{
				text += "text-decoration:underline;";
			}
			return text;
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x001F79BC File Offset: 0x001F79BC
		public override RTFStyleDescriptor GetRTF()
		{
			RTFStyleDescriptor rtfstyleDescriptor = new RTFStyleDescriptor();
			bool flag = this.BackgroundBrush is SolidBrush;
			if (flag)
			{
				rtfstyleDescriptor.BackColor = (this.BackgroundBrush as SolidBrush).Color;
			}
			bool flag2 = this.ForeBrush is SolidBrush;
			if (flag2)
			{
				rtfstyleDescriptor.ForeColor = (this.ForeBrush as SolidBrush).Color;
			}
			bool flag3 = (this.FontStyle & FontStyle.Bold) > FontStyle.Regular;
			if (flag3)
			{
				RTFStyleDescriptor rtfstyleDescriptor2 = rtfstyleDescriptor;
				rtfstyleDescriptor2.AdditionalTags += "\\b";
			}
			bool flag4 = (this.FontStyle & FontStyle.Italic) > FontStyle.Regular;
			if (flag4)
			{
				RTFStyleDescriptor rtfstyleDescriptor3 = rtfstyleDescriptor;
				rtfstyleDescriptor3.AdditionalTags += "\\i";
			}
			bool flag5 = (this.FontStyle & FontStyle.Strikeout) > FontStyle.Regular;
			if (flag5)
			{
				RTFStyleDescriptor rtfstyleDescriptor4 = rtfstyleDescriptor;
				rtfstyleDescriptor4.AdditionalTags += "\\strike";
			}
			bool flag6 = (this.FontStyle & FontStyle.Underline) > FontStyle.Regular;
			if (flag6)
			{
				RTFStyleDescriptor rtfstyleDescriptor5 = rtfstyleDescriptor;
				rtfstyleDescriptor5.AdditionalTags += "\\ul";
			}
			return rtfstyleDescriptor;
		}

		// Token: 0x040034B7 RID: 13495
		public StringFormat stringFormat;
	}
}
