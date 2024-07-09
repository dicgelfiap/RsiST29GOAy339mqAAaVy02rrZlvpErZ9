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
	// Token: 0x02000CB9 RID: 3257
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	[ComVisible(true)]
	public sealed class ImmutableSortedSet<T> : IImmutableSet<T>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable, ISortKeyCollection<T>, IReadOnlyList<T>, IList<!0>, ICollection<!0>, ISet<T>, IList, ICollection, IStrongEnumerable<T, ImmutableSortedSet<T>.Enumerator>
	{
		// Token: 0x06008362 RID: 33634 RVA: 0x0026754C File Offset: 0x0026754C
		internal ImmutableSortedSet(IComparer<T> comparer = null)
		{
			this._root = ImmutableSortedSet<T>.Node.EmptyNode;
			this._comparer = (comparer ?? Comparer<T>.Default);
		}

		// Token: 0x06008363 RID: 33635 RVA: 0x00267574 File Offset: 0x00267574
		private ImmutableSortedSet(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
			Requires.NotNull<IComparer<T>>(comparer, "comparer");
			root.Freeze();
			this._root = root;
			this._comparer = comparer;
		}

		// Token: 0x06008364 RID: 33636 RVA: 0x002675A8 File Offset: 0x002675A8
		public ImmutableSortedSet<T> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedSet<T>.Empty.WithComparer(this._comparer);
			}
			return this;
		}

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x06008365 RID: 33637 RVA: 0x002675CC File Offset: 0x002675CC
		public T Max
		{
			get
			{
				return this._root.Max;
			}
		}

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x06008366 RID: 33638 RVA: 0x002675DC File Offset: 0x002675DC
		public T Min
		{
			get
			{
				return this._root.Min;
			}
		}

		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x06008367 RID: 33639 RVA: 0x002675EC File Offset: 0x002675EC
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x06008368 RID: 33640 RVA: 0x002675FC File Offset: 0x002675FC
		public int Count
		{
			get
			{
				return this._root.Count;
			}
		}

		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x06008369 RID: 33641 RVA: 0x0026760C File Offset: 0x0026760C
		public IComparer<T> KeyComparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x0600836A RID: 33642 RVA: 0x00267614 File Offset: 0x00267614
		internal IBinaryTree Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17001C51 RID: 7249
		public unsafe T this[int index]
		{
			get
			{
				return *this._root.ItemRef(index);
			}
		}

		// Token: 0x0600836C RID: 33644 RVA: 0x00267630 File Offset: 0x00267630
		[return: System.Collections.Immutable.IsReadOnly]
		public ref T ItemRef(int index)
		{
			return this._root.ItemRef(index);
		}

		// Token: 0x0600836D RID: 33645 RVA: 0x00267640 File Offset: 0x00267640
		public ImmutableSortedSet<T>.Builder ToBuilder()
		{
			return new ImmutableSortedSet<T>.Builder(this);
		}

		// Token: 0x0600836E RID: 33646 RVA: 0x00267648 File Offset: 0x00267648
		public ImmutableSortedSet<T> Add(T value)
		{
			bool flag;
			return this.Wrap(this._root.Add(value, this._comparer, out flag));
		}

		// Token: 0x0600836F RID: 33647 RVA: 0x00267674 File Offset: 0x00267674
		public ImmutableSortedSet<T> Remove(T value)
		{
			bool flag;
			return this.Wrap(this._root.Remove(value, this._comparer, out flag));
		}

		// Token: 0x06008370 RID: 33648 RVA: 0x002676A0 File Offset: 0x002676A0
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			ImmutableSortedSet<T>.Node node = this._root.Search(equalValue, this._comparer);
			if (node.IsEmpty)
			{
				actualValue = equalValue;
				return false;
			}
			actualValue = node.Key;
			return true;
		}

		// Token: 0x06008371 RID: 33649 RVA: 0x002676E8 File Offset: 0x002676E8
		public ImmutableSortedSet<T> Intersect(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet = this.Clear();
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (this.Contains(value))
				{
					immutableSortedSet = immutableSortedSet.Add(value);
				}
			}
			return immutableSortedSet;
		}

		// Token: 0x06008372 RID: 33650 RVA: 0x00267764 File Offset: 0x00267764
		public ImmutableSortedSet<T> Except(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T>.Node node = this._root;
			foreach (T key in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				bool flag;
				node = node.Remove(key, this._comparer, out flag);
			}
			return this.Wrap(node);
		}

		// Token: 0x06008373 RID: 33651 RVA: 0x002677E4 File Offset: 0x002677E4
		public ImmutableSortedSet<T> SymmetricExcept(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet = ImmutableSortedSet.CreateRange<T>(this._comparer, other);
			ImmutableSortedSet<T> immutableSortedSet2 = this.Clear();
			foreach (T value in this)
			{
				if (!immutableSortedSet.Contains(value))
				{
					immutableSortedSet2 = immutableSortedSet2.Add(value);
				}
			}
			foreach (T value2 in immutableSortedSet)
			{
				if (!this.Contains(value2))
				{
					immutableSortedSet2 = immutableSortedSet2.Add(value2);
				}
			}
			return immutableSortedSet2;
		}

		// Token: 0x06008374 RID: 33652 RVA: 0x002678BC File Offset: 0x002678BC
		public ImmutableSortedSet<T> Union(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet;
			if (ImmutableSortedSet<T>.TryCastToImmutableSortedSet(other, out immutableSortedSet) && immutableSortedSet.KeyComparer == this.KeyComparer)
			{
				if (immutableSortedSet.IsEmpty)
				{
					return this;
				}
				if (this.IsEmpty)
				{
					return immutableSortedSet;
				}
				if (immutableSortedSet.Count > this.Count)
				{
					return immutableSortedSet.Union(this);
				}
			}
			int num;
			if (this.IsEmpty || (other.TryGetCount(out num) && (float)(this.Count + num) * 0.15f > (float)this.Count))
			{
				return this.LeafToRootRefill(other);
			}
			return this.UnionIncremental(other);
		}

		// Token: 0x06008375 RID: 33653 RVA: 0x0026796C File Offset: 0x0026796C
		public ImmutableSortedSet<T> WithComparer(IComparer<T> comparer)
		{
			if (comparer == null)
			{
				comparer = Comparer<T>.Default;
			}
			if (comparer == this._comparer)
			{
				return this;
			}
			ImmutableSortedSet<T> immutableSortedSet = new ImmutableSortedSet<T>(ImmutableSortedSet<T>.Node.EmptyNode, comparer);
			return immutableSortedSet.Union(this);
		}

		// Token: 0x06008376 RID: 33654 RVA: 0x002679B0 File Offset: 0x002679B0
		public bool SetEquals(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this == other)
			{
				return true;
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			if (this.Count != sortedSet.Count)
			{
				return false;
			}
			int num = 0;
			foreach (T value in sortedSet)
			{
				if (!this.Contains(value))
				{
					return false;
				}
				num++;
			}
			return num == this.Count;
		}

		// Token: 0x06008377 RID: 33655 RVA: 0x00267A58 File Offset: 0x00267A58
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return other.Any<T>();
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			if (this.Count >= sortedSet.Count)
			{
				return false;
			}
			int num = 0;
			bool flag = false;
			foreach (T value in sortedSet)
			{
				if (this.Contains(value))
				{
					num++;
				}
				else
				{
					flag = true;
				}
				if (num == this.Count && flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008378 RID: 33656 RVA: 0x00267B1C File Offset: 0x00267B1C
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return false;
			}
			int num = 0;
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				num++;
				if (!this.Contains(value))
				{
					return false;
				}
			}
			return this.Count > num;
		}

		// Token: 0x06008379 RID: 33657 RVA: 0x00267BB0 File Offset: 0x00267BB0
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return true;
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			int num = 0;
			foreach (T value in sortedSet)
			{
				if (this.Contains(value))
				{
					num++;
				}
			}
			return num == this.Count;
		}

		// Token: 0x0600837A RID: 33658 RVA: 0x00267C40 File Offset: 0x00267C40
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (!this.Contains(value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600837B RID: 33659 RVA: 0x00267CB8 File Offset: 0x00267CB8
		public bool Overlaps(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return false;
			}
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (this.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600837C RID: 33660 RVA: 0x00267D3C File Offset: 0x00267D3C
		public IEnumerable<T> Reverse()
		{
			return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
		}

		// Token: 0x0600837D RID: 33661 RVA: 0x00267D4C File Offset: 0x00267D4C
		public int IndexOf(T item)
		{
			return this._root.IndexOf(item, this._comparer);
		}

		// Token: 0x0600837E RID: 33662 RVA: 0x00267D60 File Offset: 0x00267D60
		public bool Contains(T value)
		{
			return this._root.Contains(value, this._comparer);
		}

		// Token: 0x0600837F RID: 33663 RVA: 0x00267D74 File Offset: 0x00267D74
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x06008380 RID: 33664 RVA: 0x00267D7C File Offset: 0x00267D7C
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Add(T value)
		{
			return this.Add(value);
		}

		// Token: 0x06008381 RID: 33665 RVA: 0x00267D88 File Offset: 0x00267D88
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Remove(T value)
		{
			return this.Remove(value);
		}

		// Token: 0x06008382 RID: 33666 RVA: 0x00267D94 File Offset: 0x00267D94
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Intersect(IEnumerable<T> other)
		{
			return this.Intersect(other);
		}

		// Token: 0x06008383 RID: 33667 RVA: 0x00267DA0 File Offset: 0x00267DA0
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Except(IEnumerable<T> other)
		{
			return this.Except(other);
		}

		// Token: 0x06008384 RID: 33668 RVA: 0x00267DAC File Offset: 0x00267DAC
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.SymmetricExcept(IEnumerable<T> other)
		{
			return this.SymmetricExcept(other);
		}

		// Token: 0x06008385 RID: 33669 RVA: 0x00267DB8 File Offset: 0x00267DB8
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Union(IEnumerable<T> other)
		{
			return this.Union(other);
		}

		// Token: 0x06008386 RID: 33670 RVA: 0x00267DC4 File Offset: 0x00267DC4
		bool ISet<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008387 RID: 33671 RVA: 0x00267DCC File Offset: 0x00267DCC
		void ISet<!0>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008388 RID: 33672 RVA: 0x00267DD4 File Offset: 0x00267DD4
		void ISet<!0>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008389 RID: 33673 RVA: 0x00267DDC File Offset: 0x00267DDC
		void ISet<!0>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600838A RID: 33674 RVA: 0x00267DE4 File Offset: 0x00267DE4
		void ISet<!0>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x0600838B RID: 33675 RVA: 0x00267DEC File Offset: 0x00267DEC
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600838C RID: 33676 RVA: 0x00267DF0 File Offset: 0x00267DF0
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600838D RID: 33677 RVA: 0x00267E00 File Offset: 0x00267E00
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600838E RID: 33678 RVA: 0x00267E08 File Offset: 0x00267E08
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600838F RID: 33679 RVA: 0x00267E10 File Offset: 0x00267E10
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C53 RID: 7251
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

		// Token: 0x06008392 RID: 33682 RVA: 0x00267E2C File Offset: 0x00267E2C
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008393 RID: 33683 RVA: 0x00267E34 File Offset: 0x00267E34
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x06008394 RID: 33684 RVA: 0x00267E3C File Offset: 0x00267E3C
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x06008395 RID: 33685 RVA: 0x00267E40 File Offset: 0x00267E40
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C56 RID: 7254
		// (get) Token: 0x06008396 RID: 33686 RVA: 0x00267E44 File Offset: 0x00267E44
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x06008397 RID: 33687 RVA: 0x00267E48 File Offset: 0x00267E48
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008398 RID: 33688 RVA: 0x00267E4C File Offset: 0x00267E4C
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008399 RID: 33689 RVA: 0x00267E54 File Offset: 0x00267E54
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600839A RID: 33690 RVA: 0x00267E5C File Offset: 0x00267E5C
		bool IList.Contains(object value)
		{
			return this.Contains((T)((object)value));
		}

		// Token: 0x0600839B RID: 33691 RVA: 0x00267E6C File Offset: 0x00267E6C
		int IList.IndexOf(object value)
		{
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x0600839C RID: 33692 RVA: 0x00267E7C File Offset: 0x00267E7C
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600839D RID: 33693 RVA: 0x00267E84 File Offset: 0x00267E84
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600839E RID: 33694 RVA: 0x00267E8C File Offset: 0x00267E8C
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C58 RID: 7256
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

		// Token: 0x060083A1 RID: 33697 RVA: 0x00267EAC File Offset: 0x00267EAC
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index);
		}

		// Token: 0x060083A2 RID: 33698 RVA: 0x00267EBC File Offset: 0x00267EBC
		[ExcludeFromCodeCoverage]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x060083A3 RID: 33699 RVA: 0x00267EF0 File Offset: 0x00267EF0
		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060083A4 RID: 33700 RVA: 0x00267F00 File Offset: 0x00267F00
		public ImmutableSortedSet<T>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x060083A5 RID: 33701 RVA: 0x00267F10 File Offset: 0x00267F10
		private static bool TryCastToImmutableSortedSet(IEnumerable<T> sequence, out ImmutableSortedSet<T> other)
		{
			other = (sequence as ImmutableSortedSet<T>);
			if (other != null)
			{
				return true;
			}
			ImmutableSortedSet<T>.Builder builder = sequence as ImmutableSortedSet<T>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x060083A6 RID: 33702 RVA: 0x00267F4C File Offset: 0x00267F4C
		private static ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedSet<T>(root, comparer);
			}
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		// Token: 0x060083A7 RID: 33703 RVA: 0x00267F6C File Offset: 0x00267F6C
		private ImmutableSortedSet<T> UnionIncremental(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableSortedSet<T>.Node node = this._root;
			foreach (T key in items.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				bool flag;
				node = node.Add(key, this._comparer, out flag);
			}
			return this.Wrap(node);
		}

		// Token: 0x060083A8 RID: 33704 RVA: 0x00267FEC File Offset: 0x00267FEC
		private ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root)
		{
			if (root == this._root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableSortedSet<T>(root, this._comparer);
			}
			return this.Clear();
		}

		// Token: 0x060083A9 RID: 33705 RVA: 0x0026801C File Offset: 0x0026801C
		private ImmutableSortedSet<T> LeafToRootRefill(IEnumerable<T> addedItems)
		{
			Requires.NotNull<IEnumerable<T>>(addedItems, "addedItems");
			List<T> list;
			if (this.IsEmpty)
			{
				int num;
				if (addedItems.TryGetCount(out num) && num == 0)
				{
					return this;
				}
				list = new List<T>(addedItems);
				if (list.Count == 0)
				{
					return this;
				}
			}
			else
			{
				list = new List<T>(this);
				list.AddRange(addedItems);
			}
			IComparer<T> keyComparer = this.KeyComparer;
			list.Sort(keyComparer);
			int num2 = 1;
			for (int i = 1; i < list.Count; i++)
			{
				if (keyComparer.Compare(list[i], list[i - 1]) != 0)
				{
					list[num2++] = list[i];
				}
			}
			list.RemoveRange(num2, list.Count - num2);
			ImmutableSortedSet<T>.Node root = ImmutableSortedSet<T>.Node.NodeTreeFromList(list.AsOrderedCollection<T>(), 0, list.Count);
			return this.Wrap(root);
		}

		// Token: 0x04003D46 RID: 15686
		private const float RefillOverIncrementalThreshold = 0.15f;

		// Token: 0x04003D47 RID: 15687
		public static readonly ImmutableSortedSet<T> Empty = new ImmutableSortedSet<T>(null);

		// Token: 0x04003D48 RID: 15688
		private readonly ImmutableSortedSet<T>.Node _root;

		// Token: 0x04003D49 RID: 15689
		private readonly IComparer<T> _comparer;

		// Token: 0x020011B5 RID: 4533
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableSortedSetBuilderDebuggerProxy<>))]
		public sealed class Builder : ISortKeyCollection<T>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable, ISet<!0>, ICollection<!0>, ICollection
		{
			// Token: 0x060095E5 RID: 38373 RVA: 0x002CA63C File Offset: 0x002CA63C
			internal Builder(ImmutableSortedSet<T> set)
			{
				Requires.NotNull<ImmutableSortedSet<T>>(set, "set");
				this._root = set._root;
				this._comparer = set.KeyComparer;
				this._immutable = set;
			}

			// Token: 0x17001F22 RID: 7970
			// (get) Token: 0x060095E6 RID: 38374 RVA: 0x002CA694 File Offset: 0x002CA694
			public int Count
			{
				get
				{
					return this.Root.Count;
				}
			}

			// Token: 0x17001F23 RID: 7971
			// (get) Token: 0x060095E7 RID: 38375 RVA: 0x002CA6A4 File Offset: 0x002CA6A4
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001F24 RID: 7972
			public unsafe T this[int index]
			{
				get
				{
					return *this._root.ItemRef(index);
				}
			}

			// Token: 0x060095E9 RID: 38377 RVA: 0x002CA6BC File Offset: 0x002CA6BC
			[return: System.Collections.Immutable.IsReadOnly]
			public ref T ItemRef(int index)
			{
				return this._root.ItemRef(index);
			}

			// Token: 0x17001F25 RID: 7973
			// (get) Token: 0x060095EA RID: 38378 RVA: 0x002CA6CC File Offset: 0x002CA6CC
			public T Max
			{
				get
				{
					return this._root.Max;
				}
			}

			// Token: 0x17001F26 RID: 7974
			// (get) Token: 0x060095EB RID: 38379 RVA: 0x002CA6DC File Offset: 0x002CA6DC
			public T Min
			{
				get
				{
					return this._root.Min;
				}
			}

			// Token: 0x17001F27 RID: 7975
			// (get) Token: 0x060095EC RID: 38380 RVA: 0x002CA6EC File Offset: 0x002CA6EC
			// (set) Token: 0x060095ED RID: 38381 RVA: 0x002CA6F4 File Offset: 0x002CA6F4
			public IComparer<T> KeyComparer
			{
				get
				{
					return this._comparer;
				}
				set
				{
					Requires.NotNull<IComparer<T>>(value, "value");
					if (value != this._comparer)
					{
						ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
						foreach (T key in this)
						{
							bool flag;
							node = node.Add(key, value, out flag);
						}
						this._immutable = null;
						this._comparer = value;
						this.Root = node;
					}
				}
			}

			// Token: 0x17001F28 RID: 7976
			// (get) Token: 0x060095EE RID: 38382 RVA: 0x002CA780 File Offset: 0x002CA780
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17001F29 RID: 7977
			// (get) Token: 0x060095EF RID: 38383 RVA: 0x002CA788 File Offset: 0x002CA788
			// (set) Token: 0x060095F0 RID: 38384 RVA: 0x002CA790 File Offset: 0x002CA790
			private ImmutableSortedSet<T>.Node Root
			{
				get
				{
					return this._root;
				}
				set
				{
					this._version++;
					if (this._root != value)
					{
						this._root = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x060095F1 RID: 38385 RVA: 0x002CA7BC File Offset: 0x002CA7BC
			public bool Add(T item)
			{
				bool result;
				this.Root = this.Root.Add(item, this._comparer, out result);
				return result;
			}

			// Token: 0x060095F2 RID: 38386 RVA: 0x002CA7E8 File Offset: 0x002CA7E8
			public void ExceptWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T key in other)
				{
					bool flag;
					this.Root = this.Root.Remove(key, this._comparer, out flag);
				}
			}

			// Token: 0x060095F3 RID: 38387 RVA: 0x002CA858 File Offset: 0x002CA858
			public void IntersectWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
				foreach (T t in other)
				{
					if (this.Contains(t))
					{
						bool flag;
						node = node.Add(t, this._comparer, out flag);
					}
				}
				this.Root = node;
			}

			// Token: 0x060095F4 RID: 38388 RVA: 0x002CA8D8 File Offset: 0x002CA8D8
			public bool IsProperSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSubsetOf(other);
			}

			// Token: 0x060095F5 RID: 38389 RVA: 0x002CA8E8 File Offset: 0x002CA8E8
			public bool IsProperSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSupersetOf(other);
			}

			// Token: 0x060095F6 RID: 38390 RVA: 0x002CA8F8 File Offset: 0x002CA8F8
			public bool IsSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSubsetOf(other);
			}

			// Token: 0x060095F7 RID: 38391 RVA: 0x002CA908 File Offset: 0x002CA908
			public bool IsSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSupersetOf(other);
			}

			// Token: 0x060095F8 RID: 38392 RVA: 0x002CA918 File Offset: 0x002CA918
			public bool Overlaps(IEnumerable<T> other)
			{
				return this.ToImmutable().Overlaps(other);
			}

			// Token: 0x060095F9 RID: 38393 RVA: 0x002CA928 File Offset: 0x002CA928
			public bool SetEquals(IEnumerable<T> other)
			{
				return this.ToImmutable().SetEquals(other);
			}

			// Token: 0x060095FA RID: 38394 RVA: 0x002CA938 File Offset: 0x002CA938
			public void SymmetricExceptWith(IEnumerable<T> other)
			{
				this.Root = this.ToImmutable().SymmetricExcept(other)._root;
			}

			// Token: 0x060095FB RID: 38395 RVA: 0x002CA954 File Offset: 0x002CA954
			public void UnionWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T key in other)
				{
					bool flag;
					this.Root = this.Root.Add(key, this._comparer, out flag);
				}
			}

			// Token: 0x060095FC RID: 38396 RVA: 0x002CA9C4 File Offset: 0x002CA9C4
			void ICollection<!0>.Add(T item)
			{
				this.Add(item);
			}

			// Token: 0x060095FD RID: 38397 RVA: 0x002CA9D0 File Offset: 0x002CA9D0
			public void Clear()
			{
				this.Root = ImmutableSortedSet<T>.Node.EmptyNode;
			}

			// Token: 0x060095FE RID: 38398 RVA: 0x002CA9E0 File Offset: 0x002CA9E0
			public bool Contains(T item)
			{
				return this.Root.Contains(item, this._comparer);
			}

			// Token: 0x060095FF RID: 38399 RVA: 0x002CA9F4 File Offset: 0x002CA9F4
			void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
			{
				this._root.CopyTo(array, arrayIndex);
			}

			// Token: 0x06009600 RID: 38400 RVA: 0x002CAA04 File Offset: 0x002CAA04
			public bool Remove(T item)
			{
				bool result;
				this.Root = this.Root.Remove(item, this._comparer, out result);
				return result;
			}

			// Token: 0x06009601 RID: 38401 RVA: 0x002CAA30 File Offset: 0x002CAA30
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x06009602 RID: 38402 RVA: 0x002CAA40 File Offset: 0x002CAA40
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.Root.GetEnumerator();
			}

			// Token: 0x06009603 RID: 38403 RVA: 0x002CAA54 File Offset: 0x002CAA54
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009604 RID: 38404 RVA: 0x002CAA64 File Offset: 0x002CAA64
			public IEnumerable<T> Reverse()
			{
				return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
			}

			// Token: 0x06009605 RID: 38405 RVA: 0x002CAA74 File Offset: 0x002CAA74
			public ImmutableSortedSet<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableSortedSet<T>.Wrap(this.Root, this._comparer);
				}
				return this._immutable;
			}

			// Token: 0x06009606 RID: 38406 RVA: 0x002CAAA0 File Offset: 0x002CAAA0
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex);
			}

			// Token: 0x17001F2A RID: 7978
			// (get) Token: 0x06009607 RID: 38407 RVA: 0x002CAAB0 File Offset: 0x002CAAB0
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001F2B RID: 7979
			// (get) Token: 0x06009608 RID: 38408 RVA: 0x002CAAB4 File Offset: 0x002CAAB4
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

			// Token: 0x04004C39 RID: 19513
			private ImmutableSortedSet<T>.Node _root = ImmutableSortedSet<T>.Node.EmptyNode;

			// Token: 0x04004C3A RID: 19514
			private IComparer<T> _comparer = Comparer<T>.Default;

			// Token: 0x04004C3B RID: 19515
			private ImmutableSortedSet<T> _immutable;

			// Token: 0x04004C3C RID: 19516
			private int _version;

			// Token: 0x04004C3D RID: 19517
			private object _syncRoot;
		}

		// Token: 0x020011B6 RID: 4534
		private class ReverseEnumerable : IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06009609 RID: 38409 RVA: 0x002CAADC File Offset: 0x002CAADC
			internal ReverseEnumerable(ImmutableSortedSet<T>.Node root)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
				this._root = root;
			}

			// Token: 0x0600960A RID: 38410 RVA: 0x002CAAF8 File Offset: 0x002CAAF8
			public IEnumerator<T> GetEnumerator()
			{
				return this._root.Reverse();
			}

			// Token: 0x0600960B RID: 38411 RVA: 0x002CAB08 File Offset: 0x002CAB08
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04004C3E RID: 19518
			private readonly ImmutableSortedSet<T>.Node _root;
		}

		// Token: 0x020011B7 RID: 4535
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator, ISecurePooledObjectUser, IStrongEnumerator<T>
		{
			// Token: 0x0600960C RID: 38412 RVA: 0x002CAB10 File Offset: 0x002CAB10
			internal Enumerator(ImmutableSortedSet<T>.Node root, ImmutableSortedSet<T>.Builder builder = null, bool reverse = false)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._reverse = reverse;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
				{
					this._stack = ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>(root.Height));
				}
				this.PushNext(this._root);
			}

			// Token: 0x17001F2C RID: 7980
			// (get) Token: 0x0600960D RID: 38413 RVA: 0x002CABBC File Offset: 0x002CABBC
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17001F2D RID: 7981
			// (get) Token: 0x0600960E RID: 38414 RVA: 0x002CABC4 File Offset: 0x002CABC4
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

			// Token: 0x17001F2E RID: 7982
			// (get) Token: 0x0600960F RID: 38415 RVA: 0x002CABE8 File Offset: 0x002CABE8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06009610 RID: 38416 RVA: 0x002CABF8 File Offset: 0x002CABF8
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableSortedSet<T>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedSet<T>.Node>>();
					ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
					this._stack = null;
				}
			}

			// Token: 0x06009611 RID: 38417 RVA: 0x002CAC58 File Offset: 0x002CAC58
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				if (stack.Count > 0)
				{
					ImmutableSortedSet<T>.Node value = stack.Pop().Value;
					this._current = value;
					this.PushNext(this._reverse ? value.Left : value.Right);
					return true;
				}
				this._current = null;
				return false;
			}

			// Token: 0x06009612 RID: 38418 RVA: 0x002CACD0 File Offset: 0x002CACD0
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedSet<T>.Node>>();
				this.PushNext(this._root);
			}

			// Token: 0x06009613 RID: 38419 RVA: 0x002CAD30 File Offset: 0x002CAD30
			private void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedSet<T>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedSet<T>.Enumerator>(this);
				}
			}

			// Token: 0x06009614 RID: 38420 RVA: 0x002CAD64 File Offset: 0x002CAD64
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(System.Collections.Immutable2448884.SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x06009615 RID: 38421 RVA: 0x002CAD94 File Offset: 0x002CAD94
			private void PushNext(ImmutableSortedSet<T>.Node node)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(node, "node");
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableSortedSet<T>.Node>(node));
					node = (this._reverse ? node.Right : node.Left);
				}
			}

			// Token: 0x04004C3F RID: 19519
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator>();

			// Token: 0x04004C40 RID: 19520
			private readonly ImmutableSortedSet<T>.Builder _builder;

			// Token: 0x04004C41 RID: 19521
			private readonly int _poolUserId;

			// Token: 0x04004C42 RID: 19522
			private readonly bool _reverse;

			// Token: 0x04004C43 RID: 19523
			private ImmutableSortedSet<T>.Node _root;

			// Token: 0x04004C44 RID: 19524
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>> _stack;

			// Token: 0x04004C45 RID: 19525
			private ImmutableSortedSet<T>.Node _current;

			// Token: 0x04004C46 RID: 19526
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x020011B8 RID: 4536
		[DebuggerDisplay("{_key}")]
		internal sealed class Node : IBinaryTree<!0>, IBinaryTree, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06009617 RID: 38423 RVA: 0x002CAE00 File Offset: 0x002CAE00
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x06009618 RID: 38424 RVA: 0x002CAE10 File Offset: 0x002CAE10
			private Node(T key, ImmutableSortedSet<T>.Node left, ImmutableSortedSet<T>.Node right, bool frozen = false)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(left, "left");
				Requires.NotNull<ImmutableSortedSet<T>.Node>(right, "right");
				this._key = key;
				this._left = left;
				this._right = right;
				this._height = checked(1 + Math.Max(left._height, right._height));
				this._count = 1 + left._count + right._count;
				this._frozen = frozen;
			}

			// Token: 0x17001F2F RID: 7983
			// (get) Token: 0x06009619 RID: 38425 RVA: 0x002CAE8C File Offset: 0x002CAE8C
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17001F30 RID: 7984
			// (get) Token: 0x0600961A RID: 38426 RVA: 0x002CAE98 File Offset: 0x002CAE98
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17001F31 RID: 7985
			// (get) Token: 0x0600961B RID: 38427 RVA: 0x002CAEA0 File Offset: 0x002CAEA0
			public ImmutableSortedSet<T>.Node Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17001F32 RID: 7986
			// (get) Token: 0x0600961C RID: 38428 RVA: 0x002CAEA8 File Offset: 0x002CAEA8
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17001F33 RID: 7987
			// (get) Token: 0x0600961D RID: 38429 RVA: 0x002CAEB0 File Offset: 0x002CAEB0
			public ImmutableSortedSet<T>.Node Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17001F34 RID: 7988
			// (get) Token: 0x0600961E RID: 38430 RVA: 0x002CAEB8 File Offset: 0x002CAEB8
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17001F35 RID: 7989
			// (get) Token: 0x0600961F RID: 38431 RVA: 0x002CAEC0 File Offset: 0x002CAEC0
			IBinaryTree<T> IBinaryTree<!0>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17001F36 RID: 7990
			// (get) Token: 0x06009620 RID: 38432 RVA: 0x002CAEC8 File Offset: 0x002CAEC8
			IBinaryTree<T> IBinaryTree<!0>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17001F37 RID: 7991
			// (get) Token: 0x06009621 RID: 38433 RVA: 0x002CAED0 File Offset: 0x002CAED0
			public T Value
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17001F38 RID: 7992
			// (get) Token: 0x06009622 RID: 38434 RVA: 0x002CAED8 File Offset: 0x002CAED8
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17001F39 RID: 7993
			// (get) Token: 0x06009623 RID: 38435 RVA: 0x002CAEE0 File Offset: 0x002CAEE0
			internal T Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17001F3A RID: 7994
			// (get) Token: 0x06009624 RID: 38436 RVA: 0x002CAEE8 File Offset: 0x002CAEE8
			internal T Max
			{
				get
				{
					if (this.IsEmpty)
					{
						return default(T);
					}
					ImmutableSortedSet<T>.Node node = this;
					while (!node._right.IsEmpty)
					{
						node = node._right;
					}
					return node._key;
				}
			}

			// Token: 0x17001F3B RID: 7995
			// (get) Token: 0x06009625 RID: 38437 RVA: 0x002CAF30 File Offset: 0x002CAF30
			internal T Min
			{
				get
				{
					if (this.IsEmpty)
					{
						return default(T);
					}
					ImmutableSortedSet<T>.Node node = this;
					while (!node._left.IsEmpty)
					{
						node = node._left;
					}
					return node._key;
				}
			}

			// Token: 0x17001F3C RID: 7996
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

			// Token: 0x06009627 RID: 38439 RVA: 0x002CAFFC File Offset: 0x002CAFFC
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

			// Token: 0x06009628 RID: 38440 RVA: 0x002CB080 File Offset: 0x002CB080
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, false);
			}

			// Token: 0x06009629 RID: 38441 RVA: 0x002CB08C File Offset: 0x002CB08C
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600962A RID: 38442 RVA: 0x002CB09C File Offset: 0x002CB09C
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600962B RID: 38443 RVA: 0x002CB0AC File Offset: 0x002CB0AC
			internal ImmutableSortedSet<T>.Enumerator GetEnumerator(ImmutableSortedSet<T>.Builder builder)
			{
				return new ImmutableSortedSet<T>.Enumerator(this, builder, false);
			}

			// Token: 0x0600962C RID: 38444 RVA: 0x002CB0B8 File Offset: 0x002CB0B8
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

			// Token: 0x0600962D RID: 38445 RVA: 0x002CB14C File Offset: 0x002CB14C
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

			// Token: 0x0600962E RID: 38446 RVA: 0x002CB1E8 File Offset: 0x002CB1E8
			internal ImmutableSortedSet<T>.Node Add(T key, IComparer<T> comparer, out bool mutated)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					mutated = true;
					return new ImmutableSortedSet<T>.Node(key, this, this, false);
				}
				ImmutableSortedSet<T>.Node node = this;
				int num = comparer.Compare(key, this._key);
				if (num > 0)
				{
					ImmutableSortedSet<T>.Node right = this._right.Add(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right);
					}
				}
				else
				{
					if (num >= 0)
					{
						mutated = false;
						return this;
					}
					ImmutableSortedSet<T>.Node left = this._left.Add(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				if (!mutated)
				{
					return node;
				}
				return ImmutableSortedSet<T>.Node.MakeBalanced(node);
			}

			// Token: 0x0600962F RID: 38447 RVA: 0x002CB298 File Offset: 0x002CB298
			internal ImmutableSortedSet<T>.Node Remove(T key, IComparer<T> comparer, out bool mutated)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					mutated = false;
					return this;
				}
				ImmutableSortedSet<T>.Node node = this;
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					mutated = true;
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableSortedSet<T>.Node.EmptyNode;
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
						ImmutableSortedSet<T>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						bool flag;
						ImmutableSortedSet<T>.Node right = this._right.Remove(node2._key, comparer, out flag);
						node = node2.Mutate(this._left, right);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedSet<T>.Node left = this._left.Remove(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				else
				{
					ImmutableSortedSet<T>.Node right2 = this._right.Remove(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right2);
					}
				}
				if (!node.IsEmpty)
				{
					return ImmutableSortedSet<T>.Node.MakeBalanced(node);
				}
				return node;
			}

			// Token: 0x06009630 RID: 38448 RVA: 0x002CB40C File Offset: 0x002CB40C
			internal bool Contains(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				return !this.Search(key, comparer).IsEmpty;
			}

			// Token: 0x06009631 RID: 38449 RVA: 0x002CB42C File Offset: 0x002CB42C
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x06009632 RID: 38450 RVA: 0x002CB458 File Offset: 0x002CB458
			internal ImmutableSortedSet<T>.Node Search(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					return this;
				}
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					return this;
				}
				if (num > 0)
				{
					return this._right.Search(key, comparer);
				}
				return this._left.Search(key, comparer);
			}

			// Token: 0x06009633 RID: 38451 RVA: 0x002CB4BC File Offset: 0x002CB4BC
			internal int IndexOf(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					return -1;
				}
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					return this._left.Count;
				}
				if (num > 0)
				{
					int num2 = this._right.IndexOf(key, comparer);
					bool flag = num2 < 0;
					if (flag)
					{
						num2 = ~num2;
					}
					num2 = this._left.Count + 1 + num2;
					if (flag)
					{
						num2 = ~num2;
					}
					return num2;
				}
				return this._left.IndexOf(key, comparer);
			}

			// Token: 0x06009634 RID: 38452 RVA: 0x002CB550 File Offset: 0x002CB550
			internal IEnumerator<T> Reverse()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, true);
			}

			// Token: 0x06009635 RID: 38453 RVA: 0x002CB560 File Offset: 0x002CB560
			private static ImmutableSortedSet<T>.Node RotateLeft(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node right = tree._right;
				return right.Mutate(tree.Mutate(null, right._left), null);
			}

			// Token: 0x06009636 RID: 38454 RVA: 0x002CB5AC File Offset: 0x002CB5AC
			private static ImmutableSortedSet<T>.Node RotateRight(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node left = tree._left;
				return left.Mutate(null, tree.Mutate(left._right, null));
			}

			// Token: 0x06009637 RID: 38455 RVA: 0x002CB5F8 File Offset: 0x002CB5F8
			private static ImmutableSortedSet<T>.Node DoubleLeft(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node tree2 = tree.Mutate(null, ImmutableSortedSet<T>.Node.RotateRight(tree._right));
				return ImmutableSortedSet<T>.Node.RotateLeft(tree2);
			}

			// Token: 0x06009638 RID: 38456 RVA: 0x002CB640 File Offset: 0x002CB640
			private static ImmutableSortedSet<T>.Node DoubleRight(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node tree2 = tree.Mutate(ImmutableSortedSet<T>.Node.RotateLeft(tree._left), null);
				return ImmutableSortedSet<T>.Node.RotateRight(tree2);
			}

			// Token: 0x06009639 RID: 38457 RVA: 0x002CB688 File Offset: 0x002CB688
			private static int Balance(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x0600963A RID: 38458 RVA: 0x002CB6AC File Offset: 0x002CB6AC
			private static bool IsRightHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) >= 2;
			}

			// Token: 0x0600963B RID: 38459 RVA: 0x002CB6C8 File Offset: 0x002CB6C8
			private static bool IsLeftHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) <= -2;
			}

			// Token: 0x0600963C RID: 38460 RVA: 0x002CB6E4 File Offset: 0x002CB6E4
			private static ImmutableSortedSet<T>.Node MakeBalanced(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (ImmutableSortedSet<T>.Node.IsRightHeavy(tree))
				{
					if (ImmutableSortedSet<T>.Node.Balance(tree._right) >= 0)
					{
						return ImmutableSortedSet<T>.Node.RotateLeft(tree);
					}
					return ImmutableSortedSet<T>.Node.DoubleLeft(tree);
				}
				else
				{
					if (!ImmutableSortedSet<T>.Node.IsLeftHeavy(tree))
					{
						return tree;
					}
					if (ImmutableSortedSet<T>.Node.Balance(tree._left) <= 0)
					{
						return ImmutableSortedSet<T>.Node.RotateRight(tree);
					}
					return ImmutableSortedSet<T>.Node.DoubleRight(tree);
				}
			}

			// Token: 0x0600963D RID: 38461 RVA: 0x002CB758 File Offset: 0x002CB758
			internal static ImmutableSortedSet<T>.Node NodeTreeFromList(IOrderedCollection<T> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<T>>(items, "items");
				if (length == 0)
				{
					return ImmutableSortedSet<T>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableSortedSet<T>.Node left = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start, num2);
				ImmutableSortedSet<T>.Node right = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				return new ImmutableSortedSet<T>.Node(items[start + num2], left, right, true);
			}

			// Token: 0x0600963E RID: 38462 RVA: 0x002CB7B4 File Offset: 0x002CB7B4
			private ImmutableSortedSet<T>.Node Mutate(ImmutableSortedSet<T>.Node left = null, ImmutableSortedSet<T>.Node right = null)
			{
				if (this._frozen)
				{
					return new ImmutableSortedSet<T>.Node(this._key, left ?? this._left, right ?? this._right, false);
				}
				if (left != null)
				{
					this._left = left;
				}
				if (right != null)
				{
					this._right = right;
				}
				this._height = checked(1 + Math.Max(this._left._height, this._right._height));
				this._count = 1 + this._left._count + this._right._count;
				return this;
			}

			// Token: 0x04004C47 RID: 19527
			internal static readonly ImmutableSortedSet<T>.Node EmptyNode = new ImmutableSortedSet<T>.Node();

			// Token: 0x04004C48 RID: 19528
			private readonly T _key;

			// Token: 0x04004C49 RID: 19529
			private bool _frozen;

			// Token: 0x04004C4A RID: 19530
			private byte _height;

			// Token: 0x04004C4B RID: 19531
			private int _count;

			// Token: 0x04004C4C RID: 19532
			private ImmutableSortedSet<T>.Node _left;

			// Token: 0x04004C4D RID: 19533
			private ImmutableSortedSet<T>.Node _right;
		}
	}
}
