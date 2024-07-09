using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008B6 RID: 2230
	[ComVisible(true)]
	public enum MetadataEvent
	{
		// Token: 0x04002938 RID: 10552
		BeginCreateTables,
		// Token: 0x04002939 RID: 10553
		AllocateTypeDefRids,
		// Token: 0x0400293A RID: 10554
		AllocateMemberDefRids,
		// Token: 0x0400293B RID: 10555
		MemberDefRidsAllocated,
		// Token: 0x0400293C RID: 10556
		MemberDefsInitialized,
		// Token: 0x0400293D RID: 10557
		BeforeSortTables,
		// Token: 0x0400293E RID: 10558
		MostTablesSorted,
		// Token: 0x0400293F RID: 10559
		MemberDefCustomAttributesWritten,
		// Token: 0x04002940 RID: 10560
		BeginAddResources,
		// Token: 0x04002941 RID: 10561
		EndAddResources,
		// Token: 0x04002942 RID: 10562
		BeginWriteMethodBodies,
		// Token: 0x04002943 RID: 10563
		EndWriteMethodBodies,
		// Token: 0x04002944 RID: 10564
		OnAllTablesSorted,
		// Token: 0x04002945 RID: 10565
		EndCreateTables
	}
}
