using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008AF RID: 2223
	[ComVisible(true)]
	public readonly struct MetadataHeapsAddedEventArgs
	{
		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06005528 RID: 21800 RVA: 0x0019F374 File Offset: 0x0019F374
		public Metadata Metadata { get; }

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06005529 RID: 21801 RVA: 0x0019F37C File Offset: 0x0019F37C
		public List<IHeap> Heaps { get; }

		// Token: 0x0600552A RID: 21802 RVA: 0x0019F384 File Offset: 0x0019F384
		public MetadataHeapsAddedEventArgs(Metadata metadata, List<IHeap> heaps)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			this.Metadata = metadata;
			if (heaps == null)
			{
				throw new ArgumentNullException("heaps");
			}
			this.Heaps = heaps;
		}
	}
}
