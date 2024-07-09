using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BDD RID: 3037
	[ComVisible(true)]
	public class InterfaceImpl : AbstractTable
	{
		// Token: 0x06007A07 RID: 31239 RVA: 0x002407AC File Offset: 0x002407AC
		public InterfaceImpl(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Class = base.ReadSize(base.IndexSizes[Index.TypeDef]);
			this.Interface = base.ReadSize(base.IndexSizes[Index.TypeDefOrRef]);
		}

		// Token: 0x17001A65 RID: 6757
		// (get) Token: 0x06007A08 RID: 31240 RVA: 0x002407FC File Offset: 0x002407FC
		public uint Class { get; }

		// Token: 0x17001A66 RID: 6758
		// (get) Token: 0x06007A09 RID: 31241 RVA: 0x00240804 File Offset: 0x00240804
		public uint Interface { get; }
	}
}
