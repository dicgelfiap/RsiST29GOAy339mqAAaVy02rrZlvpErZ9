using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CB5 RID: 3253
	[ComVisible(true)]
	public static class ImmutableSortedDictionary
	{
		// Token: 0x06008300 RID: 33536 RVA: 0x00266930 File Offset: 0x00266930
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, TValue>()
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x06008301 RID: 33537 RVA: 0x00266938 File Offset: 0x00266938
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, TValue>(IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
		}

		// Token: 0x06008302 RID: 33538 RVA: 0x00266948 File Offset: 0x00266948
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, TValue>(IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x06008303 RID: 33539 RVA: 0x00266958 File Offset: 0x00266958
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.AddRange(items);
		}

		// Token: 0x06008304 RID: 33540 RVA: 0x00266968 File Offset: 0x00266968
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(IComparer<TKey> keyComparer, IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
		}

		// Token: 0x06008305 RID: 33541 RVA: 0x0026697C File Offset: 0x0026697C
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
		}

		// Token: 0x06008306 RID: 33542 RVA: 0x00266990 File Offset: 0x00266990
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>()
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>().ToBuilder();
		}

		// Token: 0x06008307 RID: 33543 RVA: 0x0026699C File Offset: 0x0026699C
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
		}

		// Token: 0x06008308 RID: 33544 RVA: 0x002669AC File Offset: 0x002669AC
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
		}

		// Token: 0x06008309 RID: 33545 RVA: 0x002669BC File Offset: 0x002669BC
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<TSource>>(source, "source");
			Requires.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<TSource, TValue>>(elementSelector, "elementSelector");
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(from element in source
			select new KeyValuePair<TKey, TValue>(keySelector(element), elementSelector(element)));
		}

		// Token: 0x0600830A RID: 33546 RVA: 0x00266A30 File Offset: 0x00266A30
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x0600830B RID: 33547 RVA: 0x00266A44 File Offset: 0x00266A44
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary(keySelector, elementSelector, keyComparer, null);
		}

		// Token: 0x0600830C RID: 33548 RVA: 0x00266A50 File Offset: 0x00266A50
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
		{
			return source.ToImmutableSortedDictionary(keySelector, elementSelector, null, null);
		}

		// Token: 0x0600830D RID: 33549 RVA: 0x00266A5C File Offset: 0x00266A5C
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(source, "source");
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = source as ImmutableSortedDictionary<TKey, TValue>;
			if (immutableSortedDictionary != null)
			{
				return immutableSortedDictionary.WithComparers(keyComparer, valueComparer);
			}
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source);
		}

		// Token: 0x0600830E RID: 33550 RVA: 0x00266AA0 File Offset: 0x00266AA0
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary(keyComparer, null);
		}

		// Token: 0x0600830F RID: 33551 RVA: 0x00266AAC File Offset: 0x00266AAC
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return source.ToImmutableSortedDictionary(null, null);
		}
	}
}
