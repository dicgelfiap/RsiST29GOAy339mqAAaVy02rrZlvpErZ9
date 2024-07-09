using System;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000B2 RID: 178
	public class DarkStatusStrip : StatusStrip
	{
		// Token: 0x0600074B RID: 1867 RVA: 0x0003EFB8 File Offset: 0x0003EFB8
		public DarkStatusStrip()
		{
			this.AutoSize = false;
			base.BackColor = Colors.GreyBackground;
			base.ForeColor = Colors.LightText;
			base.Padding = new Padding(0, 5, 0, 3);
			base.Size = new Size(base.Size.Width, 24);
			base.SizingGrip = false;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0003F020 File Offset: 0x0003F020
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			using (SolidBrush solidBrush = new SolidBrush(Colors.GreyBackground))
			{
				graphics.FillRectangle(solidBrush, base.ClientRectangle);
			}
			using (Pen pen = new Pen(Colors.DarkBorder))
			{
				graphics.DrawLine(pen, base.ClientRectangle.Left, 0, base.ClientRectangle.Right, 0);
			}
			using (Pen pen2 = new Pen(Colors.LightBorder))
			{
				graphics.DrawLine(pen2, base.ClientRectangle.Left, 1, base.ClientRectangle.Right, 1);
			}
		}
	}
}
