using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Server.Helper;
using Server.Properties;

namespace Server.Forms
{
	// Token: 0x0200005F RID: 95
	public partial class FormPorts : DarkForm
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x00024A20 File Offset: 0x00024A20
		public FormPorts()
		{
			this.InitializeComponent();
			base.Opacity = 0.0;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00024A40 File Offset: 0x00024A40
		private void PortsFrm_Load(object sender, EventArgs e)
		{
			Methods.FadeIn(this, 5);
			if (Settings.Default.Ports.Length == 0)
			{
				this.listBox1.Items.AddRange(new object[]
				{
					"8848"
				});
			}
			else
			{
				try
				{
					foreach (string text in Settings.Default.Ports.Split(new string[]
					{
						","
					}, StringSplitOptions.None))
					{
						if (!string.IsNullOrWhiteSpace(text))
						{
							this.listBox1.Items.Add(text.Trim());
						}
					}
				}
				catch
				{
				}
			}
			this.Text = Settings.Version + " - " + Environment.UserName;
			if (!File.Exists(Settings.CertificatePath))
			{
				using (FormCertificate formCertificate = new FormCertificate())
				{
					formCertificate.ShowDialog();
					return;
				}
			}
			Settings.ServerCertificate = new X509Certificate2(Settings.CertificatePath);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00024B68 File Offset: 0x00024B68
		private void button1_Click(object sender, EventArgs e)
		{
			if (this.listBox1.Items.Count > 0)
			{
				string text = "";
				foreach (object obj in this.listBox1.Items)
				{
					string str = (string)obj;
					text = text + str + ",";
				}
				Settings.Default.Ports = text.Remove(text.Length - 1);
				Settings.Default.Save();
				FormPorts.isOK = true;
				base.Close();
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00024C20 File Offset: 0x00024C20
		private void PortsFrm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (!FormPorts.isOK)
			{
				Program.form1.notifyIcon1.Dispose();
				Environment.Exit(0);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00024C44 File Offset: 0x00024C44
		private void BtnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				Convert.ToInt32(this.textPorts.Text.Trim());
				this.listBox1.Items.Add(this.textPorts.Text.Trim());
				this.textPorts.Clear();
			}
			catch
			{
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00024CB0 File Offset: 0x00024CB0
		private void BtnDelete_Click(object sender, EventArgs e)
		{
			this.listBox1.Items.Remove(this.listBox1.SelectedItem);
		}

		// Token: 0x04000276 RID: 630
		private static bool isOK;
	}
}
