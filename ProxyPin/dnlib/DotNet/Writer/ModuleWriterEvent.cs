using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008C7 RID: 2247
	[ComVisible(true)]
	public enum ModuleWriterEvent
	{
		// Token: 0x040029C4 RID: 10692
		Begin,
		// Token: 0x040029C5 RID: 10693
		PESectionsCreated,
		// Token: 0x040029C6 RID: 10694
		ChunksCreated,
		// Token: 0x040029C7 RID: 10695
		ChunksAddedToSections,
		// Token: 0x040029C8 RID: 10696
		MDBeginCreateTables,
		// Token: 0x040029C9 RID: 10697
		MDAllocateTypeDefRids,
		// Token: 0x040029CA RID: 10698
		MDAllocateMemberDefRids,
		// Token: 0x040029CB RID: 10699
		MDMemberDefRidsAllocated,
		// Token: 0x040029CC RID: 10700
		MDMemberDefsInitialized,
		// Token: 0x040029CD RID: 10701
		MDBeforeSortTables,
		// Token: 0x040029CE RID: 10702
		MDMostTablesSorted,
		// Token: 0x040029CF RID: 10703
		MDMemberDefCustomAttributesWritten,
		// Token: 0x040029D0 RID: 10704
		MDBeginAddResources,
		// Token: 0x040029D1 RID: 10705
		MDEndAddResources,
		// Token: 0x040029D2 RID: 10706
		MDBeginWriteMethodBodies,
		// Token: 0x040029D3 RID: 10707
		MDEndWriteMethodBodies,
		// Token: 0x040029D4 RID: 10708
		MDOnAllTablesSorted,
		// Token: 0x040029D5 RID: 10709
		MDEndCreateTables,
		// Token: 0x040029D6 RID: 10710
		BeginWritePdb,
		// Token: 0x040029D7 RID: 10711
		EndWritePdb,
		// Token: 0x040029D8 RID: 10712
		BeginCalculateRvasAndFileOffsets,
		// Token: 0x040029D9 RID: 10713
		EndCalculateRvasAndFileOffsets,
		// Token: 0x040029DA RID: 10714
		BeginWriteChunks,
		// Token: 0x040029DB RID: 10715
		EndWriteChunks,
		// Token: 0x040029DC RID: 10716
		BeginStrongNameSign,
		// Token: 0x040029DD RID: 10717
		EndStrongNameSign,
		// Token: 0x040029DE RID: 10718
		BeginWritePEChecksum,
		// Token: 0x040029DF RID: 10719
		EndWritePEChecksum,
		// Token: 0x040029E0 RID: 10720
		End
	}
}
