using System;
using System.Diagnostics;

namespace System.Collections.Immutable
{
	// Token: 0x02000CA7 RID: 3239
	internal sealed class ImmutableArrayBuilderDebuggerProxy<T>
	{
		// Token: 0x060081E7 RID: 33255 RVA: 0x002641F8 File Offset: 0x002641F8
		public ImmutableArrayBuilderDebuggerProxy(ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			this._builder = builder;
		}

		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x060081E8 RID: 33256 RVA: 0x00264214 File Offset: 0x00264214
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] A
		{
			get
			{
				return this._builder.ToArray();
			}
		}

		// Token: 0x04003D2D RID: 15661
		private readonly ImmutableArray<T>.Builder _builder;
	}
}
