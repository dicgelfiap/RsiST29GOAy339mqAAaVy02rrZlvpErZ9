using System;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000B1 RID: 177
	public class DarkSeparator : Control
	{
		// Token: 0x06000748 RID: 1864 RVA: 0x0003EEC8 File Offset: 0x0003EEC8
		public DarkSeparator()
		{
			base.SetStyle(ControlStyles.Selectable, false);
			this.Dock = DockStyle.Top;
			base.Size = new Size(1, 2);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0003EF00 File Offset: 0x0003EF00
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			using (Pen pen = new Pen(Colors.DarkBorder))
			{
				graphics.DrawLine(pen, base.ClientRectangle.Left, 0, base.ClientRectangle.Right, 0);
			}
			using (Pen pen2 = new Pen(Colors.LightBorder))
			{
				graphics.DrawLine(pen2, base.ClientRectangle.Left, 1, base.ClientRectangle.Right, 1);
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0003EFB4 File Offset: 0x0003EFB4
		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}
	}
}
