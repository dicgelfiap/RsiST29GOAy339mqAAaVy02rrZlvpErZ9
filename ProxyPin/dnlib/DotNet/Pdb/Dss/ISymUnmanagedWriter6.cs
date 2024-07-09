using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200096E RID: 2414
	[ComVisible(true)]
	[Guid("CA6C2ED9-103D-46A9-B03B-05446485848B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedWriter6 : ISymUnmanagedWriter5, ISymUnmanagedWriter4, ISymUnmanagedWriter3, ISymUnmanagedWriter2
	{
		// Token: 0x06005CCF RID: 23759
		void _VtblGap1_33();

		// Token: 0x06005CD0 RID: 23760
		void InitializeDeterministic([MarshalAs(UnmanagedType.IUnknown)] object emitter, [MarshalAs(UnmanagedType.IUnknown)] object stream);
	}
}
