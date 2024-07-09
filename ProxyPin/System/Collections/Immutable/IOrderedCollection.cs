using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000CA4 RID: 3236
	internal interface IOrderedCollection<out T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x17001BFC RID: 7164
		// (get) Token: 0x06008163 RID: 33123
		int Count { get; }

		// Token: 0x17001BFD RID: 7165
		T this[int index]
		{
			get;
		}
	}
}
