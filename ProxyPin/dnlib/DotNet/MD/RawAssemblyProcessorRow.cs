using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009BC RID: 2492
	[ComVisible(true)]
	public readonly struct RawAssemblyProcessorRow
	{
		// Token: 0x06005F90 RID: 24464 RVA: 0x001C93B8 File Offset: 0x001C93B8
		public RawAssemblyProcessorRow(uint Processor)
		{
			this.Processor = Processor;
		}

		// Token: 0x170013FF RID: 5119
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index == 0)
				{
					result = this.Processor;
				}
				else
				{
					result = 0U;
				}
				return result;
			}
		}

		// Token: 0x04002EB1 RID: 11953
		public readonly uint Processor;
	}
}
