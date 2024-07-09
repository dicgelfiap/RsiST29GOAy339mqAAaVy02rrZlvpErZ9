using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009B9 RID: 2489
	[ComVisible(true)]
	public readonly struct RawENCLogRow
	{
		// Token: 0x06005F8A RID: 24458 RVA: 0x001C922C File Offset: 0x001C922C
		public RawENCLogRow(uint Token, uint FuncCode)
		{
			this.Token = Token;
			this.FuncCode = FuncCode;
		}

		// Token: 0x170013FC RID: 5116
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
						result = this.FuncCode;
					}
				}
				else
				{
					result = this.Token;
				}
				return result;
			}
		}

		// Token: 0x04002EA5 RID: 11941
		public readonly uint Token;

		// Token: 0x04002EA6 RID: 11942
		public readonly uint FuncCode;
	}
}
