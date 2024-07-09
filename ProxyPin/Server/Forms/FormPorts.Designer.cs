namespace Server.Forms
{
	// Token: 0x0200005F RID: 95
	public partial class FormPorts : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00024CD0 File Offset: 0x00024CD0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00024CF8 File Offset: 0x00024CF8
		private void InitializeComponent()
		{
			this.button1 = new global::DarkUI.Controls.DarkButton();
			this.textPorts = new global::DarkUI.Controls.DarkTextBox();
			this.label1 = new global::DarkUI.Controls.DarkLabel();
			this.groupBox1 = new global::DarkUI.Controls.DarkGroupBox();
			this.listBox1 = new global::System.Windows.Forms.ListBox();
			this.btnDelete = new global::DarkUI.Controls.DarkButton();
			this.btnAdd = new global::DarkUI.Controls.DarkButton();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.button1.Location = new global::System.Drawing.Point(0, 129);
			this.button1.Margin = new global::System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Padding = new global::System.Windows.Forms.Padding(5);
			this.button1.Size = new global::System.Drawing.Size(197, 32);
			this.button1.TabIndex = 0;
			this.button1.Text = "Start";
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			this.textPorts.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.textPorts.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.textPorts.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.textPorts.Location = new global::System.Drawing.Point(55, 17);
			this.textPorts.Margin = new global::System.Windows.Forms.Padding(2);
			this.textPorts.Name = "textPorts";
			this.textPorts.Size = new global::System.Drawing.Size(142, 20);
			this.textPorts.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label1.Location = new global::System.Drawing.Point(4, 20);
			this.label1.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(26, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Port";
			this.groupBox1.BorderColor = global::System.Drawing.Color.FromArgb(51, 51, 51);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.listBox1);
			this.groupBox1.Controls.Add(this.btnDelete);
			this.groupBox1.Controls.Add(this.btnAdd);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textPorts);
			this.groupBox1.Location = new global::System.Drawing.Point(9, 9);
			this.groupBox1.Margin = new global::System.Windows.Forms.Padding(2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new global::System.Windows.Forms.Padding(2);
			this.groupBox1.Size = new global::System.Drawing.Size(210, 165);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Setting";
			this.listBox1.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.listBox1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.listBox1.ForeColor = global::System.Drawing.Color.Gainsboro;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new global::System.Drawing.Point(107, 41);
			this.listBox1.Margin = new global::System.Windows.Forms.Padding(2);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new global::System.Drawing.Size(90, 67);
			this.listBox1.TabIndex = 4;
			this.btnDelete.Location = new global::System.Drawing.Point(55, 78);
			this.btnDelete.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnDelete.Size = new global::System.Drawing.Size(38, 29);
			this.btnDelete.TabIndex = 3;
			this.btnDelete.Text = "-";
			this.btnDelete.Click += new global::System.EventHandler(this.BtnDelete_Click);
			this.btnAdd.Location = new global::System.Drawing.Point(7, 78);
			this.btnAdd.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnAdd.Size = new global::System.Drawing.Size(38, 29);
			this.btnAdd.TabIndex = 2;
			this.btnAdd.Text = "+";
			this.btnAdd.Click += new global::System.EventHandler(this.BtnAdd_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(227, 181);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Margin = new global::System.Windows.Forms.Padding(2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormPorts";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Listen ports";
			base.TopMost = true;
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.PortsFrm_FormClosed);
			base.Load += new global::System.EventHandler(this.PortsFrm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000277 RID: 631
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000278 RID: 632
		private global::System.Windows.Forms.ListBox listBox1;

		// Token: 0x04000279 RID: 633
		private global::DarkUI.Controls.DarkButton button1;

		// Token: 0x0400027A RID: 634
		public global::DarkUI.Controls.DarkTextBox textPorts;

		// Token: 0x0400027B RID: 635
		private global::DarkUI.Controls.DarkLabel label1;

		// Token: 0x0400027C RID: 636
		private global::DarkUI.Controls.DarkGroupBox groupBox1;

		// Token: 0x0400027D RID: 637
		private global::DarkUI.Controls.DarkButton btnDelete;

		// Token: 0x0400027E RID: 638
		private global::DarkUI.Controls.DarkButton btnAdd;
	}
}
