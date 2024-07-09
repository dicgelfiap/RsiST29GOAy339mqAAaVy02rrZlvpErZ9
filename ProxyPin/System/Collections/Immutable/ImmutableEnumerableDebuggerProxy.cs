using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Collections.Immutable
{
	// Token: 0x02000CAC RID: 3244
	internal class ImmutableEnumerableDebuggerProxy<T>
	{
		// Token: 0x0600824C RID: 33356 RVA: 0x002650B4 File Offset: 0x002650B4
		public ImmutableEnumerableDebuggerProxy(IEnumerable<T> enumerable)
		{
			Requires.NotNull<IEnumerable<T>>(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x0600824D RID: 33357 RVA: 0x002650D0 File Offset: 0x002650D0
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				T[] result;
				if ((result = this._cachedContents) == null)
				{
					result = (this._cachedContents = this._enumerable.ToArray<T>());
				}
				return result;
			}
		}

		// Token: 0x04003D35 RID: 15669
		private readonly IEnumerable<T> _enumerable;

		// Token: 0x04003D36 RID: 15670
		private T[] _cachedContents;
	}
}
