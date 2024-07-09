using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000959 RID: 2393
	[ComVisible(true)]
	[Guid("B4CE6286-2A6B-3712-A3B7-1EE1DAD467B5")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedReader
	{
		// Token: 0x06005BC9 RID: 23497
		void GetDocument([MarshalAs(UnmanagedType.LPWStr)] [In] string url, [In] Guid language, [In] Guid languageVendor, [In] Guid documentType, out ISymUnmanagedDocument pRetVal);

		// Token: 0x06005BCA RID: 23498
		void GetDocuments([In] uint cDocs, out uint pcDocs, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedDocument[] pDocs);

		// Token: 0x06005BCB RID: 23499
		[PreserveSig]
		int GetUserEntryPoint(out uint pToken);

		// Token: 0x06005BCC RID: 23500
		void GetMethod([In] uint token, out ISymUnmanagedMethod retVal);

		// Token: 0x06005BCD RID: 23501
		[PreserveSig]
		int GetMethodByVersion([In] uint token, [In] int version, out ISymUnmanagedMethod pRetVal);

		// Token: 0x06005BCE RID: 23502
		void GetVariables([In] uint parent, [In] uint cVars, out uint pcVars, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] [Out] ISymUnmanagedVariable[] pVars);

		// Token: 0x06005BCF RID: 23503
		void GetGlobalVariables([In] uint cVars, out uint pcVars, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedVariable[] pVars);

		// Token: 0x06005BD0 RID: 23504
		void GetMethodFromDocumentPosition([In] ISymUnmanagedDocument document, [In] uint line, [In] uint column, out ISymUnmanagedMethod pRetVal);

		// Token: 0x06005BD1 RID: 23505
		void GetSymAttribute([In] uint parent, [MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint cBuffer, out uint pcBuffer, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] [Out] byte[] buffer);

		// Token: 0x06005BD2 RID: 23506
		void GetNamespaces([In] uint cNameSpaces, out uint pcNameSpaces, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] ISymUnmanagedNamespace[] namespaces);

		// Token: 0x06005BD3 RID: 23507
		[PreserveSig]
		int Initialize([MarshalAs(UnmanagedType.IUnknown)] [In] object importer, [MarshalAs(UnmanagedType.LPWStr)] [In] string filename, [MarshalAs(UnmanagedType.LPWStr)] [In] string searchPath, [In] IStream pIStream);

		// Token: 0x06005BD4 RID: 23508
		void UpdateSymbolStore([MarshalAs(UnmanagedType.LPWStr)] [In] string filename, [In] IStream pIStream);

		// Token: 0x06005BD5 RID: 23509
		void ReplaceSymbolStore([MarshalAs(UnmanagedType.LPWStr)] [In] string filename, [In] IStream pIStream);

		// Token: 0x06005BD6 RID: 23510
		void GetSymbolStoreFileName([In] uint cchName, out uint pcchName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] char[] szName);

		// Token: 0x06005BD7 RID: 23511
		void GetMethodsFromDocumentPosition([In] ISymUnmanagedDocument document, [In] uint line, [In] uint column, [In] uint cMethod, out uint pcMethod, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] [Out] ISymUnmanagedMethod[] pRetVal);

		// Token: 0x06005BD8 RID: 23512
		void GetDocumentVersion([In] ISymUnmanagedDocument pDoc, out int version, out bool pbCurrent);

		// Token: 0x06005BD9 RID: 23513
		void GetMethodVersion([In] ISymUnmanagedMethod pMethod, out int version);
	}
}
