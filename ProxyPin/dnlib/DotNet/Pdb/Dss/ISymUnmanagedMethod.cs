using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200095F RID: 2399
	[ComVisible(true)]
	[Guid("B62B923C-B500-3158-A543-24F307A8B7E1")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedMethod
	{
		// Token: 0x06005BF1 RID: 23537
		void GetToken(out uint pToken);

		// Token: 0x06005BF2 RID: 23538
		void GetSequencePointCount(out uint pRetVal);

		// Token: 0x06005BF3 RID: 23539
		void GetRootScope(out ISymUnmanagedScope pRetVal);

		// Token: 0x06005BF4 RID: 23540
		void GetScopeFromOffset([In] uint offset, out ISymUnmanagedScope pRetVal);

		// Token: 0x06005BF5 RID: 23541
		void GetOffset([In] ISymUnmanagedDocument document, [In] uint line, [In] uint column, out uint pRetVal);

		// Token: 0x06005BF6 RID: 23542
		void GetRanges([In] ISymUnmanagedDocument document, [In] uint line, [In] uint column, [In] uint cRanges, out uint pcRanges, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] [Out] int[] ranges);

		// Token: 0x06005BF7 RID: 23543
		void GetParameters([In] uint cParams, out uint pcParams, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedVariable[] parameters);

		// Token: 0x06005BF8 RID: 23544
		void GetNamespace(out ISymUnmanagedNamespace pRetVal);

		// Token: 0x06005BF9 RID: 23545
		void GetSourceStartEnd([In] ISymUnmanagedDocument[] docs, [In] int[] lines, [In] int[] columns, out bool pRetVal);

		// Token: 0x06005BFA RID: 23546
		void GetSequencePoints([In] uint cPoints, out uint pcPoints, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] int[] offsets, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedDocument[] documents, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] int[] lines, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] int[] columns, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] int[] endLines, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] int[] endColumns);
	}
}
