namespace FastColoredTextBoxNS
{
	// Token: 0x02000A17 RID: 2583
	public partial class ReplaceForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060063B8 RID: 25528 RVA: 0x001DE718 File Offset: 0x001DE718
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060063B9 RID: 25529 RVA: 0x001DE75C File Offset: 0x001DE75C
		private void InitializeComponent()
		{
			this.btClose = new global::System.Windows.Forms.Button();
			this.btFindNext = new global::System.Windows.Forms.Button();
			this.tbFind = new global::System.Windows.Forms.TextBox();
			this.cbRegex = new global::System.Windows.Forms.CheckBox();
			this.cbMatchCase = new global::System.Windows.Forms.CheckBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.cbWholeWord = new global::System.Windows.Forms.CheckBox();
			this.btReplace = new global::System.Windows.Forms.Button();
			this.btReplaceAll = new global::System.Windows.Forms.Button();
			this.label2 = new global::System.Windows.Forms.Label();
			this.tbReplace = new global::System.Windows.Forms.TextBox();
			base.SuspendLayout();
			this.btClose.Location = new global::System.Drawing.Point(273, 153);
			this.btClose.Name = "btClose";
			this.btClose.Size = new global::System.Drawing.Size(75, 23);
			this.btClose.TabIndex = 8;
			this.btClose.Text = "Close";
			this.btClose.UseVisualStyleBackColor = true;
			this.btClose.Click += new global::System.EventHandler(this.btClose_Click);
			this.btFindNext.Location = new global::System.Drawing.Point(111, 124);
			this.btFindNext.Name = "btFindNext";
			this.btFindNext.Size = new global::System.Drawing.Size(75, 23);
			this.btFindNext.TabIndex = 5;
			this.btFindNext.Text = "Find next";
			this.btFindNext.UseVisualStyleBackColor = true;
			this.btFindNext.Click += new global::System.EventHandler(this.btFindNext_Click);
			this.tbFind.Location = new global::System.Drawing.Point(62, 12);
			this.tbFind.Name = "tbFind";
			this.tbFind.Size = new global::System.Drawing.Size(286, 20);
			this.tbFind.TabIndex = 0;
			this.tbFind.TextChanged += new global::System.EventHandler(this.cbMatchCase_CheckedChanged);
			this.tbFind.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.tbFind_KeyPress);
			this.cbRegex.AutoSize = true;
			this.cbRegex.Location = new global::System.Drawing.Point(273, 38);
			this.cbRegex.Name = "cbRegex";
			this.cbRegex.Size = new global::System.Drawing.Size(57, 17);
			this.cbRegex.TabIndex = 3;
			this.cbRegex.Text = "Regex";
			this.cbRegex.UseVisualStyleBackColor = true;
			this.cbRegex.CheckedChanged += new global::System.EventHandler(this.cbMatchCase_CheckedChanged);
			this.cbMatchCase.AutoSize = true;
			this.cbMatchCase.Location = new global::System.Drawing.Point(66, 38);
			this.cbMatchCase.Name = "cbMatchCase";
			this.cbMatchCase.Size = new global::System.Drawing.Size(82, 17);
			this.cbMatchCase.TabIndex = 1;
			this.cbMatchCase.Text = "Match case";
			this.cbMatchCase.UseVisualStyleBackColor = true;
			this.cbMatchCase.CheckedChanged += new global::System.EventHandler(this.cbMatchCase_CheckedChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(23, 14);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(33, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Find: ";
			this.cbWholeWord.AutoSize = true;
			this.cbWholeWord.Location = new global::System.Drawing.Point(154, 38);
			this.cbWholeWord.Name = "cbWholeWord";
			this.cbWholeWord.Size = new global::System.Drawing.Size(113, 17);
			this.cbWholeWord.TabIndex = 2;
			this.cbWholeWord.Text = "Match whole word";
			this.cbWholeWord.UseVisualStyleBackColor = true;
			this.cbWholeWord.CheckedChanged += new global::System.EventHandler(this.cbMatchCase_CheckedChanged);
			this.btReplace.Location = new global::System.Drawing.Point(192, 124);
			this.btReplace.Name = "btReplace";
			this.btReplace.Size = new global::System.Drawing.Size(75, 23);
			this.btReplace.TabIndex = 6;
			this.btReplace.Text = "Replace";
			this.btReplace.UseVisualStyleBackColor = true;
			this.btReplace.Click += new global::System.EventHandler(this.btReplace_Click);
			this.btReplaceAll.Location = new global::System.Drawing.Point(273, 124);
			this.btReplaceAll.Name = "btReplaceAll";
			this.btReplaceAll.Size = new global::System.Drawing.Size(75, 23);
			this.btReplaceAll.TabIndex = 7;
			this.btReplaceAll.Text = "Replace all";
			this.btReplaceAll.UseVisualStyleBackColor = true;
			this.btReplaceAll.Click += new global::System.EventHandler(this.btReplaceAll_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(6, 81);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(50, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Replace:";
			this.tbReplace.Location = new global::System.Drawing.Point(62, 78);
			this.tbReplace.Name = "tbReplace";
			this.tbReplace.Size = new global::System.Drawing.Size(286, 20);
			this.tbReplace.TabIndex = 0;
			this.tbReplace.TextChanged += new global::System.EventHandler(this.cbMatchCase_CheckedChanged);
			this.tbReplace.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.tbFind_KeyPress);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(360, 191);
			base.Controls.Add(this.tbFind);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.tbReplace);
			base.Controls.Add(this.btReplaceAll);
			base.Controls.Add(this.btReplace);
			base.Controls.Add(this.cbWholeWord);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cbMatchCase);
			base.Controls.Add(this.cbRegex);
			base.Controls.Add(this.btFindNext);
			base.Controls.Add(this.btClose);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "ReplaceForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Find and replace";
			base.TopMost = true;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.ReplaceForm_FormClosing);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400335E RID: 13150
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400335F RID: 13151
		private global::System.Windows.Forms.Button btClose;

		// Token: 0x04003360 RID: 13152
		private global::System.Windows.Forms.Button btFindNext;

		// Token: 0x04003361 RID: 13153
		private global::System.Windows.Forms.CheckBox cbRegex;

		// Token: 0x04003362 RID: 13154
		private global::System.Windows.Forms.CheckBox cbMatchCase;

		// Token: 0x04003363 RID: 13155
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04003364 RID: 13156
		private global::System.Windows.Forms.CheckBox cbWholeWord;

		// Token: 0x04003365 RID: 13157
		private global::System.Windows.Forms.Button btReplace;

		// Token: 0x04003366 RID: 13158
		private global::System.Windows.Forms.Button btReplaceAll;

		// Token: 0x04003367 RID: 13159
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04003368 RID: 13160
		public global::System.Windows.Forms.TextBox tbFind;

		// Token: 0x04003369 RID: 13161
		public global::System.Windows.Forms.TextBox tbReplace;
	}
}
