using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.Immutable
{
	// Token: 0x02000CB7 RID: 3255
	internal class ImmutableSortedDictionaryBuilderDebuggerProxy<TKey, TValue>
	{
		// Token: 0x06008353 RID: 33619 RVA: 0x00267408 File Offset: 0x00267408
		public ImmutableSortedDictionaryBuilderDebuggerProxy(ImmutableSortedDictionary<TKey, TValue>.Builder map)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Builder>(map, "map");
			this._map = map;
		}

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x06008354 RID: 33620 RVA: 0x00267424 File Offset: 0x00267424
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

		// Token: 0x04003D44 RID: 15684
		private readonly ImmutableSortedDictionary<TKey, TValue>.Builder _map;

		// Token: 0x04003D45 RID: 15685
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
