using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200059F RID: 1439
	internal class SecP256K1Field
	{
		// Token: 0x06002E0C RID: 11788 RVA: 0x000F2430 File Offset: 0x000F2430
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat256.Add(x, y, z) != 0U || (z[7] == 4294967295U && Nat256.Gte(z, SecP256K1Field.P)))
			{
				Nat.Add33To(8, 977U, z);
			}
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000F2478 File Offset: 0x000F2478
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(16, xx, yy, zz) != 0U || (zz[15] == 4294967295U && Nat.Gte(16, zz, SecP256K1Field.PExt))) && Nat.AddTo(SecP256K1Field.PExtInv.Length, SecP256K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(16, zz, SecP256K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000F24E0 File Offset: 0x000F24E0
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(8, x, z) != 0U || (z[7] == 4294967295U && Nat256.Gte(z, SecP256K1Field.P)))
			{
				Nat.Add33To(8, 977U, z);
			}
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000F2528 File Offset: 0x000F2528
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat256.FromBigInteger(x);
			if (array[7] == 4294967295U && Nat256.Gte(array, SecP256K1Field.P))
			{
				Nat256.SubFrom(SecP256K1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000F2568 File Offset: 0x000F2568
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(8, x, 0U, z);
				return;
			}
			uint c = Nat256.Add(x, SecP256K1Field.P, z);
			Nat.ShiftDownBit(8, z, c);
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000F25A4 File Offset: 0x000F25A4
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Mul(x, y, array);
			SecP256K1Field.Reduce(array, z);
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x000F25CC File Offset: 0x000F25CC
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat256.MulAddTo(x, y, zz) != 0U || (zz[15] == 4294967295U && Nat.Gte(16, zz, SecP256K1Field.PExt))) && Nat.AddTo(SecP256K1Field.PExtInv.Length, SecP256K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(16, zz, SecP256K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000F2630 File Offset: 0x000F2630
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat256.IsZero(x))
			{
				Nat256.Zero(z);
				return;
			}
			Nat256.Sub(SecP256K1Field.P, x, z);
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000F2654 File Offset: 0x000F2654
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong y = Nat256.Mul33Add(977U, xx, 8, xx, 0, z, 0);
			if (Nat256.Mul33DWordAdd(977U, y, z, 0) != 0U || (z[7] == 4294967295U && Nat256.Gte(z, SecP256K1Field.P)))
			{
				Nat.Add33To(8, 977U, z);
			}
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000F26B0 File Offset: 0x000F26B0
		public static void Reduce32(uint x, uint[] z)
		{
			if ((x != 0U && Nat256.Mul33WordAdd(977U, x, z, 0) != 0U) || (z[7] == 4294967295U && Nat256.Gte(z, SecP256K1Field.P)))
			{
				Nat.Add33To(8, 977U, z);
			}
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000F26F0 File Offset: 0x000F26F0
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			SecP256K1Field.Reduce(array, z);
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000F2718 File Offset: 0x000F2718
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			SecP256K1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat256.Square(z, array);
				SecP256K1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000F275C File Offset: 0x000F275C
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat256.Sub(x, y, z);
			if (num != 0)
			{
				Nat.Sub33From(8, 977U, z);
			}
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000F278C File Offset: 0x000F278C
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			int num = Nat.Sub(16, xx, yy, zz);
			if (num != 0 && Nat.SubFrom(SecP256K1Field.PExtInv.Length, SecP256K1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(16, zz, SecP256K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000F27D8 File Offset: 0x000F27D8
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(8, x, 0U, z) != 0U || (z[7] == 4294967295U && Nat256.Gte(z, SecP256K1Field.P)))
			{
				Nat.Add33To(8, 977U, z);
			}
		}

		// Token: 0x04001BE1 RID: 7137
		private const uint P7 = 4294967295U;

		// Token: 0x04001BE2 RID: 7138
		private const uint PExt15 = 4294967295U;

		// Token: 0x04001BE3 RID: 7139
		private const uint PInv33 = 977U;

		// Token: 0x04001BE4 RID: 7140
		internal static readonly uint[] P = new uint[]
		{
			4294966319U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BE5 RID: 7141
		internal static readonly uint[] PExt = new uint[]
		{
			954529U,
			1954U,
			1U,
			0U,
			0U,
			0U,
			0U,
			0U,
			4294965342U,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BE6 RID: 7142
		private static readonly uint[] PExtInv = new uint[]
		{
			4294012767U,
			4294965341U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			1953U,
			2U
		};
	}
}
