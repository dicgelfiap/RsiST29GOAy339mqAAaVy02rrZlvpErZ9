using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005AB RID: 1451
	internal class SecP521R1Field
	{
		// Token: 0x06002EC3 RID: 11971 RVA: 0x000F5B30 File Offset: 0x000F5B30
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			uint num = Nat.Add(16, x, y, z) + x[16] + y[16];
			if (num > 511U || (num == 511U && Nat.Eq(16, z, SecP521R1Field.P)))
			{
				num += Nat.Inc(16, z);
				num &= 511U;
			}
			z[16] = num;
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000F5B98 File Offset: 0x000F5B98
		public static void AddOne(uint[] x, uint[] z)
		{
			uint num = Nat.Inc(16, x, z) + x[16];
			if (num > 511U || (num == 511U && Nat.Eq(16, z, SecP521R1Field.P)))
			{
				num += Nat.Inc(16, z);
				num &= 511U;
			}
			z[16] = num;
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000F5BF8 File Offset: 0x000F5BF8
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat.FromBigInteger(521, x);
			if (Nat.Eq(17, array, SecP521R1Field.P))
			{
				Nat.Zero(17, array);
			}
			return array;
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000F5C30 File Offset: 0x000F5C30
		public static void Half(uint[] x, uint[] z)
		{
			uint num = x[16];
			uint num2 = Nat.ShiftDownBit(16, x, num, z);
			z[16] = (num >> 1 | num2 >> 23);
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000F5C60 File Offset: 0x000F5C60
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat.Create(33);
			SecP521R1Field.ImplMultiply(x, y, array);
			SecP521R1Field.Reduce(array, z);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000F5C88 File Offset: 0x000F5C88
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat.IsZero(17, x))
			{
				Nat.Zero(17, z);
				return;
			}
			Nat.Sub(17, SecP521R1Field.P, x, z);
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000F5CB0 File Offset: 0x000F5CB0
		public static void Reduce(uint[] xx, uint[] z)
		{
			uint num = xx[32];
			uint num2 = Nat.ShiftDownBits(16, xx, 16, 9, num, z, 0) >> 23;
			num2 += num >> 9;
			num2 += Nat.AddTo(16, xx, z);
			if (num2 > 511U || (num2 == 511U && Nat.Eq(16, z, SecP521R1Field.P)))
			{
				num2 += Nat.Inc(16, z);
				num2 &= 511U;
			}
			z[16] = num2;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000F5D2C File Offset: 0x000F5D2C
		public static void Reduce23(uint[] z)
		{
			uint num = z[16];
			uint num2 = Nat.AddWordTo(16, num >> 9, z) + (num & 511U);
			if (num2 > 511U || (num2 == 511U && Nat.Eq(16, z, SecP521R1Field.P)))
			{
				num2 += Nat.Inc(16, z);
				num2 &= 511U;
			}
			z[16] = num2;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000F5D98 File Offset: 0x000F5D98
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat.Create(33);
			SecP521R1Field.ImplSquare(x, array);
			SecP521R1Field.Reduce(array, z);
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000F5DC0 File Offset: 0x000F5DC0
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat.Create(33);
			SecP521R1Field.ImplSquare(x, array);
			SecP521R1Field.Reduce(array, z);
			while (--n > 0)
			{
				SecP521R1Field.ImplSquare(z, array);
				SecP521R1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000F5E04 File Offset: 0x000F5E04
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat.Sub(16, x, y, z) + (int)(x[16] - y[16]);
			if (num < 0)
			{
				num += Nat.Dec(16, z);
				num &= 511;
			}
			z[16] = (uint)num;
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000F5E4C File Offset: 0x000F5E4C
		public static void Twice(uint[] x, uint[] z)
		{
			uint num = x[16];
			uint num2 = Nat.ShiftUpBit(16, x, num << 23, z) | num << 1;
			z[16] = (num2 & 511U);
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000F5E80 File Offset: 0x000F5E80
		protected static void ImplMultiply(uint[] x, uint[] y, uint[] zz)
		{
			Nat512.Mul(x, y, zz);
			uint num = x[16];
			uint num2 = y[16];
			zz[32] = Nat.Mul31BothAdd(16, num, y, num2, x, zz, 16) + num * num2;
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000F5EBC File Offset: 0x000F5EBC
		protected static void ImplSquare(uint[] x, uint[] zz)
		{
			Nat512.Square(x, zz);
			uint num = x[16];
			zz[32] = Nat.MulWordAddTo(16, num << 1, x, 0, zz, 16) + num * num;
		}

		// Token: 0x04001C05 RID: 7173
		private const int P16 = 511;

		// Token: 0x04001C06 RID: 7174
		internal static readonly uint[] P = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			511U
		};
	}
}
