using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200096D RID: 2413
	[ComVisible(true)]
	[Guid("DCF7780D-BDE9-45DF-ACFE-21731A32000C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedWriter5 : ISymUnmanagedWriter4, ISymUnmanagedWriter3, ISymUnmanagedWriter2
	{
		// Token: 0x06005CCB RID: 23755
		void _VtblGap1_30();

		// Token: 0x06005CCC RID: 23756
		void OpenMapTokensToSourceSpans();

		// Token: 0x06005CCD RID: 23757
		void CloseMapTokensToSourceSpans();

		// Token: 0x06005CCE RID: 23758
		void MapTokenToSourceSpan(uint token, ISymUnmanagedDocumentWriter document, uint line, uint column, uint endLine, uint endColumn);
	}
}
