using System;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A54 RID: 2644
	public class ExpandFoldingMarker : VisualMarker
	{
		// Token: 0x060067E1 RID: 26593 RVA: 0x001FA0F8 File Offset: 0x001FA0F8
		public ExpandFoldingMarker(int iLine, Rectangle rectangle) : base(rectangle)
		{
			this.iLine = iLine;
		}

		// Token: 0x060067E2 RID: 26594 RVA: 0x001FA10C File Offset: 0x001FA10C
		public void Draw(Graphics gr, Pen pen, Brush backgroundBrush, Pen forePen)
		{
			gr.FillRectangle(backgroundBrush, this.rectangle);
			gr.DrawRectangle(pen, this.rectangle);
			gr.DrawLine(forePen, this.rectangle.Left + 2, this.rectangle.Top + this.rectangle.Height / 2, this.rectangle.Right - 2, this.rectangle.Top + this.rectangle.Height / 2);
			gr.DrawLine(forePen, this.rectangle.Left + this.rectangle.Width / 2, this.rectangle.Top + 2, this.rectangle.Left + this.rectangle.Width / 2, this.rectangle.Bottom - 2);
		}

		// Token: 0x040034E1 RID: 13537
		public readonly int iLine;
	}
}
