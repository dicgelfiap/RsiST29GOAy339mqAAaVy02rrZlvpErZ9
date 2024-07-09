using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BDA RID: 3034
	[ComVisible(true)]
	public class GenericParam : AbstractTable
	{
		// Token: 0x060079FA RID: 31226 RVA: 0x00240648 File Offset: 0x00240648
		public GenericParam(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Number = (ushort)base.ReadSize(2U);
			this.Flags = (ushort)base.ReadSize(2U);
			this.Owner = base.ReadSize(base.IndexSizes[Index.TypeOrMethodDef]);
			this.Name = base.ReadSize(base.HeapSizes.String);
		}

		// Token: 0x17001A5B RID: 6747
		// (get) Token: 0x060079FB RID: 31227 RVA: 0x002406B0 File Offset: 0x002406B0
		public ushort Number { get; }

		// Token: 0x17001A5C RID: 6748
		// (get) Token: 0x060079FC RID: 31228 RVA: 0x002406B8 File Offset: 0x002406B8
		public ushort Flags { get; }

		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x060079FD RID: 31229 RVA: 0x002406C0 File Offset: 0x002406C0
		public uint Owner { get; }

		// Token: 0x17001A5E RID: 6750
		// (get) Token: 0x060079FE RID: 31230 RVA: 0x002406C8 File Offset: 0x002406C8
		public uint Name { get; }
	}
}
