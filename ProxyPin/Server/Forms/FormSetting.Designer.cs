namespace Server.Forms
{
	// Token: 0x02000055 RID: 85
	public partial class FormSetting : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0001B2F0 File Offset: 0x0001B2F0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001B318 File Offset: 0x0001B318
		private void InitializeComponent()
		{
			this.button1 = new global::DarkUI.Controls.DarkButton();
			this.checkBox1 = new global::DarkUI.Controls.DarkCheckBox();
			this.textBox1 = new global::DarkUI.Controls.DarkTextBox();
			this.textBox2 = new global::DarkUI.Controls.DarkTextBox();
			this.label1 = new global::DarkUI.Controls.DarkLabel();
			this.label2 = new global::DarkUI.Controls.DarkLabel();
			base.SuspendLayout();
			this.button1.Location = new global::System.Drawing.Point(12, 165);
			this.button1.Name = "button1";
			this.button1.Padding = new global::System.Windows.Forms.Padding(5);
			this.button1.Size = new global::System.Drawing.Size(321, 44);
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new global::System.Drawing.Point(12, 13);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new global::System.Drawing.Size(70, 17);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "DingDing";
			this.textBox1.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.textBox1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.textBox1.Location = new global::System.Drawing.Point(71, 51);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(262, 20);
			this.textBox1.TabIndex = 2;
			this.textBox2.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.textBox2.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox2.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.textBox2.Location = new global::System.Drawing.Point(71, 113);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new global::System.Drawing.Size(262, 20);
			this.textBox2.TabIndex = 3;
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label1.Location = new global::System.Drawing.Point(12, 60);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(57, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Webhook:";
			this.label2.AutoSize = true;
			this.label2.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label2.Location = new global::System.Drawing.Point(12, 113);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(41, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Secret:";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(345, 222);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBox2);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.button1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormSetting";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setting";
			base.Load += new global::System.EventHandler(this.FormSetting_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001CF RID: 463
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040001D0 RID: 464
		private global::DarkUI.Controls.DarkButton button1;

		// Token: 0x040001D1 RID: 465
		private global::DarkUI.Controls.DarkCheckBox checkBox1;

		// Token: 0x040001D2 RID: 466
		private global::DarkUI.Controls.DarkTextBox textBox1;

		// Token: 0x040001D3 RID: 467
		private global::DarkUI.Controls.DarkTextBox textBox2;

		// Token: 0x040001D4 RID: 468
		private global::DarkUI.Controls.DarkLabel label1;

		// Token: 0x040001D5 RID: 469
		private global::DarkUI.Controls.DarkLabel label2;
	}
}
