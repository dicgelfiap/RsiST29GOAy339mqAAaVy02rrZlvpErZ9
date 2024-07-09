using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009CE RID: 2510
	[ComVisible(true)]
	public readonly struct RawStateMachineMethodRow
	{
		// Token: 0x06005FB5 RID: 24501 RVA: 0x001C9BE4 File Offset: 0x001C9BE4
		public RawStateMachineMethodRow(uint MoveNextMethod, uint KickoffMethod)
		{
			this.MoveNextMethod = MoveNextMethod;
			this.KickoffMethod = KickoffMethod;
		}

		// Token: 0x17001411 RID: 5137
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
						result = this.KickoffMethod;
					}
				}
				else
				{
					result = this.MoveNextMethod;
				}
				return result;
			}
		}

		// Token: 0x04002EEE RID: 12014
		public readonly uint MoveNextMethod;

		// Token: 0x04002EEF RID: 12015
		public readonly uint KickoffMethod;
	}
}
