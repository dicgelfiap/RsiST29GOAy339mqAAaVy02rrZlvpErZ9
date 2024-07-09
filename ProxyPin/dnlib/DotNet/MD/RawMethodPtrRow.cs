using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009A0 RID: 2464
	[ComVisible(true)]
	public readonly struct RawMethodPtrRow
	{
		// Token: 0x06005F58 RID: 24408 RVA: 0x001C8988 File Offset: 0x001C8988
		public RawMethodPtrRow(uint Method)
		{
			this.Method = Method;
		}

		// Token: 0x170013E3 RID: 5091
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index == 0)
				{
					result = this.Method;
				}
				else
				{
					result = 0U;
				}
				return result;
			}
		}

		// Token: 0x04002E69 RID: 11881
		public readonly uint Method;
	}
}
