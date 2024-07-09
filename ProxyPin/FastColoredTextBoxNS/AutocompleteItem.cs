using System;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009F3 RID: 2547
	public class AutocompleteItem
	{
		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x060061B5 RID: 25013 RVA: 0x001D2E38 File Offset: 0x001D2E38
		// (set) Token: 0x060061B6 RID: 25014 RVA: 0x001D2E40 File Offset: 0x001D2E40
		public AutocompleteMenu Parent { get; internal set; }

		// Token: 0x060061B7 RID: 25015 RVA: 0x001D2E4C File Offset: 0x001D2E4C
		public AutocompleteItem()
		{
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x001D2E60 File Offset: 0x001D2E60
		public AutocompleteItem(string text)
		{
			this.Text = text;
		}

		// Token: 0x060061B9 RID: 25017 RVA: 0x001D2E78 File Offset: 0x001D2E78
		public AutocompleteItem(string text, int imageIndex) : this(text)
		{
			this.ImageIndex = imageIndex;
		}

		// Token: 0x060061BA RID: 25018 RVA: 0x001D2E8C File Offset: 0x001D2E8C
		public AutocompleteItem(string text, int imageIndex, string menuText) : this(text, imageIndex)
		{
			this.menuText = menuText;
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x001D2EA0 File Offset: 0x001D2EA0
		public AutocompleteItem(string text, int imageIndex, string menuText, string toolTipTitle, string toolTipText) : this(text, imageIndex, menuText)
		{
			this.toolTipTitle = toolTipTitle;
			this.toolTipText = toolTipText;
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x001D2EC0 File Offset: 0x001D2EC0
		public virtual string GetTextForReplace()
		{
			return this.Text;
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x001D2EE0 File Offset: 0x001D2EE0
		public virtual CompareResult Compare(string fragmentText)
		{
			bool flag = this.Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase) && this.Text != fragmentText;
			CompareResult result;
			if (flag)
			{
				result = CompareResult.VisibleAndSelected;
			}
			else
			{
				result = CompareResult.Hidden;
			}
			return result;
		}

		// Token: 0x060061BE RID: 25022 RVA: 0x001D2F2C File Offset: 0x001D2F2C
		public override string ToString()
		{
			return this.menuText ?? this.Text;
		}

		// Token: 0x060061BF RID: 25023 RVA: 0x001D2F58 File Offset: 0x001D2F58
		public virtual void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
		{
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x060061C0 RID: 25024 RVA: 0x001D2F5C File Offset: 0x001D2F5C
		// (set) Token: 0x060061C1 RID: 25025 RVA: 0x001D2F7C File Offset: 0x001D2F7C
		public virtual string ToolTipTitle
		{
			get
			{
				return this.toolTipTitle;
			}
			set
			{
				this.toolTipTitle = value;
			}
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x060061C2 RID: 25026 RVA: 0x001D2F88 File Offset: 0x001D2F88
		// (set) Token: 0x060061C3 RID: 25027 RVA: 0x001D2FA8 File Offset: 0x001D2FA8
		public virtual string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x060061C4 RID: 25028 RVA: 0x001D2FB4 File Offset: 0x001D2FB4
		// (set) Token: 0x060061C5 RID: 25029 RVA: 0x001D2FD4 File Offset: 0x001D2FD4
		public virtual string MenuText
		{
			get
			{
				return this.menuText;
			}
			set
			{
				this.menuText = value;
			}
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x060061C6 RID: 25030 RVA: 0x001D2FE0 File Offset: 0x001D2FE0
		// (set) Token: 0x060061C7 RID: 25031 RVA: 0x001D3000 File Offset: 0x001D3000
		public virtual Color ForeColor
		{
			get
			{
				return Color.Transparent;
			}
			set
			{
				throw new NotImplementedException("Override this property to change color");
			}
		}

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x060061C8 RID: 25032 RVA: 0x001D3010 File Offset: 0x001D3010
		// (set) Token: 0x060061C9 RID: 25033 RVA: 0x001D3030 File Offset: 0x001D3030
		public virtual Color BackColor
		{
			get
			{
				return Color.Transparent;
			}
			set
			{
				throw new NotImplementedException("Override this property to change color");
			}
		}

		// Token: 0x04003206 RID: 12806
		public string Text;

		// Token: 0x04003207 RID: 12807
		public int ImageIndex = -1;

		// Token: 0x04003208 RID: 12808
		public object Tag;

		// Token: 0x04003209 RID: 12809
		private string toolTipTitle;

		// Token: 0x0400320A RID: 12810
		private string toolTipText;

		// Token: 0x0400320B RID: 12811
		private string menuText;
	}
}
