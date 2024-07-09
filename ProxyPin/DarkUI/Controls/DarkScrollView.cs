using System;
using System.Drawing;
using System.Windows.Forms;

namespace DarkUI.Controls
{
	// Token: 0x020000AF RID: 175
	public abstract class DarkScrollView : DarkScrollBase
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x0003EA68 File Offset: 0x0003EA68
		protected DarkScrollView()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
		}

		// Token: 0x0600073C RID: 1852
		protected abstract void PaintContent(Graphics g);

		// Token: 0x0600073D RID: 1853 RVA: 0x0003EA7C File Offset: 0x0003EA7C
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
			{
				graphics.FillRectangle(solidBrush, base.ClientRectangle);
			}
			graphics.TranslateTransform((float)(base.Viewport.Left * -1), (float)(base.Viewport.Top * -1));
			this.PaintContent(graphics);
			graphics.TranslateTransform((float)base.Viewport.Left, (float)base.Viewport.Top);
			if (this._vScrollBar.Visible && this._hScrollBar.Visible)
			{
				using (SolidBrush solidBrush2 = new SolidBrush(this.BackColor))
				{
					Rectangle rect = new Rectangle(this._hScrollBar.Right, this._vScrollBar.Bottom, this._vScrollBar.Width, this._hScrollBar.Height);
					graphics.FillRectangle(solidBrush2, rect);
				}
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0003EBA4 File Offset: 0x0003EBA4
		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}
	}
}
