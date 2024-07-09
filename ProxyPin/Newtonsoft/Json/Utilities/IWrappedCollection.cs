using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AA3 RID: 2723
	internal interface IWrappedCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x06006C6E RID: 27758
		[Nullable(1)]
		object UnderlyingCollection { [NullableContext(1)] get; }
	}
}
