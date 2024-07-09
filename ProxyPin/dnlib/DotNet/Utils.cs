using System;
using System.Text;

namespace dnlib.DotNet
{
	// Token: 0x02000885 RID: 2181
	internal static class Utils
	{
		// Token: 0x0600538B RID: 21387 RVA: 0x001971F0 File Offset: 0x001971F0
		internal static string GetAssemblyNameString(UTF8String name, Version version, UTF8String culture, PublicKeyBase publicKey, AssemblyAttributes attributes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in UTF8String.ToSystemStringOrEmpty(name))
			{
				if (c == ',' || c == '=')
				{
					stringBuilder.Append('\\');
				}
				stringBuilder.Append(c);
			}
			if (version != null)
			{
				stringBuilder.Append(", Version=");
				stringBuilder.Append(Utils.CreateVersionWithNoUndefinedValues(version).ToString());
			}
			if (culture != null)
			{
				stringBuilder.Append(", Culture=");
				stringBuilder.Append(UTF8String.IsNullOrEmpty(culture) ? "neutral" : culture.String);
			}
			stringBuilder.Append(", ");
			stringBuilder.Append((publicKey == null || publicKey is PublicKeyToken) ? "PublicKeyToken=" : "PublicKey=");
			stringBuilder.Append((publicKey == null) ? "null" : publicKey.ToString());
			if ((attributes & AssemblyAttributes.Retargetable) != AssemblyAttributes.None)
			{
				stringBuilder.Append(", Retargetable=Yes");
			}
			if ((attributes & AssemblyAttributes.ContentType_Mask) == AssemblyAttributes.ContentType_WindowsRuntime)
			{
				stringBuilder.Append(", ContentType=WindowsRuntime");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x00197330 File Offset: 0x00197330
		internal static string ToHex(byte[] bytes, bool upper)
		{
			if (bytes == null)
			{
				return "";
			}
			char[] array = new char[bytes.Length * 2];
			int i = 0;
			int num = 0;
			while (i < bytes.Length)
			{
				byte b = bytes[i];
				array[num++] = Utils.ToHexChar(b >> 4, upper);
				array[num++] = Utils.ToHexChar((int)(b & 15), upper);
				i++;
			}
			return new string(array);
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x00197398 File Offset: 0x00197398
		private static char ToHexChar(int val, bool upper)
		{
			if (0 <= val && val <= 9)
			{
				return (char)(val + 48);
			}
			return (char)(val - 10 + (upper ? 65 : 97));
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x001973C4 File Offset: 0x001973C4
		internal static byte[] ParseBytes(string hexString)
		{
			byte[] result;
			try
			{
				if (hexString.Length % 2 != 0)
				{
					result = null;
				}
				else
				{
					byte[] array = new byte[hexString.Length / 2];
					for (int i = 0; i < hexString.Length; i += 2)
					{
						int num = Utils.TryParseHexChar(hexString[i]);
						int num2 = Utils.TryParseHexChar(hexString[i + 1]);
						if (num < 0 || num2 < 0)
						{
							return null;
						}
						array[i / 2] = (byte)(num << 4 | num2);
					}
					result = array;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x00197468 File Offset: 0x00197468
		private static int TryParseHexChar(char c)
		{
			if ('0' <= c && c <= '9')
			{
				return (int)(c - '0');
			}
			if ('a' <= c && c <= 'f')
			{
				return (int)('\n' + c - 'a');
			}
			if ('A' <= c && c <= 'F')
			{
				return (int)('\n' + c - 'A');
			}
			return -1;
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x001974C0 File Offset: 0x001974C0
		internal static int CompareTo(byte[] a, byte[] b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			int num = Math.Min(a.Length, b.Length);
			for (int i = 0; i < num; i++)
			{
				byte b2 = a[i];
				byte b3 = b[i];
				if (b2 < b3)
				{
					return -1;
				}
				if (b2 > b3)
				{
					return 1;
				}
			}
			return a.Length.CompareTo(b.Length);
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x00197530 File Offset: 0x00197530
		internal static bool Equals(byte[] a, byte[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x00197584 File Offset: 0x00197584
		internal static int GetHashCode(byte[] a)
		{
			if (a == null || a.Length == 0)
			{
				return 0;
			}
			int num = Math.Min(a.Length / 2, 20);
			if (num == 0)
			{
				num = 1;
			}
			uint num2 = 0U;
			int i = 0;
			int num3 = a.Length - 1;
			while (i < num)
			{
				num2 ^= (uint)((int)a[i] | (int)a[num3] << 8);
				num2 = (num2 << 13 | num2 >> 19);
				i++;
				num3--;
			}
			return (int)num2;
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x001975EC File Offset: 0x001975EC
		internal static int CompareTo(Version a, Version b)
		{
			if (a == null)
			{
				a = new Version();
			}
			if (b == null)
			{
				b = new Version();
			}
			if (a.Major != b.Major)
			{
				return a.Major.CompareTo(b.Major);
			}
			if (a.Minor != b.Minor)
			{
				return a.Minor.CompareTo(b.Minor);
			}
			if (Utils.GetDefaultVersionValue(a.Build) != Utils.GetDefaultVersionValue(b.Build))
			{
				return Utils.GetDefaultVersionValue(a.Build).CompareTo(Utils.GetDefaultVersionValue(b.Build));
			}
			return Utils.GetDefaultVersionValue(a.Revision).CompareTo(Utils.GetDefaultVersionValue(b.Revision));
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x001976BC File Offset: 0x001976BC
		internal static bool Equals(Version a, Version b)
		{
			return Utils.CompareTo(a, b) == 0;
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x001976C8 File Offset: 0x001976C8
		internal static Version CreateVersionWithNoUndefinedValues(Version a)
		{
			if (a == null)
			{
				return new Version(0, 0, 0, 0);
			}
			return new Version(a.Major, a.Minor, Utils.GetDefaultVersionValue(a.Build), Utils.GetDefaultVersionValue(a.Revision));
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x00197710 File Offset: 0x00197710
		private static int GetDefaultVersionValue(int val)
		{
			if (val != -1)
			{
				return val;
			}
			return 0;
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x0019771C File Offset: 0x0019771C
		internal static Version ParseVersion(string versionString)
		{
			Version result;
			try
			{
				result = Utils.CreateVersionWithNoUndefinedValues(new Version(versionString));
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x00197754 File Offset: 0x00197754
		internal static int LocaleCompareTo(UTF8String a, UTF8String b)
		{
			return Utils.GetCanonicalLocale(a).CompareTo(Utils.GetCanonicalLocale(b));
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x00197768 File Offset: 0x00197768
		internal static bool LocaleEquals(UTF8String a, UTF8String b)
		{
			return Utils.LocaleCompareTo(a, b) == 0;
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x00197774 File Offset: 0x00197774
		internal static int LocaleCompareTo(UTF8String a, string b)
		{
			return Utils.GetCanonicalLocale(a).CompareTo(Utils.GetCanonicalLocale(b));
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x00197788 File Offset: 0x00197788
		internal static bool LocaleEquals(UTF8String a, string b)
		{
			return Utils.LocaleCompareTo(a, b) == 0;
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x00197794 File Offset: 0x00197794
		internal static int GetHashCodeLocale(UTF8String a)
		{
			return Utils.GetCanonicalLocale(a).GetHashCode();
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x001977A4 File Offset: 0x001977A4
		private static string GetCanonicalLocale(UTF8String locale)
		{
			return Utils.GetCanonicalLocale(UTF8String.ToSystemStringOrEmpty(locale));
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x001977B4 File Offset: 0x001977B4
		private static string GetCanonicalLocale(string locale)
		{
			string text = locale.ToUpperInvariant();
			if (text == "NEUTRAL")
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x001977E4 File Offset: 0x001977E4
		public static uint AlignUp(uint v, uint alignment)
		{
			return v + alignment - 1U & ~(alignment - 1U);
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x001977F0 File Offset: 0x001977F0
		public static int AlignUp(int v, uint alignment)
		{
			return (int)Utils.AlignUp((uint)v, alignment);
		}
	}
}
