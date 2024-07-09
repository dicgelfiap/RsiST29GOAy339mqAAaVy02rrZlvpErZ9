using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x02000CA9 RID: 3241
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableDictionaryDebuggerProxy<, >))]
	[ComVisible(true)]
	public sealed class ImmutableDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IImmutableDictionaryInternal<TKey, TValue>, IHashKeyCollection<TKey>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
	{
		// Token: 0x060081FE RID: 33278 RVA: 0x00264490 File Offset: 0x00264490
		private ImmutableDictionary(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers, int count) : this(Requires.NotNullPassthrough<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers"))
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
			root.Freeze(ImmutableDictionary<TKey, TValue>.s_FreezeBucketAction);
			this._root = root;
			this._count = count;
		}

		// Token: 0x060081FF RID: 33279 RVA: 0x002644C8 File Offset: 0x002644C8
		private ImmutableDictionary(ImmutableDictionary<TKey, TValue>.Comparers comparers = null)
		{
			this._comparers = (comparers ?? ImmutableDictionary<TKey, TValue>.Comparers.Get(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default));
			this._root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;
		}

		// Token: 0x06008200 RID: 33280 RVA: 0x002644F8 File Offset: 0x002644F8
		public ImmutableDictionary<TKey, TValue> Clear()
		{
			if (!this.IsEmpty)
			{
				return ImmutableDictionary<TKey, TValue>.EmptyWithComparers(this._comparers);
			}
			return this;
		}

		// Token: 0x17001C11 RID: 7185
		// (get) Token: 0x06008201 RID: 33281 RVA: 0x00264514 File Offset: 0x00264514
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17001C12 RID: 7186
		// (get) Token: 0x06008202 RID: 33282 RVA: 0x0026451C File Offset: 0x0026451C
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17001C13 RID: 7187
		// (get) Token: 0x06008203 RID: 33283 RVA: 0x00264528 File Offset: 0x00264528
		public IEqualityComparer<TKey> KeyComparer
		{
			get
			{
				return this._comparers.KeyComparer;
			}
		}

		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x06008204 RID: 33284 RVA: 0x00264538 File Offset: 0x00264538
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this._comparers.ValueComparer;
			}
		}

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x06008205 RID: 33285 RVA: 0x00264548 File Offset: 0x00264548
		public IEnumerable<TKey> Keys
		{
			get
			{
				foreach (KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair in this._root)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair2 in keyValuePair.Value)
					{
						yield return keyValuePair2.Key;
					}
					ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator enumerator2 = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				}
				SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator enumerator = default(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x06008206 RID: 33286 RVA: 0x0026456C File Offset: 0x0026456C
		public IEnumerable<TValue> Values
		{
			get
			{
				foreach (KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair in this._root)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair2 in keyValuePair.Value)
					{
						yield return keyValuePair2.Value;
					}
					ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator enumerator2 = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				}
				SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator enumerator = default(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06008207 RID: 33287 RVA: 0x00264590 File Offset: 0x00264590
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x06008208 RID: 33288 RVA: 0x00264598 File Offset: 0x00264598
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x06008209 RID: 33289 RVA: 0x002645A0 File Offset: 0x002645A0
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x0600820A RID: 33290 RVA: 0x002645A8 File Offset: 0x002645A8
		private ImmutableDictionary<TKey, TValue>.MutationInput Origin
		{
			get
			{
				return new ImmutableDictionary<TKey, TValue>.MutationInput(this);
			}
		}

		// Token: 0x17001C1A RID: 7194
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
				throw new KeyNotFoundException();
			}
		}

		// Token: 0x17001C1B RID: 7195
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

		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x0600820E RID: 33294 RVA: 0x002645F8 File Offset: 0x002645F8
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600820F RID: 33295 RVA: 0x002645FC File Offset: 0x002645FC
		public ImmutableDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableDictionary<TKey, TValue>.Builder(this);
		}

		// Token: 0x06008210 RID: 33296 RVA: 0x00264604 File Offset: 0x00264604
		public ImmutableDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, this.Origin).Finalize(this);
		}

		// Token: 0x06008211 RID: 33297 RVA: 0x00264638 File Offset: 0x00264638
		public ImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(pairs, "pairs");
			return this.AddRange(pairs, false);
		}

		// Token: 0x06008212 RID: 33298 RVA: 0x00264650 File Offset: 0x00264650
		public ImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue, this.Origin).Finalize(this);
		}

		// Token: 0x06008213 RID: 33299 RVA: 0x00264684 File Offset: 0x00264684
		public ImmutableDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue).Finalize(this);
		}

		// Token: 0x06008214 RID: 33300 RVA: 0x002646B8 File Offset: 0x002646B8
		public ImmutableDictionary<TKey, TValue> Remove(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Remove(key, this.Origin).Finalize(this);
		}

		// Token: 0x06008215 RID: 33301 RVA: 0x002646EC File Offset: 0x002646EC
		public ImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
		{
			Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
			int num = this._count;
			SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> sortedInt32KeyNode = this._root;
			foreach (TKey tkey in keys)
			{
				int hashCode = this.KeyComparer.GetHashCode(tkey);
				ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
				if (sortedInt32KeyNode.TryGetValue(hashCode, out hashBucket))
				{
					ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
					ImmutableDictionary<TKey, TValue>.HashBucket newBucket = hashBucket.Remove(tkey, this._comparers.KeyOnlyComparer, out operationResult);
					sortedInt32KeyNode = ImmutableDictionary<TKey, TValue>.UpdateRoot(sortedInt32KeyNode, hashCode, newBucket, this._comparers.HashBucketEqualityComparer);
					if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged)
					{
						num--;
					}
				}
			}
			return this.Wrap(sortedInt32KeyNode, num);
		}

		// Token: 0x06008216 RID: 33302 RVA: 0x002647B0 File Offset: 0x002647B0
		public bool ContainsKey(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.ContainsKey(key, this.Origin);
		}

		// Token: 0x06008217 RID: 33303 RVA: 0x002647CC File Offset: 0x002647CC
		public bool Contains(KeyValuePair<TKey, TValue> pair)
		{
			return ImmutableDictionary<TKey, TValue>.Contains(pair, this.Origin);
		}

		// Token: 0x06008218 RID: 33304 RVA: 0x002647DC File Offset: 0x002647DC
		public bool TryGetValue(TKey key, out TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
		}

		// Token: 0x06008219 RID: 33305 RVA: 0x002647F8 File Offset: 0x002647F8
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
			return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
		}

		// Token: 0x0600821A RID: 33306 RVA: 0x00264814 File Offset: 0x00264814
		public ImmutableDictionary<TKey, TValue> WithComparers(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if (keyComparer == null)
			{
				keyComparer = EqualityComparer<TKey>.Default;
			}
			if (valueComparer == null)
			{
				valueComparer = EqualityComparer<TValue>.Default;
			}
			if (this.KeyComparer != keyComparer)
			{
				ImmutableDictionary<TKey, TValue>.Comparers comparers = ImmutableDictionary<TKey, TValue>.Comparers.Get(keyComparer, valueComparer);
				ImmutableDictionary<TKey, TValue> immutableDictionary = new ImmutableDictionary<TKey, TValue>(comparers);
				return immutableDictionary.AddRange(this, true);
			}
			if (this.ValueComparer == valueComparer)
			{
				return this;
			}
			ImmutableDictionary<TKey, TValue>.Comparers comparers2 = this._comparers.WithValueComparer(valueComparer);
			return new ImmutableDictionary<TKey, TValue>(this._root, comparers2, this._count);
		}

		// Token: 0x0600821B RID: 33307 RVA: 0x00264894 File Offset: 0x00264894
		public ImmutableDictionary<TKey, TValue> WithComparers(IEqualityComparer<TKey> keyComparer)
		{
			return this.WithComparers(keyComparer, this._comparers.ValueComparer);
		}

		// Token: 0x0600821C RID: 33308 RVA: 0x002648A8 File Offset: 0x002648A8
		public bool ContainsValue(TValue value)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				if (this.ValueComparer.Equals(value, keyValuePair.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600821D RID: 33309 RVA: 0x00264918 File Offset: 0x00264918
		public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new ImmutableDictionary<TKey, TValue>.Enumerator(this._root, null);
		}

		// Token: 0x0600821E RID: 33310 RVA: 0x00264928 File Offset: 0x00264928
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x0600821F RID: 33311 RVA: 0x00264934 File Offset: 0x00264934
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x06008220 RID: 33312 RVA: 0x00264940 File Offset: 0x00264940
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x06008221 RID: 33313 RVA: 0x0026494C File Offset: 0x0026494C
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x06008222 RID: 33314 RVA: 0x00264958 File Offset: 0x00264958
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x06008223 RID: 33315 RVA: 0x00264964 File Offset: 0x00264964
		[ExcludeFromCodeCoverage]
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		// Token: 0x06008224 RID: 33316 RVA: 0x00264970 File Offset: 0x00264970
		void IDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008225 RID: 33317 RVA: 0x00264978 File Offset: 0x00264978
		bool IDictionary<!0, !1>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008226 RID: 33318 RVA: 0x00264980 File Offset: 0x00264980
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008227 RID: 33319 RVA: 0x00264988 File Offset: 0x00264988
		void ICollection<KeyValuePair<!0, !1>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008228 RID: 33320 RVA: 0x00264990 File Offset: 0x00264990
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008229 RID: 33321 RVA: 0x00264998 File Offset: 0x00264998
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

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x0600822A RID: 33322 RVA: 0x00264A2C File Offset: 0x00264A2C
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x0600822B RID: 33323 RVA: 0x00264A30 File Offset: 0x00264A30
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x0600822C RID: 33324 RVA: 0x00264A34 File Offset: 0x00264A34
		ICollection IDictionary.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x0600822D RID: 33325 RVA: 0x00264A3C File Offset: 0x00264A3C
		ICollection IDictionary.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x0600822E RID: 33326 RVA: 0x00264A44 File Offset: 0x00264A44
		internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x0600822F RID: 33327 RVA: 0x00264A4C File Offset: 0x00264A4C
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008230 RID: 33328 RVA: 0x00264A54 File Offset: 0x00264A54
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06008231 RID: 33329 RVA: 0x00264A64 File Offset: 0x00264A64
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		// Token: 0x06008232 RID: 33330 RVA: 0x00264A78 File Offset: 0x00264A78
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001C22 RID: 7202
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

		// Token: 0x06008235 RID: 33333 RVA: 0x00264A9C File Offset: 0x00264A9C
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008236 RID: 33334 RVA: 0x00264AA4 File Offset: 0x00264AA4
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), arrayIndex++);
			}
		}

		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x06008237 RID: 33335 RVA: 0x00264B5C File Offset: 0x00264B5C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x06008238 RID: 33336 RVA: 0x00264B60 File Offset: 0x00264B60
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008239 RID: 33337 RVA: 0x00264B64 File Offset: 0x00264B64
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
		}

		// Token: 0x0600823A RID: 33338 RVA: 0x00264B98 File Offset: 0x00264B98
		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600823B RID: 33339 RVA: 0x00264BA8 File Offset: 0x00264BA8
		private static ImmutableDictionary<TKey, TValue> EmptyWithComparers(ImmutableDictionary<TKey, TValue>.Comparers comparers)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers");
			if (ImmutableDictionary<TKey, TValue>.Empty._comparers != comparers)
			{
				return new ImmutableDictionary<TKey, TValue>(comparers);
			}
			return ImmutableDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x0600823C RID: 33340 RVA: 0x00264BD4 File Offset: 0x00264BD4
		private static bool TryCastToImmutableMap(IEnumerable<KeyValuePair<TKey, TValue>> sequence, out ImmutableDictionary<TKey, TValue> other)
		{
			other = (sequence as ImmutableDictionary<TKey, TValue>);
			if (other != null)
			{
				return true;
			}
			ImmutableDictionary<TKey, TValue>.Builder builder = sequence as ImmutableDictionary<TKey, TValue>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x0600823D RID: 33341 RVA: 0x00264C10 File Offset: 0x00264C10
		private static bool ContainsKey(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			TValue tvalue;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.TryGetValue(key, origin.Comparers, out tvalue);
		}

		// Token: 0x0600823E RID: 33342 RVA: 0x00264C58 File Offset: 0x00264C58
		private static bool Contains(KeyValuePair<TKey, TValue> keyValuePair, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(keyValuePair.Key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			TValue x;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.TryGetValue(keyValuePair.Key, origin.Comparers, out x) && origin.ValueComparer.Equals(x, keyValuePair.Value);
		}

		// Token: 0x0600823F RID: 33343 RVA: 0x00264CC8 File Offset: 0x00264CC8
		private static bool TryGetValue(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin, out TValue value)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				return hashBucket.TryGetValue(key, origin.Comparers, out value);
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06008240 RID: 33344 RVA: 0x00264D14 File Offset: 0x00264D14
		private static bool TryGetKey(TKey equalKey, ImmutableDictionary<TKey, TValue>.MutationInput origin, out TKey actualKey)
		{
			int hashCode = origin.KeyComparer.GetHashCode(equalKey);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				return hashBucket.TryGetKey(equalKey, origin.Comparers, out actualKey);
			}
			actualKey = equalKey;
			return false;
		}

		// Token: 0x06008241 RID: 33345 RVA: 0x00264D60 File Offset: 0x00264D60
		private static ImmutableDictionary<TKey, TValue>.MutationResult Add(TKey key, TValue value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior behavior, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
			ImmutableDictionary<TKey, TValue>.HashBucket newBucket = origin.Root.GetValueOrDefault(hashCode).Add(key, value, origin.KeyOnlyComparer, origin.ValueComparer, behavior, out operationResult);
			if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired)
			{
				return new ImmutableDictionary<TKey, TValue>.MutationResult(origin);
			}
			SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root = ImmutableDictionary<TKey, TValue>.UpdateRoot(origin.Root, hashCode, newBucket, origin.HashBucketComparer);
			return new ImmutableDictionary<TKey, TValue>.MutationResult(root, (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged) ? 1 : 0);
		}

		// Token: 0x06008242 RID: 33346 RVA: 0x00264DF0 File Offset: 0x00264DF0
		private static ImmutableDictionary<TKey, TValue>.MutationResult AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items, ImmutableDictionary<TKey, TValue>.MutationInput origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior collisionBehavior = ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			int num = 0;
			SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> sortedInt32KeyNode = origin.Root;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
			{
				int hashCode = origin.KeyComparer.GetHashCode(keyValuePair.Key);
				ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
				ImmutableDictionary<TKey, TValue>.HashBucket newBucket = sortedInt32KeyNode.GetValueOrDefault(hashCode).Add(keyValuePair.Key, keyValuePair.Value, origin.KeyOnlyComparer, origin.ValueComparer, collisionBehavior, out operationResult);
				sortedInt32KeyNode = ImmutableDictionary<TKey, TValue>.UpdateRoot(sortedInt32KeyNode, hashCode, newBucket, origin.HashBucketComparer);
				if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged)
				{
					num++;
				}
			}
			return new ImmutableDictionary<TKey, TValue>.MutationResult(sortedInt32KeyNode, num);
		}

		// Token: 0x06008243 RID: 33347 RVA: 0x00264EBC File Offset: 0x00264EBC
		private static ImmutableDictionary<TKey, TValue>.MutationResult Remove(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
				SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root = ImmutableDictionary<TKey, TValue>.UpdateRoot(origin.Root, hashCode, hashBucket.Remove(key, origin.KeyOnlyComparer, out operationResult), origin.HashBucketComparer);
				return new ImmutableDictionary<TKey, TValue>.MutationResult(root, (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged) ? -1 : 0);
			}
			return new ImmutableDictionary<TKey, TValue>.MutationResult(origin);
		}

		// Token: 0x06008244 RID: 33348 RVA: 0x00264F30 File Offset: 0x00264F30
		private static SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> UpdateRoot(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int hashCode, ImmutableDictionary<TKey, TValue>.HashBucket newBucket, IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> hashBucketComparer)
		{
			bool flag;
			if (newBucket.IsEmpty)
			{
				return root.Remove(hashCode, out flag);
			}
			bool flag2;
			return root.SetItem(hashCode, newBucket, hashBucketComparer, out flag2, out flag);
		}

		// Token: 0x06008245 RID: 33349 RVA: 0x00264F64 File Offset: 0x00264F64
		private static ImmutableDictionary<TKey, TValue> Wrap(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers");
			Requires.Range(count >= 0, "count", null);
			return new ImmutableDictionary<TKey, TValue>(root, comparers, count);
		}

		// Token: 0x06008246 RID: 33350 RVA: 0x00264F98 File Offset: 0x00264F98
		private ImmutableDictionary<TKey, TValue> Wrap(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int adjustedCountIfDifferentRoot)
		{
			if (root == null)
			{
				return this.Clear();
			}
			if (this._root == root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableDictionary<TKey, TValue>(root, this._comparers, adjustedCountIfDifferentRoot);
			}
			return this.Clear();
		}

		// Token: 0x06008247 RID: 33351 RVA: 0x00264FD4 File Offset: 0x00264FD4
		private ImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs, bool avoidToHashMap)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(pairs, "pairs");
			ImmutableDictionary<TKey, TValue> immutableDictionary;
			if (this.IsEmpty && !avoidToHashMap && ImmutableDictionary<TKey, TValue>.TryCastToImmutableMap(pairs, out immutableDictionary))
			{
				return immutableDictionary.WithComparers(this.KeyComparer, this.ValueComparer);
			}
			return ImmutableDictionary<TKey, TValue>.AddRange(pairs, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent).Finalize(this);
		}

		// Token: 0x04003D2E RID: 15662
		public static readonly ImmutableDictionary<TKey, TValue> Empty = new ImmutableDictionary<TKey, TValue>(null);

		// Token: 0x04003D2F RID: 15663
		private static readonly Action<KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket>> s_FreezeBucketAction = delegate(KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> kv)
		{
			kv.Value.Freeze();
		};

		// Token: 0x04003D30 RID: 15664
		private readonly int _count;

		// Token: 0x04003D31 RID: 15665
		private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

		// Token: 0x04003D32 RID: 15666
		private readonly ImmutableDictionary<TKey, TValue>.Comparers _comparers;

		// Token: 0x0200119F RID: 4511
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableDictionaryBuilderDebuggerProxy<, >))]
		public sealed class Builder : IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x06009450 RID: 37968 RVA: 0x002C557C File Offset: 0x002C557C
			internal Builder(ImmutableDictionary<TKey, TValue> map)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(map, "map");
				this._root = map._root;
				this._count = map._count;
				this._comparers = map._comparers;
				this._immutable = map;
			}

			// Token: 0x17001EB4 RID: 7860
			// (get) Token: 0x06009451 RID: 37969 RVA: 0x002C55D4 File Offset: 0x002C55D4
			// (set) Token: 0x06009452 RID: 37970 RVA: 0x002C55E4 File Offset: 0x002C55E4
			public IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._comparers.KeyComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TKey>>(value, "value");
					if (value != this.KeyComparer)
					{
						ImmutableDictionary<TKey, TValue>.Comparers comparers = ImmutableDictionary<TKey, TValue>.Comparers.Get(value, this.ValueComparer);
						ImmutableDictionary<TKey, TValue>.MutationInput origin = new ImmutableDictionary<TKey, TValue>.MutationInput(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode, comparers);
						ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.AddRange(this, origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent);
						this._immutable = null;
						this._comparers = comparers;
						this._count = mutationResult.CountAdjustment;
						this.Root = mutationResult.Root;
					}
				}
			}

			// Token: 0x17001EB5 RID: 7861
			// (get) Token: 0x06009453 RID: 37971 RVA: 0x002C5658 File Offset: 0x002C5658
			// (set) Token: 0x06009454 RID: 37972 RVA: 0x002C5668 File Offset: 0x002C5668
			public IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._comparers.ValueComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TValue>>(value, "value");
					if (value != this.ValueComparer)
					{
						this._comparers = this._comparers.WithValueComparer(value);
						this._immutable = null;
					}
				}
			}

			// Token: 0x17001EB6 RID: 7862
			// (get) Token: 0x06009455 RID: 37973 RVA: 0x002C569C File Offset: 0x002C569C
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17001EB7 RID: 7863
			// (get) Token: 0x06009456 RID: 37974 RVA: 0x002C56A4 File Offset: 0x002C56A4
			bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001EB8 RID: 7864
			// (get) Token: 0x06009457 RID: 37975 RVA: 0x002C56A8 File Offset: 0x002C56A8
			public IEnumerable<TKey> Keys
			{
				get
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
					{
						yield return keyValuePair.Key;
					}
					ImmutableDictionary<TKey, TValue>.Enumerator enumerator = default(ImmutableDictionary<TKey, TValue>.Enumerator);
					yield break;
					yield break;
				}
			}

			// Token: 0x17001EB9 RID: 7865
			// (get) Token: 0x06009458 RID: 37976 RVA: 0x002C56CC File Offset: 0x002C56CC
			ICollection<TKey> IDictionary<!0, !1>.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17001EBA RID: 7866
			// (get) Token: 0x06009459 RID: 37977 RVA: 0x002C56E0 File Offset: 0x002C56E0
			public IEnumerable<TValue> Values
			{
				get
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
					{
						yield return keyValuePair.Value;
					}
					ImmutableDictionary<TKey, TValue>.Enumerator enumerator = default(ImmutableDictionary<TKey, TValue>.Enumerator);
					yield break;
					yield break;
				}
			}

			// Token: 0x17001EBB RID: 7867
			// (get) Token: 0x0600945A RID: 37978 RVA: 0x002C5704 File Offset: 0x002C5704
			ICollection<TValue> IDictionary<!0, !1>.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17001EBC RID: 7868
			// (get) Token: 0x0600945B RID: 37979 RVA: 0x002C5718 File Offset: 0x002C5718
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001EBD RID: 7869
			// (get) Token: 0x0600945C RID: 37980 RVA: 0x002C571C File Offset: 0x002C571C
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001EBE RID: 7870
			// (get) Token: 0x0600945D RID: 37981 RVA: 0x002C5720 File Offset: 0x002C5720
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17001EBF RID: 7871
			// (get) Token: 0x0600945E RID: 37982 RVA: 0x002C5734 File Offset: 0x002C5734
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17001EC0 RID: 7872
			// (get) Token: 0x0600945F RID: 37983 RVA: 0x002C5748 File Offset: 0x002C5748
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

			// Token: 0x17001EC1 RID: 7873
			// (get) Token: 0x06009460 RID: 37984 RVA: 0x002C5770 File Offset: 0x002C5770
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06009461 RID: 37985 RVA: 0x002C5774 File Offset: 0x002C5774
			void IDictionary.Add(object key, object value)
			{
				this.Add((TKey)((object)key), (TValue)((object)value));
			}

			// Token: 0x06009462 RID: 37986 RVA: 0x002C5788 File Offset: 0x002C5788
			bool IDictionary.Contains(object key)
			{
				return this.ContainsKey((TKey)((object)key));
			}

			// Token: 0x06009463 RID: 37987 RVA: 0x002C5798 File Offset: 0x002C5798
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
			}

			// Token: 0x06009464 RID: 37988 RVA: 0x002C57AC File Offset: 0x002C57AC
			void IDictionary.Remove(object key)
			{
				this.Remove((TKey)((object)key));
			}

			// Token: 0x17001EC2 RID: 7874
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

			// Token: 0x06009467 RID: 37991 RVA: 0x002C57E4 File Offset: 0x002C57E4
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), arrayIndex++);
				}
			}

			// Token: 0x17001EC3 RID: 7875
			// (get) Token: 0x06009468 RID: 37992 RVA: 0x002C589C File Offset: 0x002C589C
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17001EC4 RID: 7876
			// (get) Token: 0x06009469 RID: 37993 RVA: 0x002C58A4 File Offset: 0x002C58A4
			private ImmutableDictionary<TKey, TValue>.MutationInput Origin
			{
				get
				{
					return new ImmutableDictionary<TKey, TValue>.MutationInput(this.Root, this._comparers);
				}
			}

			// Token: 0x17001EC5 RID: 7877
			// (get) Token: 0x0600946A RID: 37994 RVA: 0x002C58B8 File Offset: 0x002C58B8
			// (set) Token: 0x0600946B RID: 37995 RVA: 0x002C58C0 File Offset: 0x002C58C0
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
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

			// Token: 0x17001EC6 RID: 7878
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
					ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue, this.Origin);
					this.Apply(result);
				}
			}

			// Token: 0x0600946E RID: 37998 RVA: 0x002C5954 File Offset: 0x002C5954
			public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent);
				this.Apply(result);
			}

			// Token: 0x0600946F RID: 37999 RVA: 0x002C597C File Offset: 0x002C597C
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
				foreach (TKey key in keys)
				{
					this.Remove(key);
				}
			}

			// Token: 0x06009470 RID: 38000 RVA: 0x002C59DC File Offset: 0x002C59DC
			public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableDictionary<TKey, TValue>.Enumerator(this._root, this);
			}

			// Token: 0x06009471 RID: 38001 RVA: 0x002C59EC File Offset: 0x002C59EC
			public TValue GetValueOrDefault(TKey key)
			{
				return this.GetValueOrDefault(key, default(TValue));
			}

			// Token: 0x06009472 RID: 38002 RVA: 0x002C5A10 File Offset: 0x002C5A10
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

			// Token: 0x06009473 RID: 38003 RVA: 0x002C5A40 File Offset: 0x002C5A40
			public ImmutableDictionary<TKey, TValue> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableDictionary<TKey, TValue>.Wrap(this._root, this._comparers, this._count);
				}
				return this._immutable;
			}

			// Token: 0x06009474 RID: 38004 RVA: 0x002C5A70 File Offset: 0x002C5A70
			public void Add(TKey key, TValue value)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, this.Origin);
				this.Apply(result);
			}

			// Token: 0x06009475 RID: 38005 RVA: 0x002C5A98 File Offset: 0x002C5A98
			public bool ContainsKey(TKey key)
			{
				return ImmutableDictionary<TKey, TValue>.ContainsKey(key, this.Origin);
			}

			// Token: 0x06009476 RID: 38006 RVA: 0x002C5AA8 File Offset: 0x002C5AA8
			public bool ContainsValue(TValue value)
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					if (this.ValueComparer.Equals(value, keyValuePair.Value))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06009477 RID: 38007 RVA: 0x002C5B18 File Offset: 0x002C5B18
			public bool Remove(TKey key)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.Remove(key, this.Origin);
				return this.Apply(result);
			}

			// Token: 0x06009478 RID: 38008 RVA: 0x002C5B40 File Offset: 0x002C5B40
			public bool TryGetValue(TKey key, out TValue value)
			{
				return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
			}

			// Token: 0x06009479 RID: 38009 RVA: 0x002C5B50 File Offset: 0x002C5B50
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
			}

			// Token: 0x0600947A RID: 38010 RVA: 0x002C5B60 File Offset: 0x002C5B60
			public void Add(KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x0600947B RID: 38011 RVA: 0x002C5B78 File Offset: 0x002C5B78
			public void Clear()
			{
				this.Root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;
				this._count = 0;
			}

			// Token: 0x0600947C RID: 38012 RVA: 0x002C5B8C File Offset: 0x002C5B8C
			public bool Contains(KeyValuePair<TKey, TValue> item)
			{
				return ImmutableDictionary<TKey, TValue>.Contains(item, this.Origin);
			}

			// Token: 0x0600947D RID: 38013 RVA: 0x002C5B9C File Offset: 0x002C5B9C
			void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}

			// Token: 0x0600947E RID: 38014 RVA: 0x002C5C04 File Offset: 0x002C5C04
			public bool Remove(KeyValuePair<TKey, TValue> item)
			{
				return this.Contains(item) && this.Remove(item.Key);
			}

			// Token: 0x0600947F RID: 38015 RVA: 0x002C5C24 File Offset: 0x002C5C24
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009480 RID: 38016 RVA: 0x002C5C34 File Offset: 0x002C5C34
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009481 RID: 38017 RVA: 0x002C5C44 File Offset: 0x002C5C44
			private bool Apply(ImmutableDictionary<TKey, TValue>.MutationResult result)
			{
				this.Root = result.Root;
				this._count += result.CountAdjustment;
				return result.CountAdjustment != 0;
			}

			// Token: 0x04004BD9 RID: 19417
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;

			// Token: 0x04004BDA RID: 19418
			private ImmutableDictionary<TKey, TValue>.Comparers _comparers;

			// Token: 0x04004BDB RID: 19419
			private int _count;

			// Token: 0x04004BDC RID: 19420
			private ImmutableDictionary<TKey, TValue> _immutable;

			// Token: 0x04004BDD RID: 19421
			private int _version;

			// Token: 0x04004BDE RID: 19422
			private object _syncRoot;
		}

		// Token: 0x020011A0 RID: 4512
		internal sealed class Comparers : IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket>, IEqualityComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x06009482 RID: 38018 RVA: 0x002C5C80 File Offset: 0x002C5C80
			internal Comparers(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				this._keyComparer = keyComparer;
				this._valueComparer = valueComparer;
			}

			// Token: 0x17001EC7 RID: 7879
			// (get) Token: 0x06009483 RID: 38019 RVA: 0x002C5CAC File Offset: 0x002C5CAC
			internal IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._keyComparer;
				}
			}

			// Token: 0x17001EC8 RID: 7880
			// (get) Token: 0x06009484 RID: 38020 RVA: 0x002C5CB4 File Offset: 0x002C5CB4
			internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001EC9 RID: 7881
			// (get) Token: 0x06009485 RID: 38021 RVA: 0x002C5CB8 File Offset: 0x002C5CB8
			internal IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._valueComparer;
				}
			}

			// Token: 0x17001ECA RID: 7882
			// (get) Token: 0x06009486 RID: 38022 RVA: 0x002C5CC0 File Offset: 0x002C5CC0
			internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketEqualityComparer
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06009487 RID: 38023 RVA: 0x002C5CC4 File Offset: 0x002C5CC4
			public bool Equals(ImmutableDictionary<TKey, TValue>.HashBucket x, ImmutableDictionary<TKey, TValue>.HashBucket y)
			{
				return x.AdditionalElements == y.AdditionalElements && this.KeyComparer.Equals(x.FirstValue.Key, y.FirstValue.Key) && this.ValueComparer.Equals(x.FirstValue.Value, y.FirstValue.Value);
			}

			// Token: 0x06009488 RID: 38024 RVA: 0x002C5D44 File Offset: 0x002C5D44
			public int GetHashCode(ImmutableDictionary<TKey, TValue>.HashBucket obj)
			{
				return this.KeyComparer.GetHashCode(obj.FirstValue.Key);
			}

			// Token: 0x06009489 RID: 38025 RVA: 0x002C5D70 File Offset: 0x002C5D70
			bool IEqualityComparer<KeyValuePair<!0, !1>>.Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this._keyComparer.Equals(x.Key, y.Key);
			}

			// Token: 0x0600948A RID: 38026 RVA: 0x002C5D8C File Offset: 0x002C5D8C
			int IEqualityComparer<KeyValuePair<!0, !1>>.GetHashCode(KeyValuePair<TKey, TValue> obj)
			{
				return this._keyComparer.GetHashCode(obj.Key);
			}

			// Token: 0x0600948B RID: 38027 RVA: 0x002C5DA0 File Offset: 0x002C5DA0
			internal static ImmutableDictionary<TKey, TValue>.Comparers Get(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				if (keyComparer != ImmutableDictionary<TKey, TValue>.Comparers.Default.KeyComparer || valueComparer != ImmutableDictionary<TKey, TValue>.Comparers.Default.ValueComparer)
				{
					return new ImmutableDictionary<TKey, TValue>.Comparers(keyComparer, valueComparer);
				}
				return ImmutableDictionary<TKey, TValue>.Comparers.Default;
			}

			// Token: 0x0600948C RID: 38028 RVA: 0x002C5DF4 File Offset: 0x002C5DF4
			internal ImmutableDictionary<TKey, TValue>.Comparers WithValueComparer(IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				if (this._valueComparer != valueComparer)
				{
					return ImmutableDictionary<TKey, TValue>.Comparers.Get(this.KeyComparer, valueComparer);
				}
				return this;
			}

			// Token: 0x04004BDF RID: 19423
			internal static readonly ImmutableDictionary<TKey, TValue>.Comparers Default = new ImmutableDictionary<TKey, TValue>.Comparers(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);

			// Token: 0x04004BE0 RID: 19424
			private readonly IEqualityComparer<TKey> _keyComparer;

			// Token: 0x04004BE1 RID: 19425
			private readonly IEqualityComparer<TValue> _valueComparer;
		}

		// Token: 0x020011A1 RID: 4513
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x0600948E RID: 38030 RVA: 0x002C5E34 File Offset: 0x002C5E34
			internal Enumerator(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Builder builder = null)
			{
				this._builder = builder;
				this._mapEnumerator = new SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator(root);
				this._bucketEnumerator = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
			}

			// Token: 0x17001ECB RID: 7883
			// (get) Token: 0x0600948F RID: 38031 RVA: 0x002C5E70 File Offset: 0x002C5E70
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					this._mapEnumerator.ThrowIfDisposed();
					return this._bucketEnumerator.Current;
				}
			}

			// Token: 0x17001ECC RID: 7884
			// (get) Token: 0x06009490 RID: 38032 RVA: 0x002C5E88 File Offset: 0x002C5E88
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06009491 RID: 38033 RVA: 0x002C5E98 File Offset: 0x002C5E98
			public bool MoveNext()
			{
				this.ThrowIfChanged();
				if (this._bucketEnumerator.MoveNext())
				{
					return true;
				}
				if (this._mapEnumerator.MoveNext())
				{
					KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair = this._mapEnumerator.Current;
					this._bucketEnumerator = new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator(keyValuePair.Value);
					return this._bucketEnumerator.MoveNext();
				}
				return false;
			}

			// Token: 0x06009492 RID: 38034 RVA: 0x002C5EFC File Offset: 0x002C5EFC
			public void Reset()
			{
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._mapEnumerator.Reset();
				this._bucketEnumerator.Dispose();
				this._bucketEnumerator = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
			}

			// Token: 0x06009493 RID: 38035 RVA: 0x002C5F54 File Offset: 0x002C5F54
			public void Dispose()
			{
				this._mapEnumerator.Dispose();
				this._bucketEnumerator.Dispose();
			}

			// Token: 0x06009494 RID: 38036 RVA: 0x002C5F6C File Offset: 0x002C5F6C
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(System.Collections.Immutable2448884.SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x04004BE2 RID: 19426
			private readonly ImmutableDictionary<TKey, TValue>.Builder _builder;

			// Token: 0x04004BE3 RID: 19427
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator _mapEnumerator;

			// Token: 0x04004BE4 RID: 19428
			private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator _bucketEnumerator;

			// Token: 0x04004BE5 RID: 19429
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x020011A2 RID: 4514
		[System.Collections.Immutable.IsReadOnly]
		internal struct HashBucket : IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
		{
			// Token: 0x06009495 RID: 38037 RVA: 0x002C5F9C File Offset: 0x002C5F9C
			private HashBucket(KeyValuePair<TKey, TValue> firstElement, ImmutableList<KeyValuePair<TKey, TValue>>.Node additionalElements = null)
			{
				this._firstValue = firstElement;
				this._additionalElements = (additionalElements ?? ImmutableList<KeyValuePair<TKey, TValue>>.Node.EmptyNode);
			}

			// Token: 0x17001ECD RID: 7885
			// (get) Token: 0x06009496 RID: 38038 RVA: 0x002C5FB8 File Offset: 0x002C5FB8
			internal bool IsEmpty
			{
				get
				{
					return this._additionalElements == null;
				}
			}

			// Token: 0x17001ECE RID: 7886
			// (get) Token: 0x06009497 RID: 38039 RVA: 0x002C5FC4 File Offset: 0x002C5FC4
			internal KeyValuePair<TKey, TValue> FirstValue
			{
				get
				{
					if (this.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._firstValue;
				}
			}

			// Token: 0x17001ECF RID: 7887
			// (get) Token: 0x06009498 RID: 38040 RVA: 0x002C5FE0 File Offset: 0x002C5FE0
			internal ImmutableList<KeyValuePair<TKey, TValue>>.Node AdditionalElements
			{
				get
				{
					return this._additionalElements;
				}
			}

			// Token: 0x06009499 RID: 38041 RVA: 0x002C5FE8 File Offset: 0x002C5FE8
			public ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator GetEnumerator()
			{
				return new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator(this);
			}

			// Token: 0x0600949A RID: 38042 RVA: 0x002C5FF8 File Offset: 0x002C5FF8
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600949B RID: 38043 RVA: 0x002C6008 File Offset: 0x002C6008
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600949C RID: 38044 RVA: 0x002C6018 File Offset: 0x002C6018
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600949D RID: 38045 RVA: 0x002C6020 File Offset: 0x002C6020
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600949E RID: 38046 RVA: 0x002C6028 File Offset: 0x002C6028
			internal ImmutableDictionary<TKey, TValue>.HashBucket Add(TKey key, TValue value, IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer, IEqualityComparer<TValue> valueComparer, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior behavior, out ImmutableDictionary<TKey, TValue>.OperationResult result)
			{
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
				if (this.IsEmpty)
				{
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(keyValuePair, null);
				}
				if (keyOnlyComparer.Equals(keyValuePair, this._firstValue))
				{
					switch (behavior)
					{
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.AppliedWithoutSizeChange;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(keyValuePair, this._additionalElements);
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.Skip:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent:
						if (!valueComparer.Equals(this._firstValue.Value, value))
						{
							throw new ArgumentException(System.Collections.Immutable2448884.SR.Format(System.Collections.Immutable2448884.SR.DuplicateKey, key));
						}
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowAlways:
						throw new ArgumentException(System.Collections.Immutable2448884.SR.Format(System.Collections.Immutable2448884.SR.DuplicateKey, key));
					default:
						throw new InvalidOperationException();
					}
				}
				else
				{
					int num = this._additionalElements.IndexOf(keyValuePair, keyOnlyComparer);
					if (num < 0)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.Add(keyValuePair));
					}
					switch (behavior)
					{
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.AppliedWithoutSizeChange;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.ReplaceAt(num, keyValuePair));
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.Skip:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent:
					{
						ref KeyValuePair<TKey, TValue> ptr = ref this._additionalElements.ItemRef(num);
						KeyValuePair<TKey, TValue> keyValuePair2 = ptr;
						if (!valueComparer.Equals(keyValuePair2.Value, value))
						{
							throw new ArgumentException(System.Collections.Immutable2448884.SR.Format(System.Collections.Immutable2448884.SR.DuplicateKey, key));
						}
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					}
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowAlways:
						throw new ArgumentException(System.Collections.Immutable2448884.SR.Format(System.Collections.Immutable2448884.SR.DuplicateKey, key));
					default:
						throw new InvalidOperationException();
					}
				}
			}

			// Token: 0x0600949F RID: 38047 RVA: 0x002C61D4 File Offset: 0x002C61D4
			internal ImmutableDictionary<TKey, TValue>.HashBucket Remove(TKey key, IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer, out ImmutableDictionary<TKey, TValue>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
					return this;
				}
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, default(TValue));
				if (keyOnlyComparer.Equals(this._firstValue, keyValuePair))
				{
					if (this._additionalElements.IsEmpty)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
						return default(ImmutableDictionary<TKey, TValue>.HashBucket);
					}
					int count = this._additionalElements.Left.Count;
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(this._additionalElements.Key, this._additionalElements.RemoveAt(count));
				}
				else
				{
					int num = this._additionalElements.IndexOf(keyValuePair, keyOnlyComparer);
					if (num < 0)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					}
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.RemoveAt(num));
				}
			}

			// Token: 0x060094A0 RID: 38048 RVA: 0x002C62A8 File Offset: 0x002C62A8
			internal unsafe bool TryGetValue(TKey key, ImmutableDictionary<TKey, TValue>.Comparers comparers, out TValue value)
			{
				if (this.IsEmpty)
				{
					value = default(TValue);
					return false;
				}
				if (comparers.KeyComparer.Equals(this._firstValue.Key, key))
				{
					value = this._firstValue.Value;
					return true;
				}
				KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(key, default(TValue));
				int num = this._additionalElements.IndexOf(item, comparers.KeyOnlyComparer);
				if (num < 0)
				{
					value = default(TValue);
					return false;
				}
				KeyValuePair<TKey, TValue> keyValuePair = *this._additionalElements.ItemRef(num);
				value = keyValuePair.Value;
				return true;
			}

			// Token: 0x060094A1 RID: 38049 RVA: 0x002C6358 File Offset: 0x002C6358
			internal unsafe bool TryGetKey(TKey equalKey, ImmutableDictionary<TKey, TValue>.Comparers comparers, out TKey actualKey)
			{
				if (this.IsEmpty)
				{
					actualKey = equalKey;
					return false;
				}
				if (comparers.KeyComparer.Equals(this._firstValue.Key, equalKey))
				{
					actualKey = this._firstValue.Key;
					return true;
				}
				KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(equalKey, default(TValue));
				int num = this._additionalElements.IndexOf(item, comparers.KeyOnlyComparer);
				if (num < 0)
				{
					actualKey = equalKey;
					return false;
				}
				KeyValuePair<TKey, TValue> keyValuePair = *this._additionalElements.ItemRef(num);
				actualKey = keyValuePair.Key;
				return true;
			}

			// Token: 0x060094A2 RID: 38050 RVA: 0x002C6408 File Offset: 0x002C6408
			internal void Freeze()
			{
				if (this._additionalElements != null)
				{
					this._additionalElements.Freeze();
				}
			}

			// Token: 0x04004BE6 RID: 19430
			private readonly KeyValuePair<TKey, TValue> _firstValue;

			// Token: 0x04004BE7 RID: 19431
			private readonly ImmutableList<KeyValuePair<TKey, TValue>>.Node _additionalElements;

			// Token: 0x0200121C RID: 4636
			internal struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
			{
				// Token: 0x060096DE RID: 38622 RVA: 0x002CD000 File Offset: 0x002CD000
				internal Enumerator(ImmutableDictionary<TKey, TValue>.HashBucket bucket)
				{
					this._bucket = bucket;
					this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
					this._additionalEnumerator = default(ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator);
				}

				// Token: 0x17001F60 RID: 8032
				// (get) Token: 0x060096DF RID: 38623 RVA: 0x002CD01C File Offset: 0x002CD01C
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x17001F61 RID: 8033
				// (get) Token: 0x060096E0 RID: 38624 RVA: 0x002CD02C File Offset: 0x002CD02C
				public KeyValuePair<TKey, TValue> Current
				{
					get
					{
						ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position currentPosition = this._currentPosition;
						if (currentPosition == ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First)
						{
							return this._bucket._firstValue;
						}
						if (currentPosition != ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional)
						{
							throw new InvalidOperationException();
						}
						return this._additionalEnumerator.Current;
					}
				}

				// Token: 0x060096E1 RID: 38625 RVA: 0x002CD074 File Offset: 0x002CD074
				public bool MoveNext()
				{
					if (this._bucket.IsEmpty)
					{
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End;
						return false;
					}
					switch (this._currentPosition)
					{
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst:
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First;
						return true;
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First:
						if (this._bucket._additionalElements.IsEmpty)
						{
							this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End;
							return false;
						}
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional;
						this._additionalEnumerator = new ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator(this._bucket._additionalElements, null, -1, -1, false);
						return this._additionalEnumerator.MoveNext();
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional:
						return this._additionalEnumerator.MoveNext();
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End:
						return false;
					default:
						throw new InvalidOperationException();
					}
				}

				// Token: 0x060096E2 RID: 38626 RVA: 0x002CD128 File Offset: 0x002CD128
				public void Reset()
				{
					this._additionalEnumerator.Dispose();
					this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
				}

				// Token: 0x060096E3 RID: 38627 RVA: 0x002CD13C File Offset: 0x002CD13C
				public void Dispose()
				{
					this._additionalEnumerator.Dispose();
				}

				// Token: 0x04004F3E RID: 20286
				private readonly ImmutableDictionary<TKey, TValue>.HashBucket _bucket;

				// Token: 0x04004F3F RID: 20287
				private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position _currentPosition;

				// Token: 0x04004F40 RID: 20288
				private ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator _additionalEnumerator;

				// Token: 0x0200121F RID: 4639
				private enum Position
				{
					// Token: 0x04004F4A RID: 20298
					BeforeFirst,
					// Token: 0x04004F4B RID: 20299
					First,
					// Token: 0x04004F4C RID: 20300
					Additional,
					// Token: 0x04004F4D RID: 20301
					End
				}
			}
		}

		// Token: 0x020011A3 RID: 4515
		[System.Collections.Immutable.IsReadOnly]
		private struct MutationInput
		{
			// Token: 0x060094A3 RID: 38051 RVA: 0x002C6420 File Offset: 0x002C6420
			internal MutationInput(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers)
			{
				this._root = root;
				this._comparers = comparers;
			}

			// Token: 0x060094A4 RID: 38052 RVA: 0x002C6430 File Offset: 0x002C6430
			internal MutationInput(ImmutableDictionary<TKey, TValue> map)
			{
				this._root = map._root;
				this._comparers = map._comparers;
			}

			// Token: 0x17001ED0 RID: 7888
			// (get) Token: 0x060094A5 RID: 38053 RVA: 0x002C644C File Offset: 0x002C644C
			internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17001ED1 RID: 7889
			// (get) Token: 0x060094A6 RID: 38054 RVA: 0x002C6454 File Offset: 0x002C6454
			internal ImmutableDictionary<TKey, TValue>.Comparers Comparers
			{
				get
				{
					return this._comparers;
				}
			}

			// Token: 0x17001ED2 RID: 7890
			// (get) Token: 0x060094A7 RID: 38055 RVA: 0x002C645C File Offset: 0x002C645C
			internal IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._comparers.KeyComparer;
				}
			}

			// Token: 0x17001ED3 RID: 7891
			// (get) Token: 0x060094A8 RID: 38056 RVA: 0x002C646C File Offset: 0x002C646C
			internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
			{
				get
				{
					return this._comparers.KeyOnlyComparer;
				}
			}

			// Token: 0x17001ED4 RID: 7892
			// (get) Token: 0x060094A9 RID: 38057 RVA: 0x002C647C File Offset: 0x002C647C
			internal IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._comparers.ValueComparer;
				}
			}

			// Token: 0x17001ED5 RID: 7893
			// (get) Token: 0x060094AA RID: 38058 RVA: 0x002C648C File Offset: 0x002C648C
			internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketComparer
			{
				get
				{
					return this._comparers.HashBucketEqualityComparer;
				}
			}

			// Token: 0x04004BE8 RID: 19432
			private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

			// Token: 0x04004BE9 RID: 19433
			private readonly ImmutableDictionary<TKey, TValue>.Comparers _comparers;
		}

		// Token: 0x020011A4 RID: 4516
		[System.Collections.Immutable.IsReadOnly]
		private struct MutationResult
		{
			// Token: 0x060094AB RID: 38059 RVA: 0x002C649C File Offset: 0x002C649C
			internal MutationResult(ImmutableDictionary<TKey, TValue>.MutationInput unchangedInput)
			{
				this._root = unchangedInput.Root;
				this._countAdjustment = 0;
			}

			// Token: 0x060094AC RID: 38060 RVA: 0x002C64B4 File Offset: 0x002C64B4
			internal MutationResult(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int countAdjustment)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
				this._root = root;
				this._countAdjustment = countAdjustment;
			}

			// Token: 0x17001ED6 RID: 7894
			// (get) Token: 0x060094AD RID: 38061 RVA: 0x002C64D0 File Offset: 0x002C64D0
			internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17001ED7 RID: 7895
			// (get) Token: 0x060094AE RID: 38062 RVA: 0x002C64D8 File Offset: 0x002C64D8
			internal int CountAdjustment
			{
				get
				{
					return this._countAdjustment;
				}
			}

			// Token: 0x060094AF RID: 38063 RVA: 0x002C64E0 File Offset: 0x002C64E0
			internal ImmutableDictionary<TKey, TValue> Finalize(ImmutableDictionary<TKey, TValue> priorMap)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(priorMap, "priorMap");
				return priorMap.Wrap(this.Root, priorMap._count + this.CountAdjustment);
			}

			// Token: 0x04004BEA RID: 19434
			private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

			// Token: 0x04004BEB RID: 19435
			private readonly int _countAdjustment;
		}

		// Token: 0x020011A5 RID: 4517
		internal enum KeyCollisionBehavior
		{
			// Token: 0x04004BED RID: 19437
			SetValue,
			// Token: 0x04004BEE RID: 19438
			Skip,
			// Token: 0x04004BEF RID: 19439
			ThrowIfValueDifferent,
			// Token: 0x04004BF0 RID: 19440
			ThrowAlways
		}

		// Token: 0x020011A6 RID: 4518
		internal enum OperationResult
		{
			// Token: 0x04004BF2 RID: 19442
			AppliedWithoutSizeChange,
			// Token: 0x04004BF3 RID: 19443
			SizeChanged,
			// Token: 0x04004BF4 RID: 19444
			NoChangeRequired
		}
	}
}
