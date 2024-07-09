using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000A3 RID: 163
	public class DarkRadioButton : RadioButton
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00038D74 File Offset: 0x00038D74
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Appearance Appearance
		{
			get
			{
				return base.Appearance;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00038D7C File Offset: 0x00038D7C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool AutoEllipsis
		{
			get
			{
				return base.AutoEllipsis;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00038D84 File Offset: 0x00038D84
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00038D8C File Offset: 0x00038D8C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00038D94 File Offset: 0x00038D94
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool FlatAppearance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00038D98 File Offset: 0x00038D98
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new FlatStyle FlatStyle
		{
			get
			{
				return base.FlatStyle;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00038DA0 File Offset: 0x00038DA0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image Image
		{
			get
			{
				return base.Image;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00038DA8 File Offset: 0x00038DA8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment ImageAlign
		{
			get
			{
				return base.ImageAlign;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00038DB0 File Offset: 0x00038DB0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int ImageIndex
		{
			get
			{
				return base.ImageIndex;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00038DB8 File Offset: 0x00038DB8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string ImageKey
		{
			get
			{
				return base.ImageKey;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00038DC0 File Offset: 0x00038DC0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ImageList ImageList
		{
			get
			{
				return base.ImageList;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00038DC8 File Offset: 0x00038DC8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00038DD0 File Offset: 0x00038DD0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new TextImageRelation TextImageRelation
		{
			get
			{
				return base.TextImageRelation;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00038DD8 File Offset: 0x00038DD8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool UseCompatibleTextRendering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00038DDC File Offset: 0x00038DDC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool UseVisualStyleBackColor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00038DE0 File Offset: 0x00038DE0
		public DarkRadioButton()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00038DF4 File Offset: 0x00038DF4
		private void SetControlState(DarkControlState controlState)
		{
			if (this._controlState != controlState)
			{
				this._controlState = controlState;
				base.Invalidate();
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00038E10 File Offset: 0x00038E10
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this._spacePressed)
			{
				return;
			}
			if (e.Button != MouseButtons.Left)
			{
				this.SetControlState(DarkControlState.Hover);
				return;
			}
			if (base.ClientRectangle.Contains(e.Location))
			{
				this.SetControlState(DarkControlState.Pressed);
				return;
			}
			this.SetControlState(DarkControlState.Hover);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00038E74 File Offset: 0x00038E74
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (!base.ClientRectangle.Contains(e.Location))
			{
				return;
			}
			this.SetControlState(DarkControlState.Pressed);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00038EB0 File Offset: 0x00038EB0
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this._spacePressed)
			{
				return;
			}
			this.SetControlState(DarkControlState.Normal);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00038ECC File Offset: 0x00038ECC
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (this._spacePressed)
			{
				return;
			}
			this.SetControlState(DarkControlState.Normal);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00038EE8 File Offset: 0x00038EE8
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
				this.SetControlState(DarkControlState.Normal);
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00038F30 File Offset: 0x00038F30
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00038F40 File Offset: 0x00038F40
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this._spacePressed = false;
			Point position = Cursor.Position;
			if (!base.ClientRectangle.Contains(position))
			{
				this.SetControlState(DarkControlState.Normal);
				return;
			}
			this.SetControlState(DarkControlState.Hover);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00038F88 File Offset: 0x00038F88
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rect = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
			int radioButtonSize = Consts.RadioButtonSize;
			Color color = Colors.LightText;
			Color color2 = Colors.LightText;
			Color color3 = Colors.LightestBackground;
			if (base.Enabled)
			{
				if (this.Focused)
				{
					color2 = Colors.BlueHighlight;
					color3 = Colors.BlueSelection;
				}
				if (this._controlState == DarkControlState.Hover)
				{
					color2 = Colors.BlueHighlight;
					color3 = Colors.BlueSelection;
				}
				else if (this._controlState == DarkControlState.Pressed)
				{
					color2 = Colors.GreyHighlight;
					color3 = Colors.GreySelection;
				}
			}
			else
			{
				color = Colors.DisabledText;
				color2 = Colors.GreyHighlight;
				color3 = Colors.GreySelection;
			}
			using (SolidBrush solidBrush = new SolidBrush(Colors.GreyBackground))
			{
				graphics.FillRectangle(solidBrush, rect);
			}
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			using (Pen pen = new Pen(color2))
			{
				Rectangle rect2 = new Rectangle(0, rect.Height / 2 - radioButtonSize / 2, radioButtonSize, radioButtonSize);
				graphics.DrawEllipse(pen, rect2);
			}
			if (base.Checked)
			{
				using (SolidBrush solidBrush2 = new SolidBrush(color3))
				{
					Rectangle rect3 = new Rectangle(3, rect.Height / 2 - (radioButtonSize - 7) / 2 - 1, radioButtonSize - 6, radioButtonSize - 6);
					graphics.FillEllipse(solidBrush2, rect3);
				}
			}
			graphics.SmoothingMode = SmoothingMode.Default;
			using (SolidBrush solidBrush3 = new SolidBrush(color))
			{
				StringFormat format = new StringFormat
				{
					LineAlignment = StringAlignment.Center,
					Alignment = StringAlignment.Near
				};
				Rectangle r = new Rectangle(radioButtonSize + 4, 0, rect.Width - radioButtonSize, rect.Height);
				graphics.DrawString(this.Text, this.Font, solidBrush3, r, format);
			}
		}

		// Token: 0x040004DC RID: 1244
		private DarkControlState _controlState;

		// Token: 0x040004DD RID: 1245
		private bool _spacePressed;
	}
}
