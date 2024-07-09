using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BC8 RID: 3016
	[ComVisible(true)]
	public class Assembly : AbstractTable
	{
		// Token: 0x060079AB RID: 31147 RVA: 0x0023FDD4 File Offset: 0x0023FDD4
		public Assembly(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.HashAlgId = base.ReadSize(4U);
			this.MajorVersion = (ushort)base.ReadSize(2U);
			this.MinorVersion = (ushort)base.ReadSize(2U);
			this.BuildNumber = (ushort)base.ReadSize(2U);
			this.RevisionNumber = (ushort)base.ReadSize(2U);
			this.Flags = base.ReadSize(4U);
			this.PublicKey = base.ReadSize(base.HeapSizes.Blob);
			this.Name = base.ReadSize(base.HeapSizes.String);
			this.Culture = base.ReadSize(base.HeapSizes.String);
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x060079AC RID: 31148 RVA: 0x0023FE88 File Offset: 0x0023FE88
		public uint HashAlgId { get; }

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x060079AD RID: 31149 RVA: 0x0023FE90 File Offset: 0x0023FE90
		public ushort MajorVersion { get; }

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x060079AE RID: 31150 RVA: 0x0023FE98 File Offset: 0x0023FE98
		public ushort MinorVersion { get; }

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x060079AF RID: 31151 RVA: 0x0023FEA0 File Offset: 0x0023FEA0
		public ushort BuildNumber { get; }

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x060079B0 RID: 31152 RVA: 0x0023FEA8 File Offset: 0x0023FEA8
		public ushort RevisionNumber { get; }

		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x060079B1 RID: 31153 RVA: 0x0023FEB0 File Offset: 0x0023FEB0
		public uint Flags { get; }

		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x060079B2 RID: 31154 RVA: 0x0023FEB8 File Offset: 0x0023FEB8
		public uint PublicKey { get; }

		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x060079B3 RID: 31155 RVA: 0x0023FEC0 File Offset: 0x0023FEC0
		public uint Name { get; }

		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x060079B4 RID: 31156 RVA: 0x0023FEC8 File Offset: 0x0023FEC8
		public uint Culture { get; }
	}
}
