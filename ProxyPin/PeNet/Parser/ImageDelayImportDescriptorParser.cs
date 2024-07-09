using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BF5 RID: 3061
	internal class ImageDelayImportDescriptorParser : SafeParser<IMAGE_DELAY_IMPORT_DESCRIPTOR>
	{
		// Token: 0x06007AA4 RID: 31396 RVA: 0x0024175C File Offset: 0x0024175C
		internal ImageDelayImportDescriptorParser(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x06007AA5 RID: 31397 RVA: 0x00241768 File Offset: 0x00241768
		protected override IMAGE_DELAY_IMPORT_DESCRIPTOR ParseTarget()
		{
			return new IMAGE_DELAY_IMPORT_DESCRIPTOR(this._buff, this._offset);
		}
	}
}
