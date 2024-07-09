using System;
using System.Text;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x02000701 RID: 1793
	public abstract class Strings
	{
		// Token: 0x06003ECF RID: 16079 RVA: 0x00159CA8 File Offset: 0x00159CA8
		public static string ToUpperCase(string original)
		{
			bool flag = false;
			char[] array = original.ToCharArray();
			for (int num = 0; num != array.Length; num++)
			{
				char c = array[num];
				if ('a' <= c && 'z' >= c)
				{
					flag = true;
					array[num] = c - 'a' + 'A';
				}
			}
			if (flag)
			{
				return new string(array);
			}
			return original;
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x00159D04 File Offset: 0x00159D04
		internal static bool IsOneOf(string s, params string[] candidates)
		{
			foreach (string b in candidates)
			{
				if (s == b)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x00159D48 File Offset: 0x00159D48
		public static string FromByteArray(byte[] bs)
		{
			char[] array = new char[bs.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToChar(bs[i]);
			}
			return new string(array);
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x00159D84 File Offset: 0x00159D84
		public static byte[] ToByteArray(char[] cs)
		{
			byte[] array = new byte[cs.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToByte(cs[i]);
			}
			return array;
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x00159DBC File Offset: 0x00159DBC
		public static byte[] ToByteArray(string s)
		{
			byte[] array = new byte[s.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToByte(s[i]);
			}
			return array;
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x00159DFC File Offset: 0x00159DFC
		public static string FromAsciiByteArray(byte[] bytes)
		{
			return Encoding.ASCII.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x00159E10 File Offset: 0x00159E10
		public static byte[] ToAsciiByteArray(char[] cs)
		{
			return Encoding.ASCII.GetBytes(cs);
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x00159E20 File Offset: 0x00159E20
		public static byte[] ToAsciiByteArray(string s)
		{
			return Encoding.ASCII.GetBytes(s);
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x00159E30 File Offset: 0x00159E30
		public static string FromUtf8ByteArray(byte[] bytes)
		{
			return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x00159E44 File Offset: 0x00159E44
		public static byte[] ToUtf8ByteArray(char[] cs)
		{
			return Encoding.UTF8.GetBytes(cs);
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x00159E54 File Offset: 0x00159E54
		public static byte[] ToUtf8ByteArray(string s)
		{
			return Encoding.UTF8.GetBytes(s);
		}
	}
}
