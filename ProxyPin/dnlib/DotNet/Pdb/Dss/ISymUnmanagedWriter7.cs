using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200096F RID: 2415
	[ComVisible(true)]
	[Guid("22DAEAF2-70F6-4EF1-B0C3-984F0BF27BFD")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedWriter7 : ISymUnmanagedWriter6, ISymUnmanagedWriter5, ISymUnmanagedWriter4, ISymUnmanagedWriter3, ISymUnmanagedWriter2
	{
		// Token: 0x06005CD1 RID: 23761
		void _VtblGap1_34();

		// Token: 0x06005CD2 RID: 23762
		void UpdateSignatureByHashingContent(IntPtr buffer, uint cData);
	}
}
