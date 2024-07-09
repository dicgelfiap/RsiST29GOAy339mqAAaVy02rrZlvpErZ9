using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009D3 RID: 2515
	[ComVisible(true)]
	public sealed class StringsStream : HeapStream
	{
		// Token: 0x06005FCA RID: 24522 RVA: 0x001C9F28 File Offset: 0x001C9F28
		public StringsStream()
		{
		}

		// Token: 0x06005FCB RID: 24523 RVA: 0x001C9F30 File Offset: 0x001C9F30
		public StringsStream(DataReaderFactory mdReaderFactory, uint metadataBaseOffset, StreamHeader streamHeader) : base(mdReaderFactory, metadataBaseOffset, streamHeader)
		{
		}

		// Token: 0x06005FCC RID: 24524 RVA: 0x001C9F3C File Offset: 0x001C9F3C
		public UTF8String Read(uint offset)
		{
			if (offset >= base.StreamLength)
			{
				return null;
			}
			DataReader dataReader = this.dataReader;
			dataReader.Position = offset;
			byte[] array = dataReader.TryReadBytesUntil(0);
			if (array == null)
			{
				return null;
			}
			return new UTF8String(array);
		}

		// Token: 0x06005FCD RID: 24525 RVA: 0x001C9F84 File Offset: 0x001C9F84
		public UTF8String ReadNoNull(uint offset)
		{
			return this.Read(offset) ?? UTF8String.Empty;
		}
	}
}
