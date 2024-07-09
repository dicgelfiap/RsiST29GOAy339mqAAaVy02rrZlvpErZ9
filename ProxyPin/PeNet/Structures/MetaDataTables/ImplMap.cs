using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BDC RID: 3036
	[ComVisible(true)]
	public class ImplMap : AbstractTable
	{
		// Token: 0x06007A02 RID: 31234 RVA: 0x00240718 File Offset: 0x00240718
		public ImplMap(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.MappingFlags = (ushort)base.ReadSize(2U);
			this.MemberForwarded = base.ReadSize(base.IndexSizes[Index.MemberForwarded]);
			this.ImportName = base.ReadSize(base.HeapSizes.String);
			this.ImportScope = base.ReadSize(base.IndexSizes[Index.ModuleRef]);
		}

		// Token: 0x17001A61 RID: 6753
		// (get) Token: 0x06007A03 RID: 31235 RVA: 0x0024078C File Offset: 0x0024078C
		public ushort MappingFlags { get; }

		// Token: 0x17001A62 RID: 6754
		// (get) Token: 0x06007A04 RID: 31236 RVA: 0x00240794 File Offset: 0x00240794
		public uint MemberForwarded { get; }

		// Token: 0x17001A63 RID: 6755
		// (get) Token: 0x06007A05 RID: 31237 RVA: 0x0024079C File Offset: 0x0024079C
		public uint ImportName { get; }

		// Token: 0x17001A64 RID: 6756
		// (get) Token: 0x06007A06 RID: 31238 RVA: 0x002407A4 File Offset: 0x002407A4
		public uint ImportScope { get; }
	}
}
