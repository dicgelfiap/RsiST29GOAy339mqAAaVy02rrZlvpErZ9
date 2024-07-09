using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000960 RID: 2400
	[ComVisible(true)]
	[Guid("5DA320C8-9C2C-4E5A-B823-027E0677B359")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedMethod2 : ISymUnmanagedMethod
	{
		// Token: 0x06005BFB RID: 23547
		void _VtblGap1_10();

		// Token: 0x06005BFC RID: 23548
		void GetLocalSignatureToken(out uint token);
	}
}
