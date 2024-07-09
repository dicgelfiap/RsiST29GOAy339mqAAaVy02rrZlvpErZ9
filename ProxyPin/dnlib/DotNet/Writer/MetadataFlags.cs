using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008AE RID: 2222
	[Flags]
	[ComVisible(true)]
	public enum MetadataFlags : uint
	{
		// Token: 0x040028C7 RID: 10439
		PreserveTypeRefRids = 1U,
		// Token: 0x040028C8 RID: 10440
		PreserveTypeDefRids = 2U,
		// Token: 0x040028C9 RID: 10441
		PreserveFieldRids = 4U,
		// Token: 0x040028CA RID: 10442
		PreserveMethodRids = 8U,
		// Token: 0x040028CB RID: 10443
		PreserveParamRids = 16U,
		// Token: 0x040028CC RID: 10444
		PreserveMemberRefRids = 32U,
		// Token: 0x040028CD RID: 10445
		PreserveStandAloneSigRids = 64U,
		// Token: 0x040028CE RID: 10446
		PreserveEventRids = 128U,
		// Token: 0x040028CF RID: 10447
		PreservePropertyRids = 256U,
		// Token: 0x040028D0 RID: 10448
		PreserveTypeSpecRids = 512U,
		// Token: 0x040028D1 RID: 10449
		PreserveMethodSpecRids = 1024U,
		// Token: 0x040028D2 RID: 10450
		PreserveAllMethodRids = 1064U,
		// Token: 0x040028D3 RID: 10451
		PreserveRids = 2047U,
		// Token: 0x040028D4 RID: 10452
		PreserveStringsOffsets = 2048U,
		// Token: 0x040028D5 RID: 10453
		PreserveUSOffsets = 4096U,
		// Token: 0x040028D6 RID: 10454
		PreserveBlobOffsets = 8192U,
		// Token: 0x040028D7 RID: 10455
		PreserveExtraSignatureData = 16384U,
		// Token: 0x040028D8 RID: 10456
		PreserveAll = 32767U,
		// Token: 0x040028D9 RID: 10457
		KeepOldMaxStack = 32768U,
		// Token: 0x040028DA RID: 10458
		AlwaysCreateGuidHeap = 65536U,
		// Token: 0x040028DB RID: 10459
		AlwaysCreateStringsHeap = 131072U,
		// Token: 0x040028DC RID: 10460
		AlwaysCreateUSHeap = 262144U,
		// Token: 0x040028DD RID: 10461
		AlwaysCreateBlobHeap = 524288U,
		// Token: 0x040028DE RID: 10462
		RoslynSortInterfaceImpl = 1048576U,
		// Token: 0x040028DF RID: 10463
		NoMethodBodies = 2097152U,
		// Token: 0x040028E0 RID: 10464
		NoDotNetResources = 4194304U,
		// Token: 0x040028E1 RID: 10465
		NoFieldData = 8388608U,
		// Token: 0x040028E2 RID: 10466
		OptimizeCustomAttributeSerializedTypeNames = 16777216U
	}
}
