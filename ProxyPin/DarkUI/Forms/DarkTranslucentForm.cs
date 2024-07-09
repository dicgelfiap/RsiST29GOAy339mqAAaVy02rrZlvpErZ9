using System;
using System.Drawing;
using System.Windows.Forms;

namespace DarkUI.Forms
{
	// Token: 0x02000085 RID: 133
	internal partial class DarkTranslucentForm : Form
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x000327F8 File Offset: 0x000327F8
		protected override bool ShowWithoutActivation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000327FC File Offset: 0x000327FC
		public DarkTranslucentForm(Color backColor, double opacity = 0.6)
		{
			base.StartPosition = FormStartPosition.Manual;
			base.FormBorderStyle = FormBorderStyle.None;
			base.Size = new Size(1, 1);
			base.ShowInTaskbar = false;
			base.AllowTransparency = true;
			base.Opacity = opacity;
			this.BackColor = backColor;
		}
	}
}
