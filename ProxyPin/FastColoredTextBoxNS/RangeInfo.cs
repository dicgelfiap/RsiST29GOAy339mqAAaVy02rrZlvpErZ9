using System;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A2E RID: 2606
	internal class RangeInfo
	{
		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x0600664B RID: 26187 RVA: 0x001F02FC File Offset: 0x001F02FC
		// (set) Token: 0x0600664C RID: 26188 RVA: 0x001F0304 File Offset: 0x001F0304
		public Place Start { get; set; }

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x0600664D RID: 26189 RVA: 0x001F0310 File Offset: 0x001F0310
		// (set) Token: 0x0600664E RID: 26190 RVA: 0x001F0318 File Offset: 0x001F0318
		public Place End { get; set; }

		// Token: 0x0600664F RID: 26191 RVA: 0x001F0324 File Offset: 0x001F0324
		public RangeInfo(Range r)
		{
			this.Start = r.Start;
			this.End = r.End;
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06006650 RID: 26192 RVA: 0x001F0358 File Offset: 0x001F0358
		internal int FromX
		{
			get
			{
				bool flag = this.End.iLine < this.Start.iLine;
				int result;
				if (flag)
				{
					result = this.End.iChar;
				}
				else
				{
					bool flag2 = this.End.iLine > this.Start.iLine;
					if (flag2)
					{
						result = this.Start.iChar;
					}
					else
					{
						result = Math.Min(this.End.iChar, this.Start.iChar);
					}
				}
				return result;
			}
		}
	}
}
