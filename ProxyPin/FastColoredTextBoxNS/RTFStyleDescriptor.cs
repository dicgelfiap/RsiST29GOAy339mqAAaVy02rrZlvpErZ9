using System;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A04 RID: 2564
	public class RTFStyleDescriptor
	{
		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x060062B1 RID: 25265 RVA: 0x001D7C14 File Offset: 0x001D7C14
		// (set) Token: 0x060062B2 RID: 25266 RVA: 0x001D7C1C File Offset: 0x001D7C1C
		public Color ForeColor { get; set; }

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x060062B3 RID: 25267 RVA: 0x001D7C28 File Offset: 0x001D7C28
		// (set) Token: 0x060062B4 RID: 25268 RVA: 0x001D7C30 File Offset: 0x001D7C30
		public Color BackColor { get; set; }

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x060062B5 RID: 25269 RVA: 0x001D7C3C File Offset: 0x001D7C3C
		// (set) Token: 0x060062B6 RID: 25270 RVA: 0x001D7C44 File Offset: 0x001D7C44
		public string AdditionalTags { get; set; }
	}
}
