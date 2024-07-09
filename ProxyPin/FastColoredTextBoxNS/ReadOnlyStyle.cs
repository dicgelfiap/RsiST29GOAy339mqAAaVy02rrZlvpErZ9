using System;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A48 RID: 2632
	public class ReadOnlyStyle : Style
	{
		// Token: 0x0600675B RID: 26459 RVA: 0x001F81E8 File Offset: 0x001F81E8
		public ReadOnlyStyle()
		{
			this.IsExportable = false;
		}

		// Token: 0x0600675C RID: 26460 RVA: 0x001F81FC File Offset: 0x001F81FC
		public override void Draw(Graphics gr, Point position, Range range)
		{
		}
	}
}
