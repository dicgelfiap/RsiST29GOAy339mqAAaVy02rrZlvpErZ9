using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009A6 RID: 2470
	[ComVisible(true)]
	public readonly struct RawConstantRow
	{
		// Token: 0x06005F64 RID: 24420 RVA: 0x001C8BC0 File Offset: 0x001C8BC0
		public RawConstantRow(byte Type, byte Padding, uint Parent, uint Value)
		{
			this.Type = Type;
			this.Padding = Padding;
			this.Parent = Parent;
			this.Value = Value;
		}

		// Token: 0x170013E9 RID: 5097
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.Type;
					break;
				case 1:
					result = (uint)this.Padding;
					break;
				case 2:
					result = this.Parent;
					break;
				case 3:
					result = this.Value;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E79 RID: 11897
		public readonly byte Type;

		// Token: 0x04002E7A RID: 11898
		public readonly byte Padding;

		// Token: 0x04002E7B RID: 11899
		public readonly uint Parent;

		// Token: 0x04002E7C RID: 11900
		public readonly uint Value;
	}
}
