using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CAE RID: 3246
	[ComVisible(true)]
	public static class ImmutableHashSet
	{
		// Token: 0x06008257 RID: 33367 RVA: 0x002653D8 File Offset: 0x002653D8
		public static ImmutableHashSet<T> Create<T>()
		{
			return ImmutableHashSet<T>.Empty;
		}

		// Token: 0x06008258 RID: 33368 RVA: 0x002653E0 File Offset: 0x002653E0
		public static ImmutableHashSet<T> Create<T>(IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer);
		}

		// Token: 0x06008259 RID: 33369 RVA: 0x002653F0 File Offset: 0x002653F0
		public static ImmutableHashSet<T> Create<T>(T item)
		{
			return ImmutableHashSet<T>.Empty.Add(item);
		}

		// Token: 0x0600825A RID: 33370 RVA: 0x00265400 File Offset: 0x00265400
		public static ImmutableHashSet<T> Create<T>(IEqualityComparer<T> equalityComparer, T item)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Add(item);
		}

		// Token: 0x0600825B RID: 33371 RVA: 0x00265414 File Offset: 0x00265414
		public static ImmutableHashSet<T> CreateRange<T>(IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		// Token: 0x0600825C RID: 33372 RVA: 0x00265424 File Offset: 0x00265424
		public static ImmutableHashSet<T> CreateRange<T>(IEqualityComparer<T> equalityComparer, IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		// Token: 0x0600825D RID: 33373 RVA: 0x00265438 File Offset: 0x00265438
		public static ImmutableHashSet<T> Create<T>(params T[] items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		// Token: 0x0600825E RID: 33374 RVA: 0x00265448 File Offset: 0x00265448
		public static ImmutableHashSet<T> Create<T>(IEqualityComparer<T> equalityComparer, params T[] items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		// Token: 0x0600825F RID: 33375 RVA: 0x0026545C File Offset: 0x0026545C
		public static ImmutableHashSet<T>.Builder CreateBuilder<T>()
		{
			return ImmutableHashSet.Create<T>().ToBuilder();
		}

		// Token: 0x06008260 RID: 33376 RVA: 0x00265468 File Offset: 0x00265468
		public static ImmutableHashSet<T>.Builder CreateBuilder<T>(IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet.Create<T>(equalityComparer).ToBuilder();
		}

		// Token: 0x06008261 RID: 33377 RVA: 0x00265478 File Offset: 0x00265478
		public static ImmutableHashSet<TSource> ToImmutableHashSet<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> equalityComparer)
		{
			ImmutableHashSet<TSource> immutableHashSet = source as ImmutableHashSet<TSource>;
			if (immutableHashSet != null)
			{
				return immutableHashSet.WithComparer(equalityComparer);
			}
			return ImmutableHashSet<TSource>.Empty.WithComparer(equalityComparer).Union(source);
		}

		// Token: 0x06008262 RID: 33378 RVA: 0x002654B0 File Offset: 0x002654B0
		public static ImmutableHashSet<TSource> ToImmutableHashSet<TSource>(this ImmutableHashSet<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableHashSet<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x06008263 RID: 33379 RVA: 0x002654C4 File Offset: 0x002654C4
		public static ImmutableHashSet<TSource> ToImmutableHashSet<TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableHashSet(null);
		}
	}
}
