using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BF9 RID: 3065
	internal class ImageLoadConfigDirectoryParser : SafeParser<IMAGE_LOAD_CONFIG_DIRECTORY>
	{
		// Token: 0x06007AAC RID: 31404 RVA: 0x00241848 File Offset: 0x00241848
		internal ImageLoadConfigDirectoryParser(byte[] buff, uint offset, bool is64Bit) : base(buff, offset)
		{
			this._is64Bit = is64Bit;
		}

		// Token: 0x06007AAD RID: 31405 RVA: 0x0024185C File Offset: 0x0024185C
		protected override IMAGE_LOAD_CONFIG_DIRECTORY ParseTarget()
		{
			return new IMAGE_LOAD_CONFIG_DIRECTORY(this._buff, this._offset, this._is64Bit);
		}

		// Token: 0x04003B12 RID: 15122
		private readonly bool _is64Bit;
	}
}
