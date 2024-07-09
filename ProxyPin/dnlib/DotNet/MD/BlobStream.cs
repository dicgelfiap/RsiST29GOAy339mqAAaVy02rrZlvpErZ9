using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000982 RID: 2434
	[ComVisible(true)]
	public sealed class BlobStream : HeapStream
	{
		// Token: 0x06005DD1 RID: 24017 RVA: 0x001C25D0 File Offset: 0x001C25D0
		public BlobStream()
		{
		}

		// Token: 0x06005DD2 RID: 24018 RVA: 0x001C25D8 File Offset: 0x001C25D8
		public BlobStream(DataReaderFactory mdReaderFactory, uint metadataBaseOffset, StreamHeader streamHeader) : base(mdReaderFactory, metadataBaseOffset, streamHeader)
		{
		}

		// Token: 0x06005DD3 RID: 24019 RVA: 0x001C25E4 File Offset: 0x001C25E4
		public byte[] Read(uint offset)
		{
			if (offset == 0U)
			{
				return Array2.Empty<byte>();
			}
			DataReader dataReader;
			if (!this.TryCreateReader(offset, out dataReader))
			{
				return null;
			}
			return dataReader.ToArray();
		}

		// Token: 0x06005DD4 RID: 24020 RVA: 0x001C2618 File Offset: 0x001C2618
		public byte[] ReadNoNull(uint offset)
		{
			return this.Read(offset) ?? Array2.Empty<byte>();
		}

		// Token: 0x06005DD5 RID: 24021 RVA: 0x001C2630 File Offset: 0x001C2630
		public DataReader CreateReader(uint offset)
		{
			DataReader result;
			this.TryCreateReader(offset, out result);
			return result;
		}

		// Token: 0x06005DD6 RID: 24022 RVA: 0x001C264C File Offset: 0x001C264C
		public bool TryCreateReader(uint offset, out DataReader reader)
		{
			reader = this.dataReader;
			if (!base.IsValidOffset(offset))
			{
				return false;
			}
			reader.Position = offset;
			uint length;
			if (!reader.TryReadCompressedUInt32(out length))
			{
				return false;
			}
			if (!reader.CanRead(length))
			{
				return false;
			}
			reader = reader.Slice(reader.Position, length);
			return true;
		}
	}
}
