using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.Immutable
{
	// Token: 0x02000CAA RID: 3242
	internal class ImmutableDictionaryBuilderDebuggerProxy<TKey, TValue>
	{
		// Token: 0x06008249 RID: 33353 RVA: 0x0026505C File Offset: 0x0026505C
		public ImmutableDictionaryBuilderDebuggerProxy(ImmutableDictionary<TKey, TValue>.Builder map)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Builder>(map, "map");
			this._map = map;
		}

		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x0600824A RID: 33354 RVA: 0x00265078 File Offset: 0x00265078
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<TKey, TValue>[] Contents
		{
			get
			{
				if (this._contents == null)
				{
					this._contents = this._map.ToArray(this._map.Count);
				}
				return this._contents;
			}
		}

		// Token: 0x04003D33 RID: 15667
		private readonly ImmutableDictionary<TKey, TValue>.Builder _map;

		// Token: 0x04003D34 RID: 15668
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
