using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BE6 RID: 3046
	[ComVisible(true)]
	public class Param : AbstractTable
	{
		// Token: 0x06007A2D RID: 31277 RVA: 0x00240C08 File Offset: 0x00240C08
		public Param(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Flags = (ushort)base.ReadSize(2U);
			this.Sequence = (ushort)base.ReadSize(2U);
			this.Name = base.ReadSize(base.HeapSizes.String);
		}

		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x06007A2E RID: 31278 RVA: 0x00240C58 File Offset: 0x00240C58
		public ushort Flags { get; }

		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x06007A2F RID: 31279 RVA: 0x00240C60 File Offset: 0x00240C60
		public ushort Sequence { get; }

		// Token: 0x17001A84 RID: 6788
		// (get) Token: 0x06007A30 RID: 31280 RVA: 0x00240C68 File Offset: 0x00240C68
		public uint Name { get; }
	}
}
