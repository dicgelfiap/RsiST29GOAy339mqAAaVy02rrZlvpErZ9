using System;

namespace Org.BouncyCastle.Math.EC.Rfc7748
{
	// Token: 0x02000608 RID: 1544
	[CLSCompliant(false)]
	public abstract class X448Field
	{
		// Token: 0x060033A7 RID: 13223 RVA: 0x0010B710 File Offset: 0x0010B710
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			for (int i = 0; i < 16; i++)
			{
				z[i] = x[i] + y[i];
			}
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x0010B73C File Offset: 0x0010B73C
		public static void AddOne(uint[] z)
		{
			z[0] = z[0] + 1U;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x0010B758 File Offset: 0x0010B758
		public static void AddOne(uint[] z, int zOff)
		{
			z[zOff] += 1U;
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x0010B778 File Offset: 0x0010B778
		public static void Carry(uint[] z)
		{
			uint num = z[0];
			uint num2 = z[1];
			uint num3 = z[2];
			uint num4 = z[3];
			uint num5 = z[4];
			uint num6 = z[5];
			uint num7 = z[6];
			uint num8 = z[7];
			uint num9 = z[8];
			uint num10 = z[9];
			uint num11 = z[10];
			uint num12 = z[11];
			uint num13 = z[12];
			uint num14 = z[13];
			uint num15 = z[14];
			uint num16 = z[15];
			num2 += num >> 28;
			num &= 268435455U;
			num6 += num5 >> 28;
			num5 &= 268435455U;
			num10 += num9 >> 28;
			num9 &= 268435455U;
			num14 += num13 >> 28;
			num13 &= 268435455U;
			num3 += num2 >> 28;
			num2 &= 268435455U;
			num7 += num6 >> 28;
			num6 &= 268435455U;
			num11 += num10 >> 28;
			num10 &= 268435455U;
			num15 += num14 >> 28;
			num14 &= 268435455U;
			num4 += num3 >> 28;
			num3 &= 268435455U;
			num8 += num7 >> 28;
			num7 &= 268435455U;
			num12 += num11 >> 28;
			num11 &= 268435455U;
			num16 += num15 >> 28;
			num15 &= 268435455U;
			uint num17 = num16 >> 28;
			num16 &= 268435455U;
			num += num17;
			num9 += num17;
			num5 += num4 >> 28;
			num4 &= 268435455U;
			num9 += num8 >> 28;
			num8 &= 268435455U;
			num13 += num12 >> 28;
			num12 &= 268435455U;
			num2 += num >> 28;
			num &= 268435455U;
			num6 += num5 >> 28;
			num5 &= 268435455U;
			num10 += num9 >> 28;
			num9 &= 268435455U;
			num14 += num13 >> 28;
			num13 &= 268435455U;
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
			z[10] = num11;
			z[11] = num12;
			z[12] = num13;
			z[13] = num14;
			z[14] = num15;
			z[15] = num16;
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x0010B9B4 File Offset: 0x0010B9B4
		public static void CMov(int cond, uint[] x, int xOff, uint[] z, int zOff)
		{
			for (int i = 0; i < 16; i++)
			{
				uint num = z[zOff + i];
				uint num2 = num ^ x[xOff + i];
				num ^= (num2 & (uint)cond);
				z[zOff + i] = num;
			}
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x0010B9F4 File Offset: 0x0010B9F4
		public static void CNegate(int negate, uint[] z)
		{
			uint[] array = X448Field.Create();
			X448Field.Sub(array, z, array);
			X448Field.CMov(-negate, array, 0, z, 0);
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x0010BA20 File Offset: 0x0010BA20
		public static void Copy(uint[] x, int xOff, uint[] z, int zOff)
		{
			for (int i = 0; i < 16; i++)
			{
				z[zOff + i] = x[xOff + i];
			}
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x0010BA4C File Offset: 0x0010BA4C
		public static uint[] Create()
		{
			return new uint[16];
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x0010BA58 File Offset: 0x0010BA58
		public static void CSwap(int swap, uint[] a, uint[] b)
		{
			uint num = (uint)(-(uint)swap);
			for (int i = 0; i < 16; i++)
			{
				uint num2 = a[i];
				uint num3 = b[i];
				uint num4 = num & (num2 ^ num3);
				a[i] = (num2 ^ num4);
				b[i] = (num3 ^ num4);
			}
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x0010BA9C File Offset: 0x0010BA9C
		public static void Decode(byte[] x, int xOff, uint[] z)
		{
			X448Field.Decode56(x, xOff, z, 0);
			X448Field.Decode56(x, xOff + 7, z, 2);
			X448Field.Decode56(x, xOff + 14, z, 4);
			X448Field.Decode56(x, xOff + 21, z, 6);
			X448Field.Decode56(x, xOff + 28, z, 8);
			X448Field.Decode56(x, xOff + 35, z, 10);
			X448Field.Decode56(x, xOff + 42, z, 12);
			X448Field.Decode56(x, xOff + 49, z, 14);
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x0010BB0C File Offset: 0x0010BB0C
		private static uint Decode24(byte[] bs, int off)
		{
			uint num = (uint)bs[off];
			num |= (uint)((uint)bs[++off] << 8);
			return num | (uint)((uint)bs[++off] << 16);
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x0010BB40 File Offset: 0x0010BB40
		private static uint Decode32(byte[] bs, int off)
		{
			uint num = (uint)bs[off];
			num |= (uint)((uint)bs[++off] << 8);
			num |= (uint)((uint)bs[++off] << 16);
			return num | (uint)((uint)bs[++off] << 24);
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x0010BB80 File Offset: 0x0010BB80
		private static void Decode56(byte[] bs, int off, uint[] z, int zOff)
		{
			uint num = X448Field.Decode32(bs, off);
			uint num2 = X448Field.Decode24(bs, off + 4);
			z[zOff] = (num & 268435455U);
			z[zOff + 1] = (num >> 28 | num2 << 4);
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x0010BBBC File Offset: 0x0010BBBC
		public static void Encode(uint[] x, byte[] z, int zOff)
		{
			X448Field.Encode56(x, 0, z, zOff);
			X448Field.Encode56(x, 2, z, zOff + 7);
			X448Field.Encode56(x, 4, z, zOff + 14);
			X448Field.Encode56(x, 6, z, zOff + 21);
			X448Field.Encode56(x, 8, z, zOff + 28);
			X448Field.Encode56(x, 10, z, zOff + 35);
			X448Field.Encode56(x, 12, z, zOff + 42);
			X448Field.Encode56(x, 14, z, zOff + 49);
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x0010BC2C File Offset: 0x0010BC2C
		private static void Encode24(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x0010BC4C File Offset: 0x0010BC4C
		private static void Encode32(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 24);
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x0010BC7C File Offset: 0x0010BC7C
		private static void Encode56(uint[] x, int xOff, byte[] bs, int off)
		{
			uint num = x[xOff];
			uint num2 = x[xOff + 1];
			X448Field.Encode32(num | num2 << 28, bs, off);
			X448Field.Encode24(num2 >> 4, bs, off + 4);
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x0010BCB0 File Offset: 0x0010BCB0
		public static void Inv(uint[] x, uint[] z)
		{
			uint[] array = X448Field.Create();
			X448Field.PowPm3d4(x, array);
			X448Field.Sqr(array, 2, array);
			X448Field.Mul(array, x, z);
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x0010BCE0 File Offset: 0x0010BCE0
		public static int IsZero(uint[] x)
		{
			uint num = 0U;
			for (int i = 0; i < 16; i++)
			{
				num |= x[i];
			}
			num |= num >> 16;
			num &= 65535U;
			return (int)(num - 1U) >> 31;
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x0010BD20 File Offset: 0x0010BD20
		public static bool IsZeroVar(uint[] x)
		{
			return 0L != (long)X448Field.IsZero(x);
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x0010BD30 File Offset: 0x0010BD30
		public static void Mul(uint[] x, uint y, uint[] z)
		{
			uint num = x[0];
			uint num2 = x[1];
			uint num3 = x[2];
			uint num4 = x[3];
			uint num5 = x[4];
			uint num6 = x[5];
			uint num7 = x[6];
			uint num8 = x[7];
			uint num9 = x[8];
			uint num10 = x[9];
			uint num11 = x[10];
			uint num12 = x[11];
			uint num13 = x[12];
			uint num14 = x[13];
			uint num15 = x[14];
			uint num16 = x[15];
			ulong num17 = (ulong)num2 * (ulong)y;
			uint num18 = (uint)num17 & 268435455U;
			num17 >>= 28;
			ulong num19 = (ulong)num6 * (ulong)y;
			uint num20 = (uint)num19 & 268435455U;
			num19 >>= 28;
			ulong num21 = (ulong)num10 * (ulong)y;
			uint num22 = (uint)num21 & 268435455U;
			num21 >>= 28;
			ulong num23 = (ulong)num14 * (ulong)y;
			uint num24 = (uint)num23 & 268435455U;
			num23 >>= 28;
			num17 += (ulong)num3 * (ulong)y;
			z[2] = ((uint)num17 & 268435455U);
			num17 >>= 28;
			num19 += (ulong)num7 * (ulong)y;
			z[6] = ((uint)num19 & 268435455U);
			num19 >>= 28;
			num21 += (ulong)num11 * (ulong)y;
			z[10] = ((uint)num21 & 268435455U);
			num21 >>= 28;
			num23 += (ulong)num15 * (ulong)y;
			z[14] = ((uint)num23 & 268435455U);
			num23 >>= 28;
			num17 += (ulong)num4 * (ulong)y;
			z[3] = ((uint)num17 & 268435455U);
			num17 >>= 28;
			num19 += (ulong)num8 * (ulong)y;
			z[7] = ((uint)num19 & 268435455U);
			num19 >>= 28;
			num21 += (ulong)num12 * (ulong)y;
			z[11] = ((uint)num21 & 268435455U);
			num21 >>= 28;
			num23 += (ulong)num16 * (ulong)y;
			z[15] = ((uint)num23 & 268435455U);
			num23 >>= 28;
			num19 += num23;
			num17 += (ulong)num5 * (ulong)y;
			z[4] = ((uint)num17 & 268435455U);
			num17 >>= 28;
			num19 += (ulong)num9 * (ulong)y;
			z[8] = ((uint)num19 & 268435455U);
			num19 >>= 28;
			num21 += (ulong)num13 * (ulong)y;
			z[12] = ((uint)num21 & 268435455U);
			num21 >>= 28;
			num23 += (ulong)num * (ulong)y;
			z[0] = ((uint)num23 & 268435455U);
			num23 >>= 28;
			z[1] = num18 + (uint)num23;
			z[5] = num20 + (uint)num17;
			z[9] = num22 + (uint)num19;
			z[13] = num24 + (uint)num21;
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x0010BF94 File Offset: 0x0010BF94
		public static void Mul(uint[] x, uint[] y, uint[] z)
		{
			uint num = x[0];
			uint num2 = x[1];
			uint num3 = x[2];
			uint num4 = x[3];
			uint num5 = x[4];
			uint num6 = x[5];
			uint num7 = x[6];
			uint num8 = x[7];
			uint num9 = x[8];
			uint num10 = x[9];
			uint num11 = x[10];
			uint num12 = x[11];
			uint num13 = x[12];
			uint num14 = x[13];
			uint num15 = x[14];
			uint num16 = x[15];
			uint num17 = y[0];
			uint num18 = y[1];
			uint num19 = y[2];
			uint num20 = y[3];
			uint num21 = y[4];
			uint num22 = y[5];
			uint num23 = y[6];
			uint num24 = y[7];
			uint num25 = y[8];
			uint num26 = y[9];
			uint num27 = y[10];
			uint num28 = y[11];
			uint num29 = y[12];
			uint num30 = y[13];
			uint num31 = y[14];
			uint num32 = y[15];
			uint num33 = num + num9;
			uint num34 = num2 + num10;
			uint num35 = num3 + num11;
			uint num36 = num4 + num12;
			uint num37 = num5 + num13;
			uint num38 = num6 + num14;
			uint num39 = num7 + num15;
			uint num40 = num8 + num16;
			uint num41 = num17 + num25;
			uint num42 = num18 + num26;
			uint num43 = num19 + num27;
			uint num44 = num20 + num28;
			uint num45 = num21 + num29;
			uint num46 = num22 + num30;
			uint num47 = num23 + num31;
			uint num48 = num24 + num32;
			ulong num49 = (ulong)num * (ulong)num17;
			ulong num50 = (ulong)num8 * (ulong)num18 + (ulong)num7 * (ulong)num19 + (ulong)num6 * (ulong)num20 + (ulong)num5 * (ulong)num21 + (ulong)num4 * (ulong)num22 + (ulong)num3 * (ulong)num23 + (ulong)num2 * (ulong)num24;
			ulong num51 = (ulong)num9 * (ulong)num25;
			ulong num52 = (ulong)num16 * (ulong)num26 + (ulong)num15 * (ulong)num27 + (ulong)num14 * (ulong)num28 + (ulong)num13 * (ulong)num29 + (ulong)num12 * (ulong)num30 + (ulong)num11 * (ulong)num31 + (ulong)num10 * (ulong)num32;
			ulong num53 = (ulong)num33 * (ulong)num41;
			ulong num54 = (ulong)num40 * (ulong)num42 + (ulong)num39 * (ulong)num43 + (ulong)num38 * (ulong)num44 + (ulong)num37 * (ulong)num45 + (ulong)num36 * (ulong)num46 + (ulong)num35 * (ulong)num47 + (ulong)num34 * (ulong)num48;
			ulong num55 = num49 + num51 + num54 - num50;
			uint num56 = (uint)num55 & 268435455U;
			num55 >>= 28;
			ulong num57 = num52 + num53 - num49 + num54;
			uint num58 = (uint)num57 & 268435455U;
			num57 >>= 28;
			ulong num59 = (ulong)num2 * (ulong)num17 + (ulong)num * (ulong)num18;
			ulong num60 = (ulong)num8 * (ulong)num19 + (ulong)num7 * (ulong)num20 + (ulong)num6 * (ulong)num21 + (ulong)num5 * (ulong)num22 + (ulong)num4 * (ulong)num23 + (ulong)num3 * (ulong)num24;
			ulong num61 = (ulong)num10 * (ulong)num25 + (ulong)num9 * (ulong)num26;
			ulong num62 = (ulong)num16 * (ulong)num27 + (ulong)num15 * (ulong)num28 + (ulong)num14 * (ulong)num29 + (ulong)num13 * (ulong)num30 + (ulong)num12 * (ulong)num31 + (ulong)num11 * (ulong)num32;
			ulong num63 = (ulong)num34 * (ulong)num41 + (ulong)num33 * (ulong)num42;
			ulong num64 = (ulong)num40 * (ulong)num43 + (ulong)num39 * (ulong)num44 + (ulong)num38 * (ulong)num45 + (ulong)num37 * (ulong)num46 + (ulong)num36 * (ulong)num47 + (ulong)num35 * (ulong)num48;
			num55 += num59 + num61 + num64 - num60;
			uint num65 = (uint)num55 & 268435455U;
			num55 >>= 28;
			num57 += num62 + num63 - num59 + num64;
			uint num66 = (uint)num57 & 268435455U;
			num57 >>= 28;
			ulong num67 = (ulong)num3 * (ulong)num17 + (ulong)num2 * (ulong)num18 + (ulong)num * (ulong)num19;
			ulong num68 = (ulong)num8 * (ulong)num20 + (ulong)num7 * (ulong)num21 + (ulong)num6 * (ulong)num22 + (ulong)num5 * (ulong)num23 + (ulong)num4 * (ulong)num24;
			ulong num69 = (ulong)num11 * (ulong)num25 + (ulong)num10 * (ulong)num26 + (ulong)num9 * (ulong)num27;
			ulong num70 = (ulong)num16 * (ulong)num28 + (ulong)num15 * (ulong)num29 + (ulong)num14 * (ulong)num30 + (ulong)num13 * (ulong)num31 + (ulong)num12 * (ulong)num32;
			ulong num71 = (ulong)num35 * (ulong)num41 + (ulong)num34 * (ulong)num42 + (ulong)num33 * (ulong)num43;
			ulong num72 = (ulong)num40 * (ulong)num44 + (ulong)num39 * (ulong)num45 + (ulong)num38 * (ulong)num46 + (ulong)num37 * (ulong)num47 + (ulong)num36 * (ulong)num48;
			num55 += num67 + num69 + num72 - num68;
			uint num73 = (uint)num55 & 268435455U;
			num55 >>= 28;
			num57 += num70 + num71 - num67 + num72;
			uint num74 = (uint)num57 & 268435455U;
			num57 >>= 28;
			ulong num75 = (ulong)num4 * (ulong)num17 + (ulong)num3 * (ulong)num18 + (ulong)num2 * (ulong)num19 + (ulong)num * (ulong)num20;
			ulong num76 = (ulong)num8 * (ulong)num21 + (ulong)num7 * (ulong)num22 + (ulong)num6 * (ulong)num23 + (ulong)num5 * (ulong)num24;
			ulong num77 = (ulong)num12 * (ulong)num25 + (ulong)num11 * (ulong)num26 + (ulong)num10 * (ulong)num27 + (ulong)num9 * (ulong)num28;
			ulong num78 = (ulong)num16 * (ulong)num29 + (ulong)num15 * (ulong)num30 + (ulong)num14 * (ulong)num31 + (ulong)num13 * (ulong)num32;
			ulong num79 = (ulong)num36 * (ulong)num41 + (ulong)num35 * (ulong)num42 + (ulong)num34 * (ulong)num43 + (ulong)num33 * (ulong)num44;
			ulong num80 = (ulong)num40 * (ulong)num45 + (ulong)num39 * (ulong)num46 + (ulong)num38 * (ulong)num47 + (ulong)num37 * (ulong)num48;
			num55 += num75 + num77 + num80 - num76;
			uint num81 = (uint)num55 & 268435455U;
			num55 >>= 28;
			num57 += num78 + num79 - num75 + num80;
			uint num82 = (uint)num57 & 268435455U;
			num57 >>= 28;
			ulong num83 = (ulong)num5 * (ulong)num17 + (ulong)num4 * (ulong)num18 + (ulong)num3 * (ulong)num19 + (ulong)num2 * (ulong)num20 + (ulong)num * (ulong)num21;
			ulong num84 = (ulong)num8 * (ulong)num22 + (ulong)num7 * (ulong)num23 + (ulong)num6 * (ulong)num24;
			ulong num85 = (ulong)num13 * (ulong)num25 + (ulong)num12 * (ulong)num26 + (ulong)num11 * (ulong)num27 + (ulong)num10 * (ulong)num28 + (ulong)num9 * (ulong)num29;
			ulong num86 = (ulong)num16 * (ulong)num30 + (ulong)num15 * (ulong)num31 + (ulong)num14 * (ulong)num32;
			ulong num87 = (ulong)num37 * (ulong)num41 + (ulong)num36 * (ulong)num42 + (ulong)num35 * (ulong)num43 + (ulong)num34 * (ulong)num44 + (ulong)num33 * (ulong)num45;
			ulong num88 = (ulong)num40 * (ulong)num46 + (ulong)num39 * (ulong)num47 + (ulong)num38 * (ulong)num48;
			num55 += num83 + num85 + num88 - num84;
			uint num89 = (uint)num55 & 268435455U;
			num55 >>= 28;
			num57 += num86 + num87 - num83 + num88;
			uint num90 = (uint)num57 & 268435455U;
			num57 >>= 28;
			ulong num91 = (ulong)num6 * (ulong)num17 + (ulong)num5 * (ulong)num18 + (ulong)num4 * (ulong)num19 + (ulong)num3 * (ulong)num20 + (ulong)num2 * (ulong)num21 + (ulong)num * (ulong)num22;
			ulong num92 = (ulong)num8 * (ulong)num23 + (ulong)num7 * (ulong)num24;
			ulong num93 = (ulong)num14 * (ulong)num25 + (ulong)num13 * (ulong)num26 + (ulong)num12 * (ulong)num27 + (ulong)num11 * (ulong)num28 + (ulong)num10 * (ulong)num29 + (ulong)num9 * (ulong)num30;
			ulong num94 = (ulong)num16 * (ulong)num31 + (ulong)num15 * (ulong)num32;
			ulong num95 = (ulong)num38 * (ulong)num41 + (ulong)num37 * (ulong)num42 + (ulong)num36 * (ulong)num43 + (ulong)num35 * (ulong)num44 + (ulong)num34 * (ulong)num45 + (ulong)num33 * (ulong)num46;
			ulong num96 = (ulong)num40 * (ulong)num47 + (ulong)num39 * (ulong)num48;
			num55 += num91 + num93 + num96 - num92;
			uint num97 = (uint)num55 & 268435455U;
			num55 >>= 28;
			num57 += num94 + num95 - num91 + num96;
			uint num98 = (uint)num57 & 268435455U;
			num57 >>= 28;
			ulong num99 = (ulong)num7 * (ulong)num17 + (ulong)num6 * (ulong)num18 + (ulong)num5 * (ulong)num19 + (ulong)num4 * (ulong)num20 + (ulong)num3 * (ulong)num21 + (ulong)num2 * (ulong)num22 + (ulong)num * (ulong)num23;
			ulong num100 = (ulong)num8 * (ulong)num24;
			ulong num101 = (ulong)num15 * (ulong)num25 + (ulong)num14 * (ulong)num26 + (ulong)num13 * (ulong)num27 + (ulong)num12 * (ulong)num28 + (ulong)num11 * (ulong)num29 + (ulong)num10 * (ulong)num30 + (ulong)num9 * (ulong)num31;
			ulong num102 = (ulong)num16 * (ulong)num32;
			ulong num103 = (ulong)num39 * (ulong)num41 + (ulong)num38 * (ulong)num42 + (ulong)num37 * (ulong)num43 + (ulong)num36 * (ulong)num44 + (ulong)num35 * (ulong)num45 + (ulong)num34 * (ulong)num46 + (ulong)num33 * (ulong)num47;
			ulong num104 = (ulong)num40 * (ulong)num48;
			num55 += num99 + num101 + num104 - num100;
			uint num105 = (uint)num55 & 268435455U;
			num55 >>= 28;
			num57 += num102 + num103 - num99 + num104;
			uint num106 = (uint)num57 & 268435455U;
			num57 >>= 28;
			ulong num107 = (ulong)num8 * (ulong)num17 + (ulong)num7 * (ulong)num18 + (ulong)num6 * (ulong)num19 + (ulong)num5 * (ulong)num20 + (ulong)num4 * (ulong)num21 + (ulong)num3 * (ulong)num22 + (ulong)num2 * (ulong)num23 + (ulong)num * (ulong)num24;
			ulong num108 = (ulong)num16 * (ulong)num25 + (ulong)num15 * (ulong)num26 + (ulong)num14 * (ulong)num27 + (ulong)num13 * (ulong)num28 + (ulong)num12 * (ulong)num29 + (ulong)num11 * (ulong)num30 + (ulong)num10 * (ulong)num31 + (ulong)num9 * (ulong)num32;
			ulong num109 = (ulong)num40 * (ulong)num41 + (ulong)num39 * (ulong)num42 + (ulong)num38 * (ulong)num43 + (ulong)num37 * (ulong)num44 + (ulong)num36 * (ulong)num45 + (ulong)num35 * (ulong)num46 + (ulong)num34 * (ulong)num47 + (ulong)num33 * (ulong)num48;
			num55 += num107 + num108;
			uint num110 = (uint)num55 & 268435455U;
			num55 >>= 28;
			num57 += num109 - num107;
			uint num111 = (uint)num57 & 268435455U;
			num57 >>= 28;
			num55 += num57;
			num55 += (ulong)num58;
			num58 = ((uint)num55 & 268435455U);
			num55 >>= 28;
			num57 += (ulong)num56;
			num56 = ((uint)num57 & 268435455U);
			num57 >>= 28;
			num66 += (uint)num55;
			num65 += (uint)num57;
			z[0] = num56;
			z[1] = num65;
			z[2] = num73;
			z[3] = num81;
			z[4] = num89;
			z[5] = num97;
			z[6] = num105;
			z[7] = num110;
			z[8] = num58;
			z[9] = num66;
			z[10] = num74;
			z[11] = num82;
			z[12] = num90;
			z[13] = num98;
			z[14] = num106;
			z[15] = num111;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x0010C978 File Offset: 0x0010C978
		public static void Negate(uint[] x, uint[] z)
		{
			uint[] x2 = X448Field.Create();
			X448Field.Sub(x2, x, z);
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x0010C998 File Offset: 0x0010C998
		public static void Normalize(uint[] z)
		{
			X448Field.Reduce(z, 1);
			X448Field.Reduce(z, -1);
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x0010C9A8 File Offset: 0x0010C9A8
		public static void One(uint[] z)
		{
			z[0] = 1U;
			for (int i = 1; i < 16; i++)
			{
				z[i] = 0U;
			}
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x0010C9D4 File Offset: 0x0010C9D4
		private static void PowPm3d4(uint[] x, uint[] z)
		{
			uint[] array = X448Field.Create();
			X448Field.Sqr(x, array);
			X448Field.Mul(x, array, array);
			uint[] array2 = X448Field.Create();
			X448Field.Sqr(array, array2);
			X448Field.Mul(x, array2, array2);
			uint[] array3 = X448Field.Create();
			X448Field.Sqr(array2, 3, array3);
			X448Field.Mul(array2, array3, array3);
			uint[] array4 = X448Field.Create();
			X448Field.Sqr(array3, 3, array4);
			X448Field.Mul(array2, array4, array4);
			uint[] array5 = X448Field.Create();
			X448Field.Sqr(array4, 9, array5);
			X448Field.Mul(array4, array5, array5);
			uint[] array6 = X448Field.Create();
			X448Field.Sqr(array5, array6);
			X448Field.Mul(x, array6, array6);
			uint[] array7 = X448Field.Create();
			X448Field.Sqr(array6, 18, array7);
			X448Field.Mul(array5, array7, array7);
			uint[] array8 = X448Field.Create();
			X448Field.Sqr(array7, 37, array8);
			X448Field.Mul(array7, array8, array8);
			uint[] array9 = X448Field.Create();
			X448Field.Sqr(array8, 37, array9);
			X448Field.Mul(array7, array9, array9);
			uint[] array10 = X448Field.Create();
			X448Field.Sqr(array9, 111, array10);
			X448Field.Mul(array9, array10, array10);
			uint[] array11 = X448Field.Create();
			X448Field.Sqr(array10, array11);
			X448Field.Mul(x, array11, array11);
			uint[] array12 = X448Field.Create();
			X448Field.Sqr(array11, 223, array12);
			X448Field.Mul(array12, array10, z);
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x0010CB20 File Offset: 0x0010CB20
		private static void Reduce(uint[] z, int x)
		{
			uint num = z[15];
			uint num2 = num & 268435455U;
			int num3 = (int)((num >> 28) + (uint)x);
			long num4 = (long)num3;
			for (int i = 0; i < 8; i++)
			{
				num4 += (long)((ulong)z[i]);
				z[i] = ((uint)num4 & 268435455U);
				num4 >>= 28;
			}
			num4 += (long)num3;
			for (int j = 8; j < 15; j++)
			{
				num4 += (long)((ulong)z[j]);
				z[j] = ((uint)num4 & 268435455U);
				num4 >>= 28;
			}
			z[15] = num2 + (uint)num4;
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x0010CBB0 File Offset: 0x0010CBB0
		public static void Sqr(uint[] x, uint[] z)
		{
			uint num = x[0];
			uint num2 = x[1];
			uint num3 = x[2];
			uint num4 = x[3];
			uint num5 = x[4];
			uint num6 = x[5];
			uint num7 = x[6];
			uint num8 = x[7];
			uint num9 = x[8];
			uint num10 = x[9];
			uint num11 = x[10];
			uint num12 = x[11];
			uint num13 = x[12];
			uint num14 = x[13];
			uint num15 = x[14];
			uint num16 = x[15];
			uint num17 = num * 2U;
			uint num18 = num2 * 2U;
			uint num19 = num3 * 2U;
			uint num20 = num4 * 2U;
			uint num21 = num5 * 2U;
			uint num22 = num6 * 2U;
			uint num23 = num7 * 2U;
			uint num24 = num9 * 2U;
			uint num25 = num10 * 2U;
			uint num26 = num11 * 2U;
			uint num27 = num12 * 2U;
			uint num28 = num13 * 2U;
			uint num29 = num14 * 2U;
			uint num30 = num15 * 2U;
			uint num31 = num + num9;
			uint num32 = num2 + num10;
			uint num33 = num3 + num11;
			uint num34 = num4 + num12;
			uint num35 = num5 + num13;
			uint num36 = num6 + num14;
			uint num37 = num7 + num15;
			uint num38 = num8 + num16;
			uint num39 = num31 * 2U;
			uint num40 = num32 * 2U;
			uint num41 = num33 * 2U;
			uint num42 = num34 * 2U;
			uint num43 = num35 * 2U;
			uint num44 = num36 * 2U;
			uint num45 = num37 * 2U;
			ulong num46 = (ulong)num * (ulong)num;
			ulong num47 = (ulong)num8 * (ulong)num18 + (ulong)num7 * (ulong)num19 + (ulong)num6 * (ulong)num20 + (ulong)num5 * (ulong)num5;
			ulong num48 = (ulong)num9 * (ulong)num9;
			ulong num49 = (ulong)num16 * (ulong)num25 + (ulong)num15 * (ulong)num26 + (ulong)num14 * (ulong)num27 + (ulong)num13 * (ulong)num13;
			ulong num50 = (ulong)num31 * (ulong)num31;
			ulong num51 = (ulong)num38 * (ulong)num40 + (ulong)num37 * (ulong)num41 + (ulong)num36 * (ulong)num42 + (ulong)num35 * (ulong)num35;
			ulong num52 = num46 + num48 + num51 - num47;
			uint num53 = (uint)num52 & 268435455U;
			num52 >>= 28;
			ulong num54 = num49 + num50 - num46 + num51;
			uint num55 = (uint)num54 & 268435455U;
			num54 >>= 28;
			ulong num56 = (ulong)num2 * (ulong)num17;
			ulong num57 = (ulong)num8 * (ulong)num19 + (ulong)num7 * (ulong)num20 + (ulong)num6 * (ulong)num21;
			ulong num58 = (ulong)num10 * (ulong)num24;
			ulong num59 = (ulong)num16 * (ulong)num26 + (ulong)num15 * (ulong)num27 + (ulong)num14 * (ulong)num28;
			ulong num60 = (ulong)num32 * (ulong)num39;
			ulong num61 = (ulong)num38 * (ulong)num41 + (ulong)num37 * (ulong)num42 + (ulong)num36 * (ulong)num43;
			num52 += num56 + num58 + num61 - num57;
			uint num62 = (uint)num52 & 268435455U;
			num52 >>= 28;
			num54 += num59 + num60 - num56 + num61;
			uint num63 = (uint)num54 & 268435455U;
			num54 >>= 28;
			ulong num64 = (ulong)num3 * (ulong)num17 + (ulong)num2 * (ulong)num2;
			ulong num65 = (ulong)num8 * (ulong)num20 + (ulong)num7 * (ulong)num21 + (ulong)num6 * (ulong)num6;
			ulong num66 = (ulong)num11 * (ulong)num24 + (ulong)num10 * (ulong)num10;
			ulong num67 = (ulong)num16 * (ulong)num27 + (ulong)num15 * (ulong)num28 + (ulong)num14 * (ulong)num14;
			ulong num68 = (ulong)num33 * (ulong)num39 + (ulong)num32 * (ulong)num32;
			ulong num69 = (ulong)num38 * (ulong)num42 + (ulong)num37 * (ulong)num43 + (ulong)num36 * (ulong)num36;
			num52 += num64 + num66 + num69 - num65;
			uint num70 = (uint)num52 & 268435455U;
			num52 >>= 28;
			num54 += num67 + num68 - num64 + num69;
			uint num71 = (uint)num54 & 268435455U;
			num54 >>= 28;
			ulong num72 = (ulong)num4 * (ulong)num17 + (ulong)num3 * (ulong)num18;
			ulong num73 = (ulong)num8 * (ulong)num21 + (ulong)num7 * (ulong)num22;
			ulong num74 = (ulong)num12 * (ulong)num24 + (ulong)num11 * (ulong)num25;
			ulong num75 = (ulong)num16 * (ulong)num28 + (ulong)num15 * (ulong)num29;
			ulong num76 = (ulong)num34 * (ulong)num39 + (ulong)num33 * (ulong)num40;
			ulong num77 = (ulong)num38 * (ulong)num43 + (ulong)num37 * (ulong)num44;
			num52 += num72 + num74 + num77 - num73;
			uint num78 = (uint)num52 & 268435455U;
			num52 >>= 28;
			num54 += num75 + num76 - num72 + num77;
			uint num79 = (uint)num54 & 268435455U;
			num54 >>= 28;
			ulong num80 = (ulong)num5 * (ulong)num17 + (ulong)num4 * (ulong)num18 + (ulong)num3 * (ulong)num3;
			ulong num81 = (ulong)num8 * (ulong)num22 + (ulong)num7 * (ulong)num7;
			ulong num82 = (ulong)num13 * (ulong)num24 + (ulong)num12 * (ulong)num25 + (ulong)num11 * (ulong)num11;
			ulong num83 = (ulong)num16 * (ulong)num29 + (ulong)num15 * (ulong)num15;
			ulong num84 = (ulong)num35 * (ulong)num39 + (ulong)num34 * (ulong)num40 + (ulong)num33 * (ulong)num33;
			ulong num85 = (ulong)num38 * (ulong)num44 + (ulong)num37 * (ulong)num37;
			num52 += num80 + num82 + num85 - num81;
			uint num86 = (uint)num52 & 268435455U;
			num52 >>= 28;
			num54 += num83 + num84 - num80 + num85;
			uint num87 = (uint)num54 & 268435455U;
			num54 >>= 28;
			ulong num88 = (ulong)num6 * (ulong)num17 + (ulong)num5 * (ulong)num18 + (ulong)num4 * (ulong)num19;
			ulong num89 = (ulong)num8 * (ulong)num23;
			ulong num90 = (ulong)num14 * (ulong)num24 + (ulong)num13 * (ulong)num25 + (ulong)num12 * (ulong)num26;
			ulong num91 = (ulong)num16 * (ulong)num30;
			ulong num92 = (ulong)num36 * (ulong)num39 + (ulong)num35 * (ulong)num40 + (ulong)num34 * (ulong)num41;
			ulong num93 = (ulong)num38 * (ulong)num45;
			num52 += num88 + num90 + num93 - num89;
			uint num94 = (uint)num52 & 268435455U;
			num52 >>= 28;
			num54 += num91 + num92 - num88 + num93;
			uint num95 = (uint)num54 & 268435455U;
			num54 >>= 28;
			ulong num96 = (ulong)num7 * (ulong)num17 + (ulong)num6 * (ulong)num18 + (ulong)num5 * (ulong)num19 + (ulong)num4 * (ulong)num4;
			ulong num97 = (ulong)num8 * (ulong)num8;
			ulong num98 = (ulong)num15 * (ulong)num24 + (ulong)num14 * (ulong)num25 + (ulong)num13 * (ulong)num26 + (ulong)num12 * (ulong)num12;
			ulong num99 = (ulong)num16 * (ulong)num16;
			ulong num100 = (ulong)num37 * (ulong)num39 + (ulong)num36 * (ulong)num40 + (ulong)num35 * (ulong)num41 + (ulong)num34 * (ulong)num34;
			ulong num101 = (ulong)num38 * (ulong)num38;
			num52 += num96 + num98 + num101 - num97;
			uint num102 = (uint)num52 & 268435455U;
			num52 >>= 28;
			num54 += num99 + num100 - num96 + num101;
			uint num103 = (uint)num54 & 268435455U;
			num54 >>= 28;
			ulong num104 = (ulong)num8 * (ulong)num17 + (ulong)num7 * (ulong)num18 + (ulong)num6 * (ulong)num19 + (ulong)num5 * (ulong)num20;
			ulong num105 = (ulong)num16 * (ulong)num24 + (ulong)num15 * (ulong)num25 + (ulong)num14 * (ulong)num26 + (ulong)num13 * (ulong)num27;
			ulong num106 = (ulong)num38 * (ulong)num39 + (ulong)num37 * (ulong)num40 + (ulong)num36 * (ulong)num41 + (ulong)num35 * (ulong)num42;
			num52 += num104 + num105;
			uint num107 = (uint)num52 & 268435455U;
			num52 >>= 28;
			num54 += num106 - num104;
			uint num108 = (uint)num54 & 268435455U;
			num54 >>= 28;
			num52 += num54;
			num52 += (ulong)num55;
			num55 = ((uint)num52 & 268435455U);
			num52 >>= 28;
			num54 += (ulong)num53;
			num53 = ((uint)num54 & 268435455U);
			num54 >>= 28;
			num63 += (uint)num52;
			num62 += (uint)num54;
			z[0] = num53;
			z[1] = num62;
			z[2] = num70;
			z[3] = num78;
			z[4] = num86;
			z[5] = num94;
			z[6] = num102;
			z[7] = num107;
			z[8] = num55;
			z[9] = num63;
			z[10] = num71;
			z[11] = num79;
			z[12] = num87;
			z[13] = num95;
			z[14] = num103;
			z[15] = num108;
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x0010D2F4 File Offset: 0x0010D2F4
		public static void Sqr(uint[] x, int n, uint[] z)
		{
			X448Field.Sqr(x, z);
			while (--n > 0)
			{
				X448Field.Sqr(z, z);
			}
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x0010D314 File Offset: 0x0010D314
		public static bool SqrtRatioVar(uint[] u, uint[] v, uint[] z)
		{
			uint[] array = X448Field.Create();
			uint[] array2 = X448Field.Create();
			X448Field.Sqr(u, array);
			X448Field.Mul(array, v, array);
			X448Field.Sqr(array, array2);
			X448Field.Mul(array, u, array);
			X448Field.Mul(array2, u, array2);
			X448Field.Mul(array2, v, array2);
			uint[] array3 = X448Field.Create();
			X448Field.PowPm3d4(array2, array3);
			X448Field.Mul(array3, array, array3);
			uint[] array4 = X448Field.Create();
			X448Field.Sqr(array3, array4);
			X448Field.Mul(array4, v, array4);
			X448Field.Sub(u, array4, array4);
			X448Field.Normalize(array4);
			if (X448Field.IsZeroVar(array4))
			{
				X448Field.Copy(array3, 0, z, 0);
				return true;
			}
			return false;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x0010D3B0 File Offset: 0x0010D3B0
		public static void Sub(uint[] x, uint[] y, uint[] z)
		{
			uint num = x[0];
			uint num2 = x[1];
			uint num3 = x[2];
			uint num4 = x[3];
			uint num5 = x[4];
			uint num6 = x[5];
			uint num7 = x[6];
			uint num8 = x[7];
			uint num9 = x[8];
			uint num10 = x[9];
			uint num11 = x[10];
			uint num12 = x[11];
			uint num13 = x[12];
			uint num14 = x[13];
			uint num15 = x[14];
			uint num16 = x[15];
			uint num17 = y[0];
			uint num18 = y[1];
			uint num19 = y[2];
			uint num20 = y[3];
			uint num21 = y[4];
			uint num22 = y[5];
			uint num23 = y[6];
			uint num24 = y[7];
			uint num25 = y[8];
			uint num26 = y[9];
			uint num27 = y[10];
			uint num28 = y[11];
			uint num29 = y[12];
			uint num30 = y[13];
			uint num31 = y[14];
			uint num32 = y[15];
			uint num33 = num + 536870910U - num17;
			uint num34 = num2 + 536870910U - num18;
			uint num35 = num3 + 536870910U - num19;
			uint num36 = num4 + 536870910U - num20;
			uint num37 = num5 + 536870910U - num21;
			uint num38 = num6 + 536870910U - num22;
			uint num39 = num7 + 536870910U - num23;
			uint num40 = num8 + 536870910U - num24;
			uint num41 = num9 + 536870908U - num25;
			uint num42 = num10 + 536870910U - num26;
			uint num43 = num11 + 536870910U - num27;
			uint num44 = num12 + 536870910U - num28;
			uint num45 = num13 + 536870910U - num29;
			uint num46 = num14 + 536870910U - num30;
			uint num47 = num15 + 536870910U - num31;
			uint num48 = num16 + 536870910U - num32;
			num35 += num34 >> 28;
			num34 &= 268435455U;
			num39 += num38 >> 28;
			num38 &= 268435455U;
			num43 += num42 >> 28;
			num42 &= 268435455U;
			num47 += num46 >> 28;
			num46 &= 268435455U;
			num36 += num35 >> 28;
			num35 &= 268435455U;
			num40 += num39 >> 28;
			num39 &= 268435455U;
			num44 += num43 >> 28;
			num43 &= 268435455U;
			num48 += num47 >> 28;
			num47 &= 268435455U;
			uint num49 = num48 >> 28;
			num48 &= 268435455U;
			num33 += num49;
			num41 += num49;
			num37 += num36 >> 28;
			num36 &= 268435455U;
			num41 += num40 >> 28;
			num40 &= 268435455U;
			num45 += num44 >> 28;
			num44 &= 268435455U;
			num34 += num33 >> 28;
			num33 &= 268435455U;
			num38 += num37 >> 28;
			num37 &= 268435455U;
			num42 += num41 >> 28;
			num41 &= 268435455U;
			num46 += num45 >> 28;
			num45 &= 268435455U;
			z[0] = num33;
			z[1] = num34;
			z[2] = num35;
			z[3] = num36;
			z[4] = num37;
			z[5] = num38;
			z[6] = num39;
			z[7] = num40;
			z[8] = num41;
			z[9] = num42;
			z[10] = num43;
			z[11] = num44;
			z[12] = num45;
			z[13] = num46;
			z[14] = num47;
			z[15] = num48;
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x0010D6DC File Offset: 0x0010D6DC
		public static void SubOne(uint[] z)
		{
			uint[] array = X448Field.Create();
			array[0] = 1U;
			X448Field.Sub(z, array, z);
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x0010D700 File Offset: 0x0010D700
		public static void Zero(uint[] z)
		{
			for (int i = 0; i < 16; i++)
			{
				z[i] = 0U;
			}
		}

		// Token: 0x04001CAC RID: 7340
		public const int Size = 16;

		// Token: 0x04001CAD RID: 7341
		private const uint M28 = 268435455U;
	}
}
