using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Forms
{
	// Token: 0x02000082 RID: 130
	public partial class DarkForm : Form
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00032290 File Offset: 0x00032290
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00032298 File Offset: 0x00032298
		[Category("Appearance")]
		[Description("Determines whether a single pixel border should be rendered around the form.")]
		[DefaultValue(false)]
		public bool FlatBorder
		{
			get
			{
				return this._flatBorder;
			}
			set
			{
				this._flatBorder = value;
				base.Invalidate();
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000322A8 File Offset: 0x000322A8
		public DarkForm()
		{
			this.BackColor = Colors.GreyBackground;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000322BC File Offset: 0x000322BC
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			if (!this._flatBorder)
			{
				return;
			}
			Graphics graphics = e.Graphics;
			using (Pen pen = new Pen(Colors.DarkBorder))
			{
				Rectangle rect = new Rectangle(base.ClientRectangle.Location, new Size(base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1));
				graphics.DrawRectangle(pen, rect);
			}
		}

		// Token: 0x0400044D RID: 1101
		private bool _flatBorder;
	}
}
