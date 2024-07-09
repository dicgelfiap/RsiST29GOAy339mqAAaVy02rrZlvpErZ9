using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009AD RID: 2477
	[ComVisible(true)]
	public readonly struct RawEventMapRow
	{
		// Token: 0x06005F72 RID: 24434 RVA: 0x001C8E4C File Offset: 0x001C8E4C
		public RawEventMapRow(uint Parent, uint EventList)
		{
			this.Parent = Parent;
			this.EventList = EventList;
		}

		// Token: 0x170013F0 RID: 5104
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
						result = this.EventList;
					}
				}
				else
				{
					result = this.Parent;
				}
				return result;
			}
		}

		// Token: 0x04002E8B RID: 11915
		public readonly uint Parent;

		// Token: 0x04002E8C RID: 11916
		public readonly uint EventList;
	}
}
