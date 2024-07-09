using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BE4 RID: 3044
	[ComVisible(true)]
	public class ModuleRef : AbstractTable
	{
		// Token: 0x06007A28 RID: 31272 RVA: 0x00240B70 File Offset: 0x00240B70
		public ModuleRef(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Name = base.ReadSize(base.HeapSizes.String);
		}

		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x06007A29 RID: 31273 RVA: 0x00240BA4 File Offset: 0x00240BA4
		public uint Name { get; }
	}
}
