using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BEB RID: 3051
	[ComVisible(true)]
	public class TypeDef : AbstractTable
	{
		// Token: 0x06007A85 RID: 31365 RVA: 0x00241064 File Offset: 0x00241064
		public TypeDef(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Flags = base.ReadSize(4U);
			this.Name = base.ReadSize(base.HeapSizes.String);
			this.Namespace = base.ReadSize(base.HeapSizes.String);
			this.Extends = base.ReadSize(base.IndexSizes[Index.TypeDefOrRef]);
			this.FieldList = base.ReadSize(base.IndexSizes[Index.Field]);
			this.MethodList = base.ReadSize(base.IndexSizes[Index.MethodDef]);
		}

		// Token: 0x17001AB0 RID: 6832
		// (get) Token: 0x06007A86 RID: 31366 RVA: 0x00241104 File Offset: 0x00241104
		public uint Flags { get; }

		// Token: 0x17001AB1 RID: 6833
		// (get) Token: 0x06007A87 RID: 31367 RVA: 0x0024110C File Offset: 0x0024110C
		public uint Name { get; }

		// Token: 0x17001AB2 RID: 6834
		// (get) Token: 0x06007A88 RID: 31368 RVA: 0x00241114 File Offset: 0x00241114
		public uint Namespace { get; }

		// Token: 0x17001AB3 RID: 6835
		// (get) Token: 0x06007A89 RID: 31369 RVA: 0x0024111C File Offset: 0x0024111C
		public uint Extends { get; }

		// Token: 0x17001AB4 RID: 6836
		// (get) Token: 0x06007A8A RID: 31370 RVA: 0x00241124 File Offset: 0x00241124
		public uint FieldList { get; }

		// Token: 0x17001AB5 RID: 6837
		// (get) Token: 0x06007A8B RID: 31371 RVA: 0x0024112C File Offset: 0x0024112C
		public uint MethodList { get; }
	}
}
