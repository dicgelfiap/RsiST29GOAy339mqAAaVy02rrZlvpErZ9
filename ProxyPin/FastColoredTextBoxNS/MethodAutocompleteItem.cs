using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009F6 RID: 2550
	public class MethodAutocompleteItem : AutocompleteItem
	{
		// Token: 0x060061CF RID: 25039 RVA: 0x001D3280 File Offset: 0x001D3280
		public MethodAutocompleteItem(string text) : base(text)
		{
			this.lowercaseText = this.Text.ToLower();
		}

		// Token: 0x060061D0 RID: 25040 RVA: 0x001D329C File Offset: 0x001D329C
		public override CompareResult Compare(string fragmentText)
		{
			int num = fragmentText.LastIndexOf('.');
			bool flag = num < 0;
			CompareResult result;
			if (flag)
			{
				result = CompareResult.Hidden;
			}
			else
			{
				string text = fragmentText.Substring(num + 1);
				this.firstPart = fragmentText.Substring(0, num);
				bool flag2 = text == "";
				if (flag2)
				{
					result = CompareResult.Visible;
				}
				else
				{
					bool flag3 = this.Text.StartsWith(text, StringComparison.InvariantCultureIgnoreCase);
					if (flag3)
					{
						result = CompareResult.VisibleAndSelected;
					}
					else
					{
						bool flag4 = this.lowercaseText.Contains(text.ToLower());
						if (flag4)
						{
							result = CompareResult.Visible;
						}
						else
						{
							result = CompareResult.Hidden;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060061D1 RID: 25041 RVA: 0x001D3344 File Offset: 0x001D3344
		public override string GetTextForReplace()
		{
			return this.firstPart + "." + this.Text;
		}

		// Token: 0x04003211 RID: 12817
		private string firstPart;

		// Token: 0x04003212 RID: 12818
		private string lowercaseText;
	}
}
