using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000965 RID: 2405
	[ComVisible(true)]
	[Guid("AE932FBA-3FD8-4dba-8232-30A2309B02DB")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedScope2 : ISymUnmanagedScope
	{
		// Token: 0x06005C17 RID: 23575
		void GetMethod(out ISymUnmanagedMethod pRetVal);

		// Token: 0x06005C18 RID: 23576
		void GetParent(out ISymUnmanagedScope pRetVal);

		// Token: 0x06005C19 RID: 23577
		void GetChildren([In] uint cChildren, out uint pcChildren, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedScope[] children);

		// Token: 0x06005C1A RID: 23578
		void GetStartOffset(out uint pRetVal);

		// Token: 0x06005C1B RID: 23579
		void GetEndOffset(out uint pRetVal);

		// Token: 0x06005C1C RID: 23580
		void GetLocalCount(out uint pRetVal);

		// Token: 0x06005C1D RID: 23581
		void GetLocals([In] uint cLocals, out uint pcLocals, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedVariable[] locals);

		// Token: 0x06005C1E RID: 23582
		void GetNamespaces([In] uint cNameSpaces, out uint pcNameSpaces, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedNamespace[] namespaces);

		// Token: 0x06005C1F RID: 23583
		uint GetConstantCount();

		// Token: 0x06005C20 RID: 23584
		void GetConstants([In] uint cConstants, out uint pcConstants, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] ISymUnmanagedConstant[] constants);
	}
}
