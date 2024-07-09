using System;

namespace IP2Region
{
	// Token: 0x02000A5C RID: 2652
	internal static class Utils
	{
		// Token: 0x06006806 RID: 26630 RVA: 0x001FAC48 File Offset: 0x001FAC48
		public static void Write(byte[] b, int offset, ulong v, int bytes)
		{
			for (int i = 0; i < bytes; i++)
			{
				b[offset++] = (byte)(v >> 8 * i & 255UL);
			}
		}

		// Token: 0x06006807 RID: 26631 RVA: 0x001FAC80 File Offset: 0x001FAC80
		public static void WriteIntLong(byte[] b, int offset, long v)
		{
			b[offset++] = (byte)(v & 255L);
			b[offset++] = (byte)(v >> 8 & 255L);
			b[offset++] = (byte)(v >> 16 & 255L);
			b[offset] = (byte)(v >> 24 & 255L);
		}

		// Token: 0x06006808 RID: 26632 RVA: 0x001FACD8 File Offset: 0x001FACD8
		public static long GetIntLong(byte[] b, int offset)
		{
			return (long)(((ulong)b[offset++] & 255UL) | (ulong)((long)((long)b[offset++] << 8) & 65280L) | (ulong)((long)((long)b[offset++] << 16) & 16711680L) | (ulong)((long)((long)b[offset] << 24) & (long)((ulong)-16777216)));
		}

		// Token: 0x06006809 RID: 26633 RVA: 0x001FAD30 File Offset: 0x001FAD30
		public static int GetInt3(byte[] b, int offset)
		{
			return (int)(b[offset++] & byte.MaxValue) | ((int)b[offset++] & 65280) | ((int)b[offset] & 16711680);
		}

		// Token: 0x0600680A RID: 26634 RVA: 0x001FAD5C File Offset: 0x001FAD5C
		public static int GetInt2(byte[] b, int offset)
		{
			return (int)(b[offset++] & byte.MaxValue) | ((int)b[offset] & 65280);
		}

		// Token: 0x0600680B RID: 26635 RVA: 0x001FAD78 File Offset: 0x001FAD78
		public static int GetInt1(byte[] b, int offset)
		{
			return (int)(b[offset] & byte.MaxValue);
		}

		// Token: 0x0600680C RID: 26636 RVA: 0x001FAD84 File Offset: 0x001FAD84
		public static long Ip2long(string ip)
		{
			string[] array = ip.Split(new char[]
			{
				'.'
			});
			if (array.Length != 4)
			{
				throw new IPInValidException();
			}
			foreach (string text in array)
			{
				if (text.Length > 3)
				{
					throw new IPInValidException();
				}
				int num;
				if (!int.TryParse(text, out num) || num > 255)
				{
					throw new IPInValidException();
				}
			}
			long num2;
			bool flag = long.TryParse(array[0], out num2);
			long num3;
			bool flag2 = long.TryParse(array[1], out num3);
			long num4;
			bool flag3 = long.TryParse(array[2], out num4);
			long num5;
			bool flag4 = long.TryParse(array[3], out num5);
			if (!flag || !flag2 || !flag3 || !flag4 || num5 > 255L || num2 > 255L || num3 > 255L || num4 > 255L || num5 < 0L || num2 < 0L || num3 < 0L || num4 < 0L)
			{
				throw new IPInValidException();
			}
			long num6 = num2 << 24 & (long)((ulong)-16777216);
			long num7 = num3 << 16 & 16711680L;
			long num8 = num4 << 8 & 65280L;
			long num9 = num5 & 255L;
			return (num6 | num7 | num8 | num9) & (long)((ulong)-1);
		}

		// Token: 0x0600680D RID: 26637 RVA: 0x001FAEF8 File Offset: 0x001FAEF8
		public static string Long2ip(long ip)
		{
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				ip >> 24 & 255L,
				ip >> 16 & 255L,
				ip >> 8 & 255L,
				ip & 255L
			});
		}
	}
}
