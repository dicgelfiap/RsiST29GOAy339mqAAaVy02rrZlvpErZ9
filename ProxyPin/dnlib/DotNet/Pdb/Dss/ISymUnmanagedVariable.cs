using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000962 RID: 2402
	[ComVisible(true)]
	[Guid("9F60EEBE-2D9A-3F7C-BF58-80BC991C60BB")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedVariable
	{
		// Token: 0x06005C03 RID: 23555
		void GetName([In] uint cchName, out uint pcchName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] char[] szName);

		// Token: 0x06005C04 RID: 23556
		void GetAttributes(out uint pRetVal);

		// Token: 0x06005C05 RID: 23557
		void GetSignature([In] uint cSig, out uint pcSig, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] byte[] sig);

		// Token: 0x06005C06 RID: 23558
		void GetAddressKind(out uint pRetVal);

		// Token: 0x06005C07 RID: 23559
		void GetAddressField1(out uint pRetVal);

		// Token: 0x06005C08 RID: 23560
		void GetAddressField2(out uint pRetVal);

		// Token: 0x06005C09 RID: 23561
		void GetAddressField3(out uint pRetVal);

		// Token: 0x06005C0A RID: 23562
		void GetStartOffset(out uint pRetVal);

		// Token: 0x06005C0B RID: 23563
		void GetEndOffset(out uint pRetVal);
	}
}
