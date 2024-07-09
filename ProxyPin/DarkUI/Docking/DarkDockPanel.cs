using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Config;
using DarkUI.Win32;

namespace DarkUI.Docking
{
	// Token: 0x0200008B RID: 139
	public class DarkDockPanel : UserControl
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600052A RID: 1322 RVA: 0x00034710 File Offset: 0x00034710
		// (remove) Token: 0x0600052B RID: 1323 RVA: 0x0003474C File Offset: 0x0003474C
		public event EventHandler<DockContentEventArgs> ActiveContentChanged;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600052C RID: 1324 RVA: 0x00034788 File Offset: 0x00034788
		// (remove) Token: 0x0600052D RID: 1325 RVA: 0x000347C4 File Offset: 0x000347C4
		public event EventHandler<DockContentEventArgs> ContentAdded;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600052E RID: 1326 RVA: 0x00034800 File Offset: 0x00034800
		// (remove) Token: 0x0600052F RID: 1327 RVA: 0x0003483C File Offset: 0x0003483C
		public event EventHandler<DockContentEventArgs> ContentRemoved;

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00034878 File Offset: 0x00034878
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x00034880 File Offset: 0x00034880
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DarkDockContent ActiveContent
		{
			get
			{
				return this._activeContent;
			}
			set
			{
				if (this._switchingContent)
				{
					return;
				}
				this._switchingContent = true;
				this._activeContent = value;
				this.ActiveGroup = this._activeContent.DockGroup;
				this.ActiveRegion = this.ActiveGroup.DockRegion;
				foreach (DarkDockRegion darkDockRegion in this._regions.Values)
				{
					darkDockRegion.Redraw();
				}
				if (this.ActiveContentChanged != null)
				{
					this.ActiveContentChanged(this, new DockContentEventArgs(this._activeContent));
				}
				this._switchingContent = false;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00034940 File Offset: 0x00034940
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00034948 File Offset: 0x00034948
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DarkDockRegion ActiveRegion { get; internal set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00034954 File Offset: 0x00034954
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0003495C File Offset: 0x0003495C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DarkDockGroup ActiveGroup { get; internal set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00034968 File Offset: 0x00034968
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DarkDockContent ActiveDocument
		{
			get
			{
				return this._regions[DarkDockArea.Document].ActiveDocument;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0003497C File Offset: 0x0003497C
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00034984 File Offset: 0x00034984
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DockContentDragFilter DockContentDragFilter { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00034990 File Offset: 0x00034990
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00034998 File Offset: 0x00034998
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DockResizeFilter DockResizeFilter { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x000349A4 File Offset: 0x000349A4
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x000349AC File Offset: 0x000349AC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<DarkDockSplitter> Splitters { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x000349B8 File Offset: 0x000349B8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MouseButtons MouseButtonState
		{
			get
			{
				return Control.MouseButtons;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x000349C0 File Offset: 0x000349C0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Dictionary<DarkDockArea, DarkDockRegion> Regions
		{
			get
			{
				return this._regions;
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000349C8 File Offset: 0x000349C8
		public DarkDockPanel()
		{
			this.Splitters = new List<DarkDockSplitter>();
			this.DockContentDragFilter = new DockContentDragFilter(this);
			this.DockResizeFilter = new DockResizeFilter(this);
			this._regions = new Dictionary<DarkDockArea, DarkDockRegion>();
			this._contents = new List<DarkDockContent>();
			this.BackColor = Colors.GreyBackground;
			this.CreateRegions();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00034A2C File Offset: 0x00034A2C
		public void AddContent(DarkDockContent dockContent)
		{
			this.AddContent(dockContent, null);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00034A38 File Offset: 0x00034A38
		public void AddContent(DarkDockContent dockContent, DarkDockGroup dockGroup)
		{
			if (this._contents.Contains(dockContent))
			{
				this.RemoveContent(dockContent);
			}
			dockContent.DockPanel = this;
			this._contents.Add(dockContent);
			if (dockGroup != null)
			{
				dockContent.DockArea = dockGroup.DockArea;
			}
			if (dockContent.DockArea == DarkDockArea.None)
			{
				dockContent.DockArea = dockContent.DefaultDockArea;
			}
			this._regions[dockContent.DockArea].AddContent(dockContent, dockGroup);
			if (this.ContentAdded != null)
			{
				this.ContentAdded(this, new DockContentEventArgs(dockContent));
			}
			dockContent.Select();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00034AD8 File Offset: 0x00034AD8
		public void InsertContent(DarkDockContent dockContent, DarkDockGroup dockGroup, DockInsertType insertType)
		{
			if (this._contents.Contains(dockContent))
			{
				this.RemoveContent(dockContent);
			}
			dockContent.DockPanel = this;
			this._contents.Add(dockContent);
			dockContent.DockArea = dockGroup.DockArea;
			this._regions[dockGroup.DockArea].InsertContent(dockContent, dockGroup, insertType);
			if (this.ContentAdded != null)
			{
				this.ContentAdded(this, new DockContentEventArgs(dockContent));
			}
			dockContent.Select();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00034B5C File Offset: 0x00034B5C
		public void RemoveContent(DarkDockContent dockContent)
		{
			if (!this._contents.Contains(dockContent))
			{
				return;
			}
			dockContent.DockPanel = null;
			this._contents.Remove(dockContent);
			this._regions[dockContent.DockArea].RemoveContent(dockContent);
			if (this.ContentRemoved != null)
			{
				this.ContentRemoved(this, new DockContentEventArgs(dockContent));
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00034BC8 File Offset: 0x00034BC8
		public bool ContainsContent(DarkDockContent dockContent)
		{
			return this._contents.Contains(dockContent);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00034BD8 File Offset: 0x00034BD8
		public List<DarkDockContent> GetDocuments()
		{
			return this._regions[DarkDockArea.Document].GetContents();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00034BEC File Offset: 0x00034BEC
		private void CreateRegions()
		{
			DarkDockRegion darkDockRegion = new DarkDockRegion(this, DarkDockArea.Document);
			this._regions.Add(DarkDockArea.Document, darkDockRegion);
			DarkDockRegion darkDockRegion2 = new DarkDockRegion(this, DarkDockArea.Left);
			this._regions.Add(DarkDockArea.Left, darkDockRegion2);
			DarkDockRegion darkDockRegion3 = new DarkDockRegion(this, DarkDockArea.Right);
			this._regions.Add(DarkDockArea.Right, darkDockRegion3);
			DarkDockRegion darkDockRegion4 = new DarkDockRegion(this, DarkDockArea.Bottom);
			this._regions.Add(DarkDockArea.Bottom, darkDockRegion4);
			base.Controls.Add(darkDockRegion);
			base.Controls.Add(darkDockRegion4);
			base.Controls.Add(darkDockRegion2);
			base.Controls.Add(darkDockRegion3);
			darkDockRegion.TabIndex = 0;
			darkDockRegion3.TabIndex = 1;
			darkDockRegion4.TabIndex = 2;
			darkDockRegion2.TabIndex = 3;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00034CA0 File Offset: 0x00034CA0
		public void DragContent(DarkDockContent content)
		{
			this.DockContentDragFilter.StartDrag(content);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00034CB0 File Offset: 0x00034CB0
		public DockPanelState GetDockPanelState()
		{
			DockPanelState dockPanelState = new DockPanelState();
			dockPanelState.Regions.Add(new DockRegionState(DarkDockArea.Document));
			dockPanelState.Regions.Add(new DockRegionState(DarkDockArea.Left, this._regions[DarkDockArea.Left].Size));
			dockPanelState.Regions.Add(new DockRegionState(DarkDockArea.Right, this._regions[DarkDockArea.Right].Size));
			dockPanelState.Regions.Add(new DockRegionState(DarkDockArea.Bottom, this._regions[DarkDockArea.Bottom].Size));
			Dictionary<DarkDockGroup, DockGroupState> dictionary = new Dictionary<DarkDockGroup, DockGroupState>();
			foreach (DarkDockContent darkDockContent in from c in this._contents
			orderby c.Order
			select c)
			{
				foreach (DockRegionState dockRegionState in dockPanelState.Regions)
				{
					if (dockRegionState.Area == darkDockContent.DockArea)
					{
						DockGroupState dockGroupState;
						if (dictionary.ContainsKey(darkDockContent.DockGroup))
						{
							dockGroupState = dictionary[darkDockContent.DockGroup];
						}
						else
						{
							dockGroupState = new DockGroupState();
							dockRegionState.Groups.Add(dockGroupState);
							dictionary.Add(darkDockContent.DockGroup, dockGroupState);
						}
						dockGroupState.Contents.Add(darkDockContent.SerializationKey);
						dockGroupState.VisibleContent = darkDockContent.DockGroup.VisibleContent.SerializationKey;
					}
				}
			}
			return dockPanelState;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00034E78 File Offset: 0x00034E78
		public void RestoreDockPanelState(DockPanelState state, Func<string, DarkDockContent> getContentBySerializationKey)
		{
			foreach (DockRegionState dockRegionState in state.Regions)
			{
				switch (dockRegionState.Area)
				{
				case DarkDockArea.Left:
					this._regions[DarkDockArea.Left].Size = dockRegionState.Size;
					break;
				case DarkDockArea.Right:
					this._regions[DarkDockArea.Right].Size = dockRegionState.Size;
					break;
				case DarkDockArea.Bottom:
					this._regions[DarkDockArea.Bottom].Size = dockRegionState.Size;
					break;
				}
				foreach (DockGroupState dockGroupState in dockRegionState.Groups)
				{
					DarkDockContent darkDockContent = null;
					DarkDockContent darkDockContent2 = null;
					foreach (string text in dockGroupState.Contents)
					{
						DarkDockContent darkDockContent3 = getContentBySerializationKey(text);
						if (darkDockContent3 != null)
						{
							darkDockContent3.DockArea = dockRegionState.Area;
							if (darkDockContent == null)
							{
								this.AddContent(darkDockContent3);
							}
							else
							{
								this.AddContent(darkDockContent3, darkDockContent.DockGroup);
							}
							darkDockContent = darkDockContent3;
							if (dockGroupState.VisibleContent == text)
							{
								darkDockContent2 = darkDockContent3;
							}
						}
					}
					if (darkDockContent2 != null)
					{
						darkDockContent2.Select();
					}
				}
			}
		}

		// Token: 0x04000474 RID: 1140
		private List<DarkDockContent> _contents;

		// Token: 0x04000475 RID: 1141
		private Dictionary<DarkDockArea, DarkDockRegion> _regions;

		// Token: 0x04000476 RID: 1142
		private DarkDockContent _activeContent;

		// Token: 0x04000477 RID: 1143
		private bool _switchingContent;
	}
}
