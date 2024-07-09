using System;
using System.Drawing;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A19 RID: 2585
	public class PaintLineEventArgs : PaintEventArgs
	{
		// Token: 0x060065DC RID: 26076 RVA: 0x001EF8B0 File Offset: 0x001EF8B0
		public PaintLineEventArgs(int iLine, Rectangle rect, Graphics gr, Rectangle clipRect) : base(gr, clipRect)
		{
			this.LineIndex = iLine;
			this.LineRect = rect;
		}

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x060065DD RID: 26077 RVA: 0x001EF8D0 File Offset: 0x001EF8D0
		// (set) Token: 0x060065DE RID: 26078 RVA: 0x001EF8D8 File Offset: 0x001EF8D8
		public int LineIndex { get; private set; }

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x060065DF RID: 26079 RVA: 0x001EF8E4 File Offset: 0x001EF8E4
		// (set) Token: 0x060065E0 RID: 26080 RVA: 0x001EF8EC File Offset: 0x001EF8EC
		public Rectangle LineRect { get; private set; }
	}
}
