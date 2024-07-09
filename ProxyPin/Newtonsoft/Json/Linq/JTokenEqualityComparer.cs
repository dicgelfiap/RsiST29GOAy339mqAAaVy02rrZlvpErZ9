using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B24 RID: 2852
	[NullableContext(1)]
	[Nullable(0)]
	public class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		// Token: 0x0600737E RID: 29566 RVA: 0x0022B5E0 File Offset: 0x0022B5E0
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		// Token: 0x0600737F RID: 29567 RVA: 0x0022B5EC File Offset: 0x0022B5EC
		public int GetHashCode(JToken obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}
	}
}
