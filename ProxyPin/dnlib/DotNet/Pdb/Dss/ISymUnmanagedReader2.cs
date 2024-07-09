using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200095A RID: 2394
	[ComVisible(true)]
	[Guid("A09E53B2-2A57-4cca-8F63-B84F7C35D4AA")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedReader2 : ISymUnmanagedReader
	{
		// Token: 0x06005BDA RID: 23514
		void _VtblGap1_17();

		// Token: 0x06005BDB RID: 23515
		void GetMethodByVersionPreRemap(uint token, uint version, [MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedMethod pRetVal);

		// Token: 0x06005BDC RID: 23516
		void GetSymAttributePreRemap(uint parent, [MarshalAs(UnmanagedType.LPWStr)] [In] string name, uint cBuffer, out uint pcBuffer, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] [Out] byte[] buffer);

		// Token: 0x06005BDD RID: 23517
		void GetMethodsInDocument(ISymUnmanagedDocument document, uint bufferLength, out uint count, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] [Out] ISymUnmanagedMethod[] methods);
	}
}
