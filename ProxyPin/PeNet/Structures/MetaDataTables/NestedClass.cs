using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BE5 RID: 3045
	[ComVisible(true)]
	public class NestedClass : AbstractTable
	{
		// Token: 0x06007A2A RID: 31274 RVA: 0x00240BAC File Offset: 0x00240BAC
		public NestedClass(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.NestedClassType = base.ReadSize(base.IndexSizes[Index.TypeDef]);
			this.EnclosingClassType = base.ReadSize(base.IndexSizes[Index.TypeDef]);
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x06007A2B RID: 31275 RVA: 0x00240BF8 File Offset: 0x00240BF8
		public uint NestedClassType { get; }

		// Token: 0x17001A81 RID: 6785
		// (get) Token: 0x06007A2C RID: 31276 RVA: 0x00240C00 File Offset: 0x00240C00
		public uint EnclosingClassType { get; }
	}
}
