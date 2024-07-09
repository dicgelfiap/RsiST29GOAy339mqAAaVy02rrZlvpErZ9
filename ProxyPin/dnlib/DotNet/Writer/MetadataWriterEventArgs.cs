using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008B3 RID: 2227
	[ComVisible(true)]
	public readonly struct MetadataWriterEventArgs
	{
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x0600553E RID: 21822 RVA: 0x0019F82C File Offset: 0x0019F82C
		public Metadata Metadata { get; }

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x0600553F RID: 21823 RVA: 0x0019F834 File Offset: 0x0019F834
		public MetadataEvent Event { get; }

		// Token: 0x06005540 RID: 21824 RVA: 0x0019F83C File Offset: 0x0019F83C
		public MetadataWriterEventArgs(Metadata metadata, MetadataEvent @event)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			this.Metadata = metadata;
			this.Event = @event;
		}
	}
}
