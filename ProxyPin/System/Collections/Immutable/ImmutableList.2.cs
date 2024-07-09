using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x02000CB1 RID: 3249
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	[ComVisible(true)]
	public sealed class ImmutableList<T> : IImmutableList<T>, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable, IList<T>, ICollection<T>, IList, ICollection, IOrderedCollection<T>, IImmutableListQueries<T>, IStrongEnumerable<T, ImmutableList<T>.Enumerator>
	{
		// Token: 0x06008287 RID: 33415 RVA: 0x00265C28 File Offset: 0x00265C28
		internal ImmutableList()
		{
			this._root = ImmutableList<T>.Node.EmptyNode;
		}

		// Token: 0x06008288 RID: 33416 RVA: 0x00265C3C File Offset: 0x00265C3C
		private ImmutableList(ImmutableList<T>.Node root)
		{
			Requires.NotNull<ImmutableList<T>.Node>(root, "root");
			root.Freeze();
			this._root = root;
		}

		// Token: 0x06008289 RID: 33417 RVA: 0x00265C5C File Offset: 0x00265C5C
		public ImmutableList<T> Clear()
		{
			return ImmutableList<T>.Empty;
		}

		// Token: 0x0600828A RID: 33418 RVA: 0x00265C64 File Offset: 0x00265C64
		public int BinarySearch(T item)
		{
			return this.BinarySearch(item, null);
		}

		// Token: 0x0600828B RID: 33419 RVA: 0x00265C70 File Offset: 0x00265C70
		public int BinarySearch(T item, IComparer<T> comparer)
		{
			return this.BinarySearch(0, this.Count, item, comparer);
		}

		// Token: 0x0600828C RID: 33420 RVA: 0x00265C84 File Offset: 0x00265C84
		public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
		{
			return this._root.BinarySearch(index, count, item, comparer);
		}

		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x0600828D RID: 33421 RVA: 0x00265C98 File Offset: 0x00265C98
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x0600828E RID: 33422 RVA: 0x00265CA8 File Offset: 0x00265CA8
		IImmutableList<T> IImmutableList<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x0600828F RID: 33423 RVA: 0x00265CB0 File Offset: 0x00265CB0
		public int Count
		{
			get
			{
				return this._root.Count;
			}
		}

		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x06008290 RID: 33424 RVA: 0x00265CC0 File Offset: 0x00265CC0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x06008291 RID: 33425 RVA: 0x00265CC4 File Offset: 0x00265CC4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C2B RID: 7211
		public unsafe T this[int index]
		{
			get
			{
				return *this._root.ItemRef(index);
			}
		}

		// Token: 0x06008293 RID: 33427 RVA: 0x00265CDC File Offset: 0x00265CDC
		[return: System.Collections.Immutable.IsReadOnly]
		public ref T ItemRef(int index)
		{
			return this._root.ItemRef(index);
		}

		// Token: 0x17001C2C RID: 7212
		T IOrderedCollection<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x06008295 RID: 33429 RVA: 0x00265CF8 File Offset: 0x00265CF8
		public ImmutableList<T>.Builder ToBuilder()
		{
			return new ImmutableList<T>.Builder(this);
		}

		// Token: 0x06008296 RID: 33430 RVA: 0x00265D00 File Offset: 0x00265D00
		public ImmutableList<T> Add(T value)
		{
			ImmutableList<T>.Node root = this._root.Add(value);
			return this.Wrap(root);
		}

		// Token: 0x06008297 RID: 33431 RVA: 0x00265D28 File Offset: 0x00265D28
		public ImmutableList<T> AddRange(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (this.IsEmpty)
			{
				return ImmutableList<T>.CreateRange(items);
			}
			ImmutableList<T>.Node root = this._root.AddRange(items);
			return this.Wrap(root);
		}

		// Token: 0x06008298 RID: 33432 RVA: 0x00265D6C File Offset: 0x00265D6C
		public ImmutableList<T> Insert(int index, T item)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			return this.Wrap(this._root.Insert(index, item));
		}

		// Token: 0x06008299 RID: 33433 RVA: 0x00265DA8 File Offset: 0x00265DA8
		public ImmutableList<T> InsertRange(int index, IEnumerable<T> items)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableList<T>.Node root = this._root.InsertRange(index, items);
			return this.Wrap(root);
		}

		// Token: 0x0600829A RID: 33434 RVA: 0x00265E00 File Offset: 0x00265E00
		public ImmutableList<T> Remove(T value)
		{
			return this.Remove(value, EqualityComparer<T>.Default);
		}

		// Token: 0x0600829B RID: 33435 RVA: 0x00265E10 File Offset: 0x00265E10
		public ImmutableList<T> Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			int num = this.IndexOf(value, equalityComparer);
			if (num >= 0)
			{
				return this.RemoveAt(num);
			}
			return this;
		}

		// Token: 0x0600829C RID: 33436 RVA: 0x00265E3C File Offset: 0x00265E3C
		public ImmutableList<T> RemoveRange(int index, int count)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			Requires.Range(count >= 0 && index + count <= this.Count, "count", null);
			ImmutableList<T>.Node node = this._root;
			int num = count;
			while (num-- > 0)
			{
				node = node.RemoveAt(index);
			}
			return this.Wrap(node);
		}

		// Token: 0x0600829D RID: 33437 RVA: 0x00265EBC File Offset: 0x00265EBC
		public ImmutableList<T> RemoveRange(IEnumerable<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x0600829E RID: 33438 RVA: 0x00265ECC File Offset: 0x00265ECC
		public ImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (this.IsEmpty)
			{
				return this;
			}
			ImmutableList<T>.Node node = this._root;
			foreach (T item in items.GetEnumerableDisposable<T, ImmutableList<T>.Enumerator>())
			{
				int num = node.IndexOf(item, equalityComparer);
				if (num >= 0)
				{
					node = node.RemoveAt(num);
				}
			}
			return this.Wrap(node);
		}

		// Token: 0x0600829F RID: 33439 RVA: 0x00265F64 File Offset: 0x00265F64
		public ImmutableList<T> RemoveAt(int index)
		{
			Requires.Range(index >= 0 && index < this.Count, "index", null);
			ImmutableList<T>.Node root = this._root.RemoveAt(index);
			return this.Wrap(root);
		}

		// Token: 0x060082A0 RID: 33440 RVA: 0x00265FAC File Offset: 0x00265FAC
		public ImmutableList<T> RemoveAll(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this.Wrap(this._root.RemoveAll(match));
		}

		// Token: 0x060082A1 RID: 33441 RVA: 0x00265FCC File Offset: 0x00265FCC
		public ImmutableList<T> SetItem(int index, T value)
		{
			return this.Wrap(this._root.ReplaceAt(index, value));
		}

		// Token: 0x060082A2 RID: 33442 RVA: 0x00265FE4 File Offset: 0x00265FE4
		public ImmutableList<T> Replace(T oldValue, T newValue)
		{
			return this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x060082A3 RID: 33443 RVA: 0x00265FF4 File Offset: 0x00265FF4
		public ImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			int num = this.IndexOf(oldValue, equalityComparer);
			if (num < 0)
			{
				throw new ArgumentException(System.Collections.Immutable2448884.SR.CannotFindOldValue, "oldValue");
			}
			return this.SetItem(num, newValue);
		}

		// Token: 0x060082A4 RID: 33444 RVA: 0x00266030 File Offset: 0x00266030
		public ImmutableList<T> Reverse()
		{
			return this.Wrap(this._root.Reverse());
		}

		// Token: 0x060082A5 RID: 33445 RVA: 0x00266044 File Offset: 0x00266044
		public ImmutableList<T> Reverse(int index, int count)
		{
			return this.Wrap(this._root.Reverse(index, count));
		}

		// Token: 0x060082A6 RID: 33446 RVA: 0x0026605C File Offset: 0x0026605C
		public ImmutableList<T> Sort()
		{
			return this.Wrap(this._root.Sort());
		}

		// Token: 0x060082A7 RID: 33447 RVA: 0x00266070 File Offset: 0x00266070
		public ImmutableList<T> Sort(Comparison<T> comparison)
		{
			Requires.NotNull<Comparison<T>>(comparison, "comparison");
			return this.Wrap(this._root.Sort(comparison));
		}

		// Token: 0x060082A8 RID: 33448 RVA: 0x00266090 File Offset: 0x00266090
		public ImmutableList<T> Sort(IComparer<T> comparer)
		{
			return this.Wrap(this._root.Sort(comparer));
		}

		// Token: 0x060082A9 RID: 33449 RVA: 0x002660A4 File Offset: 0x002660A4
		public ImmutableList<T> Sort(int index, int count, IComparer<T> comparer)
		{
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0, "count", null);
			Requires.Range(index + count <= this.Count, "count", null);
			return this.Wrap(this._root.Sort(index, count, comparer));
		}

		// Token: 0x060082AA RID: 33450 RVA: 0x00266108 File Offset: 0x00266108
		public void ForEach(Action<T> action)
		{
			Requires.NotNull<Action<T>>(action, "action");
			foreach (T obj in this)
			{
				action(obj);
			}
		}

		// Token: 0x060082AB RID: 33451 RVA: 0x00266168 File Offset: 0x00266168
		public void CopyTo(T[] array)
		{
			this._root.CopyTo(array);
		}

		// Token: 0x060082AC RID: 33452 RVA: 0x00266178 File Offset: 0x00266178
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x060082AD RID: 33453 RVA: 0x00266188 File Offset: 0x00266188
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			this._root.CopyTo(index, array, arrayIndex, count);
		}

		// Token: 0x060082AE RID: 33454 RVA: 0x0026619C File Offset: 0x0026619C
		public ImmutableList<T> GetRange(int index, int count)
		{
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0, "count", null);
			Requires.Range(index + count <= this.Count, "count", null);
			return this.Wrap(ImmutableList<T>.Node.NodeTreeFromList(this, index, count));
		}

		// Token: 0x060082AF RID: 33455 RVA: 0x002661F8 File Offset: 0x002661F8
		public ImmutableList<TOutput> ConvertAll<TOutput>(Func<T, TOutput> converter)
		{
			Requires.NotNull<Func<T, TOutput>>(converter, "converter");
			return ImmutableList<TOutput>.WrapNode(this._root.ConvertAll<TOutput>(converter));
		}

		// Token: 0x060082B0 RID: 33456 RVA: 0x00266218 File Offset: 0x00266218
		public bool Exists(Predicate<T> match)
		{
			return this._root.Exists(match);
		}

		// Token: 0x060082B1 RID: 33457 RVA: 0x00266228 File Offset: 0x00266228
		public T Find(Predicate<T> match)
		{
			return this._root.Find(match);
		}

		// Token: 0x060082B2 RID: 33458 RVA: 0x00266238 File Offset: 0x00266238
		public ImmutableList<T> FindAll(Predicate<T> match)
		{
			return this._root.FindAll(match);
		}

		// Token: 0x060082B3 RID: 33459 RVA: 0x00266248 File Offset: 0x00266248
		public int FindIndex(Predicate<T> match)
		{
			return this._root.FindIndex(match);
		}

		// Token: 0x060082B4 RID: 33460 RVA: 0x00266258 File Offset: 0x00266258
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return this._root.FindIndex(startIndex, match);
		}

		// Token: 0x060082B5 RID: 33461 RVA: 0x00266268 File Offset: 0x00266268
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			return this._root.FindIndex(startIndex, count, match);
		}

		// Token: 0x060082B6 RID: 33462 RVA: 0x00266278 File Offset: 0x00266278
		public T FindLast(Predicate<T> match)
		{
			return this._root.FindLast(match);
		}

		// Token: 0x060082B7 RID: 33463 RVA: 0x00266288 File Offset: 0x00266288
		public int FindLastIndex(Predicate<T> match)
		{
			return this._root.FindLastIndex(match);
		}

		// Token: 0x060082B8 RID: 33464 RVA: 0x00266298 File Offset: 0x00266298
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			return this._root.FindLastIndex(startIndex, match);
		}

		// Token: 0x060082B9 RID: 33465 RVA: 0x002662A8 File Offset: 0x002662A8
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			return this._root.FindLastIndex(startIndex, count, match);
		}

		// Token: 0x060082BA RID: 33466 RVA: 0x002662B8 File Offset: 0x002662B8
		public int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
		{
			return this._root.IndexOf(item, index, count, equalityComparer);
		}

		// Token: 0x060082BB RID: 33467 RVA: 0x002662CC File Offset: 0x002662CC
		public int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
		{
			return this._root.LastIndexOf(item, index, count, equalityComparer);
		}

		// Token: 0x060082BC RID: 33468 RVA: 0x002662E0 File Offset: 0x002662E0
		public bool TrueForAll(Predicate<T> match)
		{
			return this._root.TrueForAll(match);
		}

		// Token: 0x060082BD RID: 33469 RVA: 0x002662F0 File Offset: 0x002662F0
		public bool Contains(T value)
		{
			return this.IndexOf(value) >= 0;
		}

		// Token: 0x060082BE RID: 33470 RVA: 0x00266300 File Offset: 0x00266300
		public int IndexOf(T value)
		{
			return this.IndexOf(value, EqualityComparer<T>.Default);
		}

		// Token: 0x060082BF RID: 33471 RVA: 0x00266310 File Offset: 0x00266310
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Add(T value)
		{
			return this.Add(value);
		}

		// Token: 0x060082C0 RID: 33472 RVA: 0x0026631C File Offset: 0x0026631C
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.AddRange(IEnumerable<T> items)
		{
			return this.AddRange(items);
		}

		// Token: 0x060082C1 RID: 33473 RVA: 0x00266328 File Offset: 0x00266328
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Insert(int index, T item)
		{
			return this.Insert(index, item);
		}

		// Token: 0x060082C2 RID: 33474 RVA: 0x00266334 File Offset: 0x00266334
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.InsertRange(int index, IEnumerable<T> items)
		{
			return this.InsertRange(index, items);
		}

		// Token: 0x060082C3 RID: 33475 RVA: 0x00266340 File Offset: 0x00266340
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			return this.Remove(value, equalityComparer);
		}

		// Token: 0x060082C4 RID: 33476 RVA: 0x0026634C File Offset: 0x0026634C
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveAll(Predicate<T> match)
		{
			return this.RemoveAll(match);
		}

		// Token: 0x060082C5 RID: 33477 RVA: 0x00266358 File Offset: 0x00266358
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			return this.RemoveRange(items, equalityComparer);
		}

		// Token: 0x060082C6 RID: 33478 RVA: 0x00266364 File Offset: 0x00266364
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveRange(int index, int count)
		{
			return this.RemoveRange(index, count);
		}

		// Token: 0x060082C7 RID: 33479 RVA: 0x00266370 File Offset: 0x00266370
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveAt(int index)
		{
			return this.RemoveAt(index);
		}

		// Token: 0x060082C8 RID: 33480 RVA: 0x0026637C File Offset: 0x0026637C
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.SetItem(int index, T value)
		{
			return this.SetItem(index, value);
		}

		// Token: 0x060082C9 RID: 33481 RVA: 0x00266388 File Offset: 0x00266388
		IImmutableList<T> IImmutableList<!0>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			return this.Replace(oldValue, newValue, equalityComparer);
		}

		// Token: 0x060082CA RID: 33482 RVA: 0x00266394 File Offset: 0x00266394
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x060082CB RID: 33483 RVA: 0x002663C8 File Offset: 0x002663C8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060082CC RID: 33484 RVA: 0x002663D8 File Offset: 0x002663D8
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060082CD RID: 33485 RVA: 0x002663E0 File Offset: 0x002663E0
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C2D RID: 7213
		T IList<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060082D0 RID: 33488 RVA: 0x002663FC File Offset: 0x002663FC
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060082D1 RID: 33489 RVA: 0x00266404 File Offset: 0x00266404
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x060082D2 RID: 33490 RVA: 0x0026640C File Offset: 0x0026640C
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060082D3 RID: 33491 RVA: 0x00266410 File Offset: 0x00266410
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060082D4 RID: 33492 RVA: 0x00266418 File Offset: 0x00266418
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x060082D5 RID: 33493 RVA: 0x00266428 File Offset: 0x00266428
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060082D6 RID: 33494 RVA: 0x00266430 File Offset: 0x00266430
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060082D7 RID: 33495 RVA: 0x00266438 File Offset: 0x00266438
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060082D8 RID: 33496 RVA: 0x00266440 File Offset: 0x00266440
		bool IList.Contains(object value)
		{
			return ImmutableList<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x060082D9 RID: 33497 RVA: 0x0026645C File Offset: 0x0026645C
		int IList.IndexOf(object value)
		{
			if (!ImmutableList<T>.IsCompatibleObject(value))
			{
				return -1;
			}
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x060082DA RID: 33498 RVA: 0x00266478 File Offset: 0x00266478
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x060082DB RID: 33499 RVA: 0x00266480 File Offset: 0x00266480
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x060082DC RID: 33500 RVA: 0x00266484 File Offset: 0x00266484
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060082DD RID: 33501 RVA: 0x00266488 File Offset: 0x00266488
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C31 RID: 7217
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060082E0 RID: 33504 RVA: 0x002664A8 File Offset: 0x002664A8
		public ImmutableList<T>.Enumerator GetEnumerator()
		{
			return new ImmutableList<T>.Enumerator(this._root, null, -1, -1, false);
		}

		// Token: 0x17001C32 RID: 7218
		// (get) Token: 0x060082E1 RID: 33505 RVA: 0x002664BC File Offset: 0x002664BC
		internal ImmutableList<T>.Node Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x060082E2 RID: 33506 RVA: 0x002664C4 File Offset: 0x002664C4
		private static ImmutableList<T> WrapNode(ImmutableList<T>.Node root)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableList<T>(root);
			}
			return ImmutableList<T>.Empty;
		}

		// Token: 0x060082E3 RID: 33507 RVA: 0x002664E0 File Offset: 0x002664E0
		private static bool TryCastToImmutableList(IEnumerable<T> sequence, out ImmutableList<T> other)
		{
			other = (sequence as ImmutableList<T>);
			if (other != null)
			{
				return true;
			}
			ImmutableList<T>.Builder builder = sequence as ImmutableList<T>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x060082E4 RID: 33508 RVA: 0x0026651C File Offset: 0x0026651C
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x060082E5 RID: 33509 RVA: 0x00266554 File Offset: 0x00266554
		private ImmutableList<T> Wrap(ImmutableList<T>.Node root)
		{
			if (root == this._root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableList<T>(root);
			}
			return this.Clear();
		}

		// Token: 0x060082E6 RID: 33510 RVA: 0x0026657C File Offset: 0x0026657C
		private static ImmutableList<T> CreateRange(IEnumerable<T> items)
		{
			ImmutableList<T> result;
			if (ImmutableList<T>.TryCastToImmutableList(items, out result))
			{
				return result;
			}
			IOrderedCollection<T> orderedCollection = items.AsOrderedCollection<T>();
			if (orderedCollection.Count == 0)
			{
				return ImmutableList<T>.Empty;
			}
			ImmutableList<T>.Node root = ImmutableList<T>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			return new ImmutableList<T>(root);
		}

		// Token: 0x04003D37 RID: 15671
		public static readonly ImmutableList<T> Empty = new ImmutableList<T>();

		// Token: 0x04003D38 RID: 15672
		private readonly ImmutableList<T>.Node _root;

		// Token: 0x020011AC RID: 4524
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableListBuilderDebuggerProxy<>))]
		public sealed class Builder : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable, IList, ICollection, IOrderedCollection<!0>, IImmutableListQueries<T>, IReadOnlyList<T>, IReadOnlyCollection<T>
		{
			// Token: 0x060094D1 RID: 38097 RVA: 0x002C6AE4 File Offset: 0x002C6AE4
			internal Builder(ImmutableList<T> list)
			{
				Requires.NotNull<ImmutableList<T>>(list, "list");
				this._root = list._root;
				this._immutable = list;
			}

			// Token: 0x17001EE0 RID: 7904
			// (get) Token: 0x060094D2 RID: 38098 RVA: 0x002C6B18 File Offset: 0x002C6B18
			public int Count
			{
				get
				{
					return this.Root.Count;
				}
			}

			// Token: 0x17001EE1 RID: 7905
			// (get) Token: 0x060094D3 RID: 38099 RVA: 0x002C6B28 File Offset: 0x002C6B28
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001EE2 RID: 7906
			// (get) Token: 0x060094D4 RID: 38100 RVA: 0x002C6B2C File Offset: 0x002C6B2C
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17001EE3 RID: 7907
			// (get) Token: 0x060094D5 RID: 38101 RVA: 0x002C6B34 File Offset: 0x002C6B34
			// (set) Token: 0x060094D6 RID: 38102 RVA: 0x002C6B3C File Offset: 0x002C6B3C
			internal ImmutableList<T>.Node Root
			{
				get
				{
					return this._root;
				}
				private set
				{
					this._version++;
					if (this._root != value)
					{
						this._root = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x17001EE4 RID: 7908
			public unsafe T this[int index]
			{
				get
				{
					return *this.Root.ItemRef(index);
				}
				set
				{
					this.Root = this.Root.ReplaceAt(index, value);
				}
			}

			// Token: 0x17001EE5 RID: 7909
			T IOrderedCollection<!0>.this[int index]
			{
				get
				{
					return this[index];
				}
			}

			// Token: 0x060094DA RID: 38106 RVA: 0x002C6BA0 File Offset: 0x002C6BA0
			[return: System.Collections.Immutable.IsReadOnly]
			public ref T ItemRef(int index)
			{
				return this.Root.ItemRef(index);
			}

			// Token: 0x060094DB RID: 38107 RVA: 0x002C6BB0 File Offset: 0x002C6BB0
			public int IndexOf(T item)
			{
				return this.Root.IndexOf(item, EqualityComparer<T>.Default);
			}

			// Token: 0x060094DC RID: 38108 RVA: 0x002C6BC4 File Offset: 0x002C6BC4
			public void Insert(int index, T item)
			{
				this.Root = this.Root.Insert(index, item);
			}

			// Token: 0x060094DD RID: 38109 RVA: 0x002C6BDC File Offset: 0x002C6BDC
			public void RemoveAt(int index)
			{
				this.Root = this.Root.RemoveAt(index);
			}

			// Token: 0x060094DE RID: 38110 RVA: 0x002C6BF0 File Offset: 0x002C6BF0
			public void Add(T item)
			{
				this.Root = this.Root.Add(item);
			}

			// Token: 0x060094DF RID: 38111 RVA: 0x002C6C04 File Offset: 0x002C6C04
			public void Clear()
			{
				this.Root = ImmutableList<T>.Node.EmptyNode;
			}

			// Token: 0x060094E0 RID: 38112 RVA: 0x002C6C14 File Offset: 0x002C6C14
			public bool Contains(T item)
			{
				return this.IndexOf(item) >= 0;
			}

			// Token: 0x060094E1 RID: 38113 RVA: 0x002C6C24 File Offset: 0x002C6C24
			public bool Remove(T item)
			{
				int num = this.IndexOf(item);
				if (num < 0)
				{
					return false;
				}
				this.Root = this.Root.RemoveAt(num);
				return true;
			}

			// Token: 0x060094E2 RID: 38114 RVA: 0x002C6C5C File Offset: 0x002C6C5C
			public ImmutableList<T>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x060094E3 RID: 38115 RVA: 0x002C6C6C File Offset: 0x002C6C6C
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060094E4 RID: 38116 RVA: 0x002C6C7C File Offset: 0x002C6C7C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060094E5 RID: 38117 RVA: 0x002C6C8C File Offset: 0x002C6C8C
			public void ForEach(Action<T> action)
			{
				Requires.NotNull<Action<T>>(action, "action");
				foreach (T obj in this)
				{
					action(obj);
				}
			}

			// Token: 0x060094E6 RID: 38118 RVA: 0x002C6CEC File Offset: 0x002C6CEC
			public void CopyTo(T[] array)
			{
				this._root.CopyTo(array);
			}

			// Token: 0x060094E7 RID: 38119 RVA: 0x002C6CFC File Offset: 0x002C6CFC
			public void CopyTo(T[] array, int arrayIndex)
			{
				this._root.CopyTo(array, arrayIndex);
			}

			// Token: 0x060094E8 RID: 38120 RVA: 0x002C6D0C File Offset: 0x002C6D0C
			public void CopyTo(int index, T[] array, int arrayIndex, int count)
			{
				this._root.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x060094E9 RID: 38121 RVA: 0x002C6D20 File Offset: 0x002C6D20
			public ImmutableList<T> GetRange(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				return ImmutableList<T>.WrapNode(ImmutableList<T>.Node.NodeTreeFromList(this, index, count));
			}

			// Token: 0x060094EA RID: 38122 RVA: 0x002C6D7C File Offset: 0x002C6D7C
			public ImmutableList<TOutput> ConvertAll<TOutput>(Func<T, TOutput> converter)
			{
				Requires.NotNull<Func<T, TOutput>>(converter, "converter");
				return ImmutableList<TOutput>.WrapNode(this._root.ConvertAll<TOutput>(converter));
			}

			// Token: 0x060094EB RID: 38123 RVA: 0x002C6D9C File Offset: 0x002C6D9C
			public bool Exists(Predicate<T> match)
			{
				return this._root.Exists(match);
			}

			// Token: 0x060094EC RID: 38124 RVA: 0x002C6DAC File Offset: 0x002C6DAC
			public T Find(Predicate<T> match)
			{
				return this._root.Find(match);
			}

			// Token: 0x060094ED RID: 38125 RVA: 0x002C6DBC File Offset: 0x002C6DBC
			public ImmutableList<T> FindAll(Predicate<T> match)
			{
				return this._root.FindAll(match);
			}

			// Token: 0x060094EE RID: 38126 RVA: 0x002C6DCC File Offset: 0x002C6DCC
			public int FindIndex(Predicate<T> match)
			{
				return this._root.FindIndex(match);
			}

			// Token: 0x060094EF RID: 38127 RVA: 0x002C6DDC File Offset: 0x002C6DDC
			public int FindIndex(int startIndex, Predicate<T> match)
			{
				return this._root.FindIndex(startIndex, match);
			}

			// Token: 0x060094F0 RID: 38128 RVA: 0x002C6DEC File Offset: 0x002C6DEC
			public int FindIndex(int startIndex, int count, Predicate<T> match)
			{
				return this._root.FindIndex(startIndex, count, match);
			}

			// Token: 0x060094F1 RID: 38129 RVA: 0x002C6DFC File Offset: 0x002C6DFC
			public T FindLast(Predicate<T> match)
			{
				return this._root.FindLast(match);
			}

			// Token: 0x060094F2 RID: 38130 RVA: 0x002C6E0C File Offset: 0x002C6E0C
			public int FindLastIndex(Predicate<T> match)
			{
				return this._root.FindLastIndex(match);
			}

			// Token: 0x060094F3 RID: 38131 RVA: 0x002C6E1C File Offset: 0x002C6E1C
			public int FindLastIndex(int startIndex, Predicate<T> match)
			{
				return this._root.FindLastIndex(startIndex, match);
			}

			// Token: 0x060094F4 RID: 38132 RVA: 0x002C6E2C File Offset: 0x002C6E2C
			public int FindLastIndex(int startIndex, int count, Predicate<T> match)
			{
				return this._root.FindLastIndex(startIndex, count, match);
			}

			// Token: 0x060094F5 RID: 38133 RVA: 0x002C6E3C File Offset: 0x002C6E3C
			public int IndexOf(T item, int index)
			{
				return this._root.IndexOf(item, index, this.Count - index, EqualityComparer<T>.Default);
			}

			// Token: 0x060094F6 RID: 38134 RVA: 0x002C6E58 File Offset: 0x002C6E58
			public int IndexOf(T item, int index, int count)
			{
				return this._root.IndexOf(item, index, count, EqualityComparer<T>.Default);
			}

			// Token: 0x060094F7 RID: 38135 RVA: 0x002C6E70 File Offset: 0x002C6E70
			public int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
			{
				return this._root.IndexOf(item, index, count, equalityComparer);
			}

			// Token: 0x060094F8 RID: 38136 RVA: 0x002C6E84 File Offset: 0x002C6E84
			public int LastIndexOf(T item)
			{
				if (this.Count == 0)
				{
					return -1;
				}
				return this._root.LastIndexOf(item, this.Count - 1, this.Count, EqualityComparer<T>.Default);
			}

			// Token: 0x060094F9 RID: 38137 RVA: 0x002C6EC4 File Offset: 0x002C6EC4
			public int LastIndexOf(T item, int startIndex)
			{
				if (this.Count == 0 && startIndex == 0)
				{
					return -1;
				}
				return this._root.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
			}

			// Token: 0x060094FA RID: 38138 RVA: 0x002C6EF0 File Offset: 0x002C6EF0
			public int LastIndexOf(T item, int startIndex, int count)
			{
				return this._root.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x060094FB RID: 38139 RVA: 0x002C6F08 File Offset: 0x002C6F08
			public int LastIndexOf(T item, int startIndex, int count, IEqualityComparer<T> equalityComparer)
			{
				return this._root.LastIndexOf(item, startIndex, count, equalityComparer);
			}

			// Token: 0x060094FC RID: 38140 RVA: 0x002C6F1C File Offset: 0x002C6F1C
			public bool TrueForAll(Predicate<T> match)
			{
				return this._root.TrueForAll(match);
			}

			// Token: 0x060094FD RID: 38141 RVA: 0x002C6F2C File Offset: 0x002C6F2C
			public void AddRange(IEnumerable<T> items)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				this.Root = this.Root.AddRange(items);
			}

			// Token: 0x060094FE RID: 38142 RVA: 0x002C6F4C File Offset: 0x002C6F4C
			public void InsertRange(int index, IEnumerable<T> items)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				Requires.NotNull<IEnumerable<T>>(items, "items");
				this.Root = this.Root.InsertRange(index, items);
			}

			// Token: 0x060094FF RID: 38143 RVA: 0x002C6FA0 File Offset: 0x002C6FA0
			public int RemoveAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				int count = this.Count;
				this.Root = this.Root.RemoveAll(match);
				return count - this.Count;
			}

			// Token: 0x06009500 RID: 38144 RVA: 0x002C6FE0 File Offset: 0x002C6FE0
			public void Reverse()
			{
				this.Reverse(0, this.Count);
			}

			// Token: 0x06009501 RID: 38145 RVA: 0x002C6FF0 File Offset: 0x002C6FF0
			public void Reverse(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				this.Root = this.Root.Reverse(index, count);
			}

			// Token: 0x06009502 RID: 38146 RVA: 0x002C7054 File Offset: 0x002C7054
			public void Sort()
			{
				this.Root = this.Root.Sort();
			}

			// Token: 0x06009503 RID: 38147 RVA: 0x002C7068 File Offset: 0x002C7068
			public void Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				this.Root = this.Root.Sort(comparison);
			}

			// Token: 0x06009504 RID: 38148 RVA: 0x002C7088 File Offset: 0x002C7088
			public void Sort(IComparer<T> comparer)
			{
				this.Root = this.Root.Sort(comparer);
			}

			// Token: 0x06009505 RID: 38149 RVA: 0x002C709C File Offset: 0x002C709C
			public void Sort(int index, int count, IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				this.Root = this.Root.Sort(index, count, comparer);
			}

			// Token: 0x06009506 RID: 38150 RVA: 0x002C7100 File Offset: 0x002C7100
			public int BinarySearch(T item)
			{
				return this.BinarySearch(item, null);
			}

			// Token: 0x06009507 RID: 38151 RVA: 0x002C710C File Offset: 0x002C710C
			public int BinarySearch(T item, IComparer<T> comparer)
			{
				return this.BinarySearch(0, this.Count, item, comparer);
			}

			// Token: 0x06009508 RID: 38152 RVA: 0x002C7120 File Offset: 0x002C7120
			public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
			{
				return this.Root.BinarySearch(index, count, item, comparer);
			}

			// Token: 0x06009509 RID: 38153 RVA: 0x002C7134 File Offset: 0x002C7134
			public ImmutableList<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableList<T>.WrapNode(this.Root);
				}
				return this._immutable;
			}

			// Token: 0x0600950A RID: 38154 RVA: 0x002C7158 File Offset: 0x002C7158
			int IList.Add(object value)
			{
				this.Add((T)((object)value));
				return this.Count - 1;
			}

			// Token: 0x0600950B RID: 38155 RVA: 0x002C7170 File Offset: 0x002C7170
			void IList.Clear()
			{
				this.Clear();
			}

			// Token: 0x0600950C RID: 38156 RVA: 0x002C7178 File Offset: 0x002C7178
			bool IList.Contains(object value)
			{
				return ImmutableList<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
			}

			// Token: 0x0600950D RID: 38157 RVA: 0x002C7194 File Offset: 0x002C7194
			int IList.IndexOf(object value)
			{
				if (ImmutableList<T>.IsCompatibleObject(value))
				{
					return this.IndexOf((T)((object)value));
				}
				return -1;
			}

			// Token: 0x0600950E RID: 38158 RVA: 0x002C71B0 File Offset: 0x002C71B0
			void IList.Insert(int index, object value)
			{
				this.Insert(index, (T)((object)value));
			}

			// Token: 0x17001EE6 RID: 7910
			// (get) Token: 0x0600950F RID: 38159 RVA: 0x002C71C0 File Offset: 0x002C71C0
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001EE7 RID: 7911
			// (get) Token: 0x06009510 RID: 38160 RVA: 0x002C71C4 File Offset: 0x002C71C4
			bool IList.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06009511 RID: 38161 RVA: 0x002C71C8 File Offset: 0x002C71C8
			void IList.Remove(object value)
			{
				if (ImmutableList<T>.IsCompatibleObject(value))
				{
					this.Remove((T)((object)value));
				}
			}

			// Token: 0x17001EE8 RID: 7912
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					this[index] = (T)((object)value);
				}
			}

			// Token: 0x06009514 RID: 38164 RVA: 0x002C7204 File Offset: 0x002C7204
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex);
			}

			// Token: 0x17001EE9 RID: 7913
			// (get) Token: 0x06009515 RID: 38165 RVA: 0x002C7214 File Offset: 0x002C7214
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001EEA RID: 7914
			// (get) Token: 0x06009516 RID: 38166 RVA: 0x002C7218 File Offset: 0x002C7218
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
					return this._syncRoot;
				}
			}

			// Token: 0x04004C05 RID: 19461
			private ImmutableList<T>.Node _root = ImmutableList<T>.Node.EmptyNode;

			// Token: 0x04004C06 RID: 19462
			private ImmutableList<T> _immutable;

			// Token: 0x04004C07 RID: 19463
			private int _version;

			// Token: 0x04004C08 RID: 19464
			private object _syncRoot;
		}

		// Token: 0x020011AD RID: 4525
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator, ISecurePooledObjectUser, IStrongEnumerator<T>
		{
			// Token: 0x06009517 RID: 38167 RVA: 0x002C7240 File Offset: 0x002C7240
			internal Enumerator(ImmutableList<T>.Node root, ImmutableList<T>.Builder builder = null, int startIndex = -1, int count = -1, bool reversed = false)
			{
				Requires.NotNull<ImmutableList<T>.Node>(root, "root");
				Requires.Range(startIndex >= -1, "startIndex", null);
				Requires.Range(count >= -1, "count", null);
				Requires.Argument(reversed || count == -1 || ((startIndex == -1) ? 0 : startIndex) + count <= root.Count);
				Requires.Argument(!reversed || count == -1 || ((startIndex == -1) ? (root.Count - 1) : startIndex) - count + 1 >= 0);
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._startIndex = ((startIndex >= 0) ? startIndex : (reversed ? (root.Count - 1) : 0));
				this._count = ((count == -1) ? root.Count : count);
				this._remainingCount = this._count;
				this._reversed = reversed;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (this._count > 0)
				{
					if (!ImmutableList<T>.Enumerator.s_EnumeratingStacks.TryTake(this, out this._stack))
					{
						this._stack = ImmutableList<T>.Enumerator.s_EnumeratingStacks.PrepNew(this, new Stack<RefAsValueType<ImmutableList<T>.Node>>(root.Height));
					}
					this.ResetStack();
				}
			}

			// Token: 0x17001EEB RID: 7915
			// (get) Token: 0x06009518 RID: 38168 RVA: 0x002C73D8 File Offset: 0x002C73D8
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17001EEC RID: 7916
			// (get) Token: 0x06009519 RID: 38169 RVA: 0x002C73E0 File Offset: 0x002C73E0
			public T Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._current != null)
					{
						return this._current.Value;
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17001EED RID: 7917
			// (get) Token: 0x0600951A RID: 38170 RVA: 0x002C7404 File Offset: 0x002C7404
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600951B RID: 38171 RVA: 0x002C7414 File Offset: 0x002C7414
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableList<T>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableList<T>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableList<T>.Node>>();
					ImmutableList<T>.Enumerator.s_EnumeratingStacks.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x0600951C RID: 38172 RVA: 0x002C7474 File Offset: 0x002C7474
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
					if (this._remainingCount > 0 && stack.Count > 0)
					{
						ImmutableList<T>.Node value = stack.Pop().Value;
						this._current = value;
						this.PushNext(this.NextBranch(value));
						this._remainingCount--;
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x0600951D RID: 38173 RVA: 0x002C74FC File Offset: 0x002C74FC
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._remainingCount = this._count;
				if (this._stack != null)
				{
					this.ResetStack();
				}
			}

			// Token: 0x0600951E RID: 38174 RVA: 0x002C7554 File Offset: 0x002C7554
			private void ResetStack()
			{
				Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
				stack.ClearFastWhenEmpty<RefAsValueType<ImmutableList<T>.Node>>();
				ImmutableList<T>.Node node = this._root;
				int num = this._reversed ? (this._root.Count - this._startIndex - 1) : this._startIndex;
				while (!node.IsEmpty && num != this.PreviousBranch(node).Count)
				{
					if (num < this.PreviousBranch(node).Count)
					{
						stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
						node = this.PreviousBranch(node);
					}
					else
					{
						num -= this.PreviousBranch(node).Count + 1;
						node = this.NextBranch(node);
					}
				}
				if (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
				}
			}

			// Token: 0x0600951F RID: 38175 RVA: 0x002C7624 File Offset: 0x002C7624
			private ImmutableList<T>.Node NextBranch(ImmutableList<T>.Node node)
			{
				if (!this._reversed)
				{
					return node.Right;
				}
				return node.Left;
			}

			// Token: 0x06009520 RID: 38176 RVA: 0x002C7640 File Offset: 0x002C7640
			private ImmutableList<T>.Node PreviousBranch(ImmutableList<T>.Node node)
			{
				if (!this._reversed)
				{
					return node.Left;
				}
				return node.Right;
			}

			// Token: 0x06009521 RID: 38177 RVA: 0x002C765C File Offset: 0x002C765C
			private void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableList<T>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableList<T>.Enumerator>(this);
				}
			}

			// Token: 0x06009522 RID: 38178 RVA: 0x002C7690 File Offset: 0x002C7690
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(System.Collections.Immutable2448884.SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x06009523 RID: 38179 RVA: 0x002C76C0 File Offset: 0x002C76C0
			private void PushNext(ImmutableList<T>.Node node)
			{
				Requires.NotNull<ImmutableList<T>.Node>(node, "node");
				if (!node.IsEmpty)
				{
					Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
					while (!node.IsEmpty)
					{
						stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
						node = this.PreviousBranch(node);
					}
				}
			}

			// Token: 0x04004C09 RID: 19465
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableList<T>.Node>>, ImmutableList<T>.Enumerator> s_EnumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableList<T>.Node>>, ImmutableList<T>.Enumerator>();

			// Token: 0x04004C0A RID: 19466
			private readonly ImmutableList<T>.Builder _builder;

			// Token: 0x04004C0B RID: 19467
			private readonly int _poolUserId;

			// Token: 0x04004C0C RID: 19468
			private readonly int _startIndex;

			// Token: 0x04004C0D RID: 19469
			private readonly int _count;

			// Token: 0x04004C0E RID: 19470
			private int _remainingCount;

			// Token: 0x04004C0F RID: 19471
			private bool _reversed;

			// Token: 0x04004C10 RID: 19472
			private ImmutableList<T>.Node _root;

			// Token: 0x04004C11 RID: 19473
			private SecurePooledObject<Stack<RefAsValueType<ImmutableList<T>.Node>>> _stack;

			// Token: 0x04004C12 RID: 19474
			private ImmutableList<T>.Node _current;

			// Token: 0x04004C13 RID: 19475
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x020011AE RID: 4526
		[DebuggerDisplay("{_key}")]
		internal sealed class Node : IBinaryTree<T>, IBinaryTree, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06009525 RID: 38181 RVA: 0x002C7724 File Offset: 0x002C7724
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x06009526 RID: 38182 RVA: 0x002C7734 File Offset: 0x002C7734
			private Node(T key, ImmutableList<T>.Node left, ImmutableList<T>.Node right, bool frozen = false)
			{
				Requires.NotNull<ImmutableList<T>.Node>(left, "left");
				Requires.NotNull<ImmutableList<T>.Node>(right, "right");
				this._key = key;
				this._left = left;
				this._right = right;
				this._height = ImmutableList<T>.Node.ParentHeight(left, right);
				this._count = ImmutableList<T>.Node.ParentCount(left, right);
				this._frozen = frozen;
			}

			// Token: 0x17001EEE RID: 7918
			// (get) Token: 0x06009527 RID: 38183 RVA: 0x002C7798 File Offset: 0x002C7798
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17001EEF RID: 7919
			// (get) Token: 0x06009528 RID: 38184 RVA: 0x002C77A4 File Offset: 0x002C77A4
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17001EF0 RID: 7920
			// (get) Token: 0x06009529 RID: 38185 RVA: 0x002C77AC File Offset: 0x002C77AC
			public ImmutableList<T>.Node Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17001EF1 RID: 7921
			// (get) Token: 0x0600952A RID: 38186 RVA: 0x002C77B4 File Offset: 0x002C77B4
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17001EF2 RID: 7922
			// (get) Token: 0x0600952B RID: 38187 RVA: 0x002C77BC File Offset: 0x002C77BC
			public ImmutableList<T>.Node Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17001EF3 RID: 7923
			// (get) Token: 0x0600952C RID: 38188 RVA: 0x002C77C4 File Offset: 0x002C77C4
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17001EF4 RID: 7924
			// (get) Token: 0x0600952D RID: 38189 RVA: 0x002C77CC File Offset: 0x002C77CC
			IBinaryTree<T> IBinaryTree<!0>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17001EF5 RID: 7925
			// (get) Token: 0x0600952E RID: 38190 RVA: 0x002C77D4 File Offset: 0x002C77D4
			IBinaryTree<T> IBinaryTree<!0>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17001EF6 RID: 7926
			// (get) Token: 0x0600952F RID: 38191 RVA: 0x002C77DC File Offset: 0x002C77DC
			public T Value
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17001EF7 RID: 7927
			// (get) Token: 0x06009530 RID: 38192 RVA: 0x002C77E4 File Offset: 0x002C77E4
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17001EF8 RID: 7928
			// (get) Token: 0x06009531 RID: 38193 RVA: 0x002C77EC File Offset: 0x002C77EC
			internal T Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17001EF9 RID: 7929
			internal T this[int index]
			{
				get
				{
					Requires.Range(index >= 0 && index < this.Count, "index", null);
					if (index < this._left._count)
					{
						return this._left[index];
					}
					if (index > this._left._count)
					{
						return this._right[index - this._left._count - 1];
					}
					return this._key;
				}
			}

			// Token: 0x06009533 RID: 38195 RVA: 0x002C7878 File Offset: 0x002C7878
			[return: System.Collections.Immutable.IsReadOnly]
			internal ref T ItemRef(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				if (index < this._left._count)
				{
					return this._left.ItemRef(index);
				}
				if (index > this._left._count)
				{
					return this._right.ItemRef(index - this._left._count - 1);
				}
				return ref this._key;
			}

			// Token: 0x06009534 RID: 38196 RVA: 0x002C78FC File Offset: 0x002C78FC
			public ImmutableList<T>.Enumerator GetEnumerator()
			{
				return new ImmutableList<T>.Enumerator(this, null, -1, -1, false);
			}

			// Token: 0x06009535 RID: 38197 RVA: 0x002C7908 File Offset: 0x002C7908
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009536 RID: 38198 RVA: 0x002C7918 File Offset: 0x002C7918
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009537 RID: 38199 RVA: 0x002C7928 File Offset: 0x002C7928
			internal ImmutableList<T>.Enumerator GetEnumerator(ImmutableList<T>.Builder builder)
			{
				return new ImmutableList<T>.Enumerator(this, builder, -1, -1, false);
			}

			// Token: 0x06009538 RID: 38200 RVA: 0x002C7934 File Offset: 0x002C7934
			internal static ImmutableList<T>.Node NodeTreeFromList(IOrderedCollection<T> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<T>>(items, "items");
				Requires.Range(start >= 0, "start", null);
				Requires.Range(length >= 0, "length", null);
				if (length == 0)
				{
					return ImmutableList<T>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableList<T>.Node left = ImmutableList<T>.Node.NodeTreeFromList(items, start, num2);
				ImmutableList<T>.Node right = ImmutableList<T>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				return new ImmutableList<T>.Node(items[start + num2], left, right, true);
			}

			// Token: 0x06009539 RID: 38201 RVA: 0x002C79B4 File Offset: 0x002C79B4
			internal ImmutableList<T>.Node Add(T key)
			{
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateLeaf(key);
				}
				ImmutableList<T>.Node right = this._right.Add(key);
				ImmutableList<T>.Node node = this.MutateRight(right);
				if (!node.IsBalanced)
				{
					return node.BalanceRight();
				}
				return node;
			}

			// Token: 0x0600953A RID: 38202 RVA: 0x002C7A00 File Offset: 0x002C7A00
			internal ImmutableList<T>.Node Insert(int index, T key)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateLeaf(key);
				}
				if (index <= this._left._count)
				{
					ImmutableList<T>.Node left = this._left.Insert(index, key);
					ImmutableList<T>.Node node = this.MutateLeft(left);
					if (!node.IsBalanced)
					{
						return node.BalanceLeft();
					}
					return node;
				}
				else
				{
					ImmutableList<T>.Node right = this._right.Insert(index - this._left._count - 1, key);
					ImmutableList<T>.Node node2 = this.MutateRight(right);
					if (!node2.IsBalanced)
					{
						return node2.BalanceRight();
					}
					return node2;
				}
			}

			// Token: 0x0600953B RID: 38203 RVA: 0x002C7ABC File Offset: 0x002C7ABC
			internal ImmutableList<T>.Node AddRange(IEnumerable<T> keys)
			{
				Requires.NotNull<IEnumerable<T>>(keys, "keys");
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateRange(keys);
				}
				ImmutableList<T>.Node right = this._right.AddRange(keys);
				ImmutableList<T>.Node node = this.MutateRight(right);
				return node.BalanceMany();
			}

			// Token: 0x0600953C RID: 38204 RVA: 0x002C7B08 File Offset: 0x002C7B08
			internal ImmutableList<T>.Node InsertRange(int index, IEnumerable<T> keys)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				Requires.NotNull<IEnumerable<T>>(keys, "keys");
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateRange(keys);
				}
				ImmutableList<T>.Node node;
				if (index <= this._left._count)
				{
					ImmutableList<T>.Node left = this._left.InsertRange(index, keys);
					node = this.MutateLeft(left);
				}
				else
				{
					ImmutableList<T>.Node right = this._right.InsertRange(index - this._left._count - 1, keys);
					node = this.MutateRight(right);
				}
				return node.BalanceMany();
			}

			// Token: 0x0600953D RID: 38205 RVA: 0x002C7BB0 File Offset: 0x002C7BB0
			internal ImmutableList<T>.Node RemoveAt(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				ImmutableList<T>.Node node;
				if (index == this._left._count)
				{
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableList<T>.Node.EmptyNode;
					}
					else if (this._right.IsEmpty && !this._left.IsEmpty)
					{
						node = this._left;
					}
					else if (!this._right.IsEmpty && this._left.IsEmpty)
					{
						node = this._right;
					}
					else
					{
						ImmutableList<T>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						ImmutableList<T>.Node right = this._right.RemoveAt(0);
						node = node2.MutateBoth(this._left, right);
					}
				}
				else if (index < this._left._count)
				{
					ImmutableList<T>.Node left = this._left.RemoveAt(index);
					node = this.MutateLeft(left);
				}
				else
				{
					ImmutableList<T>.Node right2 = this._right.RemoveAt(index - this._left._count - 1);
					node = this.MutateRight(right2);
				}
				if (!node.IsEmpty && !node.IsBalanced)
				{
					return node.Balance();
				}
				return node;
			}

			// Token: 0x0600953E RID: 38206 RVA: 0x002C7D28 File Offset: 0x002C7D28
			internal ImmutableList<T>.Node RemoveAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				ImmutableList<T>.Node node = this;
				ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(node, null, -1, -1, false);
				try
				{
					int num = 0;
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							node = node.RemoveAt(num);
							enumerator.Dispose();
							enumerator = new ImmutableList<T>.Enumerator(node, null, num, -1, false);
						}
						else
						{
							num++;
						}
					}
				}
				finally
				{
					enumerator.Dispose();
				}
				return node;
			}

			// Token: 0x0600953F RID: 38207 RVA: 0x002C7DB4 File Offset: 0x002C7DB4
			internal ImmutableList<T>.Node ReplaceAt(int index, T value)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				ImmutableList<T>.Node result;
				if (index == this._left._count)
				{
					result = this.MutateKey(value);
				}
				else if (index < this._left._count)
				{
					ImmutableList<T>.Node left = this._left.ReplaceAt(index, value);
					result = this.MutateLeft(left);
				}
				else
				{
					ImmutableList<T>.Node right = this._right.ReplaceAt(index - this._left._count - 1, value);
					result = this.MutateRight(right);
				}
				return result;
			}

			// Token: 0x06009540 RID: 38208 RVA: 0x002C7E58 File Offset: 0x002C7E58
			internal ImmutableList<T>.Node Reverse()
			{
				return this.Reverse(0, this.Count);
			}

			// Token: 0x06009541 RID: 38209 RVA: 0x002C7E68 File Offset: 0x002C7E68
			internal unsafe ImmutableList<T>.Node Reverse(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "index", null);
				ImmutableList<T>.Node node = this;
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					T value = *node.ItemRef(i);
					T value2 = *node.ItemRef(num);
					node = node.ReplaceAt(num, value).ReplaceAt(i, value2);
					i++;
					num--;
				}
				return node;
			}

			// Token: 0x06009542 RID: 38210 RVA: 0x002C7F00 File Offset: 0x002C7F00
			internal ImmutableList<T>.Node Sort()
			{
				return this.Sort(Comparer<T>.Default);
			}

			// Token: 0x06009543 RID: 38211 RVA: 0x002C7F10 File Offset: 0x002C7F10
			internal ImmutableList<T>.Node Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				T[] array = new T[this.Count];
				this.CopyTo(array);
				Array.Sort<T>(array, comparison);
				return ImmutableList<T>.Node.NodeTreeFromList(array.AsOrderedCollection<T>(), 0, this.Count);
			}

			// Token: 0x06009544 RID: 38212 RVA: 0x002C7F58 File Offset: 0x002C7F58
			internal ImmutableList<T>.Node Sort(IComparer<T> comparer)
			{
				return this.Sort(0, this.Count, comparer);
			}

			// Token: 0x06009545 RID: 38213 RVA: 0x002C7F68 File Offset: 0x002C7F68
			internal ImmutableList<T>.Node Sort(int index, int count, IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Argument(index + count <= this.Count);
				T[] array = new T[this.Count];
				this.CopyTo(array);
				Array.Sort<T>(array, index, count, comparer);
				return ImmutableList<T>.Node.NodeTreeFromList(array.AsOrderedCollection<T>(), 0, this.Count);
			}

			// Token: 0x06009546 RID: 38214 RVA: 0x002C7FE0 File Offset: 0x002C7FE0
			internal int BinarySearch(int index, int count, T item, IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				comparer = (comparer ?? Comparer<T>.Default);
				if (this.IsEmpty || count <= 0)
				{
					return ~index;
				}
				int count2 = this._left.Count;
				if (index + count <= count2)
				{
					return this._left.BinarySearch(index, count, item, comparer);
				}
				if (index > count2)
				{
					int num = this._right.BinarySearch(index - count2 - 1, count, item, comparer);
					int num2 = count2 + 1;
					if (num >= 0)
					{
						return num + num2;
					}
					return num - num2;
				}
				else
				{
					int num3 = comparer.Compare(item, this._key);
					if (num3 == 0)
					{
						return count2;
					}
					if (num3 > 0)
					{
						int num4 = count - (count2 - index) - 1;
						int num5 = (num4 < 0) ? -1 : this._right.BinarySearch(0, num4, item, comparer);
						int num6 = count2 + 1;
						if (num5 >= 0)
						{
							return num5 + num6;
						}
						return num5 - num6;
					}
					else
					{
						if (index == count2)
						{
							return ~index;
						}
						return this._left.BinarySearch(index, count, item, comparer);
					}
				}
			}

			// Token: 0x06009547 RID: 38215 RVA: 0x002C8110 File Offset: 0x002C8110
			internal int IndexOf(T item, IEqualityComparer<T> equalityComparer)
			{
				return this.IndexOf(item, 0, this.Count, equalityComparer);
			}

			// Token: 0x06009548 RID: 38216 RVA: 0x002C8124 File Offset: 0x002C8124
			internal int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(count <= this.Count, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				equalityComparer = (equalityComparer ?? EqualityComparer<T>.Default);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, false))
				{
					while (enumerator.MoveNext())
					{
						if (equalityComparer.Equals(item, enumerator.Current))
						{
							return index;
						}
						index++;
					}
				}
				return -1;
			}

			// Token: 0x06009549 RID: 38217 RVA: 0x002C81F8 File Offset: 0x002C81F8
			internal int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0 && count <= this.Count, "count", null);
				Requires.Argument(index - count + 1 >= 0);
				equalityComparer = (equalityComparer ?? EqualityComparer<T>.Default);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, true))
				{
					while (enumerator.MoveNext())
					{
						if (equalityComparer.Equals(item, enumerator.Current))
						{
							return index;
						}
						index--;
					}
				}
				return -1;
			}

			// Token: 0x0600954A RID: 38218 RVA: 0x002C82BC File Offset: 0x002C82BC
			internal void CopyTo(T[] array)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(array.Length >= this.Count, "array", null);
				int num = 0;
				foreach (T t in this)
				{
					array[num++] = t;
				}
			}

			// Token: 0x0600954B RID: 38219 RVA: 0x002C833C File Offset: 0x002C833C
			internal void CopyTo(T[] array, int arrayIndex)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array[arrayIndex++] = t;
				}
			}

			// Token: 0x0600954C RID: 38220 RVA: 0x002C83D0 File Offset: 0x002C83D0
			internal void CopyTo(int index, T[] array, int arrayIndex, int count)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(arrayIndex + count <= array.Length, "arrayIndex", null);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, false))
				{
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						array[arrayIndex++] = t;
					}
				}
			}

			// Token: 0x0600954D RID: 38221 RVA: 0x002C84A4 File Offset: 0x002C84A4
			internal void CopyTo(Array array, int arrayIndex)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array.SetValue(t, arrayIndex++);
				}
			}

			// Token: 0x0600954E RID: 38222 RVA: 0x002C8540 File Offset: 0x002C8540
			internal ImmutableList<TOutput>.Node ConvertAll<TOutput>(Func<T, TOutput> converter)
			{
				ImmutableList<TOutput>.Node emptyNode = ImmutableList<TOutput>.Node.EmptyNode;
				if (this.IsEmpty)
				{
					return emptyNode;
				}
				return emptyNode.AddRange(this.Select(converter));
			}

			// Token: 0x0600954F RID: 38223 RVA: 0x002C8574 File Offset: 0x002C8574
			internal bool TrueForAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				foreach (T obj in this)
				{
					if (!match(obj))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06009550 RID: 38224 RVA: 0x002C85E4 File Offset: 0x002C85E4
			internal bool Exists(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				foreach (T obj in this)
				{
					if (match(obj))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06009551 RID: 38225 RVA: 0x002C8654 File Offset: 0x002C8654
			internal T Find(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				foreach (T t in this)
				{
					if (match(t))
					{
						return t;
					}
				}
				return default(T);
			}

			// Token: 0x06009552 RID: 38226 RVA: 0x002C86CC File Offset: 0x002C86CC
			internal ImmutableList<T> FindAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Empty;
				}
				List<T> list = null;
				foreach (T t in this)
				{
					if (match(t))
					{
						if (list == null)
						{
							list = new List<T>();
						}
						list.Add(t);
					}
				}
				if (list == null)
				{
					return ImmutableList<T>.Empty;
				}
				return ImmutableList.CreateRange<T>(list);
			}

			// Token: 0x06009553 RID: 38227 RVA: 0x002C8768 File Offset: 0x002C8768
			internal int FindIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this.FindIndex(0, this._count, match);
			}

			// Token: 0x06009554 RID: 38228 RVA: 0x002C8784 File Offset: 0x002C8784
			internal int FindIndex(int startIndex, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0 && startIndex <= this.Count, "startIndex", null);
				return this.FindIndex(startIndex, this.Count - startIndex, match);
			}

			// Token: 0x06009555 RID: 38229 RVA: 0x002C87D4 File Offset: 0x002C87D4
			internal int FindIndex(int startIndex, int count, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(startIndex + count <= this.Count, "count", null);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, startIndex, count, false))
				{
					int num = startIndex;
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return num;
						}
						num++;
					}
				}
				return -1;
			}

			// Token: 0x06009556 RID: 38230 RVA: 0x002C8888 File Offset: 0x002C8888
			internal T FindLast(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, -1, -1, true))
				{
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
				}
				return default(T);
			}

			// Token: 0x06009557 RID: 38231 RVA: 0x002C8908 File Offset: 0x002C8908
			internal int FindLastIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				if (!this.IsEmpty)
				{
					return this.FindLastIndex(this.Count - 1, this.Count, match);
				}
				return -1;
			}

			// Token: 0x06009558 RID: 38232 RVA: 0x002C8948 File Offset: 0x002C8948
			internal int FindLastIndex(int startIndex, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(startIndex == 0 || startIndex < this.Count, "startIndex", null);
				if (!this.IsEmpty)
				{
					return this.FindLastIndex(startIndex, startIndex + 1, match);
				}
				return -1;
			}

			// Token: 0x06009559 RID: 38233 RVA: 0x002C89B0 File Offset: 0x002C89B0
			internal int FindLastIndex(int startIndex, int count, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(count <= this.Count, "count", null);
				Requires.Range(startIndex - count + 1 >= 0, "startIndex", null);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, startIndex, count, true))
				{
					int num = startIndex;
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return num;
						}
						num--;
					}
				}
				return -1;
			}

			// Token: 0x0600955A RID: 38234 RVA: 0x002C8A68 File Offset: 0x002C8A68
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x0600955B RID: 38235 RVA: 0x002C8A94 File Offset: 0x002C8A94
			private ImmutableList<T>.Node RotateLeft()
			{
				return this._right.MutateLeft(this.MutateRight(this._right._left));
			}

			// Token: 0x0600955C RID: 38236 RVA: 0x002C8AB4 File Offset: 0x002C8AB4
			private ImmutableList<T>.Node RotateRight()
			{
				return this._left.MutateRight(this.MutateLeft(this._left._right));
			}

			// Token: 0x0600955D RID: 38237 RVA: 0x002C8AD4 File Offset: 0x002C8AD4
			private ImmutableList<T>.Node DoubleLeft()
			{
				ImmutableList<T>.Node right = this._right;
				ImmutableList<T>.Node left = right._left;
				return left.MutateBoth(this.MutateRight(left._left), right.MutateLeft(left._right));
			}

			// Token: 0x0600955E RID: 38238 RVA: 0x002C8B14 File Offset: 0x002C8B14
			private ImmutableList<T>.Node DoubleRight()
			{
				ImmutableList<T>.Node left = this._left;
				ImmutableList<T>.Node right = left._right;
				return right.MutateBoth(left.MutateRight(right._left), this.MutateLeft(right._right));
			}

			// Token: 0x17001EFA RID: 7930
			// (get) Token: 0x0600955F RID: 38239 RVA: 0x002C8B54 File Offset: 0x002C8B54
			private int BalanceFactor
			{
				get
				{
					return (int)(this._right._height - this._left._height);
				}
			}

			// Token: 0x17001EFB RID: 7931
			// (get) Token: 0x06009560 RID: 38240 RVA: 0x002C8B70 File Offset: 0x002C8B70
			private bool IsRightHeavy
			{
				get
				{
					return this.BalanceFactor >= 2;
				}
			}

			// Token: 0x17001EFC RID: 7932
			// (get) Token: 0x06009561 RID: 38241 RVA: 0x002C8B80 File Offset: 0x002C8B80
			private bool IsLeftHeavy
			{
				get
				{
					return this.BalanceFactor <= -2;
				}
			}

			// Token: 0x17001EFD RID: 7933
			// (get) Token: 0x06009562 RID: 38242 RVA: 0x002C8B90 File Offset: 0x002C8B90
			private bool IsBalanced
			{
				get
				{
					return this.BalanceFactor + 1 <= 2;
				}
			}

			// Token: 0x06009563 RID: 38243 RVA: 0x002C8BA0 File Offset: 0x002C8BA0
			private ImmutableList<T>.Node Balance()
			{
				if (!this.IsLeftHeavy)
				{
					return this.BalanceRight();
				}
				return this.BalanceLeft();
			}

			// Token: 0x06009564 RID: 38244 RVA: 0x002C8BBC File Offset: 0x002C8BBC
			private ImmutableList<T>.Node BalanceLeft()
			{
				if (this._left.BalanceFactor <= 0)
				{
					return this.RotateRight();
				}
				return this.DoubleRight();
			}

			// Token: 0x06009565 RID: 38245 RVA: 0x002C8BDC File Offset: 0x002C8BDC
			private ImmutableList<T>.Node BalanceRight()
			{
				if (this._right.BalanceFactor >= 0)
				{
					return this.RotateLeft();
				}
				return this.DoubleLeft();
			}

			// Token: 0x06009566 RID: 38246 RVA: 0x002C8BFC File Offset: 0x002C8BFC
			private ImmutableList<T>.Node BalanceMany()
			{
				ImmutableList<T>.Node node = this;
				while (!node.IsBalanced)
				{
					if (node.IsRightHeavy)
					{
						node = node.BalanceRight();
						node.MutateLeft(node._left.BalanceMany());
					}
					else
					{
						node = node.BalanceLeft();
						node.MutateRight(node._right.BalanceMany());
					}
				}
				return node;
			}

			// Token: 0x06009567 RID: 38247 RVA: 0x002C8C60 File Offset: 0x002C8C60
			private ImmutableList<T>.Node MutateBoth(ImmutableList<T>.Node left, ImmutableList<T>.Node right)
			{
				Requires.NotNull<ImmutableList<T>.Node>(left, "left");
				Requires.NotNull<ImmutableList<T>.Node>(right, "right");
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(this._key, left, right, false);
				}
				this._left = left;
				this._right = right;
				this._height = ImmutableList<T>.Node.ParentHeight(left, right);
				this._count = ImmutableList<T>.Node.ParentCount(left, right);
				return this;
			}

			// Token: 0x06009568 RID: 38248 RVA: 0x002C8CCC File Offset: 0x002C8CCC
			private ImmutableList<T>.Node MutateLeft(ImmutableList<T>.Node left)
			{
				Requires.NotNull<ImmutableList<T>.Node>(left, "left");
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(this._key, left, this._right, false);
				}
				this._left = left;
				this._height = ImmutableList<T>.Node.ParentHeight(left, this._right);
				this._count = ImmutableList<T>.Node.ParentCount(left, this._right);
				return this;
			}

			// Token: 0x06009569 RID: 38249 RVA: 0x002C8D34 File Offset: 0x002C8D34
			private ImmutableList<T>.Node MutateRight(ImmutableList<T>.Node right)
			{
				Requires.NotNull<ImmutableList<T>.Node>(right, "right");
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(this._key, this._left, right, false);
				}
				this._right = right;
				this._height = ImmutableList<T>.Node.ParentHeight(this._left, right);
				this._count = ImmutableList<T>.Node.ParentCount(this._left, right);
				return this;
			}

			// Token: 0x0600956A RID: 38250 RVA: 0x002C8D9C File Offset: 0x002C8D9C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static byte ParentHeight(ImmutableList<T>.Node left, ImmutableList<T>.Node right)
			{
				return checked(1 + Math.Max(left._height, right._height));
			}

			// Token: 0x0600956B RID: 38251 RVA: 0x002C8DB4 File Offset: 0x002C8DB4
			private static int ParentCount(ImmutableList<T>.Node left, ImmutableList<T>.Node right)
			{
				return 1 + left._count + right._count;
			}

			// Token: 0x0600956C RID: 38252 RVA: 0x002C8DC8 File Offset: 0x002C8DC8
			private ImmutableList<T>.Node MutateKey(T key)
			{
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(key, this._left, this._right, false);
				}
				this._key = key;
				return this;
			}

			// Token: 0x0600956D RID: 38253 RVA: 0x002C8DF4 File Offset: 0x002C8DF4
			private static ImmutableList<T>.Node CreateRange(IEnumerable<T> keys)
			{
				ImmutableList<T> immutableList;
				if (ImmutableList<T>.TryCastToImmutableList(keys, out immutableList))
				{
					return immutableList._root;
				}
				IOrderedCollection<T> orderedCollection = keys.AsOrderedCollection<T>();
				return ImmutableList<T>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			}

			// Token: 0x0600956E RID: 38254 RVA: 0x002C8E30 File Offset: 0x002C8E30
			private static ImmutableList<T>.Node CreateLeaf(T key)
			{
				return new ImmutableList<T>.Node(key, ImmutableList<T>.Node.EmptyNode, ImmutableList<T>.Node.EmptyNode, false);
			}

			// Token: 0x04004C14 RID: 19476
			internal static readonly ImmutableList<T>.Node EmptyNode = new ImmutableList<T>.Node();

			// Token: 0x04004C15 RID: 19477
			private T _key;

			// Token: 0x04004C16 RID: 19478
			private bool _frozen;

			// Token: 0x04004C17 RID: 19479
			private byte _height;

			// Token: 0x04004C18 RID: 19480
			private int _count;

			// Token: 0x04004C19 RID: 19481
			private ImmutableList<T>.Node _left;

			// Token: 0x04004C1A RID: 19482
			private ImmutableList<T>.Node _right;
		}
	}
}
