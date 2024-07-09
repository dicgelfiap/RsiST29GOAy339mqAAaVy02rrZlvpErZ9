using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009AA RID: 2474
	[ComVisible(true)]
	public readonly struct RawClassLayoutRow
	{
		// Token: 0x06005F6C RID: 24428 RVA: 0x001C8D60 File Offset: 0x001C8D60
		public RawClassLayoutRow(ushort PackingSize, uint ClassSize, uint Parent)
		{
			this.PackingSize = PackingSize;
			this.ClassSize = ClassSize;
			this.Parent = Parent;
		}

		// Token: 0x170013ED RID: 5101
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.PackingSize;
					break;
				case 1:
					result = this.ClassSize;
					break;
				case 2:
					result = this.Parent;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E85 RID: 11909
		public readonly ushort PackingSize;

		// Token: 0x04002E86 RID: 11910
		public readonly uint ClassSize;

		// Token: 0x04002E87 RID: 11911
		public readonly uint Parent;
	}
}
