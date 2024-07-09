using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x02000753 RID: 1875
	[ComVisible(true)]
	public interface IRvaFileOffsetConverter
	{
		// Token: 0x060041AE RID: 16814
		RVA ToRVA(FileOffset offset);

		// Token: 0x060041AF RID: 16815
		FileOffset ToFileOffset(RVA rva);
	}
}
