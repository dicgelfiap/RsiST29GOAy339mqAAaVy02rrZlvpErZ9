using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B41 RID: 2881
	public abstract class DateTimeConverterBase : JsonConverter
	{
		// Token: 0x06007459 RID: 29785 RVA: 0x0022FE38 File Offset: 0x0022FE38
		[NullableContext(1)]
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?));
		}
	}
}
