using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AB5 RID: 2741
	[NullableContext(1)]
	[Nullable(0)]
	internal class EnumInfo
	{
		// Token: 0x06006D2E RID: 27950 RVA: 0x00210828 File Offset: 0x00210828
		public EnumInfo(bool isFlags, ulong[] values, string[] names, string[] resolvedNames)
		{
			this.IsFlags = isFlags;
			this.Values = values;
			this.Names = names;
			this.ResolvedNames = resolvedNames;
		}

		// Token: 0x040036C1 RID: 14017
		public readonly bool IsFlags;

		// Token: 0x040036C2 RID: 14018
		public readonly ulong[] Values;

		// Token: 0x040036C3 RID: 14019
		public readonly string[] Names;

		// Token: 0x040036C4 RID: 14020
		public readonly string[] ResolvedNames;
	}
}
