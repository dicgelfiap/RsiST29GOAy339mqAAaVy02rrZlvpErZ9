using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x0200009B RID: 155
	public class DarkCheckBox : CheckBox
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00036F88 File Offset: 0x00036F88
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Appearance Appearance
		{
			get
			{
				return base.Appearance;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00036F90 File Offset: 0x00036F90
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool AutoEllipsis
		{
			get
			{
				return base.AutoEllipsis;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00036F98 File Offset: 0x00036F98
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00036FA0 File Offset: 0x00036FA0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00036FA8 File Offset: 0x00036FA8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool FlatAppearance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00036FAC File Offset: 0x00036FAC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new FlatStyle FlatStyle
		{
			get
			{
				return base.FlatStyle;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00036FB4 File Offset: 0x00036FB4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image Image
		{
			get
			{
				return base.Image;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00036FBC File Offset: 0x00036FBC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment ImageAlign
		{
			get
			{
				return base.ImageAlign;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00036FC4 File Offset: 0x00036FC4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int ImageIndex
		{
			get
			{
				return base.ImageIndex;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00036FCC File Offset: 0x00036FCC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string ImageKey
		{
			get
			{
				return base.ImageKey;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00036FD4 File Offset: 0x00036FD4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ImageList ImageList
		{
			get
			{
				return base.ImageList;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00036FDC File Offset: 0x00036FDC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00036FE4 File Offset: 0x00036FE4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new TextImageRelation TextImageRelation
		{
			get
			{
				return base.TextImageRelation;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00036FEC File Offset: 0x00036FEC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool ThreeState
		{
			get
			{
				return base.ThreeState;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00036FF4 File Offset: 0x00036FF4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool UseCompatibleTextRendering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00036FF8 File Offset: 0x00036FF8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool UseVisualStyleBackColor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00036FFC File Offset: 0x00036FFC
		public DarkCheckBox()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00037010 File Offset: 0x00037010
		private void SetControlState(DarkControlState controlState)
		{
			if (this._controlState != controlState)
			{
				this._controlState = controlState;
				base.Invalidate();
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0003702C File Offset: 0x0003702C
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

		// Token: 0x060005DD RID: 1501 RVA: 0x00037090 File Offset: 0x00037090
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (!base.ClientRectangle.Contains(e.Location))
			{
				return;
			}
			this.SetControlState(DarkControlState.Pressed);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000370CC File Offset: 0x000370CC
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this._spacePressed)
			{
				return;
			}
			this.SetControlState(DarkControlState.Normal);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000370E8 File Offset: 0x000370E8
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (this._spacePressed)
			{
				return;
			}
			this.SetControlState(DarkControlState.Normal);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00037104 File Offset: 0x00037104
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

		// Token: 0x060005E1 RID: 1505 RVA: 0x0003714C File Offset: 0x0003714C
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0003715C File Offset: 0x0003715C
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

		// Token: 0x060005E3 RID: 1507 RVA: 0x000371A4 File Offset: 0x000371A4
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Space)
			{
				this._spacePressed = true;
				this.SetControlState(DarkControlState.Pressed);
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000371C8 File Offset: 0x000371C8
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.KeyCode == Keys.Space)
			{
				this._spacePressed = false;
				Point position = Cursor.Position;
				if (!base.ClientRectangle.Contains(position))
				{
					this.SetControlState(DarkControlState.Normal);
					return;
				}
				this.SetControlState(DarkControlState.Hover);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00037220 File Offset: 0x00037220
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rect = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
			int checkBoxSize = Consts.CheckBoxSize;
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
			using (Pen pen = new Pen(color2))
			{
				Rectangle rect2 = new Rectangle(0, rect.Height / 2 - checkBoxSize / 2, checkBoxSize, checkBoxSize);
				graphics.DrawRectangle(pen, rect2);
			}
			if (base.Checked)
			{
				using (SolidBrush solidBrush2 = new SolidBrush(color3))
				{
					Rectangle rect3 = new Rectangle(2, rect.Height / 2 - (checkBoxSize - 4) / 2, checkBoxSize - 3, checkBoxSize - 3);
					graphics.FillRectangle(solidBrush2, rect3);
				}
			}
			using (SolidBrush solidBrush3 = new SolidBrush(color))
			{
				StringFormat format = new StringFormat
				{
					LineAlignment = StringAlignment.Center,
					Alignment = StringAlignment.Near
				};
				Rectangle r = new Rectangle(checkBoxSize + 4, 0, rect.Width - checkBoxSize, rect.Height);
				graphics.DrawString(this.Text, this.Font, solidBrush3, r, format);
			}
		}

		// Token: 0x040004BC RID: 1212
		private DarkControlState _controlState;

		// Token: 0x040004BD RID: 1213
		private bool _spacePressed;
	}
}
