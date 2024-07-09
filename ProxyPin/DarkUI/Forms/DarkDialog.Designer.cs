namespace DarkUI.Forms
{
	// Token: 0x02000080 RID: 128
	public partial class DarkDialog : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x00031BAC File Offset: 0x00031BAC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00031BD4 File Offset: 0x00031BD4
		private void InitializeComponent()
		{
			this.pnlFooter = new global::System.Windows.Forms.Panel();
			this.flowInner = new global::System.Windows.Forms.FlowLayoutPanel();
			this.btnOk = new global::DarkUI.Controls.DarkButton();
			this.btnCancel = new global::DarkUI.Controls.DarkButton();
			this.btnClose = new global::DarkUI.Controls.DarkButton();
			this.btnYes = new global::DarkUI.Controls.DarkButton();
			this.btnNo = new global::DarkUI.Controls.DarkButton();
			this.btnAbort = new global::DarkUI.Controls.DarkButton();
			this.btnRetry = new global::DarkUI.Controls.DarkButton();
			this.btnIgnore = new global::DarkUI.Controls.DarkButton();
			this.pnlFooter.SuspendLayout();
			this.flowInner.SuspendLayout();
			base.SuspendLayout();
			this.pnlFooter.Controls.Add(this.flowInner);
			this.pnlFooter.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.pnlFooter.Location = new global::System.Drawing.Point(0, 357);
			this.pnlFooter.Name = "pnlFooter";
			this.pnlFooter.Size = new global::System.Drawing.Size(767, 45);
			this.pnlFooter.TabIndex = 1;
			this.flowInner.Controls.Add(this.btnOk);
			this.flowInner.Controls.Add(this.btnCancel);
			this.flowInner.Controls.Add(this.btnClose);
			this.flowInner.Controls.Add(this.btnYes);
			this.flowInner.Controls.Add(this.btnNo);
			this.flowInner.Controls.Add(this.btnAbort);
			this.flowInner.Controls.Add(this.btnRetry);
			this.flowInner.Controls.Add(this.btnIgnore);
			this.flowInner.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.flowInner.Location = new global::System.Drawing.Point(104, 0);
			this.flowInner.Name = "flowInner";
			this.flowInner.Padding = new global::System.Windows.Forms.Padding(10);
			this.flowInner.Size = new global::System.Drawing.Size(663, 45);
			this.flowInner.TabIndex = 10014;
			this.btnOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new global::System.Drawing.Point(10, 10);
			this.btnOk.Margin = new global::System.Windows.Forms.Padding(0);
			this.btnOk.Name = "btnOk";
			this.btnOk.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnOk.Size = new global::System.Drawing.Size(75, 26);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "Ok";
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(85, 10);
			this.btnCancel.Margin = new global::System.Windows.Forms.Padding(0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnCancel.Size = new global::System.Drawing.Size(75, 26);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnClose.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new global::System.Drawing.Point(160, 10);
			this.btnClose.Margin = new global::System.Windows.Forms.Padding(0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnClose.Size = new global::System.Drawing.Size(75, 26);
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "Close";
			this.btnYes.DialogResult = global::System.Windows.Forms.DialogResult.Yes;
			this.btnYes.Location = new global::System.Drawing.Point(235, 10);
			this.btnYes.Margin = new global::System.Windows.Forms.Padding(0);
			this.btnYes.Name = "btnYes";
			this.btnYes.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnYes.Size = new global::System.Drawing.Size(75, 26);
			this.btnYes.TabIndex = 6;
			this.btnYes.Text = "Yes";
			this.btnNo.DialogResult = global::System.Windows.Forms.DialogResult.No;
			this.btnNo.Location = new global::System.Drawing.Point(310, 10);
			this.btnNo.Margin = new global::System.Windows.Forms.Padding(0);
			this.btnNo.Name = "btnNo";
			this.btnNo.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnNo.Size = new global::System.Drawing.Size(75, 26);
			this.btnNo.TabIndex = 7;
			this.btnNo.Text = "No";
			this.btnAbort.DialogResult = global::System.Windows.Forms.DialogResult.Abort;
			this.btnAbort.Location = new global::System.Drawing.Point(385, 10);
			this.btnAbort.Margin = new global::System.Windows.Forms.Padding(0);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnAbort.Size = new global::System.Drawing.Size(75, 26);
			this.btnAbort.TabIndex = 8;
			this.btnAbort.Text = "Abort";
			this.btnRetry.DialogResult = global::System.Windows.Forms.DialogResult.Retry;
			this.btnRetry.Location = new global::System.Drawing.Point(460, 10);
			this.btnRetry.Margin = new global::System.Windows.Forms.Padding(0);
			this.btnRetry.Name = "btnRetry";
			this.btnRetry.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnRetry.Size = new global::System.Drawing.Size(75, 26);
			this.btnRetry.TabIndex = 9;
			this.btnRetry.Text = "Retry";
			this.btnIgnore.DialogResult = global::System.Windows.Forms.DialogResult.Ignore;
			this.btnIgnore.Location = new global::System.Drawing.Point(535, 10);
			this.btnIgnore.Margin = new global::System.Windows.Forms.Padding(0);
			this.btnIgnore.Name = "btnIgnore";
			this.btnIgnore.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnIgnore.Size = new global::System.Drawing.Size(75, 26);
			this.btnIgnore.TabIndex = 10;
			this.btnIgnore.Text = "Ignore";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(767, 402);
			base.Controls.Add(this.pnlFooter);
			base.Name = "DarkDialog";
			this.Text = "DarkDialog";
			this.pnlFooter.ResumeLayout(false);
			this.flowInner.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000439 RID: 1081
		protected global::DarkUI.Controls.DarkButton btnOk;

		// Token: 0x0400043A RID: 1082
		protected global::DarkUI.Controls.DarkButton btnCancel;

		// Token: 0x0400043B RID: 1083
		protected global::DarkUI.Controls.DarkButton btnClose;

		// Token: 0x0400043C RID: 1084
		protected global::DarkUI.Controls.DarkButton btnYes;

		// Token: 0x0400043D RID: 1085
		protected global::DarkUI.Controls.DarkButton btnNo;

		// Token: 0x0400043E RID: 1086
		protected global::DarkUI.Controls.DarkButton btnAbort;

		// Token: 0x0400043F RID: 1087
		protected global::DarkUI.Controls.DarkButton btnRetry;

		// Token: 0x04000440 RID: 1088
		protected global::DarkUI.Controls.DarkButton btnIgnore;

		// Token: 0x04000442 RID: 1090
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000443 RID: 1091
		private global::System.Windows.Forms.Panel pnlFooter;

		// Token: 0x04000444 RID: 1092
		private global::System.Windows.Forms.FlowLayoutPanel flowInner;
	}
}
