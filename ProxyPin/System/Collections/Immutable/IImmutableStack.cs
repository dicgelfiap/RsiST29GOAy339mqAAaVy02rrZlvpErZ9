using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CA0 RID: 3232
	[ComVisible(true)]
	public interface IImmutableStack<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x06008119 RID: 33049
		bool IsEmpty { get; }

		// Token: 0x0600811A RID: 33050
		IImmutableStack<T> Clear();

		// Token: 0x0600811B RID: 33051
		IImmutableStack<T> Push(T value);

		// Token: 0x0600811C RID: 33052
		IImmutableStack<T> Pop();

		// Token: 0x0600811D RID: 33053
		T Peek();
	}
}
