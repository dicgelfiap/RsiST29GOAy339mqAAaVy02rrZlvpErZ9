using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Toolbelt.Drawing.Win32
{
	// Token: 0x020000BF RID: 191
	[DebuggerDisplay("{Cx} x {Cy}, {BitCount}bit, {Size}bytes")]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct ICONRESINF
	{
		// Token: 0x0400057C RID: 1404
		public byte Cx;

		// Token: 0x0400057D RID: 1405
		public byte Cy;

		// Token: 0x0400057E RID: 1406
		public byte ColorCount;

		// Token: 0x0400057F RID: 1407
		public byte Reserved;

		// Token: 0x04000580 RID: 1408
		public ushort Planes;

		// Token: 0x04000581 RID: 1409
		public ushort BitCount;

		// Token: 0x04000582 RID: 1410
		public uint Size;

		// Token: 0x04000583 RID: 1411
		public ushort ID;
	}
}
