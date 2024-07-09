using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PeNet.Structures
{
	// Token: 0x02000BBF RID: 3007
	[ComVisible(true)]
	public interface IMETADATASTREAM_US
	{
		// Token: 0x170019F7 RID: 6647
		// (get) Token: 0x0600795B RID: 31067
		List<string> UserStrings { get; }

		// Token: 0x170019F8 RID: 6648
		// (get) Token: 0x0600795C RID: 31068
		List<Tuple<string, uint>> UserStringsAndIndices { get; }

		// Token: 0x0600795D RID: 31069
		string GetUserStringAtIndex(uint index);

		// Token: 0x0600795E RID: 31070
		string ToString();
	}
}
