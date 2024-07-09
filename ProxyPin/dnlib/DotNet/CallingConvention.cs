using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000782 RID: 1922
	[Flags]
	[ComVisible(true)]
	public enum CallingConvention : byte
	{
		// Token: 0x04002405 RID: 9221
		Default = 0,
		// Token: 0x04002406 RID: 9222
		C = 1,
		// Token: 0x04002407 RID: 9223
		StdCall = 2,
		// Token: 0x04002408 RID: 9224
		ThisCall = 3,
		// Token: 0x04002409 RID: 9225
		FastCall = 4,
		// Token: 0x0400240A RID: 9226
		VarArg = 5,
		// Token: 0x0400240B RID: 9227
		Field = 6,
		// Token: 0x0400240C RID: 9228
		LocalSig = 7,
		// Token: 0x0400240D RID: 9229
		Property = 8,
		// Token: 0x0400240E RID: 9230
		Unmanaged = 9,
		// Token: 0x0400240F RID: 9231
		GenericInst = 10,
		// Token: 0x04002410 RID: 9232
		NativeVarArg = 11,
		// Token: 0x04002411 RID: 9233
		Mask = 15,
		// Token: 0x04002412 RID: 9234
		Generic = 16,
		// Token: 0x04002413 RID: 9235
		HasThis = 32,
		// Token: 0x04002414 RID: 9236
		ExplicitThis = 64,
		// Token: 0x04002415 RID: 9237
		ReservedByCLR = 128
	}
}
