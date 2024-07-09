using System;
using System.Windows.Forms;
using DarkUI.Renderers;

namespace DarkUI.Controls
{
	// Token: 0x020000AB RID: 171
	public class DarkContextMenu : ContextMenuStrip
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x0003DCA0 File Offset: 0x0003DCA0
		public DarkContextMenu()
		{
			base.Renderer = new DarkMenuRenderer();
		}
	}
}
