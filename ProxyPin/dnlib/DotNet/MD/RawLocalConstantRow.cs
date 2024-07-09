using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009CC RID: 2508
	[ComVisible(true)]
	public readonly struct RawLocalConstantRow
	{
		// Token: 0x06005FB1 RID: 24497 RVA: 0x001C9B44 File Offset: 0x001C9B44
		public RawLocalConstantRow(uint Name, uint Signature)
		{
			this.Name = Name;
			this.Signature = Signature;
		}

		// Token: 0x1700140F RID: 5135
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
						result = this.Signature;
					}
				}
				else
				{
					result = this.Name;
				}
				return result;
			}
		}

		// Token: 0x04002EEA RID: 12010
		public readonly uint Name;

		// Token: 0x04002EEB RID: 12011
		public readonly uint Signature;
	}
}
