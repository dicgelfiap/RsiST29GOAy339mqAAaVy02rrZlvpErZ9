using System;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;
using DarkUI.Extensions;
using DarkUI.Icons;

namespace DarkUI.Renderers
{
	// Token: 0x02000077 RID: 119
	public class DarkToolStripRenderer : DarkMenuRenderer
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x000305F0 File Offset: 0x000305F0
		protected override void InitializeItem(ToolStripItem item)
		{
			base.InitializeItem(item);
			if (item.GetType() == typeof(ToolStripSeparator) && !((ToolStripSeparator)item).IsOnDropDown)
			{
				item.Margin = new Padding(0, 0, 2, 0);
			}
			if (item.GetType() == typeof(ToolStripButton))
			{
				item.AutoSize = false;
				item.Size = new Size(24, 24);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00030674 File Offset: 0x00030674
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			base.OnRenderToolStripBackground(e);
			Graphics graphics = e.Graphics;
			if (e.ToolStrip.GetType() == typeof(ToolStripOverflow))
			{
				using (Pen pen = new Pen(Colors.GreyBackground))
				{
					Rectangle rect = new Rectangle(e.AffectedBounds.Left, e.AffectedBounds.Top, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1);
					graphics.DrawRectangle(pen, rect);
				}
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00030728 File Offset: 0x00030728
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip.GetType() != typeof(ToolStrip))
			{
				base.OnRenderToolStripBorder(e);
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00030750 File Offset: 0x00030750
		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rect = new Rectangle(0, 1, e.Item.Width, e.Item.Height - 2);
			if (e.Item.Selected || e.Item.Pressed)
			{
				using (SolidBrush solidBrush = new SolidBrush(Colors.GreySelection))
				{
					graphics.FillRectangle(solidBrush, rect);
				}
			}
			if (e.Item.GetType() == typeof(ToolStripButton))
			{
				ToolStripButton toolStripButton = (ToolStripButton)e.Item;
				if (toolStripButton.Checked)
				{
					using (SolidBrush solidBrush2 = new SolidBrush(Colors.GreySelection))
					{
						graphics.FillRectangle(solidBrush2, rect);
					}
				}
				if (toolStripButton.Checked && toolStripButton.Selected)
				{
					Rectangle rect2 = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
					using (Pen pen = new Pen(Colors.GreyHighlight))
					{
						graphics.DrawRectangle(pen, rect2);
					}
				}
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000308B8 File Offset: 0x000308B8
		protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rect = new Rectangle(0, 1, e.Item.Width, e.Item.Height - 2);
			if (e.Item.Selected || e.Item.Pressed)
			{
				using (SolidBrush solidBrush = new SolidBrush(Colors.GreySelection))
				{
					graphics.FillRectangle(solidBrush, rect);
				}
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00030944 File Offset: 0x00030944
		protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			if (e.GripStyle == ToolStripGripStyle.Hidden)
			{
				return;
			}
			Graphics graphics = e.Graphics;
			using (Bitmap bitmap = MenuIcons.grip.SetColor(Colors.LightBorder))
			{
				graphics.DrawImageUnscaled(bitmap, new Point(e.AffectedBounds.Left, e.AffectedBounds.Top));
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000309C0 File Offset: 0x000309C0
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			if (((ToolStripSeparator)e.Item).IsOnDropDown)
			{
				base.OnRenderSeparator(e);
				return;
			}
			Rectangle rectangle = new Rectangle(3, 3, 2, e.Item.Height - 4);
			using (Pen pen = new Pen(Colors.DarkBorder))
			{
				graphics.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Height);
			}
			using (Pen pen2 = new Pen(Colors.LightBorder))
			{
				graphics.DrawLine(pen2, rectangle.Left + 1, rectangle.Top, rectangle.Left + 1, rectangle.Height);
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00030AA8 File Offset: 0x00030AA8
		protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			if (e.Image == null)
			{
				return;
			}
			if (e.Item.Enabled)
			{
				graphics.DrawImageUnscaled(e.Image, new Point(e.ImageRectangle.Left, e.ImageRectangle.Top));
				return;
			}
			ControlPaint.DrawImageDisabled(graphics, e.Image, e.ImageRectangle.Left, e.ImageRectangle.Top, Color.Transparent);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00030B38 File Offset: 0x00030B38
		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
		}
	}
}
