using System;
using System.Diagnostics;

namespace System.Collections.Immutable
{
	// Token: 0x02000CBA RID: 3258
	internal class ImmutableSortedSetBuilderDebuggerProxy<T>
	{
		// Token: 0x060083AB RID: 33707 RVA: 0x00268110 File Offset: 0x00268110
		public ImmutableSortedSetBuilderDebuggerProxy(ImmutableSortedSet<T>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Builder>(builder, "builder");
			this._set = builder;
		}

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x060083AC RID: 33708 RVA: 0x0026812C File Offset: 0x0026812C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				return this._set.ToArray(this._set.Count);
			}
		}

		// Token: 0x04003D4A RID: 15690
		private readonly ImmutableSortedSet<T>.Builder _set;
	}
}
