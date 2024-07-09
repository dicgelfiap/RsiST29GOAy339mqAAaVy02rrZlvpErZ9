using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BCC RID: 3020
	[ComVisible(true)]
	public class AssemblyRefOS : AbstractTable
	{
		// Token: 0x060079C5 RID: 31173 RVA: 0x00240058 File Offset: 0x00240058
		public AssemblyRefOS(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.OSPlatformID = base.ReadSize(4U);
			this.OSMajorVersion = base.ReadSize(4U);
			this.OSMinorVersion = base.ReadSize(4U);
			this.AssemblyRef = base.ReadSize(base.IndexSizes[Index.AssemblyRef]);
		}

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x060079C6 RID: 31174 RVA: 0x002400B4 File Offset: 0x002400B4
		public uint OSPlatformID { get; }

		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x060079C7 RID: 31175 RVA: 0x002400BC File Offset: 0x002400BC
		public uint OSMajorVersion { get; }

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x060079C8 RID: 31176 RVA: 0x002400C4 File Offset: 0x002400C4
		public uint OSMinorVersion { get; }

		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x060079C9 RID: 31177 RVA: 0x002400CC File Offset: 0x002400CC
		public uint AssemblyRef { get; }
	}
}
