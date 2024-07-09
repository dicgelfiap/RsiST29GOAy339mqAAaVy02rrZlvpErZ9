using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009C5 RID: 2501
	[ComVisible(true)]
	public readonly struct RawGenericParamRow
	{
		// Token: 0x06005FA2 RID: 24482 RVA: 0x001C97FC File Offset: 0x001C97FC
		public RawGenericParamRow(ushort Number, ushort Flags, uint Owner, uint Name, uint Kind)
		{
			this.Number = Number;
			this.Flags = Flags;
			this.Owner = Owner;
			this.Name = Name;
			this.Kind = Kind;
		}

		// Token: 0x06005FA3 RID: 24483 RVA: 0x001C9824 File Offset: 0x001C9824
		public RawGenericParamRow(ushort Number, ushort Flags, uint Owner, uint Name)
		{
			this.Number = Number;
			this.Flags = Flags;
			this.Owner = Owner;
			this.Name = Name;
			this.Kind = 0U;
		}

		// Token: 0x17001408 RID: 5128
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.Number;
					break;
				case 1:
					result = (uint)this.Flags;
					break;
				case 2:
					result = this.Owner;
					break;
				case 3:
					result = this.Name;
					break;
				case 4:
					result = this.Kind;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002ED2 RID: 11986
		public readonly ushort Number;

		// Token: 0x04002ED3 RID: 11987
		public readonly ushort Flags;

		// Token: 0x04002ED4 RID: 11988
		public readonly uint Owner;

		// Token: 0x04002ED5 RID: 11989
		public readonly uint Name;

		// Token: 0x04002ED6 RID: 11990
		public readonly uint Kind;
	}
}
