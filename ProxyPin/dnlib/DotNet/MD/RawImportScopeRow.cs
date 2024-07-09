using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009CD RID: 2509
	[ComVisible(true)]
	public readonly struct RawImportScopeRow
	{
		// Token: 0x06005FB3 RID: 24499 RVA: 0x001C9B94 File Offset: 0x001C9B94
		public RawImportScopeRow(uint Parent, uint Imports)
		{
			this.Parent = Parent;
			this.Imports = Imports;
		}

		// Token: 0x17001410 RID: 5136
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
						result = this.Imports;
					}
				}
				else
				{
					result = this.Parent;
				}
				return result;
			}
		}

		// Token: 0x04002EEC RID: 12012
		public readonly uint Parent;

		// Token: 0x04002EED RID: 12013
		public readonly uint Imports;
	}
}
