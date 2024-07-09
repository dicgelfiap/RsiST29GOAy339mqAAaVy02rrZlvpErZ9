using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BFA RID: 3066
	internal class ImageNtHeadersParser : SafeParser<IMAGE_NT_HEADERS>
	{
		// Token: 0x06007AAE RID: 31406 RVA: 0x00241878 File Offset: 0x00241878
		internal ImageNtHeadersParser(byte[] buff, uint offset, bool is64Bit) : base(buff, offset)
		{
			this._is64Bit = is64Bit;
		}

		// Token: 0x06007AAF RID: 31407 RVA: 0x0024188C File Offset: 0x0024188C
		protected override IMAGE_NT_HEADERS ParseTarget()
		{
			return new IMAGE_NT_HEADERS(this._buff, this._offset, this._is64Bit);
		}

		// Token: 0x04003B13 RID: 15123
		private readonly bool _is64Bit;
	}
}
