using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009A2 RID: 2466
	[ComVisible(true)]
	public readonly struct RawParamPtrRow
	{
		// Token: 0x06005F5C RID: 24412 RVA: 0x001C8A6C File Offset: 0x001C8A6C
		public RawParamPtrRow(uint Param)
		{
			this.Param = Param;
		}

		// Token: 0x170013E5 RID: 5093
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index == 0)
				{
					result = this.Param;
				}
				else
				{
					result = 0U;
				}
				return result;
			}
		}

		// Token: 0x04002E70 RID: 11888
		public readonly uint Param;
	}
}
