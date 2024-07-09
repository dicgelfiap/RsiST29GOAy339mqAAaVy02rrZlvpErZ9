using System;
using System.Runtime.InteropServices;

namespace Toolbelt.Drawing.Win32
{
	// Token: 0x020000C2 RID: 194
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct ICONFILEHEAD
	{
		// Token: 0x0400058F RID: 1423
		public ushort Reserved;

		// Token: 0x04000590 RID: 1424
		public ushort Type;

		// Token: 0x04000591 RID: 1425
		public ushort Count;
	}
}
