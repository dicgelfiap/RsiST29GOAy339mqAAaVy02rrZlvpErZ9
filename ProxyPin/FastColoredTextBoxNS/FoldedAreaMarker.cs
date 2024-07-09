using System;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A55 RID: 2645
	public class FoldedAreaMarker : VisualMarker
	{
		// Token: 0x060067E3 RID: 26595 RVA: 0x001FA208 File Offset: 0x001FA208
		public FoldedAreaMarker(int iLine, Rectangle rectangle) : base(rectangle)
		{
			this.iLine = iLine;
		}

		// Token: 0x060067E4 RID: 26596 RVA: 0x001FA21C File Offset: 0x001FA21C
		public override void Draw(Graphics gr, Pen pen)
		{
			gr.DrawRectangle(pen, this.rectangle);
		}

		// Token: 0x040034E2 RID: 13538
		public readonly int iLine;
	}
}
