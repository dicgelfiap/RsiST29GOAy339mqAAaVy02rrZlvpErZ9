using System;
using System.Drawing;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A2A RID: 2602
	[Serializable]
	public class ServiceColors
	{
		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x0600662B RID: 26155 RVA: 0x001EFCEC File Offset: 0x001EFCEC
		// (set) Token: 0x0600662C RID: 26156 RVA: 0x001EFCF4 File Offset: 0x001EFCF4
		public Color CollapseMarkerForeColor { get; set; }

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x0600662D RID: 26157 RVA: 0x001EFD00 File Offset: 0x001EFD00
		// (set) Token: 0x0600662E RID: 26158 RVA: 0x001EFD08 File Offset: 0x001EFD08
		public Color CollapseMarkerBackColor { get; set; }

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x0600662F RID: 26159 RVA: 0x001EFD14 File Offset: 0x001EFD14
		// (set) Token: 0x06006630 RID: 26160 RVA: 0x001EFD1C File Offset: 0x001EFD1C
		public Color CollapseMarkerBorderColor { get; set; }

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06006631 RID: 26161 RVA: 0x001EFD28 File Offset: 0x001EFD28
		// (set) Token: 0x06006632 RID: 26162 RVA: 0x001EFD30 File Offset: 0x001EFD30
		public Color ExpandMarkerForeColor { get; set; }

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06006633 RID: 26163 RVA: 0x001EFD3C File Offset: 0x001EFD3C
		// (set) Token: 0x06006634 RID: 26164 RVA: 0x001EFD44 File Offset: 0x001EFD44
		public Color ExpandMarkerBackColor { get; set; }

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06006635 RID: 26165 RVA: 0x001EFD50 File Offset: 0x001EFD50
		// (set) Token: 0x06006636 RID: 26166 RVA: 0x001EFD58 File Offset: 0x001EFD58
		public Color ExpandMarkerBorderColor { get; set; }

		// Token: 0x06006637 RID: 26167 RVA: 0x001EFD64 File Offset: 0x001EFD64
		public ServiceColors()
		{
			this.CollapseMarkerForeColor = Color.Silver;
			this.CollapseMarkerBackColor = Color.White;
			this.CollapseMarkerBorderColor = Color.Silver;
			this.ExpandMarkerForeColor = Color.Red;
			this.ExpandMarkerBackColor = Color.White;
			this.ExpandMarkerBorderColor = Color.Silver;
		}
	}
}
