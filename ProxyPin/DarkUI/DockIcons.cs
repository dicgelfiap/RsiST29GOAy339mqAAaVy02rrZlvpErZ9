using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DarkUI
{
	// Token: 0x02000073 RID: 115
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class DockIcons
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x0002FE78 File Offset: 0x0002FE78
		internal DockIcons()
		{
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0002FE80 File Offset: 0x0002FE80
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (DockIcons.resourceMan == null)
				{
					DockIcons.resourceMan = new ResourceManager("DarkUI.Icons.DockIcons", typeof(DockIcons).Assembly);
				}
				return DockIcons.resourceMan;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0002FEB0 File Offset: 0x0002FEB0
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0002FEB8 File Offset: 0x0002FEB8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return DockIcons.resourceCulture;
			}
			set
			{
				DockIcons.resourceCulture = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0002FEC0 File Offset: 0x0002FEC0
		internal static Bitmap active_inactive_close
		{
			get
			{
				return (Bitmap)DockIcons.ResourceManager.GetObject("active_inactive_close", DockIcons.resourceCulture);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0002FEDC File Offset: 0x0002FEDC
		internal static Bitmap arrow
		{
			get
			{
				return (Bitmap)DockIcons.ResourceManager.GetObject("arrow", DockIcons.resourceCulture);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0002FEF8 File Offset: 0x0002FEF8
		internal static Bitmap close
		{
			get
			{
				return (Bitmap)DockIcons.ResourceManager.GetObject("close", DockIcons.resourceCulture);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0002FF14 File Offset: 0x0002FF14
		internal static Bitmap close_selected
		{
			get
			{
				return (Bitmap)DockIcons.ResourceManager.GetObject("close_selected", DockIcons.resourceCulture);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0002FF30 File Offset: 0x0002FF30
		internal static Bitmap inactive_close
		{
			get
			{
				return (Bitmap)DockIcons.ResourceManager.GetObject("inactive_close", DockIcons.resourceCulture);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0002FF4C File Offset: 0x0002FF4C
		internal static Bitmap inactive_close_selected
		{
			get
			{
				return (Bitmap)DockIcons.ResourceManager.GetObject("inactive_close_selected", DockIcons.resourceCulture);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0002FF68 File Offset: 0x0002FF68
		internal static Bitmap tw_active_close
		{
			get
			{
				return (Bitmap)DockIcons.ResourceManager.GetObject("tw_active_close", DockIcons.resourceCulture);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0002FF84 File Offset: 0x0002FF84
		internal static Bitmap tw_active_close_selected
		{
			get
			{
				return (Bitmap)DockIcons.ResourceManager.GetObject("tw_active_close_selected", DockIcons.resourceCulture);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0002FFA0 File Offset: 0x0002FFA0
		internal static Bitmap tw_close
		{
			get
			{
				return (Bitmap)DockIcons.ResourceManager.GetObject("tw_close", DockIcons.resourceCulture);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0002FFBC File Offset: 0x0002FFBC
		internal static Bitmap tw_close_selected
		{
			get
			{
				return (Bitmap)DockIcons.ResourceManager.GetObject("tw_close_selected", DockIcons.resourceCulture);
			}
		}

		// Token: 0x0400032A RID: 810
		private static ResourceManager resourceMan;

		// Token: 0x0400032B RID: 811
		private static CultureInfo resourceCulture;
	}
}
