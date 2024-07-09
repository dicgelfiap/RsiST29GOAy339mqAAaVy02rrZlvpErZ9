using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B3E RID: 2878
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CustomCreationConverter<[Nullable(2)] T> : JsonConverter
	{
		// Token: 0x06007449 RID: 29769 RVA: 0x0022F77C File Offset: 0x0022F77C
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			throw new NotSupportedException("CustomCreationConverter should only be used while deserializing.");
		}

		// Token: 0x0600744A RID: 29770 RVA: 0x0022F788 File Offset: 0x0022F788
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			T t = this.Create(objectType);
			if (t == null)
			{
				throw new JsonSerializationException("No object created.");
			}
			serializer.Populate(reader, t);
			return t;
		}

		// Token: 0x0600744B RID: 29771
		public abstract T Create(Type objectType);

		// Token: 0x0600744C RID: 29772 RVA: 0x0022F7DC File Offset: 0x0022F7DC
		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x0600744D RID: 29773 RVA: 0x0022F7F0 File Offset: 0x0022F7F0
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
