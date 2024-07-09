using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x0200099F RID: 2463
	[ComVisible(true)]
	public readonly struct RawFieldRow
	{
		// Token: 0x06005F56 RID: 24406 RVA: 0x001C8920 File Offset: 0x001C8920
		public RawFieldRow(ushort Flags, uint Name, uint Signature)
		{
			this.Flags = Flags;
			this.Name = Name;
			this.Signature = Signature;
		}

		// Token: 0x170013E2 RID: 5090
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

		// Token: 0x04002E66 RID: 11878
		public readonly ushort Flags;

		// Token: 0x04002E67 RID: 11879
		public readonly uint Name;

		// Token: 0x04002E68 RID: 11880
		public readonly uint Signature;
	}
}
