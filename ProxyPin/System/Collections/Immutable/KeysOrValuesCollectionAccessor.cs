using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.Immutable
{
	// Token: 0x02000CBD RID: 3261
	internal abstract class KeysOrValuesCollectionAccessor<TKey, TValue, T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x060083C4 RID: 33732 RVA: 0x00268388 File Offset: 0x00268388
		protected KeysOrValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary, IEnumerable<T> keysOrValues)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, "dictionary");
			Requires.NotNull<IEnumerable<T>>(keysOrValues, "keysOrValues");
			this._dictionary = dictionary;
			this._keysOrValues = keysOrValues;
		}

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x060083C5 RID: 33733 RVA: 0x002683B4 File Offset: 0x002683B4
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x060083C6 RID: 33734 RVA: 0x002683B8 File Offset: 0x002683B8
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x060083C7 RID: 33735 RVA: 0x002683C8 File Offset: 0x002683C8
		protected IImmutableDictionary<TKey, TValue> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x060083C8 RID: 33736 RVA: 0x002683D0 File Offset: 0x002683D0
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060083C9 RID: 33737 RVA: 0x002683D8 File Offset: 0x002683D8
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060083CA RID: 33738
		public abstract bool Contains(T item);

		// Token: 0x060083CB RID: 33739 RVA: 0x002683E0 File Offset: 0x002683E0
		public void CopyTo(T[] array, int arrayIndex)
		{
			Requires.NotNull<T[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x060083CC RID: 33740 RVA: 0x00268470 File Offset: 0x00268470
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060083CD RID: 33741 RVA: 0x00268478 File Offset: 0x00268478
		public IEnumerator<T> GetEnumerator()
		{
			return this._keysOrValues.GetEnumerator();
		}

		// Token: 0x060083CE RID: 33742 RVA: 0x00268488 File Offset: 0x00268488
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060083CF RID: 33743 RVA: 0x00268490 File Offset: 0x00268490
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

		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x060083D0 RID: 33744 RVA: 0x00268528 File Offset: 0x00268528
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x060083D1 RID: 33745 RVA: 0x0026852C File Offset: 0x0026852C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04003D4E RID: 15694
		private readonly IImmutableDictionary<TKey, TValue> _dictionary;

		// Token: 0x04003D4F RID: 15695
		private readonly IEnumerable<T> _keysOrValues;
	}
}
