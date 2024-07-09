using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008DD RID: 2269
	[ComVisible(true)]
	public sealed class TablesHeapOptions
	{
		// Token: 0x06005850 RID: 22608 RVA: 0x001B1AF4 File Offset: 0x001B1AF4
		public static TablesHeapOptions CreatePortablePdbV1_0()
		{
			return new TablesHeapOptions
			{
				Reserved1 = new uint?(0U),
				MajorVersion = new byte?((byte)2),
				MinorVersion = new byte?(0),
				UseENC = null,
				ExtraData = null,
				HasDeletedRows = null
			};
		}

		// Token: 0x04002A83 RID: 10883
		public uint? Reserved1;

		// Token: 0x04002A84 RID: 10884
		public byte? MajorVersion;

		// Token: 0x04002A85 RID: 10885
		public byte? MinorVersion;

		// Token: 0x04002A86 RID: 10886
		public bool? UseENC;

		// Token: 0x04002A87 RID: 10887
		public uint? ExtraData;

		// Token: 0x04002A88 RID: 10888
		public bool? HasDeletedRows;
	}
}
