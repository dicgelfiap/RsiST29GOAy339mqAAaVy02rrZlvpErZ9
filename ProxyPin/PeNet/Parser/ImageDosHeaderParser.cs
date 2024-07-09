using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BF6 RID: 3062
	internal class ImageDosHeaderParser : SafeParser<IMAGE_DOS_HEADER>
	{
		// Token: 0x06007AA6 RID: 31398 RVA: 0x0024177C File Offset: 0x0024177C
		internal ImageDosHeaderParser(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x06007AA7 RID: 31399 RVA: 0x00241788 File Offset: 0x00241788
		protected override IMAGE_DOS_HEADER ParseTarget()
		{
			return new IMAGE_DOS_HEADER(this._buff, this._offset);
		}
	}
}
