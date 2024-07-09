namespace Server.Forms
{
	// Token: 0x02000051 RID: 81
	public partial class FormNetstat : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x06000325 RID: 805 RVA: 0x00019B2C File Offset: 0x00019B2C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00019B54 File Offset: 0x00019B54
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.lv_id = new global::System.Windows.Forms.ColumnHeader();
			this.lv_localAddr = new global::System.Windows.Forms.ColumnHeader();
			this.lv_remoteAddr = new global::System.Windows.Forms.ColumnHeader();
			this.lv_state = new global::System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.killToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.timer1 = new global::System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.listView1.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.listView1.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.listView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.lv_id,
				this.lv_localAddr,
				this.lv_remoteAddr,
				this.lv_state
			});
			this.listView1.ContextMenuStrip = this.contextMenuStrip1;
			this.listView1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listView1.Enabled = false;
			this.listView1.ForeColor = global::System.Drawing.Color.Gainsboro;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.HideSelection = false;
			this.listView1.Location = new global::System.Drawing.Point(0, 0);
			this.listView1.Margin = new global::System.Windows.Forms.Padding(2);
			this.listView1.Name = "listView1";
			this.listView1.ShowGroups = false;
			this.listView1.ShowItemToolTips = true;
			this.listView1.Size = new global::System.Drawing.Size(545, 375);
			this.listView1.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.Details;
			this.lv_id.Text = "ID";
			this.lv_id.Width = 92;
			this.lv_localAddr.Text = "LocalAddress";
			this.lv_localAddr.Width = 161;
			this.lv_remoteAddr.Text = "RemoteAddress";
			this.lv_remoteAddr.Width = 177;
			this.lv_state.Text = "State";
			this.lv_state.Width = 110;
			this.contextMenuStrip1.ImageScalingSize = new global::System.Drawing.Size(24, 24);
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.killToolStripMenuItem,
				this.refreshToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new global::System.Drawing.Size(114, 48);
			this.killToolStripMenuItem.Name = "killToolStripMenuItem";
			this.killToolStripMenuItem.Size = new global::System.Drawing.Size(113, 22);
			this.killToolStripMenuItem.Text = "Kill";
			this.killToolStripMenuItem.Click += new global::System.EventHandler(this.killToolStripMenuItem_Click);
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new global::System.Drawing.Size(113, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new global::System.EventHandler(this.refreshToolStripMenuItem_Click);
			this.timer1.Interval = 1000;
			this.timer1.Tick += new global::System.EventHandler(this.timer1_Tick);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(545, 375);
			base.Controls.Add(this.listView1);
			base.Margin = new global::System.Windows.Forms.Padding(2);
			base.Name = "FormNetstat";
			base.ShowIcon = false;
			this.Text = "Netstat";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.FormNetstat_FormClosed);
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x040001A8 RID: 424
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040001A9 RID: 425
		private global::System.Windows.Forms.ColumnHeader lv_id;

		// Token: 0x040001AA RID: 426
		public global::System.Windows.Forms.ListView listView1;

		// Token: 0x040001AB RID: 427
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x040001AC RID: 428
		private global::System.Windows.Forms.ToolStripMenuItem killToolStripMenuItem;

		// Token: 0x040001AD RID: 429
		private global::System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;

		// Token: 0x040001AE RID: 430
		public global::System.Windows.Forms.Timer timer1;

		// Token: 0x040001AF RID: 431
		private global::System.Windows.Forms.ColumnHeader lv_localAddr;

		// Token: 0x040001B0 RID: 432
		private global::System.Windows.Forms.ColumnHeader lv_remoteAddr;

		// Token: 0x040001B1 RID: 433
		private global::System.Windows.Forms.ColumnHeader lv_state;
	}
}
