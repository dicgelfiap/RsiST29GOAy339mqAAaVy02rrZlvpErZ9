using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009C1 RID: 2497
	[ComVisible(true)]
	public readonly struct RawFileRow
	{
		// Token: 0x06005F9A RID: 24474 RVA: 0x001C962C File Offset: 0x001C962C
		public RawFileRow(uint Flags, uint Name, uint HashValue)
		{
			this.Flags = Flags;
			this.Name = Name;
			this.HashValue = HashValue;
		}

		// Token: 0x17001404 RID: 5124
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.Flags;
					break;
				case 1:
					result = this.Name;
					break;
				case 2:
					result = this.HashValue;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002EC4 RID: 11972
		public readonly uint Flags;

		// Token: 0x04002EC5 RID: 11973
		public readonly uint Name;

		// Token: 0x04002EC6 RID: 11974
		public readonly uint HashValue;
	}
}
