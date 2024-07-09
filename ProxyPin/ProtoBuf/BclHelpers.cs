using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf
{
	// Token: 0x02000C13 RID: 3091
	[ComVisible(true)]
	public static class BclHelpers
	{
		// Token: 0x06007AEE RID: 31470 RVA: 0x002447DC File Offset: 0x002447DC
		public static object GetUninitializedObject(Type type)
		{
			return FormatterServices.GetUninitializedObject(type);
		}

		// Token: 0x06007AEF RID: 31471 RVA: 0x002447E4 File Offset: 0x002447E4
		public static void WriteTimeSpan(TimeSpan timeSpan, ProtoWriter dest)
		{
			BclHelpers.WriteTimeSpanImpl(timeSpan, dest, DateTimeKind.Unspecified);
		}

		// Token: 0x06007AF0 RID: 31472 RVA: 0x002447F0 File Offset: 0x002447F0
		private static void WriteTimeSpanImpl(TimeSpan timeSpan, ProtoWriter dest, DateTimeKind kind)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			WireType wireType = dest.WireType;
			if (wireType == WireType.Fixed64)
			{
				ProtoWriter.WriteInt64(timeSpan.Ticks, dest);
				return;
			}
			if (wireType - WireType.String <= 1)
			{
				long num = timeSpan.Ticks;
				TimeSpanScale timeSpanScale;
				if (timeSpan == TimeSpan.MaxValue)
				{
					num = 1L;
					timeSpanScale = TimeSpanScale.MinMax;
				}
				else if (timeSpan == TimeSpan.MinValue)
				{
					num = -1L;
					timeSpanScale = TimeSpanScale.MinMax;
				}
				else if (num % 864000000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Days;
					num /= 864000000000L;
				}
				else if (num % 36000000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Hours;
					num /= 36000000000L;
				}
				else if (num % 600000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Minutes;
					num /= 600000000L;
				}
				else if (num % 10000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Seconds;
					num /= 10000000L;
				}
				else if (num % 10000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Milliseconds;
					num /= 10000L;
				}
				else
				{
					timeSpanScale = TimeSpanScale.Ticks;
				}
				SubItemToken token = ProtoWriter.StartSubItem(null, dest);
				if (num != 0L)
				{
					ProtoWriter.WriteFieldHeader(1, WireType.SignedVariant, dest);
					ProtoWriter.WriteInt64(num, dest);
				}
				if (timeSpanScale != TimeSpanScale.Days)
				{
					ProtoWriter.WriteFieldHeader(2, WireType.Variant, dest);
					ProtoWriter.WriteInt32((int)timeSpanScale, dest);
				}
				if (kind != DateTimeKind.Unspecified)
				{
					ProtoWriter.WriteFieldHeader(3, WireType.Variant, dest);
					ProtoWriter.WriteInt32((int)kind, dest);
				}
				ProtoWriter.EndSubItem(token, dest);
				return;
			}
			throw new ProtoException("Unexpected wire-type: " + dest.WireType.ToString());
		}

		// Token: 0x06007AF1 RID: 31473 RVA: 0x00244988 File Offset: 0x00244988
		public static TimeSpan ReadTimeSpan(ProtoReader source)
		{
			DateTimeKind dateTimeKind;
			long num = BclHelpers.ReadTimeSpanTicks(source, out dateTimeKind);
			if (num == -9223372036854775808L)
			{
				return TimeSpan.MinValue;
			}
			if (num == 9223372036854775807L)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromTicks(num);
		}

		// Token: 0x06007AF2 RID: 31474 RVA: 0x002449D4 File Offset: 0x002449D4
		public static TimeSpan ReadDuration(ProtoReader source)
		{
			long seconds = 0L;
			int nanos = 0;
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num;
			while ((num = source.ReadFieldHeader()) > 0)
			{
				if (num != 1)
				{
					if (num != 2)
					{
						source.SkipField();
					}
					else
					{
						nanos = source.ReadInt32();
					}
				}
				else
				{
					seconds = source.ReadInt64();
				}
			}
			ProtoReader.EndSubItem(token, source);
			return BclHelpers.FromDurationSeconds(seconds, nanos);
		}

		// Token: 0x06007AF3 RID: 31475 RVA: 0x00244A40 File Offset: 0x00244A40
		public static void WriteDuration(TimeSpan value, ProtoWriter dest)
		{
			int nanos;
			long seconds = BclHelpers.ToDurationSeconds(value, out nanos);
			BclHelpers.WriteSecondsNanos(seconds, nanos, dest);
		}

		// Token: 0x06007AF4 RID: 31476 RVA: 0x00244A64 File Offset: 0x00244A64
		private static void WriteSecondsNanos(long seconds, int nanos, ProtoWriter dest)
		{
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			if (seconds != 0L)
			{
				ProtoWriter.WriteFieldHeader(1, WireType.Variant, dest);
				ProtoWriter.WriteInt64(seconds, dest);
			}
			if (nanos != 0)
			{
				ProtoWriter.WriteFieldHeader(2, WireType.Variant, dest);
				ProtoWriter.WriteInt32(nanos, dest);
			}
			ProtoWriter.EndSubItem(token, dest);
		}

		// Token: 0x06007AF5 RID: 31477 RVA: 0x00244AB0 File Offset: 0x00244AB0
		public static DateTime ReadTimestamp(ProtoReader source)
		{
			return BclHelpers.TimestampEpoch + BclHelpers.ReadDuration(source);
		}

		// Token: 0x06007AF6 RID: 31478 RVA: 0x00244AC4 File Offset: 0x00244AC4
		public static void WriteTimestamp(DateTime value, ProtoWriter dest)
		{
			int num2;
			long num = BclHelpers.ToDurationSeconds(value - BclHelpers.TimestampEpoch, out num2);
			if (num2 < 0)
			{
				num -= 1L;
				num2 += 1000000000;
			}
			BclHelpers.WriteSecondsNanos(num, num2, dest);
		}

		// Token: 0x06007AF7 RID: 31479 RVA: 0x00244B04 File Offset: 0x00244B04
		private static TimeSpan FromDurationSeconds(long seconds, int nanos)
		{
			long value = checked(seconds * 10000000L + unchecked((long)nanos) * 10000L / 1000000L);
			return TimeSpan.FromTicks(value);
		}

		// Token: 0x06007AF8 RID: 31480 RVA: 0x00244B38 File Offset: 0x00244B38
		private static long ToDurationSeconds(TimeSpan value, out int nanos)
		{
			nanos = (int)(value.Ticks % 10000000L * 1000000L / 10000L);
			return value.Ticks / 10000000L;
		}

		// Token: 0x06007AF9 RID: 31481 RVA: 0x00244B68 File Offset: 0x00244B68
		public static DateTime ReadDateTime(ProtoReader source)
		{
			DateTimeKind dateTimeKind;
			long num = BclHelpers.ReadTimeSpanTicks(source, out dateTimeKind);
			if (num == -9223372036854775808L)
			{
				return DateTime.MinValue;
			}
			if (num == 9223372036854775807L)
			{
				return DateTime.MaxValue;
			}
			return BclHelpers.EpochOrigin[(int)dateTimeKind].AddTicks(num);
		}

		// Token: 0x06007AFA RID: 31482 RVA: 0x00244BC0 File Offset: 0x00244BC0
		public static void WriteDateTime(DateTime value, ProtoWriter dest)
		{
			BclHelpers.WriteDateTimeImpl(value, dest, false);
		}

		// Token: 0x06007AFB RID: 31483 RVA: 0x00244BCC File Offset: 0x00244BCC
		public static void WriteDateTimeWithKind(DateTime value, ProtoWriter dest)
		{
			BclHelpers.WriteDateTimeImpl(value, dest, true);
		}

		// Token: 0x06007AFC RID: 31484 RVA: 0x00244BD8 File Offset: 0x00244BD8
		private static void WriteDateTimeImpl(DateTime value, ProtoWriter dest, bool includeKind)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			WireType wireType = dest.WireType;
			TimeSpan timeSpan;
			if (wireType - WireType.String <= 1)
			{
				if (value == DateTime.MaxValue)
				{
					timeSpan = TimeSpan.MaxValue;
					includeKind = false;
				}
				else if (value == DateTime.MinValue)
				{
					timeSpan = TimeSpan.MinValue;
					includeKind = false;
				}
				else
				{
					timeSpan = value - BclHelpers.EpochOrigin[0];
				}
			}
			else
			{
				timeSpan = value - BclHelpers.EpochOrigin[0];
			}
			BclHelpers.WriteTimeSpanImpl(timeSpan, dest, includeKind ? value.Kind : DateTimeKind.Unspecified);
		}

		// Token: 0x06007AFD RID: 31485 RVA: 0x00244C8C File Offset: 0x00244C8C
		private static long ReadTimeSpanTicks(ProtoReader source, out DateTimeKind kind)
		{
			kind = DateTimeKind.Unspecified;
			WireType wireType = source.WireType;
			if (wireType == WireType.Fixed64)
			{
				return source.ReadInt64();
			}
			if (wireType - WireType.String > 1)
			{
				throw new ProtoException("Unexpected wire-type: " + source.WireType.ToString());
			}
			SubItemToken token = ProtoReader.StartSubItem(source);
			TimeSpanScale timeSpanScale = TimeSpanScale.Days;
			long num = 0L;
			int num2;
			while ((num2 = source.ReadFieldHeader()) > 0)
			{
				switch (num2)
				{
				case 1:
					source.Assert(WireType.SignedVariant);
					num = source.ReadInt64();
					break;
				case 2:
					timeSpanScale = (TimeSpanScale)source.ReadInt32();
					break;
				case 3:
				{
					kind = (DateTimeKind)source.ReadInt32();
					DateTimeKind dateTimeKind = kind;
					if (dateTimeKind > DateTimeKind.Local)
					{
						throw new ProtoException("Invalid date/time kind: " + kind.ToString());
					}
					break;
				}
				default:
					source.SkipField();
					break;
				}
			}
			ProtoReader.EndSubItem(token, source);
			switch (timeSpanScale)
			{
			case TimeSpanScale.Days:
				return num * 864000000000L;
			case TimeSpanScale.Hours:
				return num * 36000000000L;
			case TimeSpanScale.Minutes:
				return num * 600000000L;
			case TimeSpanScale.Seconds:
				return num * 10000000L;
			case TimeSpanScale.Milliseconds:
				return num * 10000L;
			case TimeSpanScale.Ticks:
				return num;
			default:
				if (timeSpanScale != TimeSpanScale.MinMax)
				{
					throw new ProtoException("Unknown timescale: " + timeSpanScale.ToString());
				}
				if (num == -1L)
				{
					return long.MinValue;
				}
				if (num == 1L)
				{
					return long.MaxValue;
				}
				throw new ProtoException("Unknown min/max value: " + num.ToString());
			}
		}

		// Token: 0x06007AFE RID: 31486 RVA: 0x00244E30 File Offset: 0x00244E30
		public static decimal ReadDecimal(ProtoReader reader)
		{
			ulong num = 0UL;
			uint num2 = 0U;
			uint num3 = 0U;
			SubItemToken token = ProtoReader.StartSubItem(reader);
			int num4;
			while ((num4 = reader.ReadFieldHeader()) > 0)
			{
				switch (num4)
				{
				case 1:
					num = reader.ReadUInt64();
					break;
				case 2:
					num2 = reader.ReadUInt32();
					break;
				case 3:
					num3 = reader.ReadUInt32();
					break;
				default:
					reader.SkipField();
					break;
				}
			}
			ProtoReader.EndSubItem(token, reader);
			int lo = (int)(num & (ulong)-1);
			int mid = (int)(num >> 32 & (ulong)-1);
			int hi = (int)num2;
			bool isNegative = (num3 & 1U) == 1U;
			byte scale = (byte)((num3 & 510U) >> 1);
			return new decimal(lo, mid, hi, isNegative, scale);
		}

		// Token: 0x06007AFF RID: 31487 RVA: 0x00244EE4 File Offset: 0x00244EE4
		public static void WriteDecimal(decimal value, ProtoWriter writer)
		{
			int[] bits = decimal.GetBits(value);
			ulong num = (ulong)((ulong)((long)bits[1]) << 32);
			ulong num2 = (ulong)((long)bits[0] & (long)((ulong)-1));
			ulong num3 = num | num2;
			uint num4 = (uint)bits[2];
			uint num5 = (uint)((bits[3] >> 15 & 510) | (bits[3] >> 31 & 1));
			SubItemToken token = ProtoWriter.StartSubItem(null, writer);
			if (num3 != 0UL)
			{
				ProtoWriter.WriteFieldHeader(1, WireType.Variant, writer);
				ProtoWriter.WriteUInt64(num3, writer);
			}
			if (num4 != 0U)
			{
				ProtoWriter.WriteFieldHeader(2, WireType.Variant, writer);
				ProtoWriter.WriteUInt32(num4, writer);
			}
			if (num5 != 0U)
			{
				ProtoWriter.WriteFieldHeader(3, WireType.Variant, writer);
				ProtoWriter.WriteUInt32(num5, writer);
			}
			ProtoWriter.EndSubItem(token, writer);
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x00244F80 File Offset: 0x00244F80
		public static void WriteGuid(Guid value, ProtoWriter dest)
		{
			byte[] data = value.ToByteArray();
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			if (value != Guid.Empty)
			{
				ProtoWriter.WriteFieldHeader(1, WireType.Fixed64, dest);
				ProtoWriter.WriteBytes(data, 0, 8, dest);
				ProtoWriter.WriteFieldHeader(2, WireType.Fixed64, dest);
				ProtoWriter.WriteBytes(data, 8, 8, dest);
			}
			ProtoWriter.EndSubItem(token, dest);
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x00244FDC File Offset: 0x00244FDC
		public static Guid ReadGuid(ProtoReader source)
		{
			ulong num = 0UL;
			ulong num2 = 0UL;
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				if (num3 != 1)
				{
					if (num3 != 2)
					{
						source.SkipField();
					}
					else
					{
						num2 = source.ReadUInt64();
					}
				}
				else
				{
					num = source.ReadUInt64();
				}
			}
			ProtoReader.EndSubItem(token, source);
			if (num == 0UL && num2 == 0UL)
			{
				return Guid.Empty;
			}
			uint num4 = (uint)(num >> 32);
			uint a = (uint)num;
			uint num5 = (uint)(num2 >> 32);
			uint num6 = (uint)num2;
			return new Guid((int)a, (short)num4, (short)(num4 >> 16), (byte)num6, (byte)(num6 >> 8), (byte)(num6 >> 16), (byte)(num6 >> 24), (byte)num5, (byte)(num5 >> 8), (byte)(num5 >> 16), (byte)(num5 >> 24));
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x002450A4 File Offset: 0x002450A4
		public static object ReadNetObject(object value, ProtoReader source, int key, Type type, BclHelpers.NetObjectOptions options)
		{
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num = -1;
			int num2 = -1;
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				switch (num3)
				{
				case 1:
				{
					int key2 = source.ReadInt32();
					value = source.NetCache.GetKeyedObject(key2);
					continue;
				}
				case 2:
					num = source.ReadInt32();
					continue;
				case 3:
				{
					int key2 = source.ReadInt32();
					type = (Type)source.NetCache.GetKeyedObject(key2);
					key = source.GetTypeKey(ref type);
					continue;
				}
				case 4:
					num2 = source.ReadInt32();
					continue;
				case 8:
				{
					string text = source.ReadString();
					type = source.DeserializeType(text);
					if (type == null)
					{
						throw new ProtoException("Unable to resolve type: " + text + " (you can use the TypeModel.DynamicTypeFormatting event to provide a custom mapping)");
					}
					if (type == typeof(string))
					{
						key = -1;
						continue;
					}
					key = source.GetTypeKey(ref type);
					if (key < 0)
					{
						throw new InvalidOperationException("Dynamic type is not a contract-type: " + type.Name);
					}
					continue;
				}
				case 10:
				{
					bool flag = type == typeof(string);
					bool flag2 = value == null;
					bool flag3 = flag2 && (flag || (options & BclHelpers.NetObjectOptions.LateSet) > BclHelpers.NetObjectOptions.None);
					if (num >= 0 && !flag3)
					{
						if (value == null)
						{
							source.TrapNextObject(num);
						}
						else
						{
							source.NetCache.SetKeyedObject(num, value);
						}
						if (num2 >= 0)
						{
							source.NetCache.SetKeyedObject(num2, type);
						}
					}
					object obj = value;
					if (flag)
					{
						value = source.ReadString();
					}
					else
					{
						value = ProtoReader.ReadTypedObject(obj, key, source, type);
					}
					if (num >= 0)
					{
						if (flag2 && !flag3)
						{
							obj = source.NetCache.GetKeyedObject(num);
						}
						if (flag3)
						{
							source.NetCache.SetKeyedObject(num, value);
							if (num2 >= 0)
							{
								source.NetCache.SetKeyedObject(num2, type);
							}
						}
					}
					if (num >= 0 && !flag3 && obj != value)
					{
						throw new ProtoException("A reference-tracked object changed reference during deserialization");
					}
					if (num < 0 && num2 >= 0)
					{
						source.NetCache.SetKeyedObject(num2, type);
						continue;
					}
					continue;
				}
				}
				source.SkipField();
			}
			if (num >= 0 && (options & BclHelpers.NetObjectOptions.AsReference) == BclHelpers.NetObjectOptions.None)
			{
				throw new ProtoException("Object key in input stream, but reference-tracking was not expected");
			}
			ProtoReader.EndSubItem(token, source);
			return value;
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x00245334 File Offset: 0x00245334
		public static void WriteNetObject(object value, ProtoWriter dest, int key, BclHelpers.NetObjectOptions options)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			bool flag = (options & BclHelpers.NetObjectOptions.DynamicType) > BclHelpers.NetObjectOptions.None;
			bool flag2 = (options & BclHelpers.NetObjectOptions.AsReference) > BclHelpers.NetObjectOptions.None;
			WireType wireType = dest.WireType;
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			bool flag3 = true;
			if (flag2)
			{
				bool flag4;
				int value2 = dest.NetCache.AddObjectKey(value, out flag4);
				ProtoWriter.WriteFieldHeader(flag4 ? 1 : 2, WireType.Variant, dest);
				ProtoWriter.WriteInt32(value2, dest);
				if (flag4)
				{
					flag3 = false;
				}
			}
			if (flag3)
			{
				if (flag)
				{
					Type type = value.GetType();
					if (!(value is string))
					{
						key = dest.GetTypeKey(ref type);
						if (key < 0)
						{
							throw new InvalidOperationException("Dynamic type is not a contract-type: " + type.Name);
						}
					}
					bool flag5;
					int value3 = dest.NetCache.AddObjectKey(type, out flag5);
					ProtoWriter.WriteFieldHeader(flag5 ? 3 : 4, WireType.Variant, dest);
					ProtoWriter.WriteInt32(value3, dest);
					if (!flag5)
					{
						ProtoWriter.WriteFieldHeader(8, WireType.String, dest);
						ProtoWriter.WriteString(dest.SerializeType(type), dest);
					}
				}
				ProtoWriter.WriteFieldHeader(10, wireType, dest);
				if (value is string)
				{
					ProtoWriter.WriteString((string)value, dest);
				}
				else
				{
					ProtoWriter.WriteObject(value, key, dest);
				}
			}
			ProtoWriter.EndSubItem(token, dest);
		}

		// Token: 0x04003B4C RID: 15180
		private const int FieldTimeSpanValue = 1;

		// Token: 0x04003B4D RID: 15181
		private const int FieldTimeSpanScale = 2;

		// Token: 0x04003B4E RID: 15182
		private const int FieldTimeSpanKind = 3;

		// Token: 0x04003B4F RID: 15183
		internal static readonly DateTime[] EpochOrigin = new DateTime[]
		{
			new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
			new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
			new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local)
		};

		// Token: 0x04003B50 RID: 15184
		private static readonly DateTime TimestampEpoch = BclHelpers.EpochOrigin[1];

		// Token: 0x04003B51 RID: 15185
		private const int FieldDecimalLow = 1;

		// Token: 0x04003B52 RID: 15186
		private const int FieldDecimalHigh = 2;

		// Token: 0x04003B53 RID: 15187
		private const int FieldDecimalSignScale = 3;

		// Token: 0x04003B54 RID: 15188
		private const int FieldGuidLow = 1;

		// Token: 0x04003B55 RID: 15189
		private const int FieldGuidHigh = 2;

		// Token: 0x04003B56 RID: 15190
		private const int FieldExistingObjectKey = 1;

		// Token: 0x04003B57 RID: 15191
		private const int FieldNewObjectKey = 2;

		// Token: 0x04003B58 RID: 15192
		private const int FieldExistingTypeKey = 3;

		// Token: 0x04003B59 RID: 15193
		private const int FieldNewTypeKey = 4;

		// Token: 0x04003B5A RID: 15194
		private const int FieldTypeName = 8;

		// Token: 0x04003B5B RID: 15195
		private const int FieldObject = 10;

		// Token: 0x0200116B RID: 4459
		[Flags]
		public enum NetObjectOptions : byte
		{
			// Token: 0x04004B1F RID: 19231
			None = 0,
			// Token: 0x04004B20 RID: 19232
			AsReference = 1,
			// Token: 0x04004B21 RID: 19233
			DynamicType = 2,
			// Token: 0x04004B22 RID: 19234
			UseConstructor = 4,
			// Token: 0x04004B23 RID: 19235
			LateSet = 8
		}
	}
}
