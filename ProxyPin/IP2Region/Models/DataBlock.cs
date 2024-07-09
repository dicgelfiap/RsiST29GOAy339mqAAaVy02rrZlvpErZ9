using System;
using System.Runtime.InteropServices;

namespace IP2Region.Models
{
	// Token: 0x02000A5D RID: 2653
	[ComVisible(true)]
	public class DataBlock
	{
		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x0600680E RID: 26638 RVA: 0x001FAF64 File Offset: 0x001FAF64
		// (set) Token: 0x0600680F RID: 26639 RVA: 0x001FAF6C File Offset: 0x001FAF6C
		public int CityID { get; private set; }

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06006810 RID: 26640 RVA: 0x001FAF78 File Offset: 0x001FAF78
		// (set) Token: 0x06006811 RID: 26641 RVA: 0x001FAF80 File Offset: 0x001FAF80
		public string Region { get; private set; }

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06006812 RID: 26642 RVA: 0x001FAF8C File Offset: 0x001FAF8C
		// (set) Token: 0x06006813 RID: 26643 RVA: 0x001FAF94 File Offset: 0x001FAF94
		public int DataPtr { get; private set; }

		// Token: 0x06006814 RID: 26644 RVA: 0x001FAFA0 File Offset: 0x001FAFA0
		public DataBlock(int city_id, string region, int dataPtr = 0)
		{
			this.CityID = city_id;
			this.Region = region;
			this.DataPtr = dataPtr;
		}

		// Token: 0x06006815 RID: 26645 RVA: 0x001FAFCC File Offset: 0x001FAFCC
		public DataBlock(int city_id, string region) : this(city_id, region, 0)
		{
		}

		// Token: 0x06006816 RID: 26646 RVA: 0x001FAFD8 File Offset: 0x001FAFD8
		public override string ToString()
		{
			return string.Format("{0}|{1}|{2}", this.CityID, this.Region, this.DataPtr);
		}
	}
}
