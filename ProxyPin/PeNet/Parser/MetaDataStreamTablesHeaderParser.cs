using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000C03 RID: 3075
	internal class MetaDataStreamTablesHeaderParser : SafeParser<METADATATABLESHDR>
	{
		// Token: 0x06007AC2 RID: 31426 RVA: 0x00241DEC File Offset: 0x00241DEC
		public MetaDataStreamTablesHeaderParser(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x06007AC3 RID: 31427 RVA: 0x00241DF8 File Offset: 0x00241DF8
		protected override METADATATABLESHDR ParseTarget()
		{
			return new METADATATABLESHDR(this._buff, this._offset);
		}
	}
}
