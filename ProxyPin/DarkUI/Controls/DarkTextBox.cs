using System;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000B3 RID: 179
	public class DarkTextBox : TextBox
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x0003F110 File Offset: 0x0003F110
		public DarkTextBox()
		{
			this.BackColor = Colors.LightBackground;
			this.ForeColor = Colors.LightText;
			base.Padding = new Padding(2, 2, 2, 2);
			base.BorderStyle = BorderStyle.FixedSingle;
		}
	}
}
