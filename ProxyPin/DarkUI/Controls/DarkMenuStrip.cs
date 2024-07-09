using System;
using System.Windows.Forms;
using DarkUI.Renderers;

namespace DarkUI.Controls
{
	// Token: 0x020000AD RID: 173
	public class DarkMenuStrip : MenuStrip
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x0003DDD4 File Offset: 0x0003DDD4
		public DarkMenuStrip()
		{
			base.Renderer = new DarkMenuRenderer();
			base.Padding = new Padding(3, 2, 0, 2);
		}
	}
}
