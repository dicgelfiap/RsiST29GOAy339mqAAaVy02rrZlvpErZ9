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
	// Token: 0x02000060 RID: 96
	public partial class FormProcessManager : DarkForm
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000252CC File Offset: 0x000252CC
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x000252D4 File Offset: 0x000252D4
		public Form1 F { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000252E0 File Offset: 0x000252E0
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x000252E8 File Offset: 0x000252E8
		internal Clients Client { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000252F4 File Offset: 0x000252F4
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x000252FC File Offset: 0x000252FC
		internal Clients ParentClient { get; set; }

		// Token: 0x060003B7 RID: 951 RVA: 0x00025308 File Offset: 0x00025308
		public FormProcessManager()
		{
			this.InitializeComponent();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00025318 File Offset: 0x00025318
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

		// Token: 0x060003B9 RID: 953 RVA: 0x00025378 File Offset: 0x00025378
		private void killToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormProcessManager.<killToolStripMenuItem_Click>d__14 <killToolStripMenuItem_Click>d__;
			<killToolStripMenuItem_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<killToolStripMenuItem_Click>d__.<>4__this = this;
			<killToolStripMenuItem_Click>d__.<>1__state = -1;
			<killToolStripMenuItem_Click>d__.<>t__builder.Start<FormProcessManager.<killToolStripMenuItem_Click>d__14>(ref <killToolStripMenuItem_Click>d__);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000253B4 File Offset: 0x000253B4
		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThreadPool.QueueUserWorkItem(delegate(object o)
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "processManager";
				msgPack.ForcePathObject("Option").AsString = "List";
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.Client.Send), msgPack.Encode2Bytes());
			});
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000253C8 File Offset: 0x000253C8
		private void FormProcessManager_FormClosed(object sender, FormClosedEventArgs e)
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
