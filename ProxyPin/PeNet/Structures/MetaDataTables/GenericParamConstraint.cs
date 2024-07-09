using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BDB RID: 3035
	[ComVisible(true)]
	public class GenericParamConstraint : AbstractTable
	{
		// Token: 0x060079FF RID: 31231 RVA: 0x002406D0 File Offset: 0x002406D0
		public GenericParamConstraint(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Owner = base.ReadSize(base.IndexSizes[Index.GenericParam]);
		}

		// Token: 0x17001A5F RID: 6751
		// (get) Token: 0x06007A00 RID: 31232 RVA: 0x00240708 File Offset: 0x00240708
		public uint Owner { get; }

		// Token: 0x17001A60 RID: 6752
		// (get) Token: 0x06007A01 RID: 31233 RVA: 0x00240710 File Offset: 0x00240710
		public uint Constraint { get; }
	}
}
