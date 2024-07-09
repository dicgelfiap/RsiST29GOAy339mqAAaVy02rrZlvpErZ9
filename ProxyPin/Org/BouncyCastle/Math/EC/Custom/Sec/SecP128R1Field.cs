using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000581 RID: 1409
	internal class SecP128R1Field
	{
		// Token: 0x06002C49 RID: 11337 RVA: 0x000EA268 File Offset: 0x000EA268
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat128.Add(x, y, z) != 0U || (z[3] >= 4294967293U && Nat128.Gte(z, SecP128R1Field.P)))
			{
				SecP128R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000EA2A8 File Offset: 0x000EA2A8
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat256.Add(xx, yy, zz) != 0U || (zz[7] >= 4294967292U && Nat256.Gte(zz, SecP128R1Field.PExt)))
			{
				Nat.AddTo(SecP128R1Field.PExtInv.Length, SecP128R1Field.PExtInv, zz);
			}
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000EA2F8 File Offset: 0x000EA2F8
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(4, x, z) != 0U || (z[3] >= 4294967293U && Nat128.Gte(z, SecP128R1Field.P)))
			{
				SecP128R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000EA338 File Offset: 0x000EA338
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat128.FromBigInteger(x);
			if (array[3] >= 4294967293U && Nat128.Gte(array, SecP128R1Field.P))
			{
				Nat128.SubFrom(SecP128R1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000EA378 File Offset: 0x000EA378
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(4, x, 0U, z);
				return;
			}
			uint c = Nat128.Add(x, SecP128R1Field.P, z);
			Nat.ShiftDownBit(4, z, c);
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000EA3B4 File Offset: 0x000EA3B4
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat128.CreateExt();
			Nat128.Mul(x, y, array);
			SecP128R1Field.Reduce(array, z);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000EA3DC File Offset: 0x000EA3DC
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if (Nat128.MulAddTo(x, y, zz) != 0U || (zz[7] >= 4294967292U && Nat256.Gte(zz, SecP128R1Field.PExt)))
			{
				Nat.AddTo(SecP128R1Field.PExtInv.Length, SecP128R1Field.PExtInv, zz);
			}
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000EA42C File Offset: 0x000EA42C
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat128.IsZero(x))
			{
				Nat128.Zero(z);
				return;
			}
			Nat128.Sub(SecP128R1Field.P, x, z);
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000EA450 File Offset: 0x000EA450
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong num = (ulong)xx[0];
			ulong num2 = (ulong)xx[1];
			ulong num3 = (ulong)xx[2];
			ulong num4 = (ulong)xx[3];
			ulong num5 = (ulong)xx[4];
			ulong num6 = (ulong)xx[5];
			ulong num7 = (ulong)xx[6];
			ulong num8 = (ulong)xx[7];
			num4 += num8;
			num7 += num8 << 1;
			num3 += num7;
			num6 += num7 << 1;
			num2 += num6;
			num5 += num6 << 1;
			num += num5;
			num4 += num5 << 1;
			z[0] = (uint)num;
			num2 += num >> 32;
			z[1] = (uint)num2;
			num3 += num2 >> 32;
			z[2] = (uint)num3;
			num4 += num3 >> 32;
			z[3] = (uint)num4;
			SecP128R1Field.Reduce32((uint)(num4 >> 32), z);
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000EA4F8 File Offset: 0x000EA4F8
		public static void Reduce32(uint x, uint[] z)
		{
			while (x != 0U)
			{
				ulong num = (ulong)x;
				ulong num2 = (ulong)z[0] + num;
				z[0] = (uint)num2;
				num2 >>= 32;
				if (num2 != 0UL)
				{
					num2 += (ulong)z[1];
					z[1] = (uint)num2;
					num2 >>= 32;
					num2 += (ulong)z[2];
					z[2] = (uint)num2;
					num2 >>= 32;
				}
				num2 += (ulong)z[3] + (num << 1);
				z[3] = (uint)num2;
				num2 >>= 32;
				x = (uint)num2;
			}
			if (z[3] >= 4294967293U && Nat128.Gte(z, SecP128R1Field.P))
			{
				SecP128R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000EA588 File Offset: 0x000EA588
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat128.CreateExt();
			Nat128.Square(x, array);
			SecP128R1Field.Reduce(array, z);
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000EA5B0 File Offset: 0x000EA5B0
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat128.CreateExt();
			Nat128.Square(x, array);
			SecP128R1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat128.Square(z, array);
				SecP128R1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000EA5F4 File Offset: 0x000EA5F4
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat128.Sub(x, y, z);
			if (num != 0)
			{
				SecP128R1Field.SubPInvFrom(z);
			}
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000EA61C File Offset: 0x000EA61C
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			int num = Nat.Sub(10, xx, yy, zz);
			if (num != 0)
			{
				Nat.SubFrom(SecP128R1Field.PExtInv.Length, SecP128R1Field.PExtInv, zz);
			}
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000EA654 File Offset: 0x000EA654
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(4, x, 0U, z) != 0U || (z[3] >= 4294967293U && Nat128.Gte(z, SecP128R1Field.P)))
			{
				SecP128R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000EA698 File Offset: 0x000EA698
		private static void AddPInvTo(uint[] z)
		{
			long num = (long)((ulong)z[0] + 1UL);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num += (long)((ulong)z[1]);
				z[1] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[2]);
				z[2] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[3] + 2UL);
			z[3] = (uint)num;
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000EA6F4 File Offset: 0x000EA6F4
		private static void SubPInvFrom(uint[] z)
		{
			long num = (long)((ulong)z[0] - 1UL);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num += (long)((ulong)z[1]);
				z[1] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[2]);
				z[2] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[3] - 2UL);
			z[3] = (uint)num;
		}

		// Token: 0x04001B83 RID: 7043
		private const uint P3 = 4294967293U;

		// Token: 0x04001B84 RID: 7044
		private const uint PExt7 = 4294967292U;

		// Token: 0x04001B85 RID: 7045
		internal static readonly uint[] P = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			4294967293U
		};

		// Token: 0x04001B86 RID: 7046
		internal static readonly uint[] PExt = new uint[]
		{
			1U,
			0U,
			0U,
			4U,
			4294967294U,
			uint.MaxValue,
			3U,
			4294967292U
		};

		// Token: 0x04001B87 RID: 7047
		private static readonly uint[] PExtInv = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			4294967291U,
			1U,
			0U,
			4294967292U,
			3U
		};
	}
}
