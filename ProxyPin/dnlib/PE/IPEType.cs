using System;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x02000757 RID: 1879
	internal interface IPEType
	{
		// Token: 0x060041C7 RID: 16839
		RVA ToRVA(PEInfo peInfo, FileOffset offset);

		// Token: 0x060041C8 RID: 16840
		FileOffset ToFileOffset(PEInfo peInfo, RVA rva);
	}
}
