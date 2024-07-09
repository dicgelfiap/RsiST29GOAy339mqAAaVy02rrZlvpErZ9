using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000C04 RID: 3076
	internal class MetaDataStreamUSParser : SafeParser<IMETADATASTREAM_US>
	{
		// Token: 0x06007AC4 RID: 31428 RVA: 0x00241E0C File Offset: 0x00241E0C
		public MetaDataStreamUSParser(byte[] buff, uint offset, uint size) : base(buff, offset)
		{
			this._size = size;
		}

		// Token: 0x06007AC5 RID: 31429 RVA: 0x00241E20 File Offset: 0x00241E20
		protected override IMETADATASTREAM_US ParseTarget()
		{
			return new METADATASTREAM_US(this._buff, this._offset, this._size);
		}

		// Token: 0x04003B1E RID: 15134
		private readonly uint _size;
	}
}
