using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009F7 RID: 2551
	public class SuggestItem : AutocompleteItem
	{
		// Token: 0x060061D2 RID: 25042 RVA: 0x001D3374 File Offset: 0x001D3374
		public SuggestItem(string text, int imageIndex) : base(text, imageIndex)
		{
		}

		// Token: 0x060061D3 RID: 25043 RVA: 0x001D3380 File Offset: 0x001D3380
		public override CompareResult Compare(string fragmentText)
		{
			return CompareResult.Visible;
		}
	}
}
