using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009C4 RID: 2500
	[ComVisible(true)]
	public readonly struct RawNestedClassRow
	{
		// Token: 0x06005FA0 RID: 24480 RVA: 0x001C97AC File Offset: 0x001C97AC
		public RawNestedClassRow(uint NestedClass, uint EnclosingClass)
		{
			this.NestedClass = NestedClass;
			this.EnclosingClass = EnclosingClass;
		}

		// Token: 0x17001407 RID: 5127
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
						result = this.EnclosingClass;
					}
				}
				else
				{
					result = this.NestedClass;
				}
				return result;
			}
		}

		// Token: 0x04002ED0 RID: 11984
		public readonly uint NestedClass;

		// Token: 0x04002ED1 RID: 11985
		public readonly uint EnclosingClass;
	}
}
