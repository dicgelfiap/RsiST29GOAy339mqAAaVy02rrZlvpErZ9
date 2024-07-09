using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A05 RID: 2565
	public partial class GoToForm : Form
	{
		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x060062B8 RID: 25272 RVA: 0x001D7C5C File Offset: 0x001D7C5C
		// (set) Token: 0x060062B9 RID: 25273 RVA: 0x001D7C64 File Offset: 0x001D7C64
		public int SelectedLineNumber { get; set; }

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x060062BA RID: 25274 RVA: 0x001D7C70 File Offset: 0x001D7C70
		// (set) Token: 0x060062BB RID: 25275 RVA: 0x001D7C78 File Offset: 0x001D7C78
		public int TotalLineCount { get; set; }

		// Token: 0x060062BC RID: 25276 RVA: 0x001D7C84 File Offset: 0x001D7C84
		public GoToForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x001D7C9C File Offset: 0x001D7C9C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.tbLineNumber.Text = this.SelectedLineNumber.ToString();
			this.label.Text = string.Format("Line number (1 - {0}):", this.TotalLineCount);
		}

		// Token: 0x060062BE RID: 25278 RVA: 0x001D7CF4 File Offset: 0x001D7CF4
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.tbLineNumber.Focus();
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x001D7D0C File Offset: 0x001D7D0C
		private void btnOk_Click(object sender, EventArgs e)
		{
			int num;
			bool flag = int.TryParse(this.tbLineNumber.Text, out num);
			if (flag)
			{
				num = Math.Min(num, this.TotalLineCount);
				num = Math.Max(1, num);
				this.SelectedLineNumber = num;
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x001D7D68 File Offset: 0x001D7D68
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}
	}
}
