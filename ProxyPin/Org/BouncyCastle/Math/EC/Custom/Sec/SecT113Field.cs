using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005AE RID: 1454
	internal class SecT113Field
	{
		// Token: 0x06002EF3 RID: 12019 RVA: 0x000F68EC File Offset: 0x000F68EC
		public static void Add(ulong[] x, ulong[] y, ulong[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000F6904 File Offset: 0x000F6904
		public static void AddExt(ulong[] xx, ulong[] yy, ulong[] zz)
		{
			zz[0] = (xx[0] ^ yy[0]);
			zz[1] = (xx[1] ^ yy[1]);
			zz[2] = (xx[2] ^ yy[2]);
			zz[3] = (xx[3] ^ yy[3]);
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000F6930 File Offset: 0x000F6930
		public static void AddOne(ulong[] x, ulong[] z)
		{
			z[0] = (x[0] ^ 1UL);
			z[1] = x[1];
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000F6944 File Offset: 0x000F6944
		private static void AddTo(ulong[] x, ulong[] z)
		{
			z[0] = (z[0] ^ x[0]);
			z[1] = (z[1] ^ x[1]);
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000F6970 File Offset: 0x000F6970
		public static ulong[] FromBigInteger(BigInteger x)
		{
			return Nat.FromBigInteger64(113, x);
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000F697C File Offset: 0x000F697C
		public static void HalfTrace(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat128.CreateExt64();
			Nat128.Copy64(x, z);
			for (int i = 1; i < 113; i += 2)
			{
				SecT113Field.ImplSquare(z, array);
				SecT113Field.Reduce(array, z);
				SecT113Field.ImplSquare(z, array);
				SecT113Field.Reduce(array, z);
				SecT113Field.AddTo(x, z);
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000F69D0 File Offset: 0x000F69D0
		public static void Invert(ulong[] x, ulong[] z)
		{
			if (Nat128.IsZero64(x))
			{
				throw new InvalidOperationException();
			}
			ulong[] array = Nat128.Create64();
			ulong[] array2 = Nat128.Create64();
			SecT113Field.Square(x, array);
			SecT113Field.Multiply(array, x, array);
			SecT113Field.Square(array, array);
			SecT113Field.Multiply(array, x, array);
			SecT113Field.SquareN(array, 3, array2);
			SecT113Field.Multiply(array2, array, array2);
			SecT113Field.Square(array2, array2);
			SecT113Field.Multiply(array2, x, array2);
			SecT113Field.SquareN(array2, 7, array);
			SecT113Field.Multiply(array, array2, array);
			SecT113Field.SquareN(array, 14, array2);
			SecT113Field.Multiply(array2, array, array2);
			SecT113Field.SquareN(array2, 28, array);
			SecT113Field.Multiply(array, array2, array);
			SecT113Field.SquareN(array, 56, array2);
			SecT113Field.Multiply(array2, array, array2);
			SecT113Field.Square(array2, z);
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000F6A88 File Offset: 0x000F6A88
		public static void Multiply(ulong[] x, ulong[] y, ulong[] z)
		{
			ulong[] array = Nat128.CreateExt64();
			SecT113Field.ImplMultiply(x, y, array);
			SecT113Field.Reduce(array, z);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000F6AB0 File Offset: 0x000F6AB0
		public static void MultiplyAddToExt(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = Nat128.CreateExt64();
			SecT113Field.ImplMultiply(x, y, array);
			SecT113Field.AddExt(zz, array, zz);
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000F6AD8 File Offset: 0x000F6AD8
		public static void Reduce(ulong[] xx, ulong[] z)
		{
			ulong num = xx[0];
			ulong num2 = xx[1];
			ulong num3 = xx[2];
			ulong num4 = xx[3];
			num2 ^= (num4 << 15 ^ num4 << 24);
			num3 ^= (num4 >> 49 ^ num4 >> 40);
			num ^= (num3 << 15 ^ num3 << 24);
			num2 ^= (num3 >> 49 ^ num3 >> 40);
			ulong num5 = num2 >> 49;
			z[0] = (num ^ num5 ^ num5 << 9);
			z[1] = (num2 & 562949953421311UL);
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000F6B4C File Offset: 0x000F6B4C
		public static void Reduce15(ulong[] z, int zOff)
		{
			ulong num = z[zOff + 1];
			ulong num2 = num >> 49;
			z[zOff] ^= (num2 ^ num2 << 9);
			z[zOff + 1] = (num & 562949953421311UL);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000F6B8C File Offset: 0x000F6B8C
		public static void Sqrt(ulong[] x, ulong[] z)
		{
			ulong num = Interleave.Unshuffle(x[0]);
			ulong num2 = Interleave.Unshuffle(x[1]);
			ulong num3 = (num & (ulong)-1) | num2 << 32;
			ulong num4 = num >> 32 | (num2 & 18446744069414584320UL);
			z[0] = (num3 ^ num4 << 57 ^ num4 << 5);
			z[1] = (num4 >> 7 ^ num4 >> 59);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000F6BE4 File Offset: 0x000F6BE4
		public static void Square(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat128.CreateExt64();
			SecT113Field.ImplSquare(x, array);
			SecT113Field.Reduce(array, z);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000F6C0C File Offset: 0x000F6C0C
		public static void SquareAddToExt(ulong[] x, ulong[] zz)
		{
			ulong[] array = Nat128.CreateExt64();
			SecT113Field.ImplSquare(x, array);
			SecT113Field.AddExt(zz, array, zz);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000F6C34 File Offset: 0x000F6C34
		public static void SquareN(ulong[] x, int n, ulong[] z)
		{
			ulong[] array = Nat128.CreateExt64();
			SecT113Field.ImplSquare(x, array);
			SecT113Field.Reduce(array, z);
			while (--n > 0)
			{
				SecT113Field.ImplSquare(z, array);
				SecT113Field.Reduce(array, z);
			}
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000F6C78 File Offset: 0x000F6C78
		public static uint Trace(ulong[] x)
		{
			return (uint)x[0] & 1U;
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000F6C80 File Offset: 0x000F6C80
		protected static void ImplMultiply(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong num = x[0];
			ulong num2 = x[1];
			num2 = ((num >> 57 ^ num2 << 7) & 144115188075855871UL);
			num &= 144115188075855871UL;
			ulong num3 = y[0];
			ulong num4 = y[1];
			num4 = ((num3 >> 57 ^ num4 << 7) & 144115188075855871UL);
			num3 &= 144115188075855871UL;
			ulong[] array = new ulong[6];
			SecT113Field.ImplMulw(num, num3, array, 0);
			SecT113Field.ImplMulw(num2, num4, array, 2);
			SecT113Field.ImplMulw(num ^ num2, num3 ^ num4, array, 4);
			ulong num5 = array[1] ^ array[2];
			ulong num6 = array[0];
			ulong num7 = array[3];
			ulong num8 = array[4] ^ num6 ^ num5;
			ulong num9 = array[5] ^ num7 ^ num5;
			zz[0] = (num6 ^ num8 << 57);
			zz[1] = (num8 >> 7 ^ num9 << 50);
			zz[2] = (num9 >> 14 ^ num7 << 43);
			zz[3] = num7 >> 21;
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000F6D68 File Offset: 0x000F6D68
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

		// Token: 0x06002F05 RID: 12037 RVA: 0x000F6E40 File Offset: 0x000F6E40
		protected static void ImplSquare(ulong[] x, ulong[] zz)
		{
			Interleave.Expand64To128(x[0], zz, 0);
			Interleave.Expand64To128(x[1], zz, 2);
		}

		// Token: 0x04001C09 RID: 7177
		private const ulong M49 = 562949953421311UL;

		// Token: 0x04001C0A RID: 7178
		private const ulong M57 = 144115188075855871UL;
	}
}
