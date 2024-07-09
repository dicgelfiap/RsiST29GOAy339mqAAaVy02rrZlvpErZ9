using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009AF RID: 2479
	[ComVisible(true)]
	public readonly struct RawEventRow
	{
		// Token: 0x06005F76 RID: 24438 RVA: 0x001C8ED0 File Offset: 0x001C8ED0
		public RawEventRow(ushort EventFlags, uint Name, uint EventType)
		{
			this.EventFlags = EventFlags;
			this.Name = Name;
			this.EventType = EventType;
		}

		// Token: 0x170013F2 RID: 5106
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.EventFlags;
					break;
				case 1:
					result = this.Name;
					break;
				case 2:
					result = this.EventType;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E8E RID: 11918
		public readonly ushort EventFlags;

		// Token: 0x04002E8F RID: 11919
		public readonly uint Name;

		// Token: 0x04002E90 RID: 11920
		public readonly uint EventType;
	}
}
