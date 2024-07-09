namespace Server.Forms
{
	// Token: 0x0200005A RID: 90
	public partial class FormCertificate : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x0600036B RID: 875 RVA: 0x00020504 File Offset: 0x00020504
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0002052C File Offset: 0x0002052C
		private void InitializeComponent()
		{
			this.groupBox1 = new global::DarkUI.Controls.DarkGroupBox();
			this.button1 = new global::DarkUI.Controls.DarkButton();
			this.textBox1 = new global::DarkUI.Controls.DarkTextBox();
			this.label1 = new global::DarkUI.Controls.DarkLabel();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new global::System.Drawing.Point(8, 8);
			this.groupBox1.Margin = new global::System.Windows.Forms.Padding(2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new global::System.Windows.Forms.Padding(2);
			this.groupBox1.Size = new global::System.Drawing.Size(252, 178);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "New Certificate";
			this.button1.Location = new global::System.Drawing.Point(7, 108);
			this.button1.Margin = new global::System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(218, 25);
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.Button1_Click);
			this.textBox1.Location = new global::System.Drawing.Point(7, 70);
			this.textBox1.Margin = new global::System.Windows.Forms.Padding(2);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(218, 20);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "BoratRat Server";
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(84, 40);
			this.label1.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(64, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sever name";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(271, 197);
			base.ControlBox = false;
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Margin = new global::System.Windows.Forms.Padding(2);
			base.Name = "FormCertificate";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Certificate";
			base.Load += new global::System.EventHandler(this.FormCertificate_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000221 RID: 545
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000222 RID: 546
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x04000223 RID: 547
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000224 RID: 548
		private global::System.Windows.Forms.Button button1;

		// Token: 0x04000225 RID: 549
		private global::System.Windows.Forms.TextBox textBox1;
	}
}
