using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkUI.Extensions
{
	// Token: 0x02000087 RID: 135
	internal static class IEnumerableExtensions
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x00032920 File Offset: 0x00032920
		internal static bool IsLast<T>(this IEnumerable<T> items, T item)
		{
			T t = items.LastOrDefault<T>();
			return t != null && item.Equals(t);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00032958 File Offset: 0x00032958
		internal static bool IsFirst<T>(this IEnumerable<T> items, T item)
		{
			T t = items.FirstOrDefault<T>();
			return t != null && item.Equals(t);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00032990 File Offset: 0x00032990
		internal static bool IsFirstOrLast<T>(this IEnumerable<T> items, T item)
		{
			return items.IsFirst(item) || items.IsLast(item);
		}
	}
}
