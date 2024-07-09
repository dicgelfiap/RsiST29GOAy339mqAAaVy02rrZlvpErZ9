using System;
using System.Diagnostics;

namespace System.Collections.Immutable
{
	// Token: 0x02000CB2 RID: 3250
	internal class ImmutableListBuilderDebuggerProxy<T>
	{
		// Token: 0x060082E8 RID: 33512 RVA: 0x002665D4 File Offset: 0x002665D4
		public ImmutableListBuilderDebuggerProxy(ImmutableList<T>.Builder builder)
		{
			Requires.NotNull<ImmutableList<T>.Builder>(builder, "builder");
			this._list = builder;
		}

		// Token: 0x17001C33 RID: 7219
		// (get) Token: 0x060082E9 RID: 33513 RVA: 0x002665F0 File Offset: 0x002665F0
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				if (this._cachedContents == null)
				{
					this._cachedContents = this._list.ToArray(this._list.Count);
				}
				return this._cachedContents;
			}
		}

		// Token: 0x04003D39 RID: 15673
		private readonly ImmutableList<T>.Builder _list;

		// Token: 0x04003D3A RID: 15674
		private T[] _cachedContents;
	}
}
