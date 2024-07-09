using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BE9 RID: 3049
	[ComVisible(true)]
	public class StandAloneSig : AbstractTable
	{
		// Token: 0x06007A38 RID: 31288 RVA: 0x00240D3C File Offset: 0x00240D3C
		public StandAloneSig(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Signature = base.ReadSize(base.HeapSizes.Blob);
		}

		// Token: 0x17001A8A RID: 6794
		// (get) Token: 0x06007A39 RID: 31289 RVA: 0x00240D70 File Offset: 0x00240D70
		public uint Signature { get; }
	}
}
