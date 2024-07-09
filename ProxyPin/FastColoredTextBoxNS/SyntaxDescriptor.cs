using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A12 RID: 2578
	public class SyntaxDescriptor : IDisposable
	{
		// Token: 0x0600634E RID: 25422 RVA: 0x001DB2A0 File Offset: 0x001DB2A0
		public void Dispose()
		{
			foreach (Style style in this.styles)
			{
				style.Dispose();
			}
		}

		// Token: 0x040032DB RID: 13019
		public char leftBracket = '(';

		// Token: 0x040032DC RID: 13020
		public char rightBracket = ')';

		// Token: 0x040032DD RID: 13021
		public char leftBracket2 = '{';

		// Token: 0x040032DE RID: 13022
		public char rightBracket2 = '}';

		// Token: 0x040032DF RID: 13023
		public BracketsHighlightStrategy bracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;

		// Token: 0x040032E0 RID: 13024
		public readonly List<Style> styles = new List<Style>();

		// Token: 0x040032E1 RID: 13025
		public readonly List<RuleDesc> rules = new List<RuleDesc>();

		// Token: 0x040032E2 RID: 13026
		public readonly List<FoldingDesc> foldings = new List<FoldingDesc>();
	}
}
