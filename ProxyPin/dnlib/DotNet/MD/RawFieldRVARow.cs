using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009B8 RID: 2488
	[ComVisible(true)]
	public readonly struct RawFieldRVARow
	{
		// Token: 0x06005F88 RID: 24456 RVA: 0x001C91DC File Offset: 0x001C91DC
		public RawFieldRVARow(uint RVA, uint Field)
		{
			this.RVA = RVA;
			this.Field = Field;
		}

		// Token: 0x170013FB RID: 5115
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
					result = this.RVA;
				}
				return result;
			}
		}

		// Token: 0x04002EA3 RID: 11939
		public readonly uint RVA;

		// Token: 0x04002EA4 RID: 11940
		public readonly uint Field;
	}
}
