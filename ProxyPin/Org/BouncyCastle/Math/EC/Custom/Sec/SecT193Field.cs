using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005C5 RID: 1477
	internal class SecT193Field
	{
		// Token: 0x0600306B RID: 12395 RVA: 0x000FD4D0 File Offset: 0x000FD4D0
		public static void Add(ulong[] x, ulong[] y, ulong[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
			z[2] = (x[2] ^ y[2]);
			z[3] = (x[3] ^ y[3]);
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000FD4FC File Offset: 0x000FD4FC
		public static void AddExt(ulong[] xx, ulong[] yy, ulong[] zz)
		{
			zz[0] = (xx[0] ^ yy[0]);
			zz[1] = (xx[1] ^ yy[1]);
			zz[2] = (xx[2] ^ yy[2]);
			zz[3] = (xx[3] ^ yy[3]);
			zz[4] = (xx[4] ^ yy[4]);
			zz[5] = (xx[5] ^ yy[5]);
			zz[6] = (xx[6] ^ yy[6]);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000FD554 File Offset: 0x000FD554
		public static void AddOne(ulong[] x, ulong[] z)
		{
			z[0] = (x[0] ^ 1UL);
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000FD574 File Offset: 0x000FD574
		private static void AddTo(ulong[] x, ulong[] z)
		{
			z[0] = (z[0] ^ x[0]);
			z[1] = (z[1] ^ x[1]);
			z[2] = (z[2] ^ x[2]);
			z[3] = (z[3] ^ x[3]);
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000FD5B8 File Offset: 0x000FD5B8
		public static ulong[] FromBigInteger(BigInteger x)
		{
			return Nat.FromBigInteger64(193, x);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000FD5C8 File Offset: 0x000FD5C8
		public static void HalfTrace(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			Nat256.Copy64(x, z);
			for (int i = 1; i < 193; i += 2)
			{
				SecT193Field.ImplSquare(z, array);
				SecT193Field.Reduce(array, z);
				SecT193Field.ImplSquare(z, array);
				SecT193Field.Reduce(array, z);
				SecT193Field.AddTo(x, z);
			}
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000FD61C File Offset: 0x000FD61C
		public static void Invert(ulong[] x, ulong[] z)
		{
			if (Nat256.IsZero64(x))
			{
				throw new InvalidOperationException();
			}
			ulong[] array = Nat256.Create64();
			ulong[] array2 = Nat256.Create64();
			SecT193Field.Square(x, array);
			SecT193Field.SquareN(array, 1, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array2, 1, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 3, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 6, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 12, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 24, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 48, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 96, array2);
			SecT193Field.Multiply(array, array2, z);
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000FD6D8 File Offset: 0x000FD6D8
		public static void Multiply(ulong[] x, ulong[] y, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT193Field.ImplMultiply(x, y, array);
			SecT193Field.Reduce(array, z);
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000FD700 File Offset: 0x000FD700
		public static void MultiplyAddToExt(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT193Field.ImplMultiply(x, y, array);
			SecT193Field.AddExt(zz, array, zz);
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000FD728 File Offset: 0x000FD728
		public static void Reduce(ulong[] xx, ulong[] z)
		{
			ulong num = xx[0];
			ulong num2 = xx[1];
			ulong num3 = xx[2];
			ulong num4 = xx[3];
			ulong num5 = xx[4];
			ulong num6 = xx[5];
			ulong num7 = xx[6];
			num3 ^= num7 << 63;
			num4 ^= (num7 >> 1 ^ num7 << 14);
			num5 ^= num7 >> 50;
			num2 ^= num6 << 63;
			num3 ^= (num6 >> 1 ^ num6 << 14);
			num4 ^= num6 >> 50;
			num ^= num5 << 63;
			num2 ^= (num5 >> 1 ^ num5 << 14);
			num3 ^= num5 >> 50;
			ulong num8 = num4 >> 1;
			z[0] = (num ^ num8 ^ num8 << 15);
			z[1] = (num2 ^ num8 >> 49);
			z[2] = num3;
			z[3] = (num4 & 1UL);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000FD7D8 File Offset: 0x000FD7D8
		public static void Reduce63(ulong[] z, int zOff)
		{
			ulong num = z[zOff + 3];
			ulong num2 = num >> 1;
			z[zOff] ^= (num2 ^ num2 << 15);
			IntPtr intPtr;
			z[(int)(intPtr = (IntPtr)(zOff + 1))] = (z[(int)intPtr] ^ num2 >> 49);
			z[zOff + 3] = (num & 1UL);
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000FD820 File Offset: 0x000FD820
		public static void Sqrt(ulong[] x, ulong[] z)
		{
			ulong num = Interleave.Unshuffle(x[0]);
			ulong num2 = Interleave.Unshuffle(x[1]);
			ulong num3 = (num & (ulong)-1) | num2 << 32;
			ulong num4 = num >> 32 | (num2 & 18446744069414584320UL);
			num = Interleave.Unshuffle(x[2]);
			ulong num5 = (num & (ulong)-1) ^ x[3] << 32;
			ulong num6 = num >> 32;
			z[0] = (num3 ^ num4 << 8);
			z[1] = (num5 ^ num6 << 8 ^ num4 >> 56 ^ num4 << 33);
			z[2] = (num6 >> 56 ^ num6 << 33 ^ num4 >> 31);
			z[3] = num6 >> 31;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000FD8B4 File Offset: 0x000FD8B4
		public static void Square(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT193Field.ImplSquare(x, array);
			SecT193Field.Reduce(array, z);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000FD8DC File Offset: 0x000FD8DC
		public static void SquareAddToExt(ulong[] x, ulong[] zz)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT193Field.ImplSquare(x, array);
			SecT193Field.AddExt(zz, array, zz);
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000FD904 File Offset: 0x000FD904
		public static void SquareN(ulong[] x, int n, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT193Field.ImplSquare(x, array);
			SecT193Field.Reduce(array, z);
			while (--n > 0)
			{
				SecT193Field.ImplSquare(z, array);
				SecT193Field.Reduce(array, z);
			}
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000FD948 File Offset: 0x000FD948
		public static uint Trace(ulong[] x)
		{
			return (uint)x[0] & 1U;
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000FD950 File Offset: 0x000FD950
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
			zz[0] = (num ^ num2 << 49);
			zz[1] = (num2 >> 15 ^ num3 << 34);
			zz[2] = (num3 >> 30 ^ num4 << 19);
			zz[3] = (num4 >> 45 ^ num5 << 4 ^ num6 << 53);
			zz[4] = (num5 >> 60 ^ num7 << 38 ^ num6 >> 11);
			zz[5] = (num7 >> 26 ^ num8 << 23);
			zz[6] = num8 >> 41;
			zz[7] = 0UL;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000FD9E8 File Offset: 0x000FD9E8
		protected static void ImplExpand(ulong[] x, ulong[] z)
		{
			ulong num = x[0];
			ulong num2 = x[1];
			ulong num3 = x[2];
			ulong num4 = x[3];
			z[0] = (num & 562949953421311UL);
			z[1] = ((num >> 49 ^ num2 << 15) & 562949953421311UL);
			z[2] = ((num2 >> 34 ^ num3 << 30) & 562949953421311UL);
			z[3] = (num3 >> 19 ^ num4 << 45);
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000FDA50 File Offset: 0x000FDA50
		protected static void ImplMultiply(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = new ulong[4];
			ulong[] array2 = new ulong[4];
			SecT193Field.ImplExpand(x, array);
			SecT193Field.ImplExpand(y, array2);
			SecT193Field.ImplMulwAcc(array[0], array2[0], zz, 0);
			SecT193Field.ImplMulwAcc(array[1], array2[1], zz, 1);
			SecT193Field.ImplMulwAcc(array[2], array2[2], zz, 2);
			SecT193Field.ImplMulwAcc(array[3], array2[3], zz, 3);
			for (int i = 5; i > 0; i--)
			{
				IntPtr intPtr;
				zz[(int)(intPtr = (IntPtr)i)] = (zz[(int)intPtr] ^ zz[i - 1]);
			}
			SecT193Field.ImplMulwAcc(array[0] ^ array[1], array2[0] ^ array2[1], zz, 1);
			SecT193Field.ImplMulwAcc(array[2] ^ array[3], array2[2] ^ array2[3], zz, 3);
			for (int j = 7; j > 1; j--)
			{
				IntPtr intPtr;
				zz[(int)(intPtr = (IntPtr)j)] = (zz[(int)intPtr] ^ zz[j - 2]);
			}
			ulong num = array[0] ^ array[2];
			ulong num2 = array[1] ^ array[3];
			ulong num3 = array2[0] ^ array2[2];
			ulong num4 = array2[1] ^ array2[3];
			SecT193Field.ImplMulwAcc(num ^ num2, num3 ^ num4, zz, 3);
			ulong[] array3 = new ulong[3];
			SecT193Field.ImplMulwAcc(num, num3, array3, 0);
			SecT193Field.ImplMulwAcc(num2, num4, array3, 1);
			ulong num5 = array3[0];
			ulong num6 = array3[1];
			ulong num7 = array3[2];
			zz[2] = (zz[2] ^ num5);
			zz[3] = (zz[3] ^ (num5 ^ num6));
			zz[4] = (zz[4] ^ (num7 ^ num6));
			zz[5] = (zz[5] ^ num7);
			SecT193Field.ImplCompactExt(zz);
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000FDBC4 File Offset: 0x000FDBC4
		protected static void ImplMulwAcc(ulong x, ulong y, ulong[] z, int zOff)
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
			ulong num3 = array[(int)((UIntPtr)(num & 7U))] ^ array[(int)((UIntPtr)(num >> 3 & 7U))] << 3;
			int num4 = 36;
			do
			{
				num = (uint)(x >> num4);
				ulong num5 = array[(int)((UIntPtr)(num & 7U))] ^ array[(int)((UIntPtr)(num >> 3 & 7U))] << 3 ^ array[(int)((UIntPtr)(num >> 6 & 7U))] << 6 ^ array[(int)((UIntPtr)(num >> 9 & 7U))] << 9 ^ array[(int)((UIntPtr)(num >> 12 & 7U))] << 12;
				num3 ^= num5 << num4;
				num2 ^= num5 >> -num4;
			}
			while ((num4 -= 15) > 0);
			z[zOff] ^= (num3 & 562949953421311UL);
			IntPtr intPtr;
			z[(int)(intPtr = (IntPtr)(zOff + 1))] = (z[(int)intPtr] ^ (num3 >> 49 ^ num2 << 15));
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000FDCC4 File Offset: 0x000FDCC4
		protected static void ImplSquare(ulong[] x, ulong[] zz)
		{
			Interleave.Expand64To128(x[0], zz, 0);
			Interleave.Expand64To128(x[1], zz, 2);
			Interleave.Expand64To128(x[2], zz, 4);
			zz[6] = (x[3] & 1UL);
		}

		// Token: 0x04001C31 RID: 7217
		private const ulong M01 = 1UL;

		// Token: 0x04001C32 RID: 7218
		private const ulong M49 = 562949953421311UL;
	}
}
