using System;
using System.Runtime.InteropServices;

namespace dnlib.PE
{
	// Token: 0x0200075F RID: 1887
	[ComVisible(true)]
	public enum Subsystem : ushort
	{
		// Token: 0x04002370 RID: 9072
		Unknown,
		// Token: 0x04002371 RID: 9073
		Native,
		// Token: 0x04002372 RID: 9074
		WindowsGui,
		// Token: 0x04002373 RID: 9075
		WindowsCui,
		// Token: 0x04002374 RID: 9076
		Os2Cui = 5,
		// Token: 0x04002375 RID: 9077
		PosixCui = 7,
		// Token: 0x04002376 RID: 9078
		NativeWindows,
		// Token: 0x04002377 RID: 9079
		WindowsCeGui,
		// Token: 0x04002378 RID: 9080
		EfiApplication,
		// Token: 0x04002379 RID: 9081
		EfiBootServiceDriver,
		// Token: 0x0400237A RID: 9082
		EfiRuntimeDriver,
		// Token: 0x0400237B RID: 9083
		EfiRom,
		// Token: 0x0400237C RID: 9084
		Xbox,
		// Token: 0x0400237D RID: 9085
		WindowsBootApplication = 16
	}
}
