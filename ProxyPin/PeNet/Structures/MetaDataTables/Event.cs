using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BD2 RID: 3026
	[ComVisible(true)]
	public class Event : AbstractTable
	{
		// Token: 0x060079DD RID: 31197 RVA: 0x00240304 File Offset: 0x00240304
		public Event(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.EventFlags = (ushort)base.ReadSize(2U);
			this.Name = base.ReadSize(base.HeapSizes.String);
			this.EventType = base.ReadSize(base.IndexSizes[Index.TypeDefOrRef]);
		}

		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x060079DE RID: 31198 RVA: 0x00240360 File Offset: 0x00240360
		public ushort EventFlags { get; }

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x060079DF RID: 31199 RVA: 0x00240368 File Offset: 0x00240368
		public uint Name { get; }

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x060079E0 RID: 31200 RVA: 0x00240370 File Offset: 0x00240370
		public uint EventType { get; }
	}
}
