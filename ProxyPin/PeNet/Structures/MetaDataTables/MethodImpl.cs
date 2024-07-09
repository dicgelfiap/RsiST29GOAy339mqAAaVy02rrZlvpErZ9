using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BE1 RID: 3041
	[ComVisible(true)]
	public class MethodImpl : AbstractTable
	{
		// Token: 0x06007A1A RID: 31258 RVA: 0x002409CC File Offset: 0x002409CC
		public MethodImpl(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Class = base.ReadSize(base.IndexSizes[Index.TypeDef]);
			this.MethodBody = base.ReadSize(base.IndexSizes[Index.MethodDefOrRef]);
			this.MethodDeclaration = base.ReadSize(base.IndexSizes[Index.MethodDefOrRef]);
		}

		// Token: 0x17001A74 RID: 6772
		// (get) Token: 0x06007A1B RID: 31259 RVA: 0x00240A34 File Offset: 0x00240A34
		public uint Class { get; }

		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x06007A1C RID: 31260 RVA: 0x00240A3C File Offset: 0x00240A3C
		public uint MethodBody { get; }

		// Token: 0x17001A76 RID: 6774
		// (get) Token: 0x06007A1D RID: 31261 RVA: 0x00240A44 File Offset: 0x00240A44
		public uint MethodDeclaration { get; }
	}
}
