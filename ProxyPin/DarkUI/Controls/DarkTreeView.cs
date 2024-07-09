using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Collections;
using DarkUI.Config;
using DarkUI.Extensions;
using DarkUI.Forms;
using DarkUI.Icons;

namespace DarkUI.Controls
{
	// Token: 0x020000A6 RID: 166
	public class DarkTreeView : DarkScrollView
	{
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000653 RID: 1619 RVA: 0x000392D4 File Offset: 0x000392D4
		// (remove) Token: 0x06000654 RID: 1620 RVA: 0x00039310 File Offset: 0x00039310
		public event EventHandler SelectedNodesChanged;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000655 RID: 1621 RVA: 0x0003934C File Offset: 0x0003934C
		// (remove) Token: 0x06000656 RID: 1622 RVA: 0x00039388 File Offset: 0x00039388
		public event EventHandler AfterNodeExpand;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000657 RID: 1623 RVA: 0x000393C4 File Offset: 0x000393C4
		// (remove) Token: 0x06000658 RID: 1624 RVA: 0x00039400 File Offset: 0x00039400
		public event EventHandler AfterNodeCollapse;

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0003943C File Offset: 0x0003943C
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00039444 File Offset: 0x00039444
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ObservableList<DarkTreeNode> Nodes
		{
			get
			{
				return this._nodes;
			}
			set
			{
				if (this._nodes != null)
				{
					this._nodes.ItemsAdded -= this.Nodes_ItemsAdded;
					this._nodes.ItemsRemoved -= this.Nodes_ItemsRemoved;
					foreach (DarkTreeNode node in this._nodes)
					{
						this.UnhookNodeEvents(node);
					}
				}
				this._nodes = value;
				this._nodes.ItemsAdded += this.Nodes_ItemsAdded;
				this._nodes.ItemsRemoved += this.Nodes_ItemsRemoved;
				foreach (DarkTreeNode node2 in this._nodes)
				{
					this.HookNodeEvents(node2);
				}
				this.UpdateNodes();
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0003955C File Offset: 0x0003955C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ObservableCollection<DarkTreeNode> SelectedNodes
		{
			get
			{
				return this._selectedNodes;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00039564 File Offset: 0x00039564
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0003956C File Offset: 0x0003956C
		[Category("Appearance")]
		[Description("Determines the height of tree nodes.")]
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
				base.MaxDragChange = this._itemHeight;
				this.UpdateNodes();
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00039588 File Offset: 0x00039588
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x00039590 File Offset: 0x00039590
		[Category("Appearance")]
		[Description("Determines the amount of horizontal space given by parent node.")]
		[DefaultValue(20)]
		public int Indent
		{
			get
			{
				return this._indent;
			}
			set
			{
				this._indent = value;
				this.UpdateNodes();
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x000395A0 File Offset: 0x000395A0
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x000395A8 File Offset: 0x000395A8
		[Category("Behavior")]
		[Description("Determines whether multiple tree nodes can be selected at once.")]
		[DefaultValue(false)]
		public bool MultiSelect { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x000395B4 File Offset: 0x000395B4
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x000395BC File Offset: 0x000395BC
		[Category("Behavior")]
		[Description("Determines whether nodes can be moved within this tree view.")]
		[DefaultValue(false)]
		public bool AllowMoveNodes { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x000395C8 File Offset: 0x000395C8
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x000395D0 File Offset: 0x000395D0
		[Category("Appearance")]
		[Description("Determines whether icons are rendered with the tree nodes.")]
		[DefaultValue(false)]
		public bool ShowIcons { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x000395DC File Offset: 0x000395DC
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x000395E4 File Offset: 0x000395E4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int VisibleNodeCount { get; private set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x000395F0 File Offset: 0x000395F0
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x000395F8 File Offset: 0x000395F8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IComparer<DarkTreeNode> TreeViewNodeSorter { get; set; }

		// Token: 0x0600066A RID: 1642 RVA: 0x00039604 File Offset: 0x00039604
		public DarkTreeView()
		{
			this.Nodes = new ObservableList<DarkTreeNode>();
			this._selectedNodes = new ObservableCollection<DarkTreeNode>();
			this._selectedNodes.CollectionChanged += this.SelectedNodes_CollectionChanged;
			base.MaxDragChange = this._itemHeight;
			this.LoadIcons();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0003967C File Offset: 0x0003967C
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this.DisposeIcons();
				if (this.SelectedNodesChanged != null)
				{
					this.SelectedNodesChanged = null;
				}
				if (this.AfterNodeExpand != null)
				{
					this.AfterNodeExpand = null;
				}
				if (this.AfterNodeCollapse != null)
				{
					this.AfterNodeExpand = null;
				}
				if (this._nodes != null)
				{
					this._nodes.Dispose();
				}
				if (this._selectedNodes != null)
				{
					this._selectedNodes.CollectionChanged -= this.SelectedNodes_CollectionChanged;
				}
				this._disposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0003971C File Offset: 0x0003971C
		private void Nodes_ItemsAdded(object sender, ObservableListModified<DarkTreeNode> e)
		{
			foreach (DarkTreeNode darkTreeNode in e.Items)
			{
				darkTreeNode.ParentTree = this;
				darkTreeNode.IsRoot = true;
				this.HookNodeEvents(darkTreeNode);
			}
			if (this.TreeViewNodeSorter != null)
			{
				this.Nodes.Sort(this.TreeViewNodeSorter);
			}
			this.UpdateNodes();
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000397A4 File Offset: 0x000397A4
		private void Nodes_ItemsRemoved(object sender, ObservableListModified<DarkTreeNode> e)
		{
			foreach (DarkTreeNode darkTreeNode in e.Items)
			{
				darkTreeNode.ParentTree = this;
				darkTreeNode.IsRoot = true;
				this.HookNodeEvents(darkTreeNode);
			}
			this.UpdateNodes();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00039810 File Offset: 0x00039810
		private void ChildNodes_ItemsAdded(object sender, ObservableListModified<DarkTreeNode> e)
		{
			foreach (DarkTreeNode node in e.Items)
			{
				this.HookNodeEvents(node);
			}
			this.UpdateNodes();
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0003986C File Offset: 0x0003986C
		private void ChildNodes_ItemsRemoved(object sender, ObservableListModified<DarkTreeNode> e)
		{
			foreach (DarkTreeNode darkTreeNode in e.Items)
			{
				if (this.SelectedNodes.Contains(darkTreeNode))
				{
					this.SelectedNodes.Remove(darkTreeNode);
				}
				this.UnhookNodeEvents(darkTreeNode);
			}
			this.UpdateNodes();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000398E8 File Offset: 0x000398E8
		private void SelectedNodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (this.SelectedNodesChanged != null)
			{
				this.SelectedNodesChanged(this, null);
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00039904 File Offset: 0x00039904
		private void Nodes_TextChanged(object sender, EventArgs e)
		{
			this.UpdateNodes();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0003990C File Offset: 0x0003990C
		private void Nodes_NodeExpanded(object sender, EventArgs e)
		{
			this.UpdateNodes();
			if (this.AfterNodeExpand != null)
			{
				this.AfterNodeExpand(this, null);
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0003992C File Offset: 0x0003992C
		private void Nodes_NodeCollapsed(object sender, EventArgs e)
		{
			this.UpdateNodes();
			if (this.AfterNodeCollapse != null)
			{
				this.AfterNodeCollapse(this, null);
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0003994C File Offset: 0x0003994C
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this._provisionalDragging && base.OffsetMousePosition != this._dragPos)
			{
				this.StartDrag();
				this.HandleDrag();
				return;
			}
			if (base.IsDragging && this._dropNode != null && !this.GetNodeFullRowArea(this._dropNode).Contains(base.OffsetMousePosition))
			{
				this._dropNode = null;
				base.Invalidate();
			}
			this.CheckHover();
			if (base.IsDragging)
			{
				this.HandleDrag();
			}
			base.OnMouseMove(e);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000399EC File Offset: 0x000399EC
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this.CheckHover();
			base.OnMouseWheel(e);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000399FC File Offset: 0x000399FC
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
			{
				foreach (DarkTreeNode node in this.Nodes)
				{
					this.CheckNodeClick(node, base.OffsetMousePosition, e.Button);
				}
			}
			base.OnMouseDown(e);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00039A88 File Offset: 0x00039A88
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (base.IsDragging)
			{
				this.HandleDrop();
			}
			if (this._provisionalDragging)
			{
				if (this._provisionalNode != null)
				{
					Point dragPos = this._dragPos;
					if (base.OffsetMousePosition == dragPos)
					{
						this.SelectNode(this._provisionalNode);
					}
				}
				this._provisionalDragging = false;
			}
			base.OnMouseUp(e);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00039AF4 File Offset: 0x00039AF4
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			if (Control.ModifierKeys == Keys.Control)
			{
				return;
			}
			if (e.Button == MouseButtons.Left)
			{
				foreach (DarkTreeNode node in this.Nodes)
				{
					this.CheckNodeDoubleClick(node, base.OffsetMousePosition);
				}
			}
			base.OnMouseDoubleClick(e);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00039B7C File Offset: 0x00039B7C
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			foreach (DarkTreeNode node in this.Nodes)
			{
				this.NodeMouseLeave(node);
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00039BDC File Offset: 0x00039BDC
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (base.IsDragging)
			{
				return;
			}
			if (this.Nodes.Count == 0)
			{
				return;
			}
			if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
			{
				return;
			}
			if (this._anchoredNodeEnd == null)
			{
				if (this.Nodes.Count > 0)
				{
					this.SelectNode(this.Nodes[0]);
				}
				return;
			}
			if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
			{
				if (this.MultiSelect && Control.ModifierKeys == Keys.Shift)
				{
					if (e.KeyCode == Keys.Up)
					{
						if (this._anchoredNodeEnd.PrevVisibleNode != null)
						{
							this.SelectAnchoredRange(this._anchoredNodeEnd.PrevVisibleNode);
							this.EnsureVisible();
						}
					}
					else if (e.KeyCode == Keys.Down && this._anchoredNodeEnd.NextVisibleNode != null)
					{
						this.SelectAnchoredRange(this._anchoredNodeEnd.NextVisibleNode);
						this.EnsureVisible();
					}
				}
				else if (e.KeyCode == Keys.Up)
				{
					if (this._anchoredNodeEnd.PrevVisibleNode != null)
					{
						this.SelectNode(this._anchoredNodeEnd.PrevVisibleNode);
						this.EnsureVisible();
					}
				}
				else if (e.KeyCode == Keys.Down && this._anchoredNodeEnd.NextVisibleNode != null)
				{
					this.SelectNode(this._anchoredNodeEnd.NextVisibleNode);
					this.EnsureVisible();
				}
			}
			if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
			{
				if (e.KeyCode == Keys.Left)
				{
					if (this._anchoredNodeEnd.Expanded && this._anchoredNodeEnd.Nodes.Count > 0)
					{
						this._anchoredNodeEnd.Expanded = false;
						return;
					}
					if (this._anchoredNodeEnd.ParentNode != null)
					{
						this.SelectNode(this._anchoredNodeEnd.ParentNode);
						this.EnsureVisible();
						return;
					}
				}
				else if (e.KeyCode == Keys.Right)
				{
					if (!this._anchoredNodeEnd.Expanded)
					{
						this._anchoredNodeEnd.Expanded = true;
						return;
					}
					if (this._anchoredNodeEnd.Nodes.Count > 0)
					{
						this.SelectNode(this._anchoredNodeEnd.Nodes[0]);
						this.EnsureVisible();
					}
				}
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00039E68 File Offset: 0x00039E68
		private void DragTimer_Tick(object sender, EventArgs e)
		{
			if (!base.IsDragging)
			{
				this.StopDrag();
				return;
			}
			if (Control.MouseButtons != MouseButtons.Left)
			{
				this.StopDrag();
				return;
			}
			Point point = base.PointToClient(Control.MousePosition);
			if (this._vScrollBar.Visible)
			{
				if (point.Y < base.ClientRectangle.Top)
				{
					int num = (point.Y - base.ClientRectangle.Top) * -1;
					if (num > this.ItemHeight)
					{
						num = this.ItemHeight;
					}
					this._vScrollBar.Value = this._vScrollBar.Value - num;
				}
				if (point.Y > base.ClientRectangle.Bottom)
				{
					int num2 = point.Y - base.ClientRectangle.Bottom;
					if (num2 > this.ItemHeight)
					{
						num2 = this.ItemHeight;
					}
					this._vScrollBar.Value = this._vScrollBar.Value + num2;
				}
			}
			if (this._hScrollBar.Visible)
			{
				if (point.X < base.ClientRectangle.Left)
				{
					int num3 = (point.X - base.ClientRectangle.Left) * -1;
					if (num3 > this.ItemHeight)
					{
						num3 = this.ItemHeight;
					}
					this._hScrollBar.Value = this._hScrollBar.Value - num3;
				}
				if (point.X > base.ClientRectangle.Right)
				{
					int num4 = point.X - base.ClientRectangle.Right;
					if (num4 > this.ItemHeight)
					{
						num4 = this.ItemHeight;
					}
					this._hScrollBar.Value = this._hScrollBar.Value + num4;
				}
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0003A04C File Offset: 0x0003A04C
		private void HookNodeEvents(DarkTreeNode node)
		{
			node.Nodes.ItemsAdded += this.ChildNodes_ItemsAdded;
			node.Nodes.ItemsRemoved += this.ChildNodes_ItemsRemoved;
			node.TextChanged += this.Nodes_TextChanged;
			node.NodeExpanded += this.Nodes_NodeExpanded;
			node.NodeCollapsed += this.Nodes_NodeCollapsed;
			foreach (DarkTreeNode node2 in node.Nodes)
			{
				this.HookNodeEvents(node2);
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0003A10C File Offset: 0x0003A10C
		private void UnhookNodeEvents(DarkTreeNode node)
		{
			node.Nodes.ItemsAdded -= this.ChildNodes_ItemsAdded;
			node.Nodes.ItemsRemoved -= this.ChildNodes_ItemsRemoved;
			node.TextChanged -= this.Nodes_TextChanged;
			node.NodeExpanded -= this.Nodes_NodeExpanded;
			node.NodeCollapsed -= this.Nodes_NodeCollapsed;
			foreach (DarkTreeNode node2 in node.Nodes)
			{
				this.UnhookNodeEvents(node2);
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0003A1CC File Offset: 0x0003A1CC
		private void UpdateNodes()
		{
			if (base.IsDragging)
			{
				return;
			}
			base.ContentSize = new Size(0, 0);
			if (this.Nodes.Count == 0)
			{
				return;
			}
			int height = 0;
			bool flag = false;
			int visibleNodeCount = 0;
			DarkTreeNode darkTreeNode = null;
			for (int i = 0; i <= this.Nodes.Count - 1; i++)
			{
				DarkTreeNode node = this.Nodes[i];
				this.UpdateNode(node, ref darkTreeNode, 0, ref height, ref flag, ref visibleNodeCount);
			}
			base.ContentSize = new Size(base.ContentSize.Width, height);
			this.VisibleNodeCount = visibleNodeCount;
			base.Invalidate();
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0003A278 File Offset: 0x0003A278
		private void UpdateNode(DarkTreeNode node, ref DarkTreeNode prevNode, int indent, ref int yOffset, ref bool isOdd, ref int index)
		{
			this.UpdateNodeBounds(node, yOffset, indent);
			yOffset += this.ItemHeight;
			node.Odd = isOdd;
			isOdd = !isOdd;
			node.VisibleIndex = index;
			index++;
			node.PrevVisibleNode = prevNode;
			if (prevNode != null)
			{
				prevNode.NextVisibleNode = node;
			}
			prevNode = node;
			if (node.Expanded)
			{
				foreach (DarkTreeNode node2 in node.Nodes)
				{
					this.UpdateNode(node2, ref prevNode, indent + this.Indent, ref yOffset, ref isOdd, ref index);
				}
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0003A340 File Offset: 0x0003A340
		private void UpdateNodeBounds(DarkTreeNode node, int yOffset, int indent)
		{
			int y = yOffset + this.ItemHeight / 2 - this._expandAreaSize / 2;
			node.ExpandArea = new Rectangle(indent + 3, y, this._expandAreaSize, this._expandAreaSize);
			int y2 = yOffset + this.ItemHeight / 2 - this._iconSize / 2;
			if (this.ShowIcons)
			{
				node.IconArea = new Rectangle(node.ExpandArea.Right + 2, y2, this._iconSize, this._iconSize);
			}
			else
			{
				node.IconArea = new Rectangle(node.ExpandArea.Right, y2, 0, 0);
			}
			using (Graphics graphics = base.CreateGraphics())
			{
				int num = (int)graphics.MeasureString(node.Text, this.Font).Width;
				node.TextArea = new Rectangle(node.IconArea.Right + 2, yOffset, num + 1, this.ItemHeight);
			}
			node.FullArea = new Rectangle(indent, yOffset, node.TextArea.Right - indent, this.ItemHeight);
			if (base.ContentSize.Width < node.TextArea.Right + 2)
			{
				base.ContentSize = new Size(node.TextArea.Right + 2, base.ContentSize.Height);
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0003A4C4 File Offset: 0x0003A4C4
		private void LoadIcons()
		{
			this.DisposeIcons();
			this._nodeClosed = TreeViewIcons.node_closed_empty.SetColor(Colors.LightText);
			this._nodeClosedHover = TreeViewIcons.node_closed_empty.SetColor(Colors.BlueHighlight);
			this._nodeClosedHoverSelected = TreeViewIcons.node_closed_full.SetColor(Colors.LightText);
			this._nodeOpen = TreeViewIcons.node_open.SetColor(Colors.LightText);
			this._nodeOpenHover = TreeViewIcons.node_open.SetColor(Colors.BlueHighlight);
			this._nodeOpenHoverSelected = TreeViewIcons.node_open_empty.SetColor(Colors.LightText);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0003A55C File Offset: 0x0003A55C
		private void DisposeIcons()
		{
			if (this._nodeClosed != null)
			{
				this._nodeClosed.Dispose();
			}
			if (this._nodeClosedHover != null)
			{
				this._nodeClosedHover.Dispose();
			}
			if (this._nodeClosedHoverSelected != null)
			{
				this._nodeClosedHoverSelected.Dispose();
			}
			if (this._nodeOpen != null)
			{
				this._nodeOpen.Dispose();
			}
			if (this._nodeOpenHover != null)
			{
				this._nodeOpenHover.Dispose();
			}
			if (this._nodeOpenHoverSelected != null)
			{
				this._nodeOpenHoverSelected.Dispose();
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0003A5F4 File Offset: 0x0003A5F4
		private void CheckHover()
		{
			if (!base.ClientRectangle.Contains(base.PointToClient(Control.MousePosition)))
			{
				if (base.IsDragging && this._dropNode != null)
				{
					this._dropNode = null;
					base.Invalidate();
				}
				return;
			}
			foreach (DarkTreeNode node in this.Nodes)
			{
				this.CheckNodeHover(node, base.OffsetMousePosition);
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0003A698 File Offset: 0x0003A698
		private void NodeMouseLeave(DarkTreeNode node)
		{
			node.ExpandAreaHot = false;
			foreach (DarkTreeNode node2 in node.Nodes)
			{
				this.NodeMouseLeave(node2);
			}
			base.Invalidate();
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0003A700 File Offset: 0x0003A700
		private void CheckNodeHover(DarkTreeNode node, Point location)
		{
			if (base.IsDragging)
			{
				if (this.GetNodeFullRowArea(node).Contains(base.OffsetMousePosition))
				{
					DarkTreeNode darkTreeNode = this._dragNodes.Contains(node) ? null : node;
					if (this._dropNode != darkTreeNode)
					{
						this._dropNode = darkTreeNode;
						base.Invalidate();
					}
				}
			}
			else
			{
				bool flag = node.ExpandArea.Contains(location);
				if (node.ExpandAreaHot != flag)
				{
					node.ExpandAreaHot = flag;
					base.Invalidate();
				}
			}
			foreach (DarkTreeNode node2 in node.Nodes)
			{
				this.CheckNodeHover(node2, location);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0003A7E4 File Offset: 0x0003A7E4
		private void CheckNodeClick(DarkTreeNode node, Point location, MouseButtons button)
		{
			if (this.GetNodeFullRowArea(node).Contains(location))
			{
				if (node.ExpandArea.Contains(location))
				{
					if (button == MouseButtons.Left)
					{
						node.Expanded = !node.Expanded;
					}
				}
				else if (button == MouseButtons.Left)
				{
					if (this.MultiSelect && Control.ModifierKeys == Keys.Shift)
					{
						this.SelectAnchoredRange(node);
						return;
					}
					if (this.MultiSelect && Control.ModifierKeys == Keys.Control)
					{
						this.ToggleNode(node);
						return;
					}
					if (!this.SelectedNodes.Contains(node))
					{
						this.SelectNode(node);
					}
					this._dragPos = base.OffsetMousePosition;
					this._provisionalDragging = true;
					this._provisionalNode = node;
					return;
				}
				else if (button == MouseButtons.Right)
				{
					if (this.MultiSelect && Control.ModifierKeys == Keys.Shift)
					{
						return;
					}
					if (this.MultiSelect && Control.ModifierKeys == Keys.Control)
					{
						return;
					}
					if (!this.SelectedNodes.Contains(node))
					{
						this.SelectNode(node);
					}
					return;
				}
			}
			if (node.Expanded)
			{
				foreach (DarkTreeNode node2 in node.Nodes)
				{
					this.CheckNodeClick(node2, location, button);
				}
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0003A970 File Offset: 0x0003A970
		private void CheckNodeDoubleClick(DarkTreeNode node, Point location)
		{
			if (this.GetNodeFullRowArea(node).Contains(location))
			{
				if (!node.ExpandArea.Contains(location))
				{
					node.Expanded = !node.Expanded;
				}
				return;
			}
			if (node.Expanded)
			{
				foreach (DarkTreeNode node2 in node.Nodes)
				{
					this.CheckNodeDoubleClick(node2, location);
				}
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0003AA10 File Offset: 0x0003AA10
		public void SelectNode(DarkTreeNode node)
		{
			this._selectedNodes.Clear();
			this._selectedNodes.Add(node);
			this._anchoredNodeStart = node;
			this._anchoredNodeEnd = node;
			base.Invalidate();
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0003AA40 File Offset: 0x0003AA40
		public void SelectNodes(DarkTreeNode startNode, DarkTreeNode endNode)
		{
			List<DarkTreeNode> list = new List<DarkTreeNode>();
			if (startNode == endNode)
			{
				list.Add(startNode);
			}
			if (startNode.VisibleIndex < endNode.VisibleIndex)
			{
				DarkTreeNode darkTreeNode = startNode;
				list.Add(darkTreeNode);
				while (darkTreeNode != endNode)
				{
					if (darkTreeNode == null)
					{
						break;
					}
					darkTreeNode = darkTreeNode.NextVisibleNode;
					list.Add(darkTreeNode);
				}
			}
			else if (startNode.VisibleIndex > endNode.VisibleIndex)
			{
				DarkTreeNode darkTreeNode2 = startNode;
				list.Add(darkTreeNode2);
				while (darkTreeNode2 != endNode && darkTreeNode2 != null)
				{
					darkTreeNode2 = darkTreeNode2.PrevVisibleNode;
					list.Add(darkTreeNode2);
				}
			}
			this.SelectNodes(list, false);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0003AAE0 File Offset: 0x0003AAE0
		public void SelectNodes(List<DarkTreeNode> nodes, bool updateAnchors = true)
		{
			this._selectedNodes.Clear();
			foreach (DarkTreeNode item in nodes)
			{
				this._selectedNodes.Add(item);
			}
			if (updateAnchors && this._selectedNodes.Count > 0)
			{
				this._anchoredNodeStart = this._selectedNodes[this._selectedNodes.Count - 1];
				this._anchoredNodeEnd = this._selectedNodes[this._selectedNodes.Count - 1];
			}
			base.Invalidate();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0003ABA0 File Offset: 0x0003ABA0
		private void SelectAnchoredRange(DarkTreeNode node)
		{
			this._anchoredNodeEnd = node;
			this.SelectNodes(this._anchoredNodeStart, this._anchoredNodeEnd);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0003ABBC File Offset: 0x0003ABBC
		public void ToggleNode(DarkTreeNode node)
		{
			if (this._selectedNodes.Contains(node))
			{
				this._selectedNodes.Remove(node);
				if (this._anchoredNodeStart == node && this._anchoredNodeEnd == node)
				{
					if (this._selectedNodes.Count > 0)
					{
						this._anchoredNodeStart = this._selectedNodes[0];
						this._anchoredNodeEnd = this._selectedNodes[0];
					}
					else
					{
						this._anchoredNodeStart = null;
						this._anchoredNodeEnd = null;
					}
				}
				if (this._anchoredNodeStart == node)
				{
					if (this._anchoredNodeEnd.VisibleIndex < node.VisibleIndex)
					{
						this._anchoredNodeStart = node.PrevVisibleNode;
					}
					else if (this._anchoredNodeEnd.VisibleIndex > node.VisibleIndex)
					{
						this._anchoredNodeStart = node.NextVisibleNode;
					}
					else
					{
						this._anchoredNodeStart = this._anchoredNodeEnd;
					}
				}
				if (this._anchoredNodeEnd == node)
				{
					if (this._anchoredNodeStart.VisibleIndex < node.VisibleIndex)
					{
						this._anchoredNodeEnd = node.PrevVisibleNode;
					}
					else if (this._anchoredNodeStart.VisibleIndex > node.VisibleIndex)
					{
						this._anchoredNodeEnd = node.NextVisibleNode;
					}
					else
					{
						this._anchoredNodeEnd = this._anchoredNodeStart;
					}
				}
			}
			else
			{
				this._selectedNodes.Add(node);
				this._anchoredNodeStart = node;
				this._anchoredNodeEnd = node;
			}
			base.Invalidate();
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0003AD3C File Offset: 0x0003AD3C
		public Rectangle GetNodeFullRowArea(DarkTreeNode node)
		{
			if (node.ParentNode != null && !node.ParentNode.Expanded)
			{
				return new Rectangle(-1, -1, -1, -1);
			}
			int width = Math.Max(base.ContentSize.Width, base.Viewport.Width);
			return new Rectangle(0, node.FullArea.Top, width, this.ItemHeight);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0003ADB0 File Offset: 0x0003ADB0
		public void EnsureVisible()
		{
			if (this.SelectedNodes.Count == 0)
			{
				return;
			}
			foreach (DarkTreeNode darkTreeNode in this.SelectedNodes)
			{
				darkTreeNode.EnsureVisible();
			}
			int top;
			if (!this.MultiSelect)
			{
				top = this.SelectedNodes[0].FullArea.Top;
			}
			else
			{
				top = this._anchoredNodeEnd.FullArea.Top;
			}
			int num = top + this.ItemHeight;
			if (top < base.Viewport.Top)
			{
				base.VScrollTo(top);
			}
			if (num > base.Viewport.Bottom)
			{
				base.VScrollTo(num - base.Viewport.Height);
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0003AEA4 File Offset: 0x0003AEA4
		public void Sort()
		{
			if (this.TreeViewNodeSorter == null)
			{
				return;
			}
			this.Nodes.Sort(this.TreeViewNodeSorter);
			foreach (DarkTreeNode node in this.Nodes)
			{
				this.SortChildNodes(node);
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0003AF1C File Offset: 0x0003AF1C
		private void SortChildNodes(DarkTreeNode node)
		{
			node.Nodes.Sort(this.TreeViewNodeSorter);
			foreach (DarkTreeNode node2 in node.Nodes)
			{
				this.SortChildNodes(node2);
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0003AF88 File Offset: 0x0003AF88
		public DarkTreeNode FindNode(string path)
		{
			foreach (DarkTreeNode parentNode in this.Nodes)
			{
				DarkTreeNode darkTreeNode = this.FindNode(parentNode, path, true);
				if (darkTreeNode != null)
				{
					return darkTreeNode;
				}
			}
			return null;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0003AFF4 File Offset: 0x0003AFF4
		private DarkTreeNode FindNode(DarkTreeNode parentNode, string path, bool recursive = true)
		{
			if (parentNode.FullPath == path)
			{
				return parentNode;
			}
			foreach (DarkTreeNode darkTreeNode in parentNode.Nodes)
			{
				if (darkTreeNode.FullPath == path)
				{
					return darkTreeNode;
				}
				if (recursive)
				{
					DarkTreeNode darkTreeNode2 = this.FindNode(darkTreeNode, path, true);
					if (darkTreeNode2 != null)
					{
						return darkTreeNode2;
					}
				}
			}
			return null;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0003B094 File Offset: 0x0003B094
		protected override void StartDrag()
		{
			if (!this.AllowMoveNodes)
			{
				this._provisionalDragging = false;
				return;
			}
			this._dragNodes = new List<DarkTreeNode>();
			foreach (DarkTreeNode item in this.SelectedNodes)
			{
				this._dragNodes.Add(item);
			}
			foreach (DarkTreeNode darkTreeNode in this._dragNodes.ToList<DarkTreeNode>())
			{
				if (darkTreeNode.ParentNode != null && this._dragNodes.Contains(darkTreeNode.ParentNode))
				{
					this._dragNodes.Remove(darkTreeNode);
				}
			}
			this._provisionalDragging = false;
			this.Cursor = Cursors.SizeAll;
			base.StartDrag();
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0003B19C File Offset: 0x0003B19C
		private void HandleDrag()
		{
			if (!this.AllowMoveNodes)
			{
				return;
			}
			DarkTreeNode darkTreeNode = this._dropNode;
			if (darkTreeNode == null)
			{
				if (this.Cursor != Cursors.No)
				{
					this.Cursor = Cursors.No;
				}
				return;
			}
			if (this.ForceDropToParent(darkTreeNode))
			{
				darkTreeNode = darkTreeNode.ParentNode;
			}
			if (!this.CanMoveNodes(this._dragNodes, darkTreeNode, false))
			{
				if (this.Cursor != Cursors.No)
				{
					this.Cursor = Cursors.No;
				}
				return;
			}
			if (this.Cursor != Cursors.SizeAll)
			{
				this.Cursor = Cursors.SizeAll;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0003B250 File Offset: 0x0003B250
		private void HandleDrop()
		{
			if (!this.AllowMoveNodes)
			{
				return;
			}
			DarkTreeNode darkTreeNode = this._dropNode;
			if (darkTreeNode == null)
			{
				this.StopDrag();
				return;
			}
			if (this.ForceDropToParent(darkTreeNode))
			{
				darkTreeNode = darkTreeNode.ParentNode;
			}
			if (this.CanMoveNodes(this._dragNodes, darkTreeNode, true))
			{
				List<DarkTreeNode> list = this.SelectedNodes.ToList<DarkTreeNode>();
				this.MoveNodes(this._dragNodes, darkTreeNode);
				foreach (DarkTreeNode darkTreeNode2 in this._dragNodes)
				{
					if (darkTreeNode2.ParentNode == null)
					{
						this.Nodes.Remove(darkTreeNode2);
					}
					else
					{
						darkTreeNode2.ParentNode.Nodes.Remove(darkTreeNode2);
					}
					darkTreeNode.Nodes.Add(darkTreeNode2);
				}
				if (this.TreeViewNodeSorter != null)
				{
					darkTreeNode.Nodes.Sort(this.TreeViewNodeSorter);
				}
				darkTreeNode.Expanded = true;
				this.NodesMoved(this._dragNodes);
				foreach (DarkTreeNode item in list)
				{
					this._selectedNodes.Add(item);
				}
			}
			this.StopDrag();
			this.UpdateNodes();
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0003B3C0 File Offset: 0x0003B3C0
		protected override void StopDrag()
		{
			this._dragNodes = null;
			this._dropNode = null;
			this.Cursor = Cursors.Default;
			base.Invalidate();
			base.StopDrag();
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0003B3E8 File Offset: 0x0003B3E8
		protected virtual bool ForceDropToParent(DarkTreeNode node)
		{
			return false;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0003B3EC File Offset: 0x0003B3EC
		protected virtual bool CanMoveNodes(List<DarkTreeNode> dragNodes, DarkTreeNode dropNode, bool isMoving = false)
		{
			if (dropNode == null)
			{
				return false;
			}
			foreach (DarkTreeNode darkTreeNode in dragNodes)
			{
				if (darkTreeNode == dropNode)
				{
					if (isMoving)
					{
						DarkMessageBox.ShowError("Cannot move " + darkTreeNode.Text + ". The destination folder is the same as the source folder.", Application.ProductName, DarkDialogButton.Ok);
					}
					return false;
				}
				if (darkTreeNode.ParentNode != null && darkTreeNode.ParentNode == dropNode)
				{
					if (isMoving)
					{
						DarkMessageBox.ShowError("Cannot move " + darkTreeNode.Text + ". The destination folder is the same as the source folder.", Application.ProductName, DarkDialogButton.Ok);
					}
					return false;
				}
				for (DarkTreeNode parentNode = dropNode.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
				{
					if (darkTreeNode == parentNode)
					{
						if (isMoving)
						{
							DarkMessageBox.ShowError("Cannot move " + darkTreeNode.Text + ". The destination folder is a subfolder of the source folder.", Application.ProductName, DarkDialogButton.Ok);
						}
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0003B50C File Offset: 0x0003B50C
		protected virtual void MoveNodes(List<DarkTreeNode> dragNodes, DarkTreeNode dropNode)
		{
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0003B510 File Offset: 0x0003B510
		protected virtual void NodesMoved(List<DarkTreeNode> nodesMoved)
		{
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0003B514 File Offset: 0x0003B514
		protected override void PaintContent(Graphics g)
		{
			foreach (DarkTreeNode node in this.Nodes)
			{
				this.DrawNode(node, g);
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0003B570 File Offset: 0x0003B570
		private void DrawNode(DarkTreeNode node, Graphics g)
		{
			Rectangle nodeFullRowArea = this.GetNodeFullRowArea(node);
			Color color = node.Odd ? Colors.HeaderBackground : Colors.GreyBackground;
			if (this.SelectedNodes.Count > 0 && this.SelectedNodes.Contains(node))
			{
				color = (this.Focused ? Colors.BlueSelection : Colors.GreySelection);
			}
			if (base.IsDragging && this._dropNode == node)
			{
				color = (this.Focused ? Colors.BlueSelection : Colors.GreySelection);
			}
			using (SolidBrush solidBrush = new SolidBrush(color))
			{
				g.FillRectangle(solidBrush, nodeFullRowArea);
			}
			if (node.Nodes.Count > 0)
			{
				Point point = new Point(node.ExpandArea.Location.X - 1, node.ExpandArea.Location.Y - 1);
				Bitmap image = this._nodeOpen;
				if (node.Expanded && !node.ExpandAreaHot)
				{
					image = this._nodeOpen;
				}
				else if (node.Expanded && node.ExpandAreaHot && !this.SelectedNodes.Contains(node))
				{
					image = this._nodeOpenHover;
				}
				else if (node.Expanded && node.ExpandAreaHot && this.SelectedNodes.Contains(node))
				{
					image = this._nodeOpenHoverSelected;
				}
				else if (!node.Expanded && !node.ExpandAreaHot)
				{
					image = this._nodeClosed;
				}
				else if (!node.Expanded && node.ExpandAreaHot && !this.SelectedNodes.Contains(node))
				{
					image = this._nodeClosedHover;
				}
				else if (!node.Expanded && node.ExpandAreaHot && this.SelectedNodes.Contains(node))
				{
					image = this._nodeClosedHoverSelected;
				}
				g.DrawImageUnscaled(image, point);
			}
			if (this.ShowIcons && node.Icon != null)
			{
				if (node.Expanded && node.ExpandedIcon != null)
				{
					g.DrawImageUnscaled(node.ExpandedIcon, node.IconArea.Location);
				}
				else
				{
					g.DrawImageUnscaled(node.Icon, node.IconArea.Location);
				}
			}
			using (SolidBrush solidBrush2 = new SolidBrush(Colors.LightText))
			{
				StringFormat format = new StringFormat
				{
					Alignment = StringAlignment.Near,
					LineAlignment = StringAlignment.Center
				};
				g.DrawString(node.Text, this.Font, solidBrush2, node.TextArea, format);
			}
			if (node.Expanded)
			{
				foreach (DarkTreeNode node2 in node.Nodes)
				{
					this.DrawNode(node2, g);
				}
			}
		}

		// Token: 0x040004E4 RID: 1252
		private bool _disposed;

		// Token: 0x040004E5 RID: 1253
		private readonly int _expandAreaSize = 16;

		// Token: 0x040004E6 RID: 1254
		private readonly int _iconSize = 16;

		// Token: 0x040004E7 RID: 1255
		private int _itemHeight = 20;

		// Token: 0x040004E8 RID: 1256
		private int _indent = 20;

		// Token: 0x040004E9 RID: 1257
		private ObservableList<DarkTreeNode> _nodes;

		// Token: 0x040004EA RID: 1258
		private ObservableCollection<DarkTreeNode> _selectedNodes;

		// Token: 0x040004EB RID: 1259
		private DarkTreeNode _anchoredNodeStart;

		// Token: 0x040004EC RID: 1260
		private DarkTreeNode _anchoredNodeEnd;

		// Token: 0x040004ED RID: 1261
		private Bitmap _nodeClosed;

		// Token: 0x040004EE RID: 1262
		private Bitmap _nodeClosedHover;

		// Token: 0x040004EF RID: 1263
		private Bitmap _nodeClosedHoverSelected;

		// Token: 0x040004F0 RID: 1264
		private Bitmap _nodeOpen;

		// Token: 0x040004F1 RID: 1265
		private Bitmap _nodeOpenHover;

		// Token: 0x040004F2 RID: 1266
		private Bitmap _nodeOpenHoverSelected;

		// Token: 0x040004F3 RID: 1267
		private DarkTreeNode _provisionalNode;

		// Token: 0x040004F4 RID: 1268
		private DarkTreeNode _dropNode;

		// Token: 0x040004F5 RID: 1269
		private bool _provisionalDragging;

		// Token: 0x040004F6 RID: 1270
		private List<DarkTreeNode> _dragNodes;

		// Token: 0x040004F7 RID: 1271
		private Point _dragPos;
	}
}
