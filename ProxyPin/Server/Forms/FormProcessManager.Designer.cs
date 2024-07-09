namespace Server.Forms
{
	// Token: 0x02000060 RID: 96
	public partial class FormProcessManager : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x060003BC RID: 956 RVA: 0x00025404 File Offset: 0x00025404
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0002542C File Offset: 0x0002542C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.lv_name = new global::System.Windows.Forms.ColumnHeader();
			this.lv_id = new global::System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.killToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.timer1 = new global::System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.listView1.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.listView1.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.listView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.lv_name,
				this.lv_id
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
			this.listView1.Size = new global::System.Drawing.Size(364, 414);
			this.listView1.SmallImageList = this.imageList1;
			this.listView1.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.Details;
			this.lv_name.Text = "Name";
			this.lv_name.Width = 283;
			this.lv_id.Text = "ID";
			this.lv_id.Width = 81;
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
			this.imageList1.ColorDepth = global::System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = new global::System.Drawing.Size(32, 32);
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new global::System.EventHandler(this.timer1_Tick);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(364, 414);
			base.Controls.Add(this.listView1);
			base.Margin = new global::System.Windows.Forms.Padding(2);
			base.Name = "FormProcessManager";
			base.ShowIcon = false;
			this.Text = "Process Manager";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.FormProcessManager_FormClosed);
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000282 RID: 642
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000283 RID: 643
		private global::System.Windows.Forms.ColumnHeader lv_name;

		// Token: 0x04000284 RID: 644
		private global::System.Windows.Forms.ColumnHeader lv_id;

		// Token: 0x04000285 RID: 645
		public global::System.Windows.Forms.ListView listView1;

		// Token: 0x04000286 RID: 646
		public global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x04000287 RID: 647
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000288 RID: 648
		private global::System.Windows.Forms.ToolStripMenuItem killToolStripMenuItem;

		// Token: 0x04000289 RID: 649
		private global::System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;

		// Token: 0x0400028A RID: 650
		public global::System.Windows.Forms.Timer timer1;
	}
}
