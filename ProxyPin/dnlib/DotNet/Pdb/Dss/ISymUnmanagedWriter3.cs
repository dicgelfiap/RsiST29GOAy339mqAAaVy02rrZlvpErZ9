using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200096B RID: 2411
	[ComVisible(true)]
	[Guid("12F1E02C-1E05-4B0E-9468-EBC9D1BB040F")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedWriter3 : ISymUnmanagedWriter2
	{
		// Token: 0x06005CC6 RID: 23750
		void _VtblGap1_27();

		// Token: 0x06005CC7 RID: 23751
		void OpenMethod2(uint method, uint isect, uint offset);

		// Token: 0x06005CC8 RID: 23752
		void Commit();
	}
}
