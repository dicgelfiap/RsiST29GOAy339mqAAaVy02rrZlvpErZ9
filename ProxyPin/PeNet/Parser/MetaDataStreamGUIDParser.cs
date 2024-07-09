using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000C01 RID: 3073
	internal class MetaDataStreamGUIDParser : SafeParser<IMETADATASTREAM_GUID>
	{
		// Token: 0x06007ABE RID: 31422 RVA: 0x00241D8C File Offset: 0x00241D8C
		public MetaDataStreamGUIDParser(byte[] buff, uint offset, uint size) : base(buff, offset)
		{
			this._size = size;
		}

		// Token: 0x06007ABF RID: 31423 RVA: 0x00241DA0 File Offset: 0x00241DA0
		protected override IMETADATASTREAM_GUID ParseTarget()
		{
			return new METADATASTREAM_GUID(this._buff, this._offset, this._size);
		}

		// Token: 0x04003B1C RID: 15132
		private readonly uint _size;
	}
}
