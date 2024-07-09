using System;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Renderers;

namespace DarkUI.Controls
{
	// Token: 0x020000B4 RID: 180
	public class DarkToolStrip : ToolStrip
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x0003F154 File Offset: 0x0003F154
		public DarkToolStrip()
		{
			base.Renderer = new DarkToolStripRenderer();
			base.Padding = new Padding(5, 0, 1, 0);
			this.AutoSize = false;
			base.Size = new Size(1, 28);
		}
	}
}
