using System;
using System.Text.RegularExpressions;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A13 RID: 2579
	public class RuleDesc
	{
		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x06006350 RID: 25424 RVA: 0x001DB35C File Offset: 0x001DB35C
		public Regex Regex
		{
			get
			{
				bool flag = this.regex == null;
				if (flag)
				{
					this.regex = new Regex(this.pattern, SyntaxHighlighter.RegexCompiledOption | this.options);
				}
				return this.regex;
			}
		}

		// Token: 0x040032E3 RID: 13027
		private Regex regex;

		// Token: 0x040032E4 RID: 13028
		public string pattern;

		// Token: 0x040032E5 RID: 13029
		public RegexOptions options = RegexOptions.None;

		// Token: 0x040032E6 RID: 13030
		public Style style;
	}
}
