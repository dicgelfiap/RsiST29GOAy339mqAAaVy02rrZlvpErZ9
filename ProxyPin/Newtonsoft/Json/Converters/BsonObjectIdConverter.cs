using System;
using System.Globalization;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B3D RID: 2877
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectIdConverter : JsonConverter
	{
		// Token: 0x06007445 RID: 29765 RVA: 0x0022F6E0 File Offset: 0x0022F6E0
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			BsonObjectId bsonObjectId = (BsonObjectId)value;
			BsonWriter bsonWriter = writer as BsonWriter;
			if (bsonWriter != null)
			{
				bsonWriter.WriteObjectId(bsonObjectId.Value);
				return;
			}
			writer.WriteValue(bsonObjectId.Value);
		}

		// Token: 0x06007446 RID: 29766 RVA: 0x0022F720 File Offset: 0x0022F720
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Bytes)
			{
				throw new JsonSerializationException("Expected Bytes but got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			return new BsonObjectId((byte[])reader.Value);
		}

		// Token: 0x06007447 RID: 29767 RVA: 0x0022F760 File Offset: 0x0022F760
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(BsonObjectId);
		}
	}
}
