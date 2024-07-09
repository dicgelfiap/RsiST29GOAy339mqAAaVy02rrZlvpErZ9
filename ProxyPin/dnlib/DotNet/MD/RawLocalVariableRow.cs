using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009CB RID: 2507
	[ComVisible(true)]
	public readonly struct RawLocalVariableRow
	{
		// Token: 0x06005FAF RID: 24495 RVA: 0x001C9ADC File Offset: 0x001C9ADC
		public RawLocalVariableRow(ushort Attributes, ushort Index, uint Name)
		{
			this.Attributes = Attributes;
			this.Index = Index;
			this.Name = Name;
		}

		// Token: 0x1700140E RID: 5134
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.Attributes;
					break;
				case 1:
					result = (uint)this.Index;
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

		// Token: 0x04002EE7 RID: 12007
		public readonly ushort Attributes;

		// Token: 0x04002EE8 RID: 12008
		public readonly ushort Index;

		// Token: 0x04002EE9 RID: 12009
		public readonly uint Name;
	}
}
