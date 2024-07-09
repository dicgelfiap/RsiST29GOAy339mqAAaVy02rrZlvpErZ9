using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BD7 RID: 3031
	[ComVisible(true)]
	public class FieldMarshal : AbstractTable
	{
		// Token: 0x060079F0 RID: 31216 RVA: 0x00240528 File Offset: 0x00240528
		public FieldMarshal(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Parent = base.ReadSize(base.IndexSizes[Index.HasFieldMarshal]);
			this.NativeType = base.ReadSize(base.HeapSizes.Blob);
		}

		// Token: 0x17001A54 RID: 6740
		// (get) Token: 0x060079F1 RID: 31217 RVA: 0x00240574 File Offset: 0x00240574
		public uint Parent { get; }

		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x060079F2 RID: 31218 RVA: 0x0024057C File Offset: 0x0024057C
		public uint NativeType { get; }
	}
}
