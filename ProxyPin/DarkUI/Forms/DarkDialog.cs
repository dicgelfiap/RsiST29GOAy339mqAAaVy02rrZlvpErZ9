using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;

namespace DarkUI.Forms
{
	// Token: 0x02000080 RID: 128
	public partial class DarkDialog : DarkForm
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x000317F4 File Offset: 0x000317F4
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x000317FC File Offset: 0x000317FC
		[Description("Determines the type of the dialog window.")]
		[DefaultValue(DarkDialogButton.Ok)]
		public DarkDialogButton DialogButtons
		{
			get
			{
				return this._dialogButtons;
			}
			set
			{
				if (this._dialogButtons == value)
				{
					return;
				}
				this._dialogButtons = value;
				this.SetButtons();
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00031818 File Offset: 0x00031818
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00031820 File Offset: 0x00031820
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int TotalButtonSize { get; private set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0003182C File Offset: 0x0003182C
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00031834 File Offset: 0x00031834
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new IButtonControl AcceptButton
		{
			get
			{
				return base.AcceptButton;
			}
			private set
			{
				base.AcceptButton = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00031840 File Offset: 0x00031840
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00031848 File Offset: 0x00031848
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new IButtonControl CancelButton
		{
			get
			{
				return base.CancelButton;
			}
			private set
			{
				base.CancelButton = value;
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00031854 File Offset: 0x00031854
		public DarkDialog()
		{
			this.InitializeComponent();
			this._buttons = new List<DarkButton>
			{
				this.btnAbort,
				this.btnRetry,
				this.btnIgnore,
				this.btnOk,
				this.btnCancel,
				this.btnClose,
				this.btnYes,
				this.btnNo
			};
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000318DC File Offset: 0x000318DC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.SetButtons();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000318EC File Offset: 0x000318EC
		private void SetButtons()
		{
			foreach (DarkButton darkButton in this._buttons)
			{
				darkButton.Visible = false;
			}
			switch (this._dialogButtons)
			{
			case DarkDialogButton.Ok:
				this.ShowButton(this.btnOk, true);
				this.AcceptButton = this.btnOk;
				break;
			case DarkDialogButton.Close:
				this.ShowButton(this.btnClose, true);
				this.AcceptButton = this.btnClose;
				this.CancelButton = this.btnClose;
				break;
			case DarkDialogButton.OkCancel:
				this.ShowButton(this.btnOk, false);
				this.ShowButton(this.btnCancel, true);
				this.AcceptButton = this.btnOk;
				this.CancelButton = this.btnCancel;
				break;
			case DarkDialogButton.YesNo:
				this.ShowButton(this.btnYes, false);
				this.ShowButton(this.btnNo, true);
				this.AcceptButton = this.btnYes;
				this.CancelButton = this.btnNo;
				break;
			case DarkDialogButton.YesNoCancel:
				this.ShowButton(this.btnYes, false);
				this.ShowButton(this.btnNo, false);
				this.ShowButton(this.btnCancel, true);
				this.AcceptButton = this.btnYes;
				this.CancelButton = this.btnCancel;
				break;
			case DarkDialogButton.AbortRetryIgnore:
				this.ShowButton(this.btnAbort, false);
				this.ShowButton(this.btnRetry, false);
				this.ShowButton(this.btnIgnore, true);
				this.AcceptButton = this.btnAbort;
				this.CancelButton = this.btnIgnore;
				break;
			case DarkDialogButton.RetryCancel:
				this.ShowButton(this.btnRetry, false);
				this.ShowButton(this.btnCancel, true);
				this.AcceptButton = this.btnRetry;
				this.CancelButton = this.btnCancel;
				break;
			}
			this.SetFlowSize();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00031AE8 File Offset: 0x00031AE8
		private void ShowButton(DarkButton button, bool isLast = false)
		{
			button.SendToBack();
			if (!isLast)
			{
				button.Margin = new Padding(0, 0, 10, 0);
			}
			button.Visible = true;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00031B10 File Offset: 0x00031B10
		private void SetFlowSize()
		{
			int num = this.flowInner.Padding.Horizontal;
			foreach (DarkButton darkButton in this._buttons)
			{
				if (darkButton.Visible)
				{
					num += darkButton.Width + darkButton.Margin.Right;
				}
			}
			this.flowInner.Width = num;
			this.TotalButtonSize = num;
		}

		// Token: 0x04000437 RID: 1079
		private DarkDialogButton _dialogButtons;

		// Token: 0x04000438 RID: 1080
		private List<DarkButton> _buttons;
	}
}
