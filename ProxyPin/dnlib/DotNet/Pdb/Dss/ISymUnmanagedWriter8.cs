using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000970 RID: 2416
	[ComVisible(true)]
	[Guid("5BA52F3B-6BF8-40FC-B476-D39C529B331E")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedWriter8 : ISymUnmanagedWriter7, ISymUnmanagedWriter6, ISymUnmanagedWriter5, ISymUnmanagedWriter4, ISymUnmanagedWriter3, ISymUnmanagedWriter2
	{
		// Token: 0x06005CD3 RID: 23763
		void _VtblGap1_35();

		// Token: 0x06005CD4 RID: 23764
		void UpdateSignature(Guid pdbId, uint stamp, uint age);

		// Token: 0x06005CD5 RID: 23765
		void SetSourceServerData(IntPtr data, uint cData);

		// Token: 0x06005CD6 RID: 23766
		void SetSourceLinkData(IntPtr data, uint cData);
	}
}
