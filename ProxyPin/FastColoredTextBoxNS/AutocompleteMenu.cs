using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
	// Token: 0x020009F8 RID: 2552
	[Browsable(false)]
	public class AutocompleteMenu : ToolStripDropDown, IDisposable
	{
		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x060061D4 RID: 25044 RVA: 0x001D339C File Offset: 0x001D339C
		// (set) Token: 0x060061D5 RID: 25045 RVA: 0x001D33A4 File Offset: 0x001D33A4
		public Range Fragment { get; internal set; }

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x060061D6 RID: 25046 RVA: 0x001D33B0 File Offset: 0x001D33B0
		// (set) Token: 0x060061D7 RID: 25047 RVA: 0x001D33B8 File Offset: 0x001D33B8
		public string SearchPattern { get; set; }

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x060061D8 RID: 25048 RVA: 0x001D33C4 File Offset: 0x001D33C4
		// (set) Token: 0x060061D9 RID: 25049 RVA: 0x001D33CC File Offset: 0x001D33CC
		public int MinFragmentLength { get; set; }

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060061DA RID: 25050 RVA: 0x001D33D8 File Offset: 0x001D33D8
		// (remove) Token: 0x060061DB RID: 25051 RVA: 0x001D3414 File Offset: 0x001D3414
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<SelectingEventArgs> Selecting;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060061DC RID: 25052 RVA: 0x001D3450 File Offset: 0x001D3450
		// (remove) Token: 0x060061DD RID: 25053 RVA: 0x001D348C File Offset: 0x001D348C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<SelectedEventArgs> Selected;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060061DE RID: 25054 RVA: 0x001D34C8 File Offset: 0x001D34C8
		// (remove) Token: 0x060061DF RID: 25055 RVA: 0x001D3504 File Offset: 0x001D3504
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public new event EventHandler<CancelEventArgs> Opening;

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x060061E0 RID: 25056 RVA: 0x001D3540 File Offset: 0x001D3540
		// (set) Token: 0x060061E1 RID: 25057 RVA: 0x001D3564 File Offset: 0x001D3564
		public bool AllowTabKey
		{
			get
			{
				return this.listView.AllowTabKey;
			}
			set
			{
				this.listView.AllowTabKey = value;
			}
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x060061E2 RID: 25058 RVA: 0x001D3574 File Offset: 0x001D3574
		// (set) Token: 0x060061E3 RID: 25059 RVA: 0x001D3598 File Offset: 0x001D3598
		public int AppearInterval
		{
			get
			{
				return this.listView.AppearInterval;
			}
			set
			{
				this.listView.AppearInterval = value;
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x060061E4 RID: 25060 RVA: 0x001D35A8 File Offset: 0x001D35A8
		// (set) Token: 0x060061E5 RID: 25061 RVA: 0x001D35CC File Offset: 0x001D35CC
		public Size MaxTooltipSize
		{
			get
			{
				return this.listView.MaxToolTipSize;
			}
			set
			{
				this.listView.MaxToolTipSize = value;
			}
		}

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x060061E6 RID: 25062 RVA: 0x001D35DC File Offset: 0x001D35DC
		// (set) Token: 0x060061E7 RID: 25063 RVA: 0x001D3600 File Offset: 0x001D3600
		public bool AlwaysShowTooltip
		{
			get
			{
				return this.listView.AlwaysShowTooltip;
			}
			set
			{
				this.listView.AlwaysShowTooltip = value;
			}
		}

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x060061E8 RID: 25064 RVA: 0x001D3610 File Offset: 0x001D3610
		// (set) Token: 0x060061E9 RID: 25065 RVA: 0x001D3634 File Offset: 0x001D3634
		[DefaultValue(typeof(Color), "Orange")]
		public Color SelectedColor
		{
			get
			{
				return this.listView.SelectedColor;
			}
			set
			{
				this.listView.SelectedColor = value;
			}
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x060061EA RID: 25066 RVA: 0x001D3644 File Offset: 0x001D3644
		// (set) Token: 0x060061EB RID: 25067 RVA: 0x001D3668 File Offset: 0x001D3668
		[DefaultValue(typeof(Color), "Red")]
		public Color HoveredColor
		{
			get
			{
				return this.listView.HoveredColor;
			}
			set
			{
				this.listView.HoveredColor = value;
			}
		}

		// Token: 0x060061EC RID: 25068 RVA: 0x001D3678 File Offset: 0x001D3678
		public AutocompleteMenu(FastColoredTextBox tb)
		{
			base.AutoClose = false;
			this.AutoSize = false;
			base.Margin = Padding.Empty;
			base.Padding = Padding.Empty;
			base.BackColor = Color.White;
			this.listView = new AutocompleteListView(tb);
			this.host = new ToolStripControlHost(this.listView);
			this.host.Margin = new Padding(2, 2, 2, 2);
			this.host.Padding = Padding.Empty;
			this.host.AutoSize = false;
			this.host.AutoToolTip = false;
			this.CalcSize();
			base.Items.Add(this.host);
			this.listView.Parent = this;
			this.SearchPattern = "[\\w\\.]";
			this.MinFragmentLength = 2;
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x060061ED RID: 25069 RVA: 0x001D375C File Offset: 0x001D375C
		// (set) Token: 0x060061EE RID: 25070 RVA: 0x001D3780 File Offset: 0x001D3780
		public new Font Font
		{
			get
			{
				return this.listView.Font;
			}
			set
			{
				this.listView.Font = value;
			}
		}

		// Token: 0x060061EF RID: 25071 RVA: 0x001D3790 File Offset: 0x001D3790
		internal new void OnOpening(CancelEventArgs args)
		{
			bool flag = this.Opening != null;
			if (flag)
			{
				this.Opening(this, args);
			}
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x001D37C0 File Offset: 0x001D37C0
		public new void Close()
		{
			this.listView.toolTip.Hide(this.listView);
			base.Close();
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x001D37E4 File Offset: 0x001D37E4
		internal void CalcSize()
		{
			this.host.Size = this.listView.Size;
			base.Size = new Size(this.listView.Size.Width + 4, this.listView.Size.Height + 4);
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x001D3844 File Offset: 0x001D3844
		public virtual void OnSelecting()
		{
			this.listView.OnSelecting();
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x001D3854 File Offset: 0x001D3854
		public void SelectNext(int shift)
		{
			this.listView.SelectNext(shift);
		}

		// Token: 0x060061F4 RID: 25076 RVA: 0x001D3864 File Offset: 0x001D3864
		internal void OnSelecting(SelectingEventArgs args)
		{
			bool flag = this.Selecting != null;
			if (flag)
			{
				this.Selecting(this, args);
			}
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x001D3894 File Offset: 0x001D3894
		public void OnSelected(SelectedEventArgs args)
		{
			bool flag = this.Selected != null;
			if (flag)
			{
				this.Selected(this, args);
			}
		}

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x060061F6 RID: 25078 RVA: 0x001D38C4 File Offset: 0x001D38C4
		public new AutocompleteListView Items
		{
			get
			{
				return this.listView;
			}
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x001D38E4 File Offset: 0x001D38E4
		public void Show(bool forced)
		{
			this.Items.DoAutocomplete(forced);
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x060061F8 RID: 25080 RVA: 0x001D38F4 File Offset: 0x001D38F4
		// (set) Token: 0x060061F9 RID: 25081 RVA: 0x001D3918 File Offset: 0x001D3918
		public new Size MinimumSize
		{
			get
			{
				return this.Items.MinimumSize;
			}
			set
			{
				this.Items.MinimumSize = value;
			}
		}

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x060061FA RID: 25082 RVA: 0x001D3928 File Offset: 0x001D3928
		// (set) Token: 0x060061FB RID: 25083 RVA: 0x001D394C File Offset: 0x001D394C
		public new ImageList ImageList
		{
			get
			{
				return this.Items.ImageList;
			}
			set
			{
				this.Items.ImageList = value;
			}
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x060061FC RID: 25084 RVA: 0x001D395C File Offset: 0x001D395C
		// (set) Token: 0x060061FD RID: 25085 RVA: 0x001D3980 File Offset: 0x001D3980
		public int ToolTipDuration
		{
			get
			{
				return this.Items.ToolTipDuration;
			}
			set
			{
				this.Items.ToolTipDuration = value;
			}
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x060061FE RID: 25086 RVA: 0x001D3990 File Offset: 0x001D3990
		// (set) Token: 0x060061FF RID: 25087 RVA: 0x001D39B4 File Offset: 0x001D39B4
		public ToolTip ToolTip
		{
			get
			{
				return this.Items.toolTip;
			}
			set
			{
				this.Items.toolTip = value;
			}
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x001D39C4 File Offset: 0x001D39C4
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			bool flag = this.listView != null && !this.listView.IsDisposed;
			if (flag)
			{
				this.listView.Dispose();
			}
		}

		// Token: 0x04003213 RID: 12819
		private AutocompleteListView listView;

		// Token: 0x04003214 RID: 12820
		public ToolStripControlHost host;
	}
}
