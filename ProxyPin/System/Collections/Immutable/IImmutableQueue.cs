using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000C9E RID: 3230
	[ComVisible(true)]
	public interface IImmutableQueue<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x06008105 RID: 33029
		bool IsEmpty { get; }

		// Token: 0x06008106 RID: 33030
		IImmutableQueue<T> Clear();

		// Token: 0x06008107 RID: 33031
		T Peek();

		// Token: 0x06008108 RID: 33032
		IImmutableQueue<T> Enqueue(T value);

		// Token: 0x06008109 RID: 33033
		IImmutableQueue<T> Dequeue();
	}
}
