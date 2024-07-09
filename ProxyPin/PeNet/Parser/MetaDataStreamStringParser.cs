using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000C02 RID: 3074
	internal class MetaDataStreamStringParser : SafeParser<IMETADATASTREAM_STRING>
	{
		// Token: 0x06007AC0 RID: 31424 RVA: 0x00241DBC File Offset: 0x00241DBC
		public MetaDataStreamStringParser(byte[] buff, uint offset, uint size) : base(buff, offset)
		{
			this._size = size;
		}

		// Token: 0x06007AC1 RID: 31425 RVA: 0x00241DD0 File Offset: 0x00241DD0
		protected override IMETADATASTREAM_STRING ParseTarget()
		{
			return new METADATASTREAM_STRING(this._buff, this._offset, this._size);
		}

		// Token: 0x04003B1D RID: 15133
		private readonly uint _size;
	}
}
