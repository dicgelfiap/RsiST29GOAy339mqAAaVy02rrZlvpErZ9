using System;
using System.Collections.Generic;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BF8 RID: 3064
	internal class ImageImportDescriptorsParser : SafeParser<IMAGE_IMPORT_DESCRIPTOR[]>
	{
		// Token: 0x06007AAA RID: 31402 RVA: 0x002417BC File Offset: 0x002417BC
		public ImageImportDescriptorsParser(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x06007AAB RID: 31403 RVA: 0x002417C8 File Offset: 0x002417C8
		protected override IMAGE_IMPORT_DESCRIPTOR[] ParseTarget()
		{
			if (this._offset == 0U)
			{
				return null;
			}
			List<IMAGE_IMPORT_DESCRIPTOR> list = new List<IMAGE_IMPORT_DESCRIPTOR>();
			uint num = 20U;
			uint num2 = 0U;
			for (;;)
			{
				IMAGE_IMPORT_DESCRIPTOR image_IMPORT_DESCRIPTOR = new IMAGE_IMPORT_DESCRIPTOR(this._buff, this._offset + num * num2);
				if (image_IMPORT_DESCRIPTOR.OriginalFirstThunk == 0U && image_IMPORT_DESCRIPTOR.ForwarderChain == 0U && image_IMPORT_DESCRIPTOR.Name == 0U && image_IMPORT_DESCRIPTOR.FirstThunk == 0U)
				{
					break;
				}
				list.Add(image_IMPORT_DESCRIPTOR);
				num2 += 1U;
			}
			return list.ToArray();
		}
	}
}
