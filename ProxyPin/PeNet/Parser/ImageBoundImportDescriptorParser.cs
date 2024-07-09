using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BF2 RID: 3058
	internal class ImageBoundImportDescriptorParser : SafeParser<IMAGE_BOUND_IMPORT_DESCRIPTOR>
	{
		// Token: 0x06007A9E RID: 31390 RVA: 0x002416B8 File Offset: 0x002416B8
		internal ImageBoundImportDescriptorParser(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x06007A9F RID: 31391 RVA: 0x002416C4 File Offset: 0x002416C4
		protected override IMAGE_BOUND_IMPORT_DESCRIPTOR ParseTarget()
		{
			return new IMAGE_BOUND_IMPORT_DESCRIPTOR(this._buff, this._offset);
		}
	}
}
