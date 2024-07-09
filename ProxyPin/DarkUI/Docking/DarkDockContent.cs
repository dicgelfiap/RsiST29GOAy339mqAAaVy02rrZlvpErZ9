using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DarkUI.Docking
{
	// Token: 0x02000089 RID: 137
	[ToolboxItem(false)]
	public class DarkDockContent : UserControl
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060004EF RID: 1263 RVA: 0x000329A8 File Offset: 0x000329A8
		// (remove) Token: 0x060004F0 RID: 1264 RVA: 0x000329E4 File Offset: 0x000329E4
		public event EventHandler DockTextChanged;

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00032A20 File Offset: 0x00032A20
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00032A28 File Offset: 0x00032A28
		[Category("Appearance")]
		[Description("Determines the text that will appear in the content tabs and headers.")]
		public string DockText
		{
			get
			{
				return this._dockText;
			}
			set
			{
				string dockText = this._dockText;
				this._dockText = value;
				if (this.DockTextChanged != null)
				{
					this.DockTextChanged(this, null);
				}
				base.Invalidate();
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00032A58 File Offset: 0x00032A58
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00032A60 File Offset: 0x00032A60
		[Category("Appearance")]
		[Description("Determines the icon that will appear in the content tabs and headers.")]
		public Image Icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
				base.Invalidate();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00032A70 File Offset: 0x00032A70
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00032A78 File Offset: 0x00032A78
		[Category("Layout")]
		[Description("Determines the default area of the dock panel this content will be added to.")]
		[DefaultValue(DarkDockArea.Document)]
		public DarkDockArea DefaultDockArea { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00032A84 File Offset: 0x00032A84
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00032A8C File Offset: 0x00032A8C
		[Category("Behavior")]
		[Description("Determines the key used by this content in the dock serialization.")]
		public string SerializationKey { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00032A98 File Offset: 0x00032A98
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x00032AA0 File Offset: 0x00032AA0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DarkDockPanel DockPanel { get; internal set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00032AAC File Offset: 0x00032AAC
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x00032AB4 File Offset: 0x00032AB4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DarkDockRegion DockRegion { get; internal set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00032AC0 File Offset: 0x00032AC0
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00032AC8 File Offset: 0x00032AC8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DarkDockGroup DockGroup { get; internal set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00032AD4 File Offset: 0x00032AD4
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00032ADC File Offset: 0x00032ADC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DarkDockArea DockArea { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00032AE8 File Offset: 0x00032AE8
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x00032AF0 File Offset: 0x00032AF0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Order { get; set; }

		// Token: 0x06000504 RID: 1284 RVA: 0x00032B04 File Offset: 0x00032B04
		public virtual void Close()
		{
			if (this.DockPanel != null)
			{
				this.DockPanel.RemoveContent(this);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00032B20 File Offset: 0x00032B20
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (this.DockPanel == null)
			{
				return;
			}
			this.DockPanel.ActiveContent = this;
		}

		// Token: 0x0400045F RID: 1119
		private string _dockText;

		// Token: 0x04000460 RID: 1120
		private Image _icon;
	}
}
