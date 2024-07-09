using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PeNet.Structures
{
	// Token: 0x02000BBD RID: 3005
	[ComVisible(true)]
	public interface IMETADATASTREAM_STRING
	{
		// Token: 0x170019F3 RID: 6643
		// (get) Token: 0x06007952 RID: 31058
		List<string> Strings { get; }

		// Token: 0x170019F4 RID: 6644
		// (get) Token: 0x06007953 RID: 31059
		List<Tuple<string, uint>> StringsAndIndices { get; }

		// Token: 0x06007954 RID: 31060
		string GetStringAtIndex(uint index);

		// Token: 0x06007955 RID: 31061
		string ToString();
	}
}
