using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BC9 RID: 3017
	[ComVisible(true)]
	public class AssemblyOS : AbstractTable
	{
		// Token: 0x060079B5 RID: 31157 RVA: 0x0023FED0 File Offset: 0x0023FED0
		public AssemblyOS(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.OSPlatformID = base.ReadSize(4U);
			this.OSMajorVersion = base.ReadSize(4U);
			this.OSMinorVersion = base.ReadSize(4U);
		}

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x060079B6 RID: 31158 RVA: 0x0023FF14 File Offset: 0x0023FF14
		public uint OSPlatformID { get; }

		// Token: 0x17001A28 RID: 6696
		// (get) Token: 0x060079B7 RID: 31159 RVA: 0x0023FF1C File Offset: 0x0023FF1C
		public uint OSMajorVersion { get; }

		// Token: 0x17001A29 RID: 6697
		// (get) Token: 0x060079B8 RID: 31160 RVA: 0x0023FF24 File Offset: 0x0023FF24
		public uint OSMinorVersion { get; }
	}
}
