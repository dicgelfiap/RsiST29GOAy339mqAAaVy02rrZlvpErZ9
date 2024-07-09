using System;

namespace DarkUI.Controls
{
	// Token: 0x020000B6 RID: 182
	public class ScrollValueEventArgs : EventArgs
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0003F948 File Offset: 0x0003F948
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x0003F950 File Offset: 0x0003F950
		public int Value { get; private set; }

		// Token: 0x0600078A RID: 1930 RVA: 0x0003F95C File Offset: 0x0003F95C
		public ScrollValueEventArgs(int value)
		{
			this.Value = value;
		}
	}
}
