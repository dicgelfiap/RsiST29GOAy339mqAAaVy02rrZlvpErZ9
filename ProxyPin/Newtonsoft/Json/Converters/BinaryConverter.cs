using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B3C RID: 2876
	[NullableContext(1)]
	[Nullable(0)]
	public class BinaryConverter : JsonConverter
	{
		// Token: 0x0600743E RID: 29758 RVA: 0x0022F3D8 File Offset: 0x0022F3D8
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			byte[] byteArray = this.GetByteArray(value);
			writer.WriteValue(byteArray);
		}

		// Token: 0x0600743F RID: 29759 RVA: 0x0022F408 File Offset: 0x0022F408
		private byte[] GetByteArray(object value)
		{
			if (value.GetType().FullName == "System.Data.Linq.Binary")
			{
				BinaryConverter.EnsureReflectionObject(value.GetType());
				return (byte[])BinaryConverter._reflectionObject.GetValue(value, "ToArray");
			}
			if (value is SqlBinary)
			{
				return ((SqlBinary)value).Value;
			}
			throw new JsonSerializationException("Unexpected value type when writing binary: {0}".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		// Token: 0x06007440 RID: 29760 RVA: 0x0022F488 File Offset: 0x0022F488
		private static void EnsureReflectionObject(Type t)
		{
			if (BinaryConverter._reflectionObject == null)
			{
				BinaryConverter._reflectionObject = ReflectionObject.Create(t, t.GetConstructor(new Type[]
				{
					typeof(byte[])
				}), new string[]
				{
					"ToArray"
				});
			}
		}

		// Token: 0x06007441 RID: 29761 RVA: 0x0022F4C8 File Offset: 0x0022F4C8
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				if (!ReflectionUtils.IsNullable(objectType))
				{
					throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			else
			{
				byte[] array;
				if (reader.TokenType == JsonToken.StartArray)
				{
					array = this.ReadByteArray(reader);
				}
				else
				{
					if (reader.TokenType != JsonToken.String)
					{
						throw JsonSerializationException.Create(reader, "Unexpected token parsing binary. Expected String or StartArray, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
					}
					array = Convert.FromBase64String(reader.Value.ToString());
				}
				Type type = ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
				if (type.FullName == "System.Data.Linq.Binary")
				{
					BinaryConverter.EnsureReflectionObject(type);
					return BinaryConverter._reflectionObject.Creator(new object[]
					{
						array
					});
				}
				if (type == typeof(SqlBinary))
				{
					return new SqlBinary(array);
				}
				throw JsonSerializationException.Create(reader, "Unexpected object type when writing binary: {0}".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
		}

		// Token: 0x06007442 RID: 29762 RVA: 0x0022F5EC File Offset: 0x0022F5EC
		private byte[] ReadByteArray(JsonReader reader)
		{
			List<byte> list = new List<byte>();
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (tokenType != JsonToken.Integer)
					{
						if (tokenType != JsonToken.EndArray)
						{
							throw JsonSerializationException.Create(reader, "Unexpected token when reading bytes: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
						}
						return list.ToArray();
					}
					else
					{
						list.Add(Convert.ToByte(reader.Value, CultureInfo.InvariantCulture));
					}
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end when reading bytes.");
		}

		// Token: 0x06007443 RID: 29763 RVA: 0x0022F680 File Offset: 0x0022F680
		public override bool CanConvert(Type objectType)
		{
			return objectType.FullName == "System.Data.Linq.Binary" || (objectType == typeof(SqlBinary) || objectType == typeof(SqlBinary?));
		}

		// Token: 0x040038CD RID: 14541
		private const string BinaryTypeName = "System.Data.Linq.Binary";

		// Token: 0x040038CE RID: 14542
		private const string BinaryToArrayName = "ToArray";

		// Token: 0x040038CF RID: 14543
		[Nullable(2)]
		private static ReflectionObject _reflectionObject;
	}
}
