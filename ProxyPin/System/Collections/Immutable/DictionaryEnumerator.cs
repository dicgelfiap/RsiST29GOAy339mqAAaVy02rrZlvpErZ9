using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000C95 RID: 3221
	internal sealed class DictionaryEnumerator<TKey, TValue> : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x060080C3 RID: 32963 RVA: 0x00261670 File Offset: 0x00261670
		internal DictionaryEnumerator(IEnumerator<KeyValuePair<TKey, TValue>> inner)
		{
			Requires.NotNull<IEnumerator<KeyValuePair<TKey, TValue>>>(inner, "inner");
			this._inner = inner;
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x060080C4 RID: 32964 RVA: 0x0026168C File Offset: 0x0026168C
		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				object key = keyValuePair.Key;
				keyValuePair = this._inner.Current;
				return new DictionaryEntry(key, keyValuePair.Value);
			}
		}

		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x060080C5 RID: 32965 RVA: 0x002616D4 File Offset: 0x002616D4
		public object Key
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x060080C6 RID: 32966 RVA: 0x00261700 File Offset: 0x00261700
		public object Value
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x17001BE6 RID: 7142
		// (get) Token: 0x060080C7 RID: 32967 RVA: 0x0026172C File Offset: 0x0026172C
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x060080C8 RID: 32968 RVA: 0x0026173C File Offset: 0x0026173C
		public bool MoveNext()
		{
			return this._inner.MoveNext();
		}

		// Token: 0x060080C9 RID: 32969 RVA: 0x0026174C File Offset: 0x0026174C
		public void Reset()
		{
			this._inner.Reset();
		}

		// Token: 0x04003D21 RID: 15649
		private readonly IEnumerator<KeyValuePair<TKey, TValue>> _inner;
	}
}
