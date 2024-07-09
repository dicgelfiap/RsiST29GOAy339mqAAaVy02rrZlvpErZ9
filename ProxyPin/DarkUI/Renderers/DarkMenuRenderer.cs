using System;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;
using DarkUI.Icons;

namespace DarkUI.Renderers
{
	// Token: 0x02000076 RID: 118
	public class DarkMenuRenderer : ToolStripRenderer
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x00030164 File Offset: 0x00030164
		protected override void Initialize(ToolStrip toolStrip)
		{
			base.Initialize(toolStrip);
			toolStrip.BackColor = Colors.GreyBackground;
			toolStrip.ForeColor = Colors.LightText;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00030184 File Offset: 0x00030184
		protected override void InitializeItem(ToolStripItem item)
		{
			base.InitializeItem(item);
			item.BackColor = Colors.GreyBackground;
			item.ForeColor = Colors.LightText;
			if (item.GetType() == typeof(ToolStripSeparator))
			{
				item.Margin = new Padding(0, 0, 0, 1);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000301DC File Offset: 0x000301DC
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			using (SolidBrush solidBrush = new SolidBrush(Colors.GreyBackground))
			{
				graphics.FillRectangle(solidBrush, e.AffectedBounds);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0003022C File Offset: 0x0003022C
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rect = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
			using (Pen pen = new Pen(Colors.LightBorder))
			{
				graphics.DrawRectangle(pen, rect);
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00030298 File Offset: 0x00030298
		protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rect = new Rectangle(e.ImageRectangle.Left - 2, e.ImageRectangle.Top - 2, e.ImageRectangle.Width + 4, e.ImageRectangle.Height + 4);
			using (SolidBrush solidBrush = new SolidBrush(Colors.LightBorder))
			{
				graphics.FillRectangle(solidBrush, rect);
			}
			using (Pen pen = new Pen(Colors.BlueHighlight))
			{
				Rectangle rect2 = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
				graphics.DrawRectangle(pen, rect2);
			}
			if (e.Item.ImageIndex == -1 && string.IsNullOrEmpty(e.Item.ImageKey) && e.Item.Image == null)
			{
				graphics.DrawImageUnscaled(MenuIcons.tick, new Point(e.ImageRectangle.Left, e.ImageRectangle.Top));
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000303E8 File Offset: 0x000303E8
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rect = new Rectangle(1, 3, e.Item.Width, 1);
			using (SolidBrush solidBrush = new SolidBrush(Colors.LightBorder))
			{
				graphics.FillRectangle(solidBrush, rect);
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00030448 File Offset: 0x00030448
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			e.ArrowColor = Colors.LightText;
			e.ArrowRectangle = new Rectangle(new Point(e.ArrowRectangle.Left, e.ArrowRectangle.Top - 1), e.ArrowRectangle.Size);
			base.OnRenderArrow(e);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000304A8 File Offset: 0x000304A8
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			e.Item.ForeColor = (e.Item.Enabled ? Colors.LightText : Colors.DisabledText);
			if (e.Item.Enabled)
			{
				Color color = e.Item.Selected ? Colors.GreyHighlight : e.Item.BackColor;
				Rectangle rect = new Rectangle(2, 0, e.Item.Width - 3, e.Item.Height);
				using (SolidBrush solidBrush = new SolidBrush(color))
				{
					graphics.FillRectangle(solidBrush, rect);
				}
				if (e.Item.GetType() == typeof(ToolStripMenuItem) && ((ToolStripMenuItem)e.Item).DropDown.Visible && !e.Item.IsOnDropDown)
				{
					using (SolidBrush solidBrush2 = new SolidBrush(Colors.GreySelection))
					{
						graphics.FillRectangle(solidBrush2, rect);
					}
				}
			}
		}
	}
}
