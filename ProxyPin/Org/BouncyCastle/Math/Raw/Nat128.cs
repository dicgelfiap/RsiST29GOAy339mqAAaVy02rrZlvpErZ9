using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Math.Raw
{
	// Token: 0x02000624 RID: 1572
	internal abstract class Nat128
	{
		// Token: 0x060035BA RID: 13754 RVA: 0x0011A7A4 File Offset: 0x0011A7A4
		public static uint Add(uint[] x, uint[] y, uint[] z)
		{
			ulong num = 0UL;
			num += (ulong)x[0] + (ulong)y[0];
			z[0] = (uint)num;
			num >>= 32;
			num += (ulong)x[1] + (ulong)y[1];
			z[1] = (uint)num;
			num >>= 32;
			num += (ulong)x[2] + (ulong)y[2];
			z[2] = (uint)num;
			num >>= 32;
			num += (ulong)x[3] + (ulong)y[3];
			z[3] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x0011A814 File Offset: 0x0011A814
		public static uint AddBothTo(uint[] x, uint[] y, uint[] z)
		{
			ulong num = 0UL;
			num += (ulong)x[0] + (ulong)y[0] + (ulong)z[0];
			z[0] = (uint)num;
			num >>= 32;
			num += (ulong)x[1] + (ulong)y[1] + (ulong)z[1];
			z[1] = (uint)num;
			num >>= 32;
			num += (ulong)x[2] + (ulong)y[2] + (ulong)z[2];
			z[2] = (uint)num;
			num >>= 32;
			num += (ulong)x[3] + (ulong)y[3] + (ulong)z[3];
			z[3] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x0011A898 File Offset: 0x0011A898
		public static uint AddTo(uint[] x, uint[] z)
		{
			ulong num = 0UL;
			num += (ulong)x[0] + (ulong)z[0];
			z[0] = (uint)num;
			num >>= 32;
			num += (ulong)x[1] + (ulong)z[1];
			z[1] = (uint)num;
			num >>= 32;
			num += (ulong)x[2] + (ulong)z[2];
			z[2] = (uint)num;
			num >>= 32;
			num += (ulong)x[3] + (ulong)z[3];
			z[3] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x0011A908 File Offset: 0x0011A908
		public static uint AddTo(uint[] x, int xOff, uint[] z, int zOff, uint cIn)
		{
			ulong num = (ulong)cIn;
			num += (ulong)x[xOff] + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 1] + (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 2] + (ulong)z[zOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 3] + (ulong)z[zOff + 3];
			z[zOff + 3] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x0011A98C File Offset: 0x0011A98C
		public static uint AddToEachOther(uint[] u, int uOff, uint[] v, int vOff)
		{
			ulong num = 0UL;
			num += (ulong)u[uOff] + (ulong)v[vOff];
			u[uOff] = (uint)num;
			v[vOff] = (uint)num;
			num >>= 32;
			num += (ulong)u[uOff + 1] + (ulong)v[vOff + 1];
			u[uOff + 1] = (uint)num;
			v[vOff + 1] = (uint)num;
			num >>= 32;
			num += (ulong)u[uOff + 2] + (ulong)v[vOff + 2];
			u[uOff + 2] = (uint)num;
			v[vOff + 2] = (uint)num;
			num >>= 32;
			num += (ulong)u[uOff + 3] + (ulong)v[vOff + 3];
			u[uOff + 3] = (uint)num;
			v[vOff + 3] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x0011AA28 File Offset: 0x0011AA28
		public static void Copy(uint[] x, uint[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x0011AA44 File Offset: 0x0011AA44
		public static void Copy(uint[] x, int xOff, uint[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x0011AA6C File Offset: 0x0011AA6C
		public static void Copy64(ulong[] x, ulong[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x0011AA7C File Offset: 0x0011AA7C
		public static void Copy64(ulong[] x, int xOff, ulong[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x0011AA90 File Offset: 0x0011AA90
		public static uint[] Create()
		{
			return new uint[4];
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x0011AA98 File Offset: 0x0011AA98
		public static ulong[] Create64()
		{
			return new ulong[2];
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x0011AAA0 File Offset: 0x0011AAA0
		public static uint[] CreateExt()
		{
			return new uint[8];
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x0011AAA8 File Offset: 0x0011AAA8
		public static ulong[] CreateExt64()
		{
			return new ulong[4];
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x0011AAB0 File Offset: 0x0011AAB0
		public static bool Diff(uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			bool flag = Nat128.Gte(x, xOff, y, yOff);
			if (flag)
			{
				Nat128.Sub(x, xOff, y, yOff, z, zOff);
			}
			else
			{
				Nat128.Sub(y, yOff, x, xOff, z, zOff);
			}
			return flag;
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x0011AAF4 File Offset: 0x0011AAF4
		public static bool Eq(uint[] x, uint[] y)
		{
			for (int i = 3; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x0011AB24 File Offset: 0x0011AB24
		public static bool Eq64(ulong[] x, ulong[] y)
		{
			for (int i = 1; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x0011AB54 File Offset: 0x0011AB54
		public static uint[] FromBigInteger(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 128)
			{
				throw new ArgumentException();
			}
			uint[] array = Nat128.Create();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (uint)x.IntValue;
				x = x.ShiftRight(32);
			}
			return array;
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x0011ABB4 File Offset: 0x0011ABB4
		public static ulong[] FromBigInteger64(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 128)
			{
				throw new ArgumentException();
			}
			ulong[] array = Nat128.Create64();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (ulong)x.LongValue;
				x = x.ShiftRight(64);
			}
			return array;
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x0011AC14 File Offset: 0x0011AC14
		public static uint GetBit(uint[] x, int bit)
		{
			if (bit == 0)
			{
				return x[0] & 1U;
			}
			if ((bit & 127) != bit)
			{
				return 0U;
			}
			int num = bit >> 5;
			int num2 = bit & 31;
			return x[num] >> num2 & 1U;
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x0011AC50 File Offset: 0x0011AC50
		public static bool Gte(uint[] x, uint[] y)
		{
			for (int i = 3; i >= 0; i--)
			{
				uint num = x[i];
				uint num2 = y[i];
				if (num < num2)
				{
					return false;
				}
				if (num > num2)
				{
					return true;
				}
			}
			return true;
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x0011AC8C File Offset: 0x0011AC8C
		public static bool Gte(uint[] x, int xOff, uint[] y, int yOff)
		{
			for (int i = 3; i >= 0; i--)
			{
				uint num = x[xOff + i];
				uint num2 = y[yOff + i];
				if (num < num2)
				{
					return false;
				}
				if (num > num2)
				{
					return true;
				}
			}
			return true;
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x0011ACCC File Offset: 0x0011ACCC
		public static bool IsOne(uint[] x)
		{
			if (x[0] != 1U)
			{
				return false;
			}
			for (int i = 1; i < 4; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x0011AD04 File Offset: 0x0011AD04
		public static bool IsOne64(ulong[] x)
		{
			if (x[0] != 1UL)
			{
				return false;
			}
			for (int i = 1; i < 2; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x0011AD40 File Offset: 0x0011AD40
		public static bool IsZero(uint[] x)
		{
			for (int i = 0; i < 4; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x0011AD6C File Offset: 0x0011AD6C
		public static bool IsZero64(ulong[] x)
		{
			for (int i = 0; i < 2; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x0011AD9C File Offset: 0x0011AD9C
		public static void Mul(uint[] x, uint[] y, uint[] zz)
		{
			ulong num = (ulong)y[0];
			ulong num2 = (ulong)y[1];
			ulong num3 = (ulong)y[2];
			ulong num4 = (ulong)y[3];
			ulong num5 = 0UL;
			ulong num6 = (ulong)x[0];
			num5 += num6 * num;
			zz[0] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num2;
			zz[1] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num3;
			zz[2] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num4;
			zz[3] = (uint)num5;
			num5 >>= 32;
			zz[4] = (uint)num5;
			for (int i = 1; i < 4; i++)
			{
				ulong num7 = 0UL;
				ulong num8 = (ulong)x[i];
				num7 += num8 * num + (ulong)zz[i];
				zz[i] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num2 + (ulong)zz[i + 1];
				zz[i + 1] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num3 + (ulong)zz[i + 2];
				zz[i + 2] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num4 + (ulong)zz[i + 3];
				zz[i + 3] = (uint)num7;
				num7 >>= 32;
				zz[i + 4] = (uint)num7;
			}
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x0011AED4 File Offset: 0x0011AED4
		public static void Mul(uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)y[yOff];
			ulong num2 = (ulong)y[yOff + 1];
			ulong num3 = (ulong)y[yOff + 2];
			ulong num4 = (ulong)y[yOff + 3];
			ulong num5 = 0UL;
			ulong num6 = (ulong)x[xOff];
			num5 += num6 * num;
			zz[zzOff] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num2;
			zz[zzOff + 1] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num3;
			zz[zzOff + 2] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num4;
			zz[zzOff + 3] = (uint)num5;
			num5 >>= 32;
			zz[zzOff + 4] = (uint)num5;
			for (int i = 1; i < 4; i++)
			{
				zzOff++;
				ulong num7 = 0UL;
				ulong num8 = (ulong)x[xOff + i];
				num7 += num8 * num + (ulong)zz[zzOff];
				zz[zzOff] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num2 + (ulong)zz[zzOff + 1];
				zz[zzOff + 1] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num3 + (ulong)zz[zzOff + 2];
				zz[zzOff + 2] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num4 + (ulong)zz[zzOff + 3];
				zz[zzOff + 3] = (uint)num7;
				num7 >>= 32;
				zz[zzOff + 4] = (uint)num7;
			}
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x0011B034 File Offset: 0x0011B034
		public static uint MulAddTo(uint[] x, uint[] y, uint[] zz)
		{
			ulong num = (ulong)y[0];
			ulong num2 = (ulong)y[1];
			ulong num3 = (ulong)y[2];
			ulong num4 = (ulong)y[3];
			ulong num5 = 0UL;
			for (int i = 0; i < 4; i++)
			{
				ulong num6 = 0UL;
				ulong num7 = (ulong)x[i];
				num6 += num7 * num + (ulong)zz[i];
				zz[i] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num2 + (ulong)zz[i + 1];
				zz[i + 1] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num3 + (ulong)zz[i + 2];
				zz[i + 2] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num4 + (ulong)zz[i + 3];
				zz[i + 3] = (uint)num6;
				num6 >>= 32;
				num5 += num6 + (ulong)zz[i + 4];
				zz[i + 4] = (uint)num5;
				num5 >>= 32;
			}
			return (uint)num5;
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x0011B120 File Offset: 0x0011B120
		public static uint MulAddTo(uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)y[yOff];
			ulong num2 = (ulong)y[yOff + 1];
			ulong num3 = (ulong)y[yOff + 2];
			ulong num4 = (ulong)y[yOff + 3];
			ulong num5 = 0UL;
			for (int i = 0; i < 4; i++)
			{
				ulong num6 = 0UL;
				ulong num7 = (ulong)x[xOff + i];
				num6 += num7 * num + (ulong)zz[zzOff];
				zz[zzOff] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num2 + (ulong)zz[zzOff + 1];
				zz[zzOff + 1] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num3 + (ulong)zz[zzOff + 2];
				zz[zzOff + 2] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num4 + (ulong)zz[zzOff + 3];
				zz[zzOff + 3] = (uint)num6;
				num6 >>= 32;
				num5 += num6 + (ulong)zz[zzOff + 4];
				zz[zzOff + 4] = (uint)num5;
				num5 >>= 32;
				zzOff++;
			}
			return (uint)num5;
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x0011B224 File Offset: 0x0011B224
		public static ulong Mul33Add(uint w, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)w;
			ulong num3 = (ulong)x[xOff];
			num += num2 * num3 + (ulong)y[yOff];
			z[zOff] = (uint)num;
			num >>= 32;
			ulong num4 = (ulong)x[xOff + 1];
			num += num2 * num4 + num3 + (ulong)y[yOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			ulong num5 = (ulong)x[xOff + 2];
			num += num2 * num5 + num4 + (ulong)y[yOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			ulong num6 = (ulong)x[xOff + 3];
			num += num2 * num6 + num5 + (ulong)y[yOff + 3];
			z[zOff + 3] = (uint)num;
			num >>= 32;
			return num + num6;
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x0011B2D4 File Offset: 0x0011B2D4
		public static uint MulWordAddExt(uint x, uint[] yy, int yyOff, uint[] zz, int zzOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			num += num2 * (ulong)yy[yyOff] + (ulong)zz[zzOff];
			zz[zzOff] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)yy[yyOff + 1] + (ulong)zz[zzOff + 1];
			zz[zzOff + 1] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)yy[yyOff + 2] + (ulong)zz[zzOff + 2];
			zz[zzOff + 2] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)yy[yyOff + 3] + (ulong)zz[zzOff + 3];
			zz[zzOff + 3] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x0011B368 File Offset: 0x0011B368
		public static uint Mul33DWordAdd(uint x, ulong y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			ulong num3 = y & (ulong)-1;
			num += num2 * num3 + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			ulong num4 = y >> 32;
			num += num2 * num4 + num3 + (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += num4 + (ulong)z[zOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 3];
			z[zOff + 3] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x0011B3E8 File Offset: 0x0011B3E8
		public static uint Mul33WordAdd(uint x, uint y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)y;
			num += num2 * (ulong)x + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			num += num2 + (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(4, z, zOff, 3);
			}
			return 0U;
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x0011B454 File Offset: 0x0011B454
		public static uint MulWordDwordAdd(uint x, ulong y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			num += num2 * y + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			num += num2 * (y >> 32) + (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(4, z, zOff, 3);
			}
			return 0U;
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x0011B4C4 File Offset: 0x0011B4C4
		public static uint MulWordsAdd(uint x, uint y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			ulong num3 = (ulong)y;
			num += num3 * num2 + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(4, z, zOff, 2);
			}
			return 0U;
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x0011B51C File Offset: 0x0011B51C
		public static uint MulWord(uint x, uint[] y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			int num3 = 0;
			do
			{
				num += num2 * (ulong)y[num3];
				z[zOff + num3] = (uint)num;
				num >>= 32;
			}
			while (++num3 < 4);
			return (uint)num;
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x0011B554 File Offset: 0x0011B554
		public static void Square(uint[] x, uint[] zz)
		{
			ulong num = (ulong)x[0];
			uint num2 = 0U;
			int num3 = 3;
			int num4 = 8;
			do
			{
				ulong num5 = (ulong)x[num3--];
				ulong num6 = num5 * num5;
				zz[--num4] = (num2 << 31 | (uint)(num6 >> 33));
				zz[--num4] = (uint)(num6 >> 1);
				num2 = (uint)num6;
			}
			while (num3 > 0);
			ulong num7 = num * num;
			ulong num8 = (ulong)((ulong)num2 << 31) | num7 >> 33;
			zz[0] = (uint)num7;
			num2 = ((uint)(num7 >> 32) & 1U);
			ulong num9 = (ulong)x[1];
			ulong num10 = (ulong)zz[2];
			num8 += num9 * num;
			uint num11 = (uint)num8;
			zz[1] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num10 += num8 >> 32;
			ulong num12 = (ulong)x[2];
			ulong num13 = (ulong)zz[3];
			ulong num14 = (ulong)zz[4];
			num10 += num12 * num;
			num11 = (uint)num10;
			zz[2] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num13 += (num10 >> 32) + num12 * num9;
			num14 += num13 >> 32;
			num13 &= (ulong)-1;
			ulong num15 = (ulong)x[3];
			ulong num16 = (ulong)zz[5] + (num14 >> 32);
			num14 &= (ulong)-1;
			ulong num17 = (ulong)zz[6] + (num16 >> 32);
			num16 &= (ulong)-1;
			num13 += num15 * num;
			num11 = (uint)num13;
			zz[3] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num14 += (num13 >> 32) + num15 * num9;
			num16 += (num14 >> 32) + num15 * num12;
			num17 += num16 >> 32;
			num11 = (uint)num14;
			zz[4] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num16;
			zz[5] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num17;
			zz[6] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = zz[7] + (uint)(num17 >> 32);
			zz[7] = (num11 << 1 | num2);
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x0011B710 File Offset: 0x0011B710
		public static void Square(uint[] x, int xOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)x[xOff];
			uint num2 = 0U;
			int num3 = 3;
			int num4 = 8;
			do
			{
				ulong num5 = (ulong)x[xOff + num3--];
				ulong num6 = num5 * num5;
				zz[zzOff + --num4] = (num2 << 31 | (uint)(num6 >> 33));
				zz[zzOff + --num4] = (uint)(num6 >> 1);
				num2 = (uint)num6;
			}
			while (num3 > 0);
			ulong num7 = num * num;
			ulong num8 = (ulong)((ulong)num2 << 31) | num7 >> 33;
			zz[zzOff] = (uint)num7;
			num2 = ((uint)(num7 >> 32) & 1U);
			ulong num9 = (ulong)x[xOff + 1];
			ulong num10 = (ulong)zz[zzOff + 2];
			num8 += num9 * num;
			uint num11 = (uint)num8;
			zz[zzOff + 1] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num10 += num8 >> 32;
			ulong num12 = (ulong)x[xOff + 2];
			ulong num13 = (ulong)zz[zzOff + 3];
			ulong num14 = (ulong)zz[zzOff + 4];
			num10 += num12 * num;
			num11 = (uint)num10;
			zz[zzOff + 2] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num13 += (num10 >> 32) + num12 * num9;
			num14 += num13 >> 32;
			num13 &= (ulong)-1;
			ulong num15 = (ulong)x[xOff + 3];
			ulong num16 = (ulong)zz[zzOff + 5] + (num14 >> 32);
			num14 &= (ulong)-1;
			ulong num17 = (ulong)zz[zzOff + 6] + (num16 >> 32);
			num16 &= (ulong)-1;
			num13 += num15 * num;
			num11 = (uint)num13;
			zz[zzOff + 3] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num14 += (num13 >> 32) + num15 * num9;
			num16 += (num14 >> 32) + num15 * num12;
			num17 += num16 >> 32;
			num11 = (uint)num14;
			zz[zzOff + 4] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num16;
			zz[zzOff + 5] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num17;
			zz[zzOff + 6] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = zz[zzOff + 7] + (uint)(num17 >> 32);
			zz[zzOff + 7] = (num11 << 1 | num2);
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x0011B8F4 File Offset: 0x0011B8F4
		public static int Sub(uint[] x, uint[] y, uint[] z)
		{
			long num = 0L;
			num += (long)((ulong)x[0] - (ulong)y[0]);
			z[0] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[1] - (ulong)y[1]);
			z[1] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[2] - (ulong)y[2]);
			z[2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[3] - (ulong)y[3]);
			z[3] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x0011B964 File Offset: 0x0011B964
		public static int Sub(uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			long num = 0L;
			num += (long)((ulong)x[xOff] - (ulong)y[yOff]);
			z[zOff] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[xOff + 1] - (ulong)y[yOff + 1]);
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[xOff + 2] - (ulong)y[yOff + 2]);
			z[zOff + 2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[xOff + 3] - (ulong)y[yOff + 3]);
			z[zOff + 3] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x0011B9EC File Offset: 0x0011B9EC
		public static int SubBothFrom(uint[] x, uint[] y, uint[] z)
		{
			long num = 0L;
			num += (long)((ulong)z[0] - (ulong)x[0] - (ulong)y[0]);
			z[0] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[1] - (ulong)x[1] - (ulong)y[1]);
			z[1] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[2] - (ulong)x[2] - (ulong)y[2]);
			z[2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[3] - (ulong)x[3] - (ulong)y[3]);
			z[3] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x0011BA70 File Offset: 0x0011BA70
		public static int SubFrom(uint[] x, uint[] z)
		{
			long num = 0L;
			num += (long)((ulong)z[0] - (ulong)x[0]);
			z[0] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[1] - (ulong)x[1]);
			z[1] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[2] - (ulong)x[2]);
			z[2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[3] - (ulong)x[3]);
			z[3] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x0011BAE0 File Offset: 0x0011BAE0
		public static int SubFrom(uint[] x, int xOff, uint[] z, int zOff)
		{
			long num = 0L;
			num += (long)((ulong)z[zOff] - (ulong)x[xOff]);
			z[zOff] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 1] - (ulong)x[xOff + 1]);
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 2] - (ulong)x[xOff + 2]);
			z[zOff + 2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 3] - (ulong)x[xOff + 3]);
			z[zOff + 3] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x0011BB60 File Offset: 0x0011BB60
		public static BigInteger ToBigInteger(uint[] x)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < 4; i++)
			{
				uint num = x[i];
				if (num != 0U)
				{
					Pack.UInt32_To_BE(num, array, 3 - i << 2);
				}
			}
			return new BigInteger(1, array);
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x0011BBA8 File Offset: 0x0011BBA8
		public static BigInteger ToBigInteger64(ulong[] x)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < 2; i++)
			{
				ulong num = x[i];
				if (num != 0UL)
				{
					Pack.UInt64_To_BE(num, array, 1 - i << 3);
				}
			}
			return new BigInteger(1, array);
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x0011BBF0 File Offset: 0x0011BBF0
		public static void Zero(uint[] z)
		{
			z[0] = 0U;
			z[1] = 0U;
			z[2] = 0U;
			z[3] = 0U;
		}

		// Token: 0x04001D22 RID: 7458
		private const ulong M = 4294967295UL;
	}
}
