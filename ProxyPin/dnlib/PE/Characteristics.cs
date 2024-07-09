using System;
using System.Runtime.InteropServices;

namespace dnlib.PE
{
	// Token: 0x02000747 RID: 1863
	[Flags]
	[ComVisible(true)]
	public enum Characteristics : ushort
	{
		// Token: 0x0400229D RID: 8861
		RelocsStripped = 1,
		// Token: 0x0400229E RID: 8862
		ExecutableImage = 2,
		// Token: 0x0400229F RID: 8863
		LineNumsStripped = 4,
		// Token: 0x040022A0 RID: 8864
		LocalSymsStripped = 8,
		// Token: 0x040022A1 RID: 8865
		AggressiveWsTrim = 16,
		// Token: 0x040022A2 RID: 8866
		LargeAddressAware = 32,
		// Token: 0x040022A3 RID: 8867
		Reserved1 = 64,
		// Token: 0x040022A4 RID: 8868
		BytesReversedLo = 128,
		// Token: 0x040022A5 RID: 8869
		Bit32Machine = 256,
		// Token: 0x040022A6 RID: 8870
		DebugStripped = 512,
		// Token: 0x040022A7 RID: 8871
		RemovableRunFromSwap = 1024,
		// Token: 0x040022A8 RID: 8872
		NetRunFromSwap = 2048,
		// Token: 0x040022A9 RID: 8873
		System = 4096,
		// Token: 0x040022AA RID: 8874
		Dll = 8192,
		// Token: 0x040022AB RID: 8875
		UpSystemOnly = 16384,
		// Token: 0x040022AC RID: 8876
		BytesReversedHi = 32768
	}
}
