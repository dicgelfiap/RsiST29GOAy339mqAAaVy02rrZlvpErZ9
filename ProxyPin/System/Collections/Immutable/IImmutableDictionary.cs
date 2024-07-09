using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000C9A RID: 3226
	[ComVisible(true)]
	public interface IImmutableDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
	{
		// Token: 0x060080D9 RID: 32985
		IImmutableDictionary<TKey, TValue> Clear();

		// Token: 0x060080DA RID: 32986
		IImmutableDictionary<TKey, TValue> Add(TKey key, TValue value);

		// Token: 0x060080DB RID: 32987
		IImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);

		// Token: 0x060080DC RID: 32988
		IImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value);

		// Token: 0x060080DD RID: 32989
		IImmutableDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);

		// Token: 0x060080DE RID: 32990
		IImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

		// Token: 0x060080DF RID: 32991
		IImmutableDictionary<TKey, TValue> Remove(TKey key);

		// Token: 0x060080E0 RID: 32992
		bool Contains(KeyValuePair<TKey, TValue> pair);

		// Token: 0x060080E1 RID: 32993
		bool TryGetKey(TKey equalKey, out TKey actualKey);
	}
}
