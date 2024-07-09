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
	// Token: 0x0200007E RID: 126
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class MessageBoxIcons
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x000316A0 File Offset: 0x000316A0
		internal MessageBoxIcons()
		{
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000316A8 File Offset: 0x000316A8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (MessageBoxIcons.resourceMan == null)
				{
					MessageBoxIcons.resourceMan = new ResourceManager("DarkUI.Icons.MessageBoxIcons", typeof(MessageBoxIcons).Assembly);
				}
				return MessageBoxIcons.resourceMan;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000316D8 File Offset: 0x000316D8
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x000316E0 File Offset: 0x000316E0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return MessageBoxIcons.resourceCulture;
			}
			set
			{
				MessageBoxIcons.resourceCulture = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000316E8 File Offset: 0x000316E8
		internal static Bitmap error
		{
			get
			{
				return (Bitmap)MessageBoxIcons.ResourceManager.GetObject("error", MessageBoxIcons.resourceCulture);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00031704 File Offset: 0x00031704
		internal static Bitmap info
		{
			get
			{
				return (Bitmap)MessageBoxIcons.ResourceManager.GetObject("info", MessageBoxIcons.resourceCulture);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00031720 File Offset: 0x00031720
		internal static Bitmap warning
		{
			get
			{
				return (Bitmap)MessageBoxIcons.ResourceManager.GetObject("warning", MessageBoxIcons.resourceCulture);
			}
		}

		// Token: 0x04000433 RID: 1075
		private static ResourceManager resourceMan;

		// Token: 0x04000434 RID: 1076
		private static CultureInfo resourceCulture;
	}
}
