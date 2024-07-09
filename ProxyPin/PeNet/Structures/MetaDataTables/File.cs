using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BD9 RID: 3033
	[ComVisible(true)]
	public class File : AbstractTable
	{
		// Token: 0x060079F6 RID: 31222 RVA: 0x002405D8 File Offset: 0x002405D8
		public File(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Flags = base.ReadSize(4U);
			this.Name = base.ReadSize(base.HeapSizes.String);
			this.HashValue = base.ReadSize(base.HeapSizes.Blob);
		}

		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x060079F7 RID: 31223 RVA: 0x00240630 File Offset: 0x00240630
		public uint Flags { get; }

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x060079F8 RID: 31224 RVA: 0x00240638 File Offset: 0x00240638
		public uint Name { get; }

		// Token: 0x17001A5A RID: 6746
		// (get) Token: 0x060079F9 RID: 31225 RVA: 0x00240640 File Offset: 0x00240640
		public uint HashValue { get; }
	}
}
