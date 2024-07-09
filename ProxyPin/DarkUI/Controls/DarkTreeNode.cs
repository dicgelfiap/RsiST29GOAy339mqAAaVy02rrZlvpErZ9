using System;
using System.Drawing;
using DarkUI.Collections;

namespace DarkUI.Controls
{
	// Token: 0x020000B5 RID: 181
	public class DarkTreeNode
	{
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600074F RID: 1871 RVA: 0x0003F19C File Offset: 0x0003F19C
		// (remove) Token: 0x06000750 RID: 1872 RVA: 0x0003F1D8 File Offset: 0x0003F1D8
		public event EventHandler<ObservableListModified<DarkTreeNode>> ItemsAdded;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000751 RID: 1873 RVA: 0x0003F214 File Offset: 0x0003F214
		// (remove) Token: 0x06000752 RID: 1874 RVA: 0x0003F250 File Offset: 0x0003F250
		public event EventHandler<ObservableListModified<DarkTreeNode>> ItemsRemoved;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000753 RID: 1875 RVA: 0x0003F28C File Offset: 0x0003F28C
		// (remove) Token: 0x06000754 RID: 1876 RVA: 0x0003F2C8 File Offset: 0x0003F2C8
		public event EventHandler TextChanged;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000755 RID: 1877 RVA: 0x0003F304 File Offset: 0x0003F304
		// (remove) Token: 0x06000756 RID: 1878 RVA: 0x0003F340 File Offset: 0x0003F340
		public event EventHandler NodeExpanded;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000757 RID: 1879 RVA: 0x0003F37C File Offset: 0x0003F37C
		// (remove) Token: 0x06000758 RID: 1880 RVA: 0x0003F3B8 File Offset: 0x0003F3B8
		public event EventHandler NodeCollapsed;

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0003F3F4 File Offset: 0x0003F3F4
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0003F3FC File Offset: 0x0003F3FC
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (this._text == value)
				{
					return;
				}
				this._text = value;
				this.OnTextChanged();
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0003F420 File Offset: 0x0003F420
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x0003F428 File Offset: 0x0003F428
		public Rectangle ExpandArea { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0003F434 File Offset: 0x0003F434
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0003F43C File Offset: 0x0003F43C
		public Rectangle IconArea { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0003F448 File Offset: 0x0003F448
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0003F450 File Offset: 0x0003F450
		public Rectangle TextArea { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0003F45C File Offset: 0x0003F45C
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0003F464 File Offset: 0x0003F464
		public Rectangle FullArea { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0003F470 File Offset: 0x0003F470
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0003F478 File Offset: 0x0003F478
		public bool ExpandAreaHot { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0003F484 File Offset: 0x0003F484
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0003F48C File Offset: 0x0003F48C
		public Bitmap Icon { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0003F498 File Offset: 0x0003F498
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0003F4A0 File Offset: 0x0003F4A0
		public Bitmap ExpandedIcon { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0003F4AC File Offset: 0x0003F4AC
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x0003F4B4 File Offset: 0x0003F4B4
		public bool Expanded
		{
			get
			{
				return this._expanded;
			}
			set
			{
				if (this._expanded == value)
				{
					return;
				}
				if (value && this.Nodes.Count == 0)
				{
					return;
				}
				this._expanded = value;
				if (this._expanded)
				{
					if (this.NodeExpanded != null)
					{
						this.NodeExpanded(this, null);
						return;
					}
				}
				else if (this.NodeCollapsed != null)
				{
					this.NodeCollapsed(this, null);
				}
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0003F52C File Offset: 0x0003F52C
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0003F534 File Offset: 0x0003F534
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
				}
				this._nodes = value;
				this._nodes.ItemsAdded += this.Nodes_ItemsAdded;
				this._nodes.ItemsRemoved += this.Nodes_ItemsRemoved;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0003F5B4 File Offset: 0x0003F5B4
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x0003F5BC File Offset: 0x0003F5BC
		public bool IsRoot
		{
			get
			{
				return this._isRoot;
			}
			set
			{
				this._isRoot = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0003F5C8 File Offset: 0x0003F5C8
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x0003F5D0 File Offset: 0x0003F5D0
		public DarkTreeView ParentTree
		{
			get
			{
				return this._parentTree;
			}
			set
			{
				if (this._parentTree == value)
				{
					return;
				}
				this._parentTree = value;
				foreach (DarkTreeNode darkTreeNode in this.Nodes)
				{
					darkTreeNode.ParentTree = this._parentTree;
				}
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0003F640 File Offset: 0x0003F640
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x0003F648 File Offset: 0x0003F648
		public DarkTreeNode ParentNode
		{
			get
			{
				return this._parentNode;
			}
			set
			{
				this._parentNode = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0003F654 File Offset: 0x0003F654
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x0003F65C File Offset: 0x0003F65C
		public bool Odd { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0003F668 File Offset: 0x0003F668
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x0003F670 File Offset: 0x0003F670
		public object NodeType { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0003F67C File Offset: 0x0003F67C
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x0003F684 File Offset: 0x0003F684
		public object Tag { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0003F690 File Offset: 0x0003F690
		public string FullPath
		{
			get
			{
				DarkTreeNode parentNode = this.ParentNode;
				string text = this.Text;
				while (parentNode != null)
				{
					text = string.Format("{0}{1}{2}", parentNode.Text, "\\", text);
					parentNode = parentNode.ParentNode;
				}
				return text;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0003F6D8 File Offset: 0x0003F6D8
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0003F6E0 File Offset: 0x0003F6E0
		public DarkTreeNode PrevVisibleNode { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0003F6EC File Offset: 0x0003F6EC
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0003F6F4 File Offset: 0x0003F6F4
		public DarkTreeNode NextVisibleNode { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0003F700 File Offset: 0x0003F700
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x0003F708 File Offset: 0x0003F708
		public int VisibleIndex { get; set; }

		// Token: 0x06000780 RID: 1920 RVA: 0x0003F714 File Offset: 0x0003F714
		public bool IsNodeAncestor(DarkTreeNode node)
		{
			for (DarkTreeNode parentNode = this.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				if (parentNode == node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0003F748 File Offset: 0x0003F748
		public DarkTreeNode()
		{
			this.Nodes = new ObservableList<DarkTreeNode>();
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0003F75C File Offset: 0x0003F75C
		public DarkTreeNode(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0003F76C File Offset: 0x0003F76C
		public void Remove()
		{
			if (this.ParentNode != null)
			{
				this.ParentNode.Nodes.Remove(this);
				return;
			}
			this.ParentTree.Nodes.Remove(this);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0003F7AC File Offset: 0x0003F7AC
		public void EnsureVisible()
		{
			for (DarkTreeNode parentNode = this.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				parentNode.Expanded = true;
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0003F7DC File Offset: 0x0003F7DC
		private void OnTextChanged()
		{
			if (this.ParentTree != null && this.ParentTree.TreeViewNodeSorter != null)
			{
				if (this.ParentNode != null)
				{
					this.ParentNode.Nodes.Sort(this.ParentTree.TreeViewNodeSorter);
				}
				else
				{
					this.ParentTree.Nodes.Sort(this.ParentTree.TreeViewNodeSorter);
				}
			}
			if (this.TextChanged != null)
			{
				this.TextChanged(this, null);
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0003F868 File Offset: 0x0003F868
		private void Nodes_ItemsAdded(object sender, ObservableListModified<DarkTreeNode> e)
		{
			foreach (DarkTreeNode darkTreeNode in e.Items)
			{
				darkTreeNode.ParentNode = this;
				darkTreeNode.ParentTree = this.ParentTree;
			}
			if (this.ParentTree != null && this.ParentTree.TreeViewNodeSorter != null)
			{
				this.Nodes.Sort(this.ParentTree.TreeViewNodeSorter);
			}
			if (this.ItemsAdded != null)
			{
				this.ItemsAdded(this, e);
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0003F914 File Offset: 0x0003F914
		private void Nodes_ItemsRemoved(object sender, ObservableListModified<DarkTreeNode> e)
		{
			if (this.Nodes.Count == 0)
			{
				this.Expanded = false;
			}
			if (this.ItemsRemoved != null)
			{
				this.ItemsRemoved(this, e);
			}
		}

		// Token: 0x0400053C RID: 1340
		private string _text;

		// Token: 0x0400053D RID: 1341
		private bool _isRoot;

		// Token: 0x0400053E RID: 1342
		private DarkTreeView _parentTree;

		// Token: 0x0400053F RID: 1343
		private DarkTreeNode _parentNode;

		// Token: 0x04000540 RID: 1344
		private ObservableList<DarkTreeNode> _nodes;

		// Token: 0x04000541 RID: 1345
		private bool _expanded;
	}
}
