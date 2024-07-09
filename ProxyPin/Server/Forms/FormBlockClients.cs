using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Server.Properties;

namespace Server.Forms
{
	// Token: 0x02000058 RID: 88
	public partial class FormBlockClients : DarkForm
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0001BF14 File Offset: 0x0001BF14
		public FormBlockClients()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001BF24 File Offset: 0x0001BF24
		private void BtnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				this.listBlocked.Items.Add(this.txtBlock.Text);
				this.txtBlock.Clear();
			}
			catch
			{
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001BF74 File Offset: 0x0001BF74
		private void BtnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = this.listBlocked.SelectedIndices.Count - 1; i >= 0; i--)
				{
					this.listBlocked.Items.RemoveAt(this.listBlocked.SelectedIndices[i]);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001BFE0 File Offset: 0x0001BFE0
		private void FormBlockClients_Load(object sender, EventArgs e)
		{
			try
			{
				this.listBlocked.Items.Clear();
				if (!string.IsNullOrWhiteSpace(Settings.Default.txtBlocked))
				{
					foreach (string text in Settings.Default.txtBlocked.Split(new char[]
					{
						','
					}))
					{
						if (!string.IsNullOrWhiteSpace(text))
						{
							this.listBlocked.Items.Add(text);
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001C080 File Offset: 0x0001C080
		private void FormBlockClients_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				List<string> blocked = Settings.Blocked;
				lock (blocked)
				{
					Settings.Blocked.Clear();
					List<string> list = new List<string>();
					foreach (object obj in this.listBlocked.Items)
					{
						string item = (string)obj;
						list.Add(item);
						Settings.Blocked.Add(item);
					}
					Settings.Default.txtBlocked = string.Join(",", list);
					Settings.Default.Save();
				}
			}
			catch
			{
			}
		}
	}
}
