namespace FastColoredTextBoxNS
{
	// Token: 0x02000A38 RID: 2616
	public partial class FindForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600668A RID: 26250 RVA: 0x001F2864 File Offset: 0x001F2864
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600668B RID: 26251 RVA: 0x001F28A8 File Offset: 0x001F28A8
		private void InitializeComponent()
		{
			this.btClose = new global::System.Windows.Forms.Button();
			this.btFindNext = new global::System.Windows.Forms.Button();
			this.tbFind = new global::System.Windows.Forms.TextBox();
			this.cbRegex = new global::System.Windows.Forms.CheckBox();
			this.cbMatchCase = new global::System.Windows.Forms.CheckBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.cbWholeWord = new global::System.Windows.Forms.CheckBox();
			base.SuspendLayout();
			this.btClose.Location = new global::System.Drawing.Point(273, 73);
			this.btClose.Name = "btClose";
			this.btClose.Size = new global::System.Drawing.Size(75, 23);
			this.btClose.TabIndex = 5;
			this.btClose.Text = "Close";
			this.btClose.UseVisualStyleBackColor = true;
			this.btClose.Click += new global::System.EventHandler(this.btClose_Click);
			this.btFindNext.Location = new global::System.Drawing.Point(192, 73);
			this.btFindNext.Name = "btFindNext";
			this.btFindNext.Size = new global::System.Drawing.Size(75, 23);
			this.btFindNext.TabIndex = 4;
			this.btFindNext.Text = "Find next";
			this.btFindNext.UseVisualStyleBackColor = true;
			this.btFindNext.Click += new global::System.EventHandler(this.btFindNext_Click);
			this.tbFind.Location = new global::System.Drawing.Point(42, 12);
			this.tbFind.Name = "tbFind";
			this.tbFind.Size = new global::System.Drawing.Size(306, 20);
			this.tbFind.TabIndex = 0;
			this.tbFind.TextChanged += new global::System.EventHandler(this.cbMatchCase_CheckedChanged);
			this.tbFind.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.tbFind_KeyPress);
			this.cbRegex.AutoSize = true;
			this.cbRegex.Location = new global::System.Drawing.Point(249, 38);
			this.cbRegex.Name = "cbRegex";
			this.cbRegex.Size = new global::System.Drawing.Size(57, 17);
			this.cbRegex.TabIndex = 3;
			this.cbRegex.Text = "Regex";
			this.cbRegex.UseVisualStyleBackColor = true;
			this.cbRegex.CheckedChanged += new global::System.EventHandler(this.cbMatchCase_CheckedChanged);
			this.cbMatchCase.AutoSize = true;
			this.cbMatchCase.Location = new global::System.Drawing.Point(42, 38);
			this.cbMatchCase.Name = "cbMatchCase";
			this.cbMatchCase.Size = new global::System.Drawing.Size(82, 17);
			this.cbMatchCase.TabIndex = 1;
			this.cbMatchCase.Text = "Match case";
			this.cbMatchCase.UseVisualStyleBackColor = true;
			this.cbMatchCase.CheckedChanged += new global::System.EventHandler(this.cbMatchCase_CheckedChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(6, 15);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(33, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Find: ";
			this.cbWholeWord.AutoSize = true;
			this.cbWholeWord.Location = new global::System.Drawing.Point(130, 38);
			this.cbWholeWord.Name = "cbWholeWord";
			this.cbWholeWord.Size = new global::System.Drawing.Size(113, 17);
			this.cbWholeWord.TabIndex = 2;
			this.cbWholeWord.Text = "Match whole word";
			this.cbWholeWord.UseVisualStyleBackColor = true;
			this.cbWholeWord.CheckedChanged += new global::System.EventHandler(this.cbMatchCase_CheckedChanged);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(360, 108);
			base.Controls.Add(this.cbWholeWord);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cbMatchCase);
			base.Controls.Add(this.cbRegex);
			base.Controls.Add(this.tbFind);
			base.Controls.Add(this.btFindNext);
			base.Controls.Add(this.btClose);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "FindForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Find";
			base.TopMost = true;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.FindForm_FormClosing);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04003483 RID: 13443
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04003484 RID: 13444
		private global::System.Windows.Forms.Button btClose;

		// Token: 0x04003485 RID: 13445
		private global::System.Windows.Forms.Button btFindNext;

		// Token: 0x04003486 RID: 13446
		private global::System.Windows.Forms.CheckBox cbRegex;

		// Token: 0x04003487 RID: 13447
		private global::System.Windows.Forms.CheckBox cbMatchCase;

		// Token: 0x04003488 RID: 13448
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04003489 RID: 13449
		private global::System.Windows.Forms.CheckBox cbWholeWord;

		// Token: 0x0400348A RID: 13450
		public global::System.Windows.Forms.TextBox tbFind;
	}
}
