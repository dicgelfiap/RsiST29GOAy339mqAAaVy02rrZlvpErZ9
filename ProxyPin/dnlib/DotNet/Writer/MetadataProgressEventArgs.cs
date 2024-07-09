using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008B4 RID: 2228
	[ComVisible(true)]
	public readonly struct MetadataProgressEventArgs
	{
		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x06005541 RID: 21825 RVA: 0x0019F860 File Offset: 0x0019F860
		public Metadata Metadata { get; }

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06005542 RID: 21826 RVA: 0x0019F868 File Offset: 0x0019F868
		public double Progress { get; }

		// Token: 0x06005543 RID: 21827 RVA: 0x0019F870 File Offset: 0x0019F870
		public MetadataProgressEventArgs(Metadata metadata, double progress)
		{
			if (progress < 0.0 || progress > 1.0)
			{
				throw new ArgumentOutOfRangeException("progress");
			}
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			this.Metadata = metadata;
			this.Progress = progress;
		}
	}
}
