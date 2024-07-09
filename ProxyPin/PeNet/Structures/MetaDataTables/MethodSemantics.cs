using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BE2 RID: 3042
	[ComVisible(true)]
	public class MethodSemantics : AbstractTable
	{
		// Token: 0x06007A1E RID: 31262 RVA: 0x00240A4C File Offset: 0x00240A4C
		public MethodSemantics(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Semantics = (ushort)base.ReadSize(2U);
			this.Method = base.ReadSize(base.IndexSizes[Index.MethodDef]);
			this.Association = base.ReadSize(base.IndexSizes[Index.HasSemantics]);
		}

		// Token: 0x17001A77 RID: 6775
		// (get) Token: 0x06007A1F RID: 31263 RVA: 0x00240AA8 File Offset: 0x00240AA8
		public ushort Semantics { get; }

		// Token: 0x17001A78 RID: 6776
		// (get) Token: 0x06007A20 RID: 31264 RVA: 0x00240AB0 File Offset: 0x00240AB0
		public uint Method { get; }

		// Token: 0x17001A79 RID: 6777
		// (get) Token: 0x06007A21 RID: 31265 RVA: 0x00240AB8 File Offset: 0x00240AB8
		public uint Association { get; }
	}
}
