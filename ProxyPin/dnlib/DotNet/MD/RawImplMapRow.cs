using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009B7 RID: 2487
	[ComVisible(true)]
	public readonly struct RawImplMapRow
	{
		// Token: 0x06005F86 RID: 24454 RVA: 0x001C915C File Offset: 0x001C915C
		public RawImplMapRow(ushort MappingFlags, uint MemberForwarded, uint ImportName, uint ImportScope)
		{
			this.MappingFlags = MappingFlags;
			this.MemberForwarded = MemberForwarded;
			this.ImportName = ImportName;
			this.ImportScope = ImportScope;
		}

		// Token: 0x170013FA RID: 5114
		public uint this[int index]
		{
			get
			{
				uint result;
				switch (index)
				{
				case 0:
					result = (uint)this.MappingFlags;
					break;
				case 1:
					result = this.MemberForwarded;
					break;
				case 2:
					result = this.ImportName;
					break;
				case 3:
					result = this.ImportScope;
					break;
				default:
					result = 0U;
					break;
				}
				return result;
			}
		}

		// Token: 0x04002E9F RID: 11935
		public readonly ushort MappingFlags;

		// Token: 0x04002EA0 RID: 11936
		public readonly uint MemberForwarded;

		// Token: 0x04002EA1 RID: 11937
		public readonly uint ImportName;

		// Token: 0x04002EA2 RID: 11938
		public readonly uint ImportScope;
	}
}
