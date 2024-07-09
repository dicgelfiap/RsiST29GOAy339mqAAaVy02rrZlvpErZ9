using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009B0 RID: 2480
	[ComVisible(true)]
	public readonly struct RawPropertyMapRow
	{
		// Token: 0x06005F78 RID: 24440 RVA: 0x001C8F38 File Offset: 0x001C8F38
		public RawPropertyMapRow(uint Parent, uint PropertyList)
		{
			this.Parent = Parent;
			this.PropertyList = PropertyList;
		}

		// Token: 0x170013F3 RID: 5107
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
						result = this.PropertyList;
					}
				}
				else
				{
					result = this.Parent;
				}
				return result;
			}
		}

		// Token: 0x04002E91 RID: 11921
		public readonly uint Parent;

		// Token: 0x04002E92 RID: 11922
		public readonly uint PropertyList;
	}
}
