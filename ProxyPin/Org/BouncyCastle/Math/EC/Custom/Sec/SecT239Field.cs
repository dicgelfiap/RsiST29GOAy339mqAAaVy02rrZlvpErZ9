using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005D1 RID: 1489
	internal class SecT239Field
	{
		// Token: 0x06003146 RID: 12614 RVA: 0x0010113C File Offset: 0x0010113C
		public static void Add(ulong[] x, ulong[] y, ulong[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
			z[2] = (x[2] ^ y[2]);
			z[3] = (x[3] ^ y[3]);
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x00101168 File Offset: 0x00101168
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

		// Token: 0x06003148 RID: 12616 RVA: 0x001011CC File Offset: 0x001011CC
		public static void AddOne(ulong[] x, ulong[] z)
		{
			z[0] = (x[0] ^ 1UL);
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x001011EC File Offset: 0x001011EC
		private static void AddTo(ulong[] x, ulong[] z)
		{
			z[0] = (z[0] ^ x[0]);
			z[1] = (z[1] ^ x[1]);
			z[2] = (z[2] ^ x[2]);
			z[3] = (z[3] ^ x[3]);
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x00101230 File Offset: 0x00101230
		public static ulong[] FromBigInteger(BigInteger x)
		{
			return Nat.FromBigInteger64(239, x);
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x00101240 File Offset: 0x00101240
		public static void HalfTrace(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			Nat256.Copy64(x, z);
			for (int i = 1; i < 239; i += 2)
			{
				SecT239Field.ImplSquare(z, array);
				SecT239Field.Reduce(array, z);
				SecT239Field.ImplSquare(z, array);
				SecT239Field.Reduce(array, z);
				SecT239Field.AddTo(x, z);
			}
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x00101294 File Offset: 0x00101294
		public static void Invert(ulong[] x, ulong[] z)
		{
			if (Nat256.IsZero64(x))
			{
				throw new InvalidOperationException();
			}
			ulong[] array = Nat256.Create64();
			ulong[] array2 = Nat256.Create64();
			SecT239Field.Square(x, array);
			SecT239Field.Multiply(array, x, array);
			SecT239Field.Square(array, array);
			SecT239Field.Multiply(array, x, array);
			SecT239Field.SquareN(array, 3, array2);
			SecT239Field.Multiply(array2, array, array2);
			SecT239Field.Square(array2, array2);
			SecT239Field.Multiply(array2, x, array2);
			SecT239Field.SquareN(array2, 7, array);
			SecT239Field.Multiply(array, array2, array);
			SecT239Field.SquareN(array, 14, array2);
			SecT239Field.Multiply(array2, array, array2);
			SecT239Field.Square(array2, array2);
			SecT239Field.Multiply(array2, x, array2);
			SecT239Field.SquareN(array2, 29, array);
			SecT239Field.Multiply(array, array2, array);
			SecT239Field.Square(array, array);
			SecT239Field.Multiply(array, x, array);
			SecT239Field.SquareN(array, 59, array2);
			SecT239Field.Multiply(array2, array, array2);
			SecT239Field.Square(array2, array2);
			SecT239Field.Multiply(array2, x, array2);
			SecT239Field.SquareN(array2, 119, array);
			SecT239Field.Multiply(array, array2, array);
			SecT239Field.Square(array, z);
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x00101388 File Offset: 0x00101388
		public static void Multiply(ulong[] x, ulong[] y, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT239Field.ImplMultiply(x, y, array);
			SecT239Field.Reduce(array, z);
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x001013B0 File Offset: 0x001013B0
		public static void MultiplyAddToExt(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT239Field.ImplMultiply(x, y, array);
			SecT239Field.AddExt(zz, array, zz);
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x001013D8 File Offset: 0x001013D8
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
			num4 ^= num8 << 17;
			num5 ^= num8 >> 47;
			num6 ^= num8 << 47;
			num7 ^= num8 >> 17;
			num3 ^= num7 << 17;
			num4 ^= num7 >> 47;
			num5 ^= num7 << 47;
			num6 ^= num7 >> 17;
			num2 ^= num6 << 17;
			num3 ^= num6 >> 47;
			num4 ^= num6 << 47;
			num5 ^= num6 >> 17;
			num ^= num5 << 17;
			num2 ^= num5 >> 47;
			num3 ^= num5 << 47;
			num4 ^= num5 >> 17;
			ulong num9 = num4 >> 47;
			z[0] = (num ^ num9);
			z[1] = num2;
			z[2] = (num3 ^ num9 << 30);
			z[3] = (num4 & 140737488355327UL);
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x001014C4 File Offset: 0x001014C4
		public static void Reduce17(ulong[] z, int zOff)
		{
			ulong num = z[zOff + 3];
			ulong num2 = num >> 47;
			z[zOff] ^= num2;
			IntPtr intPtr;
			z[(int)(intPtr = (IntPtr)(zOff + 2))] = (z[(int)intPtr] ^ num2 << 30);
			z[zOff + 3] = (num & 140737488355327UL);
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x00101510 File Offset: 0x00101510
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
			ulong num7 = num6 >> 49;
			ulong num8 = num4 >> 49 | num6 << 15;
			num6 ^= num4 << 15;
			ulong[] array = Nat256.CreateExt64();
			int[] array2 = new int[]
			{
				39,
				120
			};
			for (int i = 0; i < array2.Length; i++)
			{
				int num9 = array2[i] >> 6;
				int num10 = array2[i] & 63;
				ulong[] array3;
				IntPtr intPtr;
				(array3 = array)[(int)(intPtr = (IntPtr)num9)] = (array3[(int)intPtr] ^ num4 << num10);
				(array3 = array)[(int)(intPtr = (IntPtr)(num9 + 1))] = (array3[(int)intPtr] ^ (num6 << num10 | num4 >> -num10));
				(array3 = array)[(int)(intPtr = (IntPtr)(num9 + 2))] = (array3[(int)intPtr] ^ (num8 << num10 | num6 >> -num10));
				(array3 = array)[(int)(intPtr = (IntPtr)(num9 + 3))] = (array3[(int)intPtr] ^ (num7 << num10 | num8 >> -num10));
				(array3 = array)[(int)(intPtr = (IntPtr)(num9 + 4))] = (array3[(int)intPtr] ^ num7 >> -num10);
			}
			SecT239Field.Reduce(array, z);
			z[0] = (z[0] ^ num3);
			z[1] = (z[1] ^ num5);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x001016A8 File Offset: 0x001016A8
		public static void Square(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT239Field.ImplSquare(x, array);
			SecT239Field.Reduce(array, z);
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x001016D0 File Offset: 0x001016D0
		public static void SquareAddToExt(ulong[] x, ulong[] zz)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT239Field.ImplSquare(x, array);
			SecT239Field.AddExt(zz, array, zz);
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x001016F8 File Offset: 0x001016F8
		public static void SquareN(ulong[] x, int n, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT239Field.ImplSquare(x, array);
			SecT239Field.Reduce(array, z);
			while (--n > 0)
			{
				SecT239Field.ImplSquare(z, array);
				SecT239Field.Reduce(array, z);
			}
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x0010173C File Offset: 0x0010173C
		public static uint Trace(ulong[] x)
		{
			return (uint)(x[0] ^ x[1] >> 17 ^ x[2] >> 34) & 1U;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x00101754 File Offset: 0x00101754
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
			zz[0] = (num ^ num2 << 60);
			zz[1] = (num2 >> 4 ^ num3 << 56);
			zz[2] = (num3 >> 8 ^ num4 << 52);
			zz[3] = (num4 >> 12 ^ num5 << 48);
			zz[4] = (num5 >> 16 ^ num6 << 44);
			zz[5] = (num6 >> 20 ^ num7 << 40);
			zz[6] = (num7 >> 24 ^ num8 << 36);
			zz[7] = num8 >> 28;
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x001017E8 File Offset: 0x001017E8
		protected static void ImplExpand(ulong[] x, ulong[] z)
		{
			ulong num = x[0];
			ulong num2 = x[1];
			ulong num3 = x[2];
			ulong num4 = x[3];
			z[0] = (num & 1152921504606846975UL);
			z[1] = ((num >> 60 ^ num2 << 4) & 1152921504606846975UL);
			z[2] = ((num2 >> 56 ^ num3 << 8) & 1152921504606846975UL);
			z[3] = (num3 >> 52 ^ num4 << 12);
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x00101850 File Offset: 0x00101850
		protected static void ImplMultiply(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = new ulong[4];
			ulong[] array2 = new ulong[4];
			SecT239Field.ImplExpand(x, array);
			SecT239Field.ImplExpand(y, array2);
			SecT239Field.ImplMulwAcc(array[0], array2[0], zz, 0);
			SecT239Field.ImplMulwAcc(array[1], array2[1], zz, 1);
			SecT239Field.ImplMulwAcc(array[2], array2[2], zz, 2);
			SecT239Field.ImplMulwAcc(array[3], array2[3], zz, 3);
			for (int i = 5; i > 0; i--)
			{
				IntPtr intPtr;
				zz[(int)(intPtr = (IntPtr)i)] = (zz[(int)intPtr] ^ zz[i - 1]);
			}
			SecT239Field.ImplMulwAcc(array[0] ^ array[1], array2[0] ^ array2[1], zz, 1);
			SecT239Field.ImplMulwAcc(array[2] ^ array[3], array2[2] ^ array2[3], zz, 3);
			for (int j = 7; j > 1; j--)
			{
				IntPtr intPtr;
				zz[(int)(intPtr = (IntPtr)j)] = (zz[(int)intPtr] ^ zz[j - 2]);
			}
			ulong num = array[0] ^ array[2];
			ulong num2 = array[1] ^ array[3];
			ulong num3 = array2[0] ^ array2[2];
			ulong num4 = array2[1] ^ array2[3];
			SecT239Field.ImplMulwAcc(num ^ num2, num3 ^ num4, zz, 3);
			ulong[] array3 = new ulong[3];
			SecT239Field.ImplMulwAcc(num, num3, array3, 0);
			SecT239Field.ImplMulwAcc(num2, num4, array3, 1);
			ulong num5 = array3[0];
			ulong num6 = array3[1];
			ulong num7 = array3[2];
			zz[2] = (zz[2] ^ num5);
			zz[3] = (zz[3] ^ (num5 ^ num6));
			zz[4] = (zz[4] ^ (num7 ^ num6));
			zz[5] = (zz[5] ^ num7);
			SecT239Field.ImplCompactExt(zz);
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x001019C4 File Offset: 0x001019C4
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
			num2 ^= (x & 585610922974906400UL & y << 4 >> 63) >> 5;
			z[zOff] ^= (num3 & 1152921504606846975UL);
			IntPtr intPtr;
			z[(int)(intPtr = (IntPtr)(zOff + 1))] = (z[(int)intPtr] ^ (num3 >> 60 ^ num2 << 4));
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x00101AB4 File Offset: 0x00101AB4
		protected static void ImplSquare(ulong[] x, ulong[] zz)
		{
			Interleave.Expand64To128(x[0], zz, 0);
			Interleave.Expand64To128(x[1], zz, 2);
			Interleave.Expand64To128(x[2], zz, 4);
			Interleave.Expand64To128(x[3], zz, 6);
		}

		// Token: 0x04001C47 RID: 7239
		private const ulong M47 = 140737488355327UL;

		// Token: 0x04001C48 RID: 7240
		private const ulong M60 = 1152921504606846975UL;
	}
}
