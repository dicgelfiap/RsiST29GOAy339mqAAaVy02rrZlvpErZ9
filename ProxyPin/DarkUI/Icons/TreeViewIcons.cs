using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DarkUI.Icons
{
	// Token: 0x0200007F RID: 127
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class TreeViewIcons
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x0003173C File Offset: 0x0003173C
		internal TreeViewIcons()
		{
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00031744 File Offset: 0x00031744
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (TreeViewIcons.resourceMan == null)
				{
					TreeViewIcons.resourceMan = new ResourceManager("DarkUI.Icons.TreeViewIcons", typeof(TreeViewIcons).Assembly);
				}
				return TreeViewIcons.resourceMan;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00031774 File Offset: 0x00031774
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0003177C File Offset: 0x0003177C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return TreeViewIcons.resourceCulture;
			}
			set
			{
				TreeViewIcons.resourceCulture = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00031784 File Offset: 0x00031784
		internal static Bitmap node_closed_empty
		{
			get
			{
				return (Bitmap)TreeViewIcons.ResourceManager.GetObject("node_closed_empty", TreeViewIcons.resourceCulture);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x000317A0 File Offset: 0x000317A0
		internal static Bitmap node_closed_full
		{
			get
			{
				return (Bitmap)TreeViewIcons.ResourceManager.GetObject("node_closed_full", TreeViewIcons.resourceCulture);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x000317BC File Offset: 0x000317BC
		internal static Bitmap node_open
		{
			get
			{
				return (Bitmap)TreeViewIcons.ResourceManager.GetObject("node_open", TreeViewIcons.resourceCulture);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x000317D8 File Offset: 0x000317D8
		internal static Bitmap node_open_empty
		{
			get
			{
				return (Bitmap)TreeViewIcons.ResourceManager.GetObject("node_open_empty", TreeViewIcons.resourceCulture);
			}
		}

		// Token: 0x04000435 RID: 1077
		private static ResourceManager resourceMan;

		// Token: 0x04000436 RID: 1078
		private static CultureInfo resourceCulture;
	}
}
