using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000ADF RID: 2783
	[NullableContext(1)]
	public interface ISerializationBinder
	{
		// Token: 0x06006EA4 RID: 28324
		Type BindToType([Nullable(2)] string assemblyName, string typeName);

		// Token: 0x06006EA5 RID: 28325
		[NullableContext(2)]
		void BindToName([Nullable(1)] Type serializedType, out string assemblyName, out string typeName);
	}
}
