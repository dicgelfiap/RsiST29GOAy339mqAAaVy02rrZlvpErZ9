using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Djb
{
	// Token: 0x02000573 RID: 1395
	internal class Curve25519Field
	{
		// Token: 0x06002B71 RID: 11121 RVA: 0x000E7100 File Offset: 0x000E7100
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			Nat256.Add(x, y, z);
			if (Nat256.Gte(z, Curve25519Field.P))
			{
				Curve25519Field.SubPFrom(z);
			}
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000E7124 File Offset: 0x000E7124
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			Nat.Add(16, xx, yy, zz);
			if (Nat.Gte(16, zz, Curve25519Field.PExt))
			{
				Curve25519Field.SubPExtFrom(zz);
			}
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000E714C File Offset: 0x000E714C
		public static void AddOne(uint[] x, uint[] z)
		{
			Nat.Inc(8, x, z);
			if (Nat256.Gte(z, Curve25519Field.P))
			{
				Curve25519Field.SubPFrom(z);
			}
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000E7170 File Offset: 0x000E7170
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat256.FromBigInteger(x);
			while (Nat256.Gte(array, Curve25519Field.P))
			{
				Nat256.SubFrom(Curve25519Field.P, array);
			}
			return array;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000E71A8 File Offset: 0x000E71A8
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(8, x, 0U, z);
				return;
			}
			Nat256.Add(x, Curve25519Field.P, z);
			Nat.ShiftDownBit(8, z, 0U);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000E71D8 File Offset: 0x000E71D8
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Mul(x, y, array);
			Curve25519Field.Reduce(array, z);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000E7200 File Offset: 0x000E7200
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			Nat256.MulAddTo(x, y, zz);
			if (Nat.Gte(16, zz, Curve25519Field.PExt))
			{
				Curve25519Field.SubPExtFrom(zz);
			}
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000E7224 File Offset: 0x000E7224
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat256.IsZero(x))
			{
				Nat256.Zero(z);
				return;
			}
			Nat256.Sub(Curve25519Field.P, x, z);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000E7248 File Offset: 0x000E7248
		public static void Reduce(uint[] xx, uint[] z)
		{
			uint num = xx[7];
			Nat.ShiftUpBit(8, xx, 8, num, z, 0);
			uint num2 = Nat256.MulByWordAddTo(19U, xx, z) << 1;
			uint num3 = z[7];
			num2 += (num3 >> 31) - (num >> 31);
			num3 &= 2147483647U;
			num3 += Nat.AddWordTo(7, num2 * 19U, z);
			z[7] = num3;
			if (num3 >= 2147483647U && Nat256.Gte(z, Curve25519Field.P))
			{
				Curve25519Field.SubPFrom(z);
			}
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000E72C4 File Offset: 0x000E72C4
		public static void Reduce27(uint x, uint[] z)
		{
			uint num = z[7];
			uint num2 = x << 1 | num >> 31;
			num &= 2147483647U;
			num += Nat.AddWordTo(7, num2 * 19U, z);
			z[7] = num;
			if (num >= 2147483647U && Nat256.Gte(z, Curve25519Field.P))
			{
				Curve25519Field.SubPFrom(z);
			}
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000E7320 File Offset: 0x000E7320
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			Curve25519Field.Reduce(array, z);
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000E7348 File Offset: 0x000E7348
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			Curve25519Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat256.Square(z, array);
				Curve25519Field.Reduce(array, z);
			}
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000E738C File Offset: 0x000E738C
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat256.Sub(x, y, z);
			if (num != 0)
			{
				Curve25519Field.AddPTo(z);
			}
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000E73B4 File Offset: 0x000E73B4
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			int num = Nat.Sub(16, xx, yy, zz);
			if (num != 0)
			{
				Curve25519Field.AddPExtTo(zz);
			}
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000E73E0 File Offset: 0x000E73E0
		public static void Twice(uint[] x, uint[] z)
		{
			Nat.ShiftUpBit(8, x, 0U, z);
			if (Nat256.Gte(z, Curve25519Field.P))
			{
				Curve25519Field.SubPFrom(z);
			}
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000E7404 File Offset: 0x000E7404
		private static uint AddPTo(uint[] z)
		{
			long num = (long)((ulong)z[0] - 19UL);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)Nat.DecAt(7, z, 1);
			}
			num += (long)((ulong)z[7] + (ulong)int.MinValue);
			z[7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000E7454 File Offset: 0x000E7454
		private static uint AddPExtTo(uint[] zz)
		{
			long num = (long)((ulong)zz[0] + (ulong)Curve25519Field.PExt[0]);
			zz[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)((ulong)Nat.IncAt(8, zz, 1));
			}
			num += (long)((ulong)zz[8] - 19UL);
			zz[8] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)Nat.DecAt(15, zz, 9);
			}
			num += (long)((ulong)zz[15] + (ulong)(Curve25519Field.PExt[15] + 1U));
			zz[15] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000E74DC File Offset: 0x000E74DC
		private static int SubPFrom(uint[] z)
		{
			long num = (long)((ulong)z[0] + 19UL);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)((ulong)Nat.IncAt(7, z, 1));
			}
			num += (long)((ulong)z[7] - (ulong)int.MinValue);
			z[7] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000E752C File Offset: 0x000E752C
		private static int SubPExtFrom(uint[] zz)
		{
			long num = (long)((ulong)zz[0] - (ulong)Curve25519Field.PExt[0]);
			zz[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)Nat.DecAt(8, zz, 1);
			}
			num += (long)((ulong)zz[8] + 19UL);
			zz[8] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)((ulong)Nat.IncAt(15, zz, 9));
			}
			num += (long)((ulong)zz[15] - (ulong)(Curve25519Field.PExt[15] + 1U));
			zz[15] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x04001B65 RID: 7013
		private const uint P7 = 2147483647U;

		// Token: 0x04001B66 RID: 7014
		private const uint PInv = 19U;

		// Token: 0x04001B67 RID: 7015
		internal static readonly uint[] P = new uint[]
		{
			4294967277U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			2147483647U
		};

		// Token: 0x04001B68 RID: 7016
		private static readonly uint[] PExt = new uint[]
		{
			361U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			4294967277U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			1073741823U
		};
	}
}
