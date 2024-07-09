using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CA5 RID: 3237
	[ComVisible(true)]
	public static class ImmutableArray
	{
		// Token: 0x06008165 RID: 33125 RVA: 0x002626E0 File Offset: 0x002626E0
		public static ImmutableArray<T> Create<T>()
		{
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x06008166 RID: 33126 RVA: 0x002626E8 File Offset: 0x002626E8
		public static ImmutableArray<T> Create<T>(T item)
		{
			T[] items = new T[]
			{
				item
			};
			return new ImmutableArray<T>(items);
		}

		// Token: 0x06008167 RID: 33127 RVA: 0x00262710 File Offset: 0x00262710
		public static ImmutableArray<T> Create<T>(T item1, T item2)
		{
			T[] items = new T[]
			{
				item1,
				item2
			};
			return new ImmutableArray<T>(items);
		}

		// Token: 0x06008168 RID: 33128 RVA: 0x00262740 File Offset: 0x00262740
		public static ImmutableArray<T> Create<T>(T item1, T item2, T item3)
		{
			T[] items = new T[]
			{
				item1,
				item2,
				item3
			};
			return new ImmutableArray<T>(items);
		}

		// Token: 0x06008169 RID: 33129 RVA: 0x00262778 File Offset: 0x00262778
		public static ImmutableArray<T> Create<T>(T item1, T item2, T item3, T item4)
		{
			T[] items = new T[]
			{
				item1,
				item2,
				item3,
				item4
			};
			return new ImmutableArray<T>(items);
		}

		// Token: 0x0600816A RID: 33130 RVA: 0x002627B8 File Offset: 0x002627B8
		public static ImmutableArray<T> CreateRange<T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			IImmutableArray immutableArray = items as IImmutableArray;
			if (immutableArray != null)
			{
				Array array = immutableArray.Array;
				if (array == null)
				{
					throw new InvalidOperationException(System.Collections.Immutable2448884.SR.InvalidOperationOnDefaultArray);
				}
				return new ImmutableArray<T>((T[])array);
			}
			else
			{
				int count;
				if (items.TryGetCount(out count))
				{
					return new ImmutableArray<T>(items.ToArray(count));
				}
				return new ImmutableArray<T>(items.ToArray<T>());
			}
		}

		// Token: 0x0600816B RID: 33131 RVA: 0x0026282C File Offset: 0x0026282C
		public static ImmutableArray<T> Create<T>(params T[] items)
		{
			if (items == null)
			{
				return ImmutableArray.Create<T>();
			}
			return ImmutableArray.CreateDefensiveCopy<T>(items);
		}

		// Token: 0x0600816C RID: 33132 RVA: 0x00262840 File Offset: 0x00262840
		public static ImmutableArray<T> Create<T>(T[] items, int start, int length)
		{
			Requires.NotNull<T[]>(items, "items");
			Requires.Range(start >= 0 && start <= items.Length, "start", null);
			Requires.Range(length >= 0 && start + length <= items.Length, "length", null);
			if (length == 0)
			{
				return ImmutableArray.Create<T>();
			}
			T[] array = new T[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = items[start + i];
			}
			return new ImmutableArray<T>(array);
		}

		// Token: 0x0600816D RID: 33133 RVA: 0x002628DC File Offset: 0x002628DC
		public static ImmutableArray<T> Create<T>(ImmutableArray<T> items, int start, int length)
		{
			Requires.Range(start >= 0 && start <= items.Length, "start", null);
			Requires.Range(length >= 0 && start + length <= items.Length, "length", null);
			if (length == 0)
			{
				return ImmutableArray.Create<T>();
			}
			if (start == 0 && length == items.Length)
			{
				return items;
			}
			T[] array = new T[length];
			Array.Copy(items.array, start, array, 0, length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x0600816E RID: 33134 RVA: 0x00262978 File Offset: 0x00262978
		public static ImmutableArray<TResult> CreateRange<TSource, TResult>(ImmutableArray<TSource> items, Func<TSource, TResult> selector)
		{
			Requires.NotNull<Func<TSource, TResult>>(selector, "selector");
			int length = items.Length;
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i]);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x0600816F RID: 33135 RVA: 0x002629DC File Offset: 0x002629DC
		public static ImmutableArray<TResult> CreateRange<TSource, TResult>(ImmutableArray<TSource> items, int start, int length, Func<TSource, TResult> selector)
		{
			int length2 = items.Length;
			Requires.Range(start >= 0 && start <= length2, "start", null);
			Requires.Range(length >= 0 && start + length <= length2, "length", null);
			Requires.NotNull<Func<TSource, TResult>>(selector, "selector");
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i + start]);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x06008170 RID: 33136 RVA: 0x00262A84 File Offset: 0x00262A84
		public static ImmutableArray<TResult> CreateRange<TSource, TArg, TResult>(ImmutableArray<TSource> items, Func<TSource, TArg, TResult> selector, TArg arg)
		{
			Requires.NotNull<Func<TSource, TArg, TResult>>(selector, "selector");
			int length = items.Length;
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i], arg);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x06008171 RID: 33137 RVA: 0x00262AE8 File Offset: 0x00262AE8
		public static ImmutableArray<TResult> CreateRange<TSource, TArg, TResult>(ImmutableArray<TSource> items, int start, int length, Func<TSource, TArg, TResult> selector, TArg arg)
		{
			int length2 = items.Length;
			Requires.Range(start >= 0 && start <= length2, "start", null);
			Requires.Range(length >= 0 && start + length <= length2, "length", null);
			Requires.NotNull<Func<TSource, TArg, TResult>>(selector, "selector");
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i + start], arg);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x06008172 RID: 33138 RVA: 0x00262B90 File Offset: 0x00262B90
		public static ImmutableArray<T>.Builder CreateBuilder<T>()
		{
			return ImmutableArray.Create<T>().ToBuilder();
		}

		// Token: 0x06008173 RID: 33139 RVA: 0x00262BB0 File Offset: 0x00262BB0
		public static ImmutableArray<T>.Builder CreateBuilder<T>(int initialCapacity)
		{
			return new ImmutableArray<T>.Builder(initialCapacity);
		}

		// Token: 0x06008174 RID: 33140 RVA: 0x00262BB8 File Offset: 0x00262BB8
		public static ImmutableArray<TSource> ToImmutableArray<TSource>(this IEnumerable<TSource> items)
		{
			if (items is ImmutableArray<TSource>)
			{
				return (ImmutableArray<TSource>)items;
			}
			return ImmutableArray.CreateRange<TSource>(items);
		}

		// Token: 0x06008175 RID: 33141 RVA: 0x00262BD4 File Offset: 0x00262BD4
		public static ImmutableArray<TSource> ToImmutableArray<TSource>(this ImmutableArray<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x06008176 RID: 33142 RVA: 0x00262BE8 File Offset: 0x00262BE8
		public static int BinarySearch<T>(this ImmutableArray<T> array, T value)
		{
			return Array.BinarySearch<T>(array.array, value);
		}

		// Token: 0x06008177 RID: 33143 RVA: 0x00262BF8 File Offset: 0x00262BF8
		public static int BinarySearch<T>(this ImmutableArray<T> array, T value, IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.array, value, comparer);
		}

		// Token: 0x06008178 RID: 33144 RVA: 0x00262C08 File Offset: 0x00262C08
		public static int BinarySearch<T>(this ImmutableArray<T> array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array.array, index, length, value);
		}

		// Token: 0x06008179 RID: 33145 RVA: 0x00262C18 File Offset: 0x00262C18
		public static int BinarySearch<T>(this ImmutableArray<T> array, int index, int length, T value, IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.array, index, length, value, comparer);
		}

		// Token: 0x0600817A RID: 33146 RVA: 0x00262C2C File Offset: 0x00262C2C
		internal static ImmutableArray<T> CreateDefensiveCopy<T>(T[] items)
		{
			if (items.Length == 0)
			{
				return ImmutableArray<T>.Empty;
			}
			T[] array = new T[items.Length];
			Array.Copy(items, 0, array, 0, items.Length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x04003D2A RID: 15658
		internal static readonly byte[] TwoElementArray = new byte[2];
	}
}
