using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000973 RID: 2419
	[ComVisible(true)]
	[Guid("FC073774-1739-4232-BD56-A027294BEC15")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedAsyncMethodPropertiesWriter
	{
		// Token: 0x06005CDB RID: 23771
		void DefineKickoffMethod([In] uint kickoffMethod);

		// Token: 0x06005CDC RID: 23772
		void DefineCatchHandlerILOffset([In] uint catchHandlerOffset);

		// Token: 0x06005CDD RID: 23773
		void DefineAsyncStepInfo([In] uint count, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] uint[] yieldOffsets, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] uint[] breakpointOffset, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] uint[] breakpointMethod);
	}
}
