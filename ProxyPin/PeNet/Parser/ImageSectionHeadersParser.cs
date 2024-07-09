using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BFC RID: 3068
	internal class ImageSectionHeadersParser : SafeParser<IMAGE_SECTION_HEADER[]>
	{
		// Token: 0x06007AB2 RID: 31410 RVA: 0x002419D8 File Offset: 0x002419D8
		internal ImageSectionHeadersParser(byte[] buff, uint offset, ushort numOfSections, ulong imageBaseAddress) : base(buff, offset)
		{
			this._numOfSections = numOfSections;
			this._imageBaseAddress = imageBaseAddress;
		}

		// Token: 0x06007AB3 RID: 31411 RVA: 0x002419F4 File Offset: 0x002419F4
		protected override IMAGE_SECTION_HEADER[] ParseTarget()
		{
			IMAGE_SECTION_HEADER[] array = new IMAGE_SECTION_HEADER[(int)this._numOfSections];
			uint num = 40U;
			for (uint num2 = 0U; num2 < (uint)this._numOfSections; num2 += 1U)
			{
				array[(int)num2] = new IMAGE_SECTION_HEADER(this._buff, this._offset + num2 * num, this._imageBaseAddress);
			}
			return array;
		}

		// Token: 0x04003B14 RID: 15124
		private readonly ushort _numOfSections;

		// Token: 0x04003B15 RID: 15125
		private readonly ulong _imageBaseAddress;
	}
}
