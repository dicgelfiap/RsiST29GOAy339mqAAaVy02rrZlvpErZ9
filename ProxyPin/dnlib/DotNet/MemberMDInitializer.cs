using System;
using System.Collections.Generic;

namespace dnlib.DotNet
{
	// Token: 0x02000808 RID: 2056
	internal static class MemberMDInitializer
	{
		// Token: 0x06004A42 RID: 19010 RVA: 0x0017C160 File Offset: 0x0017C160
		public static void Initialize<T>(IEnumerable<T> coll)
		{
			if (coll == null)
			{
				return;
			}
			foreach (T t in coll)
			{
			}
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x0017C1B4 File Offset: 0x0017C1B4
		public static void Initialize(object o)
		{
		}
	}
}
