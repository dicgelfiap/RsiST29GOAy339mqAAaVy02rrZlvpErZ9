using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009AC RID: 2476
	[ComVisible(true)]
	public readonly struct RawStandAloneSigRow
	{
		// Token: 0x06005F70 RID: 24432 RVA: 0x001C8E18 File Offset: 0x001C8E18
		public RawStandAloneSigRow(uint Signature)
		{
			this.Signature = Signature;
		}

		// Token: 0x170013EF RID: 5103
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

		// Token: 0x04002E8A RID: 11914
		public readonly uint Signature;
	}
}
