using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000587 RID: 1415
	internal class SecP160R1Field
	{
		// Token: 0x06002C9B RID: 11419 RVA: 0x000EBB34 File Offset: 0x000EBB34
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat160.Add(x, y, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R1Field.P)))
			{
				Nat.AddWordTo(5, 2147483649U, z);
			}
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000EBB7C File Offset: 0x000EBB7C
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(10, xx, yy, zz) != 0U || (zz[9] == 4294967295U && Nat.Gte(10, zz, SecP160R1Field.PExt))) && Nat.AddTo(SecP160R1Field.PExtInv.Length, SecP160R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(10, zz, SecP160R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000EBBE4 File Offset: 0x000EBBE4
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(5, x, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R1Field.P)))
			{
				Nat.AddWordTo(5, 2147483649U, z);
			}
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000EBC2C File Offset: 0x000EBC2C
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat160.FromBigInteger(x);
			if (array[4] == 4294967295U && Nat160.Gte(array, SecP160R1Field.P))
			{
				Nat160.SubFrom(SecP160R1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000EBC6C File Offset: 0x000EBC6C
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(5, x, 0U, z);
				return;
			}
			uint c = Nat160.Add(x, SecP160R1Field.P, z);
			Nat.ShiftDownBit(5, z, c);
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000EBCA8 File Offset: 0x000EBCA8
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Mul(x, y, array);
			SecP160R1Field.Reduce(array, z);
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000EBCD0 File Offset: 0x000EBCD0
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat160.MulAddTo(x, y, zz) != 0U || (zz[9] == 4294967295U && Nat.Gte(10, zz, SecP160R1Field.PExt))) && Nat.AddTo(SecP160R1Field.PExtInv.Length, SecP160R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(10, zz, SecP160R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000EBD34 File Offset: 0x000EBD34
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat160.IsZero(x))
			{
				Nat160.Zero(z);
				return;
			}
			Nat160.Sub(SecP160R1Field.P, x, z);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000EBD58 File Offset: 0x000EBD58
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong num = (ulong)xx[5];
			ulong num2 = (ulong)xx[6];
			ulong num3 = (ulong)xx[7];
			ulong num4 = (ulong)xx[8];
			ulong num5 = (ulong)xx[9];
			ulong num6 = 0UL;
			num6 += (ulong)xx[0] + num + (num << 31);
			z[0] = (uint)num6;
			num6 >>= 32;
			num6 += (ulong)xx[1] + num2 + (num2 << 31);
			z[1] = (uint)num6;
			num6 >>= 32;
			num6 += (ulong)xx[2] + num3 + (num3 << 31);
			z[2] = (uint)num6;
			num6 >>= 32;
			num6 += (ulong)xx[3] + num4 + (num4 << 31);
			z[3] = (uint)num6;
			num6 >>= 32;
			num6 += (ulong)xx[4] + num5 + (num5 << 31);
			z[4] = (uint)num6;
			num6 >>= 32;
			SecP160R1Field.Reduce32((uint)num6, z);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000EBE24 File Offset: 0x000EBE24
		public static void Reduce32(uint x, uint[] z)
		{
			if ((x != 0U && Nat160.MulWordsAdd(2147483649U, x, z, 0) != 0U) || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R1Field.P)))
			{
				Nat.AddWordTo(5, 2147483649U, z);
			}
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000EBE64 File Offset: 0x000EBE64
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Square(x, array);
			SecP160R1Field.Reduce(array, z);
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000EBE8C File Offset: 0x000EBE8C
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Square(x, array);
			SecP160R1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat160.Square(z, array);
				SecP160R1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000EBED0 File Offset: 0x000EBED0
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat160.Sub(x, y, z);
			if (num != 0)
			{
				Nat.SubWordFrom(5, 2147483649U, z);
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000EBF00 File Offset: 0x000EBF00
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			int num = Nat.Sub(10, xx, yy, zz);
			if (num != 0 && Nat.SubFrom(SecP160R1Field.PExtInv.Length, SecP160R1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(10, zz, SecP160R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000EBF4C File Offset: 0x000EBF4C
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(5, x, 0U, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R1Field.P)))
			{
				Nat.AddWordTo(5, 2147483649U, z);
			}
		}

		// Token: 0x04001B94 RID: 7060
		private const uint P4 = 4294967295U;

		// Token: 0x04001B95 RID: 7061
		private const uint PExt9 = 4294967295U;

		// Token: 0x04001B96 RID: 7062
		private const uint PInv = 2147483649U;

		// Token: 0x04001B97 RID: 7063
		internal static readonly uint[] P = new uint[]
		{
			2147483647U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001B98 RID: 7064
		internal static readonly uint[] PExt = new uint[]
		{
			1U,
			1073741825U,
			0U,
			0U,
			0U,
			4294967294U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001B99 RID: 7065
		private static readonly uint[] PExtInv = new uint[]
		{
			uint.MaxValue,
			3221225470U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			1U,
			1U
		};
	}
}
