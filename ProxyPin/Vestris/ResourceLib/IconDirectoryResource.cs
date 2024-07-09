using System;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D1D RID: 3357
	[ComVisible(true)]
	public class IconDirectoryResource : DirectoryResource<IconResource>
	{
		// Token: 0x06008890 RID: 34960 RVA: 0x002928B4 File Offset: 0x002928B4
		internal IconDirectoryResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x06008891 RID: 34961 RVA: 0x002928C8 File Offset: 0x002928C8
		public IconDirectoryResource() : base(Kernel32.ResourceTypes.RT_GROUP_ICON)
		{
		}

		// Token: 0x06008892 RID: 34962 RVA: 0x002928D4 File Offset: 0x002928D4
		public IconDirectoryResource(IconFile iconFile) : base(Kernel32.ResourceTypes.RT_GROUP_ICON)
		{
			for (int i = 0; i < iconFile.Icons.Count; i++)
			{
				IconResource item = new IconResource(iconFile.Icons[i], new ResourceId((uint)(i + 1)), this._language);
				base.Icons.Add(item);
			}
		}
	}
}
