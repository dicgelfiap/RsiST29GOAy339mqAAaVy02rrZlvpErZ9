using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200095C RID: 2396
	[ComVisible(true)]
	[Guid("E65C58B7-2948-434D-8A6D-481740A00C16")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedReader4 : ISymUnmanagedReader3, ISymUnmanagedReader2, ISymUnmanagedReader
	{
		// Token: 0x06005BE1 RID: 23521
		void _VtblGap1_22();

		// Token: 0x06005BE2 RID: 23522
		[PreserveSig]
		int MatchesModule(Guid guid, uint stamp, uint age, [MarshalAs(UnmanagedType.Bool)] out bool result);

		// Token: 0x06005BE3 RID: 23523
		void GetPortableDebugMetadata(out IntPtr pMetadata, out uint pcMetadata);

		// Token: 0x06005BE4 RID: 23524
		[PreserveSig]
		int GetSourceServerData(out IntPtr data, out int pcData);
	}
}
