using System;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D21 RID: 3361
	[ComVisible(true)]
	public class IconResource : IconImageResource
	{
		// Token: 0x060088B9 RID: 35001 RVA: 0x00293064 File Offset: 0x00293064
		internal IconResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x060088BA RID: 35002 RVA: 0x00293078 File Offset: 0x00293078
		public IconResource() : base(new ResourceId(Kernel32.ResourceTypes.RT_ICON))
		{
		}

		// Token: 0x060088BB RID: 35003 RVA: 0x00293088 File Offset: 0x00293088
		public IconResource(IconFileIcon icon, ResourceId id, ushort language) : base(icon, new ResourceId(Kernel32.ResourceTypes.RT_ICON), id, language)
		{
		}
	}
}
