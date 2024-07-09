using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000ADD RID: 2781
	[NullableContext(1)]
	public interface IContractResolver
	{
		// Token: 0x06006E9F RID: 28319
		JsonContract ResolveContract(Type type);
	}
}
