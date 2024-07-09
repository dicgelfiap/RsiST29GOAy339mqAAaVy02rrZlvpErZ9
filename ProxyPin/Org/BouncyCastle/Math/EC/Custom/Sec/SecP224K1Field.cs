using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000597 RID: 1431
	internal class SecP224K1Field
	{
		// Token: 0x06002D8D RID: 11661 RVA: 0x000EFF1C File Offset: 0x000EFF1C
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat224.Add(x, y, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224K1Field.P)))
			{
				Nat.Add33To(7, 6803U, z);
			}
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000EFF64 File Offset: 0x000EFF64
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(14, xx, yy, zz) != 0U || (zz[13] == 4294967295U && Nat.Gte(14, zz, SecP224K1Field.PExt))) && Nat.AddTo(SecP224K1Field.PExtInv.Length, SecP224K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(14, zz, SecP224K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000EFFCC File Offset: 0x000EFFCC
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(7, x, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224K1Field.P)))
			{
				Nat.Add33To(7, 6803U, z);
			}
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000F0014 File Offset: 0x000F0014
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat224.FromBigInteger(x);
			if (array[6] == 4294967295U && Nat224.Gte(array, SecP224K1Field.P))
			{
				Nat224.SubFrom(SecP224K1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000F0054 File Offset: 0x000F0054
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(7, x, 0U, z);
				return;
			}
			uint c = Nat224.Add(x, SecP224K1Field.P, z);
			Nat.ShiftDownBit(7, z, c);
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000F0090 File Offset: 0x000F0090
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Mul(x, y, array);
			SecP224K1Field.Reduce(array, z);
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000F00B8 File Offset: 0x000F00B8
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat224.MulAddTo(x, y, zz) != 0U || (zz[13] == 4294967295U && Nat.Gte(14, zz, SecP224K1Field.PExt))) && Nat.AddTo(SecP224K1Field.PExtInv.Length, SecP224K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(14, zz, SecP224K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000F011C File Offset: 0x000F011C
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat224.IsZero(x))
			{
				Nat224.Zero(z);
				return;
			}
			Nat224.Sub(SecP224K1Field.P, x, z);
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000F0140 File Offset: 0x000F0140
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong y = Nat224.Mul33Add(6803U, xx, 7, xx, 0, z, 0);
			if (Nat224.Mul33DWordAdd(6803U, y, z, 0) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224K1Field.P)))
			{
				Nat.Add33To(7, 6803U, z);
			}
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000F019C File Offset: 0x000F019C
		public static void Reduce32(uint x, uint[] z)
		{
			if ((x != 0U && Nat224.Mul33WordAdd(6803U, x, z, 0) != 0U) || (z[6] == 4294967295U && Nat224.Gte(z, SecP224K1Field.P)))
			{
				Nat.Add33To(7, 6803U, z);
			}
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000F01DC File Offset: 0x000F01DC
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Square(x, array);
			SecP224K1Field.Reduce(array, z);
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000F0204 File Offset: 0x000F0204
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Square(x, array);
			SecP224K1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat224.Square(z, array);
				SecP224K1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000F0248 File Offset: 0x000F0248
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat224.Sub(x, y, z);
			if (num != 0)
			{
				Nat.Sub33From(7, 6803U, z);
			}
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000F0278 File Offset: 0x000F0278
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			int num = Nat.Sub(14, xx, yy, zz);
			if (num != 0 && Nat.SubFrom(SecP224K1Field.PExtInv.Length, SecP224K1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(14, zz, SecP224K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000F02C4 File Offset: 0x000F02C4
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(7, x, 0U, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224K1Field.P)))
			{
				Nat.Add33To(7, 6803U, z);
			}
		}

		// Token: 0x04001BC7 RID: 7111
		private const uint P6 = 4294967295U;

		// Token: 0x04001BC8 RID: 7112
		private const uint PExt13 = 4294967295U;

		// Token: 0x04001BC9 RID: 7113
		private const uint PInv33 = 6803U;

		// Token: 0x04001BCA RID: 7114
		internal static readonly uint[] P = new uint[]
		{
			4294960493U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BCB RID: 7115
		internal static readonly uint[] PExt = new uint[]
		{
			46280809U,
			13606U,
			1U,
			0U,
			0U,
			0U,
			0U,
			4294953690U,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BCC RID: 7116
		private static readonly uint[] PExtInv = new uint[]
		{
			4248686487U,
			4294953689U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			13605U,
			2U
		};
	}
}
