using System;

namespace System.Collections.Generic
{
	// Token: 0x02000CC9 RID: 3273
	internal interface ISortKeyCollection<in TKey>
	{
		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x0600840C RID: 33804
		IComparer<TKey> KeyComparer { get; }
	}
}
