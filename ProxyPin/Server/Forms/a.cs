using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrackedAuth;
using DarkUI.Controls;
using DarkUI.Forms;

namespace Server.Forms
{
	// Token: 0x0200004E RID: 78
	public partial class a : DarkForm
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x00017440 File Offset: 0x00017440
		public a()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00017450 File Offset: 0x00017450
		private void darkButton1_Click(object sender, EventArgs e)
		{
			this.o = Auth.login(this.darkTextBox1.Text);
			if (this.o == 1)
			{
				base.Dispose();
				return;
			}
		}

		// Token: 0x04000170 RID: 368
		public int o;
	}
}
