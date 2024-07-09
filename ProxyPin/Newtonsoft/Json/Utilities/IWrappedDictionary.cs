using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AAC RID: 2732
	internal interface IWrappedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06006CCB RID: 27851
		[Nullable(1)]
		object UnderlyingDictionary { [NullableContext(1)] get; }
	}
}
