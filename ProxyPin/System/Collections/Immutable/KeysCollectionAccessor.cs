using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000CBE RID: 3262
	internal sealed class KeysCollectionAccessor<TKey, TValue> : KeysOrValuesCollectionAccessor<TKey, TValue, TKey>
	{
		// Token: 0x060083D2 RID: 33746 RVA: 0x00268530 File Offset: 0x00268530
		internal KeysCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary) : base(dictionary, dictionary.Keys)
		{
		}

		// Token: 0x060083D3 RID: 33747 RVA: 0x00268540 File Offset: 0x00268540
		public override bool Contains(TKey item)
		{
			return base.Dictionary.ContainsKey(item);
		}
	}
}
