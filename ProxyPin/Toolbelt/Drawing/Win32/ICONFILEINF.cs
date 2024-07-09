using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Toolbelt.Drawing.Win32
{
	// Token: 0x020000C1 RID: 193
	[DebuggerDisplay("{Cx} x {Cy}, {BitCount}bit, {Size}bytes")]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct ICONFILEINF
	{
		// Token: 0x04000587 RID: 1415
		public byte Cx;

		// Token: 0x04000588 RID: 1416
		public byte Cy;

		// Token: 0x04000589 RID: 1417
		public byte ColorCount;

		// Token: 0x0400058A RID: 1418
		public byte Reserved;

		// Token: 0x0400058B RID: 1419
		public ushort Planes;

		// Token: 0x0400058C RID: 1420
		public ushort BitCount;

		// Token: 0x0400058D RID: 1421
		public uint Size;

		// Token: 0x0400058E RID: 1422
		public uint Address;
	}
}
