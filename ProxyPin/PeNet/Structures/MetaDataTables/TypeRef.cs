using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BEC RID: 3052
	[ComVisible(true)]
	public class TypeRef : AbstractTable
	{
		// Token: 0x06007A8C RID: 31372 RVA: 0x00241134 File Offset: 0x00241134
		public TypeRef(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.ResolutionScope = base.ReadSize(base.IndexSizes[Index.ResolutionScope]);
			this.TypeName = base.ReadSize(base.HeapSizes.String);
			this.TypeNamespace = base.ReadSize(base.HeapSizes.String);
		}

		// Token: 0x17001AB6 RID: 6838
		// (get) Token: 0x06007A8D RID: 31373 RVA: 0x00241198 File Offset: 0x00241198
		public uint ResolutionScope { get; }

		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x06007A8E RID: 31374 RVA: 0x002411A0 File Offset: 0x002411A0
		public uint TypeName { get; }

		// Token: 0x17001AB8 RID: 6840
		// (get) Token: 0x06007A8F RID: 31375 RVA: 0x002411A8 File Offset: 0x002411A8
		public uint TypeNamespace { get; }
	}
}
