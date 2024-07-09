using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A51 RID: 2641
	[ToolboxItem(false)]
	public class UnfocusablePanel : UserControl
	{
		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x060067D2 RID: 26578 RVA: 0x001F9E18 File Offset: 0x001F9E18
		// (set) Token: 0x060067D3 RID: 26579 RVA: 0x001F9E20 File Offset: 0x001F9E20
		public Color BackColor2 { get; set; }

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x060067D4 RID: 26580 RVA: 0x001F9E2C File Offset: 0x001F9E2C
		// (set) Token: 0x060067D5 RID: 26581 RVA: 0x001F9E34 File Offset: 0x001F9E34
		public Color BorderColor { get; set; }

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x060067D6 RID: 26582 RVA: 0x001F9E40 File Offset: 0x001F9E40
		// (set) Token: 0x060067D7 RID: 26583 RVA: 0x001F9E48 File Offset: 0x001F9E48
		public new string Text { get; set; }

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x060067D8 RID: 26584 RVA: 0x001F9E54 File Offset: 0x001F9E54
		// (set) Token: 0x060067D9 RID: 26585 RVA: 0x001F9E5C File Offset: 0x001F9E5C
		public StringAlignment TextAlignment { get; set; }

		// Token: 0x060067DA RID: 26586 RVA: 0x001F9E68 File Offset: 0x001F9E68
		public UnfocusablePanel()
		{
			base.SetStyle(ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		// Token: 0x060067DB RID: 26587 RVA: 0x001F9E8C File Offset: 0x001F9E8C
		protected override void OnPaint(PaintEventArgs e)
		{
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, this.BackColor2, this.BackColor, 90f))
			{
				e.Graphics.FillRectangle(linearGradientBrush, 0, 0, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
			}
			using (Pen pen = new Pen(this.BorderColor))
			{
				e.Graphics.DrawRectangle(pen, 0, 0, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
			}
			bool flag = !string.IsNullOrEmpty(this.Text);
			if (flag)
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = this.TextAlignment;
				stringFormat.LineAlignment = StringAlignment.Center;
				using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
				{
					e.Graphics.DrawString(this.Text, this.Font, solidBrush, new RectangleF(1f, 1f, (float)(base.ClientSize.Width - 2), (float)(base.ClientSize.Height - 2)), stringFormat);
				}
			}
		}
	}
}
