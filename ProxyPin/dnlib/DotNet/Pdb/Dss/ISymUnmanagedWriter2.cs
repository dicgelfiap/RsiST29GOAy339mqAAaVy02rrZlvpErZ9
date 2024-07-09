using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200096A RID: 2410
	[ComVisible(true)]
	[Guid("0B97726E-9E6D-4F05-9A26-424022093CAA")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedWriter2
	{
		// Token: 0x06005CAB RID: 23723
		void DefineDocument([MarshalAs(UnmanagedType.LPWStr)] [In] string url, [In] ref Guid language, [In] ref Guid languageVendor, [In] ref Guid documentType, out ISymUnmanagedDocumentWriter pRetVal);

		// Token: 0x06005CAC RID: 23724
		void SetUserEntryPoint([In] uint entryMethod);

		// Token: 0x06005CAD RID: 23725
		void OpenMethod([In] uint method);

		// Token: 0x06005CAE RID: 23726
		void CloseMethod();

		// Token: 0x06005CAF RID: 23727
		void OpenScope([In] uint startOffset, out uint pRetVal);

		// Token: 0x06005CB0 RID: 23728
		void CloseScope([In] uint endOffset);

		// Token: 0x06005CB1 RID: 23729
		void SetScopeRange([In] uint scopeID, [In] uint startOffset, [In] uint endOffset);

		// Token: 0x06005CB2 RID: 23730
		void DefineLocalVariable([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint attributes, [In] uint cSig, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] signature, [In] uint addrKind, [In] uint addr1, [In] uint addr2, [In] uint addr3, [In] uint startOffset, [In] uint endOffset);

		// Token: 0x06005CB3 RID: 23731
		void DefineParameter([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint attributes, [In] uint sequence, [In] uint addrKind, [In] uint addr1, [In] uint addr2, [In] uint addr3);

		// Token: 0x06005CB4 RID: 23732
		void DefineField([In] uint parent, [MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint attributes, [In] uint cSig, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] signature, [In] uint addrKind, [In] uint addr1, [In] uint addr2, [In] uint addr3);

		// Token: 0x06005CB5 RID: 23733
		void DefineGlobalVariable([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint attributes, [In] uint cSig, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] signature, [In] uint addrKind, [In] uint addr1, [In] uint addr2, [In] uint addr3);

		// Token: 0x06005CB6 RID: 23734
		void Close();

		// Token: 0x06005CB7 RID: 23735
		void SetSymAttribute([In] uint parent, [MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint cData, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] data);

		// Token: 0x06005CB8 RID: 23736
		void OpenNamespace([MarshalAs(UnmanagedType.LPWStr)] [In] string name);

		// Token: 0x06005CB9 RID: 23737
		void CloseNamespace();

		// Token: 0x06005CBA RID: 23738
		void UsingNamespace([MarshalAs(UnmanagedType.LPWStr)] [In] string fullName);

		// Token: 0x06005CBB RID: 23739
		void SetMethodSourceRange([In] ISymUnmanagedDocumentWriter startDoc, [In] uint startLine, [In] uint startColumn, [In] ISymUnmanagedDocumentWriter endDoc, [In] uint endLine, [In] uint endColumn);

		// Token: 0x06005CBC RID: 23740
		void Initialize([MarshalAs(UnmanagedType.IUnknown)] [In] object emitter, [MarshalAs(UnmanagedType.LPWStr)] [In] string filename, [In] IStream pIStream, [In] bool fFullBuild);

		// Token: 0x06005CBD RID: 23741
		void GetDebugInfo(out IMAGE_DEBUG_DIRECTORY pIDD, [In] uint cData, out uint pcData, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] data);

		// Token: 0x06005CBE RID: 23742
		void DefineSequencePoints([In] ISymUnmanagedDocumentWriter document, [In] uint spCount, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] offsets, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] lines, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] columns, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] endLines, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] endColumns);

		// Token: 0x06005CBF RID: 23743
		void RemapToken([In] uint oldToken, [In] uint newToken);

		// Token: 0x06005CC0 RID: 23744
		void Initialize2([MarshalAs(UnmanagedType.IUnknown)] [In] object emitter, [MarshalAs(UnmanagedType.LPWStr)] [In] string tempfilename, [In] IStream pIStream, [In] bool fFullBuild, [MarshalAs(UnmanagedType.LPWStr)] [In] string finalfilename);

		// Token: 0x06005CC1 RID: 23745
		void DefineConstant([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] object value, [In] uint cSig, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] signature);

		// Token: 0x06005CC2 RID: 23746
		void Abort();

		// Token: 0x06005CC3 RID: 23747
		void DefineLocalVariable2([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint attributes, [In] uint sigToken, [In] uint addrKind, [In] uint addr1, [In] uint addr2, [In] uint addr3, [In] uint startOffset, [In] uint endOffset);

		// Token: 0x06005CC4 RID: 23748
		void DefineGlobalVariable2([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint attributes, [In] uint sigToken, [In] uint addrKind, [In] uint addr1, [In] uint addr2, [In] uint addr3);

		// Token: 0x06005CC5 RID: 23749
		void DefineConstant2([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] object value, [In] uint sigToken);
	}
}
