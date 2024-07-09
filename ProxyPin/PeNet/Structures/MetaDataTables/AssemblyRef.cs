using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BCB RID: 3019
	[ComVisible(true)]
	public class AssemblyRef : AbstractTable
	{
		// Token: 0x060079BB RID: 31163 RVA: 0x0023FF50 File Offset: 0x0023FF50
		public AssemblyRef(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.MajorVersion = (ushort)base.ReadSize(2U);
			this.MinorVersion = (ushort)base.ReadSize(2U);
			this.BuildNumber = (ushort)base.ReadSize(2U);
			this.RevisionNumber = (ushort)base.ReadSize(2U);
			this.Flags = base.ReadSize(4U);
			this.PublicKeyOrToken = base.ReadSize(base.HeapSizes.Blob);
			this.Name = base.ReadSize(base.HeapSizes.String);
			this.Culture = base.ReadSize(base.HeapSizes.String);
			this.HashValue = base.ReadSize(base.HeapSizes.Blob);
		}

		// Token: 0x17001A2B RID: 6699
		// (get) Token: 0x060079BC RID: 31164 RVA: 0x00240010 File Offset: 0x00240010
		public ushort MajorVersion { get; }

		// Token: 0x17001A2C RID: 6700
		// (get) Token: 0x060079BD RID: 31165 RVA: 0x00240018 File Offset: 0x00240018
		public ushort MinorVersion { get; }

		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x060079BE RID: 31166 RVA: 0x00240020 File Offset: 0x00240020
		public ushort BuildNumber { get; }

		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x060079BF RID: 31167 RVA: 0x00240028 File Offset: 0x00240028
		public ushort RevisionNumber { get; }

		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x060079C0 RID: 31168 RVA: 0x00240030 File Offset: 0x00240030
		public uint Flags { get; }

		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x060079C1 RID: 31169 RVA: 0x00240038 File Offset: 0x00240038
		public uint PublicKeyOrToken { get; }

		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x060079C2 RID: 31170 RVA: 0x00240040 File Offset: 0x00240040
		public uint Name { get; }

		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x060079C3 RID: 31171 RVA: 0x00240048 File Offset: 0x00240048
		public uint Culture { get; }

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x060079C4 RID: 31172 RVA: 0x00240050 File Offset: 0x00240050
		public uint HashValue { get; }
	}
}
