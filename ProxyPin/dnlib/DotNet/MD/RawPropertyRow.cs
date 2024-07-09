using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009B2 RID: 2482
	[ComVisible(true)]
	public readonly struct RawPropertyRow
	{
		// Token: 0x06005F7C RID: 24444 RVA: 0x001C8FBC File Offset: 0x001C8FBC
		public RawPropertyRow(ushort PropFlags, uint Name, uint Type)
		{
			this.PropFlags = PropFlags;
			this.Name = Name;
			this.Type = Type;
		}

		// Token: 0x170013F5 RID: 5109
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.PropFlags;
					break;
				case 1:
					result = this.Name;
					break;
				case 2:
					result = this.Type;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E94 RID: 11924
		public readonly ushort PropFlags;

		// Token: 0x04002E95 RID: 11925
		public readonly uint Name;

		// Token: 0x04002E96 RID: 11926
		public readonly uint Type;
	}
}
