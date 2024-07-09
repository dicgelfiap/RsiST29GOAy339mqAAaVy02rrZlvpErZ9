using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009B5 RID: 2485
	[ComVisible(true)]
	public readonly struct RawModuleRefRow
	{
		// Token: 0x06005F82 RID: 24450 RVA: 0x001C90F4 File Offset: 0x001C90F4
		public RawModuleRefRow(uint Name)
		{
			this.Name = Name;
		}

		// Token: 0x170013F8 RID: 5112
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index == 0)
				{
					result = this.Name;
				}
				else
				{
					result = 0U;
				}
				return result;
			}
		}

		// Token: 0x04002E9D RID: 11933
		public readonly uint Name;
	}
}
