using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PeNet.Structures
{
	// Token: 0x02000BBB RID: 3003
	[ComVisible(true)]
	public interface IMETADATASTREAM_GUID
	{
		// Token: 0x170019EF RID: 6639
		// (get) Token: 0x06007949 RID: 31049
		List<Guid> Guids { get; }

		// Token: 0x170019F0 RID: 6640
		// (get) Token: 0x0600794A RID: 31050
		List<Tuple<Guid, uint>> GuidsAndIndices { get; }

		// Token: 0x0600794B RID: 31051
		Guid? GetGuidAtIndex(uint index);

		// Token: 0x0600794C RID: 31052
		string ToString();
	}
}
