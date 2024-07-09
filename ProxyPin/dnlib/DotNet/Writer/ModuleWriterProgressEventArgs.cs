using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008C1 RID: 2241
	[ComVisible(true)]
	public readonly struct ModuleWriterProgressEventArgs
	{
		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x060056B7 RID: 22199 RVA: 0x001A7E00 File Offset: 0x001A7E00
		public ModuleWriterBase Writer { get; }

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x060056B8 RID: 22200 RVA: 0x001A7E08 File Offset: 0x001A7E08
		public double Progress { get; }

		// Token: 0x060056B9 RID: 22201 RVA: 0x001A7E10 File Offset: 0x001A7E10
		public ModuleWriterProgressEventArgs(ModuleWriterBase writer, double progress)
		{
			if (progress < 0.0 || progress > 1.0)
			{
				throw new ArgumentOutOfRangeException("progress");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.Writer = writer;
			this.Progress = progress;
		}
	}
}
