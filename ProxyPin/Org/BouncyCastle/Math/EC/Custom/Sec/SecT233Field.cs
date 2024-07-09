using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005CB RID: 1483
	internal class SecT233Field
	{
		// Token: 0x060030D8 RID: 12504 RVA: 0x000FF2D0 File Offset: 0x000FF2D0
		public static void Add(ulong[] x, ulong[] y, ulong[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
			z[2] = (x[2] ^ y[2]);
			z[3] = (x[3] ^ y[3]);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000FF2FC File Offset: 0x000FF2FC
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
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000FF360 File Offset: 0x000FF360
		public static void AddOne(ulong[] x, ulong[] z)
		{
			z[0] = (x[0] ^ 1UL);
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000FF380 File Offset: 0x000FF380
		private static void AddTo(ulong[] x, ulong[] z)
		{
			z[0] = (z[0] ^ x[0]);
			z[1] = (z[1] ^ x[1]);
			z[2] = (z[2] ^ x[2]);
			z[3] = (z[3] ^ x[3]);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000FF3C4 File Offset: 0x000FF3C4
		public static ulong[] FromBigInteger(BigInteger x)
		{
			return Nat.FromBigInteger64(233, x);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000FF3D4 File Offset: 0x000FF3D4
		public static void HalfTrace(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			Nat256.Copy64(x, z);
			for (int i = 1; i < 233; i += 2)
			{
				SecT233Field.ImplSquare(z, array);
				SecT233Field.Reduce(array, z);
				SecT233Field.ImplSquare(z, array);
				SecT233Field.Reduce(array, z);
				SecT233Field.AddTo(x, z);
			}
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000FF428 File Offset: 0x000FF428
		public static void Invert(ulong[] x, ulong[] z)
		{
			if (Nat256.IsZero64(x))
			{
				throw new InvalidOperationException();
			}
			ulong[] array = Nat256.Create64();
			ulong[] array2 = Nat256.Create64();
			SecT233Field.Square(x, array);
			SecT233Field.Multiply(array, x, array);
			SecT233Field.Square(array, array);
			SecT233Field.Multiply(array, x, array);
			SecT233Field.SquareN(array, 3, array2);
			SecT233Field.Multiply(array2, array, array2);
			SecT233Field.Square(array2, array2);
			SecT233Field.Multiply(array2, x, array2);
			SecT233Field.SquareN(array2, 7, array);
			SecT233Field.Multiply(array, array2, array);
			SecT233Field.SquareN(array, 14, array2);
			SecT233Field.Multiply(array2, array, array2);
			SecT233Field.Square(array2, array2);
			SecT233Field.Multiply(array2, x, array2);
			SecT233Field.SquareN(array2, 29, array);
			SecT233Field.Multiply(array, array2, array);
			SecT233Field.SquareN(array, 58, array2);
			SecT233Field.Multiply(array2, array, array2);
			SecT233Field.SquareN(array2, 116, array);
			SecT233Field.Multiply(array, array2, array);
			SecT233Field.Square(array, z);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000FF500 File Offset: 0x000FF500
		public static void Multiply(ulong[] x, ulong[] y, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT233Field.ImplMultiply(x, y, array);
			SecT233Field.Reduce(array, z);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000FF528 File Offset: 0x000FF528
		public static void MultiplyAddToExt(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT233Field.ImplMultiply(x, y, array);
			SecT233Field.AddExt(zz, array, zz);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000FF550 File Offset: 0x000FF550
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
			num4 ^= num8 << 23;
			num5 ^= (num8 >> 41 ^ num8 << 33);
			num6 ^= num8 >> 31;
			num3 ^= num7 << 23;
			num4 ^= (num7 >> 41 ^ num7 << 33);
			num5 ^= num7 >> 31;
			num2 ^= num6 << 23;
			num3 ^= (num6 >> 41 ^ num6 << 33);
			num4 ^= num6 >> 31;
			num ^= num5 << 23;
			num2 ^= (num5 >> 41 ^ num5 << 33);
			num3 ^= num5 >> 31;
			ulong num9 = num4 >> 41;
			z[0] = (num ^ num9);
			z[1] = (num2 ^ num9 << 10);
			z[2] = num3;
			z[3] = (num4 & 2199023255551UL);
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000FF62C File Offset: 0x000FF62C
		public static void Reduce23(ulong[] z, int zOff)
		{
			ulong num = z[zOff + 3];
			ulong num2 = num >> 41;
			z[zOff] ^= num2;
			IntPtr intPtr;
			z[(int)(intPtr = (IntPtr)(zOff + 1))] = (z[(int)intPtr] ^ num2 << 10);
			z[zOff + 3] = (num & 2199023255551UL);
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000FF678 File Offset: 0x000FF678
		public static void Sqrt(ulong[] x, ulong[] z)
		{
			ulong num = Interleave.Unshuffle(x[0]);
			ulong num2 = Interleave.Unshuffle(x[1]);
			ulong num3 = (num & (ulong)-1) | num2 << 32;
			ulong num4 = num >> 32 | (num2 & 18446744069414584320UL);
			num = Interleave.Unshuffle(x[2]);
			num2 = Interleave.Unshuffle(x[3]);
			ulong num5 = (num & (ulong)-1) | num2 << 32;
			ulong num6 = num >> 32 | (num2 & 18446744069414584320UL);
			ulong num7 = num6 >> 27;
			num6 ^= (num4 >> 27 | num6 << 37);
			num4 ^= num4 << 37;
			ulong[] array = Nat256.CreateExt64();
			int[] array2 = new int[]
			{
				32,
				117,
				191
			};
			for (int i = 0; i < array2.Length; i++)
			{
				int num8 = array2[i] >> 6;
				int num9 = array2[i] & 63;
				ulong[] array3;
				IntPtr intPtr;
				(array3 = array)[(int)(intPtr = (IntPtr)num8)] = (array3[(int)intPtr] ^ num4 << num9);
				(array3 = array)[(int)(intPtr = (IntPtr)(num8 + 1))] = (array3[(int)intPtr] ^ (num6 << num9 | num4 >> -num9));
				(array3 = array)[(int)(intPtr = (IntPtr)(num8 + 2))] = (array3[(int)intPtr] ^ (num7 << num9 | num6 >> -num9));
				(array3 = array)[(int)(intPtr = (IntPtr)(num8 + 3))] = (array3[(int)intPtr] ^ num7 >> -num9);
			}
			SecT233Field.Reduce(array, z);
			z[0] = (z[0] ^ num3);
			z[1] = (z[1] ^ num5);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000FF7E8 File Offset: 0x000FF7E8
		public static void Square(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT233Field.ImplSquare(x, array);
			SecT233Field.Reduce(array, z);
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000FF810 File Offset: 0x000FF810
		public static void SquareAddToExt(ulong[] x, ulong[] zz)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT233Field.ImplSquare(x, array);
			SecT233Field.AddExt(zz, array, zz);
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000FF838 File Offset: 0x000FF838
		public static void SquareN(ulong[] x, int n, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT233Field.ImplSquare(x, array);
			SecT233Field.Reduce(array, z);
			while (--n > 0)
			{
				SecT233Field.ImplSquare(z, array);
				SecT233Field.Reduce(array, z);
			}
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000FF87C File Offset: 0x000FF87C
		public static uint Trace(ulong[] x)
		{
			return (uint)(x[0] ^ x[2] >> 31) & 1U;
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000FF88C File Offset: 0x000FF88C
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
			zz[0] = (num ^ num2 << 59);
			zz[1] = (num2 >> 5 ^ num3 << 54);
			zz[2] = (num3 >> 10 ^ num4 << 49);
			zz[3] = (num4 >> 15 ^ num5 << 44);
			zz[4] = (num5 >> 20 ^ num6 << 39);
			zz[5] = (num6 >> 25 ^ num7 << 34);
			zz[6] = (num7 >> 30 ^ num8 << 29);
			zz[7] = num8 >> 35;
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000FF920 File Offset: 0x000FF920
		protected static void ImplExpand(ulong[] x, ulong[] z)
		{
			ulong num = x[0];
			ulong num2 = x[1];
			ulong num3 = x[2];
			ulong num4 = x[3];
			z[0] = (num & 576460752303423487UL);
			z[1] = ((num >> 59 ^ num2 << 5) & 576460752303423487UL);
			z[2] = ((num2 >> 54 ^ num3 << 10) & 576460752303423487UL);
			z[3] = (num3 >> 49 ^ num4 << 15);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000FF988 File Offset: 0x000FF988
		protected static void ImplMultiply(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = new ulong[4];
			ulong[] array2 = new ulong[4];
			SecT233Field.ImplExpand(x, array);
			SecT233Field.ImplExpand(y, array2);
			SecT233Field.ImplMulwAcc(array[0], array2[0], zz, 0);
			SecT233Field.ImplMulwAcc(array[1], array2[1], zz, 1);
			SecT233Field.ImplMulwAcc(array[2], array2[2], zz, 2);
			SecT233Field.ImplMulwAcc(array[3], array2[3], zz, 3);
			for (int i = 5; i > 0; i--)
			{
				IntPtr intPtr;
				zz[(int)(intPtr = (IntPtr)i)] = (zz[(int)intPtr] ^ zz[i - 1]);
			}
			SecT233Field.ImplMulwAcc(array[0] ^ array[1], array2[0] ^ array2[1], zz, 1);
			SecT233Field.ImplMulwAcc(array[2] ^ array[3], array2[2] ^ array2[3], zz, 3);
			for (int j = 7; j > 1; j--)
			{
				IntPtr intPtr;
				zz[(int)(intPtr = (IntPtr)j)] = (zz[(int)intPtr] ^ zz[j - 2]);
			}
			ulong num = array[0] ^ array[2];
			ulong num2 = array[1] ^ array[3];
			ulong num3 = array2[0] ^ array2[2];
			ulong num4 = array2[1] ^ array2[3];
			SecT233Field.ImplMulwAcc(num ^ num2, num3 ^ num4, zz, 3);
			ulong[] array3 = new ulong[3];
			SecT233Field.ImplMulwAcc(num, num3, array3, 0);
			SecT233Field.ImplMulwAcc(num2, num4, array3, 1);
			ulong num5 = array3[0];
			ulong num6 = array3[1];
			ulong num7 = array3[2];
			zz[2] = (zz[2] ^ num5);
			zz[3] = (zz[3] ^ (num5 ^ num6));
			zz[4] = (zz[4] ^ (num7 ^ num6));
			zz[5] = (zz[5] ^ num7);
			SecT233Field.ImplCompactExt(zz);
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000FFAFC File Offset: 0x000FFAFC
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
			int num4 = 54;
			do
			{
				num = (uint)(x >> num4);
				ulong num5 = array[(int)((UIntPtr)(num & 7U))] ^ array[(int)((UIntPtr)(num >> 3 & 7U))] << 3;
				num3 ^= num5 << num4;
				num2 ^= num5 >> -num4;
			}
			while ((num4 -= 6) > 0);
			z[zOff] ^= (num3 & 576460752303423487UL);
			IntPtr intPtr;
			z[(int)(intPtr = (IntPtr)(zOff + 1))] = (z[(int)intPtr] ^ (num3 >> 59 ^ num2 << 5));
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000FFBD4 File Offset: 0x000FFBD4
		protected static void ImplSquare(ulong[] x, ulong[] zz)
		{
			Interleave.Expand64To128(x[0], zz, 0);
			Interleave.Expand64To128(x[1], zz, 2);
			Interleave.Expand64To128(x[2], zz, 4);
			Interleave.Expand64To128(x[3], zz, 6);
		}

		// Token: 0x04001C3C RID: 7228
		private const ulong M41 = 2199023255551UL;

		// Token: 0x04001C3D RID: 7229
		private const ulong M59 = 576460752303423487UL;
	}
}
