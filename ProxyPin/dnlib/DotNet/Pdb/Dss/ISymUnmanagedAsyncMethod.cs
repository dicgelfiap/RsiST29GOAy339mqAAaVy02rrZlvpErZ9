using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000961 RID: 2401
	[ComVisible(true)]
	[Guid("B20D55B3-532E-4906-87E7-25BD5734ABD2")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISymUnmanagedAsyncMethod
	{
		// Token: 0x06005BFD RID: 23549
		bool IsAsyncMethod();

		// Token: 0x06005BFE RID: 23550
		uint GetKickoffMethod();

		// Token: 0x06005BFF RID: 23551
		bool HasCatchHandlerILOffset();

		// Token: 0x06005C00 RID: 23552
		uint GetCatchHandlerILOffset();

		// Token: 0x06005C01 RID: 23553
		uint GetAsyncStepInfoCount();

		// Token: 0x06005C02 RID: 23554
		void GetAsyncStepInfo([In] uint cStepInfo, out uint pcStepInfo, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] uint[] yieldOffsets, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] uint[] breakpointOffset, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] [Out] uint[] breakpointMethod);
	}
}
