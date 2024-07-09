using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200058B RID: 1419
	internal class SecP160R2Field
	{
		// Token: 0x06002CD7 RID: 11479 RVA: 0x000ECBE8 File Offset: 0x000ECBE8
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat160.Add(x, y, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R2Field.P)))
			{
				Nat.Add33To(5, 21389U, z);
			}
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000ECC30 File Offset: 0x000ECC30
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(10, xx, yy, zz) != 0U || (zz[9] == 4294967295U && Nat.Gte(10, zz, SecP160R2Field.PExt))) && Nat.AddTo(SecP160R2Field.PExtInv.Length, SecP160R2Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(10, zz, SecP160R2Field.PExtInv.Length);
			}
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000ECC98 File Offset: 0x000ECC98
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(5, x, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R2Field.P)))
			{
				Nat.Add33To(5, 21389U, z);
			}
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000ECCE0 File Offset: 0x000ECCE0
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat160.FromBigInteger(x);
			if (array[4] == 4294967295U && Nat160.Gte(array, SecP160R2Field.P))
			{
				Nat160.SubFrom(SecP160R2Field.P, array);
			}
			return array;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000ECD20 File Offset: 0x000ECD20
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(5, x, 0U, z);
				return;
			}
			uint c = Nat160.Add(x, SecP160R2Field.P, z);
			Nat.ShiftDownBit(5, z, c);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000ECD5C File Offset: 0x000ECD5C
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Mul(x, y, array);
			SecP160R2Field.Reduce(array, z);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000ECD84 File Offset: 0x000ECD84
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat160.MulAddTo(x, y, zz) != 0U || (zz[9] == 4294967295U && Nat.Gte(10, zz, SecP160R2Field.PExt))) && Nat.AddTo(SecP160R2Field.PExtInv.Length, SecP160R2Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(10, zz, SecP160R2Field.PExtInv.Length);
			}
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000ECDE8 File Offset: 0x000ECDE8
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat160.IsZero(x))
			{
				Nat160.Zero(z);
				return;
			}
			Nat160.Sub(SecP160R2Field.P, x, z);
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000ECE0C File Offset: 0x000ECE0C
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong y = Nat160.Mul33Add(21389U, xx, 5, xx, 0, z, 0);
			if (Nat160.Mul33DWordAdd(21389U, y, z, 0) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R2Field.P)))
			{
				Nat.Add33To(5, 21389U, z);
			}
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000ECE68 File Offset: 0x000ECE68
		public static void Reduce32(uint x, uint[] z)
		{
			if ((x != 0U && Nat160.Mul33WordAdd(21389U, x, z, 0) != 0U) || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R2Field.P)))
			{
				Nat.Add33To(5, 21389U, z);
			}
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000ECEA8 File Offset: 0x000ECEA8
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Square(x, array);
			SecP160R2Field.Reduce(array, z);
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000ECED0 File Offset: 0x000ECED0
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Square(x, array);
			SecP160R2Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat160.Square(z, array);
				SecP160R2Field.Reduce(array, z);
			}
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000ECF14 File Offset: 0x000ECF14
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat160.Sub(x, y, z);
			if (num != 0)
			{
				Nat.Sub33From(5, 21389U, z);
			}
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000ECF44 File Offset: 0x000ECF44
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			int num = Nat.Sub(10, xx, yy, zz);
			if (num != 0 && Nat.SubFrom(SecP160R2Field.PExtInv.Length, SecP160R2Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(10, zz, SecP160R2Field.PExtInv.Length);
			}
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000ECF90 File Offset: 0x000ECF90
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(5, x, 0U, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R2Field.P)))
			{
				Nat.Add33To(5, 21389U, z);
			}
		}

		// Token: 0x04001BA1 RID: 7073
		private const uint P4 = 4294967295U;

		// Token: 0x04001BA2 RID: 7074
		private const uint PExt9 = 4294967295U;

		// Token: 0x04001BA3 RID: 7075
		private const uint PInv33 = 21389U;

		// Token: 0x04001BA4 RID: 7076
		internal static readonly uint[] P = new uint[]
		{
			4294945907U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BA5 RID: 7077
		internal static readonly uint[] PExt = new uint[]
		{
			457489321U,
			42778U,
			1U,
			0U,
			0U,
			4294924518U,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BA6 RID: 7078
		private static readonly uint[] PExtInv = new uint[]
		{
			3837477975U,
			4294924517U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			42777U,
			2U
		};
	}
}
