using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AEE RID: 2798
	public class JsonLinqContract : JsonContract
	{
		// Token: 0x06006F1E RID: 28446 RVA: 0x002194A4 File Offset: 0x002194A4
		[NullableContext(1)]
		public JsonLinqContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Linq;
		}
	}
}
