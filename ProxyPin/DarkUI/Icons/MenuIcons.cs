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
	// Token: 0x0200007D RID: 125
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	public class MenuIcons
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x00031620 File Offset: 0x00031620
		internal MenuIcons()
		{
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00031628 File Offset: 0x00031628
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (MenuIcons.resourceMan == null)
				{
					MenuIcons.resourceMan = new ResourceManager("DarkUI.Icons.MenuIcons", typeof(MenuIcons).Assembly);
				}
				return MenuIcons.resourceMan;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00031658 File Offset: 0x00031658
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00031660 File Offset: 0x00031660
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return MenuIcons.resourceCulture;
			}
			set
			{
				MenuIcons.resourceCulture = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00031668 File Offset: 0x00031668
		public static Bitmap grip
		{
			get
			{
				return (Bitmap)MenuIcons.ResourceManager.GetObject("grip", MenuIcons.resourceCulture);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00031684 File Offset: 0x00031684
		public static Bitmap tick
		{
			get
			{
				return (Bitmap)MenuIcons.ResourceManager.GetObject("tick", MenuIcons.resourceCulture);
			}
		}

		// Token: 0x04000431 RID: 1073
		private static ResourceManager resourceMan;

		// Token: 0x04000432 RID: 1074
		private static CultureInfo resourceCulture;
	}
}
