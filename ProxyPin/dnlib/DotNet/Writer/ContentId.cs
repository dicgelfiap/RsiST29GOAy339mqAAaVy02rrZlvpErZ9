using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008C2 RID: 2242
	[ComVisible(true)]
	public readonly struct ContentId
	{
		// Token: 0x060056BA RID: 22202 RVA: 0x001A7E6C File Offset: 0x001A7E6C
		public ContentId(Guid guid, uint timestamp)
		{
			this.Guid = guid;
			this.Timestamp = timestamp;
		}

		// Token: 0x0400298F RID: 10639
		public readonly Guid Guid;

		// Token: 0x04002990 RID: 10640
		public readonly uint Timestamp;
	}
}
