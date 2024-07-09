using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x0200009D RID: 157
	public class DarkDropdownList : Control
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060005ED RID: 1517 RVA: 0x00037490 File Offset: 0x00037490
		// (remove) Token: 0x060005EE RID: 1518 RVA: 0x000374CC File Offset: 0x000374CC
		public event EventHandler SelectedItemChanged;

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00037508 File Offset: 0x00037508
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ObservableCollection<DarkDropdownItem> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00037510 File Offset: 0x00037510
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00037518 File Offset: 0x00037518
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DarkDropdownItem SelectedItem
		{
			get
			{
				return this._selectedItem;
			}
			set
			{
				this._selectedItem = value;
				EventHandler selectedItemChanged = this.SelectedItemChanged;
				if (selectedItemChanged == null)
				{
					return;
				}
				selectedItemChanged(this, new EventArgs());
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0003753C File Offset: 0x0003753C
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00037544 File Offset: 0x00037544
		[Category("Appearance")]
		[Description("Determines whether a border is drawn around the control.")]
		[DefaultValue(true)]
		public bool ShowBorder
		{
			get
			{
				return this._showBorder;
			}
			set
			{
				this._showBorder = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00037554 File Offset: 0x00037554
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 26);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00037560 File Offset: 0x00037560
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DarkControlState ControlState
		{
			get
			{
				return this._controlState;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00037568 File Offset: 0x00037568
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00037570 File Offset: 0x00037570
		[Category("Appearance")]
		[Description("Determines the height of the individual list view items.")]
		[DefaultValue(22)]
		public int ItemHeight
		{
			get
			{
				return this._itemHeight;
			}
			set
			{
				this._itemHeight = value;
				this.ResizeMenu();
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00037580 File Offset: 0x00037580
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00037588 File Offset: 0x00037588
		[Category("Appearance")]
		[Description("Determines the maximum height of the dropdown panel.")]
		[DefaultValue(130)]
		public int MaxHeight
		{
			get
			{
				return this._maxHeight;
			}
			set
			{
				this._maxHeight = value;
				this.ResizeMenu();
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00037598 File Offset: 0x00037598
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x000375A0 File Offset: 0x000375A0
		[Category("Behavior")]
		[Description("Determines what location the dropdown list appears.")]
		[DefaultValue(ToolStripDropDownDirection.Default)]
		public ToolStripDropDownDirection DropdownDirection
		{
			get
			{
				return this._dropdownDirection;
			}
			set
			{
				this._dropdownDirection = value;
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000375AC File Offset: 0x000375AC
		public DarkDropdownList()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.UserMouse | ControlStyles.OptimizedDoubleBuffer, true);
			this._menu.AutoSize = false;
			this._menu.Closed += this.Menu_Closed;
			this.Items.CollectionChanged += this.Items_CollectionChanged;
			this.SelectedItemChanged += this.DarkDropdownList_SelectedItemChanged;
			this.SetControlState(DarkControlState.Normal);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00037664 File Offset: 0x00037664
		private ToolStripMenuItem GetMenuItem(DarkDropdownItem item)
		{
			foreach (object obj in this._menu.Items)
			{
				ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)obj;
				if ((DarkDropdownItem)toolStripMenuItem.Tag == item)
				{
					return toolStripMenuItem;
				}
			}
			return null;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000376E0 File Offset: 0x000376E0
		private void SetControlState(DarkControlState controlState)
		{
			if (this._menuOpen)
			{
				return;
			}
			if (this._controlState != controlState)
			{
				this._controlState = controlState;
				base.Invalidate();
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00037708 File Offset: 0x00037708
		private void ShowMenu()
		{
			if (this._menu.Visible)
			{
				return;
			}
			this.SetControlState(DarkControlState.Pressed);
			this._menuOpen = true;
			Point position = new Point(0, base.ClientRectangle.Bottom);
			if (this._dropdownDirection == ToolStripDropDownDirection.AboveLeft || this._dropdownDirection == ToolStripDropDownDirection.AboveRight)
			{
				position.Y = 0;
			}
			this._menu.Show(this, position, this._dropdownDirection);
			if (this.SelectedItem != null)
			{
				this.GetMenuItem(this.SelectedItem).Select();
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0003779C File Offset: 0x0003779C
		private void ResizeMenu()
		{
			int width = base.ClientRectangle.Width;
			int num = this._menu.Items.Count * this._itemHeight + 4;
			if (num > this._maxHeight)
			{
				num = this._maxHeight;
			}
			foreach (object obj in this._menu.Items)
			{
				ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)obj;
				toolStripMenuItem.AutoSize = true;
				if (toolStripMenuItem.Size.Width > width)
				{
					width = toolStripMenuItem.Size.Width;
				}
				toolStripMenuItem.AutoSize = false;
			}
			foreach (object obj2 in this._menu.Items)
			{
				((ToolStripMenuItem)obj2).Size = new Size(width - 1, this._itemHeight);
			}
			this._menu.Size = new Size(width, num);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000378EC File Offset: 0x000378EC
		private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (object obj in e.NewItems)
				{
					DarkDropdownItem darkDropdownItem = (DarkDropdownItem)obj;
					ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(darkDropdownItem.Text)
					{
						Image = darkDropdownItem.Icon,
						AutoSize = false,
						Height = this._itemHeight,
						Font = this.Font,
						Tag = darkDropdownItem,
						TextAlign = ContentAlignment.MiddleLeft
					};
					this._menu.Items.Add(toolStripMenuItem);
					toolStripMenuItem.Click += this.Item_Select;
					if (this.SelectedItem == null)
					{
						this.SelectedItem = darkDropdownItem;
					}
				}
			}
			if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				foreach (object obj2 in e.OldItems)
				{
					DarkDropdownItem darkDropdownItem2 = (DarkDropdownItem)obj2;
					foreach (object obj3 in this._menu.Items)
					{
						ToolStripMenuItem toolStripMenuItem2 = (ToolStripMenuItem)obj3;
						if ((DarkDropdownItem)toolStripMenuItem2.Tag == darkDropdownItem2)
						{
							this._menu.Items.Remove(toolStripMenuItem2);
						}
					}
				}
			}
			if (e.Action == NotifyCollectionChangedAction.Reset)
			{
				this._menu.Items.Clear();
				this.SelectedItem = null;
			}
			this.ResizeMenu();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00037AD0 File Offset: 0x00037AD0
		private void Item_Select(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			if (toolStripMenuItem == null)
			{
				return;
			}
			DarkDropdownItem darkDropdownItem = (DarkDropdownItem)toolStripMenuItem.Tag;
			if (this._selectedItem != darkDropdownItem)
			{
				this.SelectedItem = darkDropdownItem;
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00037B10 File Offset: 0x00037B10
		private void DarkDropdownList_SelectedItemChanged(object sender, EventArgs e)
		{
			foreach (object obj in this._menu.Items)
			{
				ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)obj;
				if ((DarkDropdownItem)toolStripMenuItem.Tag == this.SelectedItem)
				{
					toolStripMenuItem.BackColor = Colors.DarkBlueBackground;
					toolStripMenuItem.Font = new Font(this.Font, FontStyle.Bold);
				}
				else
				{
					toolStripMenuItem.BackColor = Colors.GreyBackground;
					toolStripMenuItem.Font = new Font(this.Font, FontStyle.Regular);
				}
			}
			base.Invalidate();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00037BCC File Offset: 0x00037BCC
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.ResizeMenu();
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00037BDC File Offset: 0x00037BDC
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
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

		// Token: 0x06000606 RID: 1542 RVA: 0x00037C34 File Offset: 0x00037C34
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.ShowMenu();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00037C44 File Offset: 0x00037C44
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.SetControlState(DarkControlState.Normal);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00037C54 File Offset: 0x00037C54
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.SetControlState(DarkControlState.Normal);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00037C64 File Offset: 0x00037C64
		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			base.OnMouseCaptureChanged(e);
			Point position = Cursor.Position;
			if (!base.ClientRectangle.Contains(position))
			{
				this.SetControlState(DarkControlState.Normal);
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00037CA0 File Offset: 0x00037CA0
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00037CB0 File Offset: 0x00037CB0
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			Point position = Cursor.Position;
			if (!base.ClientRectangle.Contains(position))
			{
				this.SetControlState(DarkControlState.Normal);
				return;
			}
			this.SetControlState(DarkControlState.Hover);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00037CF4 File Offset: 0x00037CF4
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Space)
			{
				this.ShowMenu();
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00037D10 File Offset: 0x00037D10
		private void Menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			this._menuOpen = false;
			if (!base.ClientRectangle.Contains(Control.MousePosition))
			{
				this.SetControlState(DarkControlState.Normal);
				return;
			}
			this.SetControlState(DarkControlState.Hover);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00037D50 File Offset: 0x00037D50
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			using (SolidBrush solidBrush = new SolidBrush(Colors.MediumBackground))
			{
				graphics.FillRectangle(solidBrush, base.ClientRectangle);
			}
			if (this.ControlState == DarkControlState.Normal && this.ShowBorder)
			{
				using (Pen pen = new Pen(Colors.LightBorder, 1f))
				{
					Rectangle rect = new Rectangle(base.ClientRectangle.Left, base.ClientRectangle.Top, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1);
					graphics.DrawRectangle(pen, rect);
				}
			}
			if (this.ControlState == DarkControlState.Hover)
			{
				using (SolidBrush solidBrush2 = new SolidBrush(Colors.DarkBorder))
				{
					graphics.FillRectangle(solidBrush2, base.ClientRectangle);
				}
				using (SolidBrush solidBrush3 = new SolidBrush(Colors.DarkBackground))
				{
					Rectangle rect2 = new Rectangle(base.ClientRectangle.Right - DropdownIcons.small_arrow.Width - 8, base.ClientRectangle.Top, DropdownIcons.small_arrow.Width + 8, base.ClientRectangle.Height);
					graphics.FillRectangle(solidBrush3, rect2);
				}
				using (Pen pen2 = new Pen(Colors.BlueSelection, 1f))
				{
					Rectangle rect3 = new Rectangle(base.ClientRectangle.Left, base.ClientRectangle.Top, base.ClientRectangle.Width - 1 - DropdownIcons.small_arrow.Width - 8, base.ClientRectangle.Height - 1);
					graphics.DrawRectangle(pen2, rect3);
				}
			}
			if (this.ControlState == DarkControlState.Pressed)
			{
				using (SolidBrush solidBrush4 = new SolidBrush(Colors.DarkBorder))
				{
					graphics.FillRectangle(solidBrush4, base.ClientRectangle);
				}
				using (SolidBrush solidBrush5 = new SolidBrush(Colors.BlueSelection))
				{
					Rectangle rect4 = new Rectangle(base.ClientRectangle.Right - DropdownIcons.small_arrow.Width - 8, base.ClientRectangle.Top, DropdownIcons.small_arrow.Width + 8, base.ClientRectangle.Height);
					graphics.FillRectangle(solidBrush5, rect4);
				}
			}
			using (Bitmap small_arrow = DropdownIcons.small_arrow)
			{
				graphics.DrawImageUnscaled(small_arrow, base.ClientRectangle.Right - small_arrow.Width - 4, base.ClientRectangle.Top + base.ClientRectangle.Height / 2 - small_arrow.Height / 2);
			}
			if (this.SelectedItem != null)
			{
				bool flag = this.SelectedItem.Icon != null;
				if (flag)
				{
					graphics.DrawImageUnscaled(this.SelectedItem.Icon, new Point(base.ClientRectangle.Left + 5, base.ClientRectangle.Top + base.ClientRectangle.Height / 2 - this._iconSize / 2));
				}
				using (SolidBrush solidBrush6 = new SolidBrush(Colors.LightText))
				{
					StringFormat format = new StringFormat
					{
						Alignment = StringAlignment.Near,
						LineAlignment = StringAlignment.Center
					};
					Rectangle r = new Rectangle(base.ClientRectangle.Left + 2, base.ClientRectangle.Top, base.ClientRectangle.Width - 16, base.ClientRectangle.Height);
					if (flag)
					{
						r.X += this._iconSize + 7;
						r.Width -= this._iconSize + 7;
					}
					graphics.DrawString(this.SelectedItem.Text, this.Font, solidBrush6, r, format);
				}
			}
		}

		// Token: 0x040004C1 RID: 1217
		private DarkControlState _controlState;

		// Token: 0x040004C2 RID: 1218
		private ObservableCollection<DarkDropdownItem> _items = new ObservableCollection<DarkDropdownItem>();

		// Token: 0x040004C3 RID: 1219
		private DarkDropdownItem _selectedItem;

		// Token: 0x040004C4 RID: 1220
		private DarkContextMenu _menu = new DarkContextMenu();

		// Token: 0x040004C5 RID: 1221
		private bool _menuOpen;

		// Token: 0x040004C6 RID: 1222
		private bool _showBorder = true;

		// Token: 0x040004C7 RID: 1223
		private int _itemHeight = 22;

		// Token: 0x040004C8 RID: 1224
		private int _maxHeight = 130;

		// Token: 0x040004C9 RID: 1225
		private readonly int _iconSize = 16;

		// Token: 0x040004CA RID: 1226
		private ToolStripDropDownDirection _dropdownDirection = ToolStripDropDownDirection.Default;
	}
}
