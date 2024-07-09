using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000986 RID: 2438
	[Flags]
	[ComVisible(true)]
	public enum ComImageFlags : uint
	{
		// Token: 0x04002DE0 RID: 11744
		ILOnly = 1U,
		// Token: 0x04002DE1 RID: 11745
		Bit32Required = 2U,
		// Token: 0x04002DE2 RID: 11746
		ILLibrary = 4U,
		// Token: 0x04002DE3 RID: 11747
		StrongNameSigned = 8U,
		// Token: 0x04002DE4 RID: 11748
		NativeEntryPoint = 16U,
		// Token: 0x04002DE5 RID: 11749
		TrackDebugData = 65536U,
		// Token: 0x04002DE6 RID: 11750
		Bit32Preferred = 131072U
	}
}
