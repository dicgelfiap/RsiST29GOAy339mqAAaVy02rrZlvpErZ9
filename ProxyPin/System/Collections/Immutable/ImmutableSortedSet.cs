using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CB8 RID: 3256
	[ComVisible(true)]
	public static class ImmutableSortedSet
	{
		// Token: 0x06008355 RID: 33621 RVA: 0x00267454 File Offset: 0x00267454
		public static ImmutableSortedSet<T> Create<T>()
		{
			return ImmutableSortedSet<T>.Empty;
		}

		// Token: 0x06008356 RID: 33622 RVA: 0x0026745C File Offset: 0x0026745C
		public static ImmutableSortedSet<T> Create<T>(IComparer<T> comparer)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		// Token: 0x06008357 RID: 33623 RVA: 0x0026746C File Offset: 0x0026746C
		public static ImmutableSortedSet<T> Create<T>(T item)
		{
			return ImmutableSortedSet<T>.Empty.Add(item);
		}

		// Token: 0x06008358 RID: 33624 RVA: 0x0026747C File Offset: 0x0026747C
		public static ImmutableSortedSet<T> Create<T>(IComparer<T> comparer, T item)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Add(item);
		}

		// Token: 0x06008359 RID: 33625 RVA: 0x00267490 File Offset: 0x00267490
		public static ImmutableSortedSet<T> CreateRange<T>(IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		// Token: 0x0600835A RID: 33626 RVA: 0x002674A0 File Offset: 0x002674A0
		public static ImmutableSortedSet<T> CreateRange<T>(IComparer<T> comparer, IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		// Token: 0x0600835B RID: 33627 RVA: 0x002674B4 File Offset: 0x002674B4
		public static ImmutableSortedSet<T> Create<T>(params T[] items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		// Token: 0x0600835C RID: 33628 RVA: 0x002674C4 File Offset: 0x002674C4
		public static ImmutableSortedSet<T> Create<T>(IComparer<T> comparer, params T[] items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		// Token: 0x0600835D RID: 33629 RVA: 0x002674D8 File Offset: 0x002674D8
		public static ImmutableSortedSet<T>.Builder CreateBuilder<T>()
		{
			return ImmutableSortedSet.Create<T>().ToBuilder();
		}

		// Token: 0x0600835E RID: 33630 RVA: 0x002674E4 File Offset: 0x002674E4
		public static ImmutableSortedSet<T>.Builder CreateBuilder<T>(IComparer<T> comparer)
		{
			return ImmutableSortedSet.Create<T>(comparer).ToBuilder();
		}

		// Token: 0x0600835F RID: 33631 RVA: 0x002674F4 File Offset: 0x002674F4
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
		{
			ImmutableSortedSet<TSource> immutableSortedSet = source as ImmutableSortedSet<TSource>;
			if (immutableSortedSet != null)
			{
				return immutableSortedSet.WithComparer(comparer);
			}
			return ImmutableSortedSet<TSource>.Empty.WithComparer(comparer).Union(source);
		}

		// Token: 0x06008360 RID: 33632 RVA: 0x0026752C File Offset: 0x0026752C
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableSortedSet(null);
		}

		// Token: 0x06008361 RID: 33633 RVA: 0x00267538 File Offset: 0x00267538
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<TSource>(this ImmutableSortedSet<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedSet<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}
	}
}
