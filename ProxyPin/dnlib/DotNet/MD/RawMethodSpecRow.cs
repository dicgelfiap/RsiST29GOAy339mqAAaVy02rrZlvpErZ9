using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009C6 RID: 2502
	[ComVisible(true)]
	public readonly struct RawMethodSpecRow
	{
		// Token: 0x06005FA5 RID: 24485 RVA: 0x001C98BC File Offset: 0x001C98BC
		public RawMethodSpecRow(uint Method, uint Instantiation)
		{
			this.Method = Method;
			this.Instantiation = Instantiation;
		}

		// Token: 0x17001409 RID: 5129
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
						result = this.Instantiation;
					}
				}
				else
				{
					result = this.Method;
				}
				return result;
			}
		}

		// Token: 0x04002ED7 RID: 11991
		public readonly uint Method;

		// Token: 0x04002ED8 RID: 11992
		public readonly uint Instantiation;
	}
}
