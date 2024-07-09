using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CA1 RID: 3233
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	[ComVisible(true)]
	public sealed class ImmutableHashSet<T> : IImmutableSet<!0>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable, IHashKeyCollection<T>, ICollection<!0>, ISet<!0>, ICollection, IStrongEnumerable<T, ImmutableHashSet<T>.Enumerator>
	{
		// Token: 0x0600811E RID: 33054 RVA: 0x00261810 File Offset: 0x00261810
		internal ImmutableHashSet(IEqualityComparer<T> equalityComparer) : this(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, equalityComparer, 0)
		{
		}

		// Token: 0x0600811F RID: 33055 RVA: 0x00261820 File Offset: 0x00261820
		private ImmutableHashSet(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			root.Freeze(ImmutableHashSet<T>.s_FreezeBucketAction);
			this._root = root;
			this._count = count;
			this._equalityComparer = equalityComparer;
			this._hashBucketEqualityComparer = ImmutableHashSet<T>.GetHashBucketEqualityComparer(equalityComparer);
		}

		// Token: 0x06008120 RID: 33056 RVA: 0x0026187C File Offset: 0x0026187C
		public ImmutableHashSet<T> Clear()
		{
			if (!this.IsEmpty)
			{
				return ImmutableHashSet<T>.Empty.WithComparer(this._equalityComparer);
			}
			return this;
		}

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x06008121 RID: 33057 RVA: 0x0026189C File Offset: 0x0026189C
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x06008122 RID: 33058 RVA: 0x002618A4 File Offset: 0x002618A4
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x06008123 RID: 33059 RVA: 0x002618B0 File Offset: 0x002618B0
		public IEqualityComparer<T> KeyComparer
		{
			get
			{
				return this._equalityComparer;
			}
		}

		// Token: 0x06008124 RID: 33060 RVA: 0x002618B8 File Offset: 0x002618B8
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x06008125 RID: 33061 RVA: 0x002618C0 File Offset: 0x002618C0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x06008126 RID: 33062 RVA: 0x002618C4 File Offset: 0x002618C4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x06008127 RID: 33063 RVA: 0x002618C8 File Offset: 0x002618C8
		internal IBinaryTree Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x06008128 RID: 33064 RVA: 0x002618D0 File Offset: 0x002618D0
		private ImmutableHashSet<T>.MutationInput Origin
		{
			get
			{
				return new ImmutableHashSet<T>.MutationInput(this);
			}
		}

		// Token: 0x06008129 RID: 33065 RVA: 0x002618D8 File Offset: 0x002618D8
		public ImmutableHashSet<T>.Builder ToBuilder()
		{
			return new ImmutableHashSet<T>.Builder(this);
		}

		// Token: 0x0600812A RID: 33066 RVA: 0x002618E0 File Offset: 0x002618E0
		public ImmutableHashSet<T> Add(T item)
		{
			return ImmutableHashSet<T>.Add(item, this.Origin).Finalize(this);
		}

		// Token: 0x0600812B RID: 33067 RVA: 0x00261908 File Offset: 0x00261908
		public ImmutableHashSet<T> Remove(T item)
		{
			return ImmutableHashSet<T>.Remove(item, this.Origin).Finalize(this);
		}

		// Token: 0x0600812C RID: 33068 RVA: 0x00261930 File Offset: 0x00261930
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			int hashCode = this._equalityComparer.GetHashCode(equalValue);
			ImmutableHashSet<T>.HashBucket hashBucket;
			if (this._root.TryGetValue(hashCode, out hashBucket))
			{
				return hashBucket.TryExchange(equalValue, this._equalityComparer, out actualValue);
			}
			actualValue = equalValue;
			return false;
		}

		// Token: 0x0600812D RID: 33069 RVA: 0x0026197C File Offset: 0x0026197C
		public ImmutableHashSet<T> Union(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return this.Union(other, false);
		}

		// Token: 0x0600812E RID: 33070 RVA: 0x00261994 File Offset: 0x00261994
		public ImmutableHashSet<T> Intersect(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Intersect(other, this.Origin).Finalize(this);
		}

		// Token: 0x0600812F RID: 33071 RVA: 0x002619C8 File Offset: 0x002619C8
		public ImmutableHashSet<T> Except(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Except(other, this._equalityComparer, this._hashBucketEqualityComparer, this._root).Finalize(this);
		}

		// Token: 0x06008130 RID: 33072 RVA: 0x00261A08 File Offset: 0x00261A08
		public ImmutableHashSet<T> SymmetricExcept(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.SymmetricExcept(other, this.Origin).Finalize(this);
		}

		// Token: 0x06008131 RID: 33073 RVA: 0x00261A3C File Offset: 0x00261A3C
		public bool SetEquals(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return this == other || ImmutableHashSet<T>.SetEquals(other, this.Origin);
		}

		// Token: 0x06008132 RID: 33074 RVA: 0x00261A60 File Offset: 0x00261A60
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x00261A7C File Offset: 0x00261A7C
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
		}

		// Token: 0x06008134 RID: 33076 RVA: 0x00261A98 File Offset: 0x00261A98
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
		}

		// Token: 0x06008135 RID: 33077 RVA: 0x00261AB4 File Offset: 0x00261AB4
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
		}

		// Token: 0x06008136 RID: 33078 RVA: 0x00261AD0 File Offset: 0x00261AD0
		public bool Overlaps(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Overlaps(other, this.Origin);
		}

		// Token: 0x06008137 RID: 33079 RVA: 0x00261AEC File Offset: 0x00261AEC
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Add(T item)
		{
			return this.Add(item);
		}

		// Token: 0x06008138 RID: 33080 RVA: 0x00261AF8 File Offset: 0x00261AF8
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Remove(T item)
		{
			return this.Remove(item);
		}

		// Token: 0x06008139 RID: 33081 RVA: 0x00261B04 File Offset: 0x00261B04
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Union(IEnumerable<T> other)
		{
			return this.Union(other);
		}

		// Token: 0x0600813A RID: 33082 RVA: 0x00261B10 File Offset: 0x00261B10
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Intersect(IEnumerable<T> other)
		{
			return this.Intersect(other);
		}

		// Token: 0x0600813B RID: 33083 RVA: 0x00261B1C File Offset: 0x00261B1C
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.Except(IEnumerable<T> other)
		{
			return this.Except(other);
		}

		// Token: 0x0600813C RID: 33084 RVA: 0x00261B28 File Offset: 0x00261B28
		[ExcludeFromCodeCoverage]
		IImmutableSet<T> IImmutableSet<!0>.SymmetricExcept(IEnumerable<T> other)
		{
			return this.SymmetricExcept(other);
		}

		// Token: 0x0600813D RID: 33085 RVA: 0x00261B34 File Offset: 0x00261B34
		public bool Contains(T item)
		{
			return ImmutableHashSet<T>.Contains(item, this.Origin);
		}

		// Token: 0x0600813E RID: 33086 RVA: 0x00261B44 File Offset: 0x00261B44
		public ImmutableHashSet<T> WithComparer(IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				equalityComparer = EqualityComparer<T>.Default;
			}
			if (equalityComparer == this._equalityComparer)
			{
				return this;
			}
			ImmutableHashSet<T> immutableHashSet = new ImmutableHashSet<T>(equalityComparer);
			return immutableHashSet.Union(this, true);
		}

		// Token: 0x0600813F RID: 33087 RVA: 0x00261B84 File Offset: 0x00261B84
		bool ISet<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008140 RID: 33088 RVA: 0x00261B8C File Offset: 0x00261B8C
		void ISet<!0>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008141 RID: 33089 RVA: 0x00261B94 File Offset: 0x00261B94
		void ISet<!0>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008142 RID: 33090 RVA: 0x00261B9C File Offset: 0x00261B9C
		void ISet<!0>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008143 RID: 33091 RVA: 0x00261BA4 File Offset: 0x00261BA4
		void ISet<!0>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001BFA RID: 7162
		// (get) Token: 0x06008144 RID: 33092 RVA: 0x00261BAC File Offset: 0x00261BAC
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008145 RID: 33093 RVA: 0x00261BB0 File Offset: 0x00261BB0
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			Requires.NotNull<T[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x06008146 RID: 33094 RVA: 0x00261C44 File Offset: 0x00261C44
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008147 RID: 33095 RVA: 0x00261C4C File Offset: 0x00261C4C
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008148 RID: 33096 RVA: 0x00261C54 File Offset: 0x00261C54
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008149 RID: 33097 RVA: 0x00261C5C File Offset: 0x00261C5C
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array.SetValue(t, arrayIndex++);
			}
		}

		// Token: 0x0600814A RID: 33098 RVA: 0x00261CF8 File Offset: 0x00261CF8
		public ImmutableHashSet<T>.Enumerator GetEnumerator()
		{
			return new ImmutableHashSet<T>.Enumerator(this._root, null);
		}

		// Token: 0x0600814B RID: 33099 RVA: 0x00261D08 File Offset: 0x00261D08
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x0600814C RID: 33100 RVA: 0x00261D3C File Offset: 0x00261D3C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600814D RID: 33101 RVA: 0x00261D4C File Offset: 0x00261D4C
		private static bool IsSupersetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			foreach (T item in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				if (!ImmutableHashSet<T>.Contains(item, origin))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600814E RID: 33102 RVA: 0x00261DC4 File Offset: 0x00261DC4
		private static ImmutableHashSet<T>.MutationResult Add(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			int hashCode = origin.EqualityComparer.GetHashCode(item);
			ImmutableHashSet<T>.OperationResult operationResult;
			ImmutableHashSet<T>.HashBucket newBucket = origin.Root.GetValueOrDefault(hashCode).Add(item, origin.EqualityComparer, out operationResult);
			if (operationResult == ImmutableHashSet<T>.OperationResult.NoChangeRequired)
			{
				return new ImmutableHashSet<T>.MutationResult(origin.Root, 0, ImmutableHashSet<T>.CountType.Adjustment);
			}
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root = ImmutableHashSet<T>.UpdateRoot(origin.Root, hashCode, origin.HashBucketEqualityComparer, newBucket);
			return new ImmutableHashSet<T>.MutationResult(root, 1, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x0600814F RID: 33103 RVA: 0x00261E3C File Offset: 0x00261E3C
		private static ImmutableHashSet<T>.MutationResult Remove(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			ImmutableHashSet<T>.OperationResult operationResult = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
			int hashCode = origin.EqualityComparer.GetHashCode(item);
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root = origin.Root;
			ImmutableHashSet<T>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				ImmutableHashSet<T>.HashBucket newBucket = hashBucket.Remove(item, origin.EqualityComparer, out operationResult);
				if (operationResult == ImmutableHashSet<T>.OperationResult.NoChangeRequired)
				{
					return new ImmutableHashSet<T>.MutationResult(origin.Root, 0, ImmutableHashSet<T>.CountType.Adjustment);
				}
				root = ImmutableHashSet<T>.UpdateRoot(origin.Root, hashCode, origin.HashBucketEqualityComparer, newBucket);
			}
			return new ImmutableHashSet<T>.MutationResult(root, (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged) ? -1 : 0, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x06008150 RID: 33104 RVA: 0x00261ED0 File Offset: 0x00261ED0
		private static bool Contains(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			int hashCode = origin.EqualityComparer.GetHashCode(item);
			ImmutableHashSet<T>.HashBucket hashBucket;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.Contains(item, origin.EqualityComparer);
		}

		// Token: 0x06008151 RID: 33105 RVA: 0x00261F14 File Offset: 0x00261F14
		private static ImmutableHashSet<T>.MutationResult Union(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = origin.Root;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				int hashCode = origin.EqualityComparer.GetHashCode(t);
				ImmutableHashSet<T>.OperationResult operationResult;
				ImmutableHashSet<T>.HashBucket newBucket = sortedInt32KeyNode.GetValueOrDefault(hashCode).Add(t, origin.EqualityComparer, out operationResult);
				if (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged)
				{
					sortedInt32KeyNode = ImmutableHashSet<T>.UpdateRoot(sortedInt32KeyNode, hashCode, origin.HashBucketEqualityComparer, newBucket);
					num++;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, num, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x06008152 RID: 33106 RVA: 0x00261FD4 File Offset: 0x00261FD4
		private static bool Overlaps(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return false;
			}
			foreach (T item in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				if (ImmutableHashSet<T>.Contains(item, origin))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008153 RID: 33107 RVA: 0x00262060 File Offset: 0x00262060
		private static bool SetEquals(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			if (origin.Count != hashSet.Count)
			{
				return false;
			}
			foreach (T item in hashSet)
			{
				if (!ImmutableHashSet<T>.Contains(item, origin))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008154 RID: 33108 RVA: 0x002620F0 File Offset: 0x002620F0
		private static SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> UpdateRoot(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int hashCode, IEqualityComparer<ImmutableHashSet<T>.HashBucket> hashBucketEqualityComparer, ImmutableHashSet<T>.HashBucket newBucket)
		{
			bool flag;
			if (newBucket.IsEmpty)
			{
				return root.Remove(hashCode, out flag);
			}
			bool flag2;
			return root.SetItem(hashCode, newBucket, hashBucketEqualityComparer, out flag2, out flag);
		}

		// Token: 0x06008155 RID: 33109 RVA: 0x00262124 File Offset: 0x00262124
		private static ImmutableHashSet<T>.MutationResult Intersect(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			int num = 0;
			foreach (T item in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				if (ImmutableHashSet<T>.Contains(item, origin))
				{
					ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(item, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, origin.HashBucketEqualityComparer, num));
					root = mutationResult.Root;
					num += mutationResult.Count;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(root, num, ImmutableHashSet<T>.CountType.FinalValue);
		}

		// Token: 0x06008156 RID: 33110 RVA: 0x002621D4 File Offset: 0x002621D4
		private static ImmutableHashSet<T>.MutationResult Except(IEnumerable<T> other, IEqualityComparer<T> equalityComparer, IEqualityComparer<ImmutableHashSet<T>.HashBucket> hashBucketEqualityComparer, SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = root;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				int hashCode = equalityComparer.GetHashCode(t);
				ImmutableHashSet<T>.HashBucket hashBucket;
				if (sortedInt32KeyNode.TryGetValue(hashCode, out hashBucket))
				{
					ImmutableHashSet<T>.OperationResult operationResult;
					ImmutableHashSet<T>.HashBucket newBucket = hashBucket.Remove(t, equalityComparer, out operationResult);
					if (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged)
					{
						num--;
						sortedInt32KeyNode = ImmutableHashSet<T>.UpdateRoot(sortedInt32KeyNode, hashCode, hashBucketEqualityComparer, newBucket);
					}
				}
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, num, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x06008157 RID: 33111 RVA: 0x00262298 File Offset: 0x00262298
		private static ImmutableHashSet<T>.MutationResult SymmetricExcept(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableHashSet<T> immutableHashSet = ImmutableHashSet.CreateRange<T>(origin.EqualityComparer, other);
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			foreach (T item in new ImmutableHashSet<T>.NodeEnumerable(origin.Root))
			{
				if (!immutableHashSet.Contains(item))
				{
					ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(item, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, origin.HashBucketEqualityComparer, num));
					root = mutationResult.Root;
					num += mutationResult.Count;
				}
			}
			foreach (T item2 in immutableHashSet)
			{
				if (!ImmutableHashSet<T>.Contains(item2, origin))
				{
					ImmutableHashSet<T>.MutationResult mutationResult2 = ImmutableHashSet<T>.Add(item2, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, origin.HashBucketEqualityComparer, num));
					root = mutationResult2.Root;
					num += mutationResult2.Count;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(root, num, ImmutableHashSet<T>.CountType.FinalValue);
		}

		// Token: 0x06008158 RID: 33112 RVA: 0x002623D8 File Offset: 0x002623D8
		private static bool IsProperSubsetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return other.Any<T>();
			}
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			if (origin.Count >= hashSet.Count)
			{
				return false;
			}
			int num = 0;
			bool flag = false;
			foreach (T item in hashSet)
			{
				if (ImmutableHashSet<T>.Contains(item, origin))
				{
					num++;
				}
				else
				{
					flag = true;
				}
				if (num == origin.Count && flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008159 RID: 33113 RVA: 0x002624A4 File Offset: 0x002624A4
		private static bool IsProperSupersetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return false;
			}
			int num = 0;
			foreach (T item in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				num++;
				if (!ImmutableHashSet<T>.Contains(item, origin))
				{
					return false;
				}
			}
			return origin.Count > num;
		}

		// Token: 0x0600815A RID: 33114 RVA: 0x00262540 File Offset: 0x00262540
		private static bool IsSubsetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return true;
			}
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			int num = 0;
			foreach (T item in hashSet)
			{
				if (ImmutableHashSet<T>.Contains(item, origin))
				{
					num++;
				}
			}
			return num == origin.Count;
		}

		// Token: 0x0600815B RID: 33115 RVA: 0x002625D8 File Offset: 0x002625D8
		private static ImmutableHashSet<T> Wrap(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			Requires.Range(count >= 0, "count", null);
			return new ImmutableHashSet<T>(root, equalityComparer, count);
		}

		// Token: 0x0600815C RID: 33116 RVA: 0x0026260C File Offset: 0x0026260C
		private static IEqualityComparer<ImmutableHashSet<T>.HashBucket> GetHashBucketEqualityComparer(IEqualityComparer<T> valueComparer)
		{
			if (!ImmutableExtensions.IsValueType<T>())
			{
				return ImmutableHashSet<T>.HashBucketByRefEqualityComparer.DefaultInstance;
			}
			if (valueComparer == EqualityComparer<T>.Default)
			{
				return ImmutableHashSet<T>.HashBucketByValueEqualityComparer.DefaultInstance;
			}
			return new ImmutableHashSet<T>.HashBucketByValueEqualityComparer(valueComparer);
		}

		// Token: 0x0600815D RID: 33117 RVA: 0x00262638 File Offset: 0x00262638
		private ImmutableHashSet<T> Wrap(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int adjustedCountIfDifferentRoot)
		{
			if (root == this._root)
			{
				return this;
			}
			return new ImmutableHashSet<T>(root, this._equalityComparer, adjustedCountIfDifferentRoot);
		}

		// Token: 0x0600815E RID: 33118 RVA: 0x00262658 File Offset: 0x00262658
		private ImmutableHashSet<T> Union(IEnumerable<T> items, bool avoidWithComparer)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (this.IsEmpty && !avoidWithComparer)
			{
				ImmutableHashSet<T> immutableHashSet = items as ImmutableHashSet<T>;
				if (immutableHashSet != null)
				{
					return immutableHashSet.WithComparer(this.KeyComparer);
				}
			}
			return ImmutableHashSet<T>.Union(items, this.Origin).Finalize(this);
		}

		// Token: 0x04003D24 RID: 15652
		public static readonly ImmutableHashSet<T> Empty = new ImmutableHashSet<T>(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, EqualityComparer<T>.Default, 0);

		// Token: 0x04003D25 RID: 15653
		private static readonly Action<KeyValuePair<int, ImmutableHashSet<T>.HashBucket>> s_FreezeBucketAction = delegate(KeyValuePair<int, ImmutableHashSet<T>.HashBucket> kv)
		{
			kv.Value.Freeze();
		};

		// Token: 0x04003D26 RID: 15654
		private readonly IEqualityComparer<T> _equalityComparer;

		// Token: 0x04003D27 RID: 15655
		private readonly int _count;

		// Token: 0x04003D28 RID: 15656
		private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

		// Token: 0x04003D29 RID: 15657
		private readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;

		// Token: 0x0200118E RID: 4494
		private class HashBucketByValueEqualityComparer : IEqualityComparer<ImmutableHashSet<T>.HashBucket>
		{
			// Token: 0x17001E9B RID: 7835
			// (get) Token: 0x060093C0 RID: 37824 RVA: 0x002C3ECC File Offset: 0x002C3ECC
			internal static IEqualityComparer<ImmutableHashSet<T>.HashBucket> DefaultInstance
			{
				get
				{
					return ImmutableHashSet<T>.HashBucketByValueEqualityComparer.s_defaultInstance;
				}
			}

			// Token: 0x060093C1 RID: 37825 RVA: 0x002C3ED4 File Offset: 0x002C3ED4
			internal HashBucketByValueEqualityComparer(IEqualityComparer<T> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<T>>(valueComparer, "valueComparer");
				this._valueComparer = valueComparer;
			}

			// Token: 0x060093C2 RID: 37826 RVA: 0x002C3EF0 File Offset: 0x002C3EF0
			public bool Equals(ImmutableHashSet<T>.HashBucket x, ImmutableHashSet<T>.HashBucket y)
			{
				return x.EqualsByValue(y, this._valueComparer);
			}

			// Token: 0x060093C3 RID: 37827 RVA: 0x002C3F00 File Offset: 0x002C3F00
			public int GetHashCode(ImmutableHashSet<T>.HashBucket obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04004BAE RID: 19374
			private static readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> s_defaultInstance = new ImmutableHashSet<T>.HashBucketByValueEqualityComparer(EqualityComparer<T>.Default);

			// Token: 0x04004BAF RID: 19375
			private readonly IEqualityComparer<T> _valueComparer;
		}

		// Token: 0x0200118F RID: 4495
		private class HashBucketByRefEqualityComparer : IEqualityComparer<ImmutableHashSet<T>.HashBucket>
		{
			// Token: 0x17001E9C RID: 7836
			// (get) Token: 0x060093C5 RID: 37829 RVA: 0x002C3F1C File Offset: 0x002C3F1C
			internal static IEqualityComparer<ImmutableHashSet<T>.HashBucket> DefaultInstance
			{
				get
				{
					return ImmutableHashSet<T>.HashBucketByRefEqualityComparer.s_defaultInstance;
				}
			}

			// Token: 0x060093C6 RID: 37830 RVA: 0x002C3F24 File Offset: 0x002C3F24
			private HashBucketByRefEqualityComparer()
			{
			}

			// Token: 0x060093C7 RID: 37831 RVA: 0x002C3F2C File Offset: 0x002C3F2C
			public bool Equals(ImmutableHashSet<T>.HashBucket x, ImmutableHashSet<T>.HashBucket y)
			{
				return x.EqualsByRef(y);
			}

			// Token: 0x060093C8 RID: 37832 RVA: 0x002C3F38 File Offset: 0x002C3F38
			public int GetHashCode(ImmutableHashSet<T>.HashBucket obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04004BB0 RID: 19376
			private static readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> s_defaultInstance = new ImmutableHashSet<T>.HashBucketByRefEqualityComparer();
		}

		// Token: 0x02001190 RID: 4496
		[DebuggerDisplay("Count = {Count}")]
		public sealed class Builder : IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable, ISet<!0>, ICollection<!0>
		{
			// Token: 0x060093CA RID: 37834 RVA: 0x002C3F4C File Offset: 0x002C3F4C
			internal Builder(ImmutableHashSet<T> set)
			{
				Requires.NotNull<ImmutableHashSet<T>>(set, "set");
				this._root = set._root;
				this._count = set._count;
				this._equalityComparer = set._equalityComparer;
				this._hashBucketEqualityComparer = set._hashBucketEqualityComparer;
				this._immutable = set;
			}

			// Token: 0x17001E9D RID: 7837
			// (get) Token: 0x060093CB RID: 37835 RVA: 0x002C3FB0 File Offset: 0x002C3FB0
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17001E9E RID: 7838
			// (get) Token: 0x060093CC RID: 37836 RVA: 0x002C3FB8 File Offset: 0x002C3FB8
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001E9F RID: 7839
			// (get) Token: 0x060093CD RID: 37837 RVA: 0x002C3FBC File Offset: 0x002C3FBC
			// (set) Token: 0x060093CE RID: 37838 RVA: 0x002C3FC4 File Offset: 0x002C3FC4
			public IEqualityComparer<T> KeyComparer
			{
				get
				{
					return this._equalityComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<T>>(value, "value");
					if (value != this._equalityComparer)
					{
						ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Union(this, new ImmutableHashSet<T>.MutationInput(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, value, this._hashBucketEqualityComparer, 0));
						this._immutable = null;
						this._equalityComparer = value;
						this.Root = mutationResult.Root;
						this._count = mutationResult.Count;
					}
				}
			}

			// Token: 0x17001EA0 RID: 7840
			// (get) Token: 0x060093CF RID: 37839 RVA: 0x002C4030 File Offset: 0x002C4030
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17001EA1 RID: 7841
			// (get) Token: 0x060093D0 RID: 37840 RVA: 0x002C4038 File Offset: 0x002C4038
			private ImmutableHashSet<T>.MutationInput Origin
			{
				get
				{
					return new ImmutableHashSet<T>.MutationInput(this.Root, this._equalityComparer, this._hashBucketEqualityComparer, this._count);
				}
			}

			// Token: 0x17001EA2 RID: 7842
			// (get) Token: 0x060093D1 RID: 37841 RVA: 0x002C4058 File Offset: 0x002C4058
			// (set) Token: 0x060093D2 RID: 37842 RVA: 0x002C4060 File Offset: 0x002C4060
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
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

			// Token: 0x060093D3 RID: 37843 RVA: 0x002C408C File Offset: 0x002C408C
			public ImmutableHashSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.Enumerator(this._root, this);
			}

			// Token: 0x060093D4 RID: 37844 RVA: 0x002C409C File Offset: 0x002C409C
			public ImmutableHashSet<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableHashSet<T>.Wrap(this._root, this._equalityComparer, this._count);
				}
				return this._immutable;
			}

			// Token: 0x060093D5 RID: 37845 RVA: 0x002C40CC File Offset: 0x002C40CC
			public bool Add(T item)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Add(item, this.Origin);
				this.Apply(result);
				return result.Count != 0;
			}

			// Token: 0x060093D6 RID: 37846 RVA: 0x002C40FC File Offset: 0x002C40FC
			public bool Remove(T item)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Remove(item, this.Origin);
				this.Apply(result);
				return result.Count != 0;
			}

			// Token: 0x060093D7 RID: 37847 RVA: 0x002C412C File Offset: 0x002C412C
			public bool Contains(T item)
			{
				return ImmutableHashSet<T>.Contains(item, this.Origin);
			}

			// Token: 0x060093D8 RID: 37848 RVA: 0x002C413C File Offset: 0x002C413C
			public void Clear()
			{
				this._count = 0;
				this.Root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			}

			// Token: 0x060093D9 RID: 37849 RVA: 0x002C4150 File Offset: 0x002C4150
			public void ExceptWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Except(other, this._equalityComparer, this._hashBucketEqualityComparer, this._root);
				this.Apply(result);
			}

			// Token: 0x060093DA RID: 37850 RVA: 0x002C4184 File Offset: 0x002C4184
			public void IntersectWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Intersect(other, this.Origin);
				this.Apply(result);
			}

			// Token: 0x060093DB RID: 37851 RVA: 0x002C41AC File Offset: 0x002C41AC
			public bool IsProperSubsetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
			}

			// Token: 0x060093DC RID: 37852 RVA: 0x002C41BC File Offset: 0x002C41BC
			public bool IsProperSupersetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
			}

			// Token: 0x060093DD RID: 37853 RVA: 0x002C41CC File Offset: 0x002C41CC
			public bool IsSubsetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
			}

			// Token: 0x060093DE RID: 37854 RVA: 0x002C41DC File Offset: 0x002C41DC
			public bool IsSupersetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
			}

			// Token: 0x060093DF RID: 37855 RVA: 0x002C41EC File Offset: 0x002C41EC
			public bool Overlaps(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.Overlaps(other, this.Origin);
			}

			// Token: 0x060093E0 RID: 37856 RVA: 0x002C41FC File Offset: 0x002C41FC
			public bool SetEquals(IEnumerable<T> other)
			{
				return this == other || ImmutableHashSet<T>.SetEquals(other, this.Origin);
			}

			// Token: 0x060093E1 RID: 37857 RVA: 0x002C4214 File Offset: 0x002C4214
			public void SymmetricExceptWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.SymmetricExcept(other, this.Origin);
				this.Apply(result);
			}

			// Token: 0x060093E2 RID: 37858 RVA: 0x002C423C File Offset: 0x002C423C
			public void UnionWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Union(other, this.Origin);
				this.Apply(result);
			}

			// Token: 0x060093E3 RID: 37859 RVA: 0x002C4264 File Offset: 0x002C4264
			void ICollection<!0>.Add(T item)
			{
				this.Add(item);
			}

			// Token: 0x060093E4 RID: 37860 RVA: 0x002C4270 File Offset: 0x002C4270
			void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array[arrayIndex++] = t;
				}
			}

			// Token: 0x060093E5 RID: 37861 RVA: 0x002C4304 File Offset: 0x002C4304
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060093E6 RID: 37862 RVA: 0x002C4314 File Offset: 0x002C4314
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060093E7 RID: 37863 RVA: 0x002C4324 File Offset: 0x002C4324
			private void Apply(ImmutableHashSet<T>.MutationResult result)
			{
				this.Root = result.Root;
				if (result.CountType == ImmutableHashSet<T>.CountType.Adjustment)
				{
					this._count += result.Count;
					return;
				}
				this._count = result.Count;
			}

			// Token: 0x04004BB1 RID: 19377
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;

			// Token: 0x04004BB2 RID: 19378
			private IEqualityComparer<T> _equalityComparer;

			// Token: 0x04004BB3 RID: 19379
			private IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;

			// Token: 0x04004BB4 RID: 19380
			private int _count;

			// Token: 0x04004BB5 RID: 19381
			private ImmutableHashSet<T> _immutable;

			// Token: 0x04004BB6 RID: 19382
			private int _version;
		}

		// Token: 0x02001191 RID: 4497
		public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator, IStrongEnumerator<T>
		{
			// Token: 0x060093E8 RID: 37864 RVA: 0x002C4370 File Offset: 0x002C4370
			internal Enumerator(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, ImmutableHashSet<T>.Builder builder = null)
			{
				this._builder = builder;
				this._mapEnumerator = new SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.Enumerator(root);
				this._bucketEnumerator = default(ImmutableHashSet<T>.HashBucket.Enumerator);
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
			}

			// Token: 0x17001EA3 RID: 7843
			// (get) Token: 0x060093E9 RID: 37865 RVA: 0x002C43AC File Offset: 0x002C43AC
			public T Current
			{
				get
				{
					this._mapEnumerator.ThrowIfDisposed();
					return this._bucketEnumerator.Current;
				}
			}

			// Token: 0x17001EA4 RID: 7844
			// (get) Token: 0x060093EA RID: 37866 RVA: 0x002C43C4 File Offset: 0x002C43C4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060093EB RID: 37867 RVA: 0x002C43D4 File Offset: 0x002C43D4
			public bool MoveNext()
			{
				this.ThrowIfChanged();
				if (this._bucketEnumerator.MoveNext())
				{
					return true;
				}
				if (this._mapEnumerator.MoveNext())
				{
					KeyValuePair<int, ImmutableHashSet<T>.HashBucket> keyValuePair = this._mapEnumerator.Current;
					this._bucketEnumerator = new ImmutableHashSet<T>.HashBucket.Enumerator(keyValuePair.Value);
					return this._bucketEnumerator.MoveNext();
				}
				return false;
			}

			// Token: 0x060093EC RID: 37868 RVA: 0x002C4438 File Offset: 0x002C4438
			public void Reset()
			{
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._mapEnumerator.Reset();
				this._bucketEnumerator.Dispose();
				this._bucketEnumerator = default(ImmutableHashSet<T>.HashBucket.Enumerator);
			}

			// Token: 0x060093ED RID: 37869 RVA: 0x002C4490 File Offset: 0x002C4490
			public void Dispose()
			{
				this._mapEnumerator.Dispose();
				this._bucketEnumerator.Dispose();
			}

			// Token: 0x060093EE RID: 37870 RVA: 0x002C44A8 File Offset: 0x002C44A8
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(System.Collections.Immutable2448884.SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x04004BB7 RID: 19383
			private readonly ImmutableHashSet<T>.Builder _builder;

			// Token: 0x04004BB8 RID: 19384
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.Enumerator _mapEnumerator;

			// Token: 0x04004BB9 RID: 19385
			private ImmutableHashSet<T>.HashBucket.Enumerator _bucketEnumerator;

			// Token: 0x04004BBA RID: 19386
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02001192 RID: 4498
		internal enum OperationResult
		{
			// Token: 0x04004BBC RID: 19388
			SizeChanged,
			// Token: 0x04004BBD RID: 19389
			NoChangeRequired
		}

		// Token: 0x02001193 RID: 4499
		[System.Collections.Immutable.IsReadOnly]
		internal struct HashBucket
		{
			// Token: 0x060093EF RID: 37871 RVA: 0x002C44D8 File Offset: 0x002C44D8
			private HashBucket(T firstElement, ImmutableList<T>.Node additionalElements = null)
			{
				this._firstValue = firstElement;
				this._additionalElements = (additionalElements ?? ImmutableList<T>.Node.EmptyNode);
			}

			// Token: 0x17001EA5 RID: 7845
			// (get) Token: 0x060093F0 RID: 37872 RVA: 0x002C44F4 File Offset: 0x002C44F4
			internal bool IsEmpty
			{
				get
				{
					return this._additionalElements == null;
				}
			}

			// Token: 0x060093F1 RID: 37873 RVA: 0x002C4500 File Offset: 0x002C4500
			public ImmutableHashSet<T>.HashBucket.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.HashBucket.Enumerator(this);
			}

			// Token: 0x060093F2 RID: 37874 RVA: 0x002C4510 File Offset: 0x002C4510
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060093F3 RID: 37875 RVA: 0x002C4518 File Offset: 0x002C4518
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060093F4 RID: 37876 RVA: 0x002C4520 File Offset: 0x002C4520
			internal bool EqualsByRef(ImmutableHashSet<T>.HashBucket other)
			{
				return this._firstValue == other._firstValue && this._additionalElements == other._additionalElements;
			}

			// Token: 0x060093F5 RID: 37877 RVA: 0x002C4550 File Offset: 0x002C4550
			internal bool EqualsByValue(ImmutableHashSet<T>.HashBucket other, IEqualityComparer<T> valueComparer)
			{
				return valueComparer.Equals(this._firstValue, other._firstValue) && this._additionalElements == other._additionalElements;
			}

			// Token: 0x060093F6 RID: 37878 RVA: 0x002C457C File Offset: 0x002C457C
			internal ImmutableHashSet<T>.HashBucket Add(T value, IEqualityComparer<T> valueComparer, out ImmutableHashSet<T>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(value, null);
				}
				if (valueComparer.Equals(value, this._firstValue) || this._additionalElements.IndexOf(value, valueComparer) >= 0)
				{
					result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
					return this;
				}
				result = ImmutableHashSet<T>.OperationResult.SizeChanged;
				return new ImmutableHashSet<T>.HashBucket(this._firstValue, this._additionalElements.Add(value));
			}

			// Token: 0x060093F7 RID: 37879 RVA: 0x002C45EC File Offset: 0x002C45EC
			internal bool Contains(T value, IEqualityComparer<T> valueComparer)
			{
				return !this.IsEmpty && (valueComparer.Equals(value, this._firstValue) || this._additionalElements.IndexOf(value, valueComparer) >= 0);
			}

			// Token: 0x060093F8 RID: 37880 RVA: 0x002C4624 File Offset: 0x002C4624
			internal unsafe bool TryExchange(T value, IEqualityComparer<T> valueComparer, out T existingValue)
			{
				if (!this.IsEmpty)
				{
					if (valueComparer.Equals(value, this._firstValue))
					{
						existingValue = this._firstValue;
						return true;
					}
					int num = this._additionalElements.IndexOf(value, valueComparer);
					if (num >= 0)
					{
						existingValue = *this._additionalElements.ItemRef(num);
						return true;
					}
				}
				existingValue = value;
				return false;
			}

			// Token: 0x060093F9 RID: 37881 RVA: 0x002C4698 File Offset: 0x002C4698
			internal ImmutableHashSet<T>.HashBucket Remove(T value, IEqualityComparer<T> equalityComparer, out ImmutableHashSet<T>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
					return this;
				}
				if (equalityComparer.Equals(this._firstValue, value))
				{
					if (this._additionalElements.IsEmpty)
					{
						result = ImmutableHashSet<T>.OperationResult.SizeChanged;
						return default(ImmutableHashSet<T>.HashBucket);
					}
					int count = this._additionalElements.Left.Count;
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(this._additionalElements.Key, this._additionalElements.RemoveAt(count));
				}
				else
				{
					int num = this._additionalElements.IndexOf(value, equalityComparer);
					if (num < 0)
					{
						result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
						return this;
					}
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(this._firstValue, this._additionalElements.RemoveAt(num));
				}
			}

			// Token: 0x060093FA RID: 37882 RVA: 0x002C4758 File Offset: 0x002C4758
			internal void Freeze()
			{
				if (this._additionalElements != null)
				{
					this._additionalElements.Freeze();
				}
			}

			// Token: 0x04004BBE RID: 19390
			private readonly T _firstValue;

			// Token: 0x04004BBF RID: 19391
			private readonly ImmutableList<T>.Node _additionalElements;

			// Token: 0x02001218 RID: 4632
			internal struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
			{
				// Token: 0x060096BF RID: 38591 RVA: 0x002CCA68 File Offset: 0x002CCA68
				internal Enumerator(ImmutableHashSet<T>.HashBucket bucket)
				{
					this._disposed = false;
					this._bucket = bucket;
					this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
					this._additionalEnumerator = default(ImmutableList<T>.Enumerator);
				}

				// Token: 0x17001F58 RID: 8024
				// (get) Token: 0x060096C0 RID: 38592 RVA: 0x002CCA8C File Offset: 0x002CCA8C
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x17001F59 RID: 8025
				// (get) Token: 0x060096C1 RID: 38593 RVA: 0x002CCA9C File Offset: 0x002CCA9C
				public T Current
				{
					get
					{
						this.ThrowIfDisposed();
						ImmutableHashSet<T>.HashBucket.Enumerator.Position currentPosition = this._currentPosition;
						if (currentPosition == ImmutableHashSet<T>.HashBucket.Enumerator.Position.First)
						{
							return this._bucket._firstValue;
						}
						if (currentPosition != ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional)
						{
							throw new InvalidOperationException();
						}
						return this._additionalEnumerator.Current;
					}
				}

				// Token: 0x060096C2 RID: 38594 RVA: 0x002CCAEC File Offset: 0x002CCAEC
				public bool MoveNext()
				{
					this.ThrowIfDisposed();
					if (this._bucket.IsEmpty)
					{
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.End;
						return false;
					}
					switch (this._currentPosition)
					{
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst:
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.First;
						return true;
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.First:
						if (this._bucket._additionalElements.IsEmpty)
						{
							this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.End;
							return false;
						}
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional;
						this._additionalEnumerator = new ImmutableList<T>.Enumerator(this._bucket._additionalElements, null, -1, -1, false);
						return this._additionalEnumerator.MoveNext();
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional:
						return this._additionalEnumerator.MoveNext();
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.End:
						return false;
					default:
						throw new InvalidOperationException();
					}
				}

				// Token: 0x060096C3 RID: 38595 RVA: 0x002CCBA8 File Offset: 0x002CCBA8
				public void Reset()
				{
					this.ThrowIfDisposed();
					this._additionalEnumerator.Dispose();
					this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
				}

				// Token: 0x060096C4 RID: 38596 RVA: 0x002CCBC4 File Offset: 0x002CCBC4
				public void Dispose()
				{
					this._disposed = true;
					this._additionalEnumerator.Dispose();
				}

				// Token: 0x060096C5 RID: 38597 RVA: 0x002CCBD8 File Offset: 0x002CCBD8
				private void ThrowIfDisposed()
				{
					if (this._disposed)
					{
						Requires.FailObjectDisposed<ImmutableHashSet<T>.HashBucket.Enumerator>(this);
					}
				}

				// Token: 0x04004F2C RID: 20268
				private readonly ImmutableHashSet<T>.HashBucket _bucket;

				// Token: 0x04004F2D RID: 20269
				private bool _disposed;

				// Token: 0x04004F2E RID: 20270
				private ImmutableHashSet<T>.HashBucket.Enumerator.Position _currentPosition;

				// Token: 0x04004F2F RID: 20271
				private ImmutableList<T>.Enumerator _additionalEnumerator;

				// Token: 0x0200121E RID: 4638
				private enum Position
				{
					// Token: 0x04004F45 RID: 20293
					BeforeFirst,
					// Token: 0x04004F46 RID: 20294
					First,
					// Token: 0x04004F47 RID: 20295
					Additional,
					// Token: 0x04004F48 RID: 20296
					End
				}
			}
		}

		// Token: 0x02001194 RID: 4500
		[System.Collections.Immutable.IsReadOnly]
		private struct MutationInput
		{
			// Token: 0x060093FB RID: 37883 RVA: 0x002C4770 File Offset: 0x002C4770
			internal MutationInput(ImmutableHashSet<T> set)
			{
				Requires.NotNull<ImmutableHashSet<T>>(set, "set");
				this._root = set._root;
				this._equalityComparer = set._equalityComparer;
				this._count = set._count;
				this._hashBucketEqualityComparer = set._hashBucketEqualityComparer;
			}

			// Token: 0x060093FC RID: 37884 RVA: 0x002C47B0 File Offset: 0x002C47B0
			internal MutationInput(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, IEqualityComparer<ImmutableHashSet<T>.HashBucket> hashBucketEqualityComparer, int count)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
				Requires.Range(count >= 0, "count", null);
				Requires.NotNull<IEqualityComparer<ImmutableHashSet<T>.HashBucket>>(hashBucketEqualityComparer, "hashBucketEqualityComparer");
				this._root = root;
				this._equalityComparer = equalityComparer;
				this._count = count;
				this._hashBucketEqualityComparer = hashBucketEqualityComparer;
			}

			// Token: 0x17001EA6 RID: 7846
			// (get) Token: 0x060093FD RID: 37885 RVA: 0x002C4814 File Offset: 0x002C4814
			internal SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17001EA7 RID: 7847
			// (get) Token: 0x060093FE RID: 37886 RVA: 0x002C481C File Offset: 0x002C481C
			internal IEqualityComparer<T> EqualityComparer
			{
				get
				{
					return this._equalityComparer;
				}
			}

			// Token: 0x17001EA8 RID: 7848
			// (get) Token: 0x060093FF RID: 37887 RVA: 0x002C4824 File Offset: 0x002C4824
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17001EA9 RID: 7849
			// (get) Token: 0x06009400 RID: 37888 RVA: 0x002C482C File Offset: 0x002C482C
			internal IEqualityComparer<ImmutableHashSet<T>.HashBucket> HashBucketEqualityComparer
			{
				get
				{
					return this._hashBucketEqualityComparer;
				}
			}

			// Token: 0x04004BC0 RID: 19392
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

			// Token: 0x04004BC1 RID: 19393
			private readonly IEqualityComparer<T> _equalityComparer;

			// Token: 0x04004BC2 RID: 19394
			private readonly int _count;

			// Token: 0x04004BC3 RID: 19395
			private readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;
		}

		// Token: 0x02001195 RID: 4501
		private enum CountType
		{
			// Token: 0x04004BC5 RID: 19397
			Adjustment,
			// Token: 0x04004BC6 RID: 19398
			FinalValue
		}

		// Token: 0x02001196 RID: 4502
		[System.Collections.Immutable.IsReadOnly]
		private struct MutationResult
		{
			// Token: 0x06009401 RID: 37889 RVA: 0x002C4834 File Offset: 0x002C4834
			internal MutationResult(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int count, ImmutableHashSet<T>.CountType countType = ImmutableHashSet<T>.CountType.Adjustment)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				this._root = root;
				this._count = count;
				this._countType = countType;
			}

			// Token: 0x17001EAA RID: 7850
			// (get) Token: 0x06009402 RID: 37890 RVA: 0x002C4858 File Offset: 0x002C4858
			internal SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17001EAB RID: 7851
			// (get) Token: 0x06009403 RID: 37891 RVA: 0x002C4860 File Offset: 0x002C4860
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17001EAC RID: 7852
			// (get) Token: 0x06009404 RID: 37892 RVA: 0x002C4868 File Offset: 0x002C4868
			internal ImmutableHashSet<T>.CountType CountType
			{
				get
				{
					return this._countType;
				}
			}

			// Token: 0x06009405 RID: 37893 RVA: 0x002C4870 File Offset: 0x002C4870
			internal ImmutableHashSet<T> Finalize(ImmutableHashSet<T> priorSet)
			{
				Requires.NotNull<ImmutableHashSet<T>>(priorSet, "priorSet");
				int num = this.Count;
				if (this.CountType == ImmutableHashSet<T>.CountType.Adjustment)
				{
					num += priorSet._count;
				}
				return priorSet.Wrap(this.Root, num);
			}

			// Token: 0x04004BC7 RID: 19399
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

			// Token: 0x04004BC8 RID: 19400
			private readonly int _count;

			// Token: 0x04004BC9 RID: 19401
			private readonly ImmutableHashSet<T>.CountType _countType;
		}

		// Token: 0x02001197 RID: 4503
		[System.Collections.Immutable.IsReadOnly]
		private struct NodeEnumerable : IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06009406 RID: 37894 RVA: 0x002C48B4 File Offset: 0x002C48B4
			internal NodeEnumerable(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				this._root = root;
			}

			// Token: 0x06009407 RID: 37895 RVA: 0x002C48C8 File Offset: 0x002C48C8
			public ImmutableHashSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.Enumerator(this._root, null);
			}

			// Token: 0x06009408 RID: 37896 RVA: 0x002C48D8 File Offset: 0x002C48D8
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009409 RID: 37897 RVA: 0x002C48E8 File Offset: 0x002C48E8
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04004BCA RID: 19402
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;
		}
	}
}
