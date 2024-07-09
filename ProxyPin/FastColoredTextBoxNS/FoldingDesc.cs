using System;
using System.Text.RegularExpressions;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A14 RID: 2580
	public class FoldingDesc
	{
		// Token: 0x040032E7 RID: 13031
		public string startMarkerRegex;

		// Token: 0x040032E8 RID: 13032
		public string finishMarkerRegex;

		// Token: 0x040032E9 RID: 13033
		public RegexOptions options = RegexOptions.None;
	}
}
