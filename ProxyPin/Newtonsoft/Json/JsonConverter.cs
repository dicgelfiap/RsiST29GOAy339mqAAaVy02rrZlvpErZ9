using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000A79 RID: 2681
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonConverter
	{
		// Token: 0x060068A6 RID: 26790
		public abstract void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer);

		// Token: 0x060068A7 RID: 26791
		[return: Nullable(2)]
		public abstract object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer);

		// Token: 0x060068A8 RID: 26792
		public abstract bool CanConvert(Type objectType);

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x060068A9 RID: 26793 RVA: 0x001FC0C4 File Offset: 0x001FC0C4
		public virtual bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x060068AA RID: 26794 RVA: 0x001FC0C8 File Offset: 0x001FC0C8
		public virtual bool CanWrite
		{
			get
			{
				return true;
			}
		}
	}
}
