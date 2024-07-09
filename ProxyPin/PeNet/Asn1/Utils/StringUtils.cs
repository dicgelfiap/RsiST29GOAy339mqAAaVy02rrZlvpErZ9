using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PeNet.Asn1.Utils
{
	// Token: 0x02000B87 RID: 2951
	[ComVisible(true)]
	public static class StringUtils
	{
		// Token: 0x060076B5 RID: 30389 RVA: 0x00238080 File Offset: 0x00238080
		public static byte[] GetBytesFromHexString(string val)
		{
			byte[] array = new byte[(val.Length + 1) / 2];
			for (int i = 0; i < val.Length; i++)
			{
				byte hexDigit = StringUtils.GetHexDigit(val[i]);
				byte b = array[i / 2];
				if ((i & 1) == 0)
				{
					b = (byte)((int)(b & 15) | (int)hexDigit << 4);
				}
				else
				{
					b = ((b & 240) | hexDigit);
				}
				array[i / 2] = b;
			}
			return array;
		}

		// Token: 0x060076B6 RID: 30390 RVA: 0x002380F4 File Offset: 0x002380F4
		public static string GetHexString(this byte[] data)
		{
			StringBuilder stringBuilder = new StringBuilder(data.Length * 2);
			foreach (byte b in data)
			{
				stringBuilder.Append(StringUtils.GetHexDigit((byte)(b >> 4)));
				stringBuilder.Append(StringUtils.GetHexDigit(b & 15));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060076B7 RID: 30391 RVA: 0x00238150 File Offset: 0x00238150
		private static char GetHexDigit(byte bt)
		{
			if (bt <= 9)
			{
				return (char)(48 + bt);
			}
			if (bt <= 15)
			{
				return (char)(97 + bt - 10);
			}
			throw new ArgumentException();
		}

		// Token: 0x060076B8 RID: 30392 RVA: 0x00238178 File Offset: 0x00238178
		private static byte GetHexDigit(char ch)
		{
			if (ch >= '0' && ch <= '9')
			{
				return (byte)(ch - '0');
			}
			if (ch >= 'a' && ch <= 'f')
			{
				return (byte)(ch - 'a' + '\n');
			}
			if (ch >= 'A' && ch <= 'F')
			{
				return (byte)(ch - 'A' + '\n');
			}
			throw new FormatException();
		}
	}
}
