using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AAB RID: 2731
	[NullableContext(1)]
	[Nullable(0)]
	internal static class DateTimeUtils
	{
		// Token: 0x06006CAB RID: 27819 RVA: 0x0020D820 File Offset: 0x0020D820
		public static TimeSpan GetUtcOffset(this DateTime d)
		{
			return TimeZoneInfo.Local.GetUtcOffset(d);
		}

		// Token: 0x06006CAC RID: 27820 RVA: 0x0020D830 File Offset: 0x0020D830
		public static XmlDateTimeSerializationMode ToSerializationMode(DateTimeKind kind)
		{
			switch (kind)
			{
			case DateTimeKind.Unspecified:
				return XmlDateTimeSerializationMode.Unspecified;
			case DateTimeKind.Utc:
				return XmlDateTimeSerializationMode.Utc;
			case DateTimeKind.Local:
				return XmlDateTimeSerializationMode.Local;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("kind", kind, "Unexpected DateTimeKind value.");
			}
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x0020D864 File Offset: 0x0020D864
		internal static DateTime EnsureDateTime(DateTime value, DateTimeZoneHandling timeZone)
		{
			switch (timeZone)
			{
			case DateTimeZoneHandling.Local:
				value = DateTimeUtils.SwitchToLocalTime(value);
				break;
			case DateTimeZoneHandling.Utc:
				value = DateTimeUtils.SwitchToUtcTime(value);
				break;
			case DateTimeZoneHandling.Unspecified:
				value = new DateTime(value.Ticks, DateTimeKind.Unspecified);
				break;
			case DateTimeZoneHandling.RoundtripKind:
				break;
			default:
				throw new ArgumentException("Invalid date time handling value.");
			}
			return value;
		}

		// Token: 0x06006CAE RID: 27822 RVA: 0x0020D8CC File Offset: 0x0020D8CC
		private static DateTime SwitchToLocalTime(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				return new DateTime(value.Ticks, DateTimeKind.Local);
			case DateTimeKind.Utc:
				return value.ToLocalTime();
			case DateTimeKind.Local:
				return value;
			default:
				return value;
			}
		}

		// Token: 0x06006CAF RID: 27823 RVA: 0x0020D918 File Offset: 0x0020D918
		private static DateTime SwitchToUtcTime(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				return new DateTime(value.Ticks, DateTimeKind.Utc);
			case DateTimeKind.Utc:
				return value;
			case DateTimeKind.Local:
				return value.ToUniversalTime();
			default:
				return value;
			}
		}

		// Token: 0x06006CB0 RID: 27824 RVA: 0x0020D964 File Offset: 0x0020D964
		private static long ToUniversalTicks(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime.Ticks;
			}
			return DateTimeUtils.ToUniversalTicks(dateTime, dateTime.GetUtcOffset());
		}

		// Token: 0x06006CB1 RID: 27825 RVA: 0x0020D988 File Offset: 0x0020D988
		private static long ToUniversalTicks(DateTime dateTime, TimeSpan offset)
		{
			if (dateTime.Kind == DateTimeKind.Utc || dateTime == DateTime.MaxValue || dateTime == DateTime.MinValue)
			{
				return dateTime.Ticks;
			}
			long num = dateTime.Ticks - offset.Ticks;
			if (num > 3155378975999999999L)
			{
				return 3155378975999999999L;
			}
			if (num < 0L)
			{
				return 0L;
			}
			return num;
		}

		// Token: 0x06006CB2 RID: 27826 RVA: 0x0020DA04 File Offset: 0x0020DA04
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, TimeSpan offset)
		{
			return DateTimeUtils.UniversialTicksToJavaScriptTicks(DateTimeUtils.ToUniversalTicks(dateTime, offset));
		}

		// Token: 0x06006CB3 RID: 27827 RVA: 0x0020DA14 File Offset: 0x0020DA14
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime)
		{
			return DateTimeUtils.ConvertDateTimeToJavaScriptTicks(dateTime, true);
		}

		// Token: 0x06006CB4 RID: 27828 RVA: 0x0020DA20 File Offset: 0x0020DA20
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, bool convertToUtc)
		{
			return DateTimeUtils.UniversialTicksToJavaScriptTicks(convertToUtc ? DateTimeUtils.ToUniversalTicks(dateTime) : dateTime.Ticks);
		}

		// Token: 0x06006CB5 RID: 27829 RVA: 0x0020DA40 File Offset: 0x0020DA40
		private static long UniversialTicksToJavaScriptTicks(long universialTicks)
		{
			return (universialTicks - DateTimeUtils.InitialJavaScriptDateTicks) / 10000L;
		}

		// Token: 0x06006CB6 RID: 27830 RVA: 0x0020DA50 File Offset: 0x0020DA50
		internal static DateTime ConvertJavaScriptTicksToDateTime(long javaScriptTicks)
		{
			return new DateTime(javaScriptTicks * 10000L + DateTimeUtils.InitialJavaScriptDateTicks, DateTimeKind.Utc);
		}

		// Token: 0x06006CB7 RID: 27831 RVA: 0x0020DA68 File Offset: 0x0020DA68
		internal static bool TryParseDateTimeIso(StringReference text, DateTimeZoneHandling dateTimeZoneHandling, out DateTime dt)
		{
			DateTimeParser dateTimeParser = default(DateTimeParser);
			if (!dateTimeParser.Parse(text.Chars, text.StartIndex, text.Length))
			{
				dt = default(DateTime);
				return false;
			}
			DateTime dateTime = DateTimeUtils.CreateDateTime(dateTimeParser);
			switch (dateTimeParser.Zone)
			{
			case ParserTimeZone.Utc:
				dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Utc);
				break;
			case ParserTimeZone.LocalWestOfUtc:
			{
				TimeSpan timeSpan = new TimeSpan(dateTimeParser.ZoneHour, dateTimeParser.ZoneMinute, 0);
				long num = dateTime.Ticks + timeSpan.Ticks;
				if (num <= DateTime.MaxValue.Ticks)
				{
					dateTime = new DateTime(num, DateTimeKind.Utc).ToLocalTime();
				}
				else
				{
					num += dateTime.GetUtcOffset().Ticks;
					if (num > DateTime.MaxValue.Ticks)
					{
						num = DateTime.MaxValue.Ticks;
					}
					dateTime = new DateTime(num, DateTimeKind.Local);
				}
				break;
			}
			case ParserTimeZone.LocalEastOfUtc:
			{
				TimeSpan timeSpan2 = new TimeSpan(dateTimeParser.ZoneHour, dateTimeParser.ZoneMinute, 0);
				long num = dateTime.Ticks - timeSpan2.Ticks;
				if (num >= DateTime.MinValue.Ticks)
				{
					dateTime = new DateTime(num, DateTimeKind.Utc).ToLocalTime();
				}
				else
				{
					num += dateTime.GetUtcOffset().Ticks;
					if (num < DateTime.MinValue.Ticks)
					{
						num = DateTime.MinValue.Ticks;
					}
					dateTime = new DateTime(num, DateTimeKind.Local);
				}
				break;
			}
			}
			dt = DateTimeUtils.EnsureDateTime(dateTime, dateTimeZoneHandling);
			return true;
		}

		// Token: 0x06006CB8 RID: 27832 RVA: 0x0020DC24 File Offset: 0x0020DC24
		internal static bool TryParseDateTimeOffsetIso(StringReference text, out DateTimeOffset dt)
		{
			DateTimeParser dateTimeParser = default(DateTimeParser);
			if (!dateTimeParser.Parse(text.Chars, text.StartIndex, text.Length))
			{
				dt = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime = DateTimeUtils.CreateDateTime(dateTimeParser);
			TimeSpan utcOffset;
			switch (dateTimeParser.Zone)
			{
			case ParserTimeZone.Utc:
				utcOffset = new TimeSpan(0L);
				break;
			case ParserTimeZone.LocalWestOfUtc:
				utcOffset = new TimeSpan(-dateTimeParser.ZoneHour, -dateTimeParser.ZoneMinute, 0);
				break;
			case ParserTimeZone.LocalEastOfUtc:
				utcOffset = new TimeSpan(dateTimeParser.ZoneHour, dateTimeParser.ZoneMinute, 0);
				break;
			default:
				utcOffset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
				break;
			}
			long num = dateTime.Ticks - utcOffset.Ticks;
			if (num < 0L || num > 3155378975999999999L)
			{
				dt = default(DateTimeOffset);
				return false;
			}
			dt = new DateTimeOffset(dateTime, utcOffset);
			return true;
		}

		// Token: 0x06006CB9 RID: 27833 RVA: 0x0020DD20 File Offset: 0x0020DD20
		private static DateTime CreateDateTime(DateTimeParser dateTimeParser)
		{
			bool flag;
			if (dateTimeParser.Hour == 24)
			{
				flag = true;
				dateTimeParser.Hour = 0;
			}
			else
			{
				flag = false;
			}
			DateTime result = new DateTime(dateTimeParser.Year, dateTimeParser.Month, dateTimeParser.Day, dateTimeParser.Hour, dateTimeParser.Minute, dateTimeParser.Second);
			result = result.AddTicks((long)dateTimeParser.Fraction);
			if (flag)
			{
				result = result.AddDays(1.0);
			}
			return result;
		}

		// Token: 0x06006CBA RID: 27834 RVA: 0x0020DDA4 File Offset: 0x0020DDA4
		internal static bool TryParseDateTime(StringReference s, DateTimeZoneHandling dateTimeZoneHandling, [Nullable(2)] string dateFormatString, CultureInfo culture, out DateTime dt)
		{
			if (s.Length > 0)
			{
				int startIndex = s.StartIndex;
				if (s[startIndex] == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(") && s.EndsWith(")/") && DateTimeUtils.TryParseDateTimeMicrosoft(s, dateTimeZoneHandling, out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s[startIndex]) && s[startIndex + 10] == 'T' && DateTimeUtils.TryParseDateTimeIso(s, dateTimeZoneHandling, out dt))
				{
					return true;
				}
				if (!StringUtils.IsNullOrEmpty(dateFormatString) && DateTimeUtils.TryParseDateTimeExact(s.ToString(), dateTimeZoneHandling, dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(DateTime);
			return false;
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x0020DE9C File Offset: 0x0020DE9C
		internal static bool TryParseDateTime(string s, DateTimeZoneHandling dateTimeZoneHandling, [Nullable(2)] string dateFormatString, CultureInfo culture, out DateTime dt)
		{
			if (s.Length > 0)
			{
				if (s[0] == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(", StringComparison.Ordinal) && s.EndsWith(")/", StringComparison.Ordinal) && DateTimeUtils.TryParseDateTimeMicrosoft(new StringReference(s.ToCharArray(), 0, s.Length), dateTimeZoneHandling, out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s[0]) && s[10] == 'T' && DateTime.TryParseExact(s, "yyyy-MM-ddTHH:mm:ss.FFFFFFFK", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dt))
				{
					dt = DateTimeUtils.EnsureDateTime(dt, dateTimeZoneHandling);
					return true;
				}
				if (!StringUtils.IsNullOrEmpty(dateFormatString) && DateTimeUtils.TryParseDateTimeExact(s, dateTimeZoneHandling, dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(DateTime);
			return false;
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x0020DFAC File Offset: 0x0020DFAC
		internal static bool TryParseDateTimeOffset(StringReference s, [Nullable(2)] string dateFormatString, CultureInfo culture, out DateTimeOffset dt)
		{
			if (s.Length > 0)
			{
				int startIndex = s.StartIndex;
				if (s[startIndex] == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(") && s.EndsWith(")/") && DateTimeUtils.TryParseDateTimeOffsetMicrosoft(s, out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s[startIndex]) && s[startIndex + 10] == 'T' && DateTimeUtils.TryParseDateTimeOffsetIso(s, out dt))
				{
					return true;
				}
				if (!StringUtils.IsNullOrEmpty(dateFormatString) && DateTimeUtils.TryParseDateTimeOffsetExact(s.ToString(), dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(DateTimeOffset);
			return false;
		}

		// Token: 0x06006CBD RID: 27837 RVA: 0x0020E09C File Offset: 0x0020E09C
		internal static bool TryParseDateTimeOffset(string s, [Nullable(2)] string dateFormatString, CultureInfo culture, out DateTimeOffset dt)
		{
			if (s.Length > 0)
			{
				if (s[0] == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(", StringComparison.Ordinal) && s.EndsWith(")/", StringComparison.Ordinal) && DateTimeUtils.TryParseDateTimeOffsetMicrosoft(new StringReference(s.ToCharArray(), 0, s.Length), out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s[0]) && s[10] == 'T' && DateTimeOffset.TryParseExact(s, "yyyy-MM-ddTHH:mm:ss.FFFFFFFK", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dt) && DateTimeUtils.TryParseDateTimeOffsetIso(new StringReference(s.ToCharArray(), 0, s.Length), out dt))
				{
					return true;
				}
				if (!StringUtils.IsNullOrEmpty(dateFormatString) && DateTimeUtils.TryParseDateTimeOffsetExact(s, dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(DateTimeOffset);
			return false;
		}

		// Token: 0x06006CBE RID: 27838 RVA: 0x0020E1AC File Offset: 0x0020E1AC
		private static bool TryParseMicrosoftDate(StringReference text, out long ticks, out TimeSpan offset, out DateTimeKind kind)
		{
			kind = DateTimeKind.Utc;
			int num = text.IndexOf('+', 7, text.Length - 8);
			if (num == -1)
			{
				num = text.IndexOf('-', 7, text.Length - 8);
			}
			if (num != -1)
			{
				kind = DateTimeKind.Local;
				if (!DateTimeUtils.TryReadOffset(text, num + text.StartIndex, out offset))
				{
					ticks = 0L;
					return false;
				}
			}
			else
			{
				offset = TimeSpan.Zero;
				num = text.Length - 2;
			}
			return ConvertUtils.Int64TryParse(text.Chars, 6 + text.StartIndex, num - 6, out ticks) == ParseResult.Success;
		}

		// Token: 0x06006CBF RID: 27839 RVA: 0x0020E244 File Offset: 0x0020E244
		private static bool TryParseDateTimeMicrosoft(StringReference text, DateTimeZoneHandling dateTimeZoneHandling, out DateTime dt)
		{
			long javaScriptTicks;
			TimeSpan timeSpan;
			DateTimeKind dateTimeKind;
			if (!DateTimeUtils.TryParseMicrosoftDate(text, out javaScriptTicks, out timeSpan, out dateTimeKind))
			{
				dt = default(DateTime);
				return false;
			}
			DateTime dateTime = DateTimeUtils.ConvertJavaScriptTicksToDateTime(javaScriptTicks);
			if (dateTimeKind != DateTimeKind.Unspecified)
			{
				if (dateTimeKind != DateTimeKind.Local)
				{
					dt = dateTime;
				}
				else
				{
					dt = dateTime.ToLocalTime();
				}
			}
			else
			{
				dt = DateTime.SpecifyKind(dateTime.ToLocalTime(), DateTimeKind.Unspecified);
			}
			dt = DateTimeUtils.EnsureDateTime(dt, dateTimeZoneHandling);
			return true;
		}

		// Token: 0x06006CC0 RID: 27840 RVA: 0x0020E2CC File Offset: 0x0020E2CC
		private static bool TryParseDateTimeExact(string text, DateTimeZoneHandling dateTimeZoneHandling, string dateFormatString, CultureInfo culture, out DateTime dt)
		{
			DateTime dateTime;
			if (DateTime.TryParseExact(text, dateFormatString, culture, DateTimeStyles.RoundtripKind, out dateTime))
			{
				dateTime = DateTimeUtils.EnsureDateTime(dateTime, dateTimeZoneHandling);
				dt = dateTime;
				return true;
			}
			dt = default(DateTime);
			return false;
		}

		// Token: 0x06006CC1 RID: 27841 RVA: 0x0020E30C File Offset: 0x0020E30C
		private static bool TryParseDateTimeOffsetMicrosoft(StringReference text, out DateTimeOffset dt)
		{
			long javaScriptTicks;
			TimeSpan timeSpan;
			DateTimeKind dateTimeKind;
			if (!DateTimeUtils.TryParseMicrosoftDate(text, out javaScriptTicks, out timeSpan, out dateTimeKind))
			{
				dt = default(DateTime);
				return false;
			}
			dt = new DateTimeOffset(DateTimeUtils.ConvertJavaScriptTicksToDateTime(javaScriptTicks).Add(timeSpan).Ticks, timeSpan);
			return true;
		}

		// Token: 0x06006CC2 RID: 27842 RVA: 0x0020E36C File Offset: 0x0020E36C
		private static bool TryParseDateTimeOffsetExact(string text, string dateFormatString, CultureInfo culture, out DateTimeOffset dt)
		{
			DateTimeOffset dateTimeOffset;
			if (DateTimeOffset.TryParseExact(text, dateFormatString, culture, DateTimeStyles.RoundtripKind, out dateTimeOffset))
			{
				dt = dateTimeOffset;
				return true;
			}
			dt = default(DateTimeOffset);
			return false;
		}

		// Token: 0x06006CC3 RID: 27843 RVA: 0x0020E3A4 File Offset: 0x0020E3A4
		private static bool TryReadOffset(StringReference offsetText, int startIndex, out TimeSpan offset)
		{
			bool flag = offsetText[startIndex] == '-';
			int num;
			if (ConvertUtils.Int32TryParse(offsetText.Chars, startIndex + 1, 2, out num) != ParseResult.Success)
			{
				offset = default(TimeSpan);
				return false;
			}
			int num2 = 0;
			if (offsetText.Length - startIndex > 5 && ConvertUtils.Int32TryParse(offsetText.Chars, startIndex + 3, 2, out num2) != ParseResult.Success)
			{
				offset = default(TimeSpan);
				return false;
			}
			offset = TimeSpan.FromHours((double)num) + TimeSpan.FromMinutes((double)num2);
			if (flag)
			{
				offset = offset.Negate();
			}
			return true;
		}

		// Token: 0x06006CC4 RID: 27844 RVA: 0x0020E444 File Offset: 0x0020E444
		internal static void WriteDateTimeString(TextWriter writer, DateTime value, DateFormatHandling format, [Nullable(2)] string formatString, CultureInfo culture)
		{
			if (StringUtils.IsNullOrEmpty(formatString))
			{
				char[] array = new char[64];
				int count = DateTimeUtils.WriteDateTimeString(array, 0, value, null, value.Kind, format);
				writer.Write(array, 0, count);
				return;
			}
			writer.Write(value.ToString(formatString, culture));
		}

		// Token: 0x06006CC5 RID: 27845 RVA: 0x0020E49C File Offset: 0x0020E49C
		internal static int WriteDateTimeString(char[] chars, int start, DateTime value, TimeSpan? offset, DateTimeKind kind, DateFormatHandling format)
		{
			int num2;
			if (format == DateFormatHandling.MicrosoftDateFormat)
			{
				TimeSpan offset2 = offset ?? value.GetUtcOffset();
				long num = DateTimeUtils.ConvertDateTimeToJavaScriptTicks(value, offset2);
				"\\/Date(".CopyTo(0, chars, start, 7);
				num2 = start + 7;
				string text = num.ToString(CultureInfo.InvariantCulture);
				text.CopyTo(0, chars, num2, text.Length);
				num2 += text.Length;
				if (kind != DateTimeKind.Unspecified)
				{
					if (kind == DateTimeKind.Local)
					{
						num2 = DateTimeUtils.WriteDateTimeOffset(chars, num2, offset2, format);
					}
				}
				else if (value != DateTime.MaxValue && value != DateTime.MinValue)
				{
					num2 = DateTimeUtils.WriteDateTimeOffset(chars, num2, offset2, format);
				}
				")\\/".CopyTo(0, chars, num2, 3);
				num2 += 3;
			}
			else
			{
				num2 = DateTimeUtils.WriteDefaultIsoDate(chars, start, value);
				if (kind != DateTimeKind.Utc)
				{
					if (kind == DateTimeKind.Local)
					{
						num2 = DateTimeUtils.WriteDateTimeOffset(chars, num2, offset ?? value.GetUtcOffset(), format);
					}
				}
				else
				{
					chars[num2++] = 'Z';
				}
			}
			return num2;
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x0020E5D4 File Offset: 0x0020E5D4
		internal static int WriteDefaultIsoDate(char[] chars, int start, DateTime dt)
		{
			int num = 19;
			int value;
			int value2;
			int value3;
			DateTimeUtils.GetDateValues(dt, out value, out value2, out value3);
			DateTimeUtils.CopyIntToCharArray(chars, start, value, 4);
			chars[start + 4] = '-';
			DateTimeUtils.CopyIntToCharArray(chars, start + 5, value2, 2);
			chars[start + 7] = '-';
			DateTimeUtils.CopyIntToCharArray(chars, start + 8, value3, 2);
			chars[start + 10] = 'T';
			DateTimeUtils.CopyIntToCharArray(chars, start + 11, dt.Hour, 2);
			chars[start + 13] = ':';
			DateTimeUtils.CopyIntToCharArray(chars, start + 14, dt.Minute, 2);
			chars[start + 16] = ':';
			DateTimeUtils.CopyIntToCharArray(chars, start + 17, dt.Second, 2);
			int num2 = (int)(dt.Ticks % 10000000L);
			if (num2 != 0)
			{
				int num3 = 7;
				while (num2 % 10 == 0)
				{
					num3--;
					num2 /= 10;
				}
				chars[start + 19] = '.';
				DateTimeUtils.CopyIntToCharArray(chars, start + 20, num2, num3);
				num += num3 + 1;
			}
			return start + num;
		}

		// Token: 0x06006CC7 RID: 27847 RVA: 0x0020E6C4 File Offset: 0x0020E6C4
		private static void CopyIntToCharArray(char[] chars, int start, int value, int digits)
		{
			while (digits-- != 0)
			{
				chars[start + digits] = (char)(value % 10 + 48);
				value /= 10;
			}
		}

		// Token: 0x06006CC8 RID: 27848 RVA: 0x0020E6E8 File Offset: 0x0020E6E8
		internal static int WriteDateTimeOffset(char[] chars, int start, TimeSpan offset, DateFormatHandling format)
		{
			chars[start++] = ((offset.Ticks >= 0L) ? '+' : '-');
			int value = Math.Abs(offset.Hours);
			DateTimeUtils.CopyIntToCharArray(chars, start, value, 2);
			start += 2;
			if (format == DateFormatHandling.IsoDateFormat)
			{
				chars[start++] = ':';
			}
			int value2 = Math.Abs(offset.Minutes);
			DateTimeUtils.CopyIntToCharArray(chars, start, value2, 2);
			start += 2;
			return start;
		}

		// Token: 0x06006CC9 RID: 27849 RVA: 0x0020E760 File Offset: 0x0020E760
		internal static void WriteDateTimeOffsetString(TextWriter writer, DateTimeOffset value, DateFormatHandling format, [Nullable(2)] string formatString, CultureInfo culture)
		{
			if (StringUtils.IsNullOrEmpty(formatString))
			{
				char[] array = new char[64];
				int count = DateTimeUtils.WriteDateTimeString(array, 0, (format == DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, new TimeSpan?(value.Offset), DateTimeKind.Local, format);
				writer.Write(array, 0, count);
				return;
			}
			writer.Write(value.ToString(formatString, culture));
		}

		// Token: 0x06006CCA RID: 27850 RVA: 0x0020E7D0 File Offset: 0x0020E7D0
		private static void GetDateValues(DateTime td, out int year, out int month, out int day)
		{
			int i = (int)(td.Ticks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			year = num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			i -= num4 * 365;
			int[] array = (num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTimeUtils.DaysToMonth366 : DateTimeUtils.DaysToMonth365;
			int num5 = i >> 6;
			while (i >= array[num5])
			{
				num5++;
			}
			month = num5;
			day = i - array[num5 - 1] + 1;
		}

		// Token: 0x040036AF RID: 13999
		internal static readonly long InitialJavaScriptDateTicks = 621355968000000000L;

		// Token: 0x040036B0 RID: 14000
		private const string IsoDateFormat = "yyyy-MM-ddTHH:mm:ss.FFFFFFFK";

		// Token: 0x040036B1 RID: 14001
		private const int DaysPer100Years = 36524;

		// Token: 0x040036B2 RID: 14002
		private const int DaysPer400Years = 146097;

		// Token: 0x040036B3 RID: 14003
		private const int DaysPer4Years = 1461;

		// Token: 0x040036B4 RID: 14004
		private const int DaysPerYear = 365;

		// Token: 0x040036B5 RID: 14005
		private const long TicksPerDay = 864000000000L;

		// Token: 0x040036B6 RID: 14006
		private static readonly int[] DaysToMonth365 = new int[]
		{
			0,
			31,
			59,
			90,
			120,
			151,
			181,
			212,
			243,
			273,
			304,
			334,
			365
		};

		// Token: 0x040036B7 RID: 14007
		private static readonly int[] DaysToMonth366 = new int[]
		{
			0,
			31,
			60,
			91,
			121,
			152,
			182,
			213,
			244,
			274,
			305,
			335,
			366
		};
	}
}
