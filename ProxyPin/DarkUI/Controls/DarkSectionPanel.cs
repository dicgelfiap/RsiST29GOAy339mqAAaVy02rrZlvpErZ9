using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000B0 RID: 176
	public class DarkSectionPanel : Panel
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0003EBA8 File Offset: 0x0003EBA8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0003EBB0 File Offset: 0x0003EBB0
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0003EBB8 File Offset: 0x0003EBB8
		[Category("Appearance")]
		[Description("The section header text associated with this control.")]
		public string SectionHeader
		{
			get
			{
				return this._sectionHeader;
			}
			set
			{
				this._sectionHeader = value;
				base.Invalidate();
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0003EBC8 File Offset: 0x0003EBC8
		public DarkSectionPanel()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			base.Padding = new Padding(1, 25, 1, 1);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0003EBFC File Offset: 0x0003EBFC
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			base.Invalidate();
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0003EC0C File Offset: 0x0003EC0C
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			base.Invalidate();
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0003EC1C File Offset: 0x0003EC1C
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (base.Controls.Count > 0)
			{
				base.Controls[0].Focus();
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0003EC58 File Offset: 0x0003EC58
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle clientRectangle = base.ClientRectangle;
			using (SolidBrush solidBrush = new SolidBrush(Colors.GreyBackground))
			{
				graphics.FillRectangle(solidBrush, clientRectangle);
			}
			Color color = base.ContainsFocus ? Colors.BlueBackground : Colors.HeaderBackground;
			Color color2 = base.ContainsFocus ? Colors.DarkBlueBorder : Colors.DarkBorder;
			Color color3 = base.ContainsFocus ? Colors.LightBlueBorder : Colors.LightBorder;
			using (SolidBrush solidBrush2 = new SolidBrush(color))
			{
				Rectangle rect = new Rectangle(0, 0, clientRectangle.Width, 25);
				graphics.FillRectangle(solidBrush2, rect);
			}
			using (Pen pen = new Pen(color2))
			{
				graphics.DrawLine(pen, clientRectangle.Left, 0, clientRectangle.Right, 0);
				graphics.DrawLine(pen, clientRectangle.Left, 24, clientRectangle.Right, 24);
			}
			using (Pen pen2 = new Pen(color3))
			{
				graphics.DrawLine(pen2, clientRectangle.Left, 1, clientRectangle.Right, 1);
			}
			int num = 3;
			using (SolidBrush solidBrush3 = new SolidBrush(Colors.LightText))
			{
				Rectangle r = new Rectangle(num, 0, clientRectangle.Width - 4 - num, 25);
				StringFormat format = new StringFormat
				{
					Alignment = StringAlignment.Near,
					LineAlignment = StringAlignment.Center,
					FormatFlags = StringFormatFlags.NoWrap,
					Trimming = StringTrimming.EllipsisCharacter
				};
				graphics.DrawString(this.SectionHeader, this.Font, solidBrush3, r, format);
			}
			using (Pen pen3 = new Pen(Colors.DarkBorder, 1f))
			{
				Rectangle rect2 = new Rectangle(clientRectangle.Left, clientRectangle.Top, clientRectangle.Width - 1, clientRectangle.Height - 1);
				graphics.DrawRectangle(pen3, rect2);
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0003EEC4 File Offset: 0x0003EEC4
		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		// Token: 0x04000536 RID: 1334
		private string _sectionHeader;
	}
}
