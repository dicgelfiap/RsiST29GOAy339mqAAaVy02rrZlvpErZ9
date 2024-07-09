using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.DotNet
{
	// Token: 0x02000883 RID: 2179
	[DebuggerDisplay("{String}")]
	[ComVisible(true)]
	public sealed class UTF8String : IEquatable<UTF8String>, IComparable<UTF8String>
	{
		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x0600533D RID: 21309 RVA: 0x00196C0C File Offset: 0x00196C0C
		public string String
		{
			get
			{
				if (this.asString == null)
				{
					this.asString = UTF8String.ConvertFromUTF8(this.data);
				}
				return this.asString;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x0600533E RID: 21310 RVA: 0x00196C30 File Offset: 0x00196C30
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x0600533F RID: 21311 RVA: 0x00196C38 File Offset: 0x00196C38
		public int Length
		{
			get
			{
				return this.String.Length;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06005340 RID: 21312 RVA: 0x00196C48 File Offset: 0x00196C48
		public int DataLength
		{
			get
			{
				if (this.data != null)
				{
					return this.data.Length;
				}
				return 0;
			}
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x00196C60 File Offset: 0x00196C60
		public static bool IsNull(UTF8String utf8)
		{
			return utf8 == null || utf8.data == null;
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x00196C74 File Offset: 0x00196C74
		public static bool IsNullOrEmpty(UTF8String utf8)
		{
			return utf8 == null || utf8.data == null || utf8.data.Length == 0;
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x00196C94 File Offset: 0x00196C94
		public static implicit operator string(UTF8String s)
		{
			return UTF8String.ToSystemString(s);
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x00196C9C File Offset: 0x00196C9C
		public static implicit operator UTF8String(string s)
		{
			if (s != null)
			{
				return new UTF8String(s);
			}
			return null;
		}

		// Token: 0x06005345 RID: 21317 RVA: 0x00196CAC File Offset: 0x00196CAC
		public static string ToSystemString(UTF8String utf8)
		{
			if (utf8 == null || utf8.data == null)
			{
				return null;
			}
			if (utf8.data.Length == 0)
			{
				return string.Empty;
			}
			return utf8.String;
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x00196CDC File Offset: 0x00196CDC
		public static string ToSystemStringOrEmpty(UTF8String utf8)
		{
			return UTF8String.ToSystemString(utf8) ?? string.Empty;
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x00196CF0 File Offset: 0x00196CF0
		public static int GetHashCode(UTF8String utf8)
		{
			if (UTF8String.IsNullOrEmpty(utf8))
			{
				return 0;
			}
			return Utils.GetHashCode(utf8.data);
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x00196D0C File Offset: 0x00196D0C
		public int CompareTo(UTF8String other)
		{
			return UTF8String.CompareTo(this, other);
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x00196D18 File Offset: 0x00196D18
		public static int CompareTo(UTF8String a, UTF8String b)
		{
			return Utils.CompareTo((a != null) ? a.data : null, (b != null) ? b.data : null);
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x00196D44 File Offset: 0x00196D44
		public static int CaseInsensitiveCompareTo(UTF8String a, UTF8String b)
		{
			if (a == b)
			{
				return 0;
			}
			string text = UTF8String.ToSystemString(a);
			string text2 = UTF8String.ToSystemString(b);
			if (text == text2)
			{
				return 0;
			}
			if (text == null)
			{
				return -1;
			}
			if (text2 == null)
			{
				return 1;
			}
			return StringComparer.OrdinalIgnoreCase.Compare(text, text2);
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x00196D94 File Offset: 0x00196D94
		public static bool CaseInsensitiveEquals(UTF8String a, UTF8String b)
		{
			return UTF8String.CaseInsensitiveCompareTo(a, b) == 0;
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x00196DA0 File Offset: 0x00196DA0
		public static bool operator ==(UTF8String left, UTF8String right)
		{
			return UTF8String.CompareTo(left, right) == 0;
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x00196DAC File Offset: 0x00196DAC
		public static bool operator ==(UTF8String left, string right)
		{
			return UTF8String.ToSystemString(left) == right;
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x00196DBC File Offset: 0x00196DBC
		public static bool operator ==(string left, UTF8String right)
		{
			return left == UTF8String.ToSystemString(right);
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x00196DCC File Offset: 0x00196DCC
		public static bool operator !=(UTF8String left, UTF8String right)
		{
			return UTF8String.CompareTo(left, right) != 0;
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x00196DD8 File Offset: 0x00196DD8
		public static bool operator !=(UTF8String left, string right)
		{
			return UTF8String.ToSystemString(left) != right;
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x00196DE8 File Offset: 0x00196DE8
		public static bool operator !=(string left, UTF8String right)
		{
			return left != UTF8String.ToSystemString(right);
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x00196DF8 File Offset: 0x00196DF8
		public static bool operator >(UTF8String left, UTF8String right)
		{
			return UTF8String.CompareTo(left, right) > 0;
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x00196E04 File Offset: 0x00196E04
		public static bool operator <(UTF8String left, UTF8String right)
		{
			return UTF8String.CompareTo(left, right) < 0;
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x00196E10 File Offset: 0x00196E10
		public static bool operator >=(UTF8String left, UTF8String right)
		{
			return UTF8String.CompareTo(left, right) >= 0;
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x00196E20 File Offset: 0x00196E20
		public static bool operator <=(UTF8String left, UTF8String right)
		{
			return UTF8String.CompareTo(left, right) <= 0;
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x00196E30 File Offset: 0x00196E30
		public UTF8String(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x00196E40 File Offset: 0x00196E40
		public UTF8String(string s) : this((s == null) ? null : Encoding.UTF8.GetBytes(s))
		{
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x00196E60 File Offset: 0x00196E60
		private static string ConvertFromUTF8(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			try
			{
				return Encoding.UTF8.GetString(data);
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x00196EA0 File Offset: 0x00196EA0
		public static bool Equals(UTF8String a, UTF8String b)
		{
			return UTF8String.CompareTo(a, b) == 0;
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x00196EAC File Offset: 0x00196EAC
		public bool Equals(UTF8String other)
		{
			return UTF8String.CompareTo(this, other) == 0;
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x00196EB8 File Offset: 0x00196EB8
		public override bool Equals(object obj)
		{
			UTF8String utf8String = obj as UTF8String;
			return utf8String != null && UTF8String.CompareTo(this, utf8String) == 0;
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x00196EE4 File Offset: 0x00196EE4
		public bool Contains(string value)
		{
			return this.String.Contains(value);
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x00196EF4 File Offset: 0x00196EF4
		public bool EndsWith(string value)
		{
			return this.String.EndsWith(value);
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x00196F04 File Offset: 0x00196F04
		public bool EndsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			return this.String.EndsWith(value, ignoreCase, culture);
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x00196F14 File Offset: 0x00196F14
		public bool EndsWith(string value, StringComparison comparisonType)
		{
			return this.String.EndsWith(value, comparisonType);
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x00196F24 File Offset: 0x00196F24
		public bool StartsWith(string value)
		{
			return this.String.StartsWith(value);
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x00196F34 File Offset: 0x00196F34
		public bool StartsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			return this.String.StartsWith(value, ignoreCase, culture);
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x00196F44 File Offset: 0x00196F44
		public bool StartsWith(string value, StringComparison comparisonType)
		{
			return this.String.StartsWith(value, comparisonType);
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x00196F54 File Offset: 0x00196F54
		public int CompareTo(string strB)
		{
			return this.String.CompareTo(strB);
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x00196F64 File Offset: 0x00196F64
		public int IndexOf(char value)
		{
			return this.String.IndexOf(value);
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x00196F74 File Offset: 0x00196F74
		public int IndexOf(char value, int startIndex)
		{
			return this.String.IndexOf(value, startIndex);
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x00196F84 File Offset: 0x00196F84
		public int IndexOf(char value, int startIndex, int count)
		{
			return this.String.IndexOf(value, startIndex, count);
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x00196F94 File Offset: 0x00196F94
		public int IndexOf(string value)
		{
			return this.String.IndexOf(value);
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x00196FA4 File Offset: 0x00196FA4
		public int IndexOf(string value, int startIndex)
		{
			return this.String.IndexOf(value, startIndex);
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x00196FB4 File Offset: 0x00196FB4
		public int IndexOf(string value, int startIndex, int count)
		{
			return this.String.IndexOf(value, startIndex, count);
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x00196FC4 File Offset: 0x00196FC4
		public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			return this.String.IndexOf(value, startIndex, count, comparisonType);
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x00196FD8 File Offset: 0x00196FD8
		public int IndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.String.IndexOf(value, startIndex, comparisonType);
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x00196FE8 File Offset: 0x00196FE8
		public int IndexOf(string value, StringComparison comparisonType)
		{
			return this.String.IndexOf(value, comparisonType);
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x00196FF8 File Offset: 0x00196FF8
		public int LastIndexOf(char value)
		{
			return this.String.LastIndexOf(value);
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x00197008 File Offset: 0x00197008
		public int LastIndexOf(char value, int startIndex)
		{
			return this.String.LastIndexOf(value, startIndex);
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x00197018 File Offset: 0x00197018
		public int LastIndexOf(char value, int startIndex, int count)
		{
			return this.String.LastIndexOf(value, startIndex, count);
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x00197028 File Offset: 0x00197028
		public int LastIndexOf(string value)
		{
			return this.String.LastIndexOf(value);
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x00197038 File Offset: 0x00197038
		public int LastIndexOf(string value, int startIndex)
		{
			return this.String.LastIndexOf(value, startIndex);
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x00197048 File Offset: 0x00197048
		public int LastIndexOf(string value, int startIndex, int count)
		{
			return this.String.LastIndexOf(value, startIndex, count);
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x00197058 File Offset: 0x00197058
		public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			return this.String.LastIndexOf(value, startIndex, count, comparisonType);
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0019706C File Offset: 0x0019706C
		public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.String.LastIndexOf(value, startIndex, comparisonType);
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x0019707C File Offset: 0x0019707C
		public int LastIndexOf(string value, StringComparison comparisonType)
		{
			return this.String.LastIndexOf(value, comparisonType);
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x0019708C File Offset: 0x0019708C
		public UTF8String Insert(int startIndex, string value)
		{
			return new UTF8String(this.String.Insert(startIndex, value));
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x001970A0 File Offset: 0x001970A0
		public UTF8String Remove(int startIndex)
		{
			return new UTF8String(this.String.Remove(startIndex));
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x001970B4 File Offset: 0x001970B4
		public UTF8String Remove(int startIndex, int count)
		{
			return new UTF8String(this.String.Remove(startIndex, count));
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x001970C8 File Offset: 0x001970C8
		public UTF8String Replace(char oldChar, char newChar)
		{
			return new UTF8String(this.String.Replace(oldChar, newChar));
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x001970DC File Offset: 0x001970DC
		public UTF8String Replace(string oldValue, string newValue)
		{
			return new UTF8String(this.String.Replace(oldValue, newValue));
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x001970F0 File Offset: 0x001970F0
		public UTF8String Substring(int startIndex)
		{
			return new UTF8String(this.String.Substring(startIndex));
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x00197104 File Offset: 0x00197104
		public UTF8String Substring(int startIndex, int length)
		{
			return new UTF8String(this.String.Substring(startIndex, length));
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x00197118 File Offset: 0x00197118
		public UTF8String ToLower()
		{
			return new UTF8String(this.String.ToLower());
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x0019712C File Offset: 0x0019712C
		public UTF8String ToLower(CultureInfo culture)
		{
			return new UTF8String(this.String.ToLower(culture));
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x00197140 File Offset: 0x00197140
		public UTF8String ToLowerInvariant()
		{
			return new UTF8String(this.String.ToLowerInvariant());
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x00197154 File Offset: 0x00197154
		public UTF8String ToUpper()
		{
			return new UTF8String(this.String.ToUpper());
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x00197168 File Offset: 0x00197168
		public UTF8String ToUpper(CultureInfo culture)
		{
			return new UTF8String(this.String.ToUpper(culture));
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x0019717C File Offset: 0x0019717C
		public UTF8String ToUpperInvariant()
		{
			return new UTF8String(this.String.ToUpperInvariant());
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x00197190 File Offset: 0x00197190
		public UTF8String Trim()
		{
			return new UTF8String(this.String.Trim());
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x001971A4 File Offset: 0x001971A4
		public override int GetHashCode()
		{
			return UTF8String.GetHashCode(this);
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x001971AC File Offset: 0x001971AC
		public override string ToString()
		{
			return this.String;
		}

		// Token: 0x040027E6 RID: 10214
		public static readonly UTF8String Empty = new UTF8String(string.Empty);

		// Token: 0x040027E7 RID: 10215
		private readonly byte[] data;

		// Token: 0x040027E8 RID: 10216
		private string asString;
	}
}
