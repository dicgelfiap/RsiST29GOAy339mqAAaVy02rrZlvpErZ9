using System;

namespace DarkUI.Docking
{
	// Token: 0x02000095 RID: 149
	internal class DockDropCollection
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00036E34 File Offset: 0x00036E34
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00036E3C File Offset: 0x00036E3C
		internal DockDropArea DropArea { get; private set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00036E48 File Offset: 0x00036E48
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00036E50 File Offset: 0x00036E50
		internal DockDropArea InsertBeforeArea { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00036E5C File Offset: 0x00036E5C
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00036E64 File Offset: 0x00036E64
		internal DockDropArea InsertAfterArea { get; private set; }

		// Token: 0x060005B8 RID: 1464 RVA: 0x00036E70 File Offset: 0x00036E70
		internal DockDropCollection(DarkDockPanel dockPanel, DarkDockGroup group)
		{
			this.DropArea = new DockDropArea(dockPanel, group, DockInsertType.None);
			this.InsertBeforeArea = new DockDropArea(dockPanel, group, DockInsertType.Before);
			this.InsertAfterArea = new DockDropArea(dockPanel, group, DockInsertType.After);
		}
	}
}
