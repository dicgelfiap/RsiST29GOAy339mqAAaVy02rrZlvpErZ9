using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x0200009E RID: 158
	public class DarkComboBox : ComboBox
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00038218 File Offset: 0x00038218
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x00038220 File Offset: 0x00038220
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color ForeColor { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0003822C File Offset: 0x0003822C
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x00038234 File Offset: 0x00038234
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color BackColor { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00038240 File Offset: 0x00038240
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00038248 File Offset: 0x00038248
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new FlatStyle FlatStyle { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00038254 File Offset: 0x00038254
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0003825C File Offset: 0x0003825C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ComboBoxStyle DropDownStyle { get; set; }

		// Token: 0x06000617 RID: 1559 RVA: 0x00038268 File Offset: 0x00038268
		public DarkComboBox()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			base.DrawMode = DrawMode.OwnerDrawVariable;
			base.FlatStyle = FlatStyle.Flat;
			base.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000382A0 File Offset: 0x000382A0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._buffer = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000382B8 File Offset: 0x000382B8
		protected override void OnTabStopChanged(EventArgs e)
		{
			base.OnTabStopChanged(e);
			base.Invalidate();
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000382C8 File Offset: 0x000382C8
		protected override void OnTabIndexChanged(EventArgs e)
		{
			base.OnTabIndexChanged(e);
			base.Invalidate();
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000382D8 File Offset: 0x000382D8
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000382E8 File Offset: 0x000382E8
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			base.Invalidate();
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000382F8 File Offset: 0x000382F8
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			base.Invalidate();
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00038308 File Offset: 0x00038308
		protected override void OnTextUpdate(EventArgs e)
		{
			base.OnTextUpdate(e);
			base.Invalidate();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00038318 File Offset: 0x00038318
		protected override void OnSelectedValueChanged(EventArgs e)
		{
			base.OnSelectedValueChanged(e);
			base.Invalidate();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00038328 File Offset: 0x00038328
		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			base.OnInvalidated(e);
			this.PaintCombobox();
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00038338 File Offset: 0x00038338
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this._buffer = null;
			base.Invalidate();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00038350 File Offset: 0x00038350
		private void PaintCombobox()
		{
			if (this._buffer == null)
			{
				this._buffer = new Bitmap(base.ClientRectangle.Width, base.ClientRectangle.Height);
			}
			using (Graphics graphics = Graphics.FromImage(this._buffer))
			{
				Rectangle rect = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
				Color lightText = Colors.LightText;
				Color color = Colors.GreySelection;
				Color lightBackground = Colors.LightBackground;
				if (this.Focused && base.TabStop)
				{
					color = Colors.BlueHighlight;
				}
				using (SolidBrush solidBrush = new SolidBrush(lightBackground))
				{
					graphics.FillRectangle(solidBrush, rect);
				}
				using (Pen pen = new Pen(color, 1f))
				{
					Rectangle rect2 = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
					graphics.DrawRectangle(pen, rect2);
				}
				Bitmap scrollbar_arrow_hot = ScrollIcons.scrollbar_arrow_hot;
				graphics.DrawImageUnscaled(scrollbar_arrow_hot, rect.Right - scrollbar_arrow_hot.Width - Consts.Padding / 2, rect.Height / 2 - scrollbar_arrow_hot.Height / 2);
				string s = (base.SelectedItem != null) ? base.SelectedItem.ToString() : this.Text;
				using (SolidBrush solidBrush2 = new SolidBrush(lightText))
				{
					int num = 2;
					Rectangle r = new Rectangle(rect.Left + num, rect.Top + num, rect.Width - scrollbar_arrow_hot.Width - Consts.Padding / 2 - num * 2, rect.Height - num * 2);
					StringFormat format = new StringFormat
					{
						LineAlignment = StringAlignment.Center,
						Alignment = StringAlignment.Near,
						FormatFlags = StringFormatFlags.NoWrap,
						Trimming = StringTrimming.EllipsisCharacter
					};
					graphics.DrawString(s, this.Font, solidBrush2, r, format);
				}
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000385D8 File Offset: 0x000385D8
		protected override void OnPaint(PaintEventArgs e)
		{
			if (this._buffer == null)
			{
				this.PaintCombobox();
			}
			e.Graphics.DrawImageUnscaled(this._buffer, Point.Empty);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00038604 File Offset: 0x00038604
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle bounds = e.Bounds;
			Color lightText = Colors.LightText;
			Color color = Colors.LightBackground;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected || (e.State & DrawItemState.Focus) == DrawItemState.Focus || (e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect)
			{
				color = Colors.BlueSelection;
			}
			using (SolidBrush solidBrush = new SolidBrush(color))
			{
				graphics.FillRectangle(solidBrush, bounds);
			}
			if (e.Index >= 0 && e.Index < base.Items.Count)
			{
				string s = base.Items[e.Index].ToString();
				using (SolidBrush solidBrush2 = new SolidBrush(lightText))
				{
					int num = 2;
					Rectangle r = new Rectangle(bounds.Left + num, bounds.Top + num, bounds.Width - num * 2, bounds.Height - num * 2);
					StringFormat format = new StringFormat
					{
						LineAlignment = StringAlignment.Center,
						Alignment = StringAlignment.Near,
						FormatFlags = StringFormatFlags.NoWrap,
						Trimming = StringTrimming.EllipsisCharacter
					};
					graphics.DrawString(s, this.Font, solidBrush2, r, format);
				}
			}
		}

		// Token: 0x040004CF RID: 1231
		private Bitmap _buffer;
	}
}
