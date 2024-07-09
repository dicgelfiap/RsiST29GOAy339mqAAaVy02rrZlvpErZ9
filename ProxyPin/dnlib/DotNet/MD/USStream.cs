using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009D7 RID: 2519
	[ComVisible(true)]
	public sealed class USStream : HeapStream
	{
		// Token: 0x06006095 RID: 24725 RVA: 0x001CC768 File Offset: 0x001CC768
		public USStream()
		{
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x001CC770 File Offset: 0x001CC770
		public USStream(DataReaderFactory mdReaderFactory, uint metadataBaseOffset, StreamHeader streamHeader) : base(mdReaderFactory, metadataBaseOffset, streamHeader)
		{
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x001CC77C File Offset: 0x001CC77C
		public string Read(uint offset)
		{
			if (offset == 0U)
			{
				return string.Empty;
			}
			if (!base.IsValidOffset(offset))
			{
				return null;
			}
			DataReader dataReader = this.dataReader;
			dataReader.Position = offset;
			uint num;
			if (!dataReader.TryReadCompressedUInt32(out num))
			{
				return null;
			}
			if (!dataReader.CanRead(num))
			{
				return null;
			}
			string result;
			try
			{
				result = dataReader.ReadUtf16String((int)(num / 2U));
			}
			catch (OutOfMemoryException)
			{
				throw;
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x001CC810 File Offset: 0x001CC810
		public string ReadNoNull(uint offset)
		{
			return this.Read(offset) ?? string.Empty;
		}
	}
}
