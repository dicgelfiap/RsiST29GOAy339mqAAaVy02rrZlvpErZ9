using System;
using System.IO;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008B1 RID: 2225
	internal sealed class DataWriterContext
	{
		// Token: 0x0600553D RID: 21821 RVA: 0x0019F808 File Offset: 0x0019F808
		public DataWriterContext()
		{
			this.OutStream = new MemoryStream();
			this.Writer = new DataWriter(this.OutStream);
		}

		// Token: 0x040028EB RID: 10475
		public readonly MemoryStream OutStream;

		// Token: 0x040028EC RID: 10476
		public readonly DataWriter Writer;
	}
}
