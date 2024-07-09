using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A0B RID: 2571
	public class MacrosManager
	{
		// Token: 0x06006321 RID: 25377 RVA: 0x001DA0FC File Offset: 0x001DA0FC
		internal MacrosManager(FastColoredTextBox ctrl)
		{
			this.UnderlayingControl = ctrl;
			this.AllowMacroRecordingByUser = true;
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x06006322 RID: 25378 RVA: 0x001DA124 File Offset: 0x001DA124
		// (set) Token: 0x06006323 RID: 25379 RVA: 0x001DA12C File Offset: 0x001DA12C
		public bool AllowMacroRecordingByUser { get; set; }

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x06006324 RID: 25380 RVA: 0x001DA138 File Offset: 0x001DA138
		// (set) Token: 0x06006325 RID: 25381 RVA: 0x001DA158 File Offset: 0x001DA158
		public bool IsRecording
		{
			get
			{
				return this.isRecording;
			}
			set
			{
				this.isRecording = value;
				this.UnderlayingControl.Invalidate();
			}
		}

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x06006326 RID: 25382 RVA: 0x001DA170 File Offset: 0x001DA170
		// (set) Token: 0x06006327 RID: 25383 RVA: 0x001DA178 File Offset: 0x001DA178
		public FastColoredTextBox UnderlayingControl { get; private set; }

		// Token: 0x06006328 RID: 25384 RVA: 0x001DA184 File Offset: 0x001DA184
		public void ExecuteMacros()
		{
			this.IsRecording = false;
			this.UnderlayingControl.BeginUpdate();
			this.UnderlayingControl.Selection.BeginUpdate();
			this.UnderlayingControl.BeginAutoUndo();
			foreach (object obj in this.macro)
			{
				bool flag = obj is Keys;
				if (flag)
				{
					this.UnderlayingControl.ProcessKey((Keys)obj);
				}
				bool flag2 = obj is KeyValuePair<char, Keys>;
				if (flag2)
				{
					KeyValuePair<char, Keys> keyValuePair = (KeyValuePair<char, Keys>)obj;
					this.UnderlayingControl.ProcessKey(keyValuePair.Key, keyValuePair.Value);
				}
			}
			this.UnderlayingControl.EndAutoUndo();
			this.UnderlayingControl.Selection.EndUpdate();
			this.UnderlayingControl.EndUpdate();
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x001DA290 File Offset: 0x001DA290
		public void AddCharToMacros(char c, Keys modifiers)
		{
			this.macro.Add(new KeyValuePair<char, Keys>(c, modifiers));
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x001DA2AC File Offset: 0x001DA2AC
		public void AddKeyToMacros(Keys keyData)
		{
			this.macro.Add(keyData);
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x001DA2C4 File Offset: 0x001DA2C4
		public void ClearMacros()
		{
			this.macro.Clear();
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x001DA2D4 File Offset: 0x001DA2D4
		internal void ProcessKey(Keys keyData)
		{
			bool flag = this.IsRecording;
			if (flag)
			{
				this.AddKeyToMacros(keyData);
			}
		}

		// Token: 0x0600632D RID: 25389 RVA: 0x001DA2FC File Offset: 0x001DA2FC
		internal void ProcessKey(char c, Keys modifiers)
		{
			bool flag = this.IsRecording;
			if (flag)
			{
				this.AddCharToMacros(c, modifiers);
			}
		}

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x0600632E RID: 25390 RVA: 0x001DA324 File Offset: 0x001DA324
		public bool MacroIsEmpty
		{
			get
			{
				return this.macro.Count == 0;
			}
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x0600632F RID: 25391 RVA: 0x001DA34C File Offset: 0x001DA34C
		// (set) Token: 0x06006330 RID: 25392 RVA: 0x001DA484 File Offset: 0x001DA484
		public string Macros
		{
			get
			{
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
				KeysConverter keysConverter = new KeysConverter();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("<macros>");
				foreach (object obj in this.macro)
				{
					bool flag = obj is Keys;
					if (flag)
					{
						stringBuilder.AppendFormat("<item key='{0}' />\r\n", keysConverter.ConvertToString((Keys)obj));
					}
					else
					{
						bool flag2 = obj is KeyValuePair<char, Keys>;
						if (flag2)
						{
							KeyValuePair<char, Keys> keyValuePair = (KeyValuePair<char, Keys>)obj;
							stringBuilder.AppendFormat("<item char='{0}' key='{1}' />\r\n", (int)keyValuePair.Key, keysConverter.ConvertToString(keyValuePair.Value));
						}
					}
				}
				stringBuilder.AppendLine("</macros>");
				Thread.CurrentThread.CurrentUICulture = currentUICulture;
				return stringBuilder.ToString();
			}
			set
			{
				this.isRecording = false;
				this.ClearMacros();
				bool flag = string.IsNullOrEmpty(value);
				if (!flag)
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(value);
					XmlNodeList xmlNodeList = xmlDocument.SelectNodes("./macros/item");
					CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
					Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
					KeysConverter keysConverter = new KeysConverter();
					bool flag2 = xmlNodeList != null;
					if (flag2)
					{
						foreach (object obj in xmlNodeList)
						{
							XmlElement xmlElement = (XmlElement)obj;
							XmlAttribute attributeNode = xmlElement.GetAttributeNode("char");
							XmlAttribute attributeNode2 = xmlElement.GetAttributeNode("key");
							bool flag3 = attributeNode != null;
							if (flag3)
							{
								bool flag4 = attributeNode2 != null;
								if (flag4)
								{
									this.AddCharToMacros((char)int.Parse(attributeNode.Value), (Keys)keysConverter.ConvertFromString(attributeNode2.Value));
								}
								else
								{
									this.AddCharToMacros((char)int.Parse(attributeNode.Value), Keys.None);
								}
							}
							else
							{
								bool flag5 = attributeNode2 != null;
								if (flag5)
								{
									this.AddKeyToMacros((Keys)keysConverter.ConvertFromString(attributeNode2.Value));
								}
							}
						}
					}
					Thread.CurrentThread.CurrentUICulture = currentUICulture;
				}
			}
		}

		// Token: 0x04003271 RID: 12913
		private readonly List<object> macro = new List<object>();

		// Token: 0x04003273 RID: 12915
		private bool isRecording;
	}
}
