using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009FE RID: 2558
	public class Bookmark
	{
		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x0600626A RID: 25194 RVA: 0x001D5868 File Offset: 0x001D5868
		// (set) Token: 0x0600626B RID: 25195 RVA: 0x001D5870 File Offset: 0x001D5870
		public FastColoredTextBox TB { get; private set; }

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x0600626C RID: 25196 RVA: 0x001D587C File Offset: 0x001D587C
		// (set) Token: 0x0600626D RID: 25197 RVA: 0x001D5884 File Offset: 0x001D5884
		public string Name { get; set; }

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x0600626E RID: 25198 RVA: 0x001D5890 File Offset: 0x001D5890
		// (set) Token: 0x0600626F RID: 25199 RVA: 0x001D5898 File Offset: 0x001D5898
		public int LineIndex { get; set; }

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06006270 RID: 25200 RVA: 0x001D58A4 File Offset: 0x001D58A4
		// (set) Token: 0x06006271 RID: 25201 RVA: 0x001D58AC File Offset: 0x001D58AC
		public Color Color { get; set; }

		// Token: 0x06006272 RID: 25202 RVA: 0x001D58B8 File Offset: 0x001D58B8
		public virtual void DoVisible()
		{
			this.TB.Selection.Start = new Place(0, this.LineIndex);
			this.TB.DoRangeVisible(this.TB.Selection, true);
			this.TB.Invalidate();
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x001D590C File Offset: 0x001D590C
		public Bookmark(FastColoredTextBox tb, string name, int lineIndex)
		{
			this.TB = tb;
			this.Name = name;
			this.LineIndex = lineIndex;
			this.Color = tb.BookmarkColor;
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x001D594C File Offset: 0x001D594C
		public virtual void Paint(Graphics gr, Rectangle lineRect)
		{
			int num = this.TB.CharHeight - 1;
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, lineRect.Top, num, num), Color.White, this.Color, 45f))
			{
				gr.FillEllipse(linearGradientBrush, 0, lineRect.Top, num, num);
			}
			using (Pen pen = new Pen(this.Color))
			{
				gr.DrawEllipse(pen, 0, lineRect.Top, num, num);
			}
		}
	}
}
