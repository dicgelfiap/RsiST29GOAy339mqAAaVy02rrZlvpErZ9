using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009A3 RID: 2467
	[ComVisible(true)]
	public readonly struct RawParamRow
	{
		// Token: 0x06005F5E RID: 24414 RVA: 0x001C8AA0 File Offset: 0x001C8AA0
		public RawParamRow(ushort Flags, ushort Sequence, uint Name)
		{
			this.Flags = Flags;
			this.Sequence = Sequence;
			this.Name = Name;
		}

		// Token: 0x170013E6 RID: 5094
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.Flags;
					break;
				case 1:
					result = (uint)this.Sequence;
					break;
				case 2:
					result = this.Name;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E71 RID: 11889
		public readonly ushort Flags;

		// Token: 0x04002E72 RID: 11890
		public readonly ushort Sequence;

		// Token: 0x04002E73 RID: 11891
		public readonly uint Name;
	}
}
