using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000ACB RID: 2763
	[NullableContext(1)]
	[Nullable(0)]
	internal static class StringUtils
	{
		// Token: 0x06006DFF RID: 28159 RVA: 0x00214C70 File Offset: 0x00214C70
		[NullableContext(2)]
		public static bool IsNullOrEmpty([NotNullWhen(false)] string value)
		{
			return string.IsNullOrEmpty(value);
		}

		// Token: 0x06006E00 RID: 28160 RVA: 0x00214C78 File Offset: 0x00214C78
		public static string FormatWith(this string format, IFormatProvider provider, [Nullable(2)] object arg0)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0
			});
		}

		// Token: 0x06006E01 RID: 28161 RVA: 0x00214C8C File Offset: 0x00214C8C
		public static string FormatWith(this string format, IFormatProvider provider, [Nullable(2)] object arg0, [Nullable(2)] object arg1)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x06006E02 RID: 28162 RVA: 0x00214CA4 File Offset: 0x00214CA4
		public static string FormatWith(this string format, IFormatProvider provider, [Nullable(2)] object arg0, [Nullable(2)] object arg1, [Nullable(2)] object arg2)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x06006E03 RID: 28163 RVA: 0x00214CC0 File Offset: 0x00214CC0
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string FormatWith([Nullable(1)] this string format, [Nullable(1)] IFormatProvider provider, object arg0, object arg1, object arg2, object arg3)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x06006E04 RID: 28164 RVA: 0x00214CE4 File Offset: 0x00214CE4
		private static string FormatWith(this string format, IFormatProvider provider, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			ValidationUtils.ArgumentNotNull(format, "format");
			return string.Format(provider, format, args);
		}

		// Token: 0x06006E05 RID: 28165 RVA: 0x00214CFC File Offset: 0x00214CFC
		public static bool IsWhiteSpace(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsWhiteSpace(s[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006E06 RID: 28166 RVA: 0x00214D54 File Offset: 0x00214D54
		public static StringWriter CreateStringWriter(int capacity)
		{
			return new StringWriter(new StringBuilder(capacity), CultureInfo.InvariantCulture);
		}

		// Token: 0x06006E07 RID: 28167 RVA: 0x00214D68 File Offset: 0x00214D68
		public static void ToCharAsUnicode(char c, char[] buffer)
		{
			buffer[0] = '\\';
			buffer[1] = 'u';
			buffer[2] = MathUtils.IntToHex((int)(c >> 12 & '\u000f'));
			buffer[3] = MathUtils.IntToHex((int)(c >> 8 & '\u000f'));
			buffer[4] = MathUtils.IntToHex((int)(c >> 4 & '\u000f'));
			buffer[5] = MathUtils.IntToHex((int)(c & '\u000f'));
		}

		// Token: 0x06006E08 RID: 28168 RVA: 0x00214DBC File Offset: 0x00214DBC
		public static TSource ForgivingCaseSensitiveFind<[Nullable(2)] TSource>(this IEnumerable<TSource> source, Func<TSource, string> valueSelector, string testValue)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (valueSelector == null)
			{
				throw new ArgumentNullException("valueSelector");
			}
			IEnumerable<TSource> source2 = from s in source
			where string.Equals(valueSelector(s), testValue, StringComparison.OrdinalIgnoreCase)
			select s;
			if (source2.Count<TSource>() <= 1)
			{
				return source2.SingleOrDefault<TSource>();
			}
			return (from s in source
			where string.Equals(valueSelector(s), testValue, StringComparison.Ordinal)
			select s).SingleOrDefault<TSource>();
		}

		// Token: 0x06006E09 RID: 28169 RVA: 0x00214E48 File Offset: 0x00214E48
		public static string ToCamelCase(string s)
		{
			if (StringUtils.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
			{
				return s;
			}
			char[] array = s.ToCharArray();
			int num = 0;
			while (num < array.Length && (num != 1 || char.IsUpper(array[num])))
			{
				bool flag = num + 1 < array.Length;
				if (num > 0 && flag && !char.IsUpper(array[num + 1]))
				{
					if (char.IsSeparator(array[num + 1]))
					{
						array[num] = StringUtils.ToLower(array[num]);
						break;
					}
					break;
				}
				else
				{
					array[num] = StringUtils.ToLower(array[num]);
					num++;
				}
			}
			return new string(array);
		}

		// Token: 0x06006E0A RID: 28170 RVA: 0x00214EF8 File Offset: 0x00214EF8
		private static char ToLower(char c)
		{
			c = char.ToLower(c, CultureInfo.InvariantCulture);
			return c;
		}

		// Token: 0x06006E0B RID: 28171 RVA: 0x00214F08 File Offset: 0x00214F08
		public static string ToSnakeCase(string s)
		{
			return StringUtils.ToSeparatedCase(s, '_');
		}

		// Token: 0x06006E0C RID: 28172 RVA: 0x00214F14 File Offset: 0x00214F14
		public static string ToKebabCase(string s)
		{
			return StringUtils.ToSeparatedCase(s, '-');
		}

		// Token: 0x06006E0D RID: 28173 RVA: 0x00214F20 File Offset: 0x00214F20
		private static string ToSeparatedCase(string s, char separator)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringUtils.SeparatedCaseState separatedCaseState = StringUtils.SeparatedCaseState.Start;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == ' ')
				{
					if (separatedCaseState != StringUtils.SeparatedCaseState.Start)
					{
						separatedCaseState = StringUtils.SeparatedCaseState.NewWord;
					}
				}
				else if (char.IsUpper(s[i]))
				{
					switch (separatedCaseState)
					{
					case StringUtils.SeparatedCaseState.Lower:
					case StringUtils.SeparatedCaseState.NewWord:
						stringBuilder.Append(separator);
						break;
					case StringUtils.SeparatedCaseState.Upper:
					{
						bool flag = i + 1 < s.Length;
						if (i > 0 && flag)
						{
							char c = s[i + 1];
							if (!char.IsUpper(c) && c != separator)
							{
								stringBuilder.Append(separator);
							}
						}
						break;
					}
					}
					char value = char.ToLower(s[i], CultureInfo.InvariantCulture);
					stringBuilder.Append(value);
					separatedCaseState = StringUtils.SeparatedCaseState.Upper;
				}
				else if (s[i] == separator)
				{
					stringBuilder.Append(separator);
					separatedCaseState = StringUtils.SeparatedCaseState.Start;
				}
				else
				{
					if (separatedCaseState == StringUtils.SeparatedCaseState.NewWord)
					{
						stringBuilder.Append(separator);
					}
					stringBuilder.Append(s[i]);
					separatedCaseState = StringUtils.SeparatedCaseState.Lower;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006E0E RID: 28174 RVA: 0x00215054 File Offset: 0x00215054
		public static bool IsHighSurrogate(char c)
		{
			return char.IsHighSurrogate(c);
		}

		// Token: 0x06006E0F RID: 28175 RVA: 0x0021505C File Offset: 0x0021505C
		public static bool IsLowSurrogate(char c)
		{
			return char.IsLowSurrogate(c);
		}

		// Token: 0x06006E10 RID: 28176 RVA: 0x00215064 File Offset: 0x00215064
		public static bool StartsWith(this string source, char value)
		{
			return source.Length > 0 && source[0] == value;
		}

		// Token: 0x06006E11 RID: 28177 RVA: 0x00215080 File Offset: 0x00215080
		public static bool EndsWith(this string source, char value)
		{
			return source.Length > 0 && source[source.Length - 1] == value;
		}

		// Token: 0x06006E12 RID: 28178 RVA: 0x002150A4 File Offset: 0x002150A4
		public static string Trim(this string s, int start, int length)
		{
			if (s == null)
			{
				throw new ArgumentNullException();
			}
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = start + length - 1;
			if (num >= s.Length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			while (start < num)
			{
				if (!char.IsWhiteSpace(s[start]))
				{
					IL_81:
					while (num >= start && char.IsWhiteSpace(s[num]))
					{
						num--;
					}
					return s.Substring(start, num - start + 1);
				}
				start++;
			}
			goto IL_81;
		}

		// Token: 0x04003705 RID: 14085
		public const string CarriageReturnLineFeed = "\r\n";

		// Token: 0x04003706 RID: 14086
		public const string Empty = "";

		// Token: 0x04003707 RID: 14087
		public const char CarriageReturn = '\r';

		// Token: 0x04003708 RID: 14088
		public const char LineFeed = '\n';

		// Token: 0x04003709 RID: 14089
		public const char Tab = '\t';

		// Token: 0x020010F1 RID: 4337
		[NullableContext(0)]
		private enum SeparatedCaseState
		{
			// Token: 0x040048AF RID: 18607
			Start,
			// Token: 0x040048B0 RID: 18608
			Lower,
			// Token: 0x040048B1 RID: 18609
			Upper,
			// Token: 0x040048B2 RID: 18610
			NewWord
		}
	}
}
