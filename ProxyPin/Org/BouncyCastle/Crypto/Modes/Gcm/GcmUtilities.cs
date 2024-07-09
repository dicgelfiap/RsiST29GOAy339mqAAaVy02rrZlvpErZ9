using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x020003F4 RID: 1012
	internal abstract class GcmUtilities
	{
		// Token: 0x0600200F RID: 8207 RVA: 0x000BABAC File Offset: 0x000BABAC
		private static uint[] GenerateLookup()
		{
			uint[] array = new uint[256];
			for (int i = 0; i < 256; i++)
			{
				uint num = 0U;
				for (int j = 7; j >= 0; j--)
				{
					if ((i & 1 << j) != 0)
					{
						num ^= 3774873600U >> 7 - j;
					}
				}
				array[i] = num;
			}
			return array;
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x000BAC10 File Offset: 0x000BAC10
		internal static byte[] OneAsBytes()
		{
			byte[] array = new byte[16];
			array[0] = 128;
			return array;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x000BAC34 File Offset: 0x000BAC34
		internal static uint[] OneAsUints()
		{
			uint[] array = new uint[4];
			array[0] = 2147483648U;
			return array;
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000BAC58 File Offset: 0x000BAC58
		internal static ulong[] OneAsUlongs()
		{
			ulong[] array = new ulong[2];
			array[0] = 9223372036854775808UL;
			return array;
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000BAC80 File Offset: 0x000BAC80
		internal static byte[] AsBytes(uint[] x)
		{
			return Pack.UInt32_To_BE(x);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x000BAC88 File Offset: 0x000BAC88
		internal static void AsBytes(uint[] x, byte[] z)
		{
			Pack.UInt32_To_BE(x, z, 0);
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x000BAC94 File Offset: 0x000BAC94
		internal static byte[] AsBytes(ulong[] x)
		{
			byte[] array = new byte[16];
			Pack.UInt64_To_BE(x, array, 0);
			return array;
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x000BACB8 File Offset: 0x000BACB8
		internal static void AsBytes(ulong[] x, byte[] z)
		{
			Pack.UInt64_To_BE(x, z, 0);
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000BACC4 File Offset: 0x000BACC4
		internal static uint[] AsUints(byte[] bs)
		{
			uint[] array = new uint[4];
			Pack.BE_To_UInt32(bs, 0, array);
			return array;
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x000BACE8 File Offset: 0x000BACE8
		internal static void AsUints(byte[] bs, uint[] output)
		{
			Pack.BE_To_UInt32(bs, 0, output);
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x000BACF4 File Offset: 0x000BACF4
		internal static ulong[] AsUlongs(byte[] x)
		{
			ulong[] array = new ulong[2];
			Pack.BE_To_UInt64(x, 0, array);
			return array;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000BAD18 File Offset: 0x000BAD18
		public static void AsUlongs(byte[] x, ulong[] z)
		{
			Pack.BE_To_UInt64(x, 0, z);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x000BAD24 File Offset: 0x000BAD24
		internal static void Multiply(byte[] x, byte[] y)
		{
			uint[] x2 = GcmUtilities.AsUints(x);
			uint[] y2 = GcmUtilities.AsUints(y);
			GcmUtilities.Multiply(x2, y2);
			GcmUtilities.AsBytes(x2, x);
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x000BAD54 File Offset: 0x000BAD54
		internal static void Multiply(uint[] x, uint[] y)
		{
			uint num = x[0];
			uint num2 = x[1];
			uint num3 = x[2];
			uint num4 = x[3];
			uint num5 = 0U;
			uint num6 = 0U;
			uint num7 = 0U;
			uint num8 = 0U;
			for (int i = 0; i < 4; i++)
			{
				int num9 = (int)y[i];
				for (int j = 0; j < 32; j++)
				{
					uint num10 = (uint)(num9 >> 31);
					num9 <<= 1;
					num5 ^= (num & num10);
					num6 ^= (num2 & num10);
					num7 ^= (num3 & num10);
					num8 ^= (num4 & num10);
					uint num11 = (uint)((int)((int)num4 << 31) >> 8);
					num4 = (num4 >> 1 | num3 << 31);
					num3 = (num3 >> 1 | num2 << 31);
					num2 = (num2 >> 1 | num << 31);
					num = (num >> 1 ^ (num11 & 3774873600U));
				}
			}
			x[0] = num5;
			x[1] = num6;
			x[2] = num7;
			x[3] = num8;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x000BAE28 File Offset: 0x000BAE28
		internal static void Multiply(ulong[] x, ulong[] y)
		{
			ulong num = x[0];
			ulong num2 = x[1];
			ulong num3 = 0UL;
			ulong num4 = 0UL;
			for (int i = 0; i < 2; i++)
			{
				long num5 = (long)y[i];
				for (int j = 0; j < 64; j++)
				{
					ulong num6 = (ulong)(num5 >> 63);
					num5 <<= 1;
					num3 ^= (num & num6);
					num4 ^= (num2 & num6);
					ulong num7 = num2 << 63 >> 8;
					num2 = (num2 >> 1 | num << 63);
					num = (num >> 1 ^ (num7 & 16212958658533785600UL));
				}
			}
			x[0] = num3;
			x[1] = num4;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x000BAEBC File Offset: 0x000BAEBC
		internal static void MultiplyP(uint[] x)
		{
			uint num = (uint)((int)GcmUtilities.ShiftRight(x) >> 8);
			x[0] = (x[0] ^ (num & 3774873600U));
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x000BAEE8 File Offset: 0x000BAEE8
		internal static void MultiplyP(uint[] x, uint[] z)
		{
			uint num = (uint)((int)GcmUtilities.ShiftRight(x, z) >> 8);
			z[0] = (z[0] ^ (num & 3774873600U));
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x000BAF14 File Offset: 0x000BAF14
		internal static void MultiplyP8(uint[] x)
		{
			uint num = GcmUtilities.ShiftRightN(x, 8);
			x[0] = (x[0] ^ GcmUtilities.LOOKUP[(int)((UIntPtr)(num >> 24))]);
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000BAF44 File Offset: 0x000BAF44
		internal static void MultiplyP8(uint[] x, uint[] y)
		{
			uint num = GcmUtilities.ShiftRightN(x, 8, y);
			y[0] = (y[0] ^ GcmUtilities.LOOKUP[(int)((UIntPtr)(num >> 24))]);
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000BAF74 File Offset: 0x000BAF74
		internal static uint ShiftRight(uint[] x)
		{
			uint num = x[0];
			x[0] = num >> 1;
			uint num2 = num << 31;
			num = x[1];
			x[1] = (num >> 1 | num2);
			num2 = num << 31;
			num = x[2];
			x[2] = (num >> 1 | num2);
			num2 = num << 31;
			num = x[3];
			x[3] = (num >> 1 | num2);
			return num << 31;
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x000BAFC8 File Offset: 0x000BAFC8
		internal static uint ShiftRight(uint[] x, uint[] z)
		{
			uint num = x[0];
			z[0] = num >> 1;
			uint num2 = num << 31;
			num = x[1];
			z[1] = (num >> 1 | num2);
			num2 = num << 31;
			num = x[2];
			z[2] = (num >> 1 | num2);
			num2 = num << 31;
			num = x[3];
			z[3] = (num >> 1 | num2);
			return num << 31;
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x000BB01C File Offset: 0x000BB01C
		internal static uint ShiftRightN(uint[] x, int n)
		{
			uint num = x[0];
			int num2 = 32 - n;
			x[0] = num >> n;
			uint num3 = num << num2;
			num = x[1];
			x[1] = (num >> n | num3);
			num3 = num << num2;
			num = x[2];
			x[2] = (num >> n | num3);
			num3 = num << num2;
			num = x[3];
			x[3] = (num >> n | num3);
			return num << num2;
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000BB088 File Offset: 0x000BB088
		internal static uint ShiftRightN(uint[] x, int n, uint[] z)
		{
			uint num = x[0];
			int num2 = 32 - n;
			z[0] = num >> n;
			uint num3 = num << num2;
			num = x[1];
			z[1] = (num >> n | num3);
			num3 = num << num2;
			num = x[2];
			z[2] = (num >> n | num3);
			num3 = num << num2;
			num = x[3];
			z[3] = (num >> n | num3);
			return num << num2;
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000BB0F4 File Offset: 0x000BB0F4
		internal static void Xor(byte[] x, byte[] y)
		{
			int num = 0;
			do
			{
				IntPtr intPtr;
				x[(int)(intPtr = (IntPtr)num)] = (x[(int)intPtr] ^ y[num]);
				num++;
				x[(int)(intPtr = (IntPtr)num)] = (x[(int)intPtr] ^ y[num]);
				num++;
				x[(int)(intPtr = (IntPtr)num)] = (x[(int)intPtr] ^ y[num]);
				num++;
				x[(int)(intPtr = (IntPtr)num)] = (x[(int)intPtr] ^ y[num]);
				num++;
			}
			while (num < 16);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000BB158 File Offset: 0x000BB158
		internal static void Xor(byte[] x, byte[] y, int yOff)
		{
			int num = 0;
			do
			{
				IntPtr intPtr;
				x[(int)(intPtr = (IntPtr)num)] = (x[(int)intPtr] ^ y[yOff + num]);
				num++;
				x[(int)(intPtr = (IntPtr)num)] = (x[(int)intPtr] ^ y[yOff + num]);
				num++;
				x[(int)(intPtr = (IntPtr)num)] = (x[(int)intPtr] ^ y[yOff + num]);
				num++;
				x[(int)(intPtr = (IntPtr)num)] = (x[(int)intPtr] ^ y[yOff + num]);
				num++;
			}
			while (num < 16);
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000BB1C4 File Offset: 0x000BB1C4
		internal static void Xor(byte[] x, int xOff, byte[] y, int yOff, byte[] z, int zOff)
		{
			int num = 0;
			do
			{
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
			}
			while (num < 16);
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000BB238 File Offset: 0x000BB238
		internal static void Xor(byte[] x, byte[] y, int yOff, int yLen)
		{
			while (--yLen >= 0)
			{
				IntPtr intPtr;
				x[(int)(intPtr = (IntPtr)yLen)] = (x[(int)intPtr] ^ y[yOff + yLen]);
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x000BB268 File Offset: 0x000BB268
		internal static void Xor(byte[] x, int xOff, byte[] y, int yOff, int len)
		{
			while (--len >= 0)
			{
				IntPtr intPtr;
				x[(int)(intPtr = (IntPtr)(xOff + len))] = (x[(int)intPtr] ^ y[yOff + len]);
			}
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x000BB2A0 File Offset: 0x000BB2A0
		internal static void Xor(byte[] x, byte[] y, byte[] z)
		{
			int num = 0;
			do
			{
				z[num] = (x[num] ^ y[num]);
				num++;
				z[num] = (x[num] ^ y[num]);
				num++;
				z[num] = (x[num] ^ y[num]);
				num++;
				z[num] = (x[num] ^ y[num]);
				num++;
			}
			while (num < 16);
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x000BB2F4 File Offset: 0x000BB2F4
		internal static void Xor(uint[] x, uint[] y)
		{
			x[0] = (x[0] ^ y[0]);
			x[1] = (x[1] ^ y[1]);
			x[2] = (x[2] ^ y[2]);
			x[3] = (x[3] ^ y[3]);
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000BB338 File Offset: 0x000BB338
		internal static void Xor(uint[] x, uint[] y, uint[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
			z[2] = (x[2] ^ y[2]);
			z[3] = (x[3] ^ y[3]);
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x000BB364 File Offset: 0x000BB364
		internal static void Xor(ulong[] x, ulong[] y)
		{
			x[0] = (x[0] ^ y[0]);
			x[1] = (x[1] ^ y[1]);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000BB390 File Offset: 0x000BB390
		internal static void Xor(ulong[] x, ulong[] y, ulong[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
		}

		// Token: 0x0400150A RID: 5386
		private const uint E1 = 3774873600U;

		// Token: 0x0400150B RID: 5387
		private const ulong E1L = 16212958658533785600UL;

		// Token: 0x0400150C RID: 5388
		private static readonly uint[] LOOKUP = GcmUtilities.GenerateLookup();
	}
}
