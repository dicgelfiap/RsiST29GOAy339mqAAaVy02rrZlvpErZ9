using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PeNet.Structures
{
	// Token: 0x02000BC1 RID: 3009
	[ComVisible(true)]
	public interface IMETADATATABLESHDR
	{
		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x06007964 RID: 31076
		// (set) Token: 0x06007965 RID: 31077
		byte HeapSizes { get; set; }

		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x06007966 RID: 31078
		List<METADATATABLESHDR.MetaDataTableInfo> TableDefinitions { get; }
	}
}
