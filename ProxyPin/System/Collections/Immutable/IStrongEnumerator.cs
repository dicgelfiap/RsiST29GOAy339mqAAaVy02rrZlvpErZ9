using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000CA3 RID: 3235
	internal interface IStrongEnumerator<T>
	{
		// Token: 0x17001BFB RID: 7163
		// (get) Token: 0x06008161 RID: 33121
		T Current { get; }

		// Token: 0x06008162 RID: 33122
		bool MoveNext();
	}
}
