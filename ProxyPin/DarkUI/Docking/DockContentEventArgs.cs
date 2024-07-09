using System;

namespace DarkUI.Docking
{
	// Token: 0x02000093 RID: 147
	public class DockContentEventArgs : EventArgs
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00036830 File Offset: 0x00036830
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x00036838 File Offset: 0x00036838
		public DarkDockContent Content { get; private set; }

		// Token: 0x060005A0 RID: 1440 RVA: 0x00036844 File Offset: 0x00036844
		public DockContentEventArgs(DarkDockContent content)
		{
			this.Content = content;
		}
	}
}
