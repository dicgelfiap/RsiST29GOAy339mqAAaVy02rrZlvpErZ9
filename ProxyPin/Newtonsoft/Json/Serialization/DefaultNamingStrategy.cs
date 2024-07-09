using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AD4 RID: 2772
	public class DefaultNamingStrategy : NamingStrategy
	{
		// Token: 0x06006E77 RID: 28279 RVA: 0x0021785C File Offset: 0x0021785C
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return name;
		}
	}
}
