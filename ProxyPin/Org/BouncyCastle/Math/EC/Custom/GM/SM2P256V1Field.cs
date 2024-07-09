using System;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC.Custom.GM
{
	// Token: 0x0200057D RID: 1405
	internal class SM2P256V1Field
	{
		// Token: 0x06002C0B RID: 11275 RVA: 0x000E8F34 File Offset: 0x000E8F34
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat256.Add(x, y, z) != 0U || (z[7] >= 4294967294U && Nat256.Gte(z, SM2P256V1Field.P)))
			{
				SM2P256V1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000E8F74 File Offset: 0x000E8F74
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Add(16, xx, yy, zz) != 0U || (zz[15] >= 4294967294U && Nat.Gte(16, zz, SM2P256V1Field.PExt)))
			{
				Nat.SubFrom(16, SM2P256V1Field.PExt, zz);
			}
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000E8FC4 File Offset: 0x000E8FC4
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(8, x, z) != 0U || (z[7] >= 4294967294U && Nat256.Gte(z, SM2P256V1Field.P)))
			{
				SM2P256V1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000E9004 File Offset: 0x000E9004
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat256.FromBigInteger(x);
			if (array[7] >= 4294967294U && Nat256.Gte(array, SM2P256V1Field.P))
			{
				Nat256.SubFrom(SM2P256V1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000E9044 File Offset: 0x000E9044
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(8, x, 0U, z);
				return;
			}
			uint c = Nat256.Add(x, SM2P256V1Field.P, z);
			Nat.ShiftDownBit(8, z, c);
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x000E9080 File Offset: 0x000E9080
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Mul(x, y, array);
			SM2P256V1Field.Reduce(array, z);
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000E90A8 File Offset: 0x000E90A8
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if (Nat256.MulAddTo(x, y, zz) != 0U || (zz[15] >= 4294967294U && Nat.Gte(16, zz, SM2P256V1Field.PExt)))
			{
				Nat.SubFrom(16, SM2P256V1Field.PExt, zz);
			}
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000E90F4 File Offset: 0x000E90F4
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat256.IsZero(x))
			{
				Nat256.Zero(z);
				return;
			}
			Nat256.Sub(SM2P256V1Field.P, x, z);
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000E9118 File Offset: 0x000E9118
		public static void Reduce(uint[] xx, uint[] z)
		{
			long num = (long)((ulong)xx[8]);
			long num2 = (long)((ulong)xx[9]);
			long num3 = (long)((ulong)xx[10]);
			long num4 = (long)((ulong)xx[11]);
			long num5 = (long)((ulong)xx[12]);
			long num6 = (long)((ulong)xx[13]);
			long num7 = (long)((ulong)xx[14]);
			long num8 = (long)((ulong)xx[15]);
			long num9 = num + num2;
			long num10 = num3 + num4;
			long num11 = num5 + num8;
			long num12 = num6 + num7;
			long num13 = num12 + (num8 << 1);
			long num14 = num9 + num12;
			long num15 = num10 + num11 + num14;
			long num16 = 0L;
			num16 += (long)((ulong)xx[0] + (ulong)num15 + (ulong)num6 + (ulong)num7 + (ulong)num8);
			z[0] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[1] + (ulong)num15 - (ulong)num + (ulong)num7 + (ulong)num8);
			z[1] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[2] - (ulong)num14);
			z[2] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[3] + (ulong)num15 - (ulong)num2 - (ulong)num3 + (ulong)num6);
			z[3] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[4] + (ulong)num15 - (ulong)num10 - (ulong)num + (ulong)num7);
			z[4] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[5] + (ulong)num13 + (ulong)num3);
			z[5] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[6] + (ulong)num4 + (ulong)num7 + (ulong)num8);
			z[6] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[7] + (ulong)num15 + (ulong)num13 + (ulong)num5);
			z[7] = (uint)num16;
			num16 >>= 32;
			SM2P256V1Field.Reduce32((uint)num16, z);
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000E9290 File Offset: 0x000E9290
		public static void Reduce32(uint x, uint[] z)
		{
			long num = 0L;
			if (x != 0U)
			{
				long num2 = (long)((ulong)x);
				num += (long)((ulong)z[0] + (ulong)num2);
				z[0] = (uint)num;
				num >>= 32;
				if (num != 0L)
				{
					num += (long)((ulong)z[1]);
					z[1] = (uint)num;
					num >>= 32;
				}
				num += (long)((ulong)z[2] - (ulong)num2);
				z[2] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[3] + (ulong)num2);
				z[3] = (uint)num;
				num >>= 32;
				if (num != 0L)
				{
					num += (long)((ulong)z[4]);
					z[4] = (uint)num;
					num >>= 32;
					num += (long)((ulong)z[5]);
					z[5] = (uint)num;
					num >>= 32;
					num += (long)((ulong)z[6]);
					z[6] = (uint)num;
					num >>= 32;
				}
				num += (long)((ulong)z[7] + (ulong)num2);
				z[7] = (uint)num;
				num >>= 32;
			}
			if (num != 0L || (z[7] >= 4294967294U && Nat256.Gte(z, SM2P256V1Field.P)))
			{
				SM2P256V1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000E9378 File Offset: 0x000E9378
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			SM2P256V1Field.Reduce(array, z);
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000E93A0 File Offset: 0x000E93A0
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			SM2P256V1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat256.Square(z, array);
				SM2P256V1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000E93E4 File Offset: 0x000E93E4
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat256.Sub(x, y, z);
			if (num != 0)
			{
				SM2P256V1Field.SubPInvFrom(z);
			}
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000E940C File Offset: 0x000E940C
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			int num = Nat.Sub(16, xx, yy, zz);
			if (num != 0)
			{
				Nat.AddTo(16, SM2P256V1Field.PExt, zz);
			}
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000E943C File Offset: 0x000E943C
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(8, x, 0U, z) != 0U || (z[7] >= 4294967294U && Nat256.Gte(z, SM2P256V1Field.P)))
			{
				SM2P256V1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x000E9480 File Offset: 0x000E9480
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
			num += (long)((ulong)z[2] - 1UL);
			z[2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[3] + 1UL);
			z[3] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num += (long)((ulong)z[4]);
				z[4] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[5]);
				z[5] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[6]);
				z[6] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[7] + 1UL);
			z[7] = (uint)num;
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000E9530 File Offset: 0x000E9530
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
			num += (long)((ulong)z[2] + 1UL);
			z[2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[3] - 1UL);
			z[3] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num += (long)((ulong)z[4]);
				z[4] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[5]);
				z[5] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[6]);
				z[6] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[7] - 1UL);
			z[7] = (uint)num;
		}

		// Token: 0x04001B78 RID: 7032
		internal const uint P7 = 4294967294U;

		// Token: 0x04001B79 RID: 7033
		internal const uint PExt15 = 4294967294U;

		// Token: 0x04001B7A RID: 7034
		internal static readonly uint[] P = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			0U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			4294967294U
		};

		// Token: 0x04001B7B RID: 7035
		internal static readonly uint[] PExt = new uint[]
		{
			1U,
			0U,
			4294967294U,
			1U,
			1U,
			4294967294U,
			0U,
			2U,
			4294967294U,
			4294967293U,
			3U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			0U,
			4294967294U
		};
	}
}
