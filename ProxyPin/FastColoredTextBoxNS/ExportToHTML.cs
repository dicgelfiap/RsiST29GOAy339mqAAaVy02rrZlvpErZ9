using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A02 RID: 2562
	public class ExportToHTML
	{
		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06006293 RID: 25235 RVA: 0x001D6CB8 File Offset: 0x001D6CB8
		// (set) Token: 0x06006294 RID: 25236 RVA: 0x001D6CC0 File Offset: 0x001D6CC0
		public bool UseNbsp { get; set; }

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06006295 RID: 25237 RVA: 0x001D6CCC File Offset: 0x001D6CCC
		// (set) Token: 0x06006296 RID: 25238 RVA: 0x001D6CD4 File Offset: 0x001D6CD4
		public bool UseForwardNbsp { get; set; }

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06006297 RID: 25239 RVA: 0x001D6CE0 File Offset: 0x001D6CE0
		// (set) Token: 0x06006298 RID: 25240 RVA: 0x001D6CE8 File Offset: 0x001D6CE8
		public bool UseOriginalFont { get; set; }

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06006299 RID: 25241 RVA: 0x001D6CF4 File Offset: 0x001D6CF4
		// (set) Token: 0x0600629A RID: 25242 RVA: 0x001D6CFC File Offset: 0x001D6CFC
		public bool UseStyleTag { get; set; }

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x0600629B RID: 25243 RVA: 0x001D6D08 File Offset: 0x001D6D08
		// (set) Token: 0x0600629C RID: 25244 RVA: 0x001D6D10 File Offset: 0x001D6D10
		public bool UseBr { get; set; }

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x0600629D RID: 25245 RVA: 0x001D6D1C File Offset: 0x001D6D1C
		// (set) Token: 0x0600629E RID: 25246 RVA: 0x001D6D24 File Offset: 0x001D6D24
		public bool IncludeLineNumbers { get; set; }

		// Token: 0x0600629F RID: 25247 RVA: 0x001D6D30 File Offset: 0x001D6D30
		public ExportToHTML()
		{
			this.UseNbsp = true;
			this.UseOriginalFont = true;
			this.UseStyleTag = true;
			this.UseBr = true;
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x001D6D74 File Offset: 0x001D6D74
		public string GetHtml(FastColoredTextBox tb)
		{
			this.tb = tb;
			Range range = new Range(tb);
			range.SelectAll();
			return this.GetHtml(range);
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x001D6DAC File Offset: 0x001D6DAC
		public string GetHtml(Range r)
		{
			this.tb = r.tb;
			Dictionary<StyleIndex, object> dictionary = new Dictionary<StyleIndex, object>();
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StyleIndex styleIndex = StyleIndex.None;
			r.Normalize();
			int iLine = r.Start.iLine;
			dictionary[styleIndex] = null;
			bool useOriginalFont = this.UseOriginalFont;
			if (useOriginalFont)
			{
				stringBuilder.AppendFormat("<font style=\"font-family: {0}, monospace; font-size: {1}pt; line-height: {2}px;\">", r.tb.Font.Name, r.tb.Font.SizeInPoints, r.tb.CharHeight);
			}
			bool includeLineNumbers = this.IncludeLineNumbers;
			if (includeLineNumbers)
			{
				stringBuilder2.AppendFormat("<span class=lineNumber>{0}</span>  ", iLine + 1);
			}
			bool flag = false;
			foreach (Place place in ((IEnumerable<Place>)r))
			{
				Char @char = r.tb[place.iLine][place.iChar];
				bool flag2 = @char.style != styleIndex;
				if (flag2)
				{
					this.Flush(stringBuilder, stringBuilder2, styleIndex);
					styleIndex = @char.style;
					dictionary[styleIndex] = null;
				}
				bool flag3 = place.iLine != iLine;
				if (flag3)
				{
					for (int i = iLine; i < place.iLine; i++)
					{
						stringBuilder2.Append(this.UseBr ? "<br>" : "\r\n");
						bool includeLineNumbers2 = this.IncludeLineNumbers;
						if (includeLineNumbers2)
						{
							stringBuilder2.AppendFormat("<span class=lineNumber>{0}</span>  ", i + 2);
						}
					}
					iLine = place.iLine;
					flag = false;
				}
				char c = @char.c;
				if (c <= '&')
				{
					if (c != ' ')
					{
						if (c != '&')
						{
							goto IL_269;
						}
						stringBuilder2.Append("&amp;");
					}
					else
					{
						bool flag4 = (flag || !this.UseForwardNbsp) && !this.UseNbsp;
						if (flag4)
						{
							goto IL_269;
						}
						stringBuilder2.Append("&nbsp;");
					}
				}
				else if (c != '<')
				{
					if (c != '>')
					{
						goto IL_269;
					}
					stringBuilder2.Append("&gt;");
				}
				else
				{
					stringBuilder2.Append("&lt;");
				}
				continue;
				IL_269:
				flag = true;
				stringBuilder2.Append(@char.c);
			}
			this.Flush(stringBuilder, stringBuilder2, styleIndex);
			bool useOriginalFont2 = this.UseOriginalFont;
			if (useOriginalFont2)
			{
				stringBuilder.Append("</font>");
			}
			bool useStyleTag = this.UseStyleTag;
			if (useStyleTag)
			{
				stringBuilder2.Length = 0;
				stringBuilder2.Append("<style type=\"text/css\">");
				foreach (StyleIndex styleIndex2 in dictionary.Keys)
				{
					stringBuilder2.AppendFormat(".fctb{0}{{ {1} }}\r\n", this.GetStyleName(styleIndex2), this.GetCss(styleIndex2));
				}
				stringBuilder2.Append("</style>");
				stringBuilder.Insert(0, stringBuilder2.ToString());
			}
			bool includeLineNumbers3 = this.IncludeLineNumbers;
			if (includeLineNumbers3)
			{
				stringBuilder.Insert(0, this.LineNumbersCSS);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x001D7174 File Offset: 0x001D7174
		private string GetCss(StyleIndex styleIndex)
		{
			List<Style> list = new List<Style>();
			TextStyle textStyle = null;
			int num = 1;
			bool flag = false;
			for (int i = 0; i < this.tb.Styles.Length; i++)
			{
				bool flag2 = this.tb.Styles[i] != null && ((int)styleIndex & num) != 0;
				if (flag2)
				{
					bool isExportable = this.tb.Styles[i].IsExportable;
					if (isExportable)
					{
						Style style = this.tb.Styles[i];
						list.Add(style);
						bool flag3 = style is TextStyle;
						bool flag4 = flag3;
						if (flag4)
						{
							bool flag5 = !flag || this.tb.AllowSeveralTextStyleDrawing;
							if (flag5)
							{
								flag = true;
								textStyle = (style as TextStyle);
							}
						}
					}
				}
				num <<= 1;
			}
			string text = "";
			bool flag6 = !flag;
			if (flag6)
			{
				text = this.tb.DefaultStyle.GetCSS();
			}
			else
			{
				text = textStyle.GetCSS();
			}
			foreach (Style style2 in list)
			{
				bool flag7 = !(style2 is TextStyle);
				if (flag7)
				{
					text += style2.GetCSS();
				}
			}
			return text;
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x001D7318 File Offset: 0x001D7318
		public static string GetColorAsString(Color color)
		{
			bool flag = color == Color.Transparent;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Format("#{0:x2}{1:x2}{2:x2}", color.R, color.G, color.B);
			}
			return result;
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x001D737C File Offset: 0x001D737C
		private string GetStyleName(StyleIndex styleIndex)
		{
			return styleIndex.ToString().Replace(" ", "").Replace(",", "");
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x001D73C0 File Offset: 0x001D73C0
		private void Flush(StringBuilder sb, StringBuilder tempSB, StyleIndex currentStyle)
		{
			bool flag = tempSB.Length == 0;
			if (!flag)
			{
				bool useStyleTag = this.UseStyleTag;
				if (useStyleTag)
				{
					sb.AppendFormat("<font class=fctb{0}>{1}</font>", this.GetStyleName(currentStyle), tempSB.ToString());
				}
				else
				{
					string css = this.GetCss(currentStyle);
					bool flag2 = css != "";
					if (flag2)
					{
						sb.AppendFormat("<font style=\"{0}\">", css);
					}
					sb.Append(tempSB.ToString());
					bool flag3 = css != "";
					if (flag3)
					{
						sb.Append("</font>");
					}
				}
				tempSB.Length = 0;
			}
		}

		// Token: 0x04003240 RID: 12864
		public string LineNumbersCSS = "<style type=\"text/css\"> .lineNumber{font-family : monospace; font-size : small; font-style : normal; font-weight : normal; color : Teal; background-color : ThreedFace;} </style>";

		// Token: 0x04003247 RID: 12871
		private FastColoredTextBox tb;
	}
}
