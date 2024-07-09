using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B17 RID: 2839
	[NullableContext(1)]
	public interface IJEnumerable<[Nullable(0)] out T> : IEnumerable<!0>, IEnumerable where T : JToken
	{
		// Token: 0x170017B2 RID: 6066
		IJEnumerable<JToken> this[object key]
		{
			get;
		}
	}
}
