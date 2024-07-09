using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000957 RID: 2391
	[ComVisible(true)]
	[Guid("969708D2-05E5-4861-A3B0-96E473CDF63F")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedDispose
	{
		// Token: 0x06005BC7 RID: 23495
		[PreserveSig]
		int Destroy();
	}
}
