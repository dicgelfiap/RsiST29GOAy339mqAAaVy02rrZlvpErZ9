using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AED RID: 2797
	public class JsonISerializableContract : JsonContainerContract
	{
		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06006F1B RID: 28443 RVA: 0x00219480 File Offset: 0x00219480
		// (set) Token: 0x06006F1C RID: 28444 RVA: 0x00219488 File Offset: 0x00219488
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public ObjectConstructor<object> ISerializableCreator { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x06006F1D RID: 28445 RVA: 0x00219494 File Offset: 0x00219494
		[NullableContext(1)]
		public JsonISerializableContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Serializable;
		}
	}
}
