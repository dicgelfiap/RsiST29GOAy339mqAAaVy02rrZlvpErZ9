using System;
using System.Runtime.InteropServices;

namespace Toolbelt.Drawing.Win32
{
	// Token: 0x020000C0 RID: 192
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct ICONRESHEAD
	{
		// Token: 0x04000584 RID: 1412
		public ushort Reserved;

		// Token: 0x04000585 RID: 1413
		public ushort Type;

		// Token: 0x04000586 RID: 1414
		public ushort Count;
	}
}
