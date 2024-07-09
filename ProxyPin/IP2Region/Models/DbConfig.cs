using System;
using System.Runtime.InteropServices;

namespace IP2Region.Models
{
	// Token: 0x02000A5F RID: 2655
	[ComVisible(true)]
	public class DbConfig
	{
		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x0600681A RID: 26650 RVA: 0x001FB034 File Offset: 0x001FB034
		// (set) Token: 0x0600681B RID: 26651 RVA: 0x001FB03C File Offset: 0x001FB03C
		public int TotalHeaderSize { get; private set; }

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x0600681C RID: 26652 RVA: 0x001FB048 File Offset: 0x001FB048
		// (set) Token: 0x0600681D RID: 26653 RVA: 0x001FB050 File Offset: 0x001FB050
		public int indexBlockSize { get; private set; }

		// Token: 0x0600681E RID: 26654 RVA: 0x001FB05C File Offset: 0x001FB05C
		public DbConfig(int totalHeaderSize)
		{
			if (totalHeaderSize % 8 != 0)
			{
				throw new DbMakerConfigException("totalHeaderSize must be times of 8");
			}
			this.TotalHeaderSize = totalHeaderSize;
			this.indexBlockSize = 8192;
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x001FB08C File Offset: 0x001FB08C
		public DbConfig() : this(16384)
		{
		}
	}
}
