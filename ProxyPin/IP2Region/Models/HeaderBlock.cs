using System;

namespace IP2Region.Models
{
	// Token: 0x02000A60 RID: 2656
	internal class HeaderBlock
	{
		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06006820 RID: 26656 RVA: 0x001FB09C File Offset: 0x001FB09C
		// (set) Token: 0x06006821 RID: 26657 RVA: 0x001FB0A4 File Offset: 0x001FB0A4
		public long IndexStartIp { get; private set; }

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06006822 RID: 26658 RVA: 0x001FB0B0 File Offset: 0x001FB0B0
		// (set) Token: 0x06006823 RID: 26659 RVA: 0x001FB0B8 File Offset: 0x001FB0B8
		public int IndexPtr { get; private set; }

		// Token: 0x06006824 RID: 26660 RVA: 0x001FB0C4 File Offset: 0x001FB0C4
		public HeaderBlock(long indexStartIp, int indexPtr)
		{
			this.IndexStartIp = indexStartIp;
			this.IndexPtr = indexPtr;
		}

		// Token: 0x06006825 RID: 26661 RVA: 0x001FB0DC File Offset: 0x001FB0DC
		public byte[] GetBytes()
		{
			byte[] array = new byte[8];
			Utils.WriteIntLong(array, 0, this.IndexStartIp);
			Utils.WriteIntLong(array, 4, (long)this.IndexPtr);
			return array;
		}
	}
}
