using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007A2 RID: 1954
	[ComVisible(true)]
	public enum ElementType : byte
	{
		// Token: 0x04002462 RID: 9314
		End,
		// Token: 0x04002463 RID: 9315
		Void,
		// Token: 0x04002464 RID: 9316
		Boolean,
		// Token: 0x04002465 RID: 9317
		Char,
		// Token: 0x04002466 RID: 9318
		I1,
		// Token: 0x04002467 RID: 9319
		U1,
		// Token: 0x04002468 RID: 9320
		I2,
		// Token: 0x04002469 RID: 9321
		U2,
		// Token: 0x0400246A RID: 9322
		I4,
		// Token: 0x0400246B RID: 9323
		U4,
		// Token: 0x0400246C RID: 9324
		I8,
		// Token: 0x0400246D RID: 9325
		U8,
		// Token: 0x0400246E RID: 9326
		R4,
		// Token: 0x0400246F RID: 9327
		R8,
		// Token: 0x04002470 RID: 9328
		String,
		// Token: 0x04002471 RID: 9329
		Ptr,
		// Token: 0x04002472 RID: 9330
		ByRef,
		// Token: 0x04002473 RID: 9331
		ValueType,
		// Token: 0x04002474 RID: 9332
		Class,
		// Token: 0x04002475 RID: 9333
		Var,
		// Token: 0x04002476 RID: 9334
		Array,
		// Token: 0x04002477 RID: 9335
		GenericInst,
		// Token: 0x04002478 RID: 9336
		TypedByRef,
		// Token: 0x04002479 RID: 9337
		ValueArray,
		// Token: 0x0400247A RID: 9338
		I,
		// Token: 0x0400247B RID: 9339
		U,
		// Token: 0x0400247C RID: 9340
		R,
		// Token: 0x0400247D RID: 9341
		FnPtr,
		// Token: 0x0400247E RID: 9342
		Object,
		// Token: 0x0400247F RID: 9343
		SZArray,
		// Token: 0x04002480 RID: 9344
		MVar,
		// Token: 0x04002481 RID: 9345
		CModReqd,
		// Token: 0x04002482 RID: 9346
		CModOpt,
		// Token: 0x04002483 RID: 9347
		Internal,
		// Token: 0x04002484 RID: 9348
		Module = 63,
		// Token: 0x04002485 RID: 9349
		Sentinel = 65,
		// Token: 0x04002486 RID: 9350
		Pinned = 69
	}
}
