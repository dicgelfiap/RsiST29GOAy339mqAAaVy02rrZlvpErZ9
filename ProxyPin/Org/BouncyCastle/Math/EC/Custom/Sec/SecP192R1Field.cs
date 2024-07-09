using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000593 RID: 1427
	internal class SecP192R1Field
	{
		// Token: 0x06002D4F RID: 11599 RVA: 0x000EED20 File Offset: 0x000EED20
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat192.Add(x, y, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192R1Field.P)))
			{
				SecP192R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000EED60 File Offset: 0x000EED60
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(12, xx, yy, zz) != 0U || (zz[11] == 4294967295U && Nat.Gte(12, zz, SecP192R1Field.PExt))) && Nat.AddTo(SecP192R1Field.PExtInv.Length, SecP192R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(12, zz, SecP192R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000EEDC8 File Offset: 0x000EEDC8
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(6, x, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192R1Field.P)))
			{
				SecP192R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000EEE08 File Offset: 0x000EEE08
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat192.FromBigInteger(x);
			if (array[5] == 4294967295U && Nat192.Gte(array, SecP192R1Field.P))
			{
				Nat192.SubFrom(SecP192R1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000EEE48 File Offset: 0x000EEE48
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(6, x, 0U, z);
				return;
			}
			uint c = Nat192.Add(x, SecP192R1Field.P, z);
			Nat.ShiftDownBit(6, z, c);
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000EEE84 File Offset: 0x000EEE84
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Mul(x, y, array);
			SecP192R1Field.Reduce(array, z);
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000EEEAC File Offset: 0x000EEEAC
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat192.MulAddTo(x, y, zz) != 0U || (zz[11] == 4294967295U && Nat.Gte(12, zz, SecP192R1Field.PExt))) && Nat.AddTo(SecP192R1Field.PExtInv.Length, SecP192R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(12, zz, SecP192R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000EEF10 File Offset: 0x000EEF10
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat192.IsZero(x))
			{
				Nat192.Zero(z);
				return;
			}
			Nat192.Sub(SecP192R1Field.P, x, z);
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000EEF34 File Offset: 0x000EEF34
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong num = (ulong)xx[6];
			ulong num2 = (ulong)xx[7];
			ulong num3 = (ulong)xx[8];
			ulong num4 = (ulong)xx[9];
			ulong num5 = (ulong)xx[10];
			ulong num6 = (ulong)xx[11];
			ulong num7 = num + num5;
			ulong num8 = num2 + num6;
			ulong num9 = 0UL;
			num9 += (ulong)xx[0] + num7;
			uint num10 = (uint)num9;
			num9 >>= 32;
			num9 += (ulong)xx[1] + num8;
			z[1] = (uint)num9;
			num9 >>= 32;
			num7 += num3;
			num8 += num4;
			num9 += (ulong)xx[2] + num7;
			ulong num11 = (ulong)((uint)num9);
			num9 >>= 32;
			num9 += (ulong)xx[3] + num8;
			z[3] = (uint)num9;
			num9 >>= 32;
			num7 -= num;
			num8 -= num2;
			num9 += (ulong)xx[4] + num7;
			z[4] = (uint)num9;
			num9 >>= 32;
			num9 += (ulong)xx[5] + num8;
			z[5] = (uint)num9;
			num9 >>= 32;
			num11 += num9;
			num9 += (ulong)num10;
			z[0] = (uint)num9;
			num9 >>= 32;
			if (num9 != 0UL)
			{
				num9 += (ulong)z[1];
				z[1] = (uint)num9;
				num11 += num9 >> 32;
			}
			z[2] = (uint)num11;
			num9 = num11 >> 32;
			if ((num9 != 0UL && Nat.IncAt(6, z, 3) != 0U) || (z[5] == 4294967295U && Nat192.Gte(z, SecP192R1Field.P)))
			{
				SecP192R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000EF0A8 File Offset: 0x000EF0A8
		public static void Reduce32(uint x, uint[] z)
		{
			ulong num = 0UL;
			if (x != 0U)
			{
				num += (ulong)z[0] + (ulong)x;
				z[0] = (uint)num;
				num >>= 32;
				if (num != 0UL)
				{
					num += (ulong)z[1];
					z[1] = (uint)num;
					num >>= 32;
				}
				num += (ulong)z[2] + (ulong)x;
				z[2] = (uint)num;
				num >>= 32;
			}
			if ((num != 0UL && Nat.IncAt(6, z, 3) != 0U) || (z[5] == 4294967295U && Nat192.Gte(z, SecP192R1Field.P)))
			{
				SecP192R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000EF138 File Offset: 0x000EF138
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Square(x, array);
			SecP192R1Field.Reduce(array, z);
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000EF160 File Offset: 0x000EF160
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Square(x, array);
			SecP192R1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat192.Square(z, array);
				SecP192R1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000EF1A4 File Offset: 0x000EF1A4
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat192.Sub(x, y, z);
			if (num != 0)
			{
				SecP192R1Field.SubPInvFrom(z);
			}
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000EF1CC File Offset: 0x000EF1CC
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			int num = Nat.Sub(12, xx, yy, zz);
			if (num != 0 && Nat.SubFrom(SecP192R1Field.PExtInv.Length, SecP192R1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(12, zz, SecP192R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000EF218 File Offset: 0x000EF218
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(6, x, 0U, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192R1Field.P)))
			{
				SecP192R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000EF258 File Offset: 0x000EF258
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
			}
			num += (long)((ulong)z[2] + 1UL);
			z[2] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				Nat.IncAt(6, z, 3);
			}
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000EF2BC File Offset: 0x000EF2BC
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
			}
			num += (long)((ulong)z[2] - 1UL);
			z[2] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				Nat.DecAt(6, z, 3);
			}
		}

		// Token: 0x04001BBB RID: 7099
		private const uint P5 = 4294967295U;

		// Token: 0x04001BBC RID: 7100
		private const uint PExt11 = 4294967295U;

		// Token: 0x04001BBD RID: 7101
		internal static readonly uint[] P = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BBE RID: 7102
		internal static readonly uint[] PExt = new uint[]
		{
			1U,
			0U,
			2U,
			0U,
			1U,
			0U,
			4294967294U,
			uint.MaxValue,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BBF RID: 7103
		private static readonly uint[] PExtInv = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			4294967293U,
			uint.MaxValue,
			4294967294U,
			uint.MaxValue,
			1U,
			0U,
			2U
		};
	}
}
