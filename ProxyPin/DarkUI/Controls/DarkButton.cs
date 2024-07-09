using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000AA RID: 170
	[ToolboxBitmap(typeof(Button))]
	[DefaultEvent("Click")]
	public class DarkButton : Button
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0003D50C File Offset: 0x0003D50C
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0003D514 File Offset: 0x0003D514
		public new string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0003D524 File Offset: 0x0003D524
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0003D52C File Offset: 0x0003D52C
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0003D53C File Offset: 0x0003D53C
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0003D544 File Offset: 0x0003D544
		[Category("Appearance")]
		[Description("Determines the style of the button.")]
		[DefaultValue(DarkButtonStyle.Normal)]
		public DarkButtonStyle ButtonStyle
		{
			get
			{
				return this._style;
			}
			set
			{
				this._style = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0003D554 File Offset: 0x0003D554
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x0003D55C File Offset: 0x0003D55C
		[Category("Appearance")]
		[Description("Determines the amount of padding between the image and text.")]
		[DefaultValue(5)]
		public int ImagePadding
		{
			get
			{
				return this._imagePadding;
			}
			set
			{
				this._imagePadding = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0003D56C File Offset: 0x0003D56C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool AutoEllipsis
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0003D570 File Offset: 0x0003D570
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DarkControlState ButtonState
		{
			get
			{
				return this._buttonState;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0003D578 File Offset: 0x0003D578
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment ImageAlign
		{
			get
			{
				return base.ImageAlign;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0003D580 File Offset: 0x0003D580
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool FlatAppearance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0003D584 File Offset: 0x0003D584
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new FlatStyle FlatStyle
		{
			get
			{
				return base.FlatStyle;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0003D58C File Offset: 0x0003D58C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0003D594 File Offset: 0x0003D594
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool UseCompatibleTextRendering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0003D598 File Offset: 0x0003D598
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool UseVisualStyleBackColor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0003D59C File Offset: 0x0003D59C
		public DarkButton()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			base.UseVisualStyleBackColor = false;
			base.UseCompatibleTextRendering = false;
			this.SetButtonState(DarkControlState.Normal);
			base.Padding = new Padding(this._padding);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0003D5FC File Offset: 0x0003D5FC
		private void SetButtonState(DarkControlState buttonState)
		{
			if (this._buttonState != buttonState)
			{
				this._buttonState = buttonState;
				base.Invalidate();
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0003D618 File Offset: 0x0003D618
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			Form form = base.FindForm();
			if (form != null && form.AcceptButton == this)
			{
				this._isDefault = true;
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0003D650 File Offset: 0x0003D650
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this._spacePressed)
			{
				return;
			}
			if (e.Button != MouseButtons.Left)
			{
				this.SetButtonState(DarkControlState.Hover);
				return;
			}
			if (base.ClientRectangle.Contains(e.Location))
			{
				this.SetButtonState(DarkControlState.Pressed);
				return;
			}
			this.SetButtonState(DarkControlState.Hover);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0003D6B4 File Offset: 0x0003D6B4
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (!base.ClientRectangle.Contains(e.Location))
			{
				return;
			}
			this.SetButtonState(DarkControlState.Pressed);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0003D6F0 File Offset: 0x0003D6F0
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this._spacePressed)
			{
				return;
			}
			this.SetButtonState(DarkControlState.Normal);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0003D70C File Offset: 0x0003D70C
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (this._spacePressed)
			{
				return;
			}
			this.SetButtonState(DarkControlState.Normal);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0003D728 File Offset: 0x0003D728
		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			base.OnMouseCaptureChanged(e);
			if (this._spacePressed)
			{
				return;
			}
			Point position = Cursor.Position;
			if (!base.ClientRectangle.Contains(position))
			{
				this.SetButtonState(DarkControlState.Normal);
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0003D770 File Offset: 0x0003D770
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0003D780 File Offset: 0x0003D780
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this._spacePressed = false;
			Point position = Cursor.Position;
			if (!base.ClientRectangle.Contains(position))
			{
				this.SetButtonState(DarkControlState.Normal);
				return;
			}
			this.SetButtonState(DarkControlState.Hover);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0003D7C8 File Offset: 0x0003D7C8
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Space)
			{
				this._spacePressed = true;
				this.SetButtonState(DarkControlState.Pressed);
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0003D7EC File Offset: 0x0003D7EC
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.KeyCode == Keys.Space)
			{
				this._spacePressed = false;
				Point position = Cursor.Position;
				if (!base.ClientRectangle.Contains(position))
				{
					this.SetButtonState(DarkControlState.Normal);
					return;
				}
				this.SetButtonState(DarkControlState.Hover);
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0003D844 File Offset: 0x0003D844
		public override void NotifyDefault(bool value)
		{
			base.NotifyDefault(value);
			if (!base.DesignMode)
			{
				return;
			}
			this._isDefault = value;
			base.Invalidate();
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0003D868 File Offset: 0x0003D868
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rect = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
			Color color = Colors.LightText;
			Color color2 = Colors.GreySelection;
			Color color3 = this._isDefault ? Colors.DarkBlueBackground : Colors.LightBackground;
			if (this.Enabled)
			{
				if (this.ButtonStyle == DarkButtonStyle.Normal)
				{
					if (this.Focused && base.TabStop)
					{
						color2 = Colors.BlueHighlight;
					}
					DarkControlState buttonState = this.ButtonState;
					if (buttonState != DarkControlState.Hover)
					{
						if (buttonState == DarkControlState.Pressed)
						{
							color3 = (this._isDefault ? Colors.DarkBackground : Colors.DarkBackground);
						}
					}
					else
					{
						color3 = (this._isDefault ? Colors.BlueBackground : Colors.LighterBackground);
					}
				}
				else if (this.ButtonStyle == DarkButtonStyle.Flat)
				{
					switch (this.ButtonState)
					{
					case DarkControlState.Normal:
						color3 = Colors.GreyBackground;
						break;
					case DarkControlState.Hover:
						color3 = Colors.MediumBackground;
						break;
					case DarkControlState.Pressed:
						color3 = Colors.DarkBackground;
						break;
					}
				}
			}
			else
			{
				color = Colors.DisabledText;
				color3 = Colors.DarkGreySelection;
			}
			using (SolidBrush solidBrush = new SolidBrush(color3))
			{
				graphics.FillRectangle(solidBrush, rect);
			}
			if (this.ButtonStyle == DarkButtonStyle.Normal)
			{
				using (Pen pen = new Pen(color2, 1f))
				{
					Rectangle rect2 = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
					graphics.DrawRectangle(pen, rect2);
				}
			}
			int num = 0;
			int num2 = 0;
			if (base.Image != null)
			{
				SizeF sizeF = graphics.MeasureString(this.Text, this.Font, rect.Size);
				int num3 = base.ClientSize.Width / 2 - base.Image.Size.Width / 2;
				int num4 = base.ClientSize.Height / 2 - base.Image.Size.Height / 2;
				TextImageRelation textImageRelation = base.TextImageRelation;
				switch (textImageRelation)
				{
				case TextImageRelation.ImageAboveText:
					num2 = base.Image.Size.Height / 2 + this.ImagePadding / 2;
					num4 -= (int)(sizeF.Height / 2f) + this.ImagePadding / 2;
					break;
				case TextImageRelation.TextAboveImage:
					num2 = (base.Image.Size.Height / 2 + this.ImagePadding / 2) * -1;
					num4 += (int)(sizeF.Height / 2f) + this.ImagePadding / 2;
					break;
				case (TextImageRelation)3:
					break;
				case TextImageRelation.ImageBeforeText:
					num = base.Image.Size.Width + this.ImagePadding * 2;
					num3 = this.ImagePadding;
					break;
				default:
					if (textImageRelation == TextImageRelation.TextBeforeImage)
					{
						num3 += (int)sizeF.Width;
					}
					break;
				}
				graphics.DrawImageUnscaled(base.Image, num3, num4);
			}
			using (SolidBrush solidBrush2 = new SolidBrush(color))
			{
				Rectangle r = new Rectangle(rect.Left + num + base.Padding.Left, rect.Top + num2 + base.Padding.Top, rect.Width - base.Padding.Horizontal, rect.Height - base.Padding.Vertical);
				StringFormat format = new StringFormat
				{
					LineAlignment = StringAlignment.Center,
					Alignment = StringAlignment.Center,
					Trimming = StringTrimming.EllipsisCharacter
				};
				graphics.DrawString(this.Text, this.Font, solidBrush2, r, format);
			}
		}

		// Token: 0x04000519 RID: 1305
		private DarkButtonStyle _style;

		// Token: 0x0400051A RID: 1306
		private DarkControlState _buttonState;

		// Token: 0x0400051B RID: 1307
		private bool _isDefault;

		// Token: 0x0400051C RID: 1308
		private bool _spacePressed;

		// Token: 0x0400051D RID: 1309
		private int _padding = Consts.Padding / 2;

		// Token: 0x0400051E RID: 1310
		private int _imagePadding = 5;
	}
}
