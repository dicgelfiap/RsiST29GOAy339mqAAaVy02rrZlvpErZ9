using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000C94 RID: 3220
	internal static class AllocFreeConcurrentStack
	{
		// Token: 0x04003D20 RID: 15648
		[ThreadStatic]
		internal static Dictionary<Type, object> t_stacks;
	}
}
