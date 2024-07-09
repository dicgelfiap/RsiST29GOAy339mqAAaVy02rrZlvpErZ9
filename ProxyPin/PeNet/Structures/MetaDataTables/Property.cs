using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BE7 RID: 3047
	[ComVisible(true)]
	public class Property : AbstractTable
	{
		// Token: 0x06007A31 RID: 31281 RVA: 0x00240C70 File Offset: 0x00240C70
		public Property(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Flags = (ushort)base.ReadSize(2U);
			this.Name = base.ReadSize(base.HeapSizes.String);
			this.Type = base.ReadSize(base.HeapSizes.Blob);
		}

		// Token: 0x17001A85 RID: 6789
		// (get) Token: 0x06007A32 RID: 31282 RVA: 0x00240CC8 File Offset: 0x00240CC8
		public ushort Flags { get; }

		// Token: 0x17001A86 RID: 6790
		// (get) Token: 0x06007A33 RID: 31283 RVA: 0x00240CD0 File Offset: 0x00240CD0
		public uint Name { get; }

		// Token: 0x17001A87 RID: 6791
		// (get) Token: 0x06007A34 RID: 31284 RVA: 0x00240CD8 File Offset: 0x00240CD8
		public uint Type { get; }
	}
}
