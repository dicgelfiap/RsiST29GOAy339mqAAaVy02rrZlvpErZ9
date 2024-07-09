using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BD8 RID: 3032
	[ComVisible(true)]
	public class FieldRVA : AbstractTable
	{
		// Token: 0x060079F3 RID: 31219 RVA: 0x00240584 File Offset: 0x00240584
		public FieldRVA(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.RVA = base.ReadSize(4U);
			this.Field = base.ReadSize(base.IndexSizes[Index.Field]);
		}

		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x060079F4 RID: 31220 RVA: 0x002405C8 File Offset: 0x002405C8
		public uint RVA { get; }

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x060079F5 RID: 31221 RVA: 0x002405D0 File Offset: 0x002405D0
		public uint Field { get; }
	}
}
