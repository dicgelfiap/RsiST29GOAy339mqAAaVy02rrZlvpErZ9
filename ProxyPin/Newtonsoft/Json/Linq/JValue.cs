using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B28 RID: 2856
	[NullableContext(1)]
	[Nullable(0)]
	public class JValue : JToken, IEquatable<JValue>, IFormattable, IComparable, IComparable<JValue>, IConvertible
	{
		// Token: 0x060073BA RID: 29626 RVA: 0x0022C110 File Offset: 0x0022C110
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return AsyncUtils.CompletedTask;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				return writer.WriteCommentAsync((value != null) ? value.ToString() : null, cancellationToken);
			}
			case JTokenType.Integer:
			{
				object value2 = this._value;
				if (value2 is int)
				{
					int value3 = (int)value2;
					return writer.WriteValueAsync(value3, cancellationToken);
				}
				value2 = this._value;
				if (value2 is long)
				{
					long value4 = (long)value2;
					return writer.WriteValueAsync(value4, cancellationToken);
				}
				value2 = this._value;
				if (value2 is ulong)
				{
					ulong value5 = (ulong)value2;
					return writer.WriteValueAsync(value5, cancellationToken);
				}
				value2 = this._value;
				if (value2 is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value2;
					return writer.WriteValueAsync(bigInteger, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToInt64(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.Float:
			{
				object value2 = this._value;
				if (value2 is decimal)
				{
					decimal value6 = (decimal)value2;
					return writer.WriteValueAsync(value6, cancellationToken);
				}
				value2 = this._value;
				if (value2 is double)
				{
					double value7 = (double)value2;
					return writer.WriteValueAsync(value7, cancellationToken);
				}
				value2 = this._value;
				if (value2 is float)
				{
					float value8 = (float)value2;
					return writer.WriteValueAsync(value8, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToDouble(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.String:
			{
				object value9 = this._value;
				return writer.WriteValueAsync((value9 != null) ? value9.ToString() : null, cancellationToken);
			}
			case JTokenType.Boolean:
				return writer.WriteValueAsync(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture), cancellationToken);
			case JTokenType.Null:
				return writer.WriteNullAsync(cancellationToken);
			case JTokenType.Undefined:
				return writer.WriteUndefinedAsync(cancellationToken);
			case JTokenType.Date:
			{
				object value2 = this._value;
				if (value2 is DateTimeOffset)
				{
					DateTimeOffset value10 = (DateTimeOffset)value2;
					return writer.WriteValueAsync(value10, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.Raw:
			{
				object value11 = this._value;
				return writer.WriteRawValueAsync((value11 != null) ? value11.ToString() : null, cancellationToken);
			}
			case JTokenType.Bytes:
				return writer.WriteValueAsync((byte[])this._value, cancellationToken);
			case JTokenType.Guid:
				return writer.WriteValueAsync((this._value != null) ? ((Guid?)this._value) : null, cancellationToken);
			case JTokenType.Uri:
				return writer.WriteValueAsync((Uri)this._value, cancellationToken);
			case JTokenType.TimeSpan:
				return writer.WriteValueAsync((this._value != null) ? ((TimeSpan?)this._value) : null, cancellationToken);
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", this._valueType, "Unexpected token type.");
			}
		}

		// Token: 0x060073BB RID: 29627 RVA: 0x0022C44C File Offset: 0x0022C44C
		[NullableContext(2)]
		internal JValue(object value, JTokenType type)
		{
			this._value = value;
			this._valueType = type;
		}

		// Token: 0x060073BC RID: 29628 RVA: 0x0022C464 File Offset: 0x0022C464
		public JValue(JValue other) : this(other.Value, other.Type)
		{
		}

		// Token: 0x060073BD RID: 29629 RVA: 0x0022C478 File Offset: 0x0022C478
		public JValue(long value) : this(value, JTokenType.Integer)
		{
		}

		// Token: 0x060073BE RID: 29630 RVA: 0x0022C488 File Offset: 0x0022C488
		public JValue(decimal value) : this(value, JTokenType.Float)
		{
		}

		// Token: 0x060073BF RID: 29631 RVA: 0x0022C498 File Offset: 0x0022C498
		public JValue(char value) : this(value, JTokenType.String)
		{
		}

		// Token: 0x060073C0 RID: 29632 RVA: 0x0022C4A8 File Offset: 0x0022C4A8
		[CLSCompliant(false)]
		public JValue(ulong value) : this(value, JTokenType.Integer)
		{
		}

		// Token: 0x060073C1 RID: 29633 RVA: 0x0022C4B8 File Offset: 0x0022C4B8
		public JValue(double value) : this(value, JTokenType.Float)
		{
		}

		// Token: 0x060073C2 RID: 29634 RVA: 0x0022C4C8 File Offset: 0x0022C4C8
		public JValue(float value) : this(value, JTokenType.Float)
		{
		}

		// Token: 0x060073C3 RID: 29635 RVA: 0x0022C4D8 File Offset: 0x0022C4D8
		public JValue(DateTime value) : this(value, JTokenType.Date)
		{
		}

		// Token: 0x060073C4 RID: 29636 RVA: 0x0022C4E8 File Offset: 0x0022C4E8
		public JValue(DateTimeOffset value) : this(value, JTokenType.Date)
		{
		}

		// Token: 0x060073C5 RID: 29637 RVA: 0x0022C4F8 File Offset: 0x0022C4F8
		public JValue(bool value) : this(value, JTokenType.Boolean)
		{
		}

		// Token: 0x060073C6 RID: 29638 RVA: 0x0022C508 File Offset: 0x0022C508
		[NullableContext(2)]
		public JValue(string value) : this(value, JTokenType.String)
		{
		}

		// Token: 0x060073C7 RID: 29639 RVA: 0x0022C514 File Offset: 0x0022C514
		public JValue(Guid value) : this(value, JTokenType.Guid)
		{
		}

		// Token: 0x060073C8 RID: 29640 RVA: 0x0022C524 File Offset: 0x0022C524
		[NullableContext(2)]
		public JValue(Uri value) : this(value, (value != null) ? JTokenType.Uri : JTokenType.Null)
		{
		}

		// Token: 0x060073C9 RID: 29641 RVA: 0x0022C544 File Offset: 0x0022C544
		public JValue(TimeSpan value) : this(value, JTokenType.TimeSpan)
		{
		}

		// Token: 0x060073CA RID: 29642 RVA: 0x0022C554 File Offset: 0x0022C554
		[NullableContext(2)]
		public JValue(object value) : this(value, JValue.GetValueType(null, value))
		{
		}

		// Token: 0x060073CB RID: 29643 RVA: 0x0022C57C File Offset: 0x0022C57C
		internal override bool DeepEquals(JToken node)
		{
			JValue jvalue = node as JValue;
			return jvalue != null && (jvalue == this || JValue.ValuesEquals(this, jvalue));
		}

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x060073CC RID: 29644 RVA: 0x0022C5AC File Offset: 0x0022C5AC
		public override bool HasValues
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060073CD RID: 29645 RVA: 0x0022C5B0 File Offset: 0x0022C5B0
		private static int CompareBigInteger(BigInteger i1, object i2)
		{
			int num = i1.CompareTo(ConvertUtils.ToBigInteger(i2));
			if (num != 0)
			{
				return num;
			}
			if (i2 is decimal)
			{
				decimal num2 = (decimal)i2;
				return 0m.CompareTo(Math.Abs(num2 - Math.Truncate(num2)));
			}
			if (i2 is double || i2 is float)
			{
				double num3 = Convert.ToDouble(i2, CultureInfo.InvariantCulture);
				return 0.0.CompareTo(Math.Abs(num3 - Math.Truncate(num3)));
			}
			return num;
		}

		// Token: 0x060073CE RID: 29646 RVA: 0x0022C64C File Offset: 0x0022C64C
		[NullableContext(2)]
		internal static int Compare(JTokenType valueType, object objA, object objB)
		{
			if (objA == objB)
			{
				return 0;
			}
			if (objB == null)
			{
				return 1;
			}
			if (objA == null)
			{
				return -1;
			}
			switch (valueType)
			{
			case JTokenType.Comment:
			case JTokenType.String:
			case JTokenType.Raw:
			{
				string strA = Convert.ToString(objA, CultureInfo.InvariantCulture);
				string strB = Convert.ToString(objB, CultureInfo.InvariantCulture);
				return string.CompareOrdinal(strA, strB);
			}
			case JTokenType.Integer:
				if (objA is BigInteger)
				{
					BigInteger i = (BigInteger)objA;
					return JValue.CompareBigInteger(i, objB);
				}
				if (objB is BigInteger)
				{
					BigInteger i2 = (BigInteger)objB;
					return -JValue.CompareBigInteger(i2, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				if (objA is float || objB is float || objA is double || objB is double)
				{
					return JValue.CompareFloat(objA, objB);
				}
				return Convert.ToInt64(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToInt64(objB, CultureInfo.InvariantCulture));
			case JTokenType.Float:
				if (objA is BigInteger)
				{
					BigInteger i3 = (BigInteger)objA;
					return JValue.CompareBigInteger(i3, objB);
				}
				if (objB is BigInteger)
				{
					BigInteger i4 = (BigInteger)objB;
					return -JValue.CompareBigInteger(i4, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				return JValue.CompareFloat(objA, objB);
			case JTokenType.Boolean:
			{
				bool flag = Convert.ToBoolean(objA, CultureInfo.InvariantCulture);
				bool value = Convert.ToBoolean(objB, CultureInfo.InvariantCulture);
				return flag.CompareTo(value);
			}
			case JTokenType.Date:
			{
				if (objA is DateTime)
				{
					DateTime dateTime = (DateTime)objA;
					DateTime value2;
					if (objB is DateTimeOffset)
					{
						value2 = ((DateTimeOffset)objB).DateTime;
					}
					else
					{
						value2 = Convert.ToDateTime(objB, CultureInfo.InvariantCulture);
					}
					return dateTime.CompareTo(value2);
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)objA;
				DateTimeOffset other;
				if (objB is DateTimeOffset)
				{
					other = (DateTimeOffset)objB;
				}
				else
				{
					other = new DateTimeOffset(Convert.ToDateTime(objB, CultureInfo.InvariantCulture));
				}
				return dateTimeOffset.CompareTo(other);
			}
			case JTokenType.Bytes:
			{
				byte[] array = objB as byte[];
				if (array == null)
				{
					throw new ArgumentException("Object must be of type byte[].");
				}
				return MiscellaneousUtils.ByteArrayCompare(objA as byte[], array);
			}
			case JTokenType.Guid:
			{
				if (!(objB is Guid))
				{
					throw new ArgumentException("Object must be of type Guid.");
				}
				Guid guid = (Guid)objA;
				Guid value3 = (Guid)objB;
				return guid.CompareTo(value3);
			}
			case JTokenType.Uri:
			{
				Uri uri = objB as Uri;
				if (uri == null)
				{
					throw new ArgumentException("Object must be of type Uri.");
				}
				Uri uri2 = (Uri)objA;
				return Comparer<string>.Default.Compare(uri2.ToString(), uri.ToString());
			}
			case JTokenType.TimeSpan:
			{
				if (!(objB is TimeSpan))
				{
					throw new ArgumentException("Object must be of type TimeSpan.");
				}
				TimeSpan timeSpan = (TimeSpan)objA;
				TimeSpan value4 = (TimeSpan)objB;
				return timeSpan.CompareTo(value4);
			}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("valueType", valueType, "Unexpected value type: {0}".FormatWith(CultureInfo.InvariantCulture, valueType));
		}

		// Token: 0x060073CF RID: 29647 RVA: 0x0022C9D0 File Offset: 0x0022C9D0
		private static int CompareFloat(object objA, object objB)
		{
			double d = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
			double num = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
			if (MathUtils.ApproxEquals(d, num))
			{
				return 0;
			}
			return d.CompareTo(num);
		}

		// Token: 0x060073D0 RID: 29648 RVA: 0x0022CA10 File Offset: 0x0022CA10
		[NullableContext(2)]
		private static bool Operation(ExpressionType operation, object objA, object objB, out object result)
		{
			if ((objA is string || objB is string) && (operation == ExpressionType.Add || operation == ExpressionType.AddAssign))
			{
				result = ((objA != null) ? objA.ToString() : null) + ((objB != null) ? objB.ToString() : null);
				return true;
			}
			if (objA is BigInteger || objB is BigInteger)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				BigInteger bigInteger = ConvertUtils.ToBigInteger(objA);
				BigInteger bigInteger2 = ConvertUtils.ToBigInteger(objB);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_48F;
							}
							goto IL_120;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_110;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_48F;
						}
						goto IL_100;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_48F;
						}
						goto IL_120;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_110;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_48F;
					}
					goto IL_100;
				}
				result = bigInteger + bigInteger2;
				return true;
				IL_100:
				result = bigInteger - bigInteger2;
				return true;
				IL_110:
				result = bigInteger * bigInteger2;
				return true;
				IL_120:
				result = bigInteger / bigInteger2;
				return true;
			}
			else if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				decimal d = Convert.ToDecimal(objA, CultureInfo.InvariantCulture);
				decimal d2 = Convert.ToDecimal(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_48F;
							}
							goto IL_21F;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_20F;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_48F;
						}
						goto IL_1FF;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_48F;
						}
						goto IL_21F;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_20F;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_48F;
					}
					goto IL_1FF;
				}
				result = d + d2;
				return true;
				IL_1FF:
				result = d - d2;
				return true;
				IL_20F:
				result = d * d2;
				return true;
				IL_21F:
				result = d / d2;
				return true;
			}
			else if (objA is float || objB is float || objA is double || objB is double)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				double num = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
				double num2 = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_48F;
							}
							goto IL_31A;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_30C;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_48F;
						}
						goto IL_2FE;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_48F;
						}
						goto IL_31A;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_30C;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_48F;
					}
					goto IL_2FE;
				}
				result = num + num2;
				return true;
				IL_2FE:
				result = num - num2;
				return true;
				IL_30C:
				result = num * num2;
				return true;
				IL_31A:
				result = num / num2;
				return true;
			}
			else if (objA is int || objA is uint || objA is long || objA is short || objA is ushort || objA is sbyte || objA is byte || objB is int || objB is uint || objB is long || objB is short || objB is ushort || objB is sbyte || objB is byte)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				long num3 = Convert.ToInt64(objA, CultureInfo.InvariantCulture);
				long num4 = Convert.ToInt64(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_48F;
							}
							goto IL_481;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_473;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_48F;
						}
						goto IL_465;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_48F;
						}
						goto IL_481;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_473;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_48F;
					}
					goto IL_465;
				}
				result = num3 + num4;
				return true;
				IL_465:
				result = num3 - num4;
				return true;
				IL_473:
				result = num3 * num4;
				return true;
				IL_481:
				result = num3 / num4;
				return true;
			}
			IL_48F:
			result = null;
			return false;
		}

		// Token: 0x060073D1 RID: 29649 RVA: 0x0022CEB4 File Offset: 0x0022CEB4
		internal override JToken CloneToken()
		{
			return new JValue(this);
		}

		// Token: 0x060073D2 RID: 29650 RVA: 0x0022CEBC File Offset: 0x0022CEBC
		public static JValue CreateComment([Nullable(2)] string value)
		{
			return new JValue(value, JTokenType.Comment);
		}

		// Token: 0x060073D3 RID: 29651 RVA: 0x0022CEC8 File Offset: 0x0022CEC8
		public static JValue CreateString([Nullable(2)] string value)
		{
			return new JValue(value, JTokenType.String);
		}

		// Token: 0x060073D4 RID: 29652 RVA: 0x0022CED4 File Offset: 0x0022CED4
		public static JValue CreateNull()
		{
			return new JValue(null, JTokenType.Null);
		}

		// Token: 0x060073D5 RID: 29653 RVA: 0x0022CEE0 File Offset: 0x0022CEE0
		public static JValue CreateUndefined()
		{
			return new JValue(null, JTokenType.Undefined);
		}

		// Token: 0x060073D6 RID: 29654 RVA: 0x0022CEEC File Offset: 0x0022CEEC
		[NullableContext(2)]
		private static JTokenType GetValueType(JTokenType? current, object value)
		{
			if (value == null)
			{
				return JTokenType.Null;
			}
			if (value == DBNull.Value)
			{
				return JTokenType.Null;
			}
			if (value is string)
			{
				return JValue.GetStringValueType(current);
			}
			if (value is long || value is int || value is short || value is sbyte || value is ulong || value is uint || value is ushort || value is byte)
			{
				return JTokenType.Integer;
			}
			if (value is Enum)
			{
				return JTokenType.Integer;
			}
			if (value is BigInteger)
			{
				return JTokenType.Integer;
			}
			if (value is double || value is float || value is decimal)
			{
				return JTokenType.Float;
			}
			if (value is DateTime)
			{
				return JTokenType.Date;
			}
			if (value is DateTimeOffset)
			{
				return JTokenType.Date;
			}
			if (value is byte[])
			{
				return JTokenType.Bytes;
			}
			if (value is bool)
			{
				return JTokenType.Boolean;
			}
			if (value is Guid)
			{
				return JTokenType.Guid;
			}
			if (value is Uri)
			{
				return JTokenType.Uri;
			}
			if (value is TimeSpan)
			{
				return JTokenType.TimeSpan;
			}
			throw new ArgumentException("Could not determine JSON object type for type {0}.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		// Token: 0x060073D7 RID: 29655 RVA: 0x0022D03C File Offset: 0x0022D03C
		private static JTokenType GetStringValueType(JTokenType? current)
		{
			if (current == null)
			{
				return JTokenType.String;
			}
			JTokenType valueOrDefault = current.GetValueOrDefault();
			if (valueOrDefault == JTokenType.Comment || valueOrDefault == JTokenType.String || valueOrDefault == JTokenType.Raw)
			{
				return current.GetValueOrDefault();
			}
			return JTokenType.String;
		}

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x060073D8 RID: 29656 RVA: 0x0022D084 File Offset: 0x0022D084
		public override JTokenType Type
		{
			get
			{
				return this._valueType;
			}
		}

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x060073D9 RID: 29657 RVA: 0x0022D08C File Offset: 0x0022D08C
		// (set) Token: 0x060073DA RID: 29658 RVA: 0x0022D094 File Offset: 0x0022D094
		[Nullable(2)]
		public new object Value
		{
			[NullableContext(2)]
			get
			{
				return this._value;
			}
			[NullableContext(2)]
			set
			{
				object value2 = this._value;
				Type left = (value2 != null) ? value2.GetType() : null;
				Type right = (value != null) ? value.GetType() : null;
				if (left != right)
				{
					this._valueType = JValue.GetValueType(new JTokenType?(this._valueType), value);
				}
				this._value = value;
			}
		}

		// Token: 0x060073DB RID: 29659 RVA: 0x0022D0FC File Offset: 0x0022D0FC
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				writer.WriteComment((value != null) ? value.ToString() : null);
				return;
			}
			case JTokenType.Integer:
			{
				object value2 = this._value;
				if (value2 is int)
				{
					int value3 = (int)value2;
					writer.WriteValue(value3);
					return;
				}
				value2 = this._value;
				if (value2 is long)
				{
					long value4 = (long)value2;
					writer.WriteValue(value4);
					return;
				}
				value2 = this._value;
				if (value2 is ulong)
				{
					ulong value5 = (ulong)value2;
					writer.WriteValue(value5);
					return;
				}
				value2 = this._value;
				if (value2 is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value2;
					writer.WriteValue(bigInteger);
					return;
				}
				writer.WriteValue(Convert.ToInt64(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Float:
			{
				object value2 = this._value;
				if (value2 is decimal)
				{
					decimal value6 = (decimal)value2;
					writer.WriteValue(value6);
					return;
				}
				value2 = this._value;
				if (value2 is double)
				{
					double value7 = (double)value2;
					writer.WriteValue(value7);
					return;
				}
				value2 = this._value;
				if (value2 is float)
				{
					float value8 = (float)value2;
					writer.WriteValue(value8);
					return;
				}
				writer.WriteValue(Convert.ToDouble(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.String:
			{
				object value9 = this._value;
				writer.WriteValue((value9 != null) ? value9.ToString() : null);
				return;
			}
			case JTokenType.Boolean:
				writer.WriteValue(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Null:
				writer.WriteNull();
				return;
			case JTokenType.Undefined:
				writer.WriteUndefined();
				return;
			case JTokenType.Date:
			{
				object value2 = this._value;
				if (value2 is DateTimeOffset)
				{
					DateTimeOffset value10 = (DateTimeOffset)value2;
					writer.WriteValue(value10);
					return;
				}
				writer.WriteValue(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Raw:
			{
				object value11 = this._value;
				writer.WriteRawValue((value11 != null) ? value11.ToString() : null);
				return;
			}
			case JTokenType.Bytes:
				writer.WriteValue((byte[])this._value);
				return;
			case JTokenType.Guid:
				writer.WriteValue((this._value != null) ? ((Guid?)this._value) : null);
				return;
			case JTokenType.Uri:
				writer.WriteValue((Uri)this._value);
				return;
			case JTokenType.TimeSpan:
				writer.WriteValue((this._value != null) ? ((TimeSpan?)this._value) : null);
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", this._valueType, "Unexpected token type.");
			}
		}

		// Token: 0x060073DC RID: 29660 RVA: 0x0022D41C File Offset: 0x0022D41C
		internal override int GetDeepHashCode()
		{
			int num = (this._value != null) ? this._value.GetHashCode() : 0;
			int valueType = (int)this._valueType;
			return valueType.GetHashCode() ^ num;
		}

		// Token: 0x060073DD RID: 29661 RVA: 0x0022D45C File Offset: 0x0022D45C
		private static bool ValuesEquals(JValue v1, JValue v2)
		{
			return v1 == v2 || (v1._valueType == v2._valueType && JValue.Compare(v1._valueType, v1._value, v2._value) == 0);
		}

		// Token: 0x060073DE RID: 29662 RVA: 0x0022D494 File Offset: 0x0022D494
		public bool Equals([AllowNull] JValue other)
		{
			return other != null && JValue.ValuesEquals(this, other);
		}

		// Token: 0x060073DF RID: 29663 RVA: 0x0022D4A8 File Offset: 0x0022D4A8
		public override bool Equals(object obj)
		{
			JValue jvalue = obj as JValue;
			return jvalue != null && this.Equals(jvalue);
		}

		// Token: 0x060073E0 RID: 29664 RVA: 0x0022D4D0 File Offset: 0x0022D4D0
		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return this._value.GetHashCode();
		}

		// Token: 0x060073E1 RID: 29665 RVA: 0x0022D4EC File Offset: 0x0022D4EC
		public override string ToString()
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			return this._value.ToString();
		}

		// Token: 0x060073E2 RID: 29666 RVA: 0x0022D50C File Offset: 0x0022D50C
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x060073E3 RID: 29667 RVA: 0x0022D51C File Offset: 0x0022D51C
		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		// Token: 0x060073E4 RID: 29668 RVA: 0x0022D528 File Offset: 0x0022D528
		public string ToString([Nullable(2)] string format, IFormatProvider formatProvider)
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			IFormattable formattable = this._value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return this._value.ToString();
		}

		// Token: 0x060073E5 RID: 29669 RVA: 0x0022D570 File Offset: 0x0022D570
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JValue>(parameter, this, new JValue.JValueDynamicProxy());
		}

		// Token: 0x060073E6 RID: 29670 RVA: 0x0022D580 File Offset: 0x0022D580
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			JValue jvalue = obj as JValue;
			object objB;
			JTokenType valueType;
			if (jvalue != null)
			{
				objB = jvalue.Value;
				valueType = ((this._valueType == JTokenType.String && this._valueType != jvalue._valueType) ? jvalue._valueType : this._valueType);
			}
			else
			{
				objB = obj;
				valueType = this._valueType;
			}
			return JValue.Compare(valueType, this._value, objB);
		}

		// Token: 0x060073E7 RID: 29671 RVA: 0x0022D5F8 File Offset: 0x0022D5F8
		public int CompareTo(JValue obj)
		{
			if (obj == null)
			{
				return 1;
			}
			return JValue.Compare((this._valueType == JTokenType.String && this._valueType != obj._valueType) ? obj._valueType : this._valueType, this._value, obj._value);
		}

		// Token: 0x060073E8 RID: 29672 RVA: 0x0022D650 File Offset: 0x0022D650
		TypeCode IConvertible.GetTypeCode()
		{
			if (this._value == null)
			{
				return TypeCode.Empty;
			}
			IConvertible convertible = this._value as IConvertible;
			if (convertible != null)
			{
				return convertible.GetTypeCode();
			}
			return TypeCode.Object;
		}

		// Token: 0x060073E9 RID: 29673 RVA: 0x0022D688 File Offset: 0x0022D688
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return (bool)this;
		}

		// Token: 0x060073EA RID: 29674 RVA: 0x0022D690 File Offset: 0x0022D690
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return (char)this;
		}

		// Token: 0x060073EB RID: 29675 RVA: 0x0022D698 File Offset: 0x0022D698
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return (sbyte)this;
		}

		// Token: 0x060073EC RID: 29676 RVA: 0x0022D6A0 File Offset: 0x0022D6A0
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return (byte)this;
		}

		// Token: 0x060073ED RID: 29677 RVA: 0x0022D6A8 File Offset: 0x0022D6A8
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return (short)this;
		}

		// Token: 0x060073EE RID: 29678 RVA: 0x0022D6B0 File Offset: 0x0022D6B0
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return (ushort)this;
		}

		// Token: 0x060073EF RID: 29679 RVA: 0x0022D6B8 File Offset: 0x0022D6B8
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		// Token: 0x060073F0 RID: 29680 RVA: 0x0022D6C0 File Offset: 0x0022D6C0
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return (uint)this;
		}

		// Token: 0x060073F1 RID: 29681 RVA: 0x0022D6C8 File Offset: 0x0022D6C8
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return (long)this;
		}

		// Token: 0x060073F2 RID: 29682 RVA: 0x0022D6D0 File Offset: 0x0022D6D0
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return (ulong)this;
		}

		// Token: 0x060073F3 RID: 29683 RVA: 0x0022D6D8 File Offset: 0x0022D6D8
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return (float)this;
		}

		// Token: 0x060073F4 RID: 29684 RVA: 0x0022D6E4 File Offset: 0x0022D6E4
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return (double)this;
		}

		// Token: 0x060073F5 RID: 29685 RVA: 0x0022D6F0 File Offset: 0x0022D6F0
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return (decimal)this;
		}

		// Token: 0x060073F6 RID: 29686 RVA: 0x0022D6F8 File Offset: 0x0022D6F8
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return (DateTime)this;
		}

		// Token: 0x060073F7 RID: 29687 RVA: 0x0022D700 File Offset: 0x0022D700
		[return: Nullable(2)]
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return base.ToObject(conversionType);
		}

		// Token: 0x0400389E RID: 14494
		private JTokenType _valueType;

		// Token: 0x0400389F RID: 14495
		[Nullable(2)]
		private object _value;

		// Token: 0x0200112D RID: 4397
		[Nullable(new byte[]
		{
			0,
			1
		})]
		private class JValueDynamicProxy : DynamicProxy<JValue>
		{
			// Token: 0x06009279 RID: 37497 RVA: 0x002C0070 File Offset: 0x002C0070
			public override bool TryConvert(JValue instance, ConvertBinder binder, [Nullable(2)] [NotNullWhen(true)] out object result)
			{
				if (binder.Type == typeof(JValue) || binder.Type == typeof(JToken))
				{
					result = instance;
					return true;
				}
				object value = instance.Value;
				if (value == null)
				{
					result = null;
					return ReflectionUtils.IsNullable(binder.Type);
				}
				result = ConvertUtils.Convert(value, CultureInfo.InvariantCulture, binder.Type);
				return true;
			}

			// Token: 0x0600927A RID: 37498 RVA: 0x002C00EC File Offset: 0x002C00EC
			public override bool TryBinaryOperation(JValue instance, BinaryOperationBinder binder, object arg, [Nullable(2)] [NotNullWhen(true)] out object result)
			{
				JValue jvalue = arg as JValue;
				object objB = (jvalue != null) ? jvalue.Value : arg;
				ExpressionType operation = binder.Operation;
				if (operation <= ExpressionType.NotEqual)
				{
					if (operation <= ExpressionType.LessThanOrEqual)
					{
						if (operation != ExpressionType.Add)
						{
							switch (operation)
							{
							case ExpressionType.Divide:
								break;
							case ExpressionType.Equal:
								result = (JValue.Compare(instance.Type, instance.Value, objB) == 0);
								return true;
							case ExpressionType.ExclusiveOr:
							case ExpressionType.Invoke:
							case ExpressionType.Lambda:
							case ExpressionType.LeftShift:
								goto IL_1A2;
							case ExpressionType.GreaterThan:
								result = (JValue.Compare(instance.Type, instance.Value, objB) > 0);
								return true;
							case ExpressionType.GreaterThanOrEqual:
								result = (JValue.Compare(instance.Type, instance.Value, objB) >= 0);
								return true;
							case ExpressionType.LessThan:
								result = (JValue.Compare(instance.Type, instance.Value, objB) < 0);
								return true;
							case ExpressionType.LessThanOrEqual:
								result = (JValue.Compare(instance.Type, instance.Value, objB) <= 0);
								return true;
							default:
								goto IL_1A2;
							}
						}
					}
					else if (operation != ExpressionType.Multiply)
					{
						if (operation != ExpressionType.NotEqual)
						{
							goto IL_1A2;
						}
						result = (JValue.Compare(instance.Type, instance.Value, objB) != 0);
						return true;
					}
				}
				else if (operation <= ExpressionType.AddAssign)
				{
					if (operation != ExpressionType.Subtract && operation != ExpressionType.AddAssign)
					{
						goto IL_1A2;
					}
				}
				else if (operation != ExpressionType.DivideAssign && operation != ExpressionType.MultiplyAssign && operation != ExpressionType.SubtractAssign)
				{
					goto IL_1A2;
				}
				if (JValue.Operation(binder.Operation, instance.Value, objB, out result))
				{
					result = new JValue(result);
					return true;
				}
				IL_1A2:
				result = null;
				return false;
			}
		}
	}
}
