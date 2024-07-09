using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009A5 RID: 2469
	[ComVisible(true)]
	public readonly struct RawMemberRefRow
	{
		// Token: 0x06005F62 RID: 24418 RVA: 0x001C8B58 File Offset: 0x001C8B58
		public RawMemberRefRow(uint Class, uint Name, uint Signature)
		{
			this.Class = Class;
			this.Name = Name;
			this.Signature = Signature;
		}

		// Token: 0x170013E8 RID: 5096
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.Class;
					break;
				case 1:
					result = this.Name;
					break;
				case 2:
					result = this.Signature;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E76 RID: 11894
		public readonly uint Class;

		// Token: 0x04002E77 RID: 11895
		public readonly uint Name;

		// Token: 0x04002E78 RID: 11896
		public readonly uint Signature;
	}
}
