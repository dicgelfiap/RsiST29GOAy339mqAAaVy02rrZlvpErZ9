using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200082D RID: 2093
	[Flags]
	[ComVisible(true)]
	public enum PInvokeAttributes : ushort
	{
		// Token: 0x04002697 RID: 9879
		NoMangle = 1,
		// Token: 0x04002698 RID: 9880
		CharSetMask = 6,
		// Token: 0x04002699 RID: 9881
		CharSetNotSpec = 0,
		// Token: 0x0400269A RID: 9882
		CharSetAnsi = 2,
		// Token: 0x0400269B RID: 9883
		CharSetUnicode = 4,
		// Token: 0x0400269C RID: 9884
		CharSetAuto = 6,
		// Token: 0x0400269D RID: 9885
		BestFitUseAssem = 0,
		// Token: 0x0400269E RID: 9886
		BestFitEnabled = 16,
		// Token: 0x0400269F RID: 9887
		BestFitDisabled = 32,
		// Token: 0x040026A0 RID: 9888
		BestFitMask = 48,
		// Token: 0x040026A1 RID: 9889
		ThrowOnUnmappableCharUseAssem = 0,
		// Token: 0x040026A2 RID: 9890
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x040026A3 RID: 9891
		ThrowOnUnmappableCharDisabled = 8192,
		// Token: 0x040026A4 RID: 9892
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x040026A5 RID: 9893
		SupportsLastError = 64,
		// Token: 0x040026A6 RID: 9894
		CallConvMask = 1792,
		// Token: 0x040026A7 RID: 9895
		CallConvWinapi = 256,
		// Token: 0x040026A8 RID: 9896
		CallConvCdecl = 512,
		// Token: 0x040026A9 RID: 9897
		CallConvStdcall = 768,
		// Token: 0x040026AA RID: 9898
		CallConvStdCall = 768,
		// Token: 0x040026AB RID: 9899
		CallConvThiscall = 1024,
		// Token: 0x040026AC RID: 9900
		CallConvFastcall = 1280
	}
}
