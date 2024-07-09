using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009A4 RID: 2468
	[ComVisible(true)]
	public readonly struct RawInterfaceImplRow
	{
		// Token: 0x06005F60 RID: 24416 RVA: 0x001C8B08 File Offset: 0x001C8B08
		public RawInterfaceImplRow(uint Class, uint Interface)
		{
			this.Class = Class;
			this.Interface = Interface;
		}

		// Token: 0x170013E7 RID: 5095
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
						result = this.Interface;
					}
				}
				else
				{
					result = this.Class;
				}
				return result;
			}
		}

		// Token: 0x04002E74 RID: 11892
		public readonly uint Class;

		// Token: 0x04002E75 RID: 11893
		public readonly uint Interface;
	}
}
