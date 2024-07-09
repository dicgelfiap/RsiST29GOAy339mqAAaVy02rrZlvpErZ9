using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A03 RID: 2563
	public class ExportToRTF
	{
		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x060062A6 RID: 25254 RVA: 0x001D7470 File Offset: 0x001D7470
		// (set) Token: 0x060062A7 RID: 25255 RVA: 0x001D7478 File Offset: 0x001D7478
		public bool IncludeLineNumbers { get; set; }

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x060062A8 RID: 25256 RVA: 0x001D7484 File Offset: 0x001D7484
		// (set) Token: 0x060062A9 RID: 25257 RVA: 0x001D748C File Offset: 0x001D748C
		public bool UseOriginalFont { get; set; }

		// Token: 0x060062AA RID: 25258 RVA: 0x001D7498 File Offset: 0x001D7498
		public ExportToRTF()
		{
			this.UseOriginalFont = true;
		}

		// Token: 0x060062AB RID: 25259 RVA: 0x001D74B8 File Offset: 0x001D74B8
		public string GetRtf(FastColoredTextBox tb)
		{
			this.tb = tb;
			Range range = new Range(tb);
			range.SelectAll();
			return this.GetRtf(range);
		}

		// Token: 0x060062AC RID: 25260 RVA: 0x001D74F0 File Offset: 0x001D74F0
		public string GetRtf(Range r)
		{
			this.tb = r.tb;
			Dictionary<StyleIndex, object> dictionary = new Dictionary<StyleIndex, object>();
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StyleIndex styleIndex = StyleIndex.None;
			r.Normalize();
			int iLine = r.Start.iLine;
			dictionary[styleIndex] = null;
			this.colorTable.Clear();
			int colorTableNumber = this.GetColorTableNumber(r.tb.LineNumberColor);
			bool includeLineNumbers = this.IncludeLineNumbers;
			if (includeLineNumbers)
			{
				stringBuilder2.AppendFormat("{{\\cf{1} {0}}}\\tab", iLine + 1, colorTableNumber);
			}
			foreach (Place place in ((IEnumerable<Place>)r))
			{
				Char @char = r.tb[place.iLine][place.iChar];
				bool flag = @char.style != styleIndex;
				if (flag)
				{
					this.Flush(stringBuilder, stringBuilder2, styleIndex);
					styleIndex = @char.style;
					dictionary[styleIndex] = null;
				}
				bool flag2 = place.iLine != iLine;
				if (flag2)
				{
					for (int i = iLine; i < place.iLine; i++)
					{
						stringBuilder2.AppendLine("\\line");
						bool includeLineNumbers2 = this.IncludeLineNumbers;
						if (includeLineNumbers2)
						{
							stringBuilder2.AppendFormat("{{\\cf{1} {0}}}\\tab", i + 2, colorTableNumber);
						}
					}
					iLine = place.iLine;
				}
				char c = @char.c;
				if (c != '\\')
				{
					if (c != '{')
					{
						if (c != '}')
						{
							char c2 = @char.c;
							int num = (int)c2;
							bool flag3 = num < 128;
							if (flag3)
							{
								stringBuilder2.Append(@char.c);
							}
							else
							{
								stringBuilder2.AppendFormat("{{\\u{0}}}", num);
							}
						}
						else
						{
							stringBuilder2.Append("\\}");
						}
					}
					else
					{
						stringBuilder2.Append("\\{");
					}
				}
				else
				{
					stringBuilder2.Append("\\\\");
				}
			}
			this.Flush(stringBuilder, stringBuilder2, styleIndex);
			SortedList<int, Color> sortedList = new SortedList<int, Color>();
			foreach (KeyValuePair<Color, int> keyValuePair in this.colorTable)
			{
				sortedList.Add(keyValuePair.Value, keyValuePair.Key);
			}
			stringBuilder2.Length = 0;
			stringBuilder2.AppendFormat("{{\\colortbl;", new object[0]);
			foreach (KeyValuePair<int, Color> keyValuePair2 in sortedList)
			{
				stringBuilder2.Append(ExportToRTF.GetColorAsString(keyValuePair2.Value) + ";");
			}
			stringBuilder2.AppendLine("}");
			bool useOriginalFont = this.UseOriginalFont;
			if (useOriginalFont)
			{
				stringBuilder.Insert(0, string.Format("{{\\fonttbl{{\\f0\\fmodern {0};}}}}{{\\fs{1} ", this.tb.Font.Name, (int)(2f * this.tb.Font.SizeInPoints), this.tb.CharHeight));
				stringBuilder.AppendLine("}");
			}
			stringBuilder.Insert(0, stringBuilder2.ToString());
			stringBuilder.Insert(0, "{\\rtf1\\ud\\deff0");
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060062AD RID: 25261 RVA: 0x001D7904 File Offset: 0x001D7904
		private RTFStyleDescriptor GetRtfDescriptor(StyleIndex styleIndex)
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
			bool flag6 = !flag;
			RTFStyleDescriptor rtf;
			if (flag6)
			{
				rtf = this.tb.DefaultStyle.GetRTF();
			}
			else
			{
				rtf = textStyle.GetRTF();
			}
			return rtf;
		}

		// Token: 0x060062AE RID: 25262 RVA: 0x001D7A3C File Offset: 0x001D7A3C
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
				result = string.Format("\\red{0}\\green{1}\\blue{2}", color.R, color.G, color.B);
			}
			return result;
		}

		// Token: 0x060062AF RID: 25263 RVA: 0x001D7AA0 File Offset: 0x001D7AA0
		private void Flush(StringBuilder sb, StringBuilder tempSB, StyleIndex currentStyle)
		{
			bool flag = tempSB.Length == 0;
			if (!flag)
			{
				RTFStyleDescriptor rtfDescriptor = this.GetRtfDescriptor(currentStyle);
				int colorTableNumber = this.GetColorTableNumber(rtfDescriptor.ForeColor);
				int colorTableNumber2 = this.GetColorTableNumber(rtfDescriptor.BackColor);
				StringBuilder stringBuilder = new StringBuilder();
				bool flag2 = colorTableNumber >= 0;
				if (flag2)
				{
					stringBuilder.AppendFormat("\\cf{0}", colorTableNumber);
				}
				bool flag3 = colorTableNumber2 >= 0;
				if (flag3)
				{
					stringBuilder.AppendFormat("\\highlight{0}", colorTableNumber2);
				}
				bool flag4 = !string.IsNullOrEmpty(rtfDescriptor.AdditionalTags);
				if (flag4)
				{
					stringBuilder.Append(rtfDescriptor.AdditionalTags.Trim());
				}
				bool flag5 = stringBuilder.Length > 0;
				if (flag5)
				{
					sb.AppendFormat("{{{0} {1}}}", stringBuilder, tempSB.ToString());
				}
				else
				{
					sb.Append(tempSB.ToString());
				}
				tempSB.Length = 0;
			}
		}

		// Token: 0x060062B0 RID: 25264 RVA: 0x001D7BA4 File Offset: 0x001D7BA4
		private int GetColorTableNumber(Color color)
		{
			bool flag = color.A == 0;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				bool flag2 = !this.colorTable.ContainsKey(color);
				if (flag2)
				{
					this.colorTable[color] = this.colorTable.Count + 1;
				}
				result = this.colorTable[color];
			}
			return result;
		}

		// Token: 0x0400324A RID: 12874
		private FastColoredTextBox tb;

		// Token: 0x0400324B RID: 12875
		private Dictionary<Color, int> colorTable = new Dictionary<Color, int>();
	}
}
