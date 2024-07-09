using System;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A56 RID: 2646
	public class StyleVisualMarker : VisualMarker
	{
		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x060067E5 RID: 26597 RVA: 0x001FA230 File Offset: 0x001FA230
		// (set) Token: 0x060067E6 RID: 26598 RVA: 0x001FA238 File Offset: 0x001FA238
		public Style Style { get; private set; }

		// Token: 0x060067E7 RID: 26599 RVA: 0x001FA244 File Offset: 0x001FA244
		public StyleVisualMarker(Rectangle rectangle, Style style) : base(rectangle)
		{
			this.Style = style;
		}
	}
}
