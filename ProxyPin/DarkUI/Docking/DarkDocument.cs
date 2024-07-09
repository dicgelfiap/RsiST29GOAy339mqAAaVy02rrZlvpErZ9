using System;
using System.ComponentModel;
using DarkUI.Config;

namespace DarkUI.Docking
{
	// Token: 0x0200008D RID: 141
	[ToolboxItem(false)]
	public class DarkDocument : DarkDockContent
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00035A6C File Offset: 0x00035A6C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DarkDockArea DefaultDockArea
		{
			get
			{
				return base.DefaultDockArea;
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00035A74 File Offset: 0x00035A74
		public DarkDocument()
		{
			this.BackColor = Colors.GreyBackground;
			base.DefaultDockArea = DarkDockArea.Document;
		}
	}
}
