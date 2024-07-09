using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BDF RID: 3039
	[ComVisible(true)]
	public class MemberRef : AbstractTable
	{
		// Token: 0x06007A0F RID: 31247 RVA: 0x00240894 File Offset: 0x00240894
		public MemberRef(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Class = base.ReadSize(base.IndexSizes[Index.MemberRefParent]);
			this.Name = base.ReadSize(base.HeapSizes.String);
			this.Signature = base.ReadSize(base.HeapSizes.Blob);
		}

		// Token: 0x17001A6B RID: 6763
		// (get) Token: 0x06007A10 RID: 31248 RVA: 0x002408F8 File Offset: 0x002408F8
		public uint Class { get; }

		// Token: 0x17001A6C RID: 6764
		// (get) Token: 0x06007A11 RID: 31249 RVA: 0x00240900 File Offset: 0x00240900
		public uint Name { get; }

		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x06007A12 RID: 31250 RVA: 0x00240908 File Offset: 0x00240908
		public uint Signature { get; }
	}
}
