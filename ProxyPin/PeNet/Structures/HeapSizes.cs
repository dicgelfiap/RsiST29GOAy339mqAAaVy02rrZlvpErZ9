using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures
{
	// Token: 0x02000BA0 RID: 2976
	[ComVisible(true)]
	public class HeapSizes
	{
		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x060077AA RID: 30634 RVA: 0x0023B470 File Offset: 0x0023B470
		public uint String { get; }

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x060077AB RID: 30635 RVA: 0x0023B478 File Offset: 0x0023B478
		public uint Guid { get; }

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x060077AC RID: 30636 RVA: 0x0023B480 File Offset: 0x0023B480
		public uint Blob { get; }

		// Token: 0x060077AD RID: 30637 RVA: 0x0023B488 File Offset: 0x0023B488
		public HeapSizes(byte heapSizes)
		{
			this.String = (((heapSizes & 1) == 0) ? 2 : 4);
			this.Guid = (((heapSizes & 2) == 0) ? 2 : 4);
			this.Blob = (((heapSizes & 4) == 0) ? 2 : 4);
		}
	}
}
