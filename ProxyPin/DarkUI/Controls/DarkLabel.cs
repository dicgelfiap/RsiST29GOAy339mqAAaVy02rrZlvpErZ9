using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000AC RID: 172
	public class DarkLabel : Label
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0003DCB4 File Offset: 0x0003DCB4
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0003DCBC File Offset: 0x0003DCBC
		[Category("Layout")]
		[Description("Enables automatic height sizing based on the contents of the label.")]
		[DefaultValue(false)]
		public bool AutoUpdateHeight
		{
			get
			{
				return this._autoUpdateHeight;
			}
			set
			{
				this._autoUpdateHeight = value;
				if (this._autoUpdateHeight)
				{
					this.AutoSize = false;
					this.ResizeLabel();
				}
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0003DCE0 File Offset: 0x0003DCE0
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0003DCE8 File Offset: 0x0003DCE8
		public new bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
				if (this.AutoSize)
				{
					this.AutoUpdateHeight = false;
				}
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0003DD04 File Offset: 0x0003DD04
		public DarkLabel()
		{
			this.ForeColor = Colors.LightText;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0003DD18 File Offset: 0x0003DD18
		private void ResizeLabel()
		{
			if (!this._autoUpdateHeight || this._isGrowing)
			{
				return;
			}
			try
			{
				this._isGrowing = true;
				Size proposedSize = new Size(base.Width, int.MaxValue);
				proposedSize = TextRenderer.MeasureText(this.Text, this.Font, proposedSize, TextFormatFlags.WordBreak);
				base.Height = proposedSize.Height + base.Padding.Vertical;
			}
			finally
			{
				this._isGrowing = false;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0003DDA4 File Offset: 0x0003DDA4
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.ResizeLabel();
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0003DDB4 File Offset: 0x0003DDB4
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.ResizeLabel();
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0003DDC4 File Offset: 0x0003DDC4
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.ResizeLabel();
		}

		// Token: 0x0400051F RID: 1311
		private bool _autoUpdateHeight;

		// Token: 0x04000520 RID: 1312
		private bool _isGrowing;
	}
}
