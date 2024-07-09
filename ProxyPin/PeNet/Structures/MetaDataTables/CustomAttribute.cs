using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BD0 RID: 3024
	[ComVisible(true)]
	public class CustomAttribute : AbstractTable
	{
		// Token: 0x060079D5 RID: 31189 RVA: 0x00240210 File Offset: 0x00240210
		public CustomAttribute(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Parent = base.ReadSize(base.IndexSizes[Index.HasCustomAttribute]);
			this.Type = base.ReadSize(base.IndexSizes[Index.CustomAttributeType]);
			this.Value = base.ReadSize(base.HeapSizes.Blob);
		}

		// Token: 0x17001A40 RID: 6720
		// (get) Token: 0x060079D6 RID: 31190 RVA: 0x00240278 File Offset: 0x00240278
		public uint Parent { get; }

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x060079D7 RID: 31191 RVA: 0x00240280 File Offset: 0x00240280
		public uint Type { get; }

		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x060079D8 RID: 31192 RVA: 0x00240288 File Offset: 0x00240288
		public uint Value { get; }
	}
}
