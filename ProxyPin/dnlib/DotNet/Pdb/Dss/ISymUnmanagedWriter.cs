using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000969 RID: 2409
	[ComVisible(true)]
	[Guid("ED14AA72-78E2-4884-84E2-334293AE5214")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedWriter
	{
		// Token: 0x06005C93 RID: 23699
		void DefineDocument([MarshalAs(UnmanagedType.LPWStr)] [In] string url, [In] ref Guid language, [In] ref Guid languageVendor, [In] ref Guid documentType, out ISymUnmanagedDocumentWriter pRetVal);

		// Token: 0x06005C94 RID: 23700
		void SetUserEntryPoint([In] uint entryMethod);

		// Token: 0x06005C95 RID: 23701
		void OpenMethod([In] uint method);

		// Token: 0x06005C96 RID: 23702
		void CloseMethod();

		// Token: 0x06005C97 RID: 23703
		void OpenScope([In] uint startOffset, out uint pRetVal);

		// Token: 0x06005C98 RID: 23704
		void CloseScope([In] uint endOffset);

		// Token: 0x06005C99 RID: 23705
		void SetScopeRange([In] uint scopeID, [In] uint startOffset, [In] uint endOffset);

		// Token: 0x06005C9A RID: 23706
		void DefineLocalVariable([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint attributes, [In] uint cSig, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] signature, [In] uint addrKind, [In] uint addr1, [In] uint addr2, [In] uint addr3, [In] uint startOffset, [In] uint endOffset);

		// Token: 0x06005C9B RID: 23707
		void DefineParameter([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint attributes, [In] uint sequence, [In] uint addrKind, [In] uint addr1, [In] uint addr2, [In] uint addr3);

		// Token: 0x06005C9C RID: 23708
		void DefineField([In] uint parent, [MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint attributes, [In] uint cSig, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] byte[] signature, [In] uint addrKind, [In] uint addr1, [In] uint addr2, [In] uint addr3);

		// Token: 0x06005C9D RID: 23709
		void DefineGlobalVariable([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint attributes, [In] uint cSig, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] signature, [In] uint addrKind, [In] uint addr1, [In] uint addr2, [In] uint addr3);

		// Token: 0x06005C9E RID: 23710
		void Close();

		// Token: 0x06005C9F RID: 23711
		void SetSymAttribute([In] uint parent, [MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] uint cData, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] data);

		// Token: 0x06005CA0 RID: 23712
		void OpenNamespace([MarshalAs(UnmanagedType.LPWStr)] [In] string name);

		// Token: 0x06005CA1 RID: 23713
		void CloseNamespace();

		// Token: 0x06005CA2 RID: 23714
		void UsingNamespace([MarshalAs(UnmanagedType.LPWStr)] [In] string fullName);

		// Token: 0x06005CA3 RID: 23715
		void SetMethodSourceRange([In] ISymUnmanagedDocumentWriter startDoc, [In] uint startLine, [In] uint startColumn, [In] ISymUnmanagedDocumentWriter endDoc, [In] uint endLine, [In] uint endColumn);

		// Token: 0x06005CA4 RID: 23716
		void Initialize([In] IntPtr emitter, [MarshalAs(UnmanagedType.LPWStr)] [In] string filename, [In] IStream pIStream, [In] bool fFullBuild);

		// Token: 0x06005CA5 RID: 23717
		void GetDebugInfo(out IMAGE_DEBUG_DIRECTORY pIDD, [In] uint cData, out uint pcData, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] data);

		// Token: 0x06005CA6 RID: 23718
		void DefineSequencePoints([In] ISymUnmanagedDocumentWriter document, [In] uint spCount, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] offsets, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] lines, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] columns, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] endLines, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] int[] endColumns);

		// Token: 0x06005CA7 RID: 23719
		void RemapToken([In] uint oldToken, [In] uint newToken);

		// Token: 0x06005CA8 RID: 23720
		void Initialize2([MarshalAs(UnmanagedType.IUnknown)] [In] object emitter, [MarshalAs(UnmanagedType.LPWStr)] [In] string tempfilename, [In] IStream pIStream, [In] bool fFullBuild, [MarshalAs(UnmanagedType.LPWStr)] [In] string finalfilename);

		// Token: 0x06005CA9 RID: 23721
		void DefineConstant([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] object value, [In] uint cSig, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] signature);

		// Token: 0x06005CAA RID: 23722
		void Abort();
	}
}
