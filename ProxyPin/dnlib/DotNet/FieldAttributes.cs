using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007AA RID: 1962
	[Flags]
	[ComVisible(true)]
	public enum FieldAttributes : ushort
	{
		// Token: 0x040024A8 RID: 9384
		FieldAccessMask = 7,
		// Token: 0x040024A9 RID: 9385
		PrivateScope = 0,
		// Token: 0x040024AA RID: 9386
		CompilerControlled = 0,
		// Token: 0x040024AB RID: 9387
		Private = 1,
		// Token: 0x040024AC RID: 9388
		FamANDAssem = 2,
		// Token: 0x040024AD RID: 9389
		Assembly = 3,
		// Token: 0x040024AE RID: 9390
		Family = 4,
		// Token: 0x040024AF RID: 9391
		FamORAssem = 5,
		// Token: 0x040024B0 RID: 9392
		Public = 6,
		// Token: 0x040024B1 RID: 9393
		Static = 16,
		// Token: 0x040024B2 RID: 9394
		InitOnly = 32,
		// Token: 0x040024B3 RID: 9395
		Literal = 64,
		// Token: 0x040024B4 RID: 9396
		NotSerialized = 128,
		// Token: 0x040024B5 RID: 9397
		SpecialName = 512,
		// Token: 0x040024B6 RID: 9398
		PinvokeImpl = 8192,
		// Token: 0x040024B7 RID: 9399
		RTSpecialName = 1024,
		// Token: 0x040024B8 RID: 9400
		HasFieldMarshal = 4096,
		// Token: 0x040024B9 RID: 9401
		HasDefault = 32768,
		// Token: 0x040024BA RID: 9402
		HasFieldRVA = 256
	}
}
