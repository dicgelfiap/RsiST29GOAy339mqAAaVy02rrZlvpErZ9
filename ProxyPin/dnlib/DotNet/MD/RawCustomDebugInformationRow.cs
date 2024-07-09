using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009CF RID: 2511
	[ComVisible(true)]
	public readonly struct RawCustomDebugInformationRow
	{
		// Token: 0x06005FB7 RID: 24503 RVA: 0x001C9C34 File Offset: 0x001C9C34
		public RawCustomDebugInformationRow(uint Parent, uint Kind, uint Value)
		{
			this.Parent = Parent;
			this.Kind = Kind;
			this.Value = Value;
		}

		// Token: 0x17001412 RID: 5138
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
					result = this.Kind;
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

		// Token: 0x04002EF0 RID: 12016
		public readonly uint Parent;

		// Token: 0x04002EF1 RID: 12017
		public readonly uint Kind;

		// Token: 0x04002EF2 RID: 12018
		public readonly uint Value;
	}
}
