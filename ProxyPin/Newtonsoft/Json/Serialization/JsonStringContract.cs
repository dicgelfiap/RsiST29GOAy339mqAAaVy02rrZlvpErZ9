using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AF7 RID: 2807
	public class JsonStringContract : JsonPrimitiveContract
	{
		// Token: 0x06007024 RID: 28708 RVA: 0x002203E0 File Offset: 0x002203E0
		[NullableContext(1)]
		public JsonStringContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.String;
		}
	}
}
