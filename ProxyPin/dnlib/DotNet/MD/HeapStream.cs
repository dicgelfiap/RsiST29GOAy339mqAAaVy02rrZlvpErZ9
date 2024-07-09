using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x0200098A RID: 2442
	[ComVisible(true)]
	public abstract class HeapStream : DotNetStream
	{
		// Token: 0x06005E0D RID: 24077 RVA: 0x001C3200 File Offset: 0x001C3200
		protected HeapStream()
		{
		}

		// Token: 0x06005E0E RID: 24078 RVA: 0x001C3208 File Offset: 0x001C3208
		protected HeapStream(DataReaderFactory mdReaderFactory, uint metadataBaseOffset, StreamHeader streamHeader) : base(mdReaderFactory, metadataBaseOffset, streamHeader)
		{
		}
	}
}
