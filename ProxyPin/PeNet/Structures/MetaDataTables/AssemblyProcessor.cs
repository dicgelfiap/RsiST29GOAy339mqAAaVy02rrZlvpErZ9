using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BCA RID: 3018
	[ComVisible(true)]
	public class AssemblyProcessor : AbstractTable
	{
		// Token: 0x060079B9 RID: 31161 RVA: 0x0023FF2C File Offset: 0x0023FF2C
		public AssemblyProcessor(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Processor = base.ReadSize(4U);
		}

		// Token: 0x17001A2A RID: 6698
		// (get) Token: 0x060079BA RID: 31162 RVA: 0x0023FF48 File Offset: 0x0023FF48
		public uint Processor { get; }
	}
}
