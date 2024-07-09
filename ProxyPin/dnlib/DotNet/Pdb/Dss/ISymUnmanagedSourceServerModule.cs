using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000958 RID: 2392
	[ComVisible(true)]
	[Guid("997DD0CC-A76F-4c82-8D79-EA87559D27AD")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedSourceServerModule
	{
		// Token: 0x06005BC8 RID: 23496
		[PreserveSig]
		int GetSourceServerData(out int pDataByteCount, out IntPtr ppData);
	}
}
