using System;
using PeNet.Structures;

namespace PeNet.Parser
{
	// Token: 0x02000BF4 RID: 3060
	internal class ImageDebugDirectoryParser : SafeParser<IMAGE_DEBUG_DIRECTORY[]>
	{
		// Token: 0x06007AA2 RID: 31394 RVA: 0x002416F8 File Offset: 0x002416F8
		internal ImageDebugDirectoryParser(byte[] buff, uint offset, uint size) : base(buff, offset)
		{
			this.size = size;
		}

		// Token: 0x06007AA3 RID: 31395 RVA: 0x0024170C File Offset: 0x0024170C
		protected override IMAGE_DEBUG_DIRECTORY[] ParseTarget()
		{
			uint num = this.size / 28U;
			IMAGE_DEBUG_DIRECTORY[] array = new IMAGE_DEBUG_DIRECTORY[num];
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				array[(int)num2] = new IMAGE_DEBUG_DIRECTORY(this._buff, this._offset + num2 * 28U);
			}
			return array;
		}

		// Token: 0x04003B11 RID: 15121
		private readonly uint size;
	}
}
