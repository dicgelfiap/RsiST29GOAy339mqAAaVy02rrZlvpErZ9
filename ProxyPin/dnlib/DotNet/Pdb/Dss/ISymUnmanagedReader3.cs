using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200095B RID: 2395
	[ComVisible(true)]
	[Guid("6151CAD9-E1EE-437A-A808-F64838C0D046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedReader3 : ISymUnmanagedReader2, ISymUnmanagedReader
	{
		// Token: 0x06005BDE RID: 23518
		void _VtblGap1_20();

		// Token: 0x06005BDF RID: 23519
		void GetSymAttributeByVersion(uint token, uint version, [MarshalAs(UnmanagedType.LPWStr)] string name, uint cBuffer, out uint pcBuffer, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] [Out] byte[] buffer);

		// Token: 0x06005BE0 RID: 23520
		void GetSymAttributeByVersionPreRemap(int methodToken, int version, [MarshalAs(UnmanagedType.LPWStr)] string name, int cBuffer, out int pcBuffer, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] [Out] byte[] buffer);
	}
}
