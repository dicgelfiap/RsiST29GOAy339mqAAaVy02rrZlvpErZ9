using System;
using System.Collections.Generic;

namespace DarkUI.Docking
{
	// Token: 0x02000096 RID: 150
	public class DockGroupState
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00036EB4 File Offset: 0x00036EB4
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x00036EBC File Offset: 0x00036EBC
		public List<string> Contents { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00036EC8 File Offset: 0x00036EC8
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x00036ED0 File Offset: 0x00036ED0
		public string VisibleContent { get; set; }

		// Token: 0x060005BD RID: 1469 RVA: 0x00036EDC File Offset: 0x00036EDC
		public DockGroupState()
		{
			this.Contents = new List<string>();
		}
	}
}
