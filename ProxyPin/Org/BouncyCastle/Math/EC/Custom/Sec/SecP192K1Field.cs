using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200058F RID: 1423
	internal class SecP192K1Field
	{
		// Token: 0x06002D13 RID: 11539 RVA: 0x000EDCAC File Offset: 0x000EDCAC
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat192.Add(x, y, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192K1Field.P)))
			{
				Nat.Add33To(6, 4553U, z);
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000EDCF4 File Offset: 0x000EDCF4
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(12, xx, yy, zz) != 0U || (zz[11] == 4294967295U && Nat.Gte(12, zz, SecP192K1Field.PExt))) && Nat.AddTo(SecP192K1Field.PExtInv.Length, SecP192K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(12, zz, SecP192K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000EDD5C File Offset: 0x000EDD5C
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(6, x, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192K1Field.P)))
			{
				Nat.Add33To(6, 4553U, z);
			}
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000EDDA4 File Offset: 0x000EDDA4
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat192.FromBigInteger(x);
			if (array[5] == 4294967295U && Nat192.Gte(array, SecP192K1Field.P))
			{
				Nat192.SubFrom(SecP192K1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000EDDE4 File Offset: 0x000EDDE4
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(6, x, 0U, z);
				return;
			}
			uint c = Nat192.Add(x, SecP192K1Field.P, z);
			Nat.ShiftDownBit(6, z, c);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000EDE20 File Offset: 0x000EDE20
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Mul(x, y, array);
			SecP192K1Field.Reduce(array, z);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000EDE48 File Offset: 0x000EDE48
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat192.MulAddTo(x, y, zz) != 0U || (zz[11] == 4294967295U && Nat.Gte(12, zz, SecP192K1Field.PExt))) && Nat.AddTo(SecP192K1Field.PExtInv.Length, SecP192K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(12, zz, SecP192K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000EDEAC File Offset: 0x000EDEAC
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat192.IsZero(x))
			{
				Nat192.Zero(z);
				return;
			}
			Nat192.Sub(SecP192K1Field.P, x, z);
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000EDED0 File Offset: 0x000EDED0
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong y = Nat192.Mul33Add(4553U, xx, 6, xx, 0, z, 0);
			if (Nat192.Mul33DWordAdd(4553U, y, z, 0) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192K1Field.P)))
			{
				Nat.Add33To(6, 4553U, z);
			}
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000EDF2C File Offset: 0x000EDF2C
		public static void Reduce32(uint x, uint[] z)
		{
			if ((x != 0U && Nat192.Mul33WordAdd(4553U, x, z, 0) != 0U) || (z[5] == 4294967295U && Nat192.Gte(z, SecP192K1Field.P)))
			{
				Nat.Add33To(6, 4553U, z);
			}
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000EDF6C File Offset: 0x000EDF6C
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Square(x, array);
			SecP192K1Field.Reduce(array, z);
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000EDF94 File Offset: 0x000EDF94
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Square(x, array);
			SecP192K1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat192.Square(z, array);
				SecP192K1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000EDFD8 File Offset: 0x000EDFD8
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat192.Sub(x, y, z);
			if (num != 0)
			{
				Nat.Sub33From(6, 4553U, z);
			}
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000EE008 File Offset: 0x000EE008
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			int num = Nat.Sub(12, xx, yy, zz);
			if (num != 0 && Nat.SubFrom(SecP192K1Field.PExtInv.Length, SecP192K1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(12, zz, SecP192K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000EE054 File Offset: 0x000EE054
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(6, x, 0U, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192K1Field.P)))
			{
				Nat.Add33To(6, 4553U, z);
			}
		}

		// Token: 0x04001BAE RID: 7086
		private const uint P5 = 4294967295U;

		// Token: 0x04001BAF RID: 7087
		private const uint PExt11 = 4294967295U;

		// Token: 0x04001BB0 RID: 7088
		private const uint PInv33 = 4553U;

		// Token: 0x04001BB1 RID: 7089
		internal static readonly uint[] P = new uint[]
		{
			4294962743U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BB2 RID: 7090
		internal static readonly uint[] PExt = new uint[]
		{
			20729809U,
			9106U,
			1U,
			0U,
			0U,
			0U,
			4294958190U,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BB3 RID: 7091
		private static readonly uint[] PExtInv = new uint[]
		{
			4274237487U,
			4294958189U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			9105U,
			2U
		};
	}
}
