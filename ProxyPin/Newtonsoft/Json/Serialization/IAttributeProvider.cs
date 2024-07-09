using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000ADC RID: 2780
	[NullableContext(1)]
	public interface IAttributeProvider
	{
		// Token: 0x06006E9D RID: 28317
		IList<Attribute> GetAttributes(bool inherit);

		// Token: 0x06006E9E RID: 28318
		IList<Attribute> GetAttributes(Type attributeType, bool inherit);
	}
}
