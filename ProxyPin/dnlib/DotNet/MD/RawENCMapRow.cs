using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009BA RID: 2490
	[ComVisible(true)]
	public readonly struct RawENCMapRow
	{
		// Token: 0x06005F8C RID: 24460 RVA: 0x001C927C File Offset: 0x001C927C
		public RawENCMapRow(uint Token)
		{
			this.Token = Token;
		}

		// Token: 0x170013FD RID: 5117
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index == 0)
				{
					result = this.Token;
				}
				else
				{
					result = 0U;
				}
				return result;
			}
		}

		// Token: 0x04002EA7 RID: 11943
		public readonly uint Token;
	}
}
