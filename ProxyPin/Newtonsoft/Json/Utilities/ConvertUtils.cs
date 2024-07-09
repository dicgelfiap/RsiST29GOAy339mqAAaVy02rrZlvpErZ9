﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AA8 RID: 2728
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ConvertUtils
	{
		// Token: 0x06006C8C RID: 27788 RVA: 0x0020BC48 File Offset: 0x0020BC48
		public static PrimitiveTypeCode GetTypeCode(Type t)
		{
			bool flag;
			return ConvertUtils.GetTypeCode(t, out flag);
		}

		// Token: 0x06006C8D RID: 27789 RVA: 0x0020BC64 File Offset: 0x0020BC64
		public static PrimitiveTypeCode GetTypeCode(Type t, out bool isEnum)
		{
			PrimitiveTypeCode result;
			if (ConvertUtils.TypeCodeMap.TryGetValue(t, out result))
			{
				isEnum = false;
				return result;
			}
			if (t.IsEnum())
			{
				isEnum = true;
				return ConvertUtils.GetTypeCode(Enum.GetUnderlyingType(t));
			}
			if (ReflectionUtils.IsNullableType(t))
			{
				Type underlyingType = Nullable.GetUnderlyingType(t);
				if (underlyingType.IsEnum())
				{
					Type t2 = typeof(Nullable<>).MakeGenericType(new Type[]
					{
						Enum.GetUnderlyingType(underlyingType)
					});
					isEnum = true;
					return ConvertUtils.GetTypeCode(t2);
				}
			}
			isEnum = false;
			return PrimitiveTypeCode.Object;
		}

		// Token: 0x06006C8E RID: 27790 RVA: 0x0020BCF0 File Offset: 0x0020BCF0
		public static TypeInformation GetTypeInformation(IConvertible convertable)
		{
			return ConvertUtils.PrimitiveTypeCodes[(int)convertable.GetTypeCode()];
		}

		// Token: 0x06006C8F RID: 27791 RVA: 0x0020BD04 File Offset: 0x0020BD04
		public static bool IsConvertible(Type t)
		{
			return typeof(IConvertible).IsAssignableFrom(t);
		}

		// Token: 0x06006C90 RID: 27792 RVA: 0x0020BD18 File Offset: 0x0020BD18
		public static TimeSpan ParseTimeSpan(string input)
		{
			return TimeSpan.Parse(input, CultureInfo.InvariantCulture);
		}

		// Token: 0x06006C91 RID: 27793 RVA: 0x0020BD28 File Offset: 0x0020BD28
		[NullableContext(2)]
		private static Func<object, object> CreateCastConverter([Nullable(new byte[]
		{
			0,
			1,
			1
		})] StructMultiKey<Type, Type> t)
		{
			Type value = t.Value1;
			Type value2 = t.Value2;
			MethodInfo method;
			if ((method = value2.GetMethod("op_Implicit", new Type[]
			{
				value
			})) == null)
			{
				method = value2.GetMethod("op_Explicit", new Type[]
				{
					value
				});
			}
			MethodInfo methodInfo = method;
			if (methodInfo == null)
			{
				return null;
			}
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
			return (object o) => call(null, new object[]
			{
				o
			});
		}

		// Token: 0x06006C92 RID: 27794 RVA: 0x0020BDAC File Offset: 0x0020BDAC
		internal static BigInteger ToBigInteger(object value)
		{
			if (value is BigInteger)
			{
				return (BigInteger)value;
			}
			string text = value as string;
			if (text != null)
			{
				return BigInteger.Parse(text, CultureInfo.InvariantCulture);
			}
			if (value is float)
			{
				float value2 = (float)value;
				return new BigInteger(value2);
			}
			if (value is double)
			{
				double value3 = (double)value;
				return new BigInteger(value3);
			}
			if (value is decimal)
			{
				decimal value4 = (decimal)value;
				return new BigInteger(value4);
			}
			if (value is int)
			{
				int value5 = (int)value;
				return new BigInteger(value5);
			}
			if (value is long)
			{
				long value6 = (long)value;
				return new BigInteger(value6);
			}
			if (value is uint)
			{
				uint value7 = (uint)value;
				return new BigInteger(value7);
			}
			if (value is ulong)
			{
				ulong value8 = (ulong)value;
				return new BigInteger(value8);
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				return new BigInteger(array);
			}
			throw new InvalidCastException("Cannot convert {0} to BigInteger.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		// Token: 0x06006C93 RID: 27795 RVA: 0x0020BED4 File Offset: 0x0020BED4
		public static object FromBigInteger(BigInteger i, Type targetType)
		{
			if (targetType == typeof(decimal))
			{
				return (decimal)i;
			}
			if (targetType == typeof(double))
			{
				return (double)i;
			}
			if (targetType == typeof(float))
			{
				return (float)i;
			}
			if (targetType == typeof(ulong))
			{
				return (ulong)i;
			}
			if (targetType == typeof(bool))
			{
				return i != 0L;
			}
			object result;
			try
			{
				result = System.Convert.ChangeType((long)i, targetType, CultureInfo.InvariantCulture);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("Can not convert from BigInteger to {0}.".FormatWith(CultureInfo.InvariantCulture, targetType), innerException);
			}
			return result;
		}

		// Token: 0x06006C94 RID: 27796 RVA: 0x0020BFD0 File Offset: 0x0020BFD0
		public static object Convert(object initialValue, CultureInfo culture, Type targetType)
		{
			object result;
			switch (ConvertUtils.TryConvertInternal(initialValue, culture, targetType, out result))
			{
			case ConvertUtils.ConvertResult.Success:
				return result;
			case ConvertUtils.ConvertResult.CannotConvertNull:
				throw new Exception("Can not convert null {0} into non-nullable {1}.".FormatWith(CultureInfo.InvariantCulture, initialValue.GetType(), targetType));
			case ConvertUtils.ConvertResult.NotInstantiableType:
				throw new ArgumentException("Target type {0} is not a value type or a non-abstract class.".FormatWith(CultureInfo.InvariantCulture, targetType), "targetType");
			case ConvertUtils.ConvertResult.NoValidConversion:
				throw new InvalidOperationException("Can not convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, initialValue.GetType(), targetType));
			default:
				throw new InvalidOperationException("Unexpected conversion result.");
			}
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x0020C068 File Offset: 0x0020C068
		private static bool TryConvert([Nullable(2)] object initialValue, CultureInfo culture, Type targetType, [Nullable(2)] out object value)
		{
			bool result;
			try
			{
				if (ConvertUtils.TryConvertInternal(initialValue, culture, targetType, out value) == ConvertUtils.ConvertResult.Success)
				{
					result = true;
				}
				else
				{
					value = null;
					result = false;
				}
			}
			catch
			{
				value = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06006C96 RID: 27798 RVA: 0x0020C0B0 File Offset: 0x0020C0B0
		private static ConvertUtils.ConvertResult TryConvertInternal([Nullable(2)] object initialValue, CultureInfo culture, Type targetType, [Nullable(2)] out object value)
		{
			if (initialValue == null)
			{
				throw new ArgumentNullException("initialValue");
			}
			if (ReflectionUtils.IsNullableType(targetType))
			{
				targetType = Nullable.GetUnderlyingType(targetType);
			}
			Type type = initialValue.GetType();
			if (targetType == type)
			{
				value = initialValue;
				return ConvertUtils.ConvertResult.Success;
			}
			if (ConvertUtils.IsConvertible(initialValue.GetType()) && ConvertUtils.IsConvertible(targetType))
			{
				if (targetType.IsEnum())
				{
					if (initialValue is string)
					{
						value = Enum.Parse(targetType, initialValue.ToString(), true);
						return ConvertUtils.ConvertResult.Success;
					}
					if (ConvertUtils.IsInteger(initialValue))
					{
						value = Enum.ToObject(targetType, initialValue);
						return ConvertUtils.ConvertResult.Success;
					}
				}
				value = System.Convert.ChangeType(initialValue, targetType, culture);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is DateTime)
			{
				DateTime dateTime = (DateTime)initialValue;
				if (targetType == typeof(DateTimeOffset))
				{
					value = new DateTimeOffset(dateTime);
					return ConvertUtils.ConvertResult.Success;
				}
			}
			byte[] array = initialValue as byte[];
			if (array != null && targetType == typeof(Guid))
			{
				value = new Guid(array);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is Guid)
			{
				Guid guid = (Guid)initialValue;
				if (targetType == typeof(byte[]))
				{
					value = guid.ToByteArray();
					return ConvertUtils.ConvertResult.Success;
				}
			}
			string text = initialValue as string;
			if (text != null)
			{
				if (targetType == typeof(Guid))
				{
					value = new Guid(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(Uri))
				{
					value = new Uri(text, UriKind.RelativeOrAbsolute);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(TimeSpan))
				{
					value = ConvertUtils.ParseTimeSpan(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(byte[]))
				{
					value = System.Convert.FromBase64String(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(Version))
				{
					Version version;
					if (ConvertUtils.VersionTryParse(text, out version))
					{
						value = version;
						return ConvertUtils.ConvertResult.Success;
					}
					value = null;
					return ConvertUtils.ConvertResult.NoValidConversion;
				}
				else if (typeof(Type).IsAssignableFrom(targetType))
				{
					value = Type.GetType(text, true);
					return ConvertUtils.ConvertResult.Success;
				}
			}
			if (targetType == typeof(BigInteger))
			{
				value = ConvertUtils.ToBigInteger(initialValue);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is BigInteger)
			{
				BigInteger i = (BigInteger)initialValue;
				value = ConvertUtils.FromBigInteger(i, targetType);
				return ConvertUtils.ConvertResult.Success;
			}
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter != null && converter.CanConvertTo(targetType))
			{
				value = converter.ConvertTo(null, culture, initialValue, targetType);
				return ConvertUtils.ConvertResult.Success;
			}
			TypeConverter converter2 = TypeDescriptor.GetConverter(targetType);
			if (converter2 != null && converter2.CanConvertFrom(type))
			{
				value = converter2.ConvertFrom(null, culture, initialValue);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue == DBNull.Value)
			{
				if (ReflectionUtils.IsNullable(targetType))
				{
					value = ConvertUtils.EnsureTypeAssignable(null, type, targetType);
					return ConvertUtils.ConvertResult.Success;
				}
				value = null;
				return ConvertUtils.ConvertResult.CannotConvertNull;
			}
			else
			{
				if (targetType.IsInterface() || targetType.IsGenericTypeDefinition() || targetType.IsAbstract())
				{
					value = null;
					return ConvertUtils.ConvertResult.NotInstantiableType;
				}
				value = null;
				return ConvertUtils.ConvertResult.NoValidConversion;
			}
		}

		// Token: 0x06006C97 RID: 27799 RVA: 0x0020C3CC File Offset: 0x0020C3CC
		[return: Nullable(2)]
		public static object ConvertOrCast([Nullable(2)] object initialValue, CultureInfo culture, Type targetType)
		{
			if (targetType == typeof(object))
			{
				return initialValue;
			}
			if (initialValue == null && ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			object result;
			if (ConvertUtils.TryConvert(initialValue, culture, targetType, out result))
			{
				return result;
			}
			return ConvertUtils.EnsureTypeAssignable(initialValue, ReflectionUtils.GetObjectType(initialValue), targetType);
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x0020C428 File Offset: 0x0020C428
		[return: Nullable(2)]
		private static object EnsureTypeAssignable([Nullable(2)] object value, Type initialType, Type targetType)
		{
			if (value != null)
			{
				Type type = value.GetType();
				if (targetType.IsAssignableFrom(type))
				{
					return value;
				}
				Func<object, object> func = ConvertUtils.CastConverters.Get(new StructMultiKey<Type, Type>(type, targetType));
				if (func != null)
				{
					return func(value);
				}
			}
			else if (ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			throw new ArgumentException("Could not cast or convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, ((initialType != null) ? initialType.ToString() : null) ?? "{null}", targetType));
		}

		// Token: 0x06006C99 RID: 27801 RVA: 0x0020C4B4 File Offset: 0x0020C4B4
		public static bool VersionTryParse(string input, [Nullable(2)] [NotNullWhen(true)] out Version result)
		{
			return Version.TryParse(input, out result);
		}

		// Token: 0x06006C9A RID: 27802 RVA: 0x0020C4C0 File Offset: 0x0020C4C0
		public static bool IsInteger(object value)
		{
			switch (ConvertUtils.GetTypeCode(value.GetType()))
			{
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
				return true;
			}
			return false;
		}

		// Token: 0x06006C9B RID: 27803 RVA: 0x0020C52C File Offset: 0x0020C52C
		public static ParseResult Int32TryParse(char[] chars, int start, int length, out int value)
		{
			value = 0;
			if (length == 0)
			{
				return ParseResult.Invalid;
			}
			bool flag = chars[start] == '-';
			if (flag)
			{
				if (length == 1)
				{
					return ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int num = start + length;
			if (length > 10 || (length == 10 && chars[start] - '0' > '\u0002'))
			{
				for (int i = start; i < num; i++)
				{
					int num2 = (int)(chars[i] - '0');
					if (num2 < 0 || num2 > 9)
					{
						return ParseResult.Invalid;
					}
				}
				return ParseResult.Overflow;
			}
			for (int j = start; j < num; j++)
			{
				int num3 = (int)(chars[j] - '0');
				if (num3 < 0 || num3 > 9)
				{
					return ParseResult.Invalid;
				}
				int num4 = 10 * value - num3;
				if (num4 > value)
				{
					for (j++; j < num; j++)
					{
						num3 = (int)(chars[j] - '0');
						if (num3 < 0 || num3 > 9)
						{
							return ParseResult.Invalid;
						}
					}
					return ParseResult.Overflow;
				}
				value = num4;
			}
			if (!flag)
			{
				if (value == -2147483648)
				{
					return ParseResult.Overflow;
				}
				value = -value;
			}
			return ParseResult.Success;
		}

		// Token: 0x06006C9C RID: 27804 RVA: 0x0020C648 File Offset: 0x0020C648
		public static ParseResult Int64TryParse(char[] chars, int start, int length, out long value)
		{
			value = 0L;
			if (length == 0)
			{
				return ParseResult.Invalid;
			}
			bool flag = chars[start] == '-';
			if (flag)
			{
				if (length == 1)
				{
					return ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int num = start + length;
			if (length > 19)
			{
				for (int i = start; i < num; i++)
				{
					int num2 = (int)(chars[i] - '0');
					if (num2 < 0 || num2 > 9)
					{
						return ParseResult.Invalid;
					}
				}
				return ParseResult.Overflow;
			}
			for (int j = start; j < num; j++)
			{
				int num3 = (int)(chars[j] - '0');
				if (num3 < 0 || num3 > 9)
				{
					return ParseResult.Invalid;
				}
				long num4 = 10L * value - (long)num3;
				if (num4 > value)
				{
					for (j++; j < num; j++)
					{
						num3 = (int)(chars[j] - '0');
						if (num3 < 0 || num3 > 9)
						{
							return ParseResult.Invalid;
						}
					}
					return ParseResult.Overflow;
				}
				value = num4;
			}
			if (!flag)
			{
				if (value == -9223372036854775808L)
				{
					return ParseResult.Overflow;
				}
				value = -value;
			}
			return ParseResult.Success;
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x0020C758 File Offset: 0x0020C758
		public static ParseResult DecimalTryParse(char[] chars, int start, int length, out decimal value)
		{
			value = 0m;
			if (length == 0)
			{
				return ParseResult.Invalid;
			}
			bool flag = chars[start] == '-';
			if (flag)
			{
				if (length == 1)
				{
					return ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int i = start;
			int num = start + length;
			int num2 = num;
			int num3 = num;
			int num4 = 0;
			ulong num5 = 0UL;
			ulong num6 = 0UL;
			int num7 = 0;
			int num8 = 0;
			char? c = null;
			bool? flag2 = null;
			while (i < num)
			{
				char c2 = chars[i];
				if (c2 == '.')
				{
					goto IL_86;
				}
				if (c2 != 'E' && c2 != 'e')
				{
					if (c2 < '0' || c2 > '9')
					{
						return ParseResult.Invalid;
					}
					if (i == start && c2 == '0')
					{
						i++;
						if (i != num)
						{
							c2 = chars[i];
							if (c2 == '.')
							{
								goto IL_86;
							}
							if (c2 != 'e' && c2 != 'E')
							{
								return ParseResult.Invalid;
							}
							goto IL_AC;
						}
					}
					if (num7 < 29)
					{
						if (num7 == 28)
						{
							bool? flag3 = flag2;
							bool valueOrDefault;
							if (flag3 == null)
							{
								flag2 = new bool?(num5 > 7922816251426433759UL || (num5 == 7922816251426433759UL && (num6 > 354395033UL || (num6 == 354395033UL && c2 > '5'))));
								bool? flag4 = flag2;
								valueOrDefault = flag4.GetValueOrDefault();
							}
							else
							{
								valueOrDefault = flag3.GetValueOrDefault();
							}
							if (valueOrDefault)
							{
								goto IL_277;
							}
						}
						if (num7 < 19)
						{
							num5 = num5 * 10UL + (ulong)((long)(c2 - '0'));
						}
						else
						{
							num6 = num6 * 10UL + (ulong)((long)(c2 - '0'));
						}
						num7++;
						goto IL_292;
					}
					IL_277:
					if (c == null)
					{
						c = new char?(c2);
					}
					num8++;
					goto IL_292;
				}
				IL_AC:
				if (i == start)
				{
					return ParseResult.Invalid;
				}
				if (i == num2)
				{
					return ParseResult.Invalid;
				}
				i++;
				if (i == num)
				{
					return ParseResult.Invalid;
				}
				if (num2 < num)
				{
					num3 = i - 1;
				}
				c2 = chars[i];
				bool flag5 = false;
				if (c2 != '+')
				{
					if (c2 == '-')
					{
						flag5 = true;
						i++;
					}
				}
				else
				{
					i++;
				}
				while (i < num)
				{
					c2 = chars[i];
					if (c2 < '0' || c2 > '9')
					{
						return ParseResult.Invalid;
					}
					int num9 = 10 * num4 + (int)(c2 - '0');
					if (num4 < num9)
					{
						num4 = num9;
					}
					i++;
				}
				if (flag5)
				{
					num4 = -num4;
				}
				IL_292:
				i++;
				continue;
				IL_86:
				if (i == start)
				{
					return ParseResult.Invalid;
				}
				if (i + 1 == num)
				{
					return ParseResult.Invalid;
				}
				if (num2 != num)
				{
					return ParseResult.Invalid;
				}
				num2 = i + 1;
				goto IL_292;
			}
			num4 += num8;
			num4 -= num3 - num2;
			if (num7 <= 19)
			{
				value = num5;
			}
			else
			{
				value = num5 / new decimal(1, 0, 0, false, (byte)(num7 - 19)) + num6;
			}
			if (num4 > 0)
			{
				num7 += num4;
				if (num7 > 29)
				{
					return ParseResult.Overflow;
				}
				if (num7 == 29)
				{
					if (num4 > 1)
					{
						value /= new decimal(1, 0, 0, false, (byte)(num4 - 1));
						if (value > 7922816251426433759354395033m)
						{
							return ParseResult.Overflow;
						}
					}
					else if (value == 7922816251426433759354395033m)
					{
						char? c3 = c;
						int? num10 = (c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null;
						int num11 = 53;
						if (num10.GetValueOrDefault() > num11 & num10 != null)
						{
							return ParseResult.Overflow;
						}
					}
					value *= 10m;
				}
				else
				{
					value /= new decimal(1, 0, 0, false, (byte)num4);
				}
			}
			else
			{
				char? c3 = c;
				int? num10 = (c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null;
				int num11 = 53;
				if ((num10.GetValueOrDefault() >= num11 & num10 != null) && num4 >= -28)
				{
					value = ++value;
				}
				if (num4 < 0)
				{
					if (num7 + num4 + 28 <= 0)
					{
						value = (flag ? 0m : 0m);
						return ParseResult.Success;
					}
					if (num4 >= -28)
					{
						value *= new decimal(1, 0, 0, false, (byte)(-(byte)num4));
					}
					else
					{
						value /= 10000000000000000000000000000m;
						value *= new decimal(1, 0, 0, false, (byte)(-num4 - 28));
					}
				}
			}
			if (flag)
			{
				value = -value;
			}
			return ParseResult.Success;
		}

		// Token: 0x06006C9E RID: 27806 RVA: 0x0020CCA8 File Offset: 0x0020CCA8
		public static bool TryConvertGuid(string s, out Guid g)
		{
			return Guid.TryParseExact(s, "D", out g);
		}

		// Token: 0x06006C9F RID: 27807 RVA: 0x0020CCB8 File Offset: 0x0020CCB8
		public static bool TryHexTextToInt(char[] text, int start, int end, out int value)
		{
			value = 0;
			for (int i = start; i < end; i++)
			{
				char c = text[i];
				int num;
				if (c <= '9' && c >= '0')
				{
					num = (int)(c - '0');
				}
				else if (c <= 'F' && c >= 'A')
				{
					num = (int)(c - '7');
				}
				else
				{
					if (c > 'f' || c < 'a')
					{
						value = 0;
						return false;
					}
					num = (int)(c - 'W');
				}
				value += num << (end - 1 - i) * 4;
			}
			return true;
		}

		// Token: 0x0400368C RID: 13964
		private static readonly Dictionary<Type, PrimitiveTypeCode> TypeCodeMap = new Dictionary<Type, PrimitiveTypeCode>
		{
			{
				typeof(char),
				PrimitiveTypeCode.Char
			},
			{
				typeof(char?),
				PrimitiveTypeCode.CharNullable
			},
			{
				typeof(bool),
				PrimitiveTypeCode.Boolean
			},
			{
				typeof(bool?),
				PrimitiveTypeCode.BooleanNullable
			},
			{
				typeof(sbyte),
				PrimitiveTypeCode.SByte
			},
			{
				typeof(sbyte?),
				PrimitiveTypeCode.SByteNullable
			},
			{
				typeof(short),
				PrimitiveTypeCode.Int16
			},
			{
				typeof(short?),
				PrimitiveTypeCode.Int16Nullable
			},
			{
				typeof(ushort),
				PrimitiveTypeCode.UInt16
			},
			{
				typeof(ushort?),
				PrimitiveTypeCode.UInt16Nullable
			},
			{
				typeof(int),
				PrimitiveTypeCode.Int32
			},
			{
				typeof(int?),
				PrimitiveTypeCode.Int32Nullable
			},
			{
				typeof(byte),
				PrimitiveTypeCode.Byte
			},
			{
				typeof(byte?),
				PrimitiveTypeCode.ByteNullable
			},
			{
				typeof(uint),
				PrimitiveTypeCode.UInt32
			},
			{
				typeof(uint?),
				PrimitiveTypeCode.UInt32Nullable
			},
			{
				typeof(long),
				PrimitiveTypeCode.Int64
			},
			{
				typeof(long?),
				PrimitiveTypeCode.Int64Nullable
			},
			{
				typeof(ulong),
				PrimitiveTypeCode.UInt64
			},
			{
				typeof(ulong?),
				PrimitiveTypeCode.UInt64Nullable
			},
			{
				typeof(float),
				PrimitiveTypeCode.Single
			},
			{
				typeof(float?),
				PrimitiveTypeCode.SingleNullable
			},
			{
				typeof(double),
				PrimitiveTypeCode.Double
			},
			{
				typeof(double?),
				PrimitiveTypeCode.DoubleNullable
			},
			{
				typeof(DateTime),
				PrimitiveTypeCode.DateTime
			},
			{
				typeof(DateTime?),
				PrimitiveTypeCode.DateTimeNullable
			},
			{
				typeof(DateTimeOffset),
				PrimitiveTypeCode.DateTimeOffset
			},
			{
				typeof(DateTimeOffset?),
				PrimitiveTypeCode.DateTimeOffsetNullable
			},
			{
				typeof(decimal),
				PrimitiveTypeCode.Decimal
			},
			{
				typeof(decimal?),
				PrimitiveTypeCode.DecimalNullable
			},
			{
				typeof(Guid),
				PrimitiveTypeCode.Guid
			},
			{
				typeof(Guid?),
				PrimitiveTypeCode.GuidNullable
			},
			{
				typeof(TimeSpan),
				PrimitiveTypeCode.TimeSpan
			},
			{
				typeof(TimeSpan?),
				PrimitiveTypeCode.TimeSpanNullable
			},
			{
				typeof(BigInteger),
				PrimitiveTypeCode.BigInteger
			},
			{
				typeof(BigInteger?),
				PrimitiveTypeCode.BigIntegerNullable
			},
			{
				typeof(Uri),
				PrimitiveTypeCode.Uri
			},
			{
				typeof(string),
				PrimitiveTypeCode.String
			},
			{
				typeof(byte[]),
				PrimitiveTypeCode.Bytes
			},
			{
				typeof(DBNull),
				PrimitiveTypeCode.DBNull
			}
		};

		// Token: 0x0400368D RID: 13965
		private static readonly TypeInformation[] PrimitiveTypeCodes = new TypeInformation[]
		{
			new TypeInformation(typeof(object), PrimitiveTypeCode.Empty),
			new TypeInformation(typeof(object), PrimitiveTypeCode.Object),
			new TypeInformation(typeof(object), PrimitiveTypeCode.DBNull),
			new TypeInformation(typeof(bool), PrimitiveTypeCode.Boolean),
			new TypeInformation(typeof(char), PrimitiveTypeCode.Char),
			new TypeInformation(typeof(sbyte), PrimitiveTypeCode.SByte),
			new TypeInformation(typeof(byte), PrimitiveTypeCode.Byte),
			new TypeInformation(typeof(short), PrimitiveTypeCode.Int16),
			new TypeInformation(typeof(ushort), PrimitiveTypeCode.UInt16),
			new TypeInformation(typeof(int), PrimitiveTypeCode.Int32),
			new TypeInformation(typeof(uint), PrimitiveTypeCode.UInt32),
			new TypeInformation(typeof(long), PrimitiveTypeCode.Int64),
			new TypeInformation(typeof(ulong), PrimitiveTypeCode.UInt64),
			new TypeInformation(typeof(float), PrimitiveTypeCode.Single),
			new TypeInformation(typeof(double), PrimitiveTypeCode.Double),
			new TypeInformation(typeof(decimal), PrimitiveTypeCode.Decimal),
			new TypeInformation(typeof(DateTime), PrimitiveTypeCode.DateTime),
			new TypeInformation(typeof(object), PrimitiveTypeCode.Empty),
			new TypeInformation(typeof(string), PrimitiveTypeCode.String)
		};

		// Token: 0x0400368E RID: 13966
		[Nullable(new byte[]
		{
			1,
			0,
			1,
			1,
			2,
			2,
			2
		})]
		private static readonly ThreadSafeStore<StructMultiKey<Type, Type>, Func<object, object>> CastConverters = new ThreadSafeStore<StructMultiKey<Type, Type>, Func<object, object>>(new Func<StructMultiKey<Type, Type>, Func<object, object>>(ConvertUtils.CreateCastConverter));

		// Token: 0x020010BF RID: 4287
		[NullableContext(0)]
		internal enum ConvertResult
		{
			// Token: 0x0400482A RID: 18474
			Success,
			// Token: 0x0400482B RID: 18475
			CannotConvertNull,
			// Token: 0x0400482C RID: 18476
			NotInstantiableType,
			// Token: 0x0400482D RID: 18477
			NoValidConversion
		}
	}
}
