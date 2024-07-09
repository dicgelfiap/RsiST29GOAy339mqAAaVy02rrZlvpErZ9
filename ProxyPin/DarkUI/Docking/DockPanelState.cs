using System;
using System.Collections.Generic;

namespace DarkUI.Docking
{
	// Token: 0x02000097 RID: 151
	public class DockPanelState
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00036EF0 File Offset: 0x00036EF0
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x00036EF8 File Offset: 0x00036EF8
		public List<DockRegionState> Regions { get; set; }

		// Token: 0x060005C0 RID: 1472 RVA: 0x00036F04 File Offset: 0x00036F04
		public DockPanelState()
		{
			this.Regions = new List<DockRegionState>();
		}
	}
}
