using System;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A53 RID: 2643
	public class CollapseFoldingMarker : VisualMarker
	{
		// Token: 0x060067DF RID: 26591 RVA: 0x001FA04C File Offset: 0x001FA04C
		public CollapseFoldingMarker(int iLine, Rectangle rectangle) : base(rectangle)
		{
			this.iLine = iLine;
		}

		// Token: 0x060067E0 RID: 26592 RVA: 0x001FA060 File Offset: 0x001FA060
		public void Draw(Graphics gr, Pen pen, Brush backgroundBrush, Pen forePen)
		{
			gr.FillRectangle(backgroundBrush, this.rectangle);
			gr.DrawRectangle(pen, this.rectangle);
			gr.DrawLine(forePen, this.rectangle.Left + 2, this.rectangle.Top + this.rectangle.Height / 2, this.rectangle.Right - 2, this.rectangle.Top + this.rectangle.Height / 2);
		}

		// Token: 0x040034E0 RID: 13536
		public readonly int iLine;
	}
}
