using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005D5 RID: 1493
	internal class SecT283Field
	{
		// Token: 0x0600319A RID: 12698 RVA: 0x00102778 File Offset: 0x00102778
		public static void Add(ulong[] x, ulong[] y, ulong[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
			z[2] = (x[2] ^ y[2]);
			z[3] = (x[3] ^ y[3]);
			z[4] = (x[4] ^ y[4]);
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x001027AC File Offset: 0x001027AC
		public static void AddExt(ulong[] xx, ulong[] yy, ulong[] zz)
		{
			zz[0] = (xx[0] ^ yy[0]);
			zz[1] = (xx[1] ^ yy[1]);
			zz[2] = (xx[2] ^ yy[2]);
			zz[3] = (xx[3] ^ yy[3]);
			zz[4] = (xx[4] ^ yy[4]);
			zz[5] = (xx[5] ^ yy[5]);
			zz[6] = (xx[6] ^ yy[6]);
			zz[7] = (xx[7] ^ yy[7]);
			zz[8] = (xx[8] ^ yy[8]);
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x00102818 File Offset: 0x00102818
		public static void AddOne(ulong[] x, ulong[] z)
		{
			z[0] = (x[0] ^ 1UL);
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
			z[4] = x[4];
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x0010283C File Offset: 0x0010283C
		private static void AddTo(ulong[] x, ulong[] z)
		{
			z[0] = (z[0] ^ x[0]);
			z[1] = (z[1] ^ x[1]);
			z[2] = (z[2] ^ x[2]);
			z[3] = (z[3] ^ x[3]);
			z[4] = (z[4] ^ x[4]);
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x0010288C File Offset: 0x0010288C
		public static ulong[] FromBigInteger(BigInteger x)
		{
			return Nat.FromBigInteger64(283, x);
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x0010289C File Offset: 0x0010289C
		public static void HalfTrace(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat.Create64(9);
			Nat320.Copy64(x, z);
			for (int i = 1; i < 283; i += 2)
			{
				SecT283Field.ImplSquare(z, array);
				SecT283Field.Reduce(array, z);
				SecT283Field.ImplSquare(z, array);
				SecT283Field.Reduce(array, z);
				SecT283Field.AddTo(x, z);
			}
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x001028F4 File Offset: 0x001028F4
		public static void Invert(ulong[] x, ulong[] z)
		{
			if (Nat320.IsZero64(x))
			{
				throw new InvalidOperationException();
			}
			ulong[] array = Nat320.Create64();
			ulong[] array2 = Nat320.Create64();
			SecT283Field.Square(x, array);
			SecT283Field.Multiply(array, x, array);
			SecT283Field.SquareN(array, 2, array2);
			SecT283Field.Multiply(array2, array, array2);
			SecT283Field.SquareN(array2, 4, array);
			SecT283Field.Multiply(array, array2, array);
			SecT283Field.SquareN(array, 8, array2);
			SecT283Field.Multiply(array2, array, array2);
			SecT283Field.Square(array2, array2);
			SecT283Field.Multiply(array2, x, array2);
			SecT283Field.SquareN(array2, 17, array);
			SecT283Field.Multiply(array, array2, array);
			SecT283Field.Square(array, array);
			SecT283Field.Multiply(array, x, array);
			SecT283Field.SquareN(array, 35, array2);
			SecT283Field.Multiply(array2, array, array2);
			SecT283Field.SquareN(array2, 70, array);
			SecT283Field.Multiply(array, array2, array);
			SecT283Field.Square(array, array);
			SecT283Field.Multiply(array, x, array);
			SecT283Field.SquareN(array, 141, array2);
			SecT283Field.Multiply(array2, array, array2);
			SecT283Field.Square(array2, z);
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x001029DC File Offset: 0x001029DC
		public static void Multiply(ulong[] x, ulong[] y, ulong[] z)
		{
			ulong[] array = Nat320.CreateExt64();
			SecT283Field.ImplMultiply(x, y, array);
			SecT283Field.Reduce(array, z);
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x00102A04 File Offset: 0x00102A04
		public static void MultiplyAddToExt(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = Nat320.CreateExt64();
			SecT283Field.ImplMultiply(x, y, array);
			SecT283Field.AddExt(zz, array, zz);
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x00102A2C File Offset: 0x00102A2C
		public static void Reduce(ulong[] xx, ulong[] z)
		{
			ulong num = xx[0];
			ulong num2 = xx[1];
			ulong num3 = xx[2];
			ulong num4 = xx[3];
			ulong num5 = xx[4];
			ulong num6 = xx[5];
			ulong num7 = xx[6];
			ulong num8 = xx[7];
			ulong num9 = xx[8];
			num4 ^= (num9 << 37 ^ num9 << 42 ^ num9 << 44 ^ num9 << 49);
			num5 ^= (num9 >> 27 ^ num9 >> 22 ^ num9 >> 20 ^ num9 >> 15);
			num3 ^= (num8 << 37 ^ num8 << 42 ^ num8 << 44 ^ num8 << 49);
			num4 ^= (num8 >> 27 ^ num8 >> 22 ^ num8 >> 20 ^ num8 >> 15);
			num2 ^= (num7 << 37 ^ num7 << 42 ^ num7 << 44 ^ num7 << 49);
			num3 ^= (num7 >> 27 ^ num7 >> 22 ^ num7 >> 20 ^ num7 >> 15);
			num ^= (num6 << 37 ^ num6 << 42 ^ num6 << 44 ^ num6 << 49);
			num2 ^= (num6 >> 27 ^ num6 >> 22 ^ num6 >> 20 ^ num6 >> 15);
			ulong num10 = num5 >> 27;
			z[0] = (num ^ num10 ^ num10 << 5 ^ num10 << 7 ^ num10 << 12);
			z[1] = num2;
			z[2] = num3;
			z[3] = num4;
			z[4] = (num5 & 134217727UL);
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x00102B70 File Offset: 0x00102B70
		public static void Reduce37(ulong[] z, int zOff)
		{
			ulong num = z[zOff + 4];
			ulong num2 = num >> 27;
			z[zOff] ^= (num2 ^ num2 << 5 ^ num2 << 7 ^ num2 << 12);
			z[zOff + 4] = (num & 134217727UL);
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x00102BB4 File Offset: 0x00102BB4
		public static void Sqrt(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat320.Create64();
			ulong num = Interleave.Unshuffle(x[0]);
			ulong num2 = Interleave.Unshuffle(x[1]);
			ulong num3 = (num & (ulong)-1) | num2 << 32;
			array[0] = (num >> 32 | (num2 & 18446744069414584320UL));
			num = Interleave.Unshuffle(x[2]);
			num2 = Interleave.Unshuffle(x[3]);
			ulong num4 = (num & (ulong)-1) | num2 << 32;
			array[1] = (num >> 32 | (num2 & 18446744069414584320UL));
			num = Interleave.Unshuffle(x[4]);
			ulong num5 = num & (ulong)-1;
			array[2] = num >> 32;
			SecT283Field.Multiply(array, SecT283Field.ROOT_Z, z);
			z[0] = (z[0] ^ num3);
			z[1] = (z[1] ^ num4);
			z[2] = (z[2] ^ num5);
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x00102C74 File Offset: 0x00102C74
		public static void Square(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat.Create64(9);
			SecT283Field.ImplSquare(x, array);
			SecT283Field.Reduce(array, z);
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x00102C9C File Offset: 0x00102C9C
		public static void SquareAddToExt(ulong[] x, ulong[] zz)
		{
			ulong[] array = Nat.Create64(9);
			SecT283Field.ImplSquare(x, array);
			SecT283Field.AddExt(zz, array, zz);
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x00102CC4 File Offset: 0x00102CC4
		public static void SquareN(ulong[] x, int n, ulong[] z)
		{
			ulong[] array = Nat.Create64(9);
			SecT283Field.ImplSquare(x, array);
			SecT283Field.Reduce(array, z);
			while (--n > 0)
			{
				SecT283Field.ImplSquare(z, array);
				SecT283Field.Reduce(array, z);
			}
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x00102D08 File Offset: 0x00102D08
		public static uint Trace(ulong[] x)
		{
			return (uint)(x[0] ^ x[4] >> 15) & 1U;
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x00102D18 File Offset: 0x00102D18
		protected static void ImplCompactExt(ulong[] zz)
		{
			ulong num = zz[0];
			ulong num2 = zz[1];
			ulong num3 = zz[2];
			ulong num4 = zz[3];
			ulong num5 = zz[4];
			ulong num6 = zz[5];
			ulong num7 = zz[6];
			ulong num8 = zz[7];
			ulong num9 = zz[8];
			ulong num10 = zz[9];
			zz[0] = (num ^ num2 << 57);
			zz[1] = (num2 >> 7 ^ num3 << 50);
			zz[2] = (num3 >> 14 ^ num4 << 43);
			zz[3] = (num4 >> 21 ^ num5 << 36);
			zz[4] = (num5 >> 28 ^ num6 << 29);
			zz[5] = (num6 >> 35 ^ num7 << 22);
			zz[6] = (num7 >> 42 ^ num8 << 15);
			zz[7] = (num8 >> 49 ^ num9 << 8);
			zz[8] = (num9 >> 56 ^ num10 << 1);
			zz[9] = num10 >> 63;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x00102DD4 File Offset: 0x00102DD4
		protected static void ImplExpand(ulong[] x, ulong[] z)
		{
			ulong num = x[0];
			ulong num2 = x[1];
			ulong num3 = x[2];
			ulong num4 = x[3];
			ulong num5 = x[4];
			z[0] = (num & 144115188075855871UL);
			z[1] = ((num >> 57 ^ num2 << 7) & 144115188075855871UL);
			z[2] = ((num2 >> 50 ^ num3 << 14) & 144115188075855871UL);
			z[3] = ((num3 >> 43 ^ num4 << 21) & 144115188075855871UL);
			z[4] = (num4 >> 36 ^ num5 << 28);
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x00102E58 File Offset: 0x00102E58
		protected static void ImplMultiply(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = new ulong[5];
			ulong[] array2 = new ulong[5];
			SecT283Field.ImplExpand(x, array);
			SecT283Field.ImplExpand(y, array2);
			ulong[] array3 = new ulong[26];
			SecT283Field.ImplMulw(array[0], array2[0], array3, 0);
			SecT283Field.ImplMulw(array[1], array2[1], array3, 2);
			SecT283Field.ImplMulw(array[2], array2[2], array3, 4);
			SecT283Field.ImplMulw(array[3], array2[3], array3, 6);
			SecT283Field.ImplMulw(array[4], array2[4], array3, 8);
			ulong num = array[0] ^ array[1];
			ulong num2 = array2[0] ^ array2[1];
			ulong num3 = array[0] ^ array[2];
			ulong num4 = array2[0] ^ array2[2];
			ulong num5 = array[2] ^ array[4];
			ulong num6 = array2[2] ^ array2[4];
			ulong num7 = array[3] ^ array[4];
			ulong num8 = array2[3] ^ array2[4];
			SecT283Field.ImplMulw(num3 ^ array[3], num4 ^ array2[3], array3, 18);
			SecT283Field.ImplMulw(num5 ^ array[1], num6 ^ array2[1], array3, 20);
			ulong num9 = num ^ num7;
			ulong num10 = num2 ^ num8;
			ulong x2 = num9 ^ array[2];
			ulong y2 = num10 ^ array2[2];
			SecT283Field.ImplMulw(num9, num10, array3, 22);
			SecT283Field.ImplMulw(x2, y2, array3, 24);
			SecT283Field.ImplMulw(num, num2, array3, 10);
			SecT283Field.ImplMulw(num3, num4, array3, 12);
			SecT283Field.ImplMulw(num5, num6, array3, 14);
			SecT283Field.ImplMulw(num7, num8, array3, 16);
			zz[0] = array3[0];
			zz[9] = array3[9];
			ulong num11 = array3[0] ^ array3[1];
			ulong num12 = num11 ^ array3[2];
			ulong num13 = num12 ^ array3[10];
			zz[1] = num13;
			ulong num14 = array3[3] ^ array3[4];
			ulong num15 = array3[11] ^ array3[12];
			ulong num16 = num14 ^ num15;
			ulong num17 = num12 ^ num16;
			zz[2] = num17;
			ulong num18 = num11 ^ num14;
			ulong num19 = array3[5] ^ array3[6];
			ulong num20 = num18 ^ num19;
			ulong num21 = num20 ^ array3[8];
			ulong num22 = array3[13] ^ array3[14];
			ulong num23 = num21 ^ num22;
			ulong num24 = array3[18] ^ array3[22];
			ulong num25 = num24 ^ array3[24];
			ulong num26 = num23 ^ num25;
			zz[3] = num26;
			ulong num27 = array3[7] ^ array3[8];
			ulong num28 = num27 ^ array3[9];
			ulong num29 = num28 ^ array3[17];
			zz[8] = num29;
			ulong num30 = num28 ^ num19;
			ulong num31 = array3[15] ^ array3[16];
			ulong num32 = num30 ^ num31;
			zz[7] = num32;
			ulong num33 = num32 ^ num13;
			ulong num34 = array3[19] ^ array3[20];
			ulong num35 = array3[25] ^ array3[24];
			ulong num36 = array3[18] ^ array3[23];
			ulong num37 = num34 ^ num35;
			ulong num38 = num37 ^ num36;
			ulong num39 = num38 ^ num33;
			zz[4] = num39;
			ulong num40 = num17 ^ num29;
			ulong num41 = num37 ^ num40;
			ulong num42 = array3[21] ^ array3[22];
			ulong num43 = num41 ^ num42;
			zz[5] = num43;
			ulong num44 = num21 ^ array3[0];
			ulong num45 = num44 ^ array3[9];
			ulong num46 = num45 ^ num22;
			ulong num47 = num46 ^ array3[21];
			ulong num48 = num47 ^ array3[23];
			ulong num49 = num48 ^ array3[25];
			zz[6] = num49;
			SecT283Field.ImplCompactExt(zz);
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x0010312C File Offset: 0x0010312C
		protected static void ImplMulw(ulong x, ulong y, ulong[] z, int zOff)
		{
			ulong[] array = new ulong[8];
			array[1] = y;
			array[2] = array[1] << 1;
			array[3] = (array[2] ^ y);
			array[4] = array[2] << 1;
			array[5] = (array[4] ^ y);
			array[6] = array[3] << 1;
			array[7] = (array[6] ^ y);
			uint num = (uint)x;
			ulong num2 = 0UL;
			ulong num3 = array[(int)((UIntPtr)(num & 7U))];
			int num4 = 48;
			do
			{
				num = (uint)(x >> num4);
				ulong num5 = array[(int)((UIntPtr)(num & 7U))] ^ array[(int)((UIntPtr)(num >> 3 & 7U))] << 3 ^ array[(int)((UIntPtr)(num >> 6 & 7U))] << 6;
				num3 ^= num5 << num4;
				num2 ^= num5 >> -num4;
			}
			while ((num4 -= 9) > 0);
			num2 ^= (x & 72198606942111744UL & y << 7 >> 63) >> 8;
			z[zOff] = (num3 & 144115188075855871UL);
			z[zOff + 1] = (num3 >> 57 ^ num2 << 7);
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x00103204 File Offset: 0x00103204
		protected static void ImplSquare(ulong[] x, ulong[] zz)
		{
			Interleave.Expand64To128(x[0], zz, 0);
			Interleave.Expand64To128(x[1], zz, 2);
			Interleave.Expand64To128(x[2], zz, 4);
			Interleave.Expand64To128(x[3], zz, 6);
			zz[8] = Interleave.Expand32to64((uint)x[4]);
		}

		// Token: 0x04001C4E RID: 7246
		private const ulong M27 = 134217727UL;

		// Token: 0x04001C4F RID: 7247
		private const ulong M57 = 144115188075855871UL;

		// Token: 0x04001C50 RID: 7248
		private static readonly ulong[] ROOT_Z = new ulong[]
		{
			878416384462358536UL,
			3513665537849438403UL,
			9369774767598502668UL,
			585610922974906400UL,
			34087042UL
		};
	}
}
