using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009A9 RID: 2473
	[ComVisible(true)]
	public readonly struct RawDeclSecurityRow
	{
		// Token: 0x06005F6A RID: 24426 RVA: 0x001C8CF8 File Offset: 0x001C8CF8
		public RawDeclSecurityRow(short Action, uint Parent, uint PermissionSet)
		{
			this.Action = Action;
			this.Parent = Parent;
			this.PermissionSet = PermissionSet;
		}

		// Token: 0x170013EC RID: 5100
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.Action;
					break;
				case 1:
					result = this.Parent;
					break;
				case 2:
					result = this.PermissionSet;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E82 RID: 11906
		public readonly short Action;

		// Token: 0x04002E83 RID: 11907
		public readonly uint Parent;

		// Token: 0x04002E84 RID: 11908
		public readonly uint PermissionSet;
	}
}
