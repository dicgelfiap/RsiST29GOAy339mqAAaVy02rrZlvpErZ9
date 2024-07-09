using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000988 RID: 2440
	[ComVisible(true)]
	public class CustomDotNetStream : DotNetStream
	{
		// Token: 0x06005DFB RID: 24059 RVA: 0x001C3000 File Offset: 0x001C3000
		public CustomDotNetStream()
		{
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x001C3008 File Offset: 0x001C3008
		public CustomDotNetStream(DataReaderFactory mdReaderFactory, uint metadataBaseOffset, StreamHeader streamHeader) : base(mdReaderFactory, metadataBaseOffset, streamHeader)
		{
		}
	}
}
