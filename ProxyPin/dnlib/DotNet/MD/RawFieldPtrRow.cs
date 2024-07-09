using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x0200099E RID: 2462
	[ComVisible(true)]
	public readonly struct RawFieldPtrRow
	{
		// Token: 0x06005F54 RID: 24404 RVA: 0x001C88EC File Offset: 0x001C88EC
		public RawFieldPtrRow(uint Field)
		{
			this.Field = Field;
		}

		// Token: 0x170013E1 RID: 5089
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index == 0)
				{
					result = this.Field;
				}
				else
				{
					result = 0U;
				}
				return result;
			}
		}

		// Token: 0x04002E65 RID: 11877
		public readonly uint Field;
	}
}
