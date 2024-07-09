using System;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A09 RID: 2569
	internal class HotkeyWrapper
	{
		// Token: 0x0600630A RID: 25354 RVA: 0x001D9D38 File Offset: 0x001D9D38
		public HotkeyWrapper(Keys keyData, FCTBAction action)
		{
			KeyEventArgs keyEventArgs = new KeyEventArgs(keyData);
			this.Ctrl = keyEventArgs.Control;
			this.Shift = keyEventArgs.Shift;
			this.Alt = keyEventArgs.Alt;
			this.Key = keyEventArgs.KeyCode;
			this.Action = action;
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x001D9D94 File Offset: 0x001D9D94
		public Keys ToKeyData()
		{
			Keys keys = this.Key;
			bool ctrl = this.Ctrl;
			if (ctrl)
			{
				keys |= Keys.Control;
			}
			bool alt = this.Alt;
			if (alt)
			{
				keys |= Keys.Alt;
			}
			bool shift = this.Shift;
			if (shift)
			{
				keys |= Keys.Shift;
			}
			return keys;
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x0600630C RID: 25356 RVA: 0x001D9DF8 File Offset: 0x001D9DF8
		// (set) Token: 0x0600630D RID: 25357 RVA: 0x001D9E7C File Offset: 0x001D9E7C
		public string Modifiers
		{
			get
			{
				string text = "";
				bool ctrl = this.Ctrl;
				if (ctrl)
				{
					text += "Ctrl + ";
				}
				bool shift = this.Shift;
				if (shift)
				{
					text += "Shift + ";
				}
				bool alt = this.Alt;
				if (alt)
				{
					text += "Alt + ";
				}
				return text.Trim(new char[]
				{
					' ',
					'+'
				});
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.Ctrl = (this.Alt = (this.Shift = false));
				}
				else
				{
					this.Ctrl = value.Contains("Ctrl");
					this.Shift = value.Contains("Shift");
					this.Alt = value.Contains("Alt");
				}
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x0600630E RID: 25358 RVA: 0x001D9EF0 File Offset: 0x001D9EF0
		// (set) Token: 0x0600630F RID: 25359 RVA: 0x001D9EF8 File Offset: 0x001D9EF8
		public Keys Key { get; set; }

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x06006310 RID: 25360 RVA: 0x001D9F04 File Offset: 0x001D9F04
		// (set) Token: 0x06006311 RID: 25361 RVA: 0x001D9F0C File Offset: 0x001D9F0C
		public FCTBAction Action { get; set; }

		// Token: 0x0400326B RID: 12907
		private bool Ctrl;

		// Token: 0x0400326C RID: 12908
		private bool Shift;

		// Token: 0x0400326D RID: 12909
		private bool Alt;
	}
}
