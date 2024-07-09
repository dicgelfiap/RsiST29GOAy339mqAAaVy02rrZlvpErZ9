using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000966 RID: 2406
	[ComVisible(true)]
	[Guid("48B25ED8-5BAD-41bc-9CEE-CD62FABC74E9")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedConstant
	{
		// Token: 0x06005C21 RID: 23585
		void GetName([In] uint cchName, out uint pcchName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] char[] szName);

		// Token: 0x06005C22 RID: 23586
		void GetValue(out object pValue);

		// Token: 0x06005C23 RID: 23587
		[PreserveSig]
		int GetSignature([In] uint cSig, out uint pcSig, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] byte[] sig);
	}
}
