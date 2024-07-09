using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A40 RID: 2624
	public struct RangeRect
	{
		// Token: 0x0600672C RID: 26412 RVA: 0x001F72B0 File Offset: 0x001F72B0
		public RangeRect(int iStartLine, int iStartChar, int iEndLine, int iEndChar)
		{
			this.iStartLine = iStartLine;
			this.iStartChar = iStartChar;
			this.iEndLine = iEndLine;
			this.iEndChar = iEndChar;
		}

		// Token: 0x040034AE RID: 13486
		public int iStartLine;

		// Token: 0x040034AF RID: 13487
		public int iStartChar;

		// Token: 0x040034B0 RID: 13488
		public int iEndLine;

		// Token: 0x040034B1 RID: 13489
		public int iEndChar;
	}
}
