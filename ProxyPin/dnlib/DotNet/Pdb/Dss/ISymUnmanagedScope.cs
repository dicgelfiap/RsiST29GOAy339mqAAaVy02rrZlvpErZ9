using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000964 RID: 2404
	[ComVisible(true)]
	[Guid("68005D0F-B8E0-3B01-84D5-A11A94154942")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedScope
	{
		// Token: 0x06005C0F RID: 23567
		void GetMethod(out ISymUnmanagedMethod pRetVal);

		// Token: 0x06005C10 RID: 23568
		void GetParent(out ISymUnmanagedScope pRetVal);

		// Token: 0x06005C11 RID: 23569
		void GetChildren([In] uint cChildren, out uint pcChildren, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedScope[] children);

		// Token: 0x06005C12 RID: 23570
		void GetStartOffset(out uint pRetVal);

		// Token: 0x06005C13 RID: 23571
		void GetEndOffset(out uint pRetVal);

		// Token: 0x06005C14 RID: 23572
		void GetLocalCount(out uint pRetVal);

		// Token: 0x06005C15 RID: 23573
		void GetLocals([In] uint cLocals, out uint pcLocals, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedVariable[] locals);

		// Token: 0x06005C16 RID: 23574
		void GetNamespaces([In] uint cNameSpaces, out uint pcNameSpaces, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedNamespace[] namespaces);
	}
}
