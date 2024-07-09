using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Icons;

namespace DarkUI.Forms
{
	// Token: 0x02000083 RID: 131
	public partial class DarkMessageBox : DarkDialog
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00032354 File Offset: 0x00032354
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x0003235C File Offset: 0x0003235C
		[Description("Determines the maximum width of the message box when it autosizes around the displayed message.")]
		[DefaultValue(350)]
		public int MaximumWidth
		{
			get
			{
				return this._maximumWidth;
			}
			set
			{
				this._maximumWidth = value;
				this.CalculateSize();
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0003236C File Offset: 0x0003236C
		public DarkMessageBox()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00032388 File Offset: 0x00032388
		public DarkMessageBox(string message, string title, DarkMessageBoxIcon icon, DarkDialogButton buttons) : this()
		{
			this.Text = title;
			this._message = message;
			base.DialogButtons = buttons;
			this.SetIcon(icon);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000323BC File Offset: 0x000323BC
		public DarkMessageBox(string message) : this(message, null, DarkMessageBoxIcon.None, DarkDialogButton.Ok)
		{
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000323C8 File Offset: 0x000323C8
		public DarkMessageBox(string message, string title) : this(message, title, DarkMessageBoxIcon.None, DarkDialogButton.Ok)
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000323D4 File Offset: 0x000323D4
		public DarkMessageBox(string message, string title, DarkDialogButton buttons) : this(message, title, DarkMessageBoxIcon.None, buttons)
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000323E0 File Offset: 0x000323E0
		public DarkMessageBox(string message, string title, DarkMessageBoxIcon icon) : this(message, title, icon, DarkDialogButton.Ok)
		{
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000323EC File Offset: 0x000323EC
		public static DialogResult ShowInformation(string message, string caption, DarkDialogButton buttons = DarkDialogButton.Ok)
		{
			return DarkMessageBox.ShowDialog(message, caption, DarkMessageBoxIcon.Information, buttons);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000323F8 File Offset: 0x000323F8
		public static DialogResult ShowWarning(string message, string caption, DarkDialogButton buttons = DarkDialogButton.Ok)
		{
			return DarkMessageBox.ShowDialog(message, caption, DarkMessageBoxIcon.Warning, buttons);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00032404 File Offset: 0x00032404
		public static DialogResult ShowError(string message, string caption, DarkDialogButton buttons = DarkDialogButton.Ok)
		{
			return DarkMessageBox.ShowDialog(message, caption, DarkMessageBoxIcon.Error, buttons);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00032410 File Offset: 0x00032410
		private static DialogResult ShowDialog(string message, string caption, DarkMessageBoxIcon icon, DarkDialogButton buttons)
		{
			DialogResult result;
			using (DarkMessageBox darkMessageBox = new DarkMessageBox(message, caption, icon, buttons))
			{
				result = darkMessageBox.ShowDialog();
			}
			return result;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00032454 File Offset: 0x00032454
		private void SetIcon(DarkMessageBoxIcon icon)
		{
			switch (icon)
			{
			case DarkMessageBoxIcon.None:
				this.picIcon.Visible = false;
				this.lblText.Left = 10;
				return;
			case DarkMessageBoxIcon.Information:
				this.picIcon.Image = MessageBoxIcons.info;
				return;
			case DarkMessageBoxIcon.Warning:
				this.picIcon.Image = MessageBoxIcons.warning;
				return;
			case DarkMessageBoxIcon.Error:
				this.picIcon.Image = MessageBoxIcons.error;
				return;
			default:
				return;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000324C8 File Offset: 0x000324C8
		private void CalculateSize()
		{
			int num = 260;
			int height = 124;
			base.Size = new Size(num, height);
			this.lblText.Text = string.Empty;
			this.lblText.AutoSize = true;
			this.lblText.Text = this._message;
			int num2 = Math.Max(num, base.TotalButtonSize + 15);
			int num3 = this.lblText.Right + 25;
			if (num3 < this._maximumWidth)
			{
				num = num3;
				this.lblText.Top = this.picIcon.Top + this.picIcon.Height / 2 - this.lblText.Height / 2;
			}
			else
			{
				num = this._maximumWidth;
				int num4 = base.Height - this.picIcon.Height;
				this.lblText.AutoUpdateHeight = true;
				this.lblText.Width = num - this.lblText.Left - 25;
				height = num4 + this.lblText.Height;
			}
			if (num < num2)
			{
				num = num2;
			}
			base.Size = new Size(num, height);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000325E4 File Offset: 0x000325E4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.CalculateSize();
		}

		// Token: 0x0400044E RID: 1102
		private string _message;

		// Token: 0x0400044F RID: 1103
		private int _maximumWidth = 350;
	}
}
