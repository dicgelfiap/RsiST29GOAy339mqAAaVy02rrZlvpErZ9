using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200059B RID: 1435
	internal class SecP224R1Field
	{
		// Token: 0x06002DC9 RID: 11721 RVA: 0x000F1008 File Offset: 0x000F1008
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat224.Add(x, y, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224R1Field.P)))
			{
				SecP224R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000F1048 File Offset: 0x000F1048
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(14, xx, yy, zz) != 0U || (zz[13] == 4294967295U && Nat.Gte(14, zz, SecP224R1Field.PExt))) && Nat.AddTo(SecP224R1Field.PExtInv.Length, SecP224R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(14, zz, SecP224R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000F10B0 File Offset: 0x000F10B0
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(7, x, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224R1Field.P)))
			{
				SecP224R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000F10F0 File Offset: 0x000F10F0
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat224.FromBigInteger(x);
			if (array[6] == 4294967295U && Nat224.Gte(array, SecP224R1Field.P))
			{
				Nat224.SubFrom(SecP224R1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000F1130 File Offset: 0x000F1130
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(7, x, 0U, z);
				return;
			}
			uint c = Nat224.Add(x, SecP224R1Field.P, z);
			Nat.ShiftDownBit(7, z, c);
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000F116C File Offset: 0x000F116C
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Mul(x, y, array);
			SecP224R1Field.Reduce(array, z);
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000F1194 File Offset: 0x000F1194
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat224.MulAddTo(x, y, zz) != 0U || (zz[13] == 4294967295U && Nat.Gte(14, zz, SecP224R1Field.PExt))) && Nat.AddTo(SecP224R1Field.PExtInv.Length, SecP224R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(14, zz, SecP224R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000F11F8 File Offset: 0x000F11F8
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat224.IsZero(x))
			{
				Nat224.Zero(z);
				return;
			}
			Nat224.Sub(SecP224R1Field.P, x, z);
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000F121C File Offset: 0x000F121C
		public static void Reduce(uint[] xx, uint[] z)
		{
			long num = (long)((ulong)xx[10]);
			long num2 = (long)((ulong)xx[11]);
			long num3 = (long)((ulong)xx[12]);
			long num4 = (long)((ulong)xx[13]);
			long num5 = (long)((ulong)xx[7] + (ulong)num2 - 1UL);
			long num6 = (long)((ulong)xx[8] + (ulong)num3);
			long num7 = (long)((ulong)xx[9] + (ulong)num4);
			long num8 = 0L;
			num8 += (long)((ulong)xx[0] - (ulong)num5);
			long num9 = (long)((ulong)((uint)num8));
			num8 >>= 32;
			num8 += (long)((ulong)xx[1] - (ulong)num6);
			z[1] = (uint)num8;
			num8 >>= 32;
			num8 += (long)((ulong)xx[2] - (ulong)num7);
			z[2] = (uint)num8;
			num8 >>= 32;
			num8 += (long)((ulong)xx[3] + (ulong)num5 - (ulong)num);
			long num10 = (long)((ulong)((uint)num8));
			num8 >>= 32;
			num8 += (long)((ulong)xx[4] + (ulong)num6 - (ulong)num2);
			z[4] = (uint)num8;
			num8 >>= 32;
			num8 += (long)((ulong)xx[5] + (ulong)num7 - (ulong)num3);
			z[5] = (uint)num8;
			num8 >>= 32;
			num8 += (long)((ulong)xx[6] + (ulong)num - (ulong)num4);
			z[6] = (uint)num8;
			num8 >>= 32;
			num8 += 1L;
			num10 += num8;
			num9 -= num8;
			z[0] = (uint)num9;
			num8 = num9 >> 32;
			if (num8 != 0L)
			{
				num8 += (long)((ulong)z[1]);
				z[1] = (uint)num8;
				num8 >>= 32;
				num8 += (long)((ulong)z[2]);
				z[2] = (uint)num8;
				num10 += num8 >> 32;
			}
			z[3] = (uint)num10;
			num8 = num10 >> 32;
			if ((num8 != 0L && Nat.IncAt(7, z, 4) != 0U) || (z[6] == 4294967295U && Nat224.Gte(z, SecP224R1Field.P)))
			{
				SecP224R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000F13B8 File Offset: 0x000F13B8
		public static void Reduce32(uint x, uint[] z)
		{
			long num = 0L;
			if (x != 0U)
			{
				long num2 = (long)((ulong)x);
				num += (long)((ulong)z[0] - (ulong)num2);
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
				num += (long)((ulong)z[3] + (ulong)num2);
				z[3] = (uint)num;
				num >>= 32;
			}
			if ((num != 0L && Nat.IncAt(7, z, 4) != 0U) || (z[6] == 4294967295U && Nat224.Gte(z, SecP224R1Field.P)))
			{
				SecP224R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000F145C File Offset: 0x000F145C
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Square(x, array);
			SecP224R1Field.Reduce(array, z);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000F1484 File Offset: 0x000F1484
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Square(x, array);
			SecP224R1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat224.Square(z, array);
				SecP224R1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000F14C8 File Offset: 0x000F14C8
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat224.Sub(x, y, z);
			if (num != 0)
			{
				SecP224R1Field.SubPInvFrom(z);
			}
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000F14F0 File Offset: 0x000F14F0
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			int num = Nat.Sub(14, xx, yy, zz);
			if (num != 0 && Nat.SubFrom(SecP224R1Field.PExtInv.Length, SecP224R1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(14, zz, SecP224R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000F153C File Offset: 0x000F153C
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(7, x, 0U, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224R1Field.P)))
			{
				SecP224R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000F157C File Offset: 0x000F157C
		private static void AddPInvTo(uint[] z)
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
			num += (long)((ulong)z[3] + 1UL);
			z[3] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				Nat.IncAt(7, z, 4);
			}
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000F15F0 File Offset: 0x000F15F0
		private static void SubPInvFrom(uint[] z)
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
			num += (long)((ulong)z[3] - 1UL);
			z[3] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				Nat.DecAt(7, z, 4);
			}
		}

		// Token: 0x04001BD5 RID: 7125
		private const uint P6 = 4294967295U;

		// Token: 0x04001BD6 RID: 7126
		private const uint PExt13 = 4294967295U;

		// Token: 0x04001BD7 RID: 7127
		internal static readonly uint[] P = new uint[]
		{
			1U,
			0U,
			0U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BD8 RID: 7128
		internal static readonly uint[] PExt = new uint[]
		{
			1U,
			0U,
			0U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			0U,
			2U,
			0U,
			0U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001BD9 RID: 7129
		private static readonly uint[] PExtInv = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			1U,
			0U,
			0U,
			uint.MaxValue,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			1U
		};
	}
}
