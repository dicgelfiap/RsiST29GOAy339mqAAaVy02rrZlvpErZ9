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
	// Token: 0x02000074 RID: 116
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class DropdownIcons
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x0002FFD8 File Offset: 0x0002FFD8
		internal DropdownIcons()
		{
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0002FFE0 File Offset: 0x0002FFE0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (DropdownIcons.resourceMan == null)
				{
					DropdownIcons.resourceMan = new ResourceManager("DarkUI.Icons.DropdownIcons", typeof(DropdownIcons).Assembly);
				}
				return DropdownIcons.resourceMan;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00030010 File Offset: 0x00030010
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x00030018 File Offset: 0x00030018
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return DropdownIcons.resourceCulture;
			}
			set
			{
				DropdownIcons.resourceCulture = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00030020 File Offset: 0x00030020
		internal static Bitmap small_arrow
		{
			get
			{
				return (Bitmap)DropdownIcons.ResourceManager.GetObject("small_arrow", DropdownIcons.resourceCulture);
			}
		}

		// Token: 0x0400032C RID: 812
		private static ResourceManager resourceMan;

		// Token: 0x0400032D RID: 813
		private static CultureInfo resourceCulture;
	}
}
