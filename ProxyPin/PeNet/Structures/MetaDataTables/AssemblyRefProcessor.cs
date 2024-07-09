using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BCD RID: 3021
	[ComVisible(true)]
	public class AssemblyRefProcessor : AbstractTable
	{
		// Token: 0x060079CA RID: 31178 RVA: 0x002400D4 File Offset: 0x002400D4
		public AssemblyRefProcessor(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Processor = base.ReadSize(4U);
			this.AssemblyRef = base.ReadSize(base.IndexSizes[Index.AssemblyRef]);
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x060079CB RID: 31179 RVA: 0x00240118 File Offset: 0x00240118
		public uint Processor { get; }

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x060079CC RID: 31180 RVA: 0x00240120 File Offset: 0x00240120
		public uint AssemblyRef { get; }
	}
}
