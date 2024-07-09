using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200095D RID: 2397
	[ComVisible(true)]
	[Guid("6576C987-7E8D-4298-A6E1-6F9783165F07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedReader5 : ISymUnmanagedReader4, ISymUnmanagedReader3, ISymUnmanagedReader2, ISymUnmanagedReader
	{
		// Token: 0x06005BE5 RID: 23525
		void _VtblGap1_25();

		// Token: 0x06005BE6 RID: 23526
		void GetPortableDebugMetadataByVersion(uint version, out IntPtr pMetadata, out uint pcMetadata);
	}
}
