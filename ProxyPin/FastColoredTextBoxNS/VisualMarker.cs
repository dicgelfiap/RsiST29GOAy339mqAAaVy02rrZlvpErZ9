using System;
using System.Drawing;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A52 RID: 2642
	public class VisualMarker
	{
		// Token: 0x060067DC RID: 26588 RVA: 0x001FA014 File Offset: 0x001FA014
		public VisualMarker(Rectangle rectangle)
		{
			this.rectangle = rectangle;
		}

		// Token: 0x060067DD RID: 26589 RVA: 0x001FA028 File Offset: 0x001FA028
		public virtual void Draw(Graphics gr, Pen pen)
		{
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x060067DE RID: 26590 RVA: 0x001FA02C File Offset: 0x001FA02C
		public virtual Cursor Cursor
		{
			get
			{
				return Cursors.Hand;
			}
		}

		// Token: 0x040034DF RID: 13535
		public readonly Rectangle rectangle;
	}
}
