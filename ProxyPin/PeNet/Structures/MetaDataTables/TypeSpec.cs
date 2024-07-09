using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BED RID: 3053
	[ComVisible(true)]
	public class TypeSpec : AbstractTable
	{
		// Token: 0x06007A90 RID: 31376 RVA: 0x002411B0 File Offset: 0x002411B0
		public TypeSpec(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Signature = base.ReadSize(base.HeapSizes.Blob);
		}

		// Token: 0x17001AB9 RID: 6841
		// (get) Token: 0x06007A91 RID: 31377 RVA: 0x002411E4 File Offset: 0x002411E4
		public uint Signature { get; }
	}
}
