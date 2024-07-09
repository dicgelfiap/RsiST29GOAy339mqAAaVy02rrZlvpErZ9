using System;
using System.Collections.Generic;
using System.Linq;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BF1 RID: 3057
	internal class ImageBaseRelocationsParser : SafeParser<IMAGE_BASE_RELOCATION[]>
	{
		// Token: 0x06007A9C RID: 31388 RVA: 0x00241634 File Offset: 0x00241634
		public ImageBaseRelocationsParser(byte[] buff, uint offset, uint directorySize) : base(buff, offset)
		{
			this._directorySize = directorySize;
		}

		// Token: 0x06007A9D RID: 31389 RVA: 0x00241648 File Offset: 0x00241648
		protected override IMAGE_BASE_RELOCATION[] ParseTarget()
		{
			if (this._offset == 0U)
			{
				return null;
			}
			List<IMAGE_BASE_RELOCATION> list = new List<IMAGE_BASE_RELOCATION>();
			for (uint num = this._offset; num < this._offset + this._directorySize - 8U; num += list.Last<IMAGE_BASE_RELOCATION>().SizeOfBlock)
			{
				list.Add(new IMAGE_BASE_RELOCATION(this._buff, num, this._directorySize));
			}
			return list.ToArray();
		}

		// Token: 0x04003B10 RID: 15120
		private readonly uint _directorySize;
	}
}
