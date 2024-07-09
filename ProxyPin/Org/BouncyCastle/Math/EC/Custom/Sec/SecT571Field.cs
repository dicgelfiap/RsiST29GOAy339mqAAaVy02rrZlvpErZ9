using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005E1 RID: 1505
	internal class SecT571Field
	{
		// Token: 0x06003277 RID: 12919 RVA: 0x0010665C File Offset: 0x0010665C
		public static void Add(ulong[] x, ulong[] y, ulong[] z)
		{
			for (int i = 0; i < 9; i++)
			{
				z[i] = (x[i] ^ y[i]);
			}
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x00106688 File Offset: 0x00106688
		private static void Add(ulong[] x, int xOff, ulong[] y, int yOff, ulong[] z, int zOff)
		{
			for (int i = 0; i < 9; i++)
			{
				z[zOff + i] = (x[xOff + i] ^ y[yOff + i]);
			}
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x001066BC File Offset: 0x001066BC
		private static void AddBothTo(ulong[] x, int xOff, ulong[] y, int yOff, ulong[] z, int zOff)
		{
			for (int i = 0; i < 9; i++)
			{
				IntPtr intPtr;
				z[(int)(intPtr = (IntPtr)(zOff + i))] = (z[(int)intPtr] ^ (x[xOff + i] ^ y[yOff + i]));
			}
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x001066F8 File Offset: 0x001066F8
		public static void AddExt(ulong[] xx, ulong[] yy, ulong[] zz)
		{
			for (int i = 0; i < 18; i++)
			{
				zz[i] = (xx[i] ^ yy[i]);
			}
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x00106724 File Offset: 0x00106724
		public static void AddOne(ulong[] x, ulong[] z)
		{
			z[0] = (x[0] ^ 1UL);
			for (int i = 1; i < 9; i++)
			{
				z[i] = x[i];
			}
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x00106754 File Offset: 0x00106754
		private static void AddTo(ulong[] x, ulong[] z)
		{
			for (int i = 0; i < 9; i++)
			{
				IntPtr intPtr;
				z[(int)(intPtr = (IntPtr)i)] = (z[(int)intPtr] ^ x[i]);
			}
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x00106784 File Offset: 0x00106784
		public static ulong[] FromBigInteger(BigInteger x)
		{
			return Nat.FromBigInteger64(571, x);
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x00106794 File Offset: 0x00106794
		public static void HalfTrace(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat576.CreateExt64();
			Nat576.Copy64(x, z);
			for (int i = 1; i < 571; i += 2)
			{
				SecT571Field.ImplSquare(z, array);
				SecT571Field.Reduce(array, z);
				SecT571Field.ImplSquare(z, array);
				SecT571Field.Reduce(array, z);
				SecT571Field.AddTo(x, z);
			}
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x001067E8 File Offset: 0x001067E8
		public static void Invert(ulong[] x, ulong[] z)
		{
			if (Nat576.IsZero64(x))
			{
				throw new InvalidOperationException();
			}
			ulong[] array = Nat576.Create64();
			ulong[] array2 = Nat576.Create64();
			ulong[] array3 = Nat576.Create64();
			SecT571Field.Square(x, array3);
			SecT571Field.Square(array3, array);
			SecT571Field.Square(array, array2);
			SecT571Field.Multiply(array, array2, array);
			SecT571Field.SquareN(array, 2, array2);
			SecT571Field.Multiply(array, array2, array);
			SecT571Field.Multiply(array, array3, array);
			SecT571Field.SquareN(array, 5, array2);
			SecT571Field.Multiply(array, array2, array);
			SecT571Field.SquareN(array2, 5, array2);
			SecT571Field.Multiply(array, array2, array);
			SecT571Field.SquareN(array, 15, array2);
			SecT571Field.Multiply(array, array2, array3);
			SecT571Field.SquareN(array3, 30, array);
			SecT571Field.SquareN(array, 30, array2);
			SecT571Field.Multiply(array, array2, array);
			SecT571Field.SquareN(array, 60, array2);
			SecT571Field.Multiply(array, array2, array);
			SecT571Field.SquareN(array2, 60, array2);
			SecT571Field.Multiply(array, array2, array);
			SecT571Field.SquareN(array, 180, array2);
			SecT571Field.Multiply(array, array2, array);
			SecT571Field.SquareN(array2, 180, array2);
			SecT571Field.Multiply(array, array2, array);
			SecT571Field.Multiply(array, array3, z);
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x001068F0 File Offset: 0x001068F0
		public static void Multiply(ulong[] x, ulong[] y, ulong[] z)
		{
			ulong[] array = Nat576.CreateExt64();
			SecT571Field.ImplMultiply(x, y, array);
			SecT571Field.Reduce(array, z);
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x00106918 File Offset: 0x00106918
		public static void MultiplyAddToExt(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = Nat576.CreateExt64();
			SecT571Field.ImplMultiply(x, y, array);
			SecT571Field.AddExt(zz, array, zz);
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x00106940 File Offset: 0x00106940
		public static void Reduce(ulong[] xx, ulong[] z)
		{
			ulong num = xx[9];
			ulong num2 = xx[17];
			ulong num3 = num;
			num = (num3 ^ num2 >> 59 ^ num2 >> 57 ^ num2 >> 54 ^ num2 >> 49);
			num3 = (xx[8] ^ num2 << 5 ^ num2 << 7 ^ num2 << 10 ^ num2 << 15);
			for (int i = 16; i >= 10; i--)
			{
				num2 = xx[i];
				z[i - 8] = (num3 ^ num2 >> 59 ^ num2 >> 57 ^ num2 >> 54 ^ num2 >> 49);
				num3 = (xx[i - 9] ^ num2 << 5 ^ num2 << 7 ^ num2 << 10 ^ num2 << 15);
			}
			num2 = num;
			z[1] = (num3 ^ num2 >> 59 ^ num2 >> 57 ^ num2 >> 54 ^ num2 >> 49);
			num3 = (xx[0] ^ num2 << 5 ^ num2 << 7 ^ num2 << 10 ^ num2 << 15);
			ulong num4 = z[8];
			ulong num5 = num4 >> 59;
			z[0] = (num3 ^ num5 ^ num5 << 2 ^ num5 << 5 ^ num5 << 10);
			z[8] = (num4 & 576460752303423487UL);
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x00106A34 File Offset: 0x00106A34
		public static void Reduce5(ulong[] z, int zOff)
		{
			ulong num = z[zOff + 8];
			ulong num2 = num >> 59;
			z[zOff] ^= (num2 ^ num2 << 2 ^ num2 << 5 ^ num2 << 10);
			z[zOff + 8] = (num & 576460752303423487UL);
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x00106A7C File Offset: 0x00106A7C
		public static void Sqrt(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat576.Create64();
			ulong[] array2 = Nat576.Create64();
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				ulong num2 = Interleave.Unshuffle(x[num++]);
				ulong num3 = Interleave.Unshuffle(x[num++]);
				array[i] = ((num2 & (ulong)-1) | num3 << 32);
				array2[i] = (num2 >> 32 | (num3 & 18446744069414584320UL));
			}
			ulong num4 = Interleave.Unshuffle(x[num]);
			array[4] = (num4 & (ulong)-1);
			array2[4] = num4 >> 32;
			SecT571Field.Multiply(array2, SecT571Field.ROOT_Z, z);
			SecT571Field.Add(z, array, z);
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x00106B18 File Offset: 0x00106B18
		public static void Square(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat576.CreateExt64();
			SecT571Field.ImplSquare(x, array);
			SecT571Field.Reduce(array, z);
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x00106B40 File Offset: 0x00106B40
		public static void SquareAddToExt(ulong[] x, ulong[] zz)
		{
			ulong[] array = Nat576.CreateExt64();
			SecT571Field.ImplSquare(x, array);
			SecT571Field.AddExt(zz, array, zz);
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x00106B68 File Offset: 0x00106B68
		public static void SquareN(ulong[] x, int n, ulong[] z)
		{
			ulong[] array = Nat576.CreateExt64();
			SecT571Field.ImplSquare(x, array);
			SecT571Field.Reduce(array, z);
			while (--n > 0)
			{
				SecT571Field.ImplSquare(z, array);
				SecT571Field.Reduce(array, z);
			}
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x00106BAC File Offset: 0x00106BAC
		public static uint Trace(ulong[] x)
		{
			return (uint)(x[0] ^ x[8] >> 49 ^ x[8] >> 57) & 1U;
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x00106BC4 File Offset: 0x00106BC4
		protected static void ImplMultiply(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = new ulong[144];
			Array.Copy(y, 0, array, 9, 9);
			int num = 0;
			for (int i = 7; i > 0; i--)
			{
				num += 18;
				Nat.ShiftUpBit64(9, array, num >> 1, 0UL, array, num);
				SecT571Field.Reduce5(array, num);
				SecT571Field.Add(array, 9, array, num, array, num + 9);
			}
			ulong[] array2 = new ulong[array.Length];
			Nat.ShiftUpBits64(array.Length, array, 0, 4, 0UL, array2, 0);
			uint num2 = 15U;
			for (int j = 56; j >= 0; j -= 8)
			{
				for (int k = 1; k < 9; k += 2)
				{
					uint num3 = (uint)(x[k] >> j);
					uint num4 = num3 & num2;
					uint num5 = num3 >> 4 & num2;
					SecT571Field.AddBothTo(array, (int)(9U * num4), array2, (int)(9U * num5), zz, k - 1);
				}
				Nat.ShiftUpBits64(16, zz, 0, 8, 0UL);
			}
			for (int l = 56; l >= 0; l -= 8)
			{
				for (int m = 0; m < 9; m += 2)
				{
					uint num6 = (uint)(x[m] >> l);
					uint num7 = num6 & num2;
					uint num8 = num6 >> 4 & num2;
					SecT571Field.AddBothTo(array, (int)(9U * num7), array2, (int)(9U * num8), zz, m);
				}
				if (l > 0)
				{
					Nat.ShiftUpBits64(18, zz, 0, 8, 0UL);
				}
			}
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x00106D1C File Offset: 0x00106D1C
		protected static void ImplMulwAcc(ulong[] xs, ulong y, ulong[] z, int zOff)
		{
			ulong[] array = new ulong[32];
			array[1] = y;
			for (int i = 2; i < 32; i += 2)
			{
				array[i] = array[i >> 1] << 1;
				array[i + 1] = (array[i] ^ y);
			}
			ulong num = 0UL;
			IntPtr intPtr;
			for (int j = 0; j < 9; j++)
			{
				ulong num2 = xs[j];
				uint num3 = (uint)num2;
				num ^= array[(int)((UIntPtr)(num3 & 31U))];
				ulong num4 = 0UL;
				int num5 = 60;
				do
				{
					num3 = (uint)(num2 >> num5);
					ulong num6 = array[(int)((UIntPtr)(num3 & 31U))];
					num ^= num6 << num5;
					num4 ^= num6 >> -num5;
				}
				while ((num5 -= 5) > 0);
				for (int k = 0; k < 4; k++)
				{
					num2 = (num2 & 17256631552825064414UL) >> 1;
					num4 ^= (num2 & y << k >> 63);
				}
				z[(int)(intPtr = (IntPtr)(zOff + j))] = (z[(int)intPtr] ^ num);
				num = num4;
			}
			z[(int)(intPtr = (IntPtr)(zOff + 9))] = (z[(int)intPtr] ^ num);
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x00106E28 File Offset: 0x00106E28
		protected static void ImplSquare(ulong[] x, ulong[] zz)
		{
			Interleave.Expand64To128(x[0], zz, 0);
			Interleave.Expand64To128(x[1], zz, 2);
			Interleave.Expand64To128(x[2], zz, 4);
			Interleave.Expand64To128(x[3], zz, 6);
			Interleave.Expand64To128(x[4], zz, 8);
			Interleave.Expand64To128(x[5], zz, 10);
			Interleave.Expand64To128(x[6], zz, 12);
			Interleave.Expand64To128(x[7], zz, 14);
			Interleave.Expand64To128(x[8], zz, 16);
		}

		// Token: 0x04001C65 RID: 7269
		private const ulong M59 = 576460752303423487UL;

		// Token: 0x04001C66 RID: 7270
		private const ulong RM = 17256631552825064414UL;

		// Token: 0x04001C67 RID: 7271
		private static readonly ulong[] ROOT_Z = new ulong[]
		{
			3161836309350906777UL,
			10804290191530228771UL,
			14625517132619890193UL,
			7312758566309945096UL,
			17890083061325672324UL,
			8945041530681231562UL,
			13695892802195391589UL,
			6847946401097695794UL,
			541669439031730457UL
		};
	}
}
