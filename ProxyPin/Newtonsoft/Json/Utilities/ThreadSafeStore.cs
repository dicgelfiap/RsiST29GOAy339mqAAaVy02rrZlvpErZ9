using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000ACD RID: 2765
	[NullableContext(1)]
	[Nullable(0)]
	internal class ThreadSafeStore<[Nullable(2)] TKey, [Nullable(2)] TValue>
	{
		// Token: 0x06006E17 RID: 28183 RVA: 0x00215260 File Offset: 0x00215260
		public ThreadSafeStore(Func<TKey, TValue> creator)
		{
			ValidationUtils.ArgumentNotNull(creator, "creator");
			this._creator = creator;
			this._concurrentStore = new ConcurrentDictionary<TKey, TValue>();
		}

		// Token: 0x06006E18 RID: 28184 RVA: 0x00215288 File Offset: 0x00215288
		public TValue Get(TKey key)
		{
			return this._concurrentStore.GetOrAdd(key, this._creator);
		}

		// Token: 0x0400370C RID: 14092
		private readonly ConcurrentDictionary<TKey, TValue> _concurrentStore;

		// Token: 0x0400370D RID: 14093
		private readonly Func<TKey, TValue> _creator;
	}
}
