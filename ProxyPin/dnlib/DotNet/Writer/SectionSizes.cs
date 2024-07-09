using System;
using System.Collections.Generic;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008D6 RID: 2262
	internal readonly struct SectionSizes
	{
		// Token: 0x0600580E RID: 22542 RVA: 0x001B053C File Offset: 0x001B053C
		public static uint GetSizeOfHeaders(uint fileAlignment, uint headerLen)
		{
			return Utils.AlignUp(headerLen, fileAlignment);
		}

		// Token: 0x0600580F RID: 22543 RVA: 0x001B0548 File Offset: 0x001B0548
		public SectionSizes(uint fileAlignment, uint sectionAlignment, uint headerLen, Func<IEnumerable<SectionSizeInfo>> getSectionSizeInfos)
		{
			this.SizeOfHeaders = SectionSizes.GetSizeOfHeaders(fileAlignment, headerLen);
			this.SizeOfImage = Utils.AlignUp(this.SizeOfHeaders, sectionAlignment);
			this.BaseOfData = 0U;
			this.BaseOfCode = 0U;
			this.SizeOfCode = 0U;
			this.SizeOfInitdData = 0U;
			this.SizeOfUninitdData = 0U;
			foreach (SectionSizeInfo sectionSizeInfo in getSectionSizeInfos())
			{
				uint num = Utils.AlignUp(sectionSizeInfo.length, sectionAlignment);
				uint num2 = Utils.AlignUp(sectionSizeInfo.length, fileAlignment);
				bool flag = (sectionSizeInfo.characteristics & 32U) > 0U;
				bool flag2 = (sectionSizeInfo.characteristics & 64U) > 0U;
				bool flag3 = (sectionSizeInfo.characteristics & 128U) > 0U;
				if (this.BaseOfCode == 0U && flag)
				{
					this.BaseOfCode = this.SizeOfImage;
				}
				if (this.BaseOfData == 0U && (flag2 || flag3))
				{
					this.BaseOfData = this.SizeOfImage;
				}
				if (flag)
				{
					this.SizeOfCode += num2;
				}
				if (flag2)
				{
					this.SizeOfInitdData += num2;
				}
				if (flag3)
				{
					this.SizeOfUninitdData += num2;
				}
				this.SizeOfImage += num;
			}
		}

		// Token: 0x04002A59 RID: 10841
		public readonly uint SizeOfHeaders;

		// Token: 0x04002A5A RID: 10842
		public readonly uint SizeOfImage;

		// Token: 0x04002A5B RID: 10843
		public readonly uint BaseOfData;

		// Token: 0x04002A5C RID: 10844
		public readonly uint BaseOfCode;

		// Token: 0x04002A5D RID: 10845
		public readonly uint SizeOfCode;

		// Token: 0x04002A5E RID: 10846
		public readonly uint SizeOfInitdData;

		// Token: 0x04002A5F RID: 10847
		public readonly uint SizeOfUninitdData;
	}
}
