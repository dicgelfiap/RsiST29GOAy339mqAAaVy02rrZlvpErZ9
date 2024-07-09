using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BD4 RID: 3028
	[ComVisible(true)]
	public class ExportedType : AbstractTable
	{
		// Token: 0x060079E4 RID: 31204 RVA: 0x002403D4 File Offset: 0x002403D4
		public ExportedType(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Flags = base.ReadSize(4U);
			this.TypeDefId = base.ReadSize(base.IndexSizes[Index.TypeDef]);
			this.TypeName = base.ReadSize(base.HeapSizes.String);
			this.TypeNamespace = base.ReadSize(base.HeapSizes.String);
		}

		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x060079E5 RID: 31205 RVA: 0x00240444 File Offset: 0x00240444
		public uint Flags { get; }

		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x060079E6 RID: 31206 RVA: 0x0024044C File Offset: 0x0024044C
		public uint TypeDefId { get; }

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x060079E7 RID: 31207 RVA: 0x00240454 File Offset: 0x00240454
		public uint TypeName { get; }

		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x060079E8 RID: 31208 RVA: 0x0024045C File Offset: 0x0024045C
		public uint TypeNamespace { get; }
	}
}
