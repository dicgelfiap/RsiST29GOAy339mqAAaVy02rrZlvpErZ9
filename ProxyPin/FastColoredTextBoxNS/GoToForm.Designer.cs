namespace FastColoredTextBoxNS
{
	// Token: 0x02000A05 RID: 2565
	public partial class GoToForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060062C1 RID: 25281 RVA: 0x001D7D7C File Offset: 0x001D7D7C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x001D7DC0 File Offset: 0x001D7DC0
		private void InitializeComponent()
		{
			this.label = new global::System.Windows.Forms.Label();
			this.tbLineNumber = new global::System.Windows.Forms.TextBox();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.label.AutoSize = true;
			this.label.Location = new global::System.Drawing.Point(12, 9);
			this.label.Name = "label";
			this.label.Size = new global::System.Drawing.Size(96, 13);
			this.label.TabIndex = 0;
			this.label.Text = "Line Number (1/1):";
			this.tbLineNumber.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.tbLineNumber.Location = new global::System.Drawing.Point(12, 29);
			this.tbLineNumber.Name = "tbLineNumber";
			this.tbLineNumber.Size = new global::System.Drawing.Size(296, 20);
			this.tbLineNumber.TabIndex = 1;
			this.btnOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new global::System.Drawing.Point(152, 71);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new global::System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new global::System.EventHandler(this.btnOk_Click);
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(233, 71);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new global::System.EventHandler(this.btnCancel_Click);
			base.AcceptButton = this.btnOk;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new global::System.Drawing.Size(320, 106);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.tbLineNumber);
			base.Controls.Add(this.label);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "GoToForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Go To Line";
			base.TopMost = true;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04003251 RID: 12881
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04003252 RID: 12882
		private global::System.Windows.Forms.Label label;

		// Token: 0x04003253 RID: 12883
		private global::System.Windows.Forms.TextBox tbLineNumber;

		// Token: 0x04003254 RID: 12884
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x04003255 RID: 12885
		private global::System.Windows.Forms.Button btnCancel;
	}
}
