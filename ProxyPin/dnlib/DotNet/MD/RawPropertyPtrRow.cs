using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009B1 RID: 2481
	[ComVisible(true)]
	public readonly struct RawPropertyPtrRow
	{
		// Token: 0x06005F7A RID: 24442 RVA: 0x001C8F88 File Offset: 0x001C8F88
		public RawPropertyPtrRow(uint Property)
		{
			this.Property = Property;
		}

		// Token: 0x170013F4 RID: 5108
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index == 0)
				{
					result = this.Property;
				}
				else
				{
					result = 0U;
				}
				return result;
			}
		}

		// Token: 0x04002E93 RID: 11923
		public readonly uint Property;
	}
}
