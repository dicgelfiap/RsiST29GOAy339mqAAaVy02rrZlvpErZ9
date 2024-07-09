using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BDE RID: 3038
	[ComVisible(true)]
	public class ManifestResource : AbstractTable
	{
		// Token: 0x06007A0A RID: 31242 RVA: 0x0024080C File Offset: 0x0024080C
		public ManifestResource(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Offset = base.ReadSize(4U);
			this.Flags = base.ReadSize(4U);
			this.Name = base.ReadSize(base.HeapSizes.String);
			this.Implementation = base.ReadSize(base.IndexSizes[Index.Implementation]);
		}

		// Token: 0x17001A67 RID: 6759
		// (get) Token: 0x06007A0B RID: 31243 RVA: 0x00240874 File Offset: 0x00240874
		public new uint Offset { get; }

		// Token: 0x17001A68 RID: 6760
		// (get) Token: 0x06007A0C RID: 31244 RVA: 0x0024087C File Offset: 0x0024087C
		public uint Flags { get; }

		// Token: 0x17001A69 RID: 6761
		// (get) Token: 0x06007A0D RID: 31245 RVA: 0x00240884 File Offset: 0x00240884
		public uint Name { get; }

		// Token: 0x17001A6A RID: 6762
		// (get) Token: 0x06007A0E RID: 31246 RVA: 0x0024088C File Offset: 0x0024088C
		public uint Implementation { get; }
	}
}
