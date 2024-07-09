using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009A8 RID: 2472
	[ComVisible(true)]
	public readonly struct RawFieldMarshalRow
	{
		// Token: 0x06005F68 RID: 24424 RVA: 0x001C8CA8 File Offset: 0x001C8CA8
		public RawFieldMarshalRow(uint Parent, uint NativeType)
		{
			this.Parent = Parent;
			this.NativeType = NativeType;
		}

		// Token: 0x170013EB RID: 5099
		public uint this[int index]
		{
			get
			{
				uint result;
				if (index != 0)
				{
					if (index != 1)
					{
						result = 0U;
					}
					else
					{
						result = this.NativeType;
					}
				}
				else
				{
					result = this.Parent;
				}
				return result;
			}
		}

		// Token: 0x04002E80 RID: 11904
		public readonly uint Parent;

		// Token: 0x04002E81 RID: 11905
		public readonly uint NativeType;
	}
}
