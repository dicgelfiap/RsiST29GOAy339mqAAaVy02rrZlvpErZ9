using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BD6 RID: 3030
	[ComVisible(true)]
	public class FieldLayout : AbstractTable
	{
		// Token: 0x060079ED RID: 31213 RVA: 0x002404D4 File Offset: 0x002404D4
		public FieldLayout(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Offset = base.ReadSize(4U);
			this.Field = base.ReadSize(base.IndexSizes[Index.Field]);
		}

		// Token: 0x17001A52 RID: 6738
		// (get) Token: 0x060079EE RID: 31214 RVA: 0x00240518 File Offset: 0x00240518
		public new uint Offset { get; }

		// Token: 0x17001A53 RID: 6739
		// (get) Token: 0x060079EF RID: 31215 RVA: 0x00240520 File Offset: 0x00240520
		public uint Field { get; }
	}
}
