using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Server.Properties;

namespace Server.Forms
{
	// Token: 0x02000055 RID: 85
	public partial class FormSetting : DarkForm
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0001B1F4 File Offset: 0x0001B1F4
		public FormSetting()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0001B204 File Offset: 0x0001B204
		private void button1_Click(object sender, EventArgs e)
		{
			if (this.checkBox1.Checked && (this.textBox1.Text == null || this.textBox2.Text == null))
			{
				MessageBox.Show("Input the WebHook and secret");
			}
			Settings.Default.DingDing = this.checkBox1.Checked;
			Settings.Default.WebHook = this.textBox1.Text;
			Settings.Default.Secret = this.textBox2.Text;
			Settings.Default.Save();
			base.Close();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001B2A0 File Offset: 0x0001B2A0
		private void FormSetting_Load(object sender, EventArgs e)
		{
			this.checkBox1.Checked = Settings.Default.DingDing;
			this.textBox1.Text = Settings.Default.WebHook;
			this.textBox2.Text = Settings.Default.Secret;
		}
	}
}
