﻿using System;

namespace Org.BouncyCastle.Math.EC.Rfc7748
{
	// Token: 0x02000606 RID: 1542
	public abstract class X25519Field
	{
		// Token: 0x0600337A RID: 13178 RVA: 0x00109FE0 File Offset: 0x00109FE0
		public static void Add(int[] x, int[] y, int[] z)
		{
			for (int i = 0; i < 10; i++)
			{
				z[i] = x[i] + y[i];
			}
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x0010A00C File Offset: 0x0010A00C
		public static void AddOne(int[] z)
		{
			z[0] = z[0] + 1;
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x0010A028 File Offset: 0x0010A028
		public static void AddOne(int[] z, int zOff)
		{
			z[zOff]++;
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x0010A048 File Offset: 0x0010A048
		public static void Apm(int[] x, int[] y, int[] zp, int[] zm)
		{
			for (int i = 0; i < 10; i++)
			{
				int num = x[i];
				int num2 = y[i];
				zp[i] = num + num2;
				zm[i] = num - num2;
			}
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x0010A080 File Offset: 0x0010A080
		public static void Carry(int[] z)
		{
			int num = z[0];
			int num2 = z[1];
			int num3 = z[2];
			int num4 = z[3];
			int num5 = z[4];
			int num6 = z[5];
			int num7 = z[6];
			int num8 = z[7];
			int num9 = z[8];
			int num10 = z[9];
			num3 += num2 >> 26;
			num2 &= 67108863;
			num5 += num4 >> 26;
			num4 &= 67108863;
			num8 += num7 >> 26;
			num7 &= 67108863;
			num10 += num9 >> 26;
			num9 &= 67108863;
			num4 += num3 >> 25;
			num3 &= 33554431;
			num6 += num5 >> 25;
			num5 &= 33554431;
			num9 += num8 >> 25;
			num8 &= 33554431;
			num += (num10 >> 25) * 38;
			num10 &= 33554431;
			num2 += num >> 26;
			num &= 67108863;
			num7 += num6 >> 26;
			num6 &= 67108863;
			num3 += num2 >> 26;
			num2 &= 67108863;
			num5 += num4 >> 26;
			num4 &= 67108863;
			num8 += num7 >> 26;
			num7 &= 67108863;
			num10 += num9 >> 26;
			num9 &= 67108863;
			z[0] = num;
			z[1] = num2;
			z[2] = num3;
			z[3] = num4;
			z[4] = num5;
			z[5] = num6;
			z[6] = num7;
			z[7] = num8;
			z[8] = num9;
			z[9] = num10;
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x0010A1F0 File Offset: 0x0010A1F0
		public static void CMov(int cond, int[] x, int xOff, int[] z, int zOff)
		{
			for (int i = 0; i < 10; i++)
			{
				int num = z[zOff + i];
				int num2 = num ^ x[xOff + i];
				num ^= (num2 & cond);
				z[zOff + i] = num;
			}
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x0010A230 File Offset: 0x0010A230
		public static void CNegate(int negate, int[] z)
		{
			int num = -negate;
			for (int i = 0; i < 10; i++)
			{
				z[i] = (z[i] ^ num) - num;
			}
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x0010A260 File Offset: 0x0010A260
		public static void Copy(int[] x, int xOff, int[] z, int zOff)
		{
			for (int i = 0; i < 10; i++)
			{
				z[zOff + i] = x[xOff + i];
			}
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x0010A28C File Offset: 0x0010A28C
		public static int[] Create()
		{
			return new int[10];
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x0010A298 File Offset: 0x0010A298
		public static int[] CreateTable(int n)
		{
			return new int[10 * n];
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x0010A2A4 File Offset: 0x0010A2A4
		public static void CSwap(int swap, int[] a, int[] b)
		{
			int num = -swap;
			for (int i = 0; i < 10; i++)
			{
				int num2 = a[i];
				int num3 = b[i];
				int num4 = num & (num2 ^ num3);
				a[i] = (num2 ^ num4);
				b[i] = (num3 ^ num4);
			}
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x0010A2E8 File Offset: 0x0010A2E8
		public static void Decode(byte[] x, int xOff, int[] z)
		{
			X25519Field.Decode128(x, xOff, z, 0);
			X25519Field.Decode128(x, xOff + 16, z, 5);
			z[9] = (z[9] & 16777215);
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x0010A320 File Offset: 0x0010A320
		private static void Decode128(byte[] bs, int off, int[] z, int zOff)
		{
			uint num = X25519Field.Decode32(bs, off);
			uint num2 = X25519Field.Decode32(bs, off + 4);
			uint num3 = X25519Field.Decode32(bs, off + 8);
			uint num4 = X25519Field.Decode32(bs, off + 12);
			z[zOff] = (int)(num & 67108863U);
			z[zOff + 1] = (int)((num2 << 6 | num >> 26) & 67108863U);
			z[zOff + 2] = (int)((num3 << 12 | num2 >> 20) & 33554431U);
			z[zOff + 3] = (int)((num4 << 19 | num3 >> 13) & 67108863U);
			z[zOff + 4] = (int)(num4 >> 7);
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x0010A3A8 File Offset: 0x0010A3A8
		private static uint Decode32(byte[] bs, int off)
		{
			uint num = (uint)bs[off];
			num |= (uint)((uint)bs[++off] << 8);
			num |= (uint)((uint)bs[++off] << 16);
			return num | (uint)((uint)bs[++off] << 24);
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x0010A3E8 File Offset: 0x0010A3E8
		public static void Encode(int[] x, byte[] z, int zOff)
		{
			X25519Field.Encode128(x, 0, z, zOff);
			X25519Field.Encode128(x, 5, z, zOff + 16);
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x0010A400 File Offset: 0x0010A400
		private static void Encode128(int[] x, int xOff, byte[] bs, int off)
		{
			uint num = (uint)x[xOff];
			uint num2 = (uint)x[xOff + 1];
			uint num3 = (uint)x[xOff + 2];
			uint num4 = (uint)x[xOff + 3];
			uint num5 = (uint)x[xOff + 4];
			uint n = num | num2 << 26;
			X25519Field.Encode32(n, bs, off);
			uint n2 = num2 >> 6 | num3 << 20;
			X25519Field.Encode32(n2, bs, off + 4);
			uint n3 = num3 >> 12 | num4 << 13;
			X25519Field.Encode32(n3, bs, off + 8);
			uint n4 = num4 >> 19 | num5 << 7;
			X25519Field.Encode32(n4, bs, off + 12);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x0010A484 File Offset: 0x0010A484
		private static void Encode32(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 24);
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x0010A4B4 File Offset: 0x0010A4B4
		public static void Inv(int[] x, int[] z)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			X25519Field.PowPm5d8(x, array, array2);
			X25519Field.Sqr(array2, 3, array2);
			X25519Field.Mul(array2, array, z);
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x0010A4EC File Offset: 0x0010A4EC
		public static int IsZero(int[] x)
		{
			int num = 0;
			for (int i = 0; i < 10; i++)
			{
				num |= x[i];
			}
			num |= num >> 16;
			num &= 65535;
			return num - 1 >> 31;
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x0010A52C File Offset: 0x0010A52C
		public static bool IsZeroVar(int[] x)
		{
			return 0 != X25519Field.IsZero(x);
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x0010A53C File Offset: 0x0010A53C
		public static void Mul(int[] x, int y, int[] z)
		{
			int num = x[0];
			int num2 = x[1];
			int num3 = x[2];
			int num4 = x[3];
			int num5 = x[4];
			int num6 = x[5];
			int num7 = x[6];
			int num8 = x[7];
			int num9 = x[8];
			int num10 = x[9];
			long num11 = (long)num3 * (long)y;
			num3 = ((int)num11 & 33554431);
			num11 >>= 25;
			long num12 = (long)num5 * (long)y;
			num5 = ((int)num12 & 33554431);
			num12 >>= 25;
			long num13 = (long)num8 * (long)y;
			num8 = ((int)num13 & 33554431);
			num13 >>= 25;
			long num14 = (long)num10 * (long)y;
			num10 = ((int)num14 & 33554431);
			num14 >>= 25;
			num14 *= 38L;
			num14 += (long)num * (long)y;
			z[0] = ((int)num14 & 67108863);
			num14 >>= 26;
			num12 += (long)num6 * (long)y;
			z[5] = ((int)num12 & 67108863);
			num12 >>= 26;
			num14 += (long)num2 * (long)y;
			z[1] = ((int)num14 & 67108863);
			num14 >>= 26;
			num11 += (long)num4 * (long)y;
			z[3] = ((int)num11 & 67108863);
			num11 >>= 26;
			num12 += (long)num7 * (long)y;
			z[6] = ((int)num12 & 67108863);
			num12 >>= 26;
			num13 += (long)num9 * (long)y;
			z[8] = ((int)num13 & 67108863);
			num13 >>= 26;
			z[2] = num3 + (int)num14;
			z[4] = num5 + (int)num11;
			z[7] = num8 + (int)num12;
			z[9] = num10 + (int)num13;
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x0010A6C0 File Offset: 0x0010A6C0
		public static void Mul(int[] x, int[] y, int[] z)
		{
			int num = x[0];
			int num2 = y[0];
			int num3 = x[1];
			int num4 = y[1];
			int num5 = x[2];
			int num6 = y[2];
			int num7 = x[3];
			int num8 = y[3];
			int num9 = x[4];
			int num10 = y[4];
			int num11 = x[5];
			int num12 = y[5];
			int num13 = x[6];
			int num14 = y[6];
			int num15 = x[7];
			int num16 = y[7];
			int num17 = x[8];
			int num18 = y[8];
			int num19 = x[9];
			int num20 = y[9];
			long num21 = (long)num * (long)num2;
			long num22 = (long)num * (long)num4 + (long)num3 * (long)num2;
			long num23 = (long)num * (long)num6 + (long)num3 * (long)num4 + (long)num5 * (long)num2;
			long num24 = (long)num3 * (long)num6 + (long)num5 * (long)num4;
			num24 <<= 1;
			num24 += (long)num * (long)num8 + (long)num7 * (long)num2;
			long num25 = (long)num5 * (long)num6;
			num25 <<= 1;
			num25 += (long)num * (long)num10 + (long)num3 * (long)num8 + (long)num7 * (long)num4 + (long)num9 * (long)num2;
			long num26 = (long)num3 * (long)num10 + (long)num5 * (long)num8 + (long)num7 * (long)num6 + (long)num9 * (long)num4;
			num26 <<= 1;
			long num27 = (long)num5 * (long)num10 + (long)num9 * (long)num6;
			num27 <<= 1;
			num27 += (long)num7 * (long)num8;
			long num28 = (long)num7 * (long)num10 + (long)num9 * (long)num8;
			long num29 = (long)num9 * (long)num10;
			num29 <<= 1;
			long num30 = (long)num11 * (long)num12;
			long num31 = (long)num11 * (long)num14 + (long)num13 * (long)num12;
			long num32 = (long)num11 * (long)num16 + (long)num13 * (long)num14 + (long)num15 * (long)num12;
			long num33 = (long)num13 * (long)num16 + (long)num15 * (long)num14;
			num33 <<= 1;
			num33 += (long)num11 * (long)num18 + (long)num17 * (long)num12;
			long num34 = (long)num15 * (long)num16;
			num34 <<= 1;
			num34 += (long)num11 * (long)num20 + (long)num13 * (long)num18 + (long)num17 * (long)num14 + (long)num19 * (long)num12;
			long num35 = (long)num13 * (long)num20 + (long)num15 * (long)num18 + (long)num17 * (long)num16 + (long)num19 * (long)num14;
			long num36 = (long)num15 * (long)num20 + (long)num19 * (long)num16;
			num36 <<= 1;
			num36 += (long)num17 * (long)num18;
			long num37 = (long)num17 * (long)num20 + (long)num19 * (long)num18;
			long num38 = (long)num19 * (long)num20;
			num21 -= num35 * 76L;
			num22 -= num36 * 38L;
			num23 -= num37 * 38L;
			num24 -= num38 * 76L;
			num26 -= num30;
			num27 -= num31;
			num28 -= num32;
			num29 -= num33;
			num += num11;
			num2 += num12;
			num3 += num13;
			num4 += num14;
			num5 += num15;
			num6 += num16;
			num7 += num17;
			num8 += num18;
			num9 += num19;
			num10 += num20;
			long num39 = (long)num * (long)num2;
			long num40 = (long)num * (long)num4 + (long)num3 * (long)num2;
			long num41 = (long)num * (long)num6 + (long)num3 * (long)num4 + (long)num5 * (long)num2;
			long num42 = (long)num3 * (long)num6 + (long)num5 * (long)num4;
			num42 <<= 1;
			num42 += (long)num * (long)num8 + (long)num7 * (long)num2;
			long num43 = (long)num5 * (long)num6;
			num43 <<= 1;
			num43 += (long)num * (long)num10 + (long)num3 * (long)num8 + (long)num7 * (long)num4 + (long)num9 * (long)num2;
			long num44 = (long)num3 * (long)num10 + (long)num5 * (long)num8 + (long)num7 * (long)num6 + (long)num9 * (long)num4;
			num44 <<= 1;
			long num45 = (long)num5 * (long)num10 + (long)num9 * (long)num6;
			num45 <<= 1;
			num45 += (long)num7 * (long)num8;
			long num46 = (long)num7 * (long)num10 + (long)num9 * (long)num8;
			long num47 = (long)num9 * (long)num10;
			num47 <<= 1;
			long num48 = num29 + (num42 - num24);
			int num49 = (int)num48 & 67108863;
			num48 >>= 26;
			num48 += num43 - num25 - num34;
			int num50 = (int)num48 & 33554431;
			num48 >>= 25;
			num48 = num21 + (num48 + num44 - num26) * 38L;
			z[0] = ((int)num48 & 67108863);
			num48 >>= 26;
			num48 += num22 + (num45 - num27) * 38L;
			z[1] = ((int)num48 & 67108863);
			num48 >>= 26;
			num48 += num23 + (num46 - num28) * 38L;
			z[2] = ((int)num48 & 33554431);
			num48 >>= 25;
			num48 += num24 + (num47 - num29) * 38L;
			z[3] = ((int)num48 & 67108863);
			num48 >>= 26;
			num48 += num25 + num34 * 38L;
			z[4] = ((int)num48 & 33554431);
			num48 >>= 25;
			num48 += num26 + (num39 - num21);
			z[5] = ((int)num48 & 67108863);
			num48 >>= 26;
			num48 += num27 + (num40 - num22);
			z[6] = ((int)num48 & 67108863);
			num48 >>= 26;
			num48 += num28 + (num41 - num23);
			z[7] = ((int)num48 & 33554431);
			num48 >>= 25;
			num48 += (long)num49;
			z[8] = ((int)num48 & 67108863);
			num48 >>= 26;
			z[9] = num50 + (int)num48;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x0010ABE8 File Offset: 0x0010ABE8
		public static void Negate(int[] x, int[] z)
		{
			for (int i = 0; i < 10; i++)
			{
				z[i] = -x[i];
			}
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x0010AC10 File Offset: 0x0010AC10
		public static void Normalize(int[] z)
		{
			int num = z[9] >> 23 & 1;
			X25519Field.Reduce(z, num);
			X25519Field.Reduce(z, -num);
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x0010AC3C File Offset: 0x0010AC3C
		public static void One(int[] z)
		{
			z[0] = 1;
			for (int i = 1; i < 10; i++)
			{
				z[i] = 0;
			}
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x0010AC68 File Offset: 0x0010AC68
		private static void PowPm5d8(int[] x, int[] rx2, int[] rz)
		{
			X25519Field.Sqr(x, rx2);
			X25519Field.Mul(x, rx2, rx2);
			int[] array = X25519Field.Create();
			X25519Field.Sqr(rx2, array);
			X25519Field.Mul(x, array, array);
			int[] array2 = array;
			X25519Field.Sqr(array, 2, array2);
			X25519Field.Mul(rx2, array2, array2);
			int[] array3 = X25519Field.Create();
			X25519Field.Sqr(array2, 5, array3);
			X25519Field.Mul(array2, array3, array3);
			int[] array4 = X25519Field.Create();
			X25519Field.Sqr(array3, 5, array4);
			X25519Field.Mul(array2, array4, array4);
			int[] array5 = array2;
			X25519Field.Sqr(array4, 10, array5);
			X25519Field.Mul(array3, array5, array5);
			int[] array6 = array3;
			X25519Field.Sqr(array5, 25, array6);
			X25519Field.Mul(array5, array6, array6);
			int[] array7 = array4;
			X25519Field.Sqr(array6, 25, array7);
			X25519Field.Mul(array5, array7, array7);
			int[] array8 = array5;
			X25519Field.Sqr(array7, 50, array8);
			X25519Field.Mul(array6, array8, array8);
			int[] array9 = array6;
			X25519Field.Sqr(array8, 125, array9);
			X25519Field.Mul(array8, array9, array9);
			int[] array10 = array8;
			X25519Field.Sqr(array9, 2, array10);
			X25519Field.Mul(array10, x, rz);
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x0010AD78 File Offset: 0x0010AD78
		private static void Reduce(int[] z, int x)
		{
			int num = z[9];
			int num2 = num & 16777215;
			num = (num >> 24) + x;
			long num3 = (long)(num * 19);
			num3 += (long)z[0];
			z[0] = ((int)num3 & 67108863);
			num3 >>= 26;
			num3 += (long)z[1];
			z[1] = ((int)num3 & 67108863);
			num3 >>= 26;
			num3 += (long)z[2];
			z[2] = ((int)num3 & 33554431);
			num3 >>= 25;
			num3 += (long)z[3];
			z[3] = ((int)num3 & 67108863);
			num3 >>= 26;
			num3 += (long)z[4];
			z[4] = ((int)num3 & 33554431);
			num3 >>= 25;
			num3 += (long)z[5];
			z[5] = ((int)num3 & 67108863);
			num3 >>= 26;
			num3 += (long)z[6];
			z[6] = ((int)num3 & 67108863);
			num3 >>= 26;
			num3 += (long)z[7];
			z[7] = ((int)num3 & 33554431);
			num3 >>= 25;
			num3 += (long)z[8];
			z[8] = ((int)num3 & 67108863);
			num3 >>= 26;
			z[9] = num2 + (int)num3;
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x0010AE7C File Offset: 0x0010AE7C
		public static void Sqr(int[] x, int[] z)
		{
			int num = x[0];
			int num2 = x[1];
			int num3 = x[2];
			int num4 = x[3];
			int num5 = x[4];
			int num6 = x[5];
			int num7 = x[6];
			int num8 = x[7];
			int num9 = x[8];
			int num10 = x[9];
			int num11 = num2 * 2;
			int num12 = num3 * 2;
			int num13 = num4 * 2;
			int num14 = num5 * 2;
			long num15 = (long)num * (long)num;
			long num16 = (long)num * (long)num11;
			long num17 = (long)num * (long)num12 + (long)num2 * (long)num2;
			long num18 = (long)num11 * (long)num12 + (long)num * (long)num13;
			long num19 = (long)num3 * (long)num12 + (long)num * (long)num14 + (long)num2 * (long)num13;
			long num20 = (long)num11 * (long)num14 + (long)num12 * (long)num13;
			long num21 = (long)num12 * (long)num14 + (long)num4 * (long)num4;
			long num22 = (long)num4 * (long)num14;
			long num23 = (long)num5 * (long)num14;
			int num24 = num7 * 2;
			int num25 = num8 * 2;
			int num26 = num9 * 2;
			int num27 = num10 * 2;
			long num28 = (long)num6 * (long)num6;
			long num29 = (long)num6 * (long)num24;
			long num30 = (long)num6 * (long)num25 + (long)num7 * (long)num7;
			long num31 = (long)num24 * (long)num25 + (long)num6 * (long)num26;
			long num32 = (long)num8 * (long)num25 + (long)num6 * (long)num27 + (long)num7 * (long)num26;
			long num33 = (long)num24 * (long)num27 + (long)num25 * (long)num26;
			long num34 = (long)num25 * (long)num27 + (long)num9 * (long)num9;
			long num35 = (long)num9 * (long)num27;
			long num36 = (long)num10 * (long)num27;
			num15 -= num33 * 38L;
			num16 -= num34 * 38L;
			num17 -= num35 * 38L;
			num18 -= num36 * 38L;
			num20 -= num28;
			num21 -= num29;
			num22 -= num30;
			num23 -= num31;
			num += num6;
			num2 += num7;
			num3 += num8;
			num4 += num9;
			num5 += num10;
			num11 = num2 * 2;
			num12 = num3 * 2;
			num13 = num4 * 2;
			num14 = num5 * 2;
			long num37 = (long)num * (long)num;
			long num38 = (long)num * (long)num11;
			long num39 = (long)num * (long)num12 + (long)num2 * (long)num2;
			long num40 = (long)num11 * (long)num12 + (long)num * (long)num13;
			long num41 = (long)num3 * (long)num12 + (long)num * (long)num14 + (long)num2 * (long)num13;
			long num42 = (long)num11 * (long)num14 + (long)num12 * (long)num13;
			long num43 = (long)num12 * (long)num14 + (long)num4 * (long)num4;
			long num44 = (long)num4 * (long)num14;
			long num45 = (long)num5 * (long)num14;
			long num46 = num23 + (num40 - num18);
			int num47 = (int)num46 & 67108863;
			num46 >>= 26;
			num46 += num41 - num19 - num32;
			int num48 = (int)num46 & 33554431;
			num46 >>= 25;
			num46 = num15 + (num46 + num42 - num20) * 38L;
			z[0] = ((int)num46 & 67108863);
			num46 >>= 26;
			num46 += num16 + (num43 - num21) * 38L;
			z[1] = ((int)num46 & 67108863);
			num46 >>= 26;
			num46 += num17 + (num44 - num22) * 38L;
			z[2] = ((int)num46 & 33554431);
			num46 >>= 25;
			num46 += num18 + (num45 - num23) * 38L;
			z[3] = ((int)num46 & 67108863);
			num46 >>= 26;
			num46 += num19 + num32 * 38L;
			z[4] = ((int)num46 & 33554431);
			num46 >>= 25;
			num46 += num20 + (num37 - num15);
			z[5] = ((int)num46 & 67108863);
			num46 >>= 26;
			num46 += num21 + (num38 - num16);
			z[6] = ((int)num46 & 67108863);
			num46 >>= 26;
			num46 += num22 + (num39 - num17);
			z[7] = ((int)num46 & 33554431);
			num46 >>= 25;
			num46 += (long)num47;
			z[8] = ((int)num46 & 67108863);
			num46 >>= 26;
			z[9] = num48 + (int)num46;
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x0010B23C File Offset: 0x0010B23C
		public static void Sqr(int[] x, int n, int[] z)
		{
			X25519Field.Sqr(x, z);
			while (--n > 0)
			{
				X25519Field.Sqr(z, z);
			}
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x0010B25C File Offset: 0x0010B25C
		public static bool SqrtRatioVar(int[] u, int[] v, int[] z)
		{
			int[] array = X25519Field.Create();
			int[] array2 = X25519Field.Create();
			X25519Field.Mul(u, v, array);
			X25519Field.Sqr(v, array2);
			X25519Field.Mul(array, array2, array);
			X25519Field.Sqr(array2, array2);
			X25519Field.Mul(array2, array, array2);
			int[] array3 = X25519Field.Create();
			int[] array4 = X25519Field.Create();
			X25519Field.PowPm5d8(array2, array3, array4);
			X25519Field.Mul(array4, array, array4);
			int[] array5 = X25519Field.Create();
			X25519Field.Sqr(array4, array5);
			X25519Field.Mul(array5, v, array5);
			X25519Field.Sub(array5, u, array3);
			X25519Field.Normalize(array3);
			if (X25519Field.IsZeroVar(array3))
			{
				X25519Field.Copy(array4, 0, z, 0);
				return true;
			}
			X25519Field.Add(array5, u, array3);
			X25519Field.Normalize(array3);
			if (X25519Field.IsZeroVar(array3))
			{
				X25519Field.Mul(array4, X25519Field.RootNegOne, z);
				return true;
			}
			return false;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x0010B324 File Offset: 0x0010B324
		public static void Sub(int[] x, int[] y, int[] z)
		{
			for (int i = 0; i < 10; i++)
			{
				z[i] = x[i] - y[i];
			}
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x0010B350 File Offset: 0x0010B350
		public static void SubOne(int[] z)
		{
			z[0] = z[0] - 1;
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x0010B36C File Offset: 0x0010B36C
		public static void Zero(int[] z)
		{
			for (int i = 0; i < 10; i++)
			{
				z[i] = 0;
			}
		}

		// Token: 0x04001CA3 RID: 7331
		public const int Size = 10;

		// Token: 0x04001CA4 RID: 7332
		private const int M24 = 16777215;

		// Token: 0x04001CA5 RID: 7333
		private const int M25 = 33554431;

		// Token: 0x04001CA6 RID: 7334
		private const int M26 = 67108863;

		// Token: 0x04001CA7 RID: 7335
		private static readonly int[] RootNegOne = new int[]
		{
			34513072,
			59165138,
			4688974,
			3500415,
			6194736,
			33281959,
			54535759,
			32551604,
			163342,
			5703241
		};
	}
}
