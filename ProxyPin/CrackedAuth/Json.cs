using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace CrackedAuth
{
	// Token: 0x0200000C RID: 12
	public static class Json
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000034B0 File Offset: 0x000034B0
		public static object Deserialize(string json)
		{
			if (json == null)
			{
				return null;
			}
			return Json.Parser.Parse(json);
		}

		// Token: 0x02000D45 RID: 3397
		private sealed class Parser : IDisposable
		{
			// Token: 0x060089D4 RID: 35284 RVA: 0x00296430 File Offset: 0x00296430
			public static bool IsWordBreak(char c)
			{
				return char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;
			}

			// Token: 0x060089D5 RID: 35285 RVA: 0x00296450 File Offset: 0x00296450
			private Parser(string jsonString)
			{
				this.json = new StringReader(jsonString);
			}

			// Token: 0x060089D6 RID: 35286 RVA: 0x00296464 File Offset: 0x00296464
			public static object Parse(string jsonString)
			{
				object result;
				using (Json.Parser parser = new Json.Parser(jsonString))
				{
					result = parser.ParseValue();
				}
				return result;
			}

			// Token: 0x060089D7 RID: 35287 RVA: 0x002964A4 File Offset: 0x002964A4
			public void Dispose()
			{
				this.json.Dispose();
				this.json = null;
			}

			// Token: 0x060089D8 RID: 35288 RVA: 0x002964B8 File Offset: 0x002964B8
			private Dictionary<string, object> ParseObject()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				this.json.Read();
				for (;;)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					if (nextToken == Json.Parser.TOKEN.NONE)
					{
						goto IL_60;
					}
					if (nextToken == Json.Parser.TOKEN.CURLY_CLOSE)
					{
						break;
					}
					if (nextToken != Json.Parser.TOKEN.COMMA)
					{
						string text = this.ParseString();
						if (text == null)
						{
							goto IL_62;
						}
						if (this.NextToken != Json.Parser.TOKEN.COLON)
						{
							goto IL_64;
						}
						this.json.Read();
						dictionary[text] = this.ParseValue();
					}
				}
				return dictionary;
				IL_60:
				return null;
				IL_62:
				return null;
				IL_64:
				return null;
			}

			// Token: 0x060089D9 RID: 35289 RVA: 0x00296530 File Offset: 0x00296530
			private List<object> ParseArray()
			{
				List<object> list = new List<object>();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					if (nextToken == Json.Parser.TOKEN.NONE)
					{
						return null;
					}
					if (nextToken != Json.Parser.TOKEN.SQUARED_CLOSE)
					{
						if (nextToken != Json.Parser.TOKEN.COMMA)
						{
							object item = this.ParseByToken(nextToken);
							list.Add(item);
						}
					}
					else
					{
						flag = false;
					}
				}
				return list;
			}

			// Token: 0x060089DA RID: 35290 RVA: 0x00296594 File Offset: 0x00296594
			private object ParseValue()
			{
				Json.Parser.TOKEN nextToken = this.NextToken;
				return this.ParseByToken(nextToken);
			}

			// Token: 0x060089DB RID: 35291 RVA: 0x002965B4 File Offset: 0x002965B4
			private object ParseByToken(Json.Parser.TOKEN token)
			{
				switch (token)
				{
				case Json.Parser.TOKEN.CURLY_OPEN:
					return this.ParseObject();
				case Json.Parser.TOKEN.SQUARED_OPEN:
					return this.ParseArray();
				case Json.Parser.TOKEN.STRING:
					return this.ParseString();
				case Json.Parser.TOKEN.NUMBER:
					return this.ParseNumber();
				case Json.Parser.TOKEN.TRUE:
					return true;
				case Json.Parser.TOKEN.FALSE:
					return false;
				case Json.Parser.TOKEN.NULL:
					return null;
				}
				return null;
			}

			// Token: 0x060089DC RID: 35292 RVA: 0x0029662C File Offset: 0x0029662C
			private string ParseString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.json.Read();
				bool flag = true;
				while (flag && this.json.Peek() != -1)
				{
					char nextChar = this.NextChar;
					if (nextChar != '"')
					{
						if (nextChar != '\\')
						{
							stringBuilder.Append(nextChar);
						}
						else if (this.json.Peek() == -1)
						{
							flag = false;
						}
						else
						{
							nextChar = this.NextChar;
							if (nextChar <= '\\')
							{
								if (nextChar == '"' || nextChar == '/' || nextChar == '\\')
								{
									stringBuilder.Append(nextChar);
								}
							}
							else if (nextChar <= 'f')
							{
								if (nextChar != 'b')
								{
									if (nextChar == 'f')
									{
										stringBuilder.Append('\f');
									}
								}
								else
								{
									stringBuilder.Append('\b');
								}
							}
							else if (nextChar != 'n')
							{
								switch (nextChar)
								{
								case 'r':
									stringBuilder.Append('\r');
									break;
								case 't':
									stringBuilder.Append('\t');
									break;
								case 'u':
								{
									char[] array = new char[4];
									for (int i = 0; i < 4; i++)
									{
										array[i] = this.NextChar;
									}
									stringBuilder.Append((char)Convert.ToInt32(new string(array), 16));
									break;
								}
								}
							}
							else
							{
								stringBuilder.Append('\n');
							}
						}
					}
					else
					{
						flag = false;
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060089DD RID: 35293 RVA: 0x002967A8 File Offset: 0x002967A8
			private object ParseNumber()
			{
				string nextWord = this.NextWord;
				if (nextWord.IndexOf('.') == -1)
				{
					long num;
					long.TryParse(nextWord, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
					return num;
				}
				double num2;
				double.TryParse(nextWord, NumberStyles.Any, CultureInfo.InvariantCulture, out num2);
				return num2;
			}

			// Token: 0x060089DE RID: 35294 RVA: 0x00296804 File Offset: 0x00296804
			private void EatWhitespace()
			{
				while (char.IsWhiteSpace(this.PeekChar))
				{
					this.json.Read();
					if (this.json.Peek() == -1)
					{
						break;
					}
				}
			}

			// Token: 0x17001D5B RID: 7515
			// (get) Token: 0x060089DF RID: 35295 RVA: 0x00296838 File Offset: 0x00296838
			private char PeekChar
			{
				get
				{
					return Convert.ToChar(this.json.Peek());
				}
			}

			// Token: 0x17001D5C RID: 7516
			// (get) Token: 0x060089E0 RID: 35296 RVA: 0x0029684C File Offset: 0x0029684C
			private char NextChar
			{
				get
				{
					return Convert.ToChar(this.json.Read());
				}
			}

			// Token: 0x17001D5D RID: 7517
			// (get) Token: 0x060089E1 RID: 35297 RVA: 0x00296860 File Offset: 0x00296860
			private string NextWord
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (!Json.Parser.IsWordBreak(this.PeekChar))
					{
						stringBuilder.Append(this.NextChar);
						if (this.json.Peek() == -1)
						{
							break;
						}
					}
					return stringBuilder.ToString();
				}
			}

			// Token: 0x17001D5E RID: 7518
			// (get) Token: 0x060089E2 RID: 35298 RVA: 0x002968B0 File Offset: 0x002968B0
			private Json.Parser.TOKEN NextToken
			{
				get
				{
					this.EatWhitespace();
					if (this.json.Peek() == -1)
					{
						return Json.Parser.TOKEN.NONE;
					}
					char peekChar = this.PeekChar;
					if (peekChar <= '[')
					{
						switch (peekChar)
						{
						case '"':
							return Json.Parser.TOKEN.STRING;
						case '#':
						case '$':
						case '%':
						case '&':
						case '\'':
						case '(':
						case ')':
						case '*':
						case '+':
						case '.':
						case '/':
							break;
						case ',':
							this.json.Read();
							return Json.Parser.TOKEN.COMMA;
						case '-':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							return Json.Parser.TOKEN.NUMBER;
						case ':':
							return Json.Parser.TOKEN.COLON;
						default:
							if (peekChar == '[')
							{
								return Json.Parser.TOKEN.SQUARED_OPEN;
							}
							break;
						}
					}
					else
					{
						if (peekChar == ']')
						{
							this.json.Read();
							return Json.Parser.TOKEN.SQUARED_CLOSE;
						}
						if (peekChar == '{')
						{
							return Json.Parser.TOKEN.CURLY_OPEN;
						}
						if (peekChar == '}')
						{
							this.json.Read();
							return Json.Parser.TOKEN.CURLY_CLOSE;
						}
					}
					string nextWord = this.NextWord;
					if (nextWord == "false")
					{
						return Json.Parser.TOKEN.FALSE;
					}
					if (nextWord == "true")
					{
						return Json.Parser.TOKEN.TRUE;
					}
					if (!(nextWord == "null"))
					{
						return Json.Parser.TOKEN.NONE;
					}
					return Json.Parser.TOKEN.NULL;
				}
			}

			// Token: 0x04003EE7 RID: 16103
			private const string WORD_BREAK = "{}[],:\"";

			// Token: 0x04003EE8 RID: 16104
			private StringReader json;

			// Token: 0x02001209 RID: 4617
			private enum TOKEN
			{
				// Token: 0x04004EF2 RID: 20210
				NONE,
				// Token: 0x04004EF3 RID: 20211
				CURLY_OPEN,
				// Token: 0x04004EF4 RID: 20212
				CURLY_CLOSE,
				// Token: 0x04004EF5 RID: 20213
				SQUARED_OPEN,
				// Token: 0x04004EF6 RID: 20214
				SQUARED_CLOSE,
				// Token: 0x04004EF7 RID: 20215
				COLON,
				// Token: 0x04004EF8 RID: 20216
				COMMA,
				// Token: 0x04004EF9 RID: 20217
				STRING,
				// Token: 0x04004EFA RID: 20218
				NUMBER,
				// Token: 0x04004EFB RID: 20219
				TRUE,
				// Token: 0x04004EFC RID: 20220
				FALSE,
				// Token: 0x04004EFD RID: 20221
				NULL
			}
		}
	}
}
