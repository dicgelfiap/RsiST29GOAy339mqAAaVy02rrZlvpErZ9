using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000972 RID: 2418
	[ComVisible(true)]
	[Guid("B01FAFEB-C450-3A4D-BEEC-B4CEEC01E006")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedDocumentWriter
	{
		// Token: 0x06005CD9 RID: 23769
		void SetSource([In] uint sourceSize, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] byte[] source);

		// Token: 0x06005CDA RID: 23770
		void SetCheckSum([In] Guid algorithmId, [In] uint checkSumSize, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] byte[] checkSum);
	}
}
