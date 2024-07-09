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
	// Token: 0x02000075 RID: 117
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class ScrollIcons
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x0003003C File Offset: 0x0003003C
		internal ScrollIcons()
		{
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00030044 File Offset: 0x00030044
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (ScrollIcons.resourceMan == null)
				{
					ScrollIcons.resourceMan = new ResourceManager("DarkUI.Icons.ScrollIcons", typeof(ScrollIcons).Assembly);
				}
				return ScrollIcons.resourceMan;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00030074 File Offset: 0x00030074
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0003007C File Offset: 0x0003007C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return ScrollIcons.resourceCulture;
			}
			set
			{
				ScrollIcons.resourceCulture = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00030084 File Offset: 0x00030084
		internal static Bitmap scrollbar_arrow
		{
			get
			{
				return (Bitmap)ScrollIcons.ResourceManager.GetObject("scrollbar_arrow", ScrollIcons.resourceCulture);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x000300A0 File Offset: 0x000300A0
		internal static Bitmap scrollbar_arrow_clicked
		{
			get
			{
				return (Bitmap)ScrollIcons.ResourceManager.GetObject("scrollbar_arrow_clicked", ScrollIcons.resourceCulture);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x000300BC File Offset: 0x000300BC
		internal static Bitmap scrollbar_arrow_disabled
		{
			get
			{
				return (Bitmap)ScrollIcons.ResourceManager.GetObject("scrollbar_arrow_disabled", ScrollIcons.resourceCulture);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x000300D8 File Offset: 0x000300D8
		internal static Bitmap scrollbar_arrow_hot
		{
			get
			{
				return (Bitmap)ScrollIcons.ResourceManager.GetObject("scrollbar_arrow_hot", ScrollIcons.resourceCulture);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x000300F4 File Offset: 0x000300F4
		internal static Bitmap scrollbar_arrow_small_clicked
		{
			get
			{
				return (Bitmap)ScrollIcons.ResourceManager.GetObject("scrollbar_arrow_small_clicked", ScrollIcons.resourceCulture);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00030110 File Offset: 0x00030110
		internal static Bitmap scrollbar_arrow_small_hot
		{
			get
			{
				return (Bitmap)ScrollIcons.ResourceManager.GetObject("scrollbar_arrow_small_hot", ScrollIcons.resourceCulture);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0003012C File Offset: 0x0003012C
		internal static Bitmap scrollbar_arrow_small_standard
		{
			get
			{
				return (Bitmap)ScrollIcons.ResourceManager.GetObject("scrollbar_arrow_small_standard", ScrollIcons.resourceCulture);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00030148 File Offset: 0x00030148
		internal static Bitmap scrollbar_arrow_standard
		{
			get
			{
				return (Bitmap)ScrollIcons.ResourceManager.GetObject("scrollbar_arrow_standard", ScrollIcons.resourceCulture);
			}
		}

		// Token: 0x0400032E RID: 814
		private static ResourceManager resourceMan;

		// Token: 0x0400032F RID: 815
		private static CultureInfo resourceCulture;
	}
}
