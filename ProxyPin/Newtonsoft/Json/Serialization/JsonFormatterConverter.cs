using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AEC RID: 2796
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonFormatterConverter : IFormatterConverter
	{
		// Token: 0x06006F08 RID: 28424 RVA: 0x002192D0 File Offset: 0x002192D0
		public JsonFormatterConverter(JsonSerializerInternalReader reader, JsonISerializableContract contract, [Nullable(2)] JsonProperty member)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(contract, "contract");
			this._reader = reader;
			this._contract = contract;
			this._member = member;
		}

		// Token: 0x06006F09 RID: 28425 RVA: 0x00219304 File Offset: 0x00219304
		private T GetTokenValue<[Nullable(2)] T>(object value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			return (T)((object)System.Convert.ChangeType(((JValue)value).Value, typeof(T), CultureInfo.InvariantCulture));
		}

		// Token: 0x06006F0A RID: 28426 RVA: 0x00219338 File Offset: 0x00219338
		[return: Nullable(2)]
		public object Convert(object value, Type type)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Value is not a JToken.", "value");
			}
			return this._reader.CreateISerializableItem(jtoken, type, this._contract, this._member);
		}

		// Token: 0x06006F0B RID: 28427 RVA: 0x0021938C File Offset: 0x0021938C
		public object Convert(object value, TypeCode typeCode)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JValue jvalue = value as JValue;
			return System.Convert.ChangeType((jvalue != null) ? jvalue.Value : value, typeCode, CultureInfo.InvariantCulture);
		}

		// Token: 0x06006F0C RID: 28428 RVA: 0x002193CC File Offset: 0x002193CC
		public bool ToBoolean(object value)
		{
			return this.GetTokenValue<bool>(value);
		}

		// Token: 0x06006F0D RID: 28429 RVA: 0x002193D8 File Offset: 0x002193D8
		public byte ToByte(object value)
		{
			return this.GetTokenValue<byte>(value);
		}

		// Token: 0x06006F0E RID: 28430 RVA: 0x002193E4 File Offset: 0x002193E4
		public char ToChar(object value)
		{
			return this.GetTokenValue<char>(value);
		}

		// Token: 0x06006F0F RID: 28431 RVA: 0x002193F0 File Offset: 0x002193F0
		public DateTime ToDateTime(object value)
		{
			return this.GetTokenValue<DateTime>(value);
		}

		// Token: 0x06006F10 RID: 28432 RVA: 0x002193FC File Offset: 0x002193FC
		public decimal ToDecimal(object value)
		{
			return this.GetTokenValue<decimal>(value);
		}

		// Token: 0x06006F11 RID: 28433 RVA: 0x00219408 File Offset: 0x00219408
		public double ToDouble(object value)
		{
			return this.GetTokenValue<double>(value);
		}

		// Token: 0x06006F12 RID: 28434 RVA: 0x00219414 File Offset: 0x00219414
		public short ToInt16(object value)
		{
			return this.GetTokenValue<short>(value);
		}

		// Token: 0x06006F13 RID: 28435 RVA: 0x00219420 File Offset: 0x00219420
		public int ToInt32(object value)
		{
			return this.GetTokenValue<int>(value);
		}

		// Token: 0x06006F14 RID: 28436 RVA: 0x0021942C File Offset: 0x0021942C
		public long ToInt64(object value)
		{
			return this.GetTokenValue<long>(value);
		}

		// Token: 0x06006F15 RID: 28437 RVA: 0x00219438 File Offset: 0x00219438
		public sbyte ToSByte(object value)
		{
			return this.GetTokenValue<sbyte>(value);
		}

		// Token: 0x06006F16 RID: 28438 RVA: 0x00219444 File Offset: 0x00219444
		public float ToSingle(object value)
		{
			return this.GetTokenValue<float>(value);
		}

		// Token: 0x06006F17 RID: 28439 RVA: 0x00219450 File Offset: 0x00219450
		public string ToString(object value)
		{
			return this.GetTokenValue<string>(value);
		}

		// Token: 0x06006F18 RID: 28440 RVA: 0x0021945C File Offset: 0x0021945C
		public ushort ToUInt16(object value)
		{
			return this.GetTokenValue<ushort>(value);
		}

		// Token: 0x06006F19 RID: 28441 RVA: 0x00219468 File Offset: 0x00219468
		public uint ToUInt32(object value)
		{
			return this.GetTokenValue<uint>(value);
		}

		// Token: 0x06006F1A RID: 28442 RVA: 0x00219474 File Offset: 0x00219474
		public ulong ToUInt64(object value)
		{
			return this.GetTokenValue<ulong>(value);
		}

		// Token: 0x04003773 RID: 14195
		private readonly JsonSerializerInternalReader _reader;

		// Token: 0x04003774 RID: 14196
		private readonly JsonISerializableContract _contract;

		// Token: 0x04003775 RID: 14197
		[Nullable(2)]
		private readonly JsonProperty _member;
	}
}
