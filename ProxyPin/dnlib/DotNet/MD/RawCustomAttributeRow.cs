using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009A7 RID: 2471
	[ComVisible(true)]
	public readonly struct RawCustomAttributeRow
	{
		// Token: 0x06005F66 RID: 24422 RVA: 0x001C8C40 File Offset: 0x001C8C40
		public RawCustomAttributeRow(uint Parent, uint Type, uint Value)
		{
			this.Parent = Parent;
			this.Type = Type;
			this.Value = Value;
		}

		// Token: 0x170013EA RID: 5098
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.Parent;
					break;
				case 1:
					result = this.Type;
					break;
				case 2:
					result = this.Value;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E7D RID: 11901
		public readonly uint Parent;

		// Token: 0x04002E7E RID: 11902
		public readonly uint Type;

		// Token: 0x04002E7F RID: 11903
		public readonly uint Value;
	}
}
