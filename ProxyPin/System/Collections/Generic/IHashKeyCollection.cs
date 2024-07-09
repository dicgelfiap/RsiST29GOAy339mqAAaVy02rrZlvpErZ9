using System;

namespace System.Collections.Generic
{
	// Token: 0x02000CC8 RID: 3272
	internal interface IHashKeyCollection<in TKey>
	{
		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x0600840B RID: 33803
		IEqualityComparer<TKey> KeyComparer { get; }
	}
}
