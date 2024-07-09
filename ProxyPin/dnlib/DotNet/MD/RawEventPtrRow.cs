using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009AE RID: 2478
	[ComVisible(true)]
	public readonly struct RawEventPtrRow
	{
		// Token: 0x06005F74 RID: 24436 RVA: 0x001C8E9C File Offset: 0x001C8E9C
		public RawEventPtrRow(uint Event)
		{
			this.Event = Event;
		}

		// Token: 0x170013F1 RID: 5105
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index == 0)
				{
					result = this.Event;
				}
				else
				{
					result = 0U;
				}
				return result;
			}
		}

		// Token: 0x04002E8D RID: 11917
		public readonly uint Event;
	}
}
