using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BCF RID: 3023
	[ComVisible(true)]
	public class Constant : AbstractTable
	{
		// Token: 0x060079D1 RID: 31185 RVA: 0x00240190 File Offset: 0x00240190
		public Constant(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Type = (byte)base.ReadSize(1U);
			this.CurrentOffset += 1U;
			this.Parent = base.ReadSize(base.IndexSizes[Index.HasConstant]);
			this.Value = base.ReadSize(base.HeapSizes.Blob);
		}

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x060079D2 RID: 31186 RVA: 0x002401F8 File Offset: 0x002401F8
		public byte Type { get; }

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x060079D3 RID: 31187 RVA: 0x00240200 File Offset: 0x00240200
		public uint Parent { get; }

		// Token: 0x17001A3F RID: 6719
		// (get) Token: 0x060079D4 RID: 31188 RVA: 0x00240208 File Offset: 0x00240208
		public uint Value { get; }
	}
}
