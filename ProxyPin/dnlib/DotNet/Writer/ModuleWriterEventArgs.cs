using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008C0 RID: 2240
	[ComVisible(true)]
	public readonly struct ModuleWriterEventArgs
	{
		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x060056B4 RID: 22196 RVA: 0x001A7DCC File Offset: 0x001A7DCC
		public ModuleWriterBase Writer { get; }

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x060056B5 RID: 22197 RVA: 0x001A7DD4 File Offset: 0x001A7DD4
		public ModuleWriterEvent Event { get; }

		// Token: 0x060056B6 RID: 22198 RVA: 0x001A7DDC File Offset: 0x001A7DDC
		public ModuleWriterEventArgs(ModuleWriterBase writer, ModuleWriterEvent @event)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.Writer = writer;
			this.Event = @event;
		}
	}
}
