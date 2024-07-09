using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BCE RID: 3022
	[ComVisible(true)]
	public class ClassLayout : AbstractTable
	{
		// Token: 0x060079CD RID: 31181 RVA: 0x00240128 File Offset: 0x00240128
		public ClassLayout(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.PackingSize = (ushort)base.ReadSize(2U);
			this.ClassSize = base.ReadSize(4U);
			this.Parent = base.ReadSize(base.IndexSizes[Index.TypeDef]);
		}

		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x060079CE RID: 31182 RVA: 0x00240178 File Offset: 0x00240178
		public ushort PackingSize { get; }

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x060079CF RID: 31183 RVA: 0x00240180 File Offset: 0x00240180
		public uint ClassSize { get; }

		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x060079D0 RID: 31184 RVA: 0x00240188 File Offset: 0x00240188
		public uint Parent { get; }
	}
}
