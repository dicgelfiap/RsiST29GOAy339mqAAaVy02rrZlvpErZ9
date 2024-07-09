using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Config;

namespace DarkUI.Controls
{
	// Token: 0x020000A8 RID: 168
	public class DarkListView : DarkScrollView
	{
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060006AD RID: 1709 RVA: 0x0003BA08 File Offset: 0x0003BA08
		// (remove) Token: 0x060006AE RID: 1710 RVA: 0x0003BA44 File Offset: 0x0003BA44
		public event EventHandler SelectedIndicesChanged;

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0003BA80 File Offset: 0x0003BA80
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0003BA88 File Offset: 0x0003BA88
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ObservableCollection<DarkListItem> Items
		{
			get
			{
				return this._items;
			}
			set
			{
				if (this._items != null)
				{
					this._items.CollectionChanged -= this.Items_CollectionChanged;
				}
				this._items = value;
				this._items.CollectionChanged += this.Items_CollectionChanged;
				this.UpdateListBox();
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0003BAE0 File Offset: 0x0003BAE0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<int> SelectedIndices
		{
			get
			{
				return this._selectedIndices;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0003BAE8 File Offset: 0x0003BAE8
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0003BAF0 File Offset: 0x0003BAF0
		[Category("Appearance")]
		[Description("Determines the height of the individual list view items.")]
		[DefaultValue(20)]
		public int ItemHeight
		{
			get
			{
				return this._itemHeight;
			}
			set
			{
				this._itemHeight = value;
				this.UpdateListBox();
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0003BB00 File Offset: 0x0003BB00
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0003BB08 File Offset: 0x0003BB08
		[Category("Behaviour")]
		[Description("Determines whether multiple list view items can be selected at once.")]
		[DefaultValue(false)]
		public bool MultiSelect
		{
			get
			{
				return this._multiSelect;
			}
			set
			{
				this._multiSelect = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0003BB14 File Offset: 0x0003BB14
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0003BB1C File Offset: 0x0003BB1C
		[Category("Appearance")]
		[Description("Determines whether icons are rendered with the list items.")]
		[DefaultValue(false)]
		public bool ShowIcons { get; set; }

		// Token: 0x060006B8 RID: 1720 RVA: 0x0003BB28 File Offset: 0x0003BB28
		public DarkListView()
		{
			this.Items = new ObservableCollection<DarkListItem>();
			this._selectedIndices = new List<int>();
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0003BB64 File Offset: 0x0003BB64
		private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null)
			{
				using (Graphics graphics = base.CreateGraphics())
				{
					foreach (object obj in e.NewItems)
					{
						DarkListItem darkListItem = (DarkListItem)obj;
						darkListItem.TextChanged += this.Item_TextChanged;
						this.UpdateItemSize(darkListItem, graphics);
					}
				}
				if (e.NewStartingIndex < this.Items.Count - 1)
				{
					for (int i = e.NewStartingIndex; i <= this.Items.Count - 1; i++)
					{
						this.UpdateItemPosition(this.Items[i], i);
					}
				}
			}
			if (e.OldItems != null)
			{
				foreach (object obj2 in e.OldItems)
				{
					((DarkListItem)obj2).TextChanged -= this.Item_TextChanged;
				}
				if (e.OldStartingIndex < this.Items.Count - 1)
				{
					for (int j = e.OldStartingIndex; j <= this.Items.Count - 1; j++)
					{
						this.UpdateItemPosition(this.Items[j], j);
					}
				}
			}
			if (this.Items.Count == 0 && this._selectedIndices.Count > 0)
			{
				this._selectedIndices.Clear();
				if (this.SelectedIndicesChanged != null)
				{
					this.SelectedIndicesChanged(this, null);
				}
			}
			this.UpdateContentSize();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0003BD58 File Offset: 0x0003BD58
		private void Item_TextChanged(object sender, EventArgs e)
		{
			DarkListItem item = (DarkListItem)sender;
			this.UpdateItemSize(item);
			this.UpdateContentSize(item);
			base.Invalidate();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0003BD84 File Offset: 0x0003BD84
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.Items.Count == 0)
			{
				return;
			}
			if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
			{
				return;
			}
			Point offsetMousePosition = base.OffsetMousePosition;
			List<int> source = this.ItemIndexesInView().ToList<int>();
			int num = source.Min();
			int num2 = source.Max();
			int width = Math.Max(base.ContentSize.Width, base.Viewport.Width);
			for (int i = num; i <= num2; i++)
			{
				Rectangle rectangle = new Rectangle(0, i * this.ItemHeight, width, this.ItemHeight);
				if (rectangle.Contains(offsetMousePosition))
				{
					if (this.MultiSelect && Control.ModifierKeys == Keys.Shift)
					{
						this.SelectAnchoredRange(i);
					}
					else if (this.MultiSelect && Control.ModifierKeys == Keys.Control)
					{
						this.ToggleItem(i);
					}
					else
					{
						this.SelectItem(i);
					}
				}
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0003BEA4 File Offset: 0x0003BEA4
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (this.Items.Count == 0)
			{
				return;
			}
			if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up)
			{
				return;
			}
			if (this.MultiSelect && Control.ModifierKeys == Keys.Shift)
			{
				if (e.KeyCode == Keys.Up)
				{
					if (this._anchoredItemEnd - 1 >= 0)
					{
						this.SelectAnchoredRange(this._anchoredItemEnd - 1);
						this.EnsureVisible();
					}
				}
				else if (e.KeyCode == Keys.Down && this._anchoredItemEnd + 1 <= this.Items.Count - 1)
				{
					this.SelectAnchoredRange(this._anchoredItemEnd + 1);
				}
			}
			else if (e.KeyCode == Keys.Up)
			{
				if (this._anchoredItemEnd - 1 >= 0)
				{
					this.SelectItem(this._anchoredItemEnd - 1);
				}
			}
			else if (e.KeyCode == Keys.Down && this._anchoredItemEnd + 1 <= this.Items.Count - 1)
			{
				this.SelectItem(this._anchoredItemEnd + 1);
			}
			this.EnsureVisible();
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0003BFDC File Offset: 0x0003BFDC
		public int GetItemIndex(DarkListItem item)
		{
			return this.Items.IndexOf(item);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0003BFEC File Offset: 0x0003BFEC
		public void SelectItem(int index)
		{
			if (index < 0 || index > this.Items.Count - 1)
			{
				throw new IndexOutOfRangeException(string.Format("Value '{0}' is outside of valid range.", index));
			}
			this._selectedIndices.Clear();
			this._selectedIndices.Add(index);
			if (this.SelectedIndicesChanged != null)
			{
				this.SelectedIndicesChanged(this, null);
			}
			this._anchoredItemStart = index;
			this._anchoredItemEnd = index;
			base.Invalidate();
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0003C070 File Offset: 0x0003C070
		public void SelectItems(IEnumerable<int> indexes)
		{
			this._selectedIndices.Clear();
			List<int> list = indexes.ToList<int>();
			foreach (int num in list)
			{
				if (num < 0 || num > this.Items.Count - 1)
				{
					throw new IndexOutOfRangeException(string.Format("Value '{0}' is outside of valid range.", num));
				}
				this._selectedIndices.Add(num);
			}
			if (this.SelectedIndicesChanged != null)
			{
				this.SelectedIndicesChanged(this, null);
			}
			this._anchoredItemStart = list[list.Count - 1];
			this._anchoredItemEnd = list[list.Count - 1];
			base.Invalidate();
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0003C154 File Offset: 0x0003C154
		public void ToggleItem(int index)
		{
			if (this._selectedIndices.Contains(index))
			{
				this._selectedIndices.Remove(index);
				if (this._anchoredItemStart == index && this._anchoredItemEnd == index)
				{
					if (this._selectedIndices.Count > 0)
					{
						this._anchoredItemStart = this._selectedIndices[0];
						this._anchoredItemEnd = this._selectedIndices[0];
					}
					else
					{
						this._anchoredItemStart = -1;
						this._anchoredItemEnd = -1;
					}
				}
				if (this._anchoredItemStart == index)
				{
					if (this._anchoredItemEnd < index)
					{
						this._anchoredItemStart = index - 1;
					}
					else if (this._anchoredItemEnd > index)
					{
						this._anchoredItemStart = index + 1;
					}
					else
					{
						this._anchoredItemStart = this._anchoredItemEnd;
					}
				}
				if (this._anchoredItemEnd == index)
				{
					if (this._anchoredItemStart < index)
					{
						this._anchoredItemEnd = index - 1;
					}
					else if (this._anchoredItemStart > index)
					{
						this._anchoredItemEnd = index + 1;
					}
					else
					{
						this._anchoredItemEnd = this._anchoredItemStart;
					}
				}
			}
			else
			{
				this._selectedIndices.Add(index);
				this._anchoredItemStart = index;
				this._anchoredItemEnd = index;
			}
			if (this.SelectedIndicesChanged != null)
			{
				this.SelectedIndicesChanged(this, null);
			}
			base.Invalidate();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0003C2B8 File Offset: 0x0003C2B8
		public void SelectItems(int startRange, int endRange)
		{
			this._selectedIndices.Clear();
			if (startRange == endRange)
			{
				this._selectedIndices.Add(startRange);
			}
			if (startRange < endRange)
			{
				for (int i = startRange; i <= endRange; i++)
				{
					this._selectedIndices.Add(i);
				}
			}
			else if (startRange > endRange)
			{
				for (int j = startRange; j >= endRange; j--)
				{
					this._selectedIndices.Add(j);
				}
			}
			if (this.SelectedIndicesChanged != null)
			{
				this.SelectedIndicesChanged(this, null);
			}
			base.Invalidate();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0003C350 File Offset: 0x0003C350
		private void SelectAnchoredRange(int index)
		{
			this._anchoredItemEnd = index;
			this.SelectItems(this._anchoredItemStart, index);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0003C368 File Offset: 0x0003C368
		private void UpdateListBox()
		{
			using (Graphics graphics = base.CreateGraphics())
			{
				for (int i = 0; i <= this.Items.Count - 1; i++)
				{
					DarkListItem item = this.Items[i];
					this.UpdateItemSize(item, graphics);
					this.UpdateItemPosition(item, i);
				}
			}
			this.UpdateContentSize();
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0003C3DC File Offset: 0x0003C3DC
		private void UpdateItemSize(DarkListItem item)
		{
			using (Graphics graphics = base.CreateGraphics())
			{
				this.UpdateItemSize(item, graphics);
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0003C41C File Offset: 0x0003C41C
		private void UpdateItemSize(DarkListItem item, Graphics g)
		{
			SizeF sizeF = g.MeasureString(item.Text, this.Font);
			float width = sizeF.Width;
			sizeF.Width = width + 1f;
			if (this.ShowIcons)
			{
				sizeF.Width += (float)(this._iconSize + 8);
			}
			item.Area = new Rectangle(item.Area.Left, item.Area.Top, (int)sizeF.Width, item.Area.Height);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0003C4B4 File Offset: 0x0003C4B4
		private void UpdateItemPosition(DarkListItem item, int index)
		{
			item.Area = new Rectangle(2, index * this.ItemHeight, item.Area.Width, this.ItemHeight);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0003C4F0 File Offset: 0x0003C4F0
		private void UpdateContentSize()
		{
			int num = 0;
			foreach (DarkListItem darkListItem in this.Items)
			{
				if (darkListItem.Area.Right + 1 > num)
				{
					num = darkListItem.Area.Right + 1;
				}
			}
			int num2 = num;
			int num3 = this.Items.Count * this.ItemHeight;
			if (base.ContentSize.Width != num2 || base.ContentSize.Height != num3)
			{
				base.ContentSize = new Size(num2, num3);
				base.Invalidate();
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0003C5C0 File Offset: 0x0003C5C0
		private void UpdateContentSize(DarkListItem item)
		{
			int num = item.Area.Right + 1;
			if (num == base.ContentSize.Width)
			{
				this.UpdateContentSize();
				return;
			}
			if (num > base.ContentSize.Width)
			{
				base.ContentSize = new Size(num, base.ContentSize.Height);
				base.Invalidate();
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0003C634 File Offset: 0x0003C634
		public void EnsureVisible()
		{
			if (this.SelectedIndices.Count == 0)
			{
				return;
			}
			int num;
			if (!this.MultiSelect)
			{
				num = this.SelectedIndices[0] * this.ItemHeight;
			}
			else
			{
				num = this._anchoredItemEnd * this.ItemHeight;
			}
			int num2 = num + this.ItemHeight;
			if (num < base.Viewport.Top)
			{
				base.VScrollTo(num);
			}
			if (num2 > base.Viewport.Bottom)
			{
				base.VScrollTo(num2 - base.Viewport.Height);
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0003C6D8 File Offset: 0x0003C6D8
		private IEnumerable<int> ItemIndexesInView()
		{
			int num = base.Viewport.Top / this.ItemHeight - 1;
			if (num < 0)
			{
				num = 0;
			}
			int num2 = (base.Viewport.Top + base.Viewport.Height) / this.ItemHeight + 1;
			if (num2 > this.Items.Count)
			{
				num2 = this.Items.Count;
			}
			return Enumerable.Range(num, num2 - num);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0003C758 File Offset: 0x0003C758
		private IEnumerable<DarkListItem> ItemsInView()
		{
			return (from index in this.ItemIndexesInView()
			select this.Items[index]).ToList<DarkListItem>();
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0003C778 File Offset: 0x0003C778
		protected override void PaintContent(Graphics g)
		{
			List<int> list = this.ItemIndexesInView().ToList<int>();
			if (list.Count == 0)
			{
				return;
			}
			int num = list.Min();
			int num2 = list.Max();
			for (int i = num; i <= num2; i++)
			{
				int width = Math.Max(base.ContentSize.Width, base.Viewport.Width);
				Rectangle rect = new Rectangle(0, i * this.ItemHeight, width, this.ItemHeight);
				Color color = (i % 2 == 0) ? Colors.HeaderBackground : Colors.GreyBackground;
				if (this.SelectedIndices.Count > 0 && this.SelectedIndices.Contains(i))
				{
					color = (this.Focused ? Colors.BlueSelection : Colors.GreySelection);
				}
				using (SolidBrush solidBrush = new SolidBrush(color))
				{
					g.FillRectangle(solidBrush, rect);
				}
				if (this.ShowIcons && this.Items[i].Icon != null)
				{
					g.DrawImageUnscaled(this.Items[i].Icon, new Point(rect.Left + 5, rect.Top + rect.Height / 2 - this._iconSize / 2));
				}
				using (SolidBrush solidBrush2 = new SolidBrush(this.Items[i].TextColor))
				{
					StringFormat format = new StringFormat
					{
						Alignment = StringAlignment.Near,
						LineAlignment = StringAlignment.Center
					};
					Font font = new Font(this.Font, this.Items[i].FontStyle);
					Rectangle r = new Rectangle(rect.Left + 2, rect.Top, rect.Width, rect.Height);
					if (this.ShowIcons)
					{
						r.X += this._iconSize + 8;
					}
					g.DrawString(this.Items[i].Text, font, solidBrush2, r, format);
				}
			}
		}

		// Token: 0x04000505 RID: 1285
		private int _itemHeight = 20;

		// Token: 0x04000506 RID: 1286
		private bool _multiSelect;

		// Token: 0x04000507 RID: 1287
		private readonly int _iconSize = 16;

		// Token: 0x04000508 RID: 1288
		private ObservableCollection<DarkListItem> _items;

		// Token: 0x04000509 RID: 1289
		private List<int> _selectedIndices;

		// Token: 0x0400050A RID: 1290
		private int _anchoredItemStart = -1;

		// Token: 0x0400050B RID: 1291
		private int _anchoredItemEnd = -1;
	}
}
