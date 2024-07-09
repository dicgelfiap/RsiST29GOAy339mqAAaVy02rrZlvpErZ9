using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BE0 RID: 3040
	[ComVisible(true)]
	public class MethodDef : AbstractTable
	{
		// Token: 0x06007A13 RID: 31251 RVA: 0x00240910 File Offset: 0x00240910
		public MethodDef(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.RVA = base.ReadSize(4U);
			this.ImplFlags = (ushort)base.ReadSize(2U);
			this.Flags = (ushort)base.ReadSize(2U);
			this.Name = base.ReadSize(base.HeapSizes.String);
			this.Signature = base.ReadSize(base.HeapSizes.Blob);
			this.ParamList = base.ReadSize(base.IndexSizes[Index.Param]);
		}

		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x06007A14 RID: 31252 RVA: 0x0024099C File Offset: 0x0024099C
		public uint RVA { get; }

		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x06007A15 RID: 31253 RVA: 0x002409A4 File Offset: 0x002409A4
		public ushort ImplFlags { get; }

		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x06007A16 RID: 31254 RVA: 0x002409AC File Offset: 0x002409AC
		public ushort Flags { get; }

		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x06007A17 RID: 31255 RVA: 0x002409B4 File Offset: 0x002409B4
		public uint Name { get; }

		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x06007A18 RID: 31256 RVA: 0x002409BC File Offset: 0x002409BC
		public uint Signature { get; }

		// Token: 0x17001A73 RID: 6771
		// (get) Token: 0x06007A19 RID: 31257 RVA: 0x002409C4 File Offset: 0x002409C4
		public uint ParamList { get; }
	}
}
