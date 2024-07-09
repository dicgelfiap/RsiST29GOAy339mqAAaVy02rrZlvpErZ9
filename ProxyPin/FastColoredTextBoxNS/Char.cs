using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009FF RID: 2559
	public struct Char
	{
		// Token: 0x06006275 RID: 25205 RVA: 0x001D5A00 File Offset: 0x001D5A00
		public Char(char c)
		{
			this.c = c;
			this.style = StyleIndex.None;
		}

		// Token: 0x04003237 RID: 12855
		public char c;

		// Token: 0x04003238 RID: 12856
		public StyleIndex style;
	}
}
