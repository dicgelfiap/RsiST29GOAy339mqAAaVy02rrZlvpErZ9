using System;
using System.Collections.Generic;
using System.Drawing;

namespace DarkUI.Docking
{
	// Token: 0x02000098 RID: 152
	public class DockRegionState
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00036F18 File Offset: 0x00036F18
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00036F20 File Offset: 0x00036F20
		public DarkDockArea Area { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00036F2C File Offset: 0x00036F2C
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00036F34 File Offset: 0x00036F34
		public Size Size { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00036F40 File Offset: 0x00036F40
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x00036F48 File Offset: 0x00036F48
		public List<DockGroupState> Groups { get; set; }

		// Token: 0x060005C7 RID: 1479 RVA: 0x00036F54 File Offset: 0x00036F54
		public DockRegionState()
		{
			this.Groups = new List<DockGroupState>();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00036F68 File Offset: 0x00036F68
		public DockRegionState(DarkDockArea area) : this()
		{
			this.Area = area;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00036F78 File Offset: 0x00036F78
		public DockRegionState(DarkDockArea area, Size size) : this(area)
		{
			this.Size = size;
		}
	}
}
