using System;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A57 RID: 2647
	public class VisualMarkerEventArgs : MouseEventArgs
	{
		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x060067E8 RID: 26600 RVA: 0x001FA258 File Offset: 0x001FA258
		// (set) Token: 0x060067E9 RID: 26601 RVA: 0x001FA260 File Offset: 0x001FA260
		public Style Style { get; private set; }

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x060067EA RID: 26602 RVA: 0x001FA26C File Offset: 0x001FA26C
		// (set) Token: 0x060067EB RID: 26603 RVA: 0x001FA274 File Offset: 0x001FA274
		public StyleVisualMarker Marker { get; private set; }

		// Token: 0x060067EC RID: 26604 RVA: 0x001FA280 File Offset: 0x001FA280
		public VisualMarkerEventArgs(Style style, StyleVisualMarker marker, MouseEventArgs args) : base(args.Button, args.Clicks, args.X, args.Y, args.Delta)
		{
			this.Style = style;
			this.Marker = marker;
		}
	}
}
