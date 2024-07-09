using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009B6 RID: 2486
	[ComVisible(true)]
	public readonly struct RawTypeSpecRow
	{
		// Token: 0x06005F84 RID: 24452 RVA: 0x001C9128 File Offset: 0x001C9128
		public RawTypeSpecRow(uint Signature)
		{
			this.Signature = Signature;
		}

		// Token: 0x170013F9 RID: 5113
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index == 0)
				{
					result = this.Signature;
				}
				else
				{
					result = 0U;
				}
				return result;
			}
		}

		// Token: 0x04002E9E RID: 11934
		public readonly uint Signature;
	}
}
