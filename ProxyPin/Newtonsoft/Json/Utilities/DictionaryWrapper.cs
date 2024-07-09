using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AAD RID: 2733
	[NullableContext(1)]
	[Nullable(0)]
	internal class DictionaryWrapper<[Nullable(2)] TKey, [Nullable(2)] TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IWrappedDictionary, IDictionary, ICollection
	{
		// Token: 0x06006CCC RID: 27852 RVA: 0x0020E8C8 File Offset: 0x0020E8C8
		public DictionaryWrapper(IDictionary dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._dictionary = dictionary;
		}

		// Token: 0x06006CCD RID: 27853 RVA: 0x0020E8E4 File Offset: 0x0020E8E4
		public DictionaryWrapper(IDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._genericDictionary = dictionary;
		}

		// Token: 0x06006CCE RID: 27854 RVA: 0x0020E900 File Offset: 0x0020E900
		public DictionaryWrapper(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._readOnlyDictionary = dictionary;
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06006CCF RID: 27855 RVA: 0x0020E91C File Offset: 0x0020E91C
		internal IDictionary<TKey, TValue> GenericDictionary
		{
			get
			{
				return this._genericDictionary;
			}
		}

		// Token: 0x06006CD0 RID: 27856 RVA: 0x0020E924 File Offset: 0x0020E924
		public void Add(TKey key, TValue value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add(key, value);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006CD1 RID: 27857 RVA: 0x0020E978 File Offset: 0x0020E978
		public bool ContainsKey(TKey key)
		{
			if (this._dictionary != null)
			{
				return this._dictionary.Contains(key);
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.ContainsKey(key);
			}
			return this.GenericDictionary.ContainsKey(key);
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06006CD2 RID: 27858 RVA: 0x0020E9CC File Offset: 0x0020E9CC
		public ICollection<TKey> Keys
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Keys.Cast<TKey>().ToList<TKey>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Keys.ToList<TKey>();
				}
				return this.GenericDictionary.Keys;
			}
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x0020EA28 File Offset: 0x0020EA28
		public bool Remove(TKey key)
		{
			if (this._dictionary != null)
			{
				if (this._dictionary.Contains(key))
				{
					this._dictionary.Remove(key);
					return true;
				}
				return false;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this.GenericDictionary.Remove(key);
			}
		}

		// Token: 0x06006CD4 RID: 27860 RVA: 0x0020EA8C File Offset: 0x0020EA8C
		public bool TryGetValue(TKey key, [MaybeNull] out TValue value)
		{
			if (this._dictionary != null)
			{
				if (!this._dictionary.Contains(key))
				{
					value = default(TValue);
					return false;
				}
				value = (TValue)((object)this._dictionary[key]);
				return true;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this.GenericDictionary.TryGetValue(key, out value);
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06006CD5 RID: 27861 RVA: 0x0020EB04 File Offset: 0x0020EB04
		public ICollection<TValue> Values
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Values.Cast<TValue>().ToList<TValue>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Values.ToList<TValue>();
				}
				return this.GenericDictionary.Values;
			}
		}

		// Token: 0x170016AA RID: 5802
		public TValue this[TKey key]
		{
			get
			{
				if (this._dictionary != null)
				{
					return (TValue)((object)this._dictionary[key]);
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary[key];
				}
				return this.GenericDictionary[key];
			}
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				this.GenericDictionary[key] = value;
			}
		}

		// Token: 0x06006CD8 RID: 27864 RVA: 0x0020EC0C File Offset: 0x0020EC0C
		public void Add([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				((IList)this._dictionary).Add(item);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			IDictionary<TKey, TValue> genericDictionary = this._genericDictionary;
			if (genericDictionary == null)
			{
				return;
			}
			genericDictionary.Add(item);
		}

		// Token: 0x06006CD9 RID: 27865 RVA: 0x0020EC68 File Offset: 0x0020EC68
		public void Clear()
		{
			if (this._dictionary != null)
			{
				this._dictionary.Clear();
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this.GenericDictionary.Clear();
		}

		// Token: 0x06006CDA RID: 27866 RVA: 0x0020ECA0 File Offset: 0x0020ECA0
		public bool Contains([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				return ((IList)this._dictionary).Contains(item);
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.Contains(item);
			}
			return this.GenericDictionary.Contains(item);
		}

		// Token: 0x06006CDB RID: 27867 RVA: 0x0020ECF8 File Offset: 0x0020ECF8
		public void CopyTo([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (this._dictionary != null)
			{
				using (IDictionaryEnumerator enumerator = this._dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DictionaryEntry entry = enumerator.Entry;
						array[arrayIndex++] = new KeyValuePair<TKey, TValue>((TKey)((object)entry.Key), (TValue)((object)entry.Value));
					}
					return;
				}
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this.GenericDictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x06006CDC RID: 27868 RVA: 0x0020EDA4 File Offset: 0x0020EDA4
		public int Count
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Count;
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Count;
				}
				return this.GenericDictionary.Count;
			}
		}

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06006CDD RID: 27869 RVA: 0x0020EDE0 File Offset: 0x0020EDE0
		public bool IsReadOnly
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.IsReadOnly;
				}
				return this._readOnlyDictionary != null || this.GenericDictionary.IsReadOnly;
			}
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x0020EE14 File Offset: 0x0020EE14
		public bool Remove([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				if (!this._dictionary.Contains(item.Key))
				{
					return true;
				}
				if (object.Equals(this._dictionary[item.Key], item.Value))
				{
					this._dictionary.Remove(item.Key);
					return true;
				}
				return false;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this.GenericDictionary.Remove(item);
			}
		}

		// Token: 0x06006CDF RID: 27871 RVA: 0x0020EEB4 File Offset: 0x0020EEB4
		[return: Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})]
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return (from DictionaryEntry de in this._dictionary
				select new KeyValuePair<TKey, TValue>((TKey)((object)de.Key), (TValue)((object)de.Value))).GetEnumerator();
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.GetEnumerator();
			}
			return this.GenericDictionary.GetEnumerator();
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x0020EF2C File Offset: 0x0020EF2C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006CE1 RID: 27873 RVA: 0x0020EF34 File Offset: 0x0020EF34
		void IDictionary.Add(object key, object value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this.GenericDictionary.Add((TKey)((object)key), (TValue)((object)value));
		}

		// Token: 0x170016AD RID: 5805
		[Nullable(2)]
		object IDictionary.this[object key]
		{
			[return: Nullable(2)]
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary[(TKey)((object)key)];
				}
				return this.GenericDictionary[(TKey)((object)key)];
			}
			[param: Nullable(2)]
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				this.GenericDictionary[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x06006CE4 RID: 27876 RVA: 0x0020F040 File Offset: 0x0020F040
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return this._dictionary.GetEnumerator();
			}
			if (this._readOnlyDictionary != null)
			{
				return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this._readOnlyDictionary.GetEnumerator());
			}
			return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this.GenericDictionary.GetEnumerator());
		}

		// Token: 0x06006CE5 RID: 27877 RVA: 0x0020F0A0 File Offset: 0x0020F0A0
		bool IDictionary.Contains(object key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.ContainsKey((TKey)((object)key));
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.ContainsKey((TKey)((object)key));
			}
			return this._dictionary.Contains(key);
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06006CE6 RID: 27878 RVA: 0x0020F0F8 File Offset: 0x0020F0F8
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._genericDictionary == null && (this._readOnlyDictionary != null || this._dictionary.IsFixedSize);
			}
		}

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06006CE7 RID: 27879 RVA: 0x0020F120 File Offset: 0x0020F120
		ICollection IDictionary.Keys
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Keys.ToList<TKey>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Keys.ToList<TKey>();
				}
				return this._dictionary.Keys;
			}
		}

		// Token: 0x06006CE8 RID: 27880 RVA: 0x0020F174 File Offset: 0x0020F174
		public void Remove(object key)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Remove(key);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this.GenericDictionary.Remove((TKey)((object)key));
		}

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06006CE9 RID: 27881 RVA: 0x0020F1B4 File Offset: 0x0020F1B4
		ICollection IDictionary.Values
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Values.ToList<TValue>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Values.ToList<TValue>();
				}
				return this._dictionary.Values;
			}
		}

		// Token: 0x06006CEA RID: 27882 RVA: 0x0020F208 File Offset: 0x0020F208
		void ICollection.CopyTo(Array array, int index)
		{
			if (this._dictionary != null)
			{
				this._dictionary.CopyTo(array, index);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this.GenericDictionary.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
		}

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x06006CEB RID: 27883 RVA: 0x0020F248 File Offset: 0x0020F248
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._dictionary != null && this._dictionary.IsSynchronized;
			}
		}

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06006CEC RID: 27884 RVA: 0x0020F264 File Offset: 0x0020F264
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06006CED RID: 27885 RVA: 0x0020F28C File Offset: 0x0020F28C
		public object UnderlyingDictionary
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary;
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary;
				}
				return this.GenericDictionary;
			}
		}

		// Token: 0x040036B8 RID: 14008
		[Nullable(2)]
		private readonly IDictionary _dictionary;

		// Token: 0x040036B9 RID: 14009
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private readonly IDictionary<TKey, TValue> _genericDictionary;

		// Token: 0x040036BA RID: 14010
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private readonly IReadOnlyDictionary<TKey, TValue> _readOnlyDictionary;

		// Token: 0x040036BB RID: 14011
		[Nullable(2)]
		private object _syncRoot;

		// Token: 0x020010C1 RID: 4289
		[Nullable(0)]
		[Newtonsoft.Json.IsReadOnly]
		private struct DictionaryEnumerator<[Nullable(2)] TEnumeratorKey, [Nullable(2)] TEnumeratorValue> : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06009125 RID: 37157 RVA: 0x002BB3BC File Offset: 0x002BB3BC
			public DictionaryEnumerator([Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})] IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				ValidationUtils.ArgumentNotNull(e, "e");
				this._e = e;
			}

			// Token: 0x17001E3C RID: 7740
			// (get) Token: 0x06009126 RID: 37158 RVA: 0x002BB3D0 File Offset: 0x002BB3D0
			public DictionaryEntry Entry
			{
				get
				{
					return (DictionaryEntry)this.Current;
				}
			}

			// Token: 0x17001E3D RID: 7741
			// (get) Token: 0x06009127 RID: 37159 RVA: 0x002BB3E0 File Offset: 0x002BB3E0
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x17001E3E RID: 7742
			// (get) Token: 0x06009128 RID: 37160 RVA: 0x002BB400 File Offset: 0x002BB400
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x17001E3F RID: 7743
			// (get) Token: 0x06009129 RID: 37161 RVA: 0x002BB420 File Offset: 0x002BB420
			public object Current
			{
				get
				{
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair = this._e.Current;
					object key = keyValuePair.Key;
					keyValuePair = this._e.Current;
					return new DictionaryEntry(key, keyValuePair.Value);
				}
			}

			// Token: 0x0600912A RID: 37162 RVA: 0x002BB46C File Offset: 0x002BB46C
			public bool MoveNext()
			{
				return this._e.MoveNext();
			}

			// Token: 0x0600912B RID: 37163 RVA: 0x002BB47C File Offset: 0x002BB47C
			public void Reset()
			{
				this._e.Reset();
			}

			// Token: 0x0400482F RID: 18479
			[Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})]
			private readonly IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;
		}
	}
}
