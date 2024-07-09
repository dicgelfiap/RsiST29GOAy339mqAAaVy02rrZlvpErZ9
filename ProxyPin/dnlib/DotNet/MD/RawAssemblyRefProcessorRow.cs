using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009BF RID: 2495
	[ComVisible(true)]
	public readonly struct RawAssemblyRefProcessorRow
	{
		// Token: 0x06005F96 RID: 24470 RVA: 0x001C955C File Offset: 0x001C955C
		public RawAssemblyRefProcessorRow(uint Processor, uint AssemblyRef)
		{
			this.Processor = Processor;
			this.AssemblyRef = AssemblyRef;
		}

		// Token: 0x17001402 RID: 5122
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index != 0)
				{
					if (index != 1)
					{
						result = 0U;
					}
					else
					{
						result = this.AssemblyRef;
					}
				}
				else
				{
					result = this.Processor;
				}
				return result;
			}
		}

		// Token: 0x04002EBE RID: 11966
		public readonly uint Processor;

		// Token: 0x04002EBF RID: 11967
		public readonly uint AssemblyRef;
	}
}
