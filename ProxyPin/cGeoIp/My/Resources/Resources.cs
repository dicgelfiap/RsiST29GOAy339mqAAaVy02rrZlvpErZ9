using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace cGeoIp.My.Resources
{
	// Token: 0x0200006E RID: 110
	[StandardModule]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[HideModuleName]
	internal sealed class Resources
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0002C38C File Offset: 0x0002C38C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					Resources.resourceMan = new ResourceManager("cGeoIp.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0002C3C4 File Offset: 0x0002C3C4
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0002C3CC File Offset: 0x0002C3CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0002C3D4 File Offset: 0x0002C3D4
		internal static byte[] GeoIP
		{
			get
			{
				return (byte[])RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("GeoIP", Resources.resourceCulture));
			}
		}

		// Token: 0x0400031E RID: 798
		private static ResourceManager resourceMan;

		// Token: 0x0400031F RID: 799
		private static CultureInfo resourceCulture;
	}
}
