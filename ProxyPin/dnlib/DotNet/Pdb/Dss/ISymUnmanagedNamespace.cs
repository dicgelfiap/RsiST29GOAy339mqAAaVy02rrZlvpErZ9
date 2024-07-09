using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000963 RID: 2403
	[ComVisible(true)]
	[Guid("0DFF7289-54F8-11D3-BD28-0000F80849BD")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedNamespace
	{
		// Token: 0x06005C0C RID: 23564
		void GetName([In] uint cchName, out uint pcchName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] char[] szName);

		// Token: 0x06005C0D RID: 23565
		void GetNamespaces([In] uint cNameSpaces, out uint pcNameSpaces, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedNamespace[] namespaces);

		// Token: 0x06005C0E RID: 23566
		void GetVariables([In] uint cVars, out uint pcVars, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedVariable[] pVars);
	}
}
