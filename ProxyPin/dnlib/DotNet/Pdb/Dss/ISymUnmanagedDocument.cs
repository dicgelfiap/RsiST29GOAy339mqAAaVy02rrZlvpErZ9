using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200095E RID: 2398
	[ComVisible(true)]
	[Guid("40DE4037-7C81-3E1E-B022-AE1ABFF2CA08")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedDocument
	{
		// Token: 0x06005BE7 RID: 23527
		void GetURL([In] uint cchUrl, out uint pcchUrl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] char[] szUrl);

		// Token: 0x06005BE8 RID: 23528
		void GetDocumentType(out Guid pRetVal);

		// Token: 0x06005BE9 RID: 23529
		void GetLanguage(out Guid pRetVal);

		// Token: 0x06005BEA RID: 23530
		void GetLanguageVendor(out Guid pRetVal);

		// Token: 0x06005BEB RID: 23531
		void GetCheckSumAlgorithmId(out Guid pRetVal);

		// Token: 0x06005BEC RID: 23532
		void GetCheckSum([In] uint cData, out uint pcData, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] byte[] data);

		// Token: 0x06005BED RID: 23533
		void FindClosestLine([In] uint line, out uint pRetVal);

		// Token: 0x06005BEE RID: 23534
		void HasEmbeddedSource(out bool pRetVal);

		// Token: 0x06005BEF RID: 23535
		[PreserveSig]
		int GetSourceLength(out int pRetVal);

		// Token: 0x06005BF0 RID: 23536
		[PreserveSig]
		int GetSourceRange([In] uint startLine, [In] uint startColumn, [In] uint endLine, [In] uint endColumn, [In] int cSourceBytes, out int pcSourceBytes, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] [In] [Out] byte[] source);
	}
}
