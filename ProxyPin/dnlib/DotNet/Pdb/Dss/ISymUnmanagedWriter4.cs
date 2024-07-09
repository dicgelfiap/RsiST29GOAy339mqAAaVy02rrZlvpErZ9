using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200096C RID: 2412
	[ComVisible(true)]
	[Guid("BC7E3F53-F458-4C23-9DBD-A189E6E96594")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedWriter4 : ISymUnmanagedWriter3, ISymUnmanagedWriter2
	{
		// Token: 0x06005CC9 RID: 23753
		void _VtblGap1_29();

		// Token: 0x06005CCA RID: 23754
		void GetDebugInfoWithPadding([In] [Out] ref IMAGE_DEBUG_DIRECTORY pIDD, uint cData, out uint pcData, IntPtr data);
	}
}
