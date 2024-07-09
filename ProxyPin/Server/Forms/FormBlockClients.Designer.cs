namespace Server.Forms
{
	// Token: 0x02000058 RID: 88
	public partial class FormBlockClients : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0001C16C File Offset: 0x0001C16C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001C194 File Offset: 0x0001C194
		private void InitializeComponent()
		{
			this.groupBox1 = new global::DarkUI.Controls.DarkGroupBox();
			this.listBlocked = new global::System.Windows.Forms.ListBox();
			this.btnDelete = new global::DarkUI.Controls.DarkButton();
			this.btnAdd = new global::DarkUI.Controls.DarkButton();
			this.label1 = new global::DarkUI.Controls.DarkLabel();
			this.txtBlock = new global::DarkUI.Controls.DarkTextBox();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.BorderColor = global::System.Drawing.Color.FromArgb(51, 51, 51);
			this.groupBox1.Controls.Add(this.listBlocked);
			this.groupBox1.Controls.Add(this.btnDelete);
			this.groupBox1.Controls.Add(this.btnAdd);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtBlock);
			this.groupBox1.Location = new global::System.Drawing.Point(8, 8);
			this.groupBox1.Margin = new global::System.Windows.Forms.Padding(2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new global::System.Windows.Forms.Padding(2);
			this.groupBox1.Size = new global::System.Drawing.Size(543, 231);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Blocklist";
			this.listBlocked.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.listBlocked.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.listBlocked.DataBindings.Add(new global::System.Windows.Forms.Binding("Name", global::Server.Properties.Settings.Default, "txtBlocked", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.listBlocked.ForeColor = global::System.Drawing.Color.Gainsboro;
			this.listBlocked.FormattingEnabled = true;
			this.listBlocked.Location = new global::System.Drawing.Point(225, 30);
			this.listBlocked.Margin = new global::System.Windows.Forms.Padding(2);
			this.listBlocked.Name = global::Server.Properties.Settings.Default.txtBlocked;
			this.listBlocked.SelectionMode = global::System.Windows.Forms.SelectionMode.MultiSimple;
			this.listBlocked.Size = new global::System.Drawing.Size(297, 171);
			this.listBlocked.TabIndex = 4;
			this.btnDelete.Location = new global::System.Drawing.Point(117, 107);
			this.btnDelete.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnDelete.Size = new global::System.Drawing.Size(38, 23);
			this.btnDelete.TabIndex = 3;
			this.btnDelete.Text = "-";
			this.btnDelete.Click += new global::System.EventHandler(this.BtnDelete_Click);
			this.btnAdd.Location = new global::System.Drawing.Point(24, 107);
			this.btnAdd.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnAdd.Size = new global::System.Drawing.Size(38, 23);
			this.btnAdd.TabIndex = 2;
			this.btnAdd.Text = "+";
			this.btnAdd.Click += new global::System.EventHandler(this.BtnAdd_Click);
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label1.Location = new global::System.Drawing.Point(4, 34);
			this.label1.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(89, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Input HWID or IP";
			this.txtBlock.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtBlock.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtBlock.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtBlock.Location = new global::System.Drawing.Point(7, 56);
			this.txtBlock.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtBlock.Name = "txtBlock";
			this.txtBlock.Size = new global::System.Drawing.Size(192, 20);
			this.txtBlock.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(562, 246);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Margin = new global::System.Windows.Forms.Padding(2);
			base.Name = "FormBlockClients";
			base.ShowIcon = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Block";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.FormBlockClients_FormClosed);
			base.Load += new global::System.EventHandler(this.FormBlockClients_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040001E4 RID: 484
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040001E5 RID: 485
		private global::System.Windows.Forms.ListBox listBlocked;

		// Token: 0x040001E6 RID: 486
		private global::DarkUI.Controls.DarkGroupBox groupBox1;

		// Token: 0x040001E7 RID: 487
		private global::DarkUI.Controls.DarkButton btnDelete;

		// Token: 0x040001E8 RID: 488
		private global::DarkUI.Controls.DarkButton btnAdd;

		// Token: 0x040001E9 RID: 489
		private global::DarkUI.Controls.DarkLabel label1;

		// Token: 0x040001EA RID: 490
		public global::DarkUI.Controls.DarkTextBox txtBlock;
	}
}
