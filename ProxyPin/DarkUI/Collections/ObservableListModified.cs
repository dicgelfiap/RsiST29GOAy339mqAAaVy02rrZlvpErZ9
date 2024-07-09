using System;
using System.Collections.Generic;

namespace DarkUI.Collections
{
	// Token: 0x020000BA RID: 186
	public class ObservableListModified<T> : EventArgs
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0003FD80 File Offset: 0x0003FD80
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x0003FD88 File Offset: 0x0003FD88
		public IEnumerable<T> Items { get; private set; }

		// Token: 0x060007B1 RID: 1969 RVA: 0x0003FD94 File Offset: 0x0003FD94
		public ObservableListModified(IEnumerable<T> items)
		{
			this.Items = items;
		}
	}
}
