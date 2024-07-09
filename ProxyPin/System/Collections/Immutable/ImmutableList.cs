using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CB0 RID: 3248
	[ComVisible(true)]
	public static class ImmutableList
	{
		// Token: 0x06008275 RID: 33397 RVA: 0x00265A04 File Offset: 0x00265A04
		public static ImmutableList<T> Create<T>()
		{
			return ImmutableList<T>.Empty;
		}

		// Token: 0x06008276 RID: 33398 RVA: 0x00265A0C File Offset: 0x00265A0C
		public static ImmutableList<T> Create<T>(T item)
		{
			return ImmutableList<T>.Empty.Add(item);
		}

		// Token: 0x06008277 RID: 33399 RVA: 0x00265A1C File Offset: 0x00265A1C
		public static ImmutableList<T> CreateRange<T>(IEnumerable<T> items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		// Token: 0x06008278 RID: 33400 RVA: 0x00265A2C File Offset: 0x00265A2C
		public static ImmutableList<T> Create<T>(params T[] items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		// Token: 0x06008279 RID: 33401 RVA: 0x00265A3C File Offset: 0x00265A3C
		public static ImmutableList<T>.Builder CreateBuilder<T>()
		{
			return ImmutableList.Create<T>().ToBuilder();
		}

		// Token: 0x0600827A RID: 33402 RVA: 0x00265A48 File Offset: 0x00265A48
		public static ImmutableList<TSource> ToImmutableList<TSource>(this IEnumerable<TSource> source)
		{
			ImmutableList<TSource> immutableList = source as ImmutableList<TSource>;
			if (immutableList != null)
			{
				return immutableList;
			}
			return ImmutableList<TSource>.Empty.AddRange(source);
		}

		// Token: 0x0600827B RID: 33403 RVA: 0x00265A74 File Offset: 0x00265A74
		public static ImmutableList<TSource> ToImmutableList<TSource>(this ImmutableList<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableList<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x0600827C RID: 33404 RVA: 0x00265A88 File Offset: 0x00265A88
		public static IImmutableList<T> Replace<T>(this IImmutableList<T> list, T oldValue, T newValue)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x0600827D RID: 33405 RVA: 0x00265AA4 File Offset: 0x00265AA4
		public static IImmutableList<T> Remove<T>(this IImmutableList<T> list, T value)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Remove(value, EqualityComparer<T>.Default);
		}

		// Token: 0x0600827E RID: 33406 RVA: 0x00265AC0 File Offset: 0x00265AC0
		public static IImmutableList<T> RemoveRange<T>(this IImmutableList<T> list, IEnumerable<T> items)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x0600827F RID: 33407 RVA: 0x00265ADC File Offset: 0x00265ADC
		public static int IndexOf<T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, EqualityComparer<T>.Default);
		}

		// Token: 0x06008280 RID: 33408 RVA: 0x00265AFC File Offset: 0x00265AFC
		public static int IndexOf<T>(this IImmutableList<T> list, T item, IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, equalityComparer);
		}

		// Token: 0x06008281 RID: 33409 RVA: 0x00265B18 File Offset: 0x00265B18
		public static int IndexOf<T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, list.Count - startIndex, EqualityComparer<T>.Default);
		}

		// Token: 0x06008282 RID: 33410 RVA: 0x00265B3C File Offset: 0x00265B3C
		public static int IndexOf<T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x06008283 RID: 33411 RVA: 0x00265B58 File Offset: 0x00265B58
		public static int LastIndexOf<T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, EqualityComparer<T>.Default);
		}

		// Token: 0x06008284 RID: 33412 RVA: 0x00265B9C File Offset: 0x00265B9C
		public static int LastIndexOf<T>(this IImmutableList<T> list, T item, IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, equalityComparer);
		}

		// Token: 0x06008285 RID: 33413 RVA: 0x00265BDC File Offset: 0x00265BDC
		public static int LastIndexOf<T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0 && startIndex == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		// Token: 0x06008286 RID: 33414 RVA: 0x00265C0C File Offset: 0x00265C0C
		public static int LastIndexOf<T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}
	}
}
