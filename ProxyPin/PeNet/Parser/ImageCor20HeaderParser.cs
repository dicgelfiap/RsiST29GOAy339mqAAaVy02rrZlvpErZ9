using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BF3 RID: 3059
	internal class ImageCor20HeaderParser : SafeParser<IMAGE_COR20_HEADER>
	{
		// Token: 0x06007AA0 RID: 31392 RVA: 0x002416D8 File Offset: 0x002416D8
		public ImageCor20HeaderParser(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x06007AA1 RID: 31393 RVA: 0x002416E4 File Offset: 0x002416E4
		protected override IMAGE_COR20_HEADER ParseTarget()
		{
			return new IMAGE_COR20_HEADER(this._buff, this._offset);
		}
	}
}
