using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AE1 RID: 2785
	[NullableContext(1)]
	public interface IValueProvider
	{
		// Token: 0x06006EA8 RID: 28328
		void SetValue(object target, [Nullable(2)] object value);

		// Token: 0x06006EA9 RID: 28329
		[return: Nullable(2)]
		object GetValue(object target);
	}
}
