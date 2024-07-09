using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BD3 RID: 3027
	[ComVisible(true)]
	public class EventMap : AbstractTable
	{
		// Token: 0x060079E1 RID: 31201 RVA: 0x00240378 File Offset: 0x00240378
		public EventMap(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Parent = base.ReadSize(base.IndexSizes[Index.TypeDef]);
			this.EventList = base.ReadSize(base.IndexSizes[Index.Event]);
		}

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x060079E2 RID: 31202 RVA: 0x002403C4 File Offset: 0x002403C4
		public uint Parent { get; }

		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x060079E3 RID: 31203 RVA: 0x002403CC File Offset: 0x002403CC
		public uint EventList { get; }
	}
}
