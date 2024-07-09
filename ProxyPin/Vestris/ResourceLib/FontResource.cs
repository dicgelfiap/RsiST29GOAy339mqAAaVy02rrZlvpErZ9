using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D1A RID: 3354
	[ComVisible(true)]
	public class FontResource : GenericResource
	{
		// Token: 0x0600887B RID: 34939 RVA: 0x002927B0 File Offset: 0x002927B0
		public FontResource() : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(Kernel32.ResourceTypes.RT_FONT), null, ResourceUtil.NEUTRALLANGID, 0)
		{
		}

		// Token: 0x0600887C RID: 34940 RVA: 0x002927D0 File Offset: 0x002927D0
		public FontResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x0600887D RID: 34941 RVA: 0x002927E4 File Offset: 0x002927E4
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			return base.Read(hModule, lpRes);
		}

		// Token: 0x0600887E RID: 34942 RVA: 0x002927F0 File Offset: 0x002927F0
		internal override void Write(BinaryWriter w)
		{
			base.Write(w);
		}
	}
}
