using System;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008D5 RID: 2261
	internal readonly struct SectionSizeInfo
	{
		// Token: 0x0600580D RID: 22541 RVA: 0x001B052C File Offset: 0x001B052C
		public SectionSizeInfo(uint length, uint characteristics)
		{
			this.length = length;
			this.characteristics = characteristics;
		}

		// Token: 0x04002A57 RID: 10839
		public readonly uint length;

		// Token: 0x04002A58 RID: 10840
		public readonly uint characteristics;
	}
}
