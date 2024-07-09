using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000882 RID: 2178
	[ComVisible(true)]
	public sealed class UTF8StringEqualityComparer : IEqualityComparer<UTF8String>
	{
		// Token: 0x06005339 RID: 21305 RVA: 0x00196BE4 File Offset: 0x00196BE4
		public bool Equals(UTF8String x, UTF8String y)
		{
			return UTF8String.Equals(x, y);
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x00196BF0 File Offset: 0x00196BF0
		public int GetHashCode(UTF8String obj)
		{
			return UTF8String.GetHashCode(obj);
		}

		// Token: 0x040027E5 RID: 10213
		public static readonly UTF8StringEqualityComparer Instance = new UTF8StringEqualityComparer();
	}
}
