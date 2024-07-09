using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BFF RID: 3071
	internal class MetaDataHdrParser : SafeParser<METADATAHDR>
	{
		// Token: 0x06007ABA RID: 31418 RVA: 0x00241D1C File Offset: 0x00241D1C
		public MetaDataHdrParser(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x06007ABB RID: 31419 RVA: 0x00241D28 File Offset: 0x00241D28
		protected override METADATAHDR ParseTarget()
		{
			return new METADATAHDR(this._buff, this._offset);
		}
	}
}
