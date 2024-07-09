using System;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D0B RID: 3339
	[ComVisible(true)]
	public class CursorDirectoryResource : DirectoryResource<CursorResource>
	{
		// Token: 0x060087A7 RID: 34727 RVA: 0x0028FFC4 File Offset: 0x0028FFC4
		internal CursorDirectoryResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x060087A8 RID: 34728 RVA: 0x0028FFD8 File Offset: 0x0028FFD8
		public CursorDirectoryResource() : base(Kernel32.ResourceTypes.RT_GROUP_CURSOR)
		{
		}

		// Token: 0x060087A9 RID: 34729 RVA: 0x0028FFE4 File Offset: 0x0028FFE4
		public CursorDirectoryResource(IconFile iconFile) : base(Kernel32.ResourceTypes.RT_GROUP_CURSOR)
		{
			ushort num = 0;
			while ((int)num < iconFile.Icons.Count)
			{
				CursorResource cursorResource = new CursorResource(iconFile.Icons[(int)num], new ResourceId((uint)num), this._language);
				cursorResource.HotspotX = iconFile.Icons[(int)num].Header.wPlanes;
				cursorResource.HotspotY = iconFile.Icons[(int)num].Header.wBitsPerPixel;
				base.Icons.Add(cursorResource);
				num += 1;
			}
		}
	}
}
