using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009A1 RID: 2465
	[ComVisible(true)]
	public readonly struct RawMethodRow
	{
		// Token: 0x06005F5A RID: 24410 RVA: 0x001C89BC File Offset: 0x001C89BC
		public RawMethodRow(uint RVA, ushort ImplFlags, ushort Flags, uint Name, uint Signature, uint ParamList)
		{
			this.RVA = RVA;
			this.ImplFlags = ImplFlags;
			this.Flags = Flags;
			this.Name = Name;
			this.Signature = Signature;
			this.ParamList = ParamList;
		}

		// Token: 0x170013E4 RID: 5092
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = this.RVA;
					break;
				case 1:
					result = (uint)this.ImplFlags;
					break;
				case 2:
					result = (uint)this.Flags;
					break;
				case 3:
					result = this.Name;
					break;
				case 4:
					result = this.Signature;
					break;
				case 5:
					result = this.ParamList;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E6A RID: 11882
		public readonly uint RVA;

		// Token: 0x04002E6B RID: 11883
		public readonly ushort ImplFlags;

		// Token: 0x04002E6C RID: 11884
		public readonly ushort Flags;

		// Token: 0x04002E6D RID: 11885
		public readonly uint Name;

		// Token: 0x04002E6E RID: 11886
		public readonly uint Signature;

		// Token: 0x04002E6F RID: 11887
		public readonly uint ParamList;
	}
}
