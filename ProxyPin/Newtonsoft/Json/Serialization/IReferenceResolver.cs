using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000ADE RID: 2782
	[NullableContext(1)]
	public interface IReferenceResolver
	{
		// Token: 0x06006EA0 RID: 28320
		object ResolveReference(object context, string reference);

		// Token: 0x06006EA1 RID: 28321
		string GetReference(object context, object value);

		// Token: 0x06006EA2 RID: 28322
		bool IsReferenced(object context, object value);

		// Token: 0x06006EA3 RID: 28323
		void AddReference(object context, string reference, object value);
	}
}
