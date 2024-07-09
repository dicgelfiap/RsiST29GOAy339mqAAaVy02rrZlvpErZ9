using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BE8 RID: 3048
	[ComVisible(true)]
	public class PropertyMap : AbstractTable
	{
		// Token: 0x06007A35 RID: 31285 RVA: 0x00240CE0 File Offset: 0x00240CE0
		public PropertyMap(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Parent = base.ReadSize(base.IndexSizes[Index.TypeDef]);
			this.PropertyList = base.ReadSize(base.IndexSizes[Index.Property]);
		}

		// Token: 0x17001A88 RID: 6792
		// (get) Token: 0x06007A36 RID: 31286 RVA: 0x00240D2C File Offset: 0x00240D2C
		public uint Parent { get; }

		// Token: 0x17001A89 RID: 6793
		// (get) Token: 0x06007A37 RID: 31287 RVA: 0x00240D34 File Offset: 0x00240D34
		public uint PropertyList { get; }
	}
}
