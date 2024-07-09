using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009B3 RID: 2483
	[ComVisible(true)]
	public readonly struct RawMethodSemanticsRow
	{
		// Token: 0x06005F7E RID: 24446 RVA: 0x001C9024 File Offset: 0x001C9024
		public RawMethodSemanticsRow(ushort Semantic, uint Method, uint Association)
		{
			this.Semantic = Semantic;
			this.Method = Method;
			this.Association = Association;
		}

		// Token: 0x170013F6 RID: 5110
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.Semantic;
					break;
				case 1:
					result = this.Method;
					break;
				case 2:
					result = this.Association;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E97 RID: 11927
		public readonly ushort Semantic;

		// Token: 0x04002E98 RID: 11928
		public readonly uint Method;

		// Token: 0x04002E99 RID: 11929
		public readonly uint Association;
	}
}
