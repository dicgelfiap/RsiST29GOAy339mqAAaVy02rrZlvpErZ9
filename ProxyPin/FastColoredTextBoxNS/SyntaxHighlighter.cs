using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A15 RID: 2581
	public class SyntaxHighlighter : IDisposable
	{
		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x06006353 RID: 25427 RVA: 0x001DB3CC File Offset: 0x001DB3CC
		public static RegexOptions RegexCompiledOption
		{
			get
			{
				bool flag = SyntaxHighlighter.platformType == Platform.X86;
				RegexOptions result;
				if (flag)
				{
					result = RegexOptions.Compiled;
				}
				else
				{
					result = RegexOptions.None;
				}
				return result;
			}
		}

		// Token: 0x06006354 RID: 25428 RVA: 0x001DB3FC File Offset: 0x001DB3FC
		public SyntaxHighlighter(FastColoredTextBox currentTb)
		{
			this.currentTb = currentTb;
		}

		// Token: 0x06006355 RID: 25429 RVA: 0x001DB4E4 File Offset: 0x001DB4E4
		public void Dispose()
		{
			foreach (SyntaxDescriptor syntaxDescriptor in this.descByXMLfileNames.Values)
			{
				syntaxDescriptor.Dispose();
			}
		}

		// Token: 0x06006356 RID: 25430 RVA: 0x001DB548 File Offset: 0x001DB548
		public virtual void HighlightSyntax(Language language, Range range)
		{
			switch (language)
			{
			case Language.CSharp:
				this.CSharpSyntaxHighlight(range);
				break;
			case Language.VB:
				this.VBSyntaxHighlight(range);
				break;
			case Language.HTML:
				this.HTMLSyntaxHighlight(range);
				break;
			case Language.XML:
				this.XMLSyntaxHighlight(range);
				break;
			case Language.SQL:
				this.SQLSyntaxHighlight(range);
				break;
			case Language.PHP:
				this.PHPSyntaxHighlight(range);
				break;
			case Language.JS:
				this.JScriptSyntaxHighlight(range);
				break;
			case Language.Lua:
				this.LuaSyntaxHighlight(range);
				break;
			}
		}

		// Token: 0x06006357 RID: 25431 RVA: 0x001DB5F8 File Offset: 0x001DB5F8
		public virtual void HighlightSyntax(string XMLdescriptionFile, Range range)
		{
			SyntaxDescriptor syntaxDescriptor = null;
			bool flag = !this.descByXMLfileNames.TryGetValue(XMLdescriptionFile, out syntaxDescriptor);
			if (flag)
			{
				XmlDocument xmlDocument = new XmlDocument();
				string path = XMLdescriptionFile;
				bool flag2 = !File.Exists(path);
				if (flag2)
				{
					path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(path));
				}
				xmlDocument.LoadXml(File.ReadAllText(path));
				syntaxDescriptor = SyntaxHighlighter.ParseXmlDescription(xmlDocument);
				this.descByXMLfileNames[XMLdescriptionFile] = syntaxDescriptor;
			}
			this.HighlightSyntax(syntaxDescriptor, range);
		}

		// Token: 0x06006358 RID: 25432 RVA: 0x001DB684 File Offset: 0x001DB684
		public virtual void AutoIndentNeeded(object sender, AutoIndentEventArgs args)
		{
			FastColoredTextBox fastColoredTextBox = sender as FastColoredTextBox;
			switch (fastColoredTextBox.Language)
			{
			case Language.CSharp:
				this.CSharpAutoIndentNeeded(sender, args);
				break;
			case Language.VB:
				this.VBAutoIndentNeeded(sender, args);
				break;
			case Language.HTML:
				this.HTMLAutoIndentNeeded(sender, args);
				break;
			case Language.XML:
				this.XMLAutoIndentNeeded(sender, args);
				break;
			case Language.SQL:
				this.SQLAutoIndentNeeded(sender, args);
				break;
			case Language.PHP:
				this.PHPAutoIndentNeeded(sender, args);
				break;
			case Language.JS:
				this.CSharpAutoIndentNeeded(sender, args);
				break;
			case Language.Lua:
				this.LuaAutoIndentNeeded(sender, args);
				break;
			}
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x001DB748 File Offset: 0x001DB748
		protected void PHPAutoIndentNeeded(object sender, AutoIndentEventArgs args)
		{
			bool flag = Regex.IsMatch(args.LineText, "^[^\"']*\\{.*\\}[^\"']*$");
			if (!flag)
			{
				bool flag2 = Regex.IsMatch(args.LineText, "^[^\"']*\\{");
				if (flag2)
				{
					args.ShiftNextLines = args.TabLength;
				}
				else
				{
					bool flag3 = Regex.IsMatch(args.LineText, "}[^\"']*$");
					if (flag3)
					{
						args.Shift = -args.TabLength;
						args.ShiftNextLines = -args.TabLength;
					}
					else
					{
						bool flag4 = Regex.IsMatch(args.PrevLineText, "^\\s*(if|for|foreach|while|[\\}\\s]*else)\\b[^{]*$");
						if (flag4)
						{
							bool flag5 = !Regex.IsMatch(args.PrevLineText, "(;\\s*$)|(;\\s*//)");
							if (flag5)
							{
								args.Shift = args.TabLength;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600635A RID: 25434 RVA: 0x001DB820 File Offset: 0x001DB820
		protected void SQLAutoIndentNeeded(object sender, AutoIndentEventArgs args)
		{
			FastColoredTextBox fastColoredTextBox = sender as FastColoredTextBox;
			fastColoredTextBox.CalcAutoIndentShiftByCodeFolding(sender, args);
		}

		// Token: 0x0600635B RID: 25435 RVA: 0x001DB844 File Offset: 0x001DB844
		protected void HTMLAutoIndentNeeded(object sender, AutoIndentEventArgs args)
		{
			FastColoredTextBox fastColoredTextBox = sender as FastColoredTextBox;
			fastColoredTextBox.CalcAutoIndentShiftByCodeFolding(sender, args);
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x001DB868 File Offset: 0x001DB868
		protected void XMLAutoIndentNeeded(object sender, AutoIndentEventArgs args)
		{
			FastColoredTextBox fastColoredTextBox = sender as FastColoredTextBox;
			fastColoredTextBox.CalcAutoIndentShiftByCodeFolding(sender, args);
		}

		// Token: 0x0600635D RID: 25437 RVA: 0x001DB88C File Offset: 0x001DB88C
		protected void VBAutoIndentNeeded(object sender, AutoIndentEventArgs args)
		{
			bool flag = Regex.IsMatch(args.LineText, "^\\s*(End|EndIf|Next|Loop)\\b", RegexOptions.IgnoreCase);
			if (flag)
			{
				args.Shift = -args.TabLength;
				args.ShiftNextLines = -args.TabLength;
			}
			else
			{
				bool flag2 = Regex.IsMatch(args.LineText, "\\b(Class|Property|Enum|Structure|Sub|Function|Namespace|Interface|Get)\\b|(Set\\s*\\()", RegexOptions.IgnoreCase);
				if (flag2)
				{
					args.ShiftNextLines = args.TabLength;
				}
				else
				{
					bool flag3 = Regex.IsMatch(args.LineText, "\\b(Then)\\s*\\S+", RegexOptions.IgnoreCase);
					if (!flag3)
					{
						bool flag4 = Regex.IsMatch(args.LineText, "^\\s*(If|While|For|Do|Try|With|Using|Select)\\b", RegexOptions.IgnoreCase);
						if (flag4)
						{
							args.ShiftNextLines = args.TabLength;
						}
						else
						{
							bool flag5 = Regex.IsMatch(args.LineText, "^\\s*(Else|ElseIf|Case|Catch|Finally)\\b", RegexOptions.IgnoreCase);
							if (flag5)
							{
								args.Shift = -args.TabLength;
							}
							else
							{
								bool flag6 = args.PrevLineText.TrimEnd(new char[0]).EndsWith("_");
								if (flag6)
								{
									args.Shift = args.TabLength;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x001DB9B0 File Offset: 0x001DB9B0
		protected void CSharpAutoIndentNeeded(object sender, AutoIndentEventArgs args)
		{
			bool flag = Regex.IsMatch(args.LineText, "^[^\"']*\\{.*\\}[^\"']*$");
			if (!flag)
			{
				bool flag2 = Regex.IsMatch(args.LineText, "^[^\"']*\\{");
				if (flag2)
				{
					args.ShiftNextLines = args.TabLength;
				}
				else
				{
					bool flag3 = Regex.IsMatch(args.LineText, "}[^\"']*$");
					if (flag3)
					{
						args.Shift = -args.TabLength;
						args.ShiftNextLines = -args.TabLength;
					}
					else
					{
						bool flag4 = Regex.IsMatch(args.LineText, "^\\s*\\w+\\s*:\\s*($|//)") && !Regex.IsMatch(args.LineText, "^\\s*default\\s*:");
						if (flag4)
						{
							args.Shift = -args.TabLength;
						}
						else
						{
							bool flag5 = Regex.IsMatch(args.LineText, "^\\s*(case|default)\\b.*:\\s*($|//)");
							if (flag5)
							{
								args.Shift = -args.TabLength / 2;
							}
							else
							{
								bool flag6 = Regex.IsMatch(args.PrevLineText, "^\\s*(if|for|foreach|while|[\\}\\s]*else)\\b[^{]*$");
								if (flag6)
								{
									bool flag7 = !Regex.IsMatch(args.PrevLineText, "(;\\s*$)|(;\\s*//)");
									if (flag7)
									{
										args.Shift = args.TabLength;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x001DBB04 File Offset: 0x001DBB04
		public virtual void AddXmlDescription(string descriptionFileName, XmlDocument doc)
		{
			SyntaxDescriptor value = SyntaxHighlighter.ParseXmlDescription(doc);
			this.descByXMLfileNames[descriptionFileName] = value;
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x001DBB2C File Offset: 0x001DBB2C
		public virtual void AddResilientStyle(Style style)
		{
			bool flag = this.resilientStyles.Contains(style);
			if (!flag)
			{
				this.currentTb.CheckStylesBufferSize();
				this.resilientStyles.Add(style);
			}
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x001DBB70 File Offset: 0x001DBB70
		public static SyntaxDescriptor ParseXmlDescription(XmlDocument doc)
		{
			SyntaxDescriptor syntaxDescriptor = new SyntaxDescriptor();
			XmlNode xmlNode = doc.SelectSingleNode("doc/brackets");
			bool flag = xmlNode != null;
			if (flag)
			{
				bool flag2 = xmlNode.Attributes["left"] == null || xmlNode.Attributes["right"] == null || xmlNode.Attributes["left"].Value == "" || xmlNode.Attributes["right"].Value == "";
				if (flag2)
				{
					syntaxDescriptor.leftBracket = '\0';
					syntaxDescriptor.rightBracket = '\0';
				}
				else
				{
					syntaxDescriptor.leftBracket = xmlNode.Attributes["left"].Value[0];
					syntaxDescriptor.rightBracket = xmlNode.Attributes["right"].Value[0];
				}
				bool flag3 = xmlNode.Attributes["left2"] == null || xmlNode.Attributes["right2"] == null || xmlNode.Attributes["left2"].Value == "" || xmlNode.Attributes["right2"].Value == "";
				if (flag3)
				{
					syntaxDescriptor.leftBracket2 = '\0';
					syntaxDescriptor.rightBracket2 = '\0';
				}
				else
				{
					syntaxDescriptor.leftBracket2 = xmlNode.Attributes["left2"].Value[0];
					syntaxDescriptor.rightBracket2 = xmlNode.Attributes["right2"].Value[0];
				}
				bool flag4 = xmlNode.Attributes["strategy"] == null || xmlNode.Attributes["strategy"].Value == "";
				if (flag4)
				{
					syntaxDescriptor.bracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
				}
				else
				{
					syntaxDescriptor.bracketsHighlightStrategy = (BracketsHighlightStrategy)Enum.Parse(typeof(BracketsHighlightStrategy), xmlNode.Attributes["strategy"].Value);
				}
			}
			Dictionary<string, Style> dictionary = new Dictionary<string, Style>();
			foreach (object obj in doc.SelectNodes("doc/style"))
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				Style style = SyntaxHighlighter.ParseStyle(xmlNode2);
				dictionary[xmlNode2.Attributes["name"].Value] = style;
				syntaxDescriptor.styles.Add(style);
			}
			foreach (object obj2 in doc.SelectNodes("doc/rule"))
			{
				XmlNode ruleNode = (XmlNode)obj2;
				syntaxDescriptor.rules.Add(SyntaxHighlighter.ParseRule(ruleNode, dictionary));
			}
			foreach (object obj3 in doc.SelectNodes("doc/folding"))
			{
				XmlNode foldingNode = (XmlNode)obj3;
				syntaxDescriptor.foldings.Add(SyntaxHighlighter.ParseFolding(foldingNode));
			}
			return syntaxDescriptor;
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x001DBF44 File Offset: 0x001DBF44
		protected static FoldingDesc ParseFolding(XmlNode foldingNode)
		{
			FoldingDesc foldingDesc = new FoldingDesc();
			foldingDesc.startMarkerRegex = foldingNode.Attributes["start"].Value;
			foldingDesc.finishMarkerRegex = foldingNode.Attributes["finish"].Value;
			XmlAttribute xmlAttribute = foldingNode.Attributes["options"];
			bool flag = xmlAttribute != null;
			if (flag)
			{
				foldingDesc.options = (RegexOptions)Enum.Parse(typeof(RegexOptions), xmlAttribute.Value);
			}
			return foldingDesc;
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x001DBFD8 File Offset: 0x001DBFD8
		protected static RuleDesc ParseRule(XmlNode ruleNode, Dictionary<string, Style> styles)
		{
			RuleDesc ruleDesc = new RuleDesc();
			ruleDesc.pattern = ruleNode.InnerText;
			XmlAttribute xmlAttribute = ruleNode.Attributes["style"];
			XmlAttribute xmlAttribute2 = ruleNode.Attributes["options"];
			bool flag = xmlAttribute == null;
			if (flag)
			{
				throw new Exception("Rule must contain style name.");
			}
			bool flag2 = !styles.ContainsKey(xmlAttribute.Value);
			if (flag2)
			{
				throw new Exception("Style '" + xmlAttribute.Value + "' is not found.");
			}
			ruleDesc.style = styles[xmlAttribute.Value];
			bool flag3 = xmlAttribute2 != null;
			if (flag3)
			{
				ruleDesc.options = (RegexOptions)Enum.Parse(typeof(RegexOptions), xmlAttribute2.Value);
			}
			return ruleDesc;
		}

		// Token: 0x06006364 RID: 25444 RVA: 0x001DC0B0 File Offset: 0x001DC0B0
		protected static Style ParseStyle(XmlNode styleNode)
		{
			XmlAttribute xmlAttribute = styleNode.Attributes["type"];
			XmlAttribute xmlAttribute2 = styleNode.Attributes["color"];
			XmlAttribute xmlAttribute3 = styleNode.Attributes["backColor"];
			XmlAttribute xmlAttribute4 = styleNode.Attributes["fontStyle"];
			XmlAttribute xmlAttribute5 = styleNode.Attributes["name"];
			SolidBrush foreBrush = null;
			bool flag = xmlAttribute2 != null;
			if (flag)
			{
				foreBrush = new SolidBrush(SyntaxHighlighter.ParseColor(xmlAttribute2.Value));
			}
			SolidBrush backgroundBrush = null;
			bool flag2 = xmlAttribute3 != null;
			if (flag2)
			{
				backgroundBrush = new SolidBrush(SyntaxHighlighter.ParseColor(xmlAttribute3.Value));
			}
			FontStyle fontStyle = FontStyle.Regular;
			bool flag3 = xmlAttribute4 != null;
			if (flag3)
			{
				fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), xmlAttribute4.Value);
			}
			return new TextStyle(foreBrush, backgroundBrush, fontStyle);
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x001DC19C File Offset: 0x001DC19C
		protected static Color ParseColor(string s)
		{
			bool flag = s.StartsWith("#");
			Color result;
			if (flag)
			{
				bool flag2 = s.Length <= 7;
				if (flag2)
				{
					result = Color.FromArgb(255, Color.FromArgb(int.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier)));
				}
				else
				{
					result = Color.FromArgb(int.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier));
				}
			}
			else
			{
				result = Color.FromName(s);
			}
			return result;
		}

		// Token: 0x06006366 RID: 25446 RVA: 0x001DC224 File Offset: 0x001DC224
		public void HighlightSyntax(SyntaxDescriptor desc, Range range)
		{
			range.tb.ClearStylesBuffer();
			for (int i = 0; i < desc.styles.Count; i++)
			{
				range.tb.Styles[i] = desc.styles[i];
			}
			int count = desc.styles.Count;
			for (int j = 0; j < this.resilientStyles.Count; j++)
			{
				range.tb.Styles[count + j] = this.resilientStyles[j];
			}
			char[] oldBrackets = this.RememberBrackets(range.tb);
			range.tb.LeftBracket = desc.leftBracket;
			range.tb.RightBracket = desc.rightBracket;
			range.tb.LeftBracket2 = desc.leftBracket2;
			range.tb.RightBracket2 = desc.rightBracket2;
			range.ClearStyle(desc.styles.ToArray());
			foreach (RuleDesc ruleDesc in desc.rules)
			{
				range.SetStyle(ruleDesc.style, ruleDesc.Regex);
			}
			range.ClearFoldingMarkers();
			foreach (FoldingDesc foldingDesc in desc.foldings)
			{
				range.SetFoldingMarkers(foldingDesc.startMarkerRegex, foldingDesc.finishMarkerRegex, foldingDesc.options);
			}
			this.RestoreBrackets(range.tb, oldBrackets);
		}

		// Token: 0x06006367 RID: 25447 RVA: 0x001DC400 File Offset: 0x001DC400
		protected void RestoreBrackets(FastColoredTextBox tb, char[] oldBrackets)
		{
			tb.LeftBracket = oldBrackets[0];
			tb.RightBracket = oldBrackets[1];
			tb.LeftBracket2 = oldBrackets[2];
			tb.RightBracket2 = oldBrackets[3];
		}

		// Token: 0x06006368 RID: 25448 RVA: 0x001DC43C File Offset: 0x001DC43C
		protected char[] RememberBrackets(FastColoredTextBox tb)
		{
			return new char[]
			{
				tb.LeftBracket,
				tb.RightBracket,
				tb.LeftBracket2,
				tb.RightBracket2
			};
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x001DC480 File Offset: 0x001DC480
		protected void InitCShaprRegex()
		{
			this.CSharpStringRegex = new Regex("\r\n                            # Character definitions:\r\n                            '\r\n                            (?> # disable backtracking\r\n                              (?:\r\n                                \\\\[^\\r\\n]|    # escaped meta char\r\n                                [^'\\r\\n]      # any character except '\r\n                              )*\r\n                            )\r\n                            '?\r\n                            |\r\n                            # Normal string & verbatim strings definitions:\r\n                            (?<verbatimIdentifier>@)?         # this group matches if it is an verbatim string\r\n                            \"\r\n                            (?> # disable backtracking\r\n                              (?:\r\n                                # match and consume an escaped character including escaped double quote (\") char\r\n                                (?(verbatimIdentifier)        # if it is a verbatim string ...\r\n                                  \"\"|                         #   then: only match an escaped double quote (\") char\r\n                                  \\\\.                         #   else: match an escaped sequence\r\n                                )\r\n                                | # OR\r\n            \r\n                                # match any char except double quote char (\")\r\n                                [^\"]\r\n                              )*\r\n                            )\r\n                            \"\r\n                        ", RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | SyntaxHighlighter.RegexCompiledOption);
			this.CSharpCommentRegex1 = new Regex("//.*$", RegexOptions.Multiline | SyntaxHighlighter.RegexCompiledOption);
			this.CSharpCommentRegex2 = new Regex("(/\\*.*?\\*/)|(/\\*.*)", RegexOptions.Singleline | SyntaxHighlighter.RegexCompiledOption);
			this.CSharpCommentRegex3 = new Regex("(/\\*.*?\\*/)|(.*\\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | SyntaxHighlighter.RegexCompiledOption);
			this.CSharpNumberRegex = new Regex("\\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\\b|\\b0x[a-fA-F\\d]+\\b", SyntaxHighlighter.RegexCompiledOption);
			this.CSharpAttributeRegex = new Regex("^\\s*(?<range>\\[.+?\\])\\s*$", RegexOptions.Multiline | SyntaxHighlighter.RegexCompiledOption);
			this.CSharpClassNameRegex = new Regex("\\b(class|struct|enum|interface)\\s+(?<range>\\w+?)\\b", SyntaxHighlighter.RegexCompiledOption);
			this.CSharpKeywordRegex = new Regex("\\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\\b|#region\\b|#endregion\\b", SyntaxHighlighter.RegexCompiledOption);
		}

		// Token: 0x0600636A RID: 25450 RVA: 0x001DC548 File Offset: 0x001DC548
		public void InitStyleSchema(Language lang)
		{
			switch (lang)
			{
			case Language.CSharp:
				this.StringStyle = this.BrownStyle;
				this.CommentStyle = this.GreenStyle;
				this.NumberStyle = this.MagentaStyle;
				this.AttributeStyle = this.GreenStyle;
				this.ClassNameStyle = this.BoldStyle;
				this.KeywordStyle = this.BlueStyle;
				this.CommentTagStyle = this.GrayStyle;
				break;
			case Language.VB:
				this.StringStyle = this.BrownStyle;
				this.CommentStyle = this.GreenStyle;
				this.NumberStyle = this.MagentaStyle;
				this.ClassNameStyle = this.BoldStyle;
				this.KeywordStyle = this.BlueStyle;
				break;
			case Language.HTML:
				this.CommentStyle = this.GreenStyle;
				this.TagBracketStyle = this.BlueStyle;
				this.TagNameStyle = this.MaroonStyle;
				this.AttributeStyle = this.RedStyle;
				this.AttributeValueStyle = this.BlueStyle;
				this.HtmlEntityStyle = this.RedStyle;
				break;
			case Language.XML:
				this.CommentStyle = this.GreenStyle;
				this.XmlTagBracketStyle = this.BlueStyle;
				this.XmlTagNameStyle = this.MaroonStyle;
				this.XmlAttributeStyle = this.RedStyle;
				this.XmlAttributeValueStyle = this.BlueStyle;
				this.XmlEntityStyle = this.RedStyle;
				this.XmlCDataStyle = this.BlackStyle;
				break;
			case Language.SQL:
				this.StringStyle = this.RedStyle;
				this.CommentStyle = this.GreenStyle;
				this.NumberStyle = this.MagentaStyle;
				this.KeywordStyle = this.BlueBoldStyle;
				this.StatementsStyle = this.BlueBoldStyle;
				this.FunctionsStyle = this.MaroonStyle;
				this.VariableStyle = this.MaroonStyle;
				this.TypesStyle = this.BrownStyle;
				break;
			case Language.PHP:
				this.StringStyle = this.RedStyle;
				this.CommentStyle = this.GreenStyle;
				this.NumberStyle = this.RedStyle;
				this.VariableStyle = this.MaroonStyle;
				this.KeywordStyle = this.MagentaStyle;
				this.KeywordStyle2 = this.BlueStyle;
				this.KeywordStyle3 = this.GrayStyle;
				break;
			case Language.JS:
				this.StringStyle = this.BrownStyle;
				this.CommentStyle = this.GreenStyle;
				this.NumberStyle = this.MagentaStyle;
				this.KeywordStyle = this.BlueStyle;
				break;
			case Language.Lua:
				this.StringStyle = this.BrownStyle;
				this.CommentStyle = this.GreenStyle;
				this.NumberStyle = this.MagentaStyle;
				this.KeywordStyle = this.BlueBoldStyle;
				this.FunctionsStyle = this.MaroonStyle;
				break;
			}
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x001DC830 File Offset: 0x001DC830
		public virtual void CSharpSyntaxHighlight(Range range)
		{
			range.tb.CommentPrefix = "//";
			range.tb.LeftBracket = '(';
			range.tb.RightBracket = ')';
			range.tb.LeftBracket2 = '{';
			range.tb.RightBracket2 = '}';
			range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
			range.tb.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
			range.ClearStyle(new Style[]
			{
				this.StringStyle,
				this.CommentStyle,
				this.NumberStyle,
				this.AttributeStyle,
				this.ClassNameStyle,
				this.KeywordStyle
			});
			bool flag = this.CSharpStringRegex == null;
			if (flag)
			{
				this.InitCShaprRegex();
			}
			range.SetStyle(this.StringStyle, this.CSharpStringRegex);
			range.SetStyle(this.CommentStyle, this.CSharpCommentRegex1);
			range.SetStyle(this.CommentStyle, this.CSharpCommentRegex2);
			range.SetStyle(this.CommentStyle, this.CSharpCommentRegex3);
			range.SetStyle(this.NumberStyle, this.CSharpNumberRegex);
			range.SetStyle(this.AttributeStyle, this.CSharpAttributeRegex);
			range.SetStyle(this.ClassNameStyle, this.CSharpClassNameRegex);
			range.SetStyle(this.KeywordStyle, this.CSharpKeywordRegex);
			foreach (Range range2 in range.GetRanges("^\\s*///.*$", RegexOptions.Multiline))
			{
				range2.ClearStyle(StyleIndex.All);
				bool flag2 = this.HTMLTagRegex == null;
				if (flag2)
				{
					this.InitHTMLRegex();
				}
				range2.SetStyle(this.CommentStyle);
				foreach (Range range3 in range2.GetRanges(this.HTMLTagContentRegex))
				{
					range3.ClearStyle(StyleIndex.All);
					range3.SetStyle(this.CommentTagStyle);
				}
				foreach (Range range4 in range2.GetRanges("^\\s*///", RegexOptions.Multiline))
				{
					range4.ClearStyle(StyleIndex.All);
					range4.SetStyle(this.CommentTagStyle);
				}
			}
			range.ClearFoldingMarkers();
			range.SetFoldingMarkers("{", "}");
			range.SetFoldingMarkers("#region\\b", "#endregion\\b");
			range.SetFoldingMarkers("/\\*", "\\*/");
		}

		// Token: 0x0600636C RID: 25452 RVA: 0x001DCB48 File Offset: 0x001DCB48
		protected void InitVBRegex()
		{
			this.VBStringRegex = new Regex("\"\"|\".*?[^\\\\]\"", SyntaxHighlighter.RegexCompiledOption);
			this.VBCommentRegex = new Regex("'.*$", RegexOptions.Multiline | SyntaxHighlighter.RegexCompiledOption);
			this.VBNumberRegex = new Regex("\\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?\\b", SyntaxHighlighter.RegexCompiledOption);
			this.VBClassNameRegex = new Regex("\\b(Class|Structure|Enum|Interface)[ ]+(?<range>\\w+?)\\b", RegexOptions.IgnoreCase | SyntaxHighlighter.RegexCompiledOption);
			this.VBKeywordRegex = new Regex("\\b(AddHandler|AddressOf|Alias|And|AndAlso|As|Boolean|ByRef|Byte|ByVal|Call|Case|Catch|CBool|CByte|CChar|CDate|CDbl|CDec|Char|CInt|Class|CLng|CObj|Const|Continue|CSByte|CShort|CSng|CStr|CType|CUInt|CULng|CUShort|Date|Decimal|Declare|Default|Delegate|Dim|DirectCast|Do|Double|Each|Else|ElseIf|End|EndIf|Enum|Erase|Error|Event|Exit|False|Finally|For|Friend|Function|Get|GetType|GetXMLNamespace|Global|GoSub|GoTo|Handles|If|Implements|Imports|In|Inherits|Integer|Interface|Is|IsNot|Let|Lib|Like|Long|Loop|Me|Mod|Module|MustInherit|MustOverride|MyBase|MyClass|Namespace|Narrowing|New|Next|Not|Nothing|NotInheritable|NotOverridable|Object|Of|On|Operator|Option|Optional|Or|OrElse|Overloads|Overridable|Overrides|ParamArray|Partial|Private|Property|Protected|Public|RaiseEvent|ReadOnly|ReDim|REM|RemoveHandler|Resume|Return|SByte|Select|Set|Shadows|Shared|Short|Single|Static|Step|Stop|String|Structure|Sub|SyncLock|Then|Throw|To|True|Try|TryCast|TypeOf|UInteger|ULong|UShort|Using|Variant|Wend|When|While|Widening|With|WithEvents|WriteOnly|Xor|Region)\\b|(#Const|#Else|#ElseIf|#End|#If|#Region)\\b", RegexOptions.IgnoreCase | SyntaxHighlighter.RegexCompiledOption);
		}

		// Token: 0x0600636D RID: 25453 RVA: 0x001DCBCC File Offset: 0x001DCBCC
		public virtual void VBSyntaxHighlight(Range range)
		{
			range.tb.CommentPrefix = "'";
			range.tb.LeftBracket = '(';
			range.tb.RightBracket = ')';
			range.tb.LeftBracket2 = '\0';
			range.tb.RightBracket2 = '\0';
			range.tb.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.\\(\\)]+\\s*(?<range>=)\\s*(?<range>.+)\r\n";
			range.ClearStyle(new Style[]
			{
				this.StringStyle,
				this.CommentStyle,
				this.NumberStyle,
				this.ClassNameStyle,
				this.KeywordStyle
			});
			bool flag = this.VBStringRegex == null;
			if (flag)
			{
				this.InitVBRegex();
			}
			range.SetStyle(this.StringStyle, this.VBStringRegex);
			range.SetStyle(this.CommentStyle, this.VBCommentRegex);
			range.SetStyle(this.NumberStyle, this.VBNumberRegex);
			range.SetStyle(this.ClassNameStyle, this.VBClassNameRegex);
			range.SetStyle(this.KeywordStyle, this.VBKeywordRegex);
			range.ClearFoldingMarkers();
			range.SetFoldingMarkers("#Region\\b", "#End\\s+Region\\b", RegexOptions.IgnoreCase);
			range.SetFoldingMarkers("\\b(Class|Property|Enum|Structure|Interface)[ \\t]+\\S+", "\\bEnd (Class|Property|Enum|Structure|Interface)\\b", RegexOptions.IgnoreCase);
			range.SetFoldingMarkers("^\\s*(?<range>While)[ \\t]+\\S+", "^\\s*(?<range>End While)\\b", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			range.SetFoldingMarkers("\\b(Sub|Function)[ \\t]+[^\\s']+", "\\bEnd (Sub|Function)\\b", RegexOptions.IgnoreCase);
			range.SetFoldingMarkers("(\\r|\\n|^)[ \\t]*(?<range>Get|Set)[ \\t]*(\\r|\\n|$)", "\\bEnd (Get|Set)\\b", RegexOptions.IgnoreCase);
			range.SetFoldingMarkers("^\\s*(?<range>For|For\\s+Each)\\b", "^\\s*(?<range>Next)\\b", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			range.SetFoldingMarkers("^\\s*(?<range>Do)\\b", "^\\s*(?<range>Loop)\\b", RegexOptions.IgnoreCase | RegexOptions.Multiline);
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x001DCD6C File Offset: 0x001DCD6C
		protected void InitHTMLRegex()
		{
			this.HTMLCommentRegex1 = new Regex("(<!--.*?-->)|(<!--.*)", RegexOptions.Singleline | SyntaxHighlighter.RegexCompiledOption);
			this.HTMLCommentRegex2 = new Regex("(<!--.*?-->)|(.*-->)", RegexOptions.Singleline | RegexOptions.RightToLeft | SyntaxHighlighter.RegexCompiledOption);
			this.HTMLTagRegex = new Regex("<|/>|</|>", SyntaxHighlighter.RegexCompiledOption);
			this.HTMLTagNameRegex = new Regex("<(?<range>[!\\w:]+)", SyntaxHighlighter.RegexCompiledOption);
			this.HTMLEndTagRegex = new Regex("</(?<range>[\\w:]+)>", SyntaxHighlighter.RegexCompiledOption);
			this.HTMLTagContentRegex = new Regex("<[^>]+>", SyntaxHighlighter.RegexCompiledOption);
			this.HTMLAttrRegex = new Regex("(?<range>[\\w\\d\\-]{1,20}?)='[^']*'|(?<range>[\\w\\d\\-]{1,20})=\"[^\"]*\"|(?<range>[\\w\\d\\-]{1,20})=[\\w\\d\\-]{1,20}", SyntaxHighlighter.RegexCompiledOption);
			this.HTMLAttrValRegex = new Regex("[\\w\\d\\-]{1,20}?=(?<range>'[^']*')|[\\w\\d\\-]{1,20}=(?<range>\"[^\"]*\")|[\\w\\d\\-]{1,20}=(?<range>[\\w\\d\\-]{1,20})", SyntaxHighlighter.RegexCompiledOption);
			this.HTMLEntityRegex = new Regex("\\&(amp|gt|lt|nbsp|quot|apos|copy|reg|#[0-9]{1,8}|#x[0-9a-f]{1,8});", SyntaxHighlighter.RegexCompiledOption | RegexOptions.IgnoreCase);
		}

		// Token: 0x0600636F RID: 25455 RVA: 0x001DCE44 File Offset: 0x001DCE44
		public virtual void HTMLSyntaxHighlight(Range range)
		{
			range.tb.CommentPrefix = null;
			range.tb.LeftBracket = '<';
			range.tb.RightBracket = '>';
			range.tb.LeftBracket2 = '(';
			range.tb.RightBracket2 = ')';
			range.tb.AutoIndentCharsPatterns = "";
			range.ClearStyle(new Style[]
			{
				this.CommentStyle,
				this.TagBracketStyle,
				this.TagNameStyle,
				this.AttributeStyle,
				this.AttributeValueStyle,
				this.HtmlEntityStyle
			});
			bool flag = this.HTMLTagRegex == null;
			if (flag)
			{
				this.InitHTMLRegex();
			}
			range.SetStyle(this.CommentStyle, this.HTMLCommentRegex1);
			range.SetStyle(this.CommentStyle, this.HTMLCommentRegex2);
			range.SetStyle(this.TagBracketStyle, this.HTMLTagRegex);
			range.SetStyle(this.TagNameStyle, this.HTMLTagNameRegex);
			range.SetStyle(this.TagNameStyle, this.HTMLEndTagRegex);
			range.SetStyle(this.AttributeStyle, this.HTMLAttrRegex);
			range.SetStyle(this.AttributeValueStyle, this.HTMLAttrValRegex);
			range.SetStyle(this.HtmlEntityStyle, this.HTMLEntityRegex);
			range.ClearFoldingMarkers();
			range.SetFoldingMarkers("<head", "</head>", RegexOptions.IgnoreCase);
			range.SetFoldingMarkers("<body", "</body>", RegexOptions.IgnoreCase);
			range.SetFoldingMarkers("<table", "</table>", RegexOptions.IgnoreCase);
			range.SetFoldingMarkers("<form", "</form>", RegexOptions.IgnoreCase);
			range.SetFoldingMarkers("<div", "</div>", RegexOptions.IgnoreCase);
			range.SetFoldingMarkers("<script", "</script>", RegexOptions.IgnoreCase);
			range.SetFoldingMarkers("<tr", "</tr>", RegexOptions.IgnoreCase);
		}

		// Token: 0x06006370 RID: 25456 RVA: 0x001DD024 File Offset: 0x001DD024
		protected void InitXMLRegex()
		{
			this.XMLCommentRegex1 = new Regex("(<!--.*?-->)|(<!--.*)", RegexOptions.Singleline | SyntaxHighlighter.RegexCompiledOption);
			this.XMLCommentRegex2 = new Regex("(<!--.*?-->)|(.*-->)", RegexOptions.Singleline | RegexOptions.RightToLeft | SyntaxHighlighter.RegexCompiledOption);
			this.XMLTagRegex = new Regex("<\\?|<|/>|</|>|\\?>", SyntaxHighlighter.RegexCompiledOption);
			this.XMLTagNameRegex = new Regex("<[?](?<range1>[x][m][l]{1})|<(?<range>[!\\w:]+)", SyntaxHighlighter.RegexCompiledOption);
			this.XMLEndTagRegex = new Regex("</(?<range>[\\w:]+)>", SyntaxHighlighter.RegexCompiledOption);
			this.XMLTagContentRegex = new Regex("<[^>]+>", SyntaxHighlighter.RegexCompiledOption);
			this.XMLAttrRegex = new Regex("(?<range>[\\w\\d\\-\\:]+)[ ]*=[ ]*'[^']*'|(?<range>[\\w\\d\\-\\:]+)[ ]*=[ ]*\"[^\"]*\"|(?<range>[\\w\\d\\-\\:]+)[ ]*=[ ]*[\\w\\d\\-\\:]+", SyntaxHighlighter.RegexCompiledOption);
			this.XMLAttrValRegex = new Regex("[\\w\\d\\-]+?=(?<range>'[^']*')|[\\w\\d\\-]+[ ]*=[ ]*(?<range>\"[^\"]*\")|[\\w\\d\\-]+[ ]*=[ ]*(?<range>[\\w\\d\\-]+)", SyntaxHighlighter.RegexCompiledOption);
			this.XMLEntityRegex = new Regex("\\&(amp|gt|lt|nbsp|quot|apos|copy|reg|#[0-9]{1,8}|#x[0-9a-f]{1,8});", SyntaxHighlighter.RegexCompiledOption | RegexOptions.IgnoreCase);
			this.XMLCDataRegex = new Regex("<!\\s*\\[CDATA\\s*\\[(?<text>(?>[^]]+|](?!]>))*)]]>", SyntaxHighlighter.RegexCompiledOption | RegexOptions.IgnoreCase);
			this.XMLFoldingRegex = new Regex("<(?<range>/?\\w+)\\s[^>]*?[^/]>|<(?<range>/?\\w+)\\s*>", RegexOptions.Singleline | SyntaxHighlighter.RegexCompiledOption);
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x001DD12C File Offset: 0x001DD12C
		public virtual void XMLSyntaxHighlight(Range range)
		{
			range.tb.CommentPrefix = null;
			range.tb.LeftBracket = '<';
			range.tb.RightBracket = '>';
			range.tb.LeftBracket2 = '(';
			range.tb.RightBracket2 = ')';
			range.tb.AutoIndentCharsPatterns = "";
			range.ClearStyle(new Style[]
			{
				this.CommentStyle,
				this.XmlTagBracketStyle,
				this.XmlTagNameStyle,
				this.XmlAttributeStyle,
				this.XmlAttributeValueStyle,
				this.XmlEntityStyle,
				this.XmlCDataStyle
			});
			bool flag = this.XMLTagRegex == null;
			if (flag)
			{
				this.InitXMLRegex();
			}
			range.SetStyle(this.XmlCDataStyle, this.XMLCDataRegex);
			range.SetStyle(this.CommentStyle, this.XMLCommentRegex1);
			range.SetStyle(this.CommentStyle, this.XMLCommentRegex2);
			range.SetStyle(this.XmlTagBracketStyle, this.XMLTagRegex);
			range.SetStyle(this.XmlTagNameStyle, this.XMLTagNameRegex);
			range.SetStyle(this.XmlTagNameStyle, this.XMLEndTagRegex);
			range.SetStyle(this.XmlAttributeStyle, this.XMLAttrRegex);
			range.SetStyle(this.XmlAttributeValueStyle, this.XMLAttrValRegex);
			range.SetStyle(this.XmlEntityStyle, this.XMLEntityRegex);
			range.ClearFoldingMarkers();
			this.XmlFolding(range);
		}

		// Token: 0x06006372 RID: 25458 RVA: 0x001DD2B4 File Offset: 0x001DD2B4
		private void XmlFolding(Range range)
		{
			Stack<SyntaxHighlighter.XmlFoldingTag> stack = new Stack<SyntaxHighlighter.XmlFoldingTag>();
			int num = 0;
			FastColoredTextBox tb = range.tb;
			foreach (Range range2 in range.GetRanges(this.XMLFoldingRegex))
			{
				string text = range2.Text;
				int iLine = range2.Start.iLine;
				bool flag = text[0] != '/';
				if (flag)
				{
					SyntaxHighlighter.XmlFoldingTag xmlFoldingTag = new SyntaxHighlighter.XmlFoldingTag
					{
						Name = text,
						id = num++,
						startLine = range2.Start.iLine
					};
					stack.Push(xmlFoldingTag);
					bool flag2 = string.IsNullOrEmpty(tb[iLine].FoldingStartMarker);
					if (flag2)
					{
						tb[iLine].FoldingStartMarker = xmlFoldingTag.Marker;
					}
				}
				else
				{
					bool flag3 = stack.Count > 0;
					if (flag3)
					{
						SyntaxHighlighter.XmlFoldingTag xmlFoldingTag2 = stack.Pop();
						bool flag4 = iLine == xmlFoldingTag2.startLine;
						if (flag4)
						{
							bool flag5 = tb[iLine].FoldingStartMarker == xmlFoldingTag2.Marker;
							if (flag5)
							{
								tb[iLine].FoldingStartMarker = null;
							}
						}
						else
						{
							bool flag6 = string.IsNullOrEmpty(tb[iLine].FoldingEndMarker);
							if (flag6)
							{
								tb[iLine].FoldingEndMarker = xmlFoldingTag2.Marker;
							}
						}
					}
				}
			}
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x001DD464 File Offset: 0x001DD464
		protected void InitSQLRegex()
		{
			this.SQLStringRegex = new Regex("\"\"|''|\".*?[^\\\\]\"|'.*?[^\\\\]'", SyntaxHighlighter.RegexCompiledOption);
			this.SQLNumberRegex = new Regex("\\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?\\b", SyntaxHighlighter.RegexCompiledOption);
			this.SQLCommentRegex1 = new Regex("--.*$", RegexOptions.Multiline | SyntaxHighlighter.RegexCompiledOption);
			this.SQLCommentRegex2 = new Regex("(/\\*.*?\\*/)|(/\\*.*)", RegexOptions.Singleline | SyntaxHighlighter.RegexCompiledOption);
			this.SQLCommentRegex3 = new Regex("(/\\*.*?\\*/)|(.*\\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | SyntaxHighlighter.RegexCompiledOption);
			this.SQLCommentRegex4 = new Regex("#.*$", RegexOptions.Multiline | SyntaxHighlighter.RegexCompiledOption);
			this.SQLVarRegex = new Regex("@[a-zA-Z_\\d]*\\b", SyntaxHighlighter.RegexCompiledOption);
			this.SQLStatementsRegex = new Regex("\\b(ALTER APPLICATION ROLE|ALTER ASSEMBLY|ALTER ASYMMETRIC KEY|ALTER AUTHORIZATION|ALTER BROKER PRIORITY|ALTER CERTIFICATE|ALTER CREDENTIAL|ALTER CRYPTOGRAPHIC PROVIDER|ALTER DATABASE|ALTER DATABASE AUDIT SPECIFICATION|ALTER DATABASE ENCRYPTION KEY|ALTER ENDPOINT|ALTER EVENT SESSION|ALTER FULLTEXT CATALOG|ALTER FULLTEXT INDEX|ALTER FULLTEXT STOPLIST|ALTER FUNCTION|ALTER INDEX|ALTER LOGIN|ALTER MASTER KEY|ALTER MESSAGE TYPE|ALTER PARTITION FUNCTION|ALTER PARTITION SCHEME|ALTER PROCEDURE|ALTER QUEUE|ALTER REMOTE SERVICE BINDING|ALTER RESOURCE GOVERNOR|ALTER RESOURCE POOL|ALTER ROLE|ALTER ROUTE|ALTER SCHEMA|ALTER SERVER AUDIT|ALTER SERVER AUDIT SPECIFICATION|ALTER SERVICE|ALTER SERVICE MASTER KEY|ALTER SYMMETRIC KEY|ALTER TABLE|ALTER TRIGGER|ALTER USER|ALTER VIEW|ALTER WORKLOAD GROUP|ALTER XML SCHEMA COLLECTION|BULK INSERT|CREATE AGGREGATE|CREATE APPLICATION ROLE|CREATE ASSEMBLY|CREATE ASYMMETRIC KEY|CREATE BROKER PRIORITY|CREATE CERTIFICATE|CREATE CONTRACT|CREATE CREDENTIAL|CREATE CRYPTOGRAPHIC PROVIDER|CREATE DATABASE|CREATE DATABASE AUDIT SPECIFICATION|CREATE DATABASE ENCRYPTION KEY|CREATE DEFAULT|CREATE ENDPOINT|CREATE EVENT NOTIFICATION|CREATE EVENT SESSION|CREATE FULLTEXT CATALOG|CREATE FULLTEXT INDEX|CREATE FULLTEXT STOPLIST|CREATE FUNCTION|CREATE INDEX|CREATE LOGIN|CREATE MASTER KEY|CREATE MESSAGE TYPE|CREATE PARTITION FUNCTION|CREATE PARTITION SCHEME|CREATE PROCEDURE|CREATE QUEUE|CREATE REMOTE SERVICE BINDING|CREATE RESOURCE POOL|CREATE ROLE|CREATE ROUTE|CREATE RULE|CREATE SCHEMA|CREATE SERVER AUDIT|CREATE SERVER AUDIT SPECIFICATION|CREATE SERVICE|CREATE SPATIAL INDEX|CREATE STATISTICS|CREATE SYMMETRIC KEY|CREATE SYNONYM|CREATE TABLE|CREATE TRIGGER|CREATE TYPE|CREATE USER|CREATE VIEW|CREATE WORKLOAD GROUP|CREATE XML INDEX|CREATE XML SCHEMA COLLECTION|DELETE|DISABLE TRIGGER|DROP AGGREGATE|DROP APPLICATION ROLE|DROP ASSEMBLY|DROP ASYMMETRIC KEY|DROP BROKER PRIORITY|DROP CERTIFICATE|DROP CONTRACT|DROP CREDENTIAL|DROP CRYPTOGRAPHIC PROVIDER|DROP DATABASE|DROP DATABASE AUDIT SPECIFICATION|DROP DATABASE ENCRYPTION KEY|DROP DEFAULT|DROP ENDPOINT|DROP EVENT NOTIFICATION|DROP EVENT SESSION|DROP FULLTEXT CATALOG|DROP FULLTEXT INDEX|DROP FULLTEXT STOPLIST|DROP FUNCTION|DROP INDEX|DROP LOGIN|DROP MASTER KEY|DROP MESSAGE TYPE|DROP PARTITION FUNCTION|DROP PARTITION SCHEME|DROP PROCEDURE|DROP QUEUE|DROP REMOTE SERVICE BINDING|DROP RESOURCE POOL|DROP ROLE|DROP ROUTE|DROP RULE|DROP SCHEMA|DROP SERVER AUDIT|DROP SERVER AUDIT SPECIFICATION|DROP SERVICE|DROP SIGNATURE|DROP STATISTICS|DROP SYMMETRIC KEY|DROP SYNONYM|DROP TABLE|DROP TRIGGER|DROP TYPE|DROP USER|DROP VIEW|DROP WORKLOAD GROUP|DROP XML SCHEMA COLLECTION|ENABLE TRIGGER|EXEC|EXECUTE|REPLACE|FROM|INSERT|MERGE|OPTION|OUTPUT|SELECT|TOP|TRUNCATE TABLE|UPDATE|UPDATE STATISTICS|WHERE|WITH|INTO|IN|SET)\\b", RegexOptions.IgnoreCase | SyntaxHighlighter.RegexCompiledOption);
			this.SQLKeywordsRegex = new Regex("\\b(ADD|ALL|AND|ANY|AS|ASC|AUTHORIZATION|BACKUP|BEGIN|BETWEEN|BREAK|BROWSE|BY|CASCADE|CHECK|CHECKPOINT|CLOSE|CLUSTERED|COLLATE|COLUMN|COMMIT|COMPUTE|CONSTRAINT|CONTAINS|CONTINUE|CROSS|CURRENT|CURRENT_DATE|CURRENT_TIME|CURSOR|DATABASE|DBCC|DEALLOCATE|DECLARE|DEFAULT|DENY|DESC|DISK|DISTINCT|DISTRIBUTED|DOUBLE|DUMP|ELSE|END|ERRLVL|ESCAPE|EXCEPT|EXISTS|EXIT|EXTERNAL|FETCH|FILE|FILLFACTOR|FOR|FOREIGN|FREETEXT|FULL|FUNCTION|GOTO|GRANT|GROUP|HAVING|HOLDLOCK|IDENTITY|IDENTITY_INSERT|IDENTITYCOL|IF|INDEX|INNER|INTERSECT|IS|JOIN|KEY|KILL|LIKE|LINENO|LOAD|NATIONAL|NOCHECK|NONCLUSTERED|NOT|NULL|OF|OFF|OFFSETS|ON|OPEN|OR|ORDER|OUTER|OVER|PERCENT|PIVOT|PLAN|PRECISION|PRIMARY|PRINT|PROC|PROCEDURE|PUBLIC|RAISERROR|READ|READTEXT|RECONFIGURE|REFERENCES|REPLICATION|RESTORE|RESTRICT|RETURN|REVERT|REVOKE|ROLLBACK|ROWCOUNT|ROWGUIDCOL|RULE|SAVE|SCHEMA|SECURITYAUDIT|SHUTDOWN|SOME|STATISTICS|TABLE|TABLESAMPLE|TEXTSIZE|THEN|TO|TRAN|TRANSACTION|TRIGGER|TSEQUAL|UNION|UNIQUE|UNPIVOT|UPDATETEXT|USE|USER|VALUES|VARYING|VIEW|WAITFOR|WHEN|WHILE|WRITETEXT)\\b", RegexOptions.IgnoreCase | SyntaxHighlighter.RegexCompiledOption);
			this.SQLFunctionsRegex = new Regex("(@@CONNECTIONS|@@CPU_BUSY|@@CURSOR_ROWS|@@DATEFIRST|@@DATEFIRST|@@DBTS|@@ERROR|@@FETCH_STATUS|@@IDENTITY|@@IDLE|@@IO_BUSY|@@LANGID|@@LANGUAGE|@@LOCK_TIMEOUT|@@MAX_CONNECTIONS|@@MAX_PRECISION|@@NESTLEVEL|@@OPTIONS|@@PACKET_ERRORS|@@PROCID|@@REMSERVER|@@ROWCOUNT|@@SERVERNAME|@@SERVICENAME|@@SPID|@@TEXTSIZE|@@TRANCOUNT|@@VERSION)\\b|\\b(ABS|ACOS|APP_NAME|ASCII|ASIN|ASSEMBLYPROPERTY|AsymKey_ID|ASYMKEY_ID|asymkeyproperty|ASYMKEYPROPERTY|ATAN|ATN2|AVG|CASE|CAST|CEILING|Cert_ID|Cert_ID|CertProperty|CHAR|CHARINDEX|CHECKSUM_AGG|COALESCE|COL_LENGTH|COL_NAME|COLLATIONPROPERTY|COLLATIONPROPERTY|COLUMNPROPERTY|COLUMNS_UPDATED|COLUMNS_UPDATED|CONTAINSTABLE|CONVERT|COS|COT|COUNT|COUNT_BIG|CRYPT_GEN_RANDOM|CURRENT_TIMESTAMP|CURRENT_TIMESTAMP|CURRENT_USER|CURRENT_USER|CURSOR_STATUS|DATABASE_PRINCIPAL_ID|DATABASE_PRINCIPAL_ID|DATABASEPROPERTY|DATABASEPROPERTYEX|DATALENGTH|DATALENGTH|DATEADD|DATEDIFF|DATENAME|DATEPART|DAY|DB_ID|DB_NAME|DECRYPTBYASYMKEY|DECRYPTBYCERT|DECRYPTBYKEY|DECRYPTBYKEYAUTOASYMKEY|DECRYPTBYKEYAUTOCERT|DECRYPTBYPASSPHRASE|DEGREES|DENSE_RANK|DIFFERENCE|ENCRYPTBYASYMKEY|ENCRYPTBYCERT|ENCRYPTBYKEY|ENCRYPTBYPASSPHRASE|ERROR_LINE|ERROR_MESSAGE|ERROR_NUMBER|ERROR_PROCEDURE|ERROR_SEVERITY|ERROR_STATE|EVENTDATA|EXP|FILE_ID|FILE_IDEX|FILE_NAME|FILEGROUP_ID|FILEGROUP_NAME|FILEGROUPPROPERTY|FILEPROPERTY|FLOOR|fn_helpcollations|fn_listextendedproperty|fn_servershareddrives|fn_virtualfilestats|fn_virtualfilestats|FORMATMESSAGE|FREETEXTTABLE|FULLTEXTCATALOGPROPERTY|FULLTEXTSERVICEPROPERTY|GETANSINULL|GETDATE|GETUTCDATE|GROUPING|HAS_PERMS_BY_NAME|HOST_ID|HOST_NAME|IDENT_CURRENT|IDENT_CURRENT|IDENT_INCR|IDENT_INCR|IDENT_SEED|IDENTITY\\(|INDEX_COL|INDEXKEY_PROPERTY|INDEXPROPERTY|IS_MEMBER|IS_OBJECTSIGNED|IS_SRVROLEMEMBER|ISDATE|ISDATE|ISNULL|ISNUMERIC|Key_GUID|Key_GUID|Key_ID|Key_ID|KEY_NAME|KEY_NAME|LEFT|LEN|LOG|LOG10|LOWER|LTRIM|MAX|MIN|MONTH|NCHAR|NEWID|NTILE|NULLIF|OBJECT_DEFINITION|OBJECT_ID|OBJECT_NAME|OBJECT_SCHEMA_NAME|OBJECTPROPERTY|OBJECTPROPERTYEX|OPENDATASOURCE|OPENQUERY|OPENROWSET|OPENXML|ORIGINAL_LOGIN|ORIGINAL_LOGIN|PARSENAME|PATINDEX|PATINDEX|PERMISSIONS|PI|POWER|PUBLISHINGSERVERNAME|PWDCOMPARE|PWDENCRYPT|QUOTENAME|RADIANS|RAND|RANK|REPLICATE|REVERSE|RIGHT|ROUND|ROW_NUMBER|ROWCOUNT_BIG|RTRIM|SCHEMA_ID|SCHEMA_ID|SCHEMA_NAME|SCHEMA_NAME|SCOPE_IDENTITY|SERVERPROPERTY|SESSION_USER|SESSION_USER|SESSIONPROPERTY|SETUSER|SIGN|SignByAsymKey|SignByCert|SIN|SOUNDEX|SPACE|SQL_VARIANT_PROPERTY|SQRT|SQUARE|STATS_DATE|STDEV|STDEVP|STR|STUFF|SUBSTRING|SUM|SUSER_ID|SUSER_NAME|SUSER_SID|SUSER_SNAME|SWITCHOFFSET|SYMKEYPROPERTY|symkeyproperty|sys\\.dm_db_index_physical_stats|sys\\.fn_builtin_permissions|sys\\.fn_my_permissions|SYSDATETIME|SYSDATETIMEOFFSET|SYSTEM_USER|SYSTEM_USER|SYSUTCDATETIME|TAN|TERTIARY_WEIGHTS|TEXTPTR|TODATETIMEOFFSET|TRIGGER_NESTLEVEL|TYPE_ID|TYPE_NAME|TYPEPROPERTY|UNICODE|UPDATE\\(|UPPER|USER_ID|USER_NAME|USER_NAME|VAR|VARP|VerifySignedByAsymKey|VerifySignedByCert|XACT_STATE|YEAR)\\b", RegexOptions.IgnoreCase | SyntaxHighlighter.RegexCompiledOption);
			this.SQLTypesRegex = new Regex("\\b(BIGINT|NUMERIC|BIT|SMALLINT|DECIMAL|SMALLMONEY|INT|TINYINT|MONEY|FLOAT|REAL|DATE|DATETIMEOFFSET|DATETIME2|SMALLDATETIME|DATETIME|TIME|CHAR|VARCHAR|TEXT|NCHAR|NVARCHAR|NTEXT|BINARY|VARBINARY|IMAGE|TIMESTAMP|HIERARCHYID|TABLE|UNIQUEIDENTIFIER|SQL_VARIANT|XML)\\b", RegexOptions.IgnoreCase | SyntaxHighlighter.RegexCompiledOption);
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x001DD570 File Offset: 0x001DD570
		public virtual void SQLSyntaxHighlight(Range range)
		{
			range.tb.CommentPrefix = "--";
			range.tb.LeftBracket = '(';
			range.tb.RightBracket = ')';
			range.tb.LeftBracket2 = '\0';
			range.tb.RightBracket2 = '\0';
			range.tb.AutoIndentCharsPatterns = "";
			range.ClearStyle(new Style[]
			{
				this.CommentStyle,
				this.StringStyle,
				this.NumberStyle,
				this.VariableStyle,
				this.StatementsStyle,
				this.KeywordStyle,
				this.FunctionsStyle,
				this.TypesStyle
			});
			bool flag = this.SQLStringRegex == null;
			if (flag)
			{
				this.InitSQLRegex();
			}
			range.SetStyle(this.CommentStyle, this.SQLCommentRegex1);
			range.SetStyle(this.CommentStyle, this.SQLCommentRegex2);
			range.SetStyle(this.CommentStyle, this.SQLCommentRegex3);
			range.SetStyle(this.CommentStyle, this.SQLCommentRegex4);
			range.SetStyle(this.StringStyle, this.SQLStringRegex);
			range.SetStyle(this.NumberStyle, this.SQLNumberRegex);
			range.SetStyle(this.TypesStyle, this.SQLTypesRegex);
			range.SetStyle(this.VariableStyle, this.SQLVarRegex);
			range.SetStyle(this.StatementsStyle, this.SQLStatementsRegex);
			range.SetStyle(this.KeywordStyle, this.SQLKeywordsRegex);
			range.SetStyle(this.FunctionsStyle, this.SQLFunctionsRegex);
			range.ClearFoldingMarkers();
			range.SetFoldingMarkers("\\bBEGIN\\b", "\\bEND\\b", RegexOptions.IgnoreCase);
			range.SetFoldingMarkers("/\\*", "\\*/");
		}

		// Token: 0x06006375 RID: 25461 RVA: 0x001DD744 File Offset: 0x001DD744
		protected void InitPHPRegex()
		{
			this.PHPStringRegex = new Regex("\"\"|''|\".*?[^\\\\]\"|'.*?[^\\\\]'", SyntaxHighlighter.RegexCompiledOption);
			this.PHPNumberRegex = new Regex("\\b\\d+[\\.]?\\d*\\b", SyntaxHighlighter.RegexCompiledOption);
			this.PHPCommentRegex1 = new Regex("(//|#).*$", RegexOptions.Multiline | SyntaxHighlighter.RegexCompiledOption);
			this.PHPCommentRegex2 = new Regex("(/\\*.*?\\*/)|(/\\*.*)", RegexOptions.Singleline | SyntaxHighlighter.RegexCompiledOption);
			this.PHPCommentRegex3 = new Regex("(/\\*.*?\\*/)|(.*\\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | SyntaxHighlighter.RegexCompiledOption);
			this.PHPVarRegex = new Regex("\\$[a-zA-Z_\\d]*\\b", SyntaxHighlighter.RegexCompiledOption);
			this.PHPKeywordRegex1 = new Regex("\\b(die|echo|empty|exit|eval|include|include_once|isset|list|require|require_once|return|print|unset)\\b", SyntaxHighlighter.RegexCompiledOption);
			this.PHPKeywordRegex2 = new Regex("\\b(abstract|and|array|as|break|case|catch|cfunction|class|clone|const|continue|declare|default|do|else|elseif|enddeclare|endfor|endforeach|endif|endswitch|endwhile|extends|final|for|foreach|function|global|goto|if|implements|instanceof|interface|namespace|new|or|private|protected|public|static|switch|throw|try|use|var|while|xor)\\b", SyntaxHighlighter.RegexCompiledOption);
			this.PHPKeywordRegex3 = new Regex("__CLASS__|__DIR__|__FILE__|__LINE__|__FUNCTION__|__METHOD__|__NAMESPACE__", SyntaxHighlighter.RegexCompiledOption);
		}

		// Token: 0x06006376 RID: 25462 RVA: 0x001DD81C File Offset: 0x001DD81C
		public virtual void PHPSyntaxHighlight(Range range)
		{
			range.tb.CommentPrefix = "//";
			range.tb.LeftBracket = '(';
			range.tb.RightBracket = ')';
			range.tb.LeftBracket2 = '{';
			range.tb.RightBracket2 = '}';
			range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
			range.ClearStyle(new Style[]
			{
				this.StringStyle,
				this.CommentStyle,
				this.NumberStyle,
				this.VariableStyle,
				this.KeywordStyle,
				this.KeywordStyle2,
				this.KeywordStyle3
			});
			range.tb.AutoIndentCharsPatterns = "\r\n^\\s*\\$[\\w\\.\\[\\]\\'\\\"]+\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n";
			bool flag = this.PHPStringRegex == null;
			if (flag)
			{
				this.InitPHPRegex();
			}
			range.SetStyle(this.StringStyle, this.PHPStringRegex);
			range.SetStyle(this.CommentStyle, this.PHPCommentRegex1);
			range.SetStyle(this.CommentStyle, this.PHPCommentRegex2);
			range.SetStyle(this.CommentStyle, this.PHPCommentRegex3);
			range.SetStyle(this.NumberStyle, this.PHPNumberRegex);
			range.SetStyle(this.VariableStyle, this.PHPVarRegex);
			range.SetStyle(this.KeywordStyle, this.PHPKeywordRegex1);
			range.SetStyle(this.KeywordStyle2, this.PHPKeywordRegex2);
			range.SetStyle(this.KeywordStyle3, this.PHPKeywordRegex3);
			range.ClearFoldingMarkers();
			range.SetFoldingMarkers("{", "}");
			range.SetFoldingMarkers("/\\*", "\\*/");
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x001DD9CC File Offset: 0x001DD9CC
		protected void InitJScriptRegex()
		{
			this.JScriptStringRegex = new Regex("\"\"|''|\".*?[^\\\\]\"|'.*?[^\\\\]'", SyntaxHighlighter.RegexCompiledOption);
			this.JScriptCommentRegex1 = new Regex("//.*$", RegexOptions.Multiline | SyntaxHighlighter.RegexCompiledOption);
			this.JScriptCommentRegex2 = new Regex("(/\\*.*?\\*/)|(/\\*.*)", RegexOptions.Singleline | SyntaxHighlighter.RegexCompiledOption);
			this.JScriptCommentRegex3 = new Regex("(/\\*.*?\\*/)|(.*\\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | SyntaxHighlighter.RegexCompiledOption);
			this.JScriptNumberRegex = new Regex("\\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\\b|\\b0x[a-fA-F\\d]+\\b", SyntaxHighlighter.RegexCompiledOption);
			this.JScriptKeywordRegex = new Regex("\\b(true|false|break|case|catch|const|continue|default|delete|do|else|export|for|function|if|in|instanceof|new|null|return|switch|this|throw|try|var|void|while|with|typeof)\\b", SyntaxHighlighter.RegexCompiledOption);
		}

		// Token: 0x06006378 RID: 25464 RVA: 0x001DDA64 File Offset: 0x001DDA64
		public virtual void JScriptSyntaxHighlight(Range range)
		{
			range.tb.CommentPrefix = "//";
			range.tb.LeftBracket = '(';
			range.tb.RightBracket = ')';
			range.tb.LeftBracket2 = '{';
			range.tb.RightBracket2 = '}';
			range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
			range.tb.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n";
			range.ClearStyle(new Style[]
			{
				this.StringStyle,
				this.CommentStyle,
				this.NumberStyle,
				this.KeywordStyle
			});
			bool flag = this.JScriptStringRegex == null;
			if (flag)
			{
				this.InitJScriptRegex();
			}
			range.SetStyle(this.StringStyle, this.JScriptStringRegex);
			range.SetStyle(this.CommentStyle, this.JScriptCommentRegex1);
			range.SetStyle(this.CommentStyle, this.JScriptCommentRegex2);
			range.SetStyle(this.CommentStyle, this.JScriptCommentRegex3);
			range.SetStyle(this.NumberStyle, this.JScriptNumberRegex);
			range.SetStyle(this.KeywordStyle, this.JScriptKeywordRegex);
			range.ClearFoldingMarkers();
			range.SetFoldingMarkers("{", "}");
			range.SetFoldingMarkers("/\\*", "\\*/");
		}

		// Token: 0x06006379 RID: 25465 RVA: 0x001DDBC0 File Offset: 0x001DDBC0
		protected void InitLuaRegex()
		{
			this.LuaStringRegex = new Regex("\"\"|''|\".*?[^\\\\]\"|'.*?[^\\\\]'", SyntaxHighlighter.RegexCompiledOption);
			this.LuaCommentRegex1 = new Regex("--.*$", RegexOptions.Multiline | SyntaxHighlighter.RegexCompiledOption);
			this.LuaCommentRegex2 = new Regex("(--\\[\\[.*?\\]\\])|(--\\[\\[.*)", RegexOptions.Singleline | SyntaxHighlighter.RegexCompiledOption);
			this.LuaCommentRegex3 = new Regex("(--\\[\\[.*?\\]\\])|(.*\\]\\])", RegexOptions.Singleline | RegexOptions.RightToLeft | SyntaxHighlighter.RegexCompiledOption);
			this.LuaNumberRegex = new Regex("\\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\\b|\\b0x[a-fA-F\\d]+\\b", SyntaxHighlighter.RegexCompiledOption);
			this.LuaKeywordRegex = new Regex("\\b(and|break|do|else|elseif|end|false|for|function|if|in|local|nil|not|or|repeat|return|then|true|until|while)\\b", SyntaxHighlighter.RegexCompiledOption);
			this.LuaFunctionsRegex = new Regex("\\b(assert|collectgarbage|dofile|error|getfenv|getmetatable|ipairs|load|loadfile|loadstring|module|next|pairs|pcall|print|rawequal|rawget|rawset|require|select|setfenv|setmetatable|tonumber|tostring|type|unpack|xpcall)\\b", SyntaxHighlighter.RegexCompiledOption);
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x001DDC70 File Offset: 0x001DDC70
		public virtual void LuaSyntaxHighlight(Range range)
		{
			range.tb.CommentPrefix = "--";
			range.tb.LeftBracket = '(';
			range.tb.RightBracket = ')';
			range.tb.LeftBracket2 = '{';
			range.tb.RightBracket2 = '}';
			range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
			range.tb.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>.+)\r\n";
			range.ClearStyle(new Style[]
			{
				this.StringStyle,
				this.CommentStyle,
				this.NumberStyle,
				this.KeywordStyle,
				this.FunctionsStyle
			});
			bool flag = this.LuaStringRegex == null;
			if (flag)
			{
				this.InitLuaRegex();
			}
			range.SetStyle(this.StringStyle, this.LuaStringRegex);
			range.SetStyle(this.CommentStyle, this.LuaCommentRegex1);
			range.SetStyle(this.CommentStyle, this.LuaCommentRegex2);
			range.SetStyle(this.CommentStyle, this.LuaCommentRegex3);
			range.SetStyle(this.NumberStyle, this.LuaNumberRegex);
			range.SetStyle(this.KeywordStyle, this.LuaKeywordRegex);
			range.SetStyle(this.FunctionsStyle, this.LuaFunctionsRegex);
			range.ClearFoldingMarkers();
			range.SetFoldingMarkers("{", "}");
			range.SetFoldingMarkers("--\\[\\[", "\\]\\]");
		}

		// Token: 0x0600637B RID: 25467 RVA: 0x001DDDE8 File Offset: 0x001DDDE8
		protected void LuaAutoIndentNeeded(object sender, AutoIndentEventArgs args)
		{
			bool flag = Regex.IsMatch(args.LineText, "^\\s*(end|until)\\b");
			if (flag)
			{
				args.Shift = -args.TabLength;
				args.ShiftNextLines = -args.TabLength;
			}
			else
			{
				bool flag2 = Regex.IsMatch(args.LineText, "\\b(then)\\s*\\S+");
				if (!flag2)
				{
					bool flag3 = Regex.IsMatch(args.LineText, "^\\s*(function|do|for|while|repeat|if)\\b");
					if (flag3)
					{
						args.ShiftNextLines = args.TabLength;
					}
					else
					{
						bool flag4 = Regex.IsMatch(args.LineText, "^\\s*(else|elseif)\\b", RegexOptions.IgnoreCase);
						if (flag4)
						{
							args.Shift = -args.TabLength;
						}
					}
				}
			}
		}

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x0600637C RID: 25468 RVA: 0x001DDEA8 File Offset: 0x001DDEA8
		// (set) Token: 0x0600637D RID: 25469 RVA: 0x001DDEB0 File Offset: 0x001DDEB0
		public Style StringStyle { get; set; }

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x0600637E RID: 25470 RVA: 0x001DDEBC File Offset: 0x001DDEBC
		// (set) Token: 0x0600637F RID: 25471 RVA: 0x001DDEC4 File Offset: 0x001DDEC4
		public Style CommentStyle { get; set; }

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06006380 RID: 25472 RVA: 0x001DDED0 File Offset: 0x001DDED0
		// (set) Token: 0x06006381 RID: 25473 RVA: 0x001DDED8 File Offset: 0x001DDED8
		public Style NumberStyle { get; set; }

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06006382 RID: 25474 RVA: 0x001DDEE4 File Offset: 0x001DDEE4
		// (set) Token: 0x06006383 RID: 25475 RVA: 0x001DDEEC File Offset: 0x001DDEEC
		public Style AttributeStyle { get; set; }

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x06006384 RID: 25476 RVA: 0x001DDEF8 File Offset: 0x001DDEF8
		// (set) Token: 0x06006385 RID: 25477 RVA: 0x001DDF00 File Offset: 0x001DDF00
		public Style ClassNameStyle { get; set; }

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x06006386 RID: 25478 RVA: 0x001DDF0C File Offset: 0x001DDF0C
		// (set) Token: 0x06006387 RID: 25479 RVA: 0x001DDF14 File Offset: 0x001DDF14
		public Style KeywordStyle { get; set; }

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x06006388 RID: 25480 RVA: 0x001DDF20 File Offset: 0x001DDF20
		// (set) Token: 0x06006389 RID: 25481 RVA: 0x001DDF28 File Offset: 0x001DDF28
		public Style CommentTagStyle { get; set; }

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x0600638A RID: 25482 RVA: 0x001DDF34 File Offset: 0x001DDF34
		// (set) Token: 0x0600638B RID: 25483 RVA: 0x001DDF3C File Offset: 0x001DDF3C
		public Style AttributeValueStyle { get; set; }

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x0600638C RID: 25484 RVA: 0x001DDF48 File Offset: 0x001DDF48
		// (set) Token: 0x0600638D RID: 25485 RVA: 0x001DDF50 File Offset: 0x001DDF50
		public Style TagBracketStyle { get; set; }

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x0600638E RID: 25486 RVA: 0x001DDF5C File Offset: 0x001DDF5C
		// (set) Token: 0x0600638F RID: 25487 RVA: 0x001DDF64 File Offset: 0x001DDF64
		public Style TagNameStyle { get; set; }

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x06006390 RID: 25488 RVA: 0x001DDF70 File Offset: 0x001DDF70
		// (set) Token: 0x06006391 RID: 25489 RVA: 0x001DDF78 File Offset: 0x001DDF78
		public Style HtmlEntityStyle { get; set; }

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06006392 RID: 25490 RVA: 0x001DDF84 File Offset: 0x001DDF84
		// (set) Token: 0x06006393 RID: 25491 RVA: 0x001DDF8C File Offset: 0x001DDF8C
		public Style XmlAttributeStyle { get; set; }

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06006394 RID: 25492 RVA: 0x001DDF98 File Offset: 0x001DDF98
		// (set) Token: 0x06006395 RID: 25493 RVA: 0x001DDFA0 File Offset: 0x001DDFA0
		public Style XmlAttributeValueStyle { get; set; }

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x06006396 RID: 25494 RVA: 0x001DDFAC File Offset: 0x001DDFAC
		// (set) Token: 0x06006397 RID: 25495 RVA: 0x001DDFB4 File Offset: 0x001DDFB4
		public Style XmlTagBracketStyle { get; set; }

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x06006398 RID: 25496 RVA: 0x001DDFC0 File Offset: 0x001DDFC0
		// (set) Token: 0x06006399 RID: 25497 RVA: 0x001DDFC8 File Offset: 0x001DDFC8
		public Style XmlTagNameStyle { get; set; }

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x0600639A RID: 25498 RVA: 0x001DDFD4 File Offset: 0x001DDFD4
		// (set) Token: 0x0600639B RID: 25499 RVA: 0x001DDFDC File Offset: 0x001DDFDC
		public Style XmlEntityStyle { get; set; }

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x0600639C RID: 25500 RVA: 0x001DDFE8 File Offset: 0x001DDFE8
		// (set) Token: 0x0600639D RID: 25501 RVA: 0x001DDFF0 File Offset: 0x001DDFF0
		public Style XmlCDataStyle { get; set; }

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x0600639E RID: 25502 RVA: 0x001DDFFC File Offset: 0x001DDFFC
		// (set) Token: 0x0600639F RID: 25503 RVA: 0x001DE004 File Offset: 0x001DE004
		public Style VariableStyle { get; set; }

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x060063A0 RID: 25504 RVA: 0x001DE010 File Offset: 0x001DE010
		// (set) Token: 0x060063A1 RID: 25505 RVA: 0x001DE018 File Offset: 0x001DE018
		public Style KeywordStyle2 { get; set; }

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x060063A2 RID: 25506 RVA: 0x001DE024 File Offset: 0x001DE024
		// (set) Token: 0x060063A3 RID: 25507 RVA: 0x001DE02C File Offset: 0x001DE02C
		public Style KeywordStyle3 { get; set; }

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x060063A4 RID: 25508 RVA: 0x001DE038 File Offset: 0x001DE038
		// (set) Token: 0x060063A5 RID: 25509 RVA: 0x001DE040 File Offset: 0x001DE040
		public Style StatementsStyle { get; set; }

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x060063A6 RID: 25510 RVA: 0x001DE04C File Offset: 0x001DE04C
		// (set) Token: 0x060063A7 RID: 25511 RVA: 0x001DE054 File Offset: 0x001DE054
		public Style FunctionsStyle { get; set; }

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x060063A8 RID: 25512 RVA: 0x001DE060 File Offset: 0x001DE060
		// (set) Token: 0x060063A9 RID: 25513 RVA: 0x001DE068 File Offset: 0x001DE068
		public Style TypesStyle { get; set; }

		// Token: 0x040032EA RID: 13034
		protected static readonly Platform platformType = PlatformType.GetOperationSystemPlatform();

		// Token: 0x040032EB RID: 13035
		public readonly Style BlueBoldStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);

		// Token: 0x040032EC RID: 13036
		public readonly Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);

		// Token: 0x040032ED RID: 13037
		public readonly Style BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);

		// Token: 0x040032EE RID: 13038
		public readonly Style BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);

		// Token: 0x040032EF RID: 13039
		public readonly Style GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);

		// Token: 0x040032F0 RID: 13040
		public readonly Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);

		// Token: 0x040032F1 RID: 13041
		public readonly Style MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);

		// Token: 0x040032F2 RID: 13042
		public readonly Style MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);

		// Token: 0x040032F3 RID: 13043
		public readonly Style RedStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);

		// Token: 0x040032F4 RID: 13044
		public readonly Style BlackStyle = new TextStyle(Brushes.Black, null, FontStyle.Regular);

		// Token: 0x040032F5 RID: 13045
		protected readonly Dictionary<string, SyntaxDescriptor> descByXMLfileNames = new Dictionary<string, SyntaxDescriptor>();

		// Token: 0x040032F6 RID: 13046
		protected readonly List<Style> resilientStyles = new List<Style>(5);

		// Token: 0x040032F7 RID: 13047
		protected Regex CSharpAttributeRegex;

		// Token: 0x040032F8 RID: 13048
		protected Regex CSharpClassNameRegex;

		// Token: 0x040032F9 RID: 13049
		protected Regex CSharpCommentRegex1;

		// Token: 0x040032FA RID: 13050
		protected Regex CSharpCommentRegex2;

		// Token: 0x040032FB RID: 13051
		protected Regex CSharpCommentRegex3;

		// Token: 0x040032FC RID: 13052
		protected Regex CSharpKeywordRegex;

		// Token: 0x040032FD RID: 13053
		protected Regex CSharpNumberRegex;

		// Token: 0x040032FE RID: 13054
		protected Regex CSharpStringRegex;

		// Token: 0x040032FF RID: 13055
		protected Regex HTMLAttrRegex;

		// Token: 0x04003300 RID: 13056
		protected Regex HTMLAttrValRegex;

		// Token: 0x04003301 RID: 13057
		protected Regex HTMLCommentRegex1;

		// Token: 0x04003302 RID: 13058
		protected Regex HTMLCommentRegex2;

		// Token: 0x04003303 RID: 13059
		protected Regex HTMLEndTagRegex;

		// Token: 0x04003304 RID: 13060
		protected Regex HTMLEntityRegex;

		// Token: 0x04003305 RID: 13061
		protected Regex HTMLTagContentRegex;

		// Token: 0x04003306 RID: 13062
		protected Regex HTMLTagNameRegex;

		// Token: 0x04003307 RID: 13063
		protected Regex HTMLTagRegex;

		// Token: 0x04003308 RID: 13064
		protected Regex XMLAttrRegex;

		// Token: 0x04003309 RID: 13065
		protected Regex XMLAttrValRegex;

		// Token: 0x0400330A RID: 13066
		protected Regex XMLCommentRegex1;

		// Token: 0x0400330B RID: 13067
		protected Regex XMLCommentRegex2;

		// Token: 0x0400330C RID: 13068
		protected Regex XMLEndTagRegex;

		// Token: 0x0400330D RID: 13069
		protected Regex XMLEntityRegex;

		// Token: 0x0400330E RID: 13070
		protected Regex XMLTagContentRegex;

		// Token: 0x0400330F RID: 13071
		protected Regex XMLTagNameRegex;

		// Token: 0x04003310 RID: 13072
		protected Regex XMLTagRegex;

		// Token: 0x04003311 RID: 13073
		protected Regex XMLCDataRegex;

		// Token: 0x04003312 RID: 13074
		protected Regex XMLFoldingRegex;

		// Token: 0x04003313 RID: 13075
		protected Regex JScriptCommentRegex1;

		// Token: 0x04003314 RID: 13076
		protected Regex JScriptCommentRegex2;

		// Token: 0x04003315 RID: 13077
		protected Regex JScriptCommentRegex3;

		// Token: 0x04003316 RID: 13078
		protected Regex JScriptKeywordRegex;

		// Token: 0x04003317 RID: 13079
		protected Regex JScriptNumberRegex;

		// Token: 0x04003318 RID: 13080
		protected Regex JScriptStringRegex;

		// Token: 0x04003319 RID: 13081
		protected Regex LuaCommentRegex1;

		// Token: 0x0400331A RID: 13082
		protected Regex LuaCommentRegex2;

		// Token: 0x0400331B RID: 13083
		protected Regex LuaCommentRegex3;

		// Token: 0x0400331C RID: 13084
		protected Regex LuaKeywordRegex;

		// Token: 0x0400331D RID: 13085
		protected Regex LuaNumberRegex;

		// Token: 0x0400331E RID: 13086
		protected Regex LuaStringRegex;

		// Token: 0x0400331F RID: 13087
		protected Regex LuaFunctionsRegex;

		// Token: 0x04003320 RID: 13088
		protected Regex PHPCommentRegex1;

		// Token: 0x04003321 RID: 13089
		protected Regex PHPCommentRegex2;

		// Token: 0x04003322 RID: 13090
		protected Regex PHPCommentRegex3;

		// Token: 0x04003323 RID: 13091
		protected Regex PHPKeywordRegex1;

		// Token: 0x04003324 RID: 13092
		protected Regex PHPKeywordRegex2;

		// Token: 0x04003325 RID: 13093
		protected Regex PHPKeywordRegex3;

		// Token: 0x04003326 RID: 13094
		protected Regex PHPNumberRegex;

		// Token: 0x04003327 RID: 13095
		protected Regex PHPStringRegex;

		// Token: 0x04003328 RID: 13096
		protected Regex PHPVarRegex;

		// Token: 0x04003329 RID: 13097
		protected Regex SQLCommentRegex1;

		// Token: 0x0400332A RID: 13098
		protected Regex SQLCommentRegex2;

		// Token: 0x0400332B RID: 13099
		protected Regex SQLCommentRegex3;

		// Token: 0x0400332C RID: 13100
		protected Regex SQLCommentRegex4;

		// Token: 0x0400332D RID: 13101
		protected Regex SQLFunctionsRegex;

		// Token: 0x0400332E RID: 13102
		protected Regex SQLKeywordsRegex;

		// Token: 0x0400332F RID: 13103
		protected Regex SQLNumberRegex;

		// Token: 0x04003330 RID: 13104
		protected Regex SQLStatementsRegex;

		// Token: 0x04003331 RID: 13105
		protected Regex SQLStringRegex;

		// Token: 0x04003332 RID: 13106
		protected Regex SQLTypesRegex;

		// Token: 0x04003333 RID: 13107
		protected Regex SQLVarRegex;

		// Token: 0x04003334 RID: 13108
		protected Regex VBClassNameRegex;

		// Token: 0x04003335 RID: 13109
		protected Regex VBCommentRegex;

		// Token: 0x04003336 RID: 13110
		protected Regex VBKeywordRegex;

		// Token: 0x04003337 RID: 13111
		protected Regex VBNumberRegex;

		// Token: 0x04003338 RID: 13112
		protected Regex VBStringRegex;

		// Token: 0x04003339 RID: 13113
		protected FastColoredTextBox currentTb;

		// Token: 0x02001051 RID: 4177
		private class XmlFoldingTag
		{
			// Token: 0x17001E19 RID: 7705
			// (get) Token: 0x06008FE8 RID: 36840 RVA: 0x002ADC58 File Offset: 0x002ADC58
			public string Marker
			{
				get
				{
					return this.Name + this.id;
				}
			}

			// Token: 0x04004559 RID: 17753
			public string Name;

			// Token: 0x0400455A RID: 17754
			public int id;

			// Token: 0x0400455B RID: 17755
			public int startLine;
		}
	}
}
