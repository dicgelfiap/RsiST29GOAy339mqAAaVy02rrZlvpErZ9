using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BE3 RID: 3043
	[ComVisible(true)]
	public class Module : AbstractTable
	{
		// Token: 0x06007A22 RID: 31266 RVA: 0x00240AC0 File Offset: 0x00240AC0
		public Module(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Generation = (ushort)base.ReadSize(2U);
			this.Name = base.ReadSize(base.HeapSizes.String);
			this.Mvid = base.ReadSize(base.HeapSizes.Guid);
			this.EncId = base.ReadSize(base.HeapSizes.Guid);
			this.EncBaseId = base.ReadSize(base.HeapSizes.Guid);
		}

		// Token: 0x17001A7A RID: 6778
		// (get) Token: 0x06007A23 RID: 31267 RVA: 0x00240B48 File Offset: 0x00240B48
		public ushort Generation { get; }

		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x06007A24 RID: 31268 RVA: 0x00240B50 File Offset: 0x00240B50
		public uint Name { get; }

		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x06007A25 RID: 31269 RVA: 0x00240B58 File Offset: 0x00240B58
		public uint Mvid { get; }

		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x06007A26 RID: 31270 RVA: 0x00240B60 File Offset: 0x00240B60
		public uint EncId { get; }

		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x06007A27 RID: 31271 RVA: 0x00240B68 File Offset: 0x00240B68
		public uint EncBaseId { get; }
	}
}
