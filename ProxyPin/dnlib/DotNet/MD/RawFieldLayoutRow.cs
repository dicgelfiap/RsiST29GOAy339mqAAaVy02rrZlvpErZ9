using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009AB RID: 2475
	[ComVisible(true)]
	public readonly struct RawFieldLayoutRow
	{
		// Token: 0x06005F6E RID: 24430 RVA: 0x001C8DC8 File Offset: 0x001C8DC8
		public RawFieldLayoutRow(uint OffSet, uint Field)
		{
			this.OffSet = OffSet;
			this.Field = Field;
		}

		// Token: 0x170013EE RID: 5102
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
						result = this.Field;
					}
				}
				else
				{
					result = this.OffSet;
				}
				return result;
			}
		}

		// Token: 0x04002E88 RID: 11912
		public readonly uint OffSet;

		// Token: 0x04002E89 RID: 11913
		public readonly uint Field;
	}
}
