using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000CA2 RID: 3234
	internal interface IStrongEnumerable<out T, TEnumerator> where TEnumerator : struct, IStrongEnumerator<T>
	{
		// Token: 0x06008160 RID: 33120
		TEnumerator GetEnumerator();
	}
}
