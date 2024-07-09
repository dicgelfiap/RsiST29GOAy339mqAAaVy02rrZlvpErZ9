using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009F5 RID: 2549
	public class SnippetAutocompleteItem : AutocompleteItem
	{
		// Token: 0x060061CA RID: 25034 RVA: 0x001D3040 File Offset: 0x001D3040
		public SnippetAutocompleteItem(string snippet)
		{
			this.Text = snippet.Replace("\r", "");
			this.ToolTipTitle = "Code snippet:";
			this.ToolTipText = this.Text;
		}

		// Token: 0x060061CB RID: 25035 RVA: 0x001D3088 File Offset: 0x001D3088
		public override string ToString()
		{
			return this.MenuText ?? this.Text.Replace("\n", " ").Replace("^", "");
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x001D30D4 File Offset: 0x001D30D4
		public override string GetTextForReplace()
		{
			return this.Text;
		}

		// Token: 0x060061CD RID: 25037 RVA: 0x001D30F4 File Offset: 0x001D30F4
		public override void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
		{
			e.Tb.BeginUpdate();
			e.Tb.Selection.BeginUpdate();
			Place start = popupMenu.Fragment.Start;
			Place start2 = e.Tb.Selection.Start;
			bool autoIndent = e.Tb.AutoIndent;
			if (autoIndent)
			{
				for (int i = start.iLine + 1; i <= start2.iLine; i++)
				{
					e.Tb.Selection.Start = new Place(0, i);
					e.Tb.DoAutoIndent(i);
				}
			}
			e.Tb.Selection.Start = start;
			while (e.Tb.Selection.CharBeforeStart != '^')
			{
				bool flag = !e.Tb.Selection.GoRightThroughFolded();
				if (flag)
				{
					break;
				}
			}
			e.Tb.Selection.GoLeft(true);
			e.Tb.InsertText("");
			e.Tb.Selection.EndUpdate();
			e.Tb.EndUpdate();
		}

		// Token: 0x060061CE RID: 25038 RVA: 0x001D3234 File Offset: 0x001D3234
		public override CompareResult Compare(string fragmentText)
		{
			bool flag = this.Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase) && this.Text != fragmentText;
			CompareResult result;
			if (flag)
			{
				result = CompareResult.Visible;
			}
			else
			{
				result = CompareResult.Hidden;
			}
			return result;
		}
	}
}
