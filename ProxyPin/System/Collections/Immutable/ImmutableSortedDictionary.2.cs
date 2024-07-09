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
	// Token: 0x02000CB6 RID: 3254
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableDictionaryDebuggerProxy<, >))]
	[ComVisible(true)]
	public sealed class ImmutableSortedDictionary<TKey, TValue> : IImmutableDictionary<!0, !1>, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, ISortKeyCollection<TKey>, IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IDictionary, ICollection
	{
		// Token: 0x06008310 RID: 33552 RVA: 0x00266AB8 File Offset: 0x00266AB8
		internal ImmutableSortedDictionary(IComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null)
		{
			this._keyComparer = (keyComparer ?? Comparer<TKey>.Default);
			this._valueComparer = (valueComparer ?? EqualityComparer<TValue>.Default);
			this._root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
		}

		// Token: 0x06008311 RID: 33553 RVA: 0x00266AF4 File Offset: 0x00266AF4
		private ImmutableSortedDictionary(ImmutableSortedDictionary<TKey, TValue>.Node root, int count, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(root, "root");
			Requires.Range(count >= 0, "count", null);
			Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
			Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
			root.Freeze();
			this._root = root;
			this._count = count;
			this._keyComparer = keyComparer;
			this._valueComparer = valueComparer;
		}

		// Token: 0x06008312 RID: 33554 RVA: 0x00266B64 File Offset: 0x00266B64
		public ImmutableSortedDictionary<TKey, TValue> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(this._keyComparer, this._valueComparer);
			}
			return this;
		}

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x06008313 RID: 33555 RVA: 0x00266B90 File Offset: 0x00266B90
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this._valueComparer;
			}
		}

		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x06008314 RID: 33556 RVA: 0x00266B98 File Offset: 0x00266B98
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x06008315 RID: 33557 RVA: 0x00266BA8 File Offset: 0x00266BA8
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x06008316 RID: 33558 RVA: 0x00266BB0 File Offset: 0x00266BB0
		public IEnumerable<TKey> Keys
		{
			get
			{
				return this._root.Keys;
			}
		}

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x06008317 RID: 33559 RVA: 0x00266BC0 File Offset: 0x00266BC0
		public IEnumerable<TValue> Values
		{
			get
			{
				return this._root.Values;
			}
		}

		// Token: 0x06008318 RID: 33560 RVA: 0x00266BD0 File Offset: 0x00266BD0
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x06008319 RID: 33561 RVA: 0x00266BD8 File Offset: 0x00266BD8
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x0600831A RID: 33562 RVA: 0x00266BE0 File Offset: 0x00266BE0
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x0600831B RID: 33563 RVA: 0x00266BE8 File Offset: 0x00266BE8
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x0600831C RID: 33564 RVA: 0x00266BEC File Offset: 0x00266BEC
		public IComparer<TKey> KeyComparer
		{
			get
			{
				return this._keyComparer;
			}
		}

		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x0600831D RID: 33565 RVA: 0x00266BF4 File Offset: 0x00266BF4
		internal ImmutableSortedDictionary<TKey, TValue>.Node Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17001C41 RID: 7233
		public TValue this[TKey key]
		{
			get
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue result;
				if (this.TryGetValue(key, out result))
				{
					return result;
				}
				throw new KeyNotFoundException(System.Collections.Immutable2448884.SR.Format(System.Collections.Immutable2448884.SR.Arg_KeyNotFoundWithKey, key.ToString()));
			}
		}

		// Token: 0x0600831F RID: 33567 RVA: 0x00266C44 File Offset: 0x00266C44
		[return: System.Collections.Immutable.IsReadOnly]
		public ref TValue ValueRef(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.ValueRef(key, this._keyComparer);
		}

		// Token: 0x17001C42 RID: 7234
		TValue IDictionary<!0, !1>.this[TKey key]
		{
			get
			{
				return this[key];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06008322 RID: 33570 RVA: 0x00266C78 File Offset: 0x00266C78
		public ImmutableSortedDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableSortedDictionary<TKey, TValue>.Builder(this);
		}

		// Token: 0x06008323 RID: 33571 RVA: 0x00266C80 File Offset: 0x00266C80
		public ImmutableSortedDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node root = this._root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
			return this.Wrap(root, this._count + 1);
		}

		// Token: 0x06008324 RID: 33572 RVA: 0x00266CC8 File Offset: 0x00266CC8
		public ImmutableSortedDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			bool flag2;
			ImmutableSortedDictionary<TKey, TValue>.Node root = this._root.SetItem(key, value, this._keyComparer, this._valueComparer, out flag, out flag2);
			return this.Wrap(root, flag ? this._count : (this._count + 1));
		}

		// Token: 0x06008325 RID: 33573 RVA: 0x00266D24 File Offset: 0x00266D24
		public ImmutableSortedDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, true, false);
		}

		// Token: 0x06008326 RID: 33574 RVA: 0x00266D3C File Offset: 0x00266D3C
		public ImmutableSortedDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, false, false);
		}

		// Token: 0x06008327 RID: 33575 RVA: 0x00266D54 File Offset: 0x00266D54
		public ImmutableSortedDictionary<TKey, TValue> Remove(TKey value)
		{
			Requires.NotNullAllowStructs<TKey>(value, "value");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node root = this._root.Remove(value, this._keyComparer, out flag);
			return this.Wrap(root, this._count - 1);
		}

		// Token: 0x06008328 RID: 33576 RVA: 0x00266D94 File Offset: 0x00266D94
		public ImmutableSortedDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
		{
			Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root;
			int num = this._count;
			foreach (TKey key in keys)
			{
				bool flag;
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = node.Remove(key, this._keyComparer, out flag);
				if (flag)
				{
					node = node2;
					num--;
				}
			}
			return this.Wrap(node, num);
		}

		// Token: 0x06008329 RID: 33577 RVA: 0x00266E20 File Offset: 0x00266E20
		public ImmutableSortedDictionary<TKey, TValue> WithComparers(IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if (keyComparer == null)
			{
				keyComparer = Comparer<TKey>.Default;
			}
			if (valueComparer == null)
			{
				valueComparer = EqualityComparer<TValue>.Default;
			}
			if (keyComparer != this._keyComparer)
			{
				ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = new ImmutableSortedDictionary<TKey, TValue>(ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode, 0, keyComparer, valueComparer);
				return immutableSortedDictionary.AddRange(this, false, true);
			}
			if (valueComparer == this._valueComparer)
			{
				return this;
			}
			return new ImmutableSortedDictionary<TKey, TValue>(this._root, this._count, this._keyComparer, valueComparer);
		}

		// Token: 0x0600832A RID: 33578 RVA: 0x00266E98 File Offset: 0x00266E98
		public ImmutableSortedDictionary<TKey, TValue> WithComparers(IComparer<TKey> keyComparer)
		{
			return this.WithComparers(keyComparer, this._valueComparer);
		}

		// Token: 0x0600832B RID: 33579 RVA: 0x00266EA8 File Offset: 0x00266EA8
		public bool ContainsValue(TValue value)
		{
			return this._root.ContainsValue(value, this._valueComparer);
		}

		// Token: 0x0600832C RID: 33580 RVA: 0x00266EBC File Offset: 0x00266EBC
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x0600832D RID: 33581 RVA: 0x00266EC8 File Offset: 0x00266EC8
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x0600832E RID: 33582 RVA: 0x00266ED4 File Offset: 0x00266ED4
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x0600832F RID: 33583 RVA: 0x00266EE0 File Offset: 0x00266EE0
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x06008330 RID: 33584 RVA: 0x00266EEC File Offset: 0x00266EEC
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x06008331 RID: 33585 RVA: 0x00266EF8 File Offset: 0x00266EF8
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		// Token: 0x06008332 RID: 33586 RVA: 0x00266F04 File Offset: 0x00266F04
		public bool ContainsKey(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.ContainsKey(key, this._keyComparer);
		}

		// Token: 0x06008333 RID: 33587 RVA: 0x00266F24 File Offset: 0x00266F24
		public bool Contains(KeyValuePair<TKey, TValue> pair)
		{
			return this._root.Contains(pair, this._keyComparer, this._valueComparer);
		}

		// Token: 0x06008334 RID: 33588 RVA: 0x00266F40 File Offset: 0x00266F40
		public bool TryGetValue(TKey key, out TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.TryGetValue(key, this._keyComparer, out value);
		}

		// Token: 0x06008335 RID: 33589 RVA: 0x00266F60 File Offset: 0x00266F60
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
			return this._root.TryGetKey(equalKey, this._keyComparer, out actualKey);
		}

		// Token: 0x06008336 RID: 33590 RVA: 0x00266F80 File Offset: 0x00266F80
		void IDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008337 RID: 33591 RVA: 0x00266F88 File Offset: 0x00266F88
		bool IDictionary<!0, !1>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008338 RID: 33592 RVA: 0x00266F90 File Offset: 0x00266F90
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008339 RID: 33593 RVA: 0x00266F98 File Offset: 0x00266F98
		void ICollection<KeyValuePair<!0, !1>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600833A RID: 33594 RVA: 0x00266FA0 File Offset: 0x00266FA0
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600833B RID: 33595 RVA: 0x00266FA8 File Offset: 0x00266FA8
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array[arrayIndex++] = keyValuePair;
			}
		}

		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x0600833C RID: 33596 RVA: 0x0026703C File Offset: 0x0026703C
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x0600833D RID: 33597 RVA: 0x00267040 File Offset: 0x00267040
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x0600833E RID: 33598 RVA: 0x00267044 File Offset: 0x00267044
		ICollection IDictionary.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x0600833F RID: 33599 RVA: 0x0026704C File Offset: 0x0026704C
		ICollection IDictionary.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x06008340 RID: 33600 RVA: 0x00267054 File Offset: 0x00267054
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008341 RID: 33601 RVA: 0x0026705C File Offset: 0x0026705C
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06008342 RID: 33602 RVA: 0x0026706C File Offset: 0x0026706C
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		// Token: 0x06008343 RID: 33603 RVA: 0x00267080 File Offset: 0x00267080
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C47 RID: 7239
		object IDictionary.this[object key]
		{
			get
			{
				return this[(TKey)((object)key)];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06008346 RID: 33606 RVA: 0x002670A4 File Offset: 0x002670A4
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008347 RID: 33607 RVA: 0x002670AC File Offset: 0x002670AC
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index, this.Count);
		}

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x06008348 RID: 33608 RVA: 0x002670C4 File Offset: 0x002670C4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x06008349 RID: 33609 RVA: 0x002670C8 File Offset: 0x002670C8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600834A RID: 33610 RVA: 0x002670CC File Offset: 0x002670CC
		[ExcludeFromCodeCoverage]
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
		}

		// Token: 0x0600834B RID: 33611 RVA: 0x00267100 File Offset: 0x00267100
		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600834C RID: 33612 RVA: 0x00267110 File Offset: 0x00267110
		public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x0600834D RID: 33613 RVA: 0x00267120 File Offset: 0x00267120
		private static ImmutableSortedDictionary<TKey, TValue> Wrap(ImmutableSortedDictionary<TKey, TValue>.Node root, int count, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(root, count, keyComparer, valueComparer);
			}
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x0600834E RID: 33614 RVA: 0x00267144 File Offset: 0x00267144
		private static bool TryCastToImmutableMap(IEnumerable<KeyValuePair<TKey, TValue>> sequence, out ImmutableSortedDictionary<TKey, TValue> other)
		{
			other = (sequence as ImmutableSortedDictionary<TKey, TValue>);
			if (other != null)
			{
				return true;
			}
			ImmutableSortedDictionary<TKey, TValue>.Builder builder = sequence as ImmutableSortedDictionary<TKey, TValue>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x0600834F RID: 33615 RVA: 0x00267180 File Offset: 0x00267180
		private ImmutableSortedDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items, bool overwriteOnCollision, bool avoidToSortedMap)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			if (this.IsEmpty && !avoidToSortedMap)
			{
				return this.FillFromEmpty(items, overwriteOnCollision);
			}
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root;
			int num = this._count;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
			{
				bool flag = false;
				bool flag2;
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = overwriteOnCollision ? node.SetItem(keyValuePair.Key, keyValuePair.Value, this._keyComparer, this._valueComparer, out flag, out flag2) : node.Add(keyValuePair.Key, keyValuePair.Value, this._keyComparer, this._valueComparer, out flag2);
				if (flag2)
				{
					node = node2;
					if (!flag)
					{
						num++;
					}
				}
			}
			return this.Wrap(node, num);
		}

		// Token: 0x06008350 RID: 33616 RVA: 0x00267270 File Offset: 0x00267270
		private ImmutableSortedDictionary<TKey, TValue> Wrap(ImmutableSortedDictionary<TKey, TValue>.Node root, int adjustedCountIfDifferentRoot)
		{
			if (this._root == root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(root, adjustedCountIfDifferentRoot, this._keyComparer, this._valueComparer);
			}
			return this.Clear();
		}

		// Token: 0x06008351 RID: 33617 RVA: 0x002672A8 File Offset: 0x002672A8
		private ImmutableSortedDictionary<TKey, TValue> FillFromEmpty(IEnumerable<KeyValuePair<TKey, TValue>> items, bool overwriteOnCollision)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary;
			if (ImmutableSortedDictionary<TKey, TValue>.TryCastToImmutableMap(items, out immutableSortedDictionary))
			{
				return immutableSortedDictionary.WithComparers(this.KeyComparer, this.ValueComparer);
			}
			IDictionary<TKey, TValue> dictionary = items as IDictionary<!0, !1>;
			SortedDictionary<TKey, TValue> sortedDictionary;
			if (dictionary != null)
			{
				sortedDictionary = new SortedDictionary<TKey, TValue>(dictionary, this.KeyComparer);
			}
			else
			{
				sortedDictionary = new SortedDictionary<TKey, TValue>(this.KeyComparer);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
				{
					TValue x;
					if (overwriteOnCollision)
					{
						sortedDictionary[keyValuePair.Key] = keyValuePair.Value;
					}
					else if (sortedDictionary.TryGetValue(keyValuePair.Key, out x))
					{
						if (!this._valueComparer.Equals(x, keyValuePair.Value))
						{
							throw new ArgumentException(System.Collections.Immutable2448884.SR.Format(System.Collections.Immutable2448884.SR.DuplicateKey, keyValuePair.Key));
						}
					}
					else
					{
						sortedDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			if (sortedDictionary.Count == 0)
			{
				return this;
			}
			ImmutableSortedDictionary<TKey, TValue>.Node root = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromSortedDictionary(sortedDictionary);
			return new ImmutableSortedDictionary<TKey, TValue>(root, sortedDictionary.Count, this.KeyComparer, this.ValueComparer);
		}

		// Token: 0x04003D3F RID: 15679
		public static readonly ImmutableSortedDictionary<TKey, TValue> Empty = new ImmutableSortedDictionary<TKey, TValue>(null, null);

		// Token: 0x04003D40 RID: 15680
		private readonly ImmutableSortedDictionary<TKey, TValue>.Node _root;

		// Token: 0x04003D41 RID: 15681
		private readonly int _count;

		// Token: 0x04003D42 RID: 15682
		private readonly IComparer<TKey> _keyComparer;

		// Token: 0x04003D43 RID: 15683
		private readonly IEqualityComparer<TValue> _valueComparer;

		// Token: 0x020011B2 RID: 4530
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableSortedDictionaryBuilderDebuggerProxy<, >))]
		public sealed class Builder : IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x0600957C RID: 38268 RVA: 0x002C910C File Offset: 0x002C910C
			internal Builder(ImmutableSortedDictionary<TKey, TValue> map)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>>(map, "map");
				this._root = map._root;
				this._keyComparer = map.KeyComparer;
				this._valueComparer = map.ValueComparer;
				this._count = map.Count;
				this._immutable = map;
			}

			// Token: 0x17001F01 RID: 7937
			// (get) Token: 0x0600957D RID: 38269 RVA: 0x002C9188 File Offset: 0x002C9188
			ICollection<TKey> IDictionary<!0, !1>.Keys
			{
				get
				{
					return this.Root.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17001F02 RID: 7938
			// (get) Token: 0x0600957E RID: 38270 RVA: 0x002C91A0 File Offset: 0x002C91A0
			public IEnumerable<TKey> Keys
			{
				get
				{
					return this.Root.Keys;
				}
			}

			// Token: 0x17001F03 RID: 7939
			// (get) Token: 0x0600957F RID: 38271 RVA: 0x002C91B0 File Offset: 0x002C91B0
			ICollection<TValue> IDictionary<!0, !1>.Values
			{
				get
				{
					return this.Root.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17001F04 RID: 7940
			// (get) Token: 0x06009580 RID: 38272 RVA: 0x002C91C8 File Offset: 0x002C91C8
			public IEnumerable<TValue> Values
			{
				get
				{
					return this.Root.Values;
				}
			}

			// Token: 0x17001F05 RID: 7941
			// (get) Token: 0x06009581 RID: 38273 RVA: 0x002C91D8 File Offset: 0x002C91D8
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17001F06 RID: 7942
			// (get) Token: 0x06009582 RID: 38274 RVA: 0x002C91E0 File Offset: 0x002C91E0
			bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001F07 RID: 7943
			// (get) Token: 0x06009583 RID: 38275 RVA: 0x002C91E4 File Offset: 0x002C91E4
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17001F08 RID: 7944
			// (get) Token: 0x06009584 RID: 38276 RVA: 0x002C91EC File Offset: 0x002C91EC
			// (set) Token: 0x06009585 RID: 38277 RVA: 0x002C91F4 File Offset: 0x002C91F4
			private ImmutableSortedDictionary<TKey, TValue>.Node Root
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

			// Token: 0x17001F09 RID: 7945
			public TValue this[TKey key]
			{
				get
				{
					TValue result;
					if (this.TryGetValue(key, out result))
					{
						return result;
					}
					throw new KeyNotFoundException(System.Collections.Immutable2448884.SR.Format(System.Collections.Immutable2448884.SR.Arg_KeyNotFoundWithKey, key.ToString()));
				}
				set
				{
					bool flag;
					bool flag2;
					this.Root = this._root.SetItem(key, value, this._keyComparer, this._valueComparer, out flag, out flag2);
					if (flag2 && !flag)
					{
						this._count++;
					}
				}
			}

			// Token: 0x06009588 RID: 38280 RVA: 0x002C92B0 File Offset: 0x002C92B0
			[return: System.Collections.Immutable.IsReadOnly]
			public ref TValue ValueRef(TKey key)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				return this._root.ValueRef(key, this._keyComparer);
			}

			// Token: 0x17001F0A RID: 7946
			// (get) Token: 0x06009589 RID: 38281 RVA: 0x002C92D0 File Offset: 0x002C92D0
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001F0B RID: 7947
			// (get) Token: 0x0600958A RID: 38282 RVA: 0x002C92D4 File Offset: 0x002C92D4
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001F0C RID: 7948
			// (get) Token: 0x0600958B RID: 38283 RVA: 0x002C92D8 File Offset: 0x002C92D8
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17001F0D RID: 7949
			// (get) Token: 0x0600958C RID: 38284 RVA: 0x002C92EC File Offset: 0x002C92EC
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17001F0E RID: 7950
			// (get) Token: 0x0600958D RID: 38285 RVA: 0x002C9300 File Offset: 0x002C9300
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

			// Token: 0x17001F0F RID: 7951
			// (get) Token: 0x0600958E RID: 38286 RVA: 0x002C9328 File Offset: 0x002C9328
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001F10 RID: 7952
			// (get) Token: 0x0600958F RID: 38287 RVA: 0x002C932C File Offset: 0x002C932C
			// (set) Token: 0x06009590 RID: 38288 RVA: 0x002C9334 File Offset: 0x002C9334
			public IComparer<TKey> KeyComparer
			{
				get
				{
					return this._keyComparer;
				}
				set
				{
					Requires.NotNull<IComparer<TKey>>(value, "value");
					if (value != this._keyComparer)
					{
						ImmutableSortedDictionary<TKey, TValue>.Node node = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
						int num = 0;
						foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
						{
							bool flag;
							node = node.Add(keyValuePair.Key, keyValuePair.Value, value, this._valueComparer, out flag);
							if (flag)
							{
								num++;
							}
						}
						this._keyComparer = value;
						this.Root = node;
						this._count = num;
					}
				}
			}

			// Token: 0x17001F11 RID: 7953
			// (get) Token: 0x06009591 RID: 38289 RVA: 0x002C93E0 File Offset: 0x002C93E0
			// (set) Token: 0x06009592 RID: 38290 RVA: 0x002C93E8 File Offset: 0x002C93E8
			public IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._valueComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TValue>>(value, "value");
					if (value != this._valueComparer)
					{
						this._valueComparer = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x06009593 RID: 38291 RVA: 0x002C9410 File Offset: 0x002C9410
			void IDictionary.Add(object key, object value)
			{
				this.Add((TKey)((object)key), (TValue)((object)value));
			}

			// Token: 0x06009594 RID: 38292 RVA: 0x002C9424 File Offset: 0x002C9424
			bool IDictionary.Contains(object key)
			{
				return this.ContainsKey((TKey)((object)key));
			}

			// Token: 0x06009595 RID: 38293 RVA: 0x002C9434 File Offset: 0x002C9434
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
			}

			// Token: 0x06009596 RID: 38294 RVA: 0x002C9448 File Offset: 0x002C9448
			void IDictionary.Remove(object key)
			{
				this.Remove((TKey)((object)key));
			}

			// Token: 0x17001F12 RID: 7954
			object IDictionary.this[object key]
			{
				get
				{
					return this[(TKey)((object)key)];
				}
				set
				{
					this[(TKey)((object)key)] = (TValue)((object)value);
				}
			}

			// Token: 0x06009599 RID: 38297 RVA: 0x002C9480 File Offset: 0x002C9480
			void ICollection.CopyTo(Array array, int index)
			{
				this.Root.CopyTo(array, index, this.Count);
			}

			// Token: 0x0600959A RID: 38298 RVA: 0x002C94A4 File Offset: 0x002C94A4
			public void Add(TKey key, TValue value)
			{
				bool flag;
				this.Root = this.Root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
				if (flag)
				{
					this._count++;
				}
			}

			// Token: 0x0600959B RID: 38299 RVA: 0x002C94EC File Offset: 0x002C94EC
			public bool ContainsKey(TKey key)
			{
				return this.Root.ContainsKey(key, this._keyComparer);
			}

			// Token: 0x0600959C RID: 38300 RVA: 0x002C9500 File Offset: 0x002C9500
			public bool Remove(TKey key)
			{
				bool flag;
				this.Root = this.Root.Remove(key, this._keyComparer, out flag);
				if (flag)
				{
					this._count--;
				}
				return flag;
			}

			// Token: 0x0600959D RID: 38301 RVA: 0x002C9540 File Offset: 0x002C9540
			public bool TryGetValue(TKey key, out TValue value)
			{
				return this.Root.TryGetValue(key, this._keyComparer, out value);
			}

			// Token: 0x0600959E RID: 38302 RVA: 0x002C9558 File Offset: 0x002C9558
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
				return this.Root.TryGetKey(equalKey, this._keyComparer, out actualKey);
			}

			// Token: 0x0600959F RID: 38303 RVA: 0x002C9578 File Offset: 0x002C9578
			public void Add(KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x060095A0 RID: 38304 RVA: 0x002C9590 File Offset: 0x002C9590
			public void Clear()
			{
				this.Root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
				this._count = 0;
			}

			// Token: 0x060095A1 RID: 38305 RVA: 0x002C95A4 File Offset: 0x002C95A4
			public bool Contains(KeyValuePair<TKey, TValue> item)
			{
				return this.Root.Contains(item, this._keyComparer, this._valueComparer);
			}

			// Token: 0x060095A2 RID: 38306 RVA: 0x002C95C0 File Offset: 0x002C95C0
			void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex, this.Count);
			}

			// Token: 0x060095A3 RID: 38307 RVA: 0x002C95E4 File Offset: 0x002C95E4
			public bool Remove(KeyValuePair<TKey, TValue> item)
			{
				return this.Contains(item) && this.Remove(item.Key);
			}

			// Token: 0x060095A4 RID: 38308 RVA: 0x002C9604 File Offset: 0x002C9604
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x060095A5 RID: 38309 RVA: 0x002C9614 File Offset: 0x002C9614
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060095A6 RID: 38310 RVA: 0x002C9624 File Offset: 0x002C9624
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060095A7 RID: 38311 RVA: 0x002C9634 File Offset: 0x002C9634
			public bool ContainsValue(TValue value)
			{
				return this._root.ContainsValue(value, this._valueComparer);
			}

			// Token: 0x060095A8 RID: 38312 RVA: 0x002C9648 File Offset: 0x002C9648
			public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
				foreach (KeyValuePair<TKey, TValue> item in items)
				{
					this.Add(item);
				}
			}

			// Token: 0x060095A9 RID: 38313 RVA: 0x002C96A4 File Offset: 0x002C96A4
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
				foreach (TKey key in keys)
				{
					this.Remove(key);
				}
			}

			// Token: 0x060095AA RID: 38314 RVA: 0x002C9704 File Offset: 0x002C9704
			public TValue GetValueOrDefault(TKey key)
			{
				return this.GetValueOrDefault(key, default(TValue));
			}

			// Token: 0x060095AB RID: 38315 RVA: 0x002C9728 File Offset: 0x002C9728
			public TValue GetValueOrDefault(TKey key, TValue defaultValue)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue result;
				if (this.TryGetValue(key, out result))
				{
					return result;
				}
				return defaultValue;
			}

			// Token: 0x060095AC RID: 38316 RVA: 0x002C9758 File Offset: 0x002C9758
			public ImmutableSortedDictionary<TKey, TValue> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableSortedDictionary<TKey, TValue>.Wrap(this.Root, this._count, this._keyComparer, this._valueComparer);
				}
				return this._immutable;
			}

			// Token: 0x04004C24 RID: 19492
			private ImmutableSortedDictionary<TKey, TValue>.Node _root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;

			// Token: 0x04004C25 RID: 19493
			private IComparer<TKey> _keyComparer = Comparer<TKey>.Default;

			// Token: 0x04004C26 RID: 19494
			private IEqualityComparer<TValue> _valueComparer = EqualityComparer<TValue>.Default;

			// Token: 0x04004C27 RID: 19495
			private int _count;

			// Token: 0x04004C28 RID: 19496
			private ImmutableSortedDictionary<TKey, TValue> _immutable;

			// Token: 0x04004C29 RID: 19497
			private int _version;

			// Token: 0x04004C2A RID: 19498
			private object _syncRoot;
		}

		// Token: 0x020011B3 RID: 4531
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, ISecurePooledObjectUser
		{
			// Token: 0x060095AD RID: 38317 RVA: 0x002C9790 File Offset: 0x002C9790
			internal Enumerator(ImmutableSortedDictionary<TKey, TValue>.Node root, ImmutableSortedDictionary<TKey, TValue>.Builder builder = null)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(root, "root");
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!this._root.IsEmpty)
				{
					if (!ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
					{
						this._stack = ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>(root.Height));
					}
					this.PushLeft(this._root);
				}
			}

			// Token: 0x17001F13 RID: 7955
			// (get) Token: 0x060095AE RID: 38318 RVA: 0x002C9844 File Offset: 0x002C9844
			public KeyValuePair<TKey, TValue> Current
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

			// Token: 0x17001F14 RID: 7956
			// (get) Token: 0x060095AF RID: 38319 RVA: 0x002C9868 File Offset: 0x002C9868
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17001F15 RID: 7957
			// (get) Token: 0x060095B0 RID: 38320 RVA: 0x002C9870 File Offset: 0x002C9870
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060095B1 RID: 38321 RVA: 0x002C9880 File Offset: 0x002C9880
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>();
					ImmutableSortedDictionary<TKey, TValue>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x060095B2 RID: 38322 RVA: 0x002C98E0 File Offset: 0x002C98E0
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
					if (stack.Count > 0)
					{
						ImmutableSortedDictionary<TKey, TValue>.Node value = stack.Pop().Value;
						this._current = value;
						this.PushLeft(value.Right);
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x060095B3 RID: 38323 RVA: 0x002C994C File Offset: 0x002C994C
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._current = null;
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>();
					this.PushLeft(this._root);
				}
			}

			// Token: 0x060095B4 RID: 38324 RVA: 0x002C99B8 File Offset: 0x002C99B8
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(this);
				}
			}

			// Token: 0x060095B5 RID: 38325 RVA: 0x002C99EC File Offset: 0x002C99EC
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(System.Collections.Immutable2448884.SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x060095B6 RID: 38326 RVA: 0x002C9A1C File Offset: 0x002C9A1C
			private void PushLeft(ImmutableSortedDictionary<TKey, TValue>.Node node)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(node, "node");
				Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>(node));
					node = node.Left;
				}
			}

			// Token: 0x04004C2B RID: 19499
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator>();

			// Token: 0x04004C2C RID: 19500
			private readonly ImmutableSortedDictionary<TKey, TValue>.Builder _builder;

			// Token: 0x04004C2D RID: 19501
			private readonly int _poolUserId;

			// Token: 0x04004C2E RID: 19502
			private ImmutableSortedDictionary<TKey, TValue>.Node _root;

			// Token: 0x04004C2F RID: 19503
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>> _stack;

			// Token: 0x04004C30 RID: 19504
			private ImmutableSortedDictionary<TKey, TValue>.Node _current;

			// Token: 0x04004C31 RID: 19505
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x020011B4 RID: 4532
		[DebuggerDisplay("{_key} = {_value}")]
		internal sealed class Node : IBinaryTree<KeyValuePair<TKey, TValue>>, IBinaryTree, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
		{
			// Token: 0x060095B8 RID: 38328 RVA: 0x002C9A74 File Offset: 0x002C9A74
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x060095B9 RID: 38329 RVA: 0x002C9A84 File Offset: 0x002C9A84
			private Node(TKey key, TValue value, ImmutableSortedDictionary<TKey, TValue>.Node left, ImmutableSortedDictionary<TKey, TValue>.Node right, bool frozen = false)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(left, "left");
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(right, "right");
				this._key = key;
				this._value = value;
				this._left = left;
				this._right = right;
				this._height = checked(1 + Math.Max(left._height, right._height));
				this._frozen = frozen;
			}

			// Token: 0x17001F16 RID: 7958
			// (get) Token: 0x060095BA RID: 38330 RVA: 0x002C9B00 File Offset: 0x002C9B00
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17001F17 RID: 7959
			// (get) Token: 0x060095BB RID: 38331 RVA: 0x002C9B0C File Offset: 0x002C9B0C
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<!0, !1>>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17001F18 RID: 7960
			// (get) Token: 0x060095BC RID: 38332 RVA: 0x002C9B14 File Offset: 0x002C9B14
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<!0, !1>>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17001F19 RID: 7961
			// (get) Token: 0x060095BD RID: 38333 RVA: 0x002C9B1C File Offset: 0x002C9B1C
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17001F1A RID: 7962
			// (get) Token: 0x060095BE RID: 38334 RVA: 0x002C9B24 File Offset: 0x002C9B24
			public ImmutableSortedDictionary<TKey, TValue>.Node Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17001F1B RID: 7963
			// (get) Token: 0x060095BF RID: 38335 RVA: 0x002C9B2C File Offset: 0x002C9B2C
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17001F1C RID: 7964
			// (get) Token: 0x060095C0 RID: 38336 RVA: 0x002C9B34 File Offset: 0x002C9B34
			public ImmutableSortedDictionary<TKey, TValue>.Node Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17001F1D RID: 7965
			// (get) Token: 0x060095C1 RID: 38337 RVA: 0x002C9B3C File Offset: 0x002C9B3C
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17001F1E RID: 7966
			// (get) Token: 0x060095C2 RID: 38338 RVA: 0x002C9B44 File Offset: 0x002C9B44
			public KeyValuePair<TKey, TValue> Value
			{
				get
				{
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17001F1F RID: 7967
			// (get) Token: 0x060095C3 RID: 38339 RVA: 0x002C9B58 File Offset: 0x002C9B58
			int IBinaryTree.Count
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17001F20 RID: 7968
			// (get) Token: 0x060095C4 RID: 38340 RVA: 0x002C9B60 File Offset: 0x002C9B60
			internal IEnumerable<TKey> Keys
			{
				get
				{
					return from p in this
					select p.Key;
				}
			}

			// Token: 0x17001F21 RID: 7969
			// (get) Token: 0x060095C5 RID: 38341 RVA: 0x002C9B8C File Offset: 0x002C9B8C
			internal IEnumerable<TValue> Values
			{
				get
				{
					return from p in this
					select p.Value;
				}
			}

			// Token: 0x060095C6 RID: 38342 RVA: 0x002C9BB8 File Offset: 0x002C9BB8
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, null);
			}

			// Token: 0x060095C7 RID: 38343 RVA: 0x002C9BC4 File Offset: 0x002C9BC4
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060095C8 RID: 38344 RVA: 0x002C9BD4 File Offset: 0x002C9BD4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060095C9 RID: 38345 RVA: 0x002C9BE4 File Offset: 0x002C9BE4
			internal ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator(ImmutableSortedDictionary<TKey, TValue>.Builder builder)
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, builder);
			}

			// Token: 0x060095CA RID: 38346 RVA: 0x002C9BF0 File Offset: 0x002C9BF0
			internal void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex, int dictionarySize)
			{
				Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + dictionarySize, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}

			// Token: 0x060095CB RID: 38347 RVA: 0x002C9C80 File Offset: 0x002C9C80
			internal void CopyTo(Array array, int arrayIndex, int dictionarySize)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + dictionarySize, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), arrayIndex++);
				}
			}

			// Token: 0x060095CC RID: 38348 RVA: 0x002C9D34 File Offset: 0x002C9D34
			internal static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromSortedDictionary(SortedDictionary<TKey, TValue> dictionary)
			{
				Requires.NotNull<SortedDictionary<TKey, TValue>>(dictionary, "dictionary");
				IOrderedCollection<KeyValuePair<TKey, TValue>> orderedCollection = dictionary.AsOrderedCollection<KeyValuePair<TKey, TValue>>();
				return ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			}

			// Token: 0x060095CD RID: 38349 RVA: 0x002C9D64 File Offset: 0x002C9D64
			internal ImmutableSortedDictionary<TKey, TValue>.Node Add(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				bool flag;
				return this.SetOrAdd(key, value, keyComparer, valueComparer, false, out flag, out mutated);
			}

			// Token: 0x060095CE RID: 38350 RVA: 0x002C9DA8 File Offset: 0x002C9DA8
			internal ImmutableSortedDictionary<TKey, TValue>.Node SetItem(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				return this.SetOrAdd(key, value, keyComparer, valueComparer, true, out replacedExistingValue, out mutated);
			}

			// Token: 0x060095CF RID: 38351 RVA: 0x002C9DEC File Offset: 0x002C9DEC
			internal ImmutableSortedDictionary<TKey, TValue>.Node Remove(TKey key, IComparer<TKey> keyComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return this.RemoveRecursive(key, keyComparer, out mutated);
			}

			// Token: 0x060095D0 RID: 38352 RVA: 0x002C9E10 File Offset: 0x002C9E10
			[return: System.Collections.Immutable.IsReadOnly]
			internal ref TValue ValueRef(TKey key, IComparer<TKey> keyComparer)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
				if (node.IsEmpty)
				{
					throw new KeyNotFoundException(System.Collections.Immutable2448884.SR.Format(System.Collections.Immutable2448884.SR.Arg_KeyNotFoundWithKey, key.ToString()));
				}
				return ref node._value;
			}

			// Token: 0x060095D1 RID: 38353 RVA: 0x002C9E70 File Offset: 0x002C9E70
			internal bool TryGetValue(TKey key, IComparer<TKey> keyComparer, out TValue value)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
				if (node.IsEmpty)
				{
					value = default(TValue);
					return false;
				}
				value = node._value;
				return true;
			}

			// Token: 0x060095D2 RID: 38354 RVA: 0x002C9EC4 File Offset: 0x002C9EC4
			internal bool TryGetKey(TKey equalKey, IComparer<TKey> keyComparer, out TKey actualKey)
			{
				Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(equalKey, keyComparer);
				if (node.IsEmpty)
				{
					actualKey = equalKey;
					return false;
				}
				actualKey = node._key;
				return true;
			}

			// Token: 0x060095D3 RID: 38355 RVA: 0x002C9F18 File Offset: 0x002C9F18
			internal bool ContainsKey(TKey key, IComparer<TKey> keyComparer)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return !this.Search(key, keyComparer).IsEmpty;
			}

			// Token: 0x060095D4 RID: 38356 RVA: 0x002C9F40 File Offset: 0x002C9F40
			internal bool ContainsValue(TValue value, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					if (valueComparer.Equals(value, keyValuePair.Value))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060095D5 RID: 38357 RVA: 0x002C9FB8 File Offset: 0x002C9FB8
			internal bool Contains(KeyValuePair<TKey, TValue> pair, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNullAllowStructs<TKey>(pair.Key, "Key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(pair.Key, keyComparer);
				return !node.IsEmpty && valueComparer.Equals(node._value, pair.Value);
			}

			// Token: 0x060095D6 RID: 38358 RVA: 0x002CA020 File Offset: 0x002CA020
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x060095D7 RID: 38359 RVA: 0x002CA04C File Offset: 0x002CA04C
			private static ImmutableSortedDictionary<TKey, TValue>.Node RotateLeft(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node right = tree._right;
				return right.Mutate(tree.Mutate(null, right._left), null);
			}

			// Token: 0x060095D8 RID: 38360 RVA: 0x002CA098 File Offset: 0x002CA098
			private static ImmutableSortedDictionary<TKey, TValue>.Node RotateRight(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node left = tree._left;
				return left.Mutate(null, tree.Mutate(left._right, null));
			}

			// Token: 0x060095D9 RID: 38361 RVA: 0x002CA0E4 File Offset: 0x002CA0E4
			private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleLeft(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node tree2 = tree.Mutate(null, ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree._right));
				return ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree2);
			}

			// Token: 0x060095DA RID: 38362 RVA: 0x002CA12C File Offset: 0x002CA12C
			private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleRight(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node tree2 = tree.Mutate(ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree._left), null);
				return ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree2);
			}

			// Token: 0x060095DB RID: 38363 RVA: 0x002CA174 File Offset: 0x002CA174
			private static int Balance(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x060095DC RID: 38364 RVA: 0x002CA198 File Offset: 0x002CA198
			private static bool IsRightHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) >= 2;
			}

			// Token: 0x060095DD RID: 38365 RVA: 0x002CA1B4 File Offset: 0x002CA1B4
			private static bool IsLeftHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) <= -2;
			}

			// Token: 0x060095DE RID: 38366 RVA: 0x002CA1D0 File Offset: 0x002CA1D0
			private static ImmutableSortedDictionary<TKey, TValue>.Node MakeBalanced(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (ImmutableSortedDictionary<TKey, TValue>.Node.IsRightHeavy(tree))
				{
					if (ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree._right) >= 0)
					{
						return ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree);
					}
					return ImmutableSortedDictionary<TKey, TValue>.Node.DoubleLeft(tree);
				}
				else
				{
					if (!ImmutableSortedDictionary<TKey, TValue>.Node.IsLeftHeavy(tree))
					{
						return tree;
					}
					if (ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree._left) <= 0)
					{
						return ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree);
					}
					return ImmutableSortedDictionary<TKey, TValue>.Node.DoubleRight(tree);
				}
			}

			// Token: 0x060095DF RID: 38367 RVA: 0x002CA244 File Offset: 0x002CA244
			private static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromList(IOrderedCollection<KeyValuePair<TKey, TValue>> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<KeyValuePair<TKey, TValue>>>(items, "items");
				Requires.Range(start >= 0, "start", null);
				Requires.Range(length >= 0, "length", null);
				if (length == 0)
				{
					return ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableSortedDictionary<TKey, TValue>.Node left = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start, num2);
				ImmutableSortedDictionary<TKey, TValue>.Node right = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				KeyValuePair<TKey, TValue> keyValuePair = items[start + num2];
				return new ImmutableSortedDictionary<TKey, TValue>.Node(keyValuePair.Key, keyValuePair.Value, left, right, true);
			}

			// Token: 0x060095E0 RID: 38368 RVA: 0x002CA2D4 File Offset: 0x002CA2D4
			private ImmutableSortedDictionary<TKey, TValue>.Node SetOrAdd(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, bool overwriteExistingValue, out bool replacedExistingValue, out bool mutated)
			{
				replacedExistingValue = false;
				if (this.IsEmpty)
				{
					mutated = true;
					return new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this, this, false);
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = this;
				int num = keyComparer.Compare(key, this._key);
				if (num > 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node right = this._right.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node left = this._left.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				else
				{
					if (valueComparer.Equals(this._value, value))
					{
						mutated = false;
						return this;
					}
					if (!overwriteExistingValue)
					{
						throw new ArgumentException(System.Collections.Immutable2448884.SR.Format(System.Collections.Immutable2448884.SR.DuplicateKey, key));
					}
					mutated = true;
					replacedExistingValue = true;
					node = new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this._left, this._right, false);
				}
				if (!mutated)
				{
					return node;
				}
				return ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(node);
			}

			// Token: 0x060095E1 RID: 38369 RVA: 0x002CA3E4 File Offset: 0x002CA3E4
			private ImmutableSortedDictionary<TKey, TValue>.Node RemoveRecursive(TKey key, IComparer<TKey> keyComparer, out bool mutated)
			{
				if (this.IsEmpty)
				{
					mutated = false;
					return this;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = this;
				int num = keyComparer.Compare(key, this._key);
				if (num == 0)
				{
					mutated = true;
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
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
						ImmutableSortedDictionary<TKey, TValue>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						bool flag;
						ImmutableSortedDictionary<TKey, TValue>.Node right = this._right.Remove(node2._key, keyComparer, out flag);
						node = node2.Mutate(this._left, right);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node left = this._left.Remove(key, keyComparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				else
				{
					ImmutableSortedDictionary<TKey, TValue>.Node right2 = this._right.Remove(key, keyComparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right2);
					}
				}
				if (!node.IsEmpty)
				{
					return ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(node);
				}
				return node;
			}

			// Token: 0x060095E2 RID: 38370 RVA: 0x002CA54C File Offset: 0x002CA54C
			private ImmutableSortedDictionary<TKey, TValue>.Node Mutate(ImmutableSortedDictionary<TKey, TValue>.Node left = null, ImmutableSortedDictionary<TKey, TValue>.Node right = null)
			{
				if (this._frozen)
				{
					return new ImmutableSortedDictionary<TKey, TValue>.Node(this._key, this._value, left ?? this._left, right ?? this._right, false);
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
				return this;
			}

			// Token: 0x060095E3 RID: 38371 RVA: 0x002CA5D8 File Offset: 0x002CA5D8
			private ImmutableSortedDictionary<TKey, TValue>.Node Search(TKey key, IComparer<TKey> keyComparer)
			{
				if (this.IsEmpty)
				{
					return this;
				}
				int num = keyComparer.Compare(key, this._key);
				if (num == 0)
				{
					return this;
				}
				if (num > 0)
				{
					return this._right.Search(key, keyComparer);
				}
				return this._left.Search(key, keyComparer);
			}

			// Token: 0x04004C32 RID: 19506
			internal static readonly ImmutableSortedDictionary<TKey, TValue>.Node EmptyNode = new ImmutableSortedDictionary<TKey, TValue>.Node();

			// Token: 0x04004C33 RID: 19507
			private readonly TKey _key;

			// Token: 0x04004C34 RID: 19508
			private readonly TValue _value;

			// Token: 0x04004C35 RID: 19509
			private bool _frozen;

			// Token: 0x04004C36 RID: 19510
			private byte _height;

			// Token: 0x04004C37 RID: 19511
			private ImmutableSortedDictionary<TKey, TValue>.Node _left;

			// Token: 0x04004C38 RID: 19512
			private ImmutableSortedDictionary<TKey, TValue>.Node _right;
		}
	}
}
