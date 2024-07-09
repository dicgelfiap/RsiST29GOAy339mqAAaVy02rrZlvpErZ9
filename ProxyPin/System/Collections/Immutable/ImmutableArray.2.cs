using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System.Collections.Immutable
{
	// Token: 0x02000CA6 RID: 3238
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[NonVersionable]
	[ComVisible(true)]
	public struct ImmutableArray<T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable, IList<!0>, ICollection<!0>, IEquatable<ImmutableArray<T>>, IList, ICollection, IImmutableArray, IStructuralComparable, IStructuralEquatable, IImmutableList<!0>
	{
		// Token: 0x17001BFE RID: 7166
		T IList<!0>.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001BFF RID: 7167
		// (get) Token: 0x0600817E RID: 33150 RVA: 0x00262CA8 File Offset: 0x00262CA8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x0600817F RID: 33151 RVA: 0x00262CAC File Offset: 0x00262CAC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int ICollection<!0>.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x06008180 RID: 33152 RVA: 0x00262CD4 File Offset: 0x00262CD4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int IReadOnlyCollection<!0>.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x17001C02 RID: 7170
		T IReadOnlyList<!0>.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
		}

		// Token: 0x06008182 RID: 33154 RVA: 0x00262D24 File Offset: 0x00262D24
		public ReadOnlySpan<T> AsSpan()
		{
			return new ReadOnlySpan<T>(this.array);
		}

		// Token: 0x06008183 RID: 33155 RVA: 0x00262D34 File Offset: 0x00262D34
		public ReadOnlyMemory<T> AsMemory()
		{
			return new ReadOnlyMemory<T>(this.array);
		}

		// Token: 0x06008184 RID: 33156 RVA: 0x00262D44 File Offset: 0x00262D44
		public int IndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, 0, immutableArray.Length, EqualityComparer<T>.Default);
		}

		// Token: 0x06008185 RID: 33157 RVA: 0x00262D74 File Offset: 0x00262D74
		public int IndexOf(T item, int startIndex, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, equalityComparer);
		}

		// Token: 0x06008186 RID: 33158 RVA: 0x00262DA0 File Offset: 0x00262DA0
		public int IndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, EqualityComparer<T>.Default);
		}

		// Token: 0x06008187 RID: 33159 RVA: 0x00262DD0 File Offset: 0x00262DD0
		public int IndexOf(T item, int startIndex, int count)
		{
			return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x06008188 RID: 33160 RVA: 0x00262DE0 File Offset: 0x00262DE0
		public int IndexOf(T item, int startIndex, int count, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			if (count == 0 && startIndex == 0)
			{
				return -1;
			}
			Requires.Range(startIndex >= 0 && startIndex < immutableArray.Length, "startIndex", null);
			Requires.Range(count >= 0 && startIndex + count <= immutableArray.Length, "count", null);
			equalityComparer = (equalityComparer ?? EqualityComparer<T>.Default);
			if (equalityComparer == EqualityComparer<T>.Default)
			{
				return Array.IndexOf<T>(immutableArray.array, item, startIndex, count);
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (equalityComparer.Equals(immutableArray.array[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06008189 RID: 33161 RVA: 0x00262EB0 File Offset: 0x00262EB0
		public int LastIndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, immutableArray.Length - 1, immutableArray.Length, EqualityComparer<T>.Default);
		}

		// Token: 0x0600818A RID: 33162 RVA: 0x00262EF4 File Offset: 0x00262EF4
		public int LastIndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0 && startIndex == 0)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		// Token: 0x0600818B RID: 33163 RVA: 0x00262F34 File Offset: 0x00262F34
		public int LastIndexOf(T item, int startIndex, int count)
		{
			return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x0600818C RID: 33164 RVA: 0x00262F44 File Offset: 0x00262F44
		public int LastIndexOf(T item, int startIndex, int count, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			if (startIndex == 0 && count == 0)
			{
				return -1;
			}
			Requires.Range(startIndex >= 0 && startIndex < immutableArray.Length, "startIndex", null);
			Requires.Range(count >= 0 && startIndex - count + 1 >= 0, "count", null);
			equalityComparer = (equalityComparer ?? EqualityComparer<T>.Default);
			if (equalityComparer == EqualityComparer<T>.Default)
			{
				return Array.LastIndexOf<T>(immutableArray.array, item, startIndex, count);
			}
			for (int i = startIndex; i >= startIndex - count + 1; i--)
			{
				if (equalityComparer.Equals(item, immutableArray.array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600818D RID: 33165 RVA: 0x00263010 File Offset: 0x00263010
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x0600818E RID: 33166 RVA: 0x00263020 File Offset: 0x00263020
		public ImmutableArray<T> Insert(int index, T item)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.Create<T>(item);
			}
			T[] array = new T[immutableArray.Length + 1];
			array[index] = item;
			if (index != 0)
			{
				Array.Copy(immutableArray.array, 0, array, 0, index);
			}
			if (index != immutableArray.Length)
			{
				Array.Copy(immutableArray.array, index, array, index + 1, immutableArray.Length - index);
			}
			return new ImmutableArray<T>(array);
		}

		// Token: 0x0600818F RID: 33167 RVA: 0x002630D0 File Offset: 0x002630D0
		public ImmutableArray<T> InsertRange(int index, IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.CreateRange<T>(items);
			}
			int count = ImmutableExtensions.GetCount<T>(ref items);
			if (count == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length + count];
			if (index != 0)
			{
				Array.Copy(immutableArray.array, 0, array, 0, index);
			}
			if (index != immutableArray.Length)
			{
				Array.Copy(immutableArray.array, index, array, index + count, immutableArray.Length - index);
			}
			if (!items.TryCopyTo(array, index))
			{
				int num = index;
				foreach (T t in items)
				{
					array[num++] = t;
				}
			}
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06008190 RID: 33168 RVA: 0x002631F0 File Offset: 0x002631F0
		public ImmutableArray<T> InsertRange(int index, ImmutableArray<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			if (immutableArray.IsEmpty)
			{
				return items;
			}
			if (items.IsEmpty)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length + items.Length];
			if (index != 0)
			{
				Array.Copy(immutableArray.array, 0, array, 0, index);
			}
			if (index != immutableArray.Length)
			{
				Array.Copy(immutableArray.array, index, array, index + items.Length, immutableArray.Length - index);
			}
			Array.Copy(items.array, 0, array, index, items.Length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06008191 RID: 33169 RVA: 0x002632C8 File Offset: 0x002632C8
		public ImmutableArray<T> Add(T item)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.Create<T>(item);
			}
			return immutableArray.Insert(immutableArray.Length, item);
		}

		// Token: 0x06008192 RID: 33170 RVA: 0x00263304 File Offset: 0x00263304
		public ImmutableArray<T> AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		// Token: 0x06008193 RID: 33171 RVA: 0x0026332C File Offset: 0x0026332C
		public ImmutableArray<T> AddRange(ImmutableArray<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		// Token: 0x06008194 RID: 33172 RVA: 0x00263354 File Offset: 0x00263354
		public ImmutableArray<T> SetItem(int index, T item)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index < immutableArray.Length, "index", null);
			T[] array = new T[immutableArray.Length];
			Array.Copy(immutableArray.array, 0, array, 0, immutableArray.Length);
			array[index] = item;
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06008195 RID: 33173 RVA: 0x002633C8 File Offset: 0x002633C8
		public ImmutableArray<T> Replace(T oldValue, T newValue)
		{
			return this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x06008196 RID: 33174 RVA: 0x002633D8 File Offset: 0x002633D8
		public ImmutableArray<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			int num = immutableArray.IndexOf(oldValue, 0, immutableArray.Length, equalityComparer);
			if (num < 0)
			{
				throw new ArgumentException(System.Collections.Immutable2448884.SR.CannotFindOldValue, "oldValue");
			}
			return immutableArray.SetItem(num, newValue);
		}

		// Token: 0x06008197 RID: 33175 RVA: 0x00263424 File Offset: 0x00263424
		public ImmutableArray<T> Remove(T item)
		{
			return this.Remove(item, EqualityComparer<T>.Default);
		}

		// Token: 0x06008198 RID: 33176 RVA: 0x00263434 File Offset: 0x00263434
		public ImmutableArray<T> Remove(T item, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> result = this;
			result.ThrowNullRefIfNotInitialized();
			int num = result.IndexOf(item, 0, result.Length, equalityComparer);
			if (num >= 0)
			{
				return result.RemoveAt(num);
			}
			return result;
		}

		// Token: 0x06008199 RID: 33177 RVA: 0x00263478 File Offset: 0x00263478
		public ImmutableArray<T> RemoveAt(int index)
		{
			return this.RemoveRange(index, 1);
		}

		// Token: 0x0600819A RID: 33178 RVA: 0x00263484 File Offset: 0x00263484
		public ImmutableArray<T> RemoveRange(int index, int length)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			Requires.Range(length >= 0 && index + length <= immutableArray.Length, "length", null);
			if (length == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length - length];
			Array.Copy(immutableArray.array, 0, array, 0, index);
			Array.Copy(immutableArray.array, index + length, array, index, immutableArray.Length - index - length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x0600819B RID: 33179 RVA: 0x00263538 File Offset: 0x00263538
		public ImmutableArray<T> RemoveRange(IEnumerable<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x0600819C RID: 33180 RVA: 0x00263548 File Offset: 0x00263548
		public ImmutableArray<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<IEnumerable<T>>(items, "items");
			SortedSet<int> sortedSet = new SortedSet<int>();
			foreach (T item in items)
			{
				int num = immutableArray.IndexOf(item, 0, immutableArray.Length, equalityComparer);
				while (num >= 0 && !sortedSet.Add(num) && num + 1 < immutableArray.Length)
				{
					num = immutableArray.IndexOf(item, num + 1, equalityComparer);
				}
			}
			return immutableArray.RemoveAtRange(sortedSet);
		}

		// Token: 0x0600819D RID: 33181 RVA: 0x00263604 File Offset: 0x00263604
		public ImmutableArray<T> RemoveRange(ImmutableArray<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x0600819E RID: 33182 RVA: 0x00263614 File Offset: 0x00263614
		public ImmutableArray<T> RemoveRange(ImmutableArray<T> items, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> result = this;
			Requires.NotNull<T[]>(items.array, "items");
			if (items.IsEmpty)
			{
				result.ThrowNullRefIfNotInitialized();
				return result;
			}
			if (items.Length == 1)
			{
				return result.Remove(items[0], equalityComparer);
			}
			return result.RemoveRange(items.array, equalityComparer);
		}

		// Token: 0x0600819F RID: 33183 RVA: 0x00263680 File Offset: 0x00263680
		public ImmutableArray<T> RemoveAll(Predicate<T> match)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Predicate<T>>(match, "match");
			if (immutableArray.IsEmpty)
			{
				return immutableArray;
			}
			List<int> list = null;
			for (int i = 0; i < immutableArray.array.Length; i++)
			{
				if (match(immutableArray.array[i]))
				{
					if (list == null)
					{
						list = new List<int>();
					}
					list.Add(i);
				}
			}
			if (list == null)
			{
				return immutableArray;
			}
			return immutableArray.RemoveAtRange(list);
		}

		// Token: 0x060081A0 RID: 33184 RVA: 0x0026370C File Offset: 0x0026370C
		public ImmutableArray<T> Clear()
		{
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x060081A1 RID: 33185 RVA: 0x00263714 File Offset: 0x00263714
		public ImmutableArray<T> Sort()
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, Comparer<T>.Default);
		}

		// Token: 0x060081A2 RID: 33186 RVA: 0x00263740 File Offset: 0x00263740
		public ImmutableArray<T> Sort(Comparison<T> comparison)
		{
			Requires.NotNull<Comparison<T>>(comparison, "comparison");
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(Comparer<T>.Create(comparison));
		}

		// Token: 0x060081A3 RID: 33187 RVA: 0x00263770 File Offset: 0x00263770
		public ImmutableArray<T> Sort(IComparer<T> comparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, comparer);
		}

		// Token: 0x060081A4 RID: 33188 RVA: 0x00263798 File Offset: 0x00263798
		public ImmutableArray<T> Sort(int index, int count, IComparer<T> comparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0 && index + count <= immutableArray.Length, "count", null);
			if (count > 1)
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				bool flag = false;
				for (int i = index + 1; i < index + count; i++)
				{
					if (comparer.Compare(immutableArray.array[i - 1], immutableArray.array[i]) > 0)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					T[] array = new T[immutableArray.Length];
					Array.Copy(immutableArray.array, 0, array, 0, immutableArray.Length);
					Array.Sort<T>(array, index, count, comparer);
					return new ImmutableArray<T>(array);
				}
			}
			return immutableArray;
		}

		// Token: 0x060081A5 RID: 33189 RVA: 0x00263880 File Offset: 0x00263880
		public IEnumerable<TResult> OfType<TResult>()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array == null || immutableArray.array.Length == 0)
			{
				return Enumerable.Empty<TResult>();
			}
			return immutableArray.array.OfType<TResult>();
		}

		// Token: 0x060081A6 RID: 33190 RVA: 0x002638C0 File Offset: 0x002638C0
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060081A7 RID: 33191 RVA: 0x002638C8 File Offset: 0x002638C8
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060081A8 RID: 33192 RVA: 0x002638D0 File Offset: 0x002638D0
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060081A9 RID: 33193 RVA: 0x002638D8 File Offset: 0x002638D8
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060081AA RID: 33194 RVA: 0x002638E0 File Offset: 0x002638E0
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060081AB RID: 33195 RVA: 0x002638E8 File Offset: 0x002638E8
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Clear()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Clear();
		}

		// Token: 0x060081AC RID: 33196 RVA: 0x00263914 File Offset: 0x00263914
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Add(T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Add(value);
		}

		// Token: 0x060081AD RID: 33197 RVA: 0x00263940 File Offset: 0x00263940
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.AddRange(items);
		}

		// Token: 0x060081AE RID: 33198 RVA: 0x0026396C File Offset: 0x0026396C
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Insert(int index, T element)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Insert(index, element);
		}

		// Token: 0x060081AF RID: 33199 RVA: 0x0026399C File Offset: 0x0026399C
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.InsertRange(int index, IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.InsertRange(index, items);
		}

		// Token: 0x060081B0 RID: 33200 RVA: 0x002639CC File Offset: 0x002639CC
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Remove(value, equalityComparer);
		}

		// Token: 0x060081B1 RID: 33201 RVA: 0x002639FC File Offset: 0x002639FC
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveAll(Predicate<T> match)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAll(match);
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x00263A28 File Offset: 0x00263A28
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(items, equalityComparer);
		}

		// Token: 0x060081B3 RID: 33203 RVA: 0x00263A58 File Offset: 0x00263A58
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveRange(int index, int count)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(index, count);
		}

		// Token: 0x060081B4 RID: 33204 RVA: 0x00263A88 File Offset: 0x00263A88
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveAt(int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAt(index);
		}

		// Token: 0x060081B5 RID: 33205 RVA: 0x00263AB4 File Offset: 0x00263AB4
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.SetItem(int index, T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.SetItem(index, value);
		}

		// Token: 0x060081B6 RID: 33206 RVA: 0x00263AE4 File Offset: 0x00263AE4
		IImmutableList<T> IImmutableList<!0>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Replace(oldValue, newValue, equalityComparer);
		}

		// Token: 0x060081B7 RID: 33207 RVA: 0x00263B14 File Offset: 0x00263B14
		[ExcludeFromCodeCoverage]
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060081B8 RID: 33208 RVA: 0x00263B1C File Offset: 0x00263B1C
		[ExcludeFromCodeCoverage]
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060081B9 RID: 33209 RVA: 0x00263B24 File Offset: 0x00263B24
		[ExcludeFromCodeCoverage]
		bool IList.Contains(object value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Contains((T)((object)value));
		}

		// Token: 0x060081BA RID: 33210 RVA: 0x00263B50 File Offset: 0x00263B50
		[ExcludeFromCodeCoverage]
		int IList.IndexOf(object value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.IndexOf((T)((object)value));
		}

		// Token: 0x060081BB RID: 33211 RVA: 0x00263B7C File Offset: 0x00263B7C
		[ExcludeFromCodeCoverage]
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x060081BC RID: 33212 RVA: 0x00263B84 File Offset: 0x00263B84
		[ExcludeFromCodeCoverage]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x060081BD RID: 33213 RVA: 0x00263B88 File Offset: 0x00263B88
		[ExcludeFromCodeCoverage]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x060081BE RID: 33214 RVA: 0x00263B8C File Offset: 0x00263B8C
		[ExcludeFromCodeCoverage]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int ICollection.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x060081BF RID: 33215 RVA: 0x00263BB4 File Offset: 0x00263BB4
		[ExcludeFromCodeCoverage]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x060081C0 RID: 33216 RVA: 0x00263BB8 File Offset: 0x00263BB8
		[ExcludeFromCodeCoverage]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060081C1 RID: 33217 RVA: 0x00263BC0 File Offset: 0x00263BC0
		[ExcludeFromCodeCoverage]
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060081C2 RID: 33218 RVA: 0x00263BC8 File Offset: 0x00263BC8
		[ExcludeFromCodeCoverage]
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C08 RID: 7176
		[ExcludeFromCodeCoverage]
		object IList.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060081C5 RID: 33221 RVA: 0x00263C04 File Offset: 0x00263C04
		[ExcludeFromCodeCoverage]
		void ICollection.CopyTo(Array array, int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			Array.Copy(immutableArray.array, 0, array, index, immutableArray.Length);
		}

		// Token: 0x060081C6 RID: 33222 RVA: 0x00263C38 File Offset: 0x00263C38
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			Array array = other as Array;
			if (array == null)
			{
				IImmutableArray immutableArray2 = other as IImmutableArray;
				if (immutableArray2 != null)
				{
					array = immutableArray2.Array;
					if (immutableArray.array == null && array == null)
					{
						return true;
					}
					if (immutableArray.array == null)
					{
						return false;
					}
				}
			}
			IStructuralEquatable structuralEquatable = immutableArray.array;
			return structuralEquatable.Equals(array, comparer);
		}

		// Token: 0x060081C7 RID: 33223 RVA: 0x00263CA0 File Offset: 0x00263CA0
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			IStructuralEquatable structuralEquatable = immutableArray.array;
			if (structuralEquatable == null)
			{
				return immutableArray.GetHashCode();
			}
			return structuralEquatable.GetHashCode(comparer);
		}

		// Token: 0x060081C8 RID: 33224 RVA: 0x00263CDC File Offset: 0x00263CDC
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			Array array = other as Array;
			if (array == null)
			{
				IImmutableArray immutableArray2 = other as IImmutableArray;
				if (immutableArray2 != null)
				{
					array = immutableArray2.Array;
					if (immutableArray.array == null && array == null)
					{
						return 0;
					}
					if (immutableArray.array == null ^ array == null)
					{
						throw new ArgumentException(System.Collections.Immutable2448884.SR.ArrayInitializedStateNotEqual, "other");
					}
				}
			}
			if (array != null)
			{
				IStructuralComparable structuralComparable = immutableArray.array;
				return structuralComparable.CompareTo(array, comparer);
			}
			throw new ArgumentException(System.Collections.Immutable2448884.SR.ArrayLengthsNotEqual, "other");
		}

		// Token: 0x060081C9 RID: 33225 RVA: 0x00263D70 File Offset: 0x00263D70
		private ImmutableArray<T> RemoveAtRange(ICollection<int> indicesToRemove)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<ICollection<int>>(indicesToRemove, "indicesToRemove");
			if (indicesToRemove.Count == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length - indicesToRemove.Count];
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			foreach (int num4 in indicesToRemove)
			{
				int num5 = (num3 == -1) ? num4 : (num4 - num3 - 1);
				Array.Copy(immutableArray.array, num + num2, array, num, num5);
				num2++;
				num += num5;
				num3 = num4;
			}
			Array.Copy(immutableArray.array, num + num2, array, num, immutableArray.Length - (num + num2));
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060081CA RID: 33226 RVA: 0x00263E5C File Offset: 0x00263E5C
		internal ImmutableArray(T[] items)
		{
			this.array = items;
		}

		// Token: 0x060081CB RID: 33227 RVA: 0x00263E68 File Offset: 0x00263E68
		[NonVersionable]
		public static bool operator ==(ImmutableArray<T> left, ImmutableArray<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060081CC RID: 33228 RVA: 0x00263E74 File Offset: 0x00263E74
		[NonVersionable]
		public static bool operator !=(ImmutableArray<T> left, ImmutableArray<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060081CD RID: 33229 RVA: 0x00263E84 File Offset: 0x00263E84
		public static bool operator ==(ImmutableArray<T>? left, ImmutableArray<T>? right)
		{
			return left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x060081CE RID: 33230 RVA: 0x00263EAC File Offset: 0x00263EAC
		public static bool operator !=(ImmutableArray<T>? left, ImmutableArray<T>? right)
		{
			return !left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x17001C09 RID: 7177
		public T this[int index]
		{
			[NonVersionable]
			get
			{
				return this.array[index];
			}
		}

		// Token: 0x060081D0 RID: 33232 RVA: 0x00263EE8 File Offset: 0x00263EE8
		[return: System.Collections.Immutable.IsReadOnly]
		public ref T ItemRef(int index)
		{
			return ref this.array[index];
		}

		// Token: 0x17001C0A RID: 7178
		// (get) Token: 0x060081D1 RID: 33233 RVA: 0x00263EF8 File Offset: 0x00263EF8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsEmpty
		{
			[NonVersionable]
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17001C0B RID: 7179
		// (get) Token: 0x060081D2 RID: 33234 RVA: 0x00263F04 File Offset: 0x00263F04
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int Length
		{
			[NonVersionable]
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x17001C0C RID: 7180
		// (get) Token: 0x060081D3 RID: 33235 RVA: 0x00263F10 File Offset: 0x00263F10
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsDefault
		{
			get
			{
				return this.array == null;
			}
		}

		// Token: 0x17001C0D RID: 7181
		// (get) Token: 0x060081D4 RID: 33236 RVA: 0x00263F1C File Offset: 0x00263F1C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsDefaultOrEmpty
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				return immutableArray.array == null || immutableArray.array.Length == 0;
			}
		}

		// Token: 0x17001C0E RID: 7182
		// (get) Token: 0x060081D5 RID: 33237 RVA: 0x00263F4C File Offset: 0x00263F4C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Array IImmutableArray.Array
		{
			get
			{
				return this.array;
			}
		}

		// Token: 0x17001C0F RID: 7183
		// (get) Token: 0x060081D6 RID: 33238 RVA: 0x00263F54 File Offset: 0x00263F54
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				if (!immutableArray.IsDefault)
				{
					return string.Format(CultureInfo.CurrentCulture, "Length = {0}", immutableArray.Length);
				}
				return "Uninitialized";
			}
		}

		// Token: 0x060081D7 RID: 33239 RVA: 0x00263F9C File Offset: 0x00263F9C
		public void CopyTo(T[] destination)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, 0, destination, 0, immutableArray.Length);
		}

		// Token: 0x060081D8 RID: 33240 RVA: 0x00263FD0 File Offset: 0x00263FD0
		public void CopyTo(T[] destination, int destinationIndex)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, 0, destination, destinationIndex, immutableArray.Length);
		}

		// Token: 0x060081D9 RID: 33241 RVA: 0x00264004 File Offset: 0x00264004
		public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, sourceIndex, destination, destinationIndex, length);
		}

		// Token: 0x060081DA RID: 33242 RVA: 0x00264034 File Offset: 0x00264034
		public ImmutableArray<T>.Builder ToBuilder()
		{
			ImmutableArray<T> items = this;
			if (items.Length == 0)
			{
				return new ImmutableArray<T>.Builder();
			}
			ImmutableArray<T>.Builder builder = new ImmutableArray<T>.Builder(items.Length);
			builder.AddRange(items);
			return builder;
		}

		// Token: 0x060081DB RID: 33243 RVA: 0x00264074 File Offset: 0x00264074
		public ImmutableArray<T>.Enumerator GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			return new ImmutableArray<T>.Enumerator(immutableArray.array);
		}

		// Token: 0x060081DC RID: 33244 RVA: 0x002640A0 File Offset: 0x002640A0
		public override int GetHashCode()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array != null)
			{
				return immutableArray.array.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060081DD RID: 33245 RVA: 0x002640D0 File Offset: 0x002640D0
		public override bool Equals(object obj)
		{
			IImmutableArray immutableArray = obj as IImmutableArray;
			return immutableArray != null && this.array == immutableArray.Array;
		}

		// Token: 0x060081DE RID: 33246 RVA: 0x00264100 File Offset: 0x00264100
		[NonVersionable]
		public bool Equals(ImmutableArray<T> other)
		{
			return this.array == other.array;
		}

		// Token: 0x060081DF RID: 33247 RVA: 0x00264110 File Offset: 0x00264110
		public static ImmutableArray<T> CastUp<TDerived>(ImmutableArray<TDerived> items) where TDerived : class, T
		{
			T[] items2 = items.array;
			return new ImmutableArray<T>(items2);
		}

		// Token: 0x060081E0 RID: 33248 RVA: 0x00264130 File Offset: 0x00264130
		public ImmutableArray<TOther> CastArray<TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>((TOther[])this.array);
		}

		// Token: 0x060081E1 RID: 33249 RVA: 0x00264144 File Offset: 0x00264144
		public ImmutableArray<TOther> As<TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>(this.array as TOther[]);
		}

		// Token: 0x060081E2 RID: 33250 RVA: 0x00264158 File Offset: 0x00264158
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		// Token: 0x060081E3 RID: 33251 RVA: 0x00264184 File Offset: 0x00264184
		IEnumerator IEnumerable.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		// Token: 0x060081E4 RID: 33252 RVA: 0x002641B0 File Offset: 0x002641B0
		internal void ThrowNullRefIfNotInitialized()
		{
			int num = this.array.Length;
		}

		// Token: 0x060081E5 RID: 33253 RVA: 0x002641CC File Offset: 0x002641CC
		private void ThrowInvalidOperationIfNotInitialized()
		{
			if (this.IsDefault)
			{
				throw new InvalidOperationException(System.Collections.Immutable2448884.SR.InvalidOperationOnDefaultArray);
			}
		}

		// Token: 0x04003D2B RID: 15659
		public static readonly ImmutableArray<T> Empty = new ImmutableArray<T>(new T[0]);

		// Token: 0x04003D2C RID: 15660
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		internal T[] array;

		// Token: 0x02001199 RID: 4505
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableArrayBuilderDebuggerProxy<>))]
		public sealed class Builder : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable, IReadOnlyList<!0>, IReadOnlyCollection<!0>
		{
			// Token: 0x0600940D RID: 37901 RVA: 0x002C492C File Offset: 0x002C492C
			internal Builder(int capacity)
			{
				Requires.Range(capacity >= 0, "capacity", null);
				this._elements = new T[capacity];
				this._count = 0;
			}

			// Token: 0x0600940E RID: 37902 RVA: 0x002C495C File Offset: 0x002C495C
			internal Builder() : this(8)
			{
			}

			// Token: 0x17001EAD RID: 7853
			// (get) Token: 0x0600940F RID: 37903 RVA: 0x002C4968 File Offset: 0x002C4968
			// (set) Token: 0x06009410 RID: 37904 RVA: 0x002C4974 File Offset: 0x002C4974
			public int Capacity
			{
				get
				{
					return this._elements.Length;
				}
				set
				{
					if (value < this._count)
					{
						throw new ArgumentException(System.Collections.Immutable2448884.SR.CapacityMustBeGreaterThanOrEqualToCount, "value");
					}
					if (value != this._elements.Length)
					{
						if (value > 0)
						{
							T[] array = new T[value];
							if (this._count > 0)
							{
								Array.Copy(this._elements, 0, array, 0, this._count);
							}
							this._elements = array;
							return;
						}
						this._elements = ImmutableArray<T>.Empty.array;
					}
				}
			}

			// Token: 0x17001EAE RID: 7854
			// (get) Token: 0x06009411 RID: 37905 RVA: 0x002C49F8 File Offset: 0x002C49F8
			// (set) Token: 0x06009412 RID: 37906 RVA: 0x002C4A00 File Offset: 0x002C4A00
			public int Count
			{
				get
				{
					return this._count;
				}
				set
				{
					Requires.Range(value >= 0, "value", null);
					if (value < this._count)
					{
						if (this._count - value > 64)
						{
							Array.Clear(this._elements, value, this._count - value);
						}
						else
						{
							for (int i = value; i < this.Count; i++)
							{
								this._elements[i] = default(T);
							}
						}
					}
					else if (value > this._count)
					{
						this.EnsureCapacity(value);
					}
					this._count = value;
				}
			}

			// Token: 0x06009413 RID: 37907 RVA: 0x002C4AA0 File Offset: 0x002C4AA0
			private static void ThrowIndexOutOfRangeException()
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x17001EAF RID: 7855
			public T this[int index]
			{
				get
				{
					if (index >= this.Count)
					{
						ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
					}
					return this._elements[index];
				}
				set
				{
					if (index >= this.Count)
					{
						ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
					}
					this._elements[index] = value;
				}
			}

			// Token: 0x06009416 RID: 37910 RVA: 0x002C4AE8 File Offset: 0x002C4AE8
			[return: System.Collections.Immutable.IsReadOnly]
			public ref T ItemRef(int index)
			{
				if (index >= this.Count)
				{
					ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
				}
				return ref this._elements[index];
			}

			// Token: 0x17001EB0 RID: 7856
			// (get) Token: 0x06009417 RID: 37911 RVA: 0x002C4B0C File Offset: 0x002C4B0C
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06009418 RID: 37912 RVA: 0x002C4B10 File Offset: 0x002C4B10
			public ImmutableArray<T> ToImmutable()
			{
				return new ImmutableArray<T>(this.ToArray());
			}

			// Token: 0x06009419 RID: 37913 RVA: 0x002C4B20 File Offset: 0x002C4B20
			public ImmutableArray<T> MoveToImmutable()
			{
				if (this.Capacity != this.Count)
				{
					throw new InvalidOperationException(System.Collections.Immutable2448884.SR.CapacityMustEqualCountOnMove);
				}
				T[] elements = this._elements;
				this._elements = ImmutableArray<T>.Empty.array;
				this._count = 0;
				return new ImmutableArray<T>(elements);
			}

			// Token: 0x0600941A RID: 37914 RVA: 0x002C4B74 File Offset: 0x002C4B74
			public void Clear()
			{
				this.Count = 0;
			}

			// Token: 0x0600941B RID: 37915 RVA: 0x002C4B80 File Offset: 0x002C4B80
			public void Insert(int index, T item)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				this.EnsureCapacity(this.Count + 1);
				if (index < this.Count)
				{
					Array.Copy(this._elements, index, this._elements, index + 1, this.Count - index);
				}
				this._count++;
				this._elements[index] = item;
			}

			// Token: 0x0600941C RID: 37916 RVA: 0x002C4C08 File Offset: 0x002C4C08
			public void Add(T item)
			{
				int num = this._count + 1;
				this.EnsureCapacity(num);
				this._elements[this._count] = item;
				this._count = num;
			}

			// Token: 0x0600941D RID: 37917 RVA: 0x002C4C44 File Offset: 0x002C4C44
			public void AddRange(IEnumerable<T> items)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				int num;
				if (items.TryGetCount(out num))
				{
					this.EnsureCapacity(this.Count + num);
					if (items.TryCopyTo(this._elements, this._count))
					{
						this._count += num;
						return;
					}
				}
				foreach (T item in items)
				{
					this.Add(item);
				}
			}

			// Token: 0x0600941E RID: 37918 RVA: 0x002C4CE4 File Offset: 0x002C4CE4
			public void AddRange(params T[] items)
			{
				Requires.NotNull<T[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				Array.Copy(items, 0, this._elements, count, items.Length);
			}

			// Token: 0x0600941F RID: 37919 RVA: 0x002C4D28 File Offset: 0x002C4D28
			public void AddRange<TDerived>(TDerived[] items) where TDerived : T
			{
				Requires.NotNull<TDerived[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				Array.Copy(items, 0, this._elements, count, items.Length);
			}

			// Token: 0x06009420 RID: 37920 RVA: 0x002C4D6C File Offset: 0x002C4D6C
			public void AddRange(T[] items, int length)
			{
				Requires.NotNull<T[]>(items, "items");
				Requires.Range(length >= 0 && length <= items.Length, "length", null);
				int count = this.Count;
				this.Count += length;
				Array.Copy(items, 0, this._elements, count, length);
			}

			// Token: 0x06009421 RID: 37921 RVA: 0x002C4DD0 File Offset: 0x002C4DD0
			public void AddRange(ImmutableArray<T> items)
			{
				this.AddRange(items, items.Length);
			}

			// Token: 0x06009422 RID: 37922 RVA: 0x002C4DE0 File Offset: 0x002C4DE0
			public void AddRange(ImmutableArray<T> items, int length)
			{
				Requires.Range(length >= 0, "length", null);
				if (items.array != null)
				{
					this.AddRange(items.array, length);
				}
			}

			// Token: 0x06009423 RID: 37923 RVA: 0x002C4E0C File Offset: 0x002C4E0C
			public void AddRange<TDerived>(ImmutableArray<TDerived> items) where TDerived : T
			{
				if (items.array != null)
				{
					this.AddRange<TDerived>(items.array);
				}
			}

			// Token: 0x06009424 RID: 37924 RVA: 0x002C4E28 File Offset: 0x002C4E28
			public void AddRange(ImmutableArray<T>.Builder items)
			{
				Requires.NotNull<ImmutableArray<T>.Builder>(items, "items");
				this.AddRange(items._elements, items.Count);
			}

			// Token: 0x06009425 RID: 37925 RVA: 0x002C4E48 File Offset: 0x002C4E48
			public void AddRange<TDerived>(ImmutableArray<TDerived>.Builder items) where TDerived : T
			{
				Requires.NotNull<ImmutableArray<TDerived>.Builder>(items, "items");
				this.AddRange<TDerived>(items._elements, items.Count);
			}

			// Token: 0x06009426 RID: 37926 RVA: 0x002C4E68 File Offset: 0x002C4E68
			public bool Remove(T element)
			{
				int num = this.IndexOf(element);
				if (num >= 0)
				{
					this.RemoveAt(num);
					return true;
				}
				return false;
			}

			// Token: 0x06009427 RID: 37927 RVA: 0x002C4E94 File Offset: 0x002C4E94
			public void RemoveAt(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				if (index < this.Count - 1)
				{
					Array.Copy(this._elements, index + 1, this._elements, index, this.Count - index - 1);
				}
				int count = this.Count;
				this.Count = count - 1;
			}

			// Token: 0x06009428 RID: 37928 RVA: 0x002C4F04 File Offset: 0x002C4F04
			public bool Contains(T item)
			{
				return this.IndexOf(item) >= 0;
			}

			// Token: 0x06009429 RID: 37929 RVA: 0x002C4F14 File Offset: 0x002C4F14
			public T[] ToArray()
			{
				if (this.Count == 0)
				{
					return ImmutableArray<T>.Empty.array;
				}
				T[] array = new T[this.Count];
				Array.Copy(this._elements, 0, array, 0, this.Count);
				return array;
			}

			// Token: 0x0600942A RID: 37930 RVA: 0x002C4F5C File Offset: 0x002C4F5C
			public void CopyTo(T[] array, int index)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(index >= 0 && index + this.Count <= array.Length, "index", null);
				Array.Copy(this._elements, 0, array, index, this.Count);
			}

			// Token: 0x0600942B RID: 37931 RVA: 0x002C4FB4 File Offset: 0x002C4FB4
			private void EnsureCapacity(int capacity)
			{
				if (this._elements.Length < capacity)
				{
					int newSize = Math.Max(this._elements.Length * 2, capacity);
					Array.Resize<T>(ref this._elements, newSize);
				}
			}

			// Token: 0x0600942C RID: 37932 RVA: 0x002C4FF0 File Offset: 0x002C4FF0
			public int IndexOf(T item)
			{
				return this.IndexOf(item, 0, this._count, EqualityComparer<T>.Default);
			}

			// Token: 0x0600942D RID: 37933 RVA: 0x002C5008 File Offset: 0x002C5008
			public int IndexOf(T item, int startIndex)
			{
				return this.IndexOf(item, startIndex, this.Count - startIndex, EqualityComparer<T>.Default);
			}

			// Token: 0x0600942E RID: 37934 RVA: 0x002C5020 File Offset: 0x002C5020
			public int IndexOf(T item, int startIndex, int count)
			{
				return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x0600942F RID: 37935 RVA: 0x002C5030 File Offset: 0x002C5030
			public int IndexOf(T item, int startIndex, int count, IEqualityComparer<T> equalityComparer)
			{
				if (count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				Requires.Range(count >= 0 && startIndex + count <= this.Count, "count", null);
				equalityComparer = (equalityComparer ?? EqualityComparer<T>.Default);
				if (equalityComparer == EqualityComparer<T>.Default)
				{
					return Array.IndexOf<T>(this._elements, item, startIndex, count);
				}
				for (int i = startIndex; i < startIndex + count; i++)
				{
					if (equalityComparer.Equals(this._elements[i], item))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06009430 RID: 37936 RVA: 0x002C50F0 File Offset: 0x002C50F0
			public int LastIndexOf(T item)
			{
				if (this.Count == 0)
				{
					return -1;
				}
				return this.LastIndexOf(item, this.Count - 1, this.Count, EqualityComparer<T>.Default);
			}

			// Token: 0x06009431 RID: 37937 RVA: 0x002C5128 File Offset: 0x002C5128
			public int LastIndexOf(T item, int startIndex)
			{
				if (this.Count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				return this.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
			}

			// Token: 0x06009432 RID: 37938 RVA: 0x002C5180 File Offset: 0x002C5180
			public int LastIndexOf(T item, int startIndex, int count)
			{
				return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x06009433 RID: 37939 RVA: 0x002C5190 File Offset: 0x002C5190
			public int LastIndexOf(T item, int startIndex, int count, IEqualityComparer<T> equalityComparer)
			{
				if (count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				Requires.Range(count >= 0 && startIndex - count + 1 >= 0, "count", null);
				equalityComparer = (equalityComparer ?? EqualityComparer<T>.Default);
				if (equalityComparer == EqualityComparer<T>.Default)
				{
					return Array.LastIndexOf<T>(this._elements, item, startIndex, count);
				}
				for (int i = startIndex; i >= startIndex - count + 1; i--)
				{
					if (equalityComparer.Equals(item, this._elements[i]))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06009434 RID: 37940 RVA: 0x002C5250 File Offset: 0x002C5250
			public void Reverse()
			{
				int i = 0;
				int num = this._count - 1;
				T[] elements = this._elements;
				while (i < num)
				{
					T t = elements[i];
					elements[i] = elements[num];
					elements[num] = t;
					i++;
					num--;
				}
			}

			// Token: 0x06009435 RID: 37941 RVA: 0x002C52A4 File Offset: 0x002C52A4
			public void Sort()
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this.Count, Comparer<T>.Default);
				}
			}

			// Token: 0x06009436 RID: 37942 RVA: 0x002C52CC File Offset: 0x002C52CC
			public void Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this._count, Comparer<T>.Create(comparison));
				}
			}

			// Token: 0x06009437 RID: 37943 RVA: 0x002C5300 File Offset: 0x002C5300
			public void Sort(IComparer<T> comparer)
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this._count, comparer);
				}
			}

			// Token: 0x06009438 RID: 37944 RVA: 0x002C5324 File Offset: 0x002C5324
			public void Sort(int index, int count, IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0 && index + count <= this.Count, "count", null);
				if (count > 1)
				{
					Array.Sort<T>(this._elements, index, count, comparer);
				}
			}

			// Token: 0x06009439 RID: 37945 RVA: 0x002C5384 File Offset: 0x002C5384
			public IEnumerator<T> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.Count; i = num + 1)
				{
					yield return this[i];
					num = i;
				}
				yield break;
			}

			// Token: 0x0600943A RID: 37946 RVA: 0x002C5394 File Offset: 0x002C5394
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600943B RID: 37947 RVA: 0x002C539C File Offset: 0x002C539C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600943C RID: 37948 RVA: 0x002C53A4 File Offset: 0x002C53A4
			private void AddRange<TDerived>(TDerived[] items, int length) where TDerived : T
			{
				this.EnsureCapacity(this.Count + length);
				int count = this.Count;
				this.Count += length;
				T[] elements = this._elements;
				for (int i = 0; i < length; i++)
				{
					elements[count + i] = (T)((object)items[i]);
				}
			}

			// Token: 0x04004BCC RID: 19404
			private T[] _elements;

			// Token: 0x04004BCD RID: 19405
			private int _count;
		}

		// Token: 0x0200119A RID: 4506
		public struct Enumerator
		{
			// Token: 0x0600943D RID: 37949 RVA: 0x002C5408 File Offset: 0x002C5408
			internal Enumerator(T[] array)
			{
				this._array = array;
				this._index = -1;
			}

			// Token: 0x17001EB1 RID: 7857
			// (get) Token: 0x0600943E RID: 37950 RVA: 0x002C5418 File Offset: 0x002C5418
			public T Current
			{
				get
				{
					return this._array[this._index];
				}
			}

			// Token: 0x0600943F RID: 37951 RVA: 0x002C542C File Offset: 0x002C542C
			public bool MoveNext()
			{
				int num = this._index + 1;
				this._index = num;
				return num < this._array.Length;
			}

			// Token: 0x04004BCE RID: 19406
			private readonly T[] _array;

			// Token: 0x04004BCF RID: 19407
			private int _index;
		}

		// Token: 0x0200119B RID: 4507
		private class EnumeratorObject : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06009440 RID: 37952 RVA: 0x002C5458 File Offset: 0x002C5458
			private EnumeratorObject(T[] array)
			{
				this._index = -1;
				this._array = array;
			}

			// Token: 0x17001EB2 RID: 7858
			// (get) Token: 0x06009441 RID: 37953 RVA: 0x002C5470 File Offset: 0x002C5470
			public T Current
			{
				get
				{
					if (this._index < this._array.Length)
					{
						return this._array[this._index];
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17001EB3 RID: 7859
			// (get) Token: 0x06009442 RID: 37954 RVA: 0x002C549C File Offset: 0x002C549C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06009443 RID: 37955 RVA: 0x002C54AC File Offset: 0x002C54AC
			public bool MoveNext()
			{
				int num = this._index + 1;
				int num2 = this._array.Length;
				if (num <= num2)
				{
					this._index = num;
					return num < num2;
				}
				return false;
			}

			// Token: 0x06009444 RID: 37956 RVA: 0x002C54E4 File Offset: 0x002C54E4
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x06009445 RID: 37957 RVA: 0x002C54F0 File Offset: 0x002C54F0
			public void Dispose()
			{
			}

			// Token: 0x06009446 RID: 37958 RVA: 0x002C54F4 File Offset: 0x002C54F4
			internal static IEnumerator<T> Create(T[] array)
			{
				if (array.Length != 0)
				{
					return new ImmutableArray<T>.EnumeratorObject(array);
				}
				return ImmutableArray<T>.EnumeratorObject.s_EmptyEnumerator;
			}

			// Token: 0x04004BD0 RID: 19408
			private static readonly IEnumerator<T> s_EmptyEnumerator = new ImmutableArray<T>.EnumeratorObject(ImmutableArray<T>.Empty.array);

			// Token: 0x04004BD1 RID: 19409
			private readonly T[] _array;

			// Token: 0x04004BD2 RID: 19410
			private int _index;
		}
	}
}
