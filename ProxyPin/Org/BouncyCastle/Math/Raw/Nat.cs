using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Math.Raw
{
	// Token: 0x02000623 RID: 1571
	internal abstract class Nat
	{
		// Token: 0x06003552 RID: 13650 RVA: 0x00118C08 File Offset: 0x00118C08
		public static uint Add(int len, uint[] x, uint[] y, uint[] z)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)x[i] + (ulong)y[i];
				z[i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x00118C44 File Offset: 0x00118C44
		public static uint Add33At(int len, uint x, uint[] z, int zPos)
		{
			ulong num = (ulong)z[zPos] + (ulong)x;
			z[zPos] = (uint)num;
			num >>= 32;
			num += (ulong)z[zPos + 1] + 1UL;
			z[zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zPos + 2);
			}
			return 0U;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x00118C94 File Offset: 0x00118C94
		public static uint Add33At(int len, uint x, uint[] z, int zOff, int zPos)
		{
			ulong num = (ulong)z[zOff + zPos] + (ulong)x;
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + zPos + 1] + 1UL;
			z[zOff + zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, zPos + 2);
			}
			return 0U;
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x00118CF4 File Offset: 0x00118CF4
		public static uint Add33To(int len, uint x, uint[] z)
		{
			ulong num = (ulong)z[0] + (ulong)x;
			z[0] = (uint)num;
			num >>= 32;
			num += (ulong)z[1] + 1UL;
			z[1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, 2);
			}
			return 0U;
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x00118D40 File Offset: 0x00118D40
		public static uint Add33To(int len, uint x, uint[] z, int zOff)
		{
			ulong num = (ulong)z[zOff] + (ulong)x;
			z[zOff] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 1] + 1UL;
			z[zOff + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, 2);
			}
			return 0U;
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x00118D90 File Offset: 0x00118D90
		public static uint AddBothTo(int len, uint[] x, uint[] y, uint[] z)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)x[i] + (ulong)y[i] + (ulong)z[i];
				z[i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x00118DD0 File Offset: 0x00118DD0
		public static uint AddBothTo(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)x[xOff + i] + (ulong)y[yOff + i] + (ulong)z[zOff + i];
				z[zOff + i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x00118E20 File Offset: 0x00118E20
		public static uint AddDWordAt(int len, ulong x, uint[] z, int zPos)
		{
			ulong num = (ulong)z[zPos] + (x & (ulong)-1);
			z[zPos] = (uint)num;
			num >>= 32;
			num += (ulong)z[zPos + 1] + (x >> 32);
			z[zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zPos + 2);
			}
			return 0U;
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x00118E74 File Offset: 0x00118E74
		public static uint AddDWordAt(int len, ulong x, uint[] z, int zOff, int zPos)
		{
			ulong num = (ulong)z[zOff + zPos] + (x & (ulong)-1);
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + zPos + 1] + (x >> 32);
			z[zOff + zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, zPos + 2);
			}
			return 0U;
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x00118ED8 File Offset: 0x00118ED8
		public static uint AddDWordTo(int len, ulong x, uint[] z)
		{
			ulong num = (ulong)z[0] + (x & (ulong)-1);
			z[0] = (uint)num;
			num >>= 32;
			num += (ulong)z[1] + (x >> 32);
			z[1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, 2);
			}
			return 0U;
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x00118F28 File Offset: 0x00118F28
		public static uint AddDWordTo(int len, ulong x, uint[] z, int zOff)
		{
			ulong num = (ulong)z[zOff] + (x & (ulong)-1);
			z[zOff] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 1] + (x >> 32);
			z[zOff + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, 2);
			}
			return 0U;
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x00118F7C File Offset: 0x00118F7C
		public static uint AddTo(int len, uint[] x, uint[] z)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)x[i] + (ulong)z[i];
				z[i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x00118FB8 File Offset: 0x00118FB8
		public static uint AddTo(int len, uint[] x, int xOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)x[xOff + i] + (ulong)z[zOff + i];
				z[zOff + i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x00118FFC File Offset: 0x00118FFC
		public static uint AddTo(int len, uint[] x, int xOff, uint[] z, int zOff, uint cIn)
		{
			ulong num = (ulong)cIn;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)x[xOff + i] + (ulong)z[zOff + i];
				z[zOff + i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x00119040 File Offset: 0x00119040
		public static uint AddToEachOther(int len, uint[] u, int uOff, uint[] v, int vOff)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)u[uOff + i] + (ulong)v[vOff + i];
				u[uOff + i] = (uint)num;
				v[vOff + i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x0011908C File Offset: 0x0011908C
		public static uint AddWordAt(int len, uint x, uint[] z, int zPos)
		{
			ulong num = (ulong)x + (ulong)z[zPos];
			z[zPos] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zPos + 1);
			}
			return 0U;
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x001190C4 File Offset: 0x001190C4
		public static uint AddWordAt(int len, uint x, uint[] z, int zOff, int zPos)
		{
			ulong num = (ulong)x + (ulong)z[zOff + zPos];
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, zPos + 1);
			}
			return 0U;
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x00119104 File Offset: 0x00119104
		public static uint AddWordTo(int len, uint x, uint[] z)
		{
			ulong num = (ulong)x + (ulong)z[0];
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, 1);
			}
			return 0U;
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x0011913C File Offset: 0x0011913C
		public static uint AddWordTo(int len, uint x, uint[] z, int zOff)
		{
			ulong num = (ulong)x + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, 1);
			}
			return 0U;
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x00119174 File Offset: 0x00119174
		public static uint CAdd(int len, int mask, uint[] x, uint[] y, uint[] z)
		{
			uint num = (uint)(-(uint)(mask & 1));
			ulong num2 = 0UL;
			for (int i = 0; i < len; i++)
			{
				num2 += (ulong)x[i] + (ulong)(y[i] & num);
				z[i] = (uint)num2;
				num2 >>= 32;
			}
			return (uint)num2;
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x001191B8 File Offset: 0x001191B8
		public static void CMov(int len, int mask, uint[] x, int xOff, uint[] z, int zOff)
		{
			uint num = (uint)(-(uint)(mask & 1));
			for (int i = 0; i < len; i++)
			{
				uint num2 = z[zOff + i];
				uint num3 = num2 ^ x[xOff + i];
				num2 ^= (num3 & num);
				z[zOff + i] = num2;
			}
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x001191FC File Offset: 0x001191FC
		public static void CMov(int len, int mask, int[] x, int xOff, int[] z, int zOff)
		{
			mask = -(mask & 1);
			for (int i = 0; i < len; i++)
			{
				int num = z[zOff + i];
				int num2 = num ^ x[xOff + i];
				num ^= (num2 & mask);
				z[zOff + i] = num;
			}
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x00119240 File Offset: 0x00119240
		public static void Copy(int len, uint[] x, uint[] z)
		{
			Array.Copy(x, 0, z, 0, len);
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x0011924C File Offset: 0x0011924C
		public static uint[] Copy(int len, uint[] x)
		{
			uint[] array = new uint[len];
			Array.Copy(x, 0, array, 0, len);
			return array;
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x00119270 File Offset: 0x00119270
		public static void Copy(int len, uint[] x, int xOff, uint[] z, int zOff)
		{
			Array.Copy(x, xOff, z, zOff, len);
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x00119280 File Offset: 0x00119280
		public static ulong[] Copy64(int len, ulong[] x)
		{
			ulong[] array = new ulong[len];
			Array.Copy(x, 0, array, 0, len);
			return array;
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x001192A4 File Offset: 0x001192A4
		public static void Copy64(int len, ulong[] x, ulong[] z)
		{
			Array.Copy(x, 0, z, 0, len);
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x001192B0 File Offset: 0x001192B0
		public static void Copy64(int len, ulong[] x, int xOff, ulong[] z, int zOff)
		{
			Array.Copy(x, xOff, z, zOff, len);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x001192C0 File Offset: 0x001192C0
		public static uint[] Create(int len)
		{
			return new uint[len];
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x001192C8 File Offset: 0x001192C8
		public static ulong[] Create64(int len)
		{
			return new ulong[len];
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x001192D0 File Offset: 0x001192D0
		public static int CSub(int len, int mask, uint[] x, uint[] y, uint[] z)
		{
			long num = (long)((ulong)(-(ulong)(mask & 1)));
			long num2 = 0L;
			for (int i = 0; i < len; i++)
			{
				num2 += (long)((ulong)x[i] - ((ulong)y[i] & (ulong)num));
				z[i] = (uint)num2;
				num2 >>= 32;
			}
			return (int)num2;
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x00119314 File Offset: 0x00119314
		public static int CSub(int len, int mask, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			long num = (long)((ulong)(-(ulong)(mask & 1)));
			long num2 = 0L;
			for (int i = 0; i < len; i++)
			{
				num2 += (long)((ulong)x[xOff + i] - ((ulong)y[yOff + i] & (ulong)num));
				z[zOff + i] = (uint)num2;
				num2 >>= 32;
			}
			return (int)num2;
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x00119364 File Offset: 0x00119364
		public static int Dec(int len, uint[] z)
		{
			for (int i = 0; i < len; i++)
			{
				IntPtr intPtr;
				if ((z[(int)(intPtr = (IntPtr)i)] = z[(int)intPtr] - 1U) != 4294967295U)
				{
					return 0;
				}
			}
			return -1;
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x0011939C File Offset: 0x0011939C
		public static int Dec(int len, uint[] x, uint[] z)
		{
			int i = 0;
			while (i < len)
			{
				uint num = x[i] - 1U;
				z[i] = num;
				i++;
				if (num != 4294967295U)
				{
					while (i < len)
					{
						z[i] = x[i];
						i++;
					}
					return 0;
				}
			}
			return -1;
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x001193E4 File Offset: 0x001193E4
		public static int DecAt(int len, uint[] z, int zPos)
		{
			for (int i = zPos; i < len; i++)
			{
				IntPtr intPtr;
				if ((z[(int)(intPtr = (IntPtr)i)] = z[(int)intPtr] - 1U) != 4294967295U)
				{
					return 0;
				}
			}
			return -1;
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x0011941C File Offset: 0x0011941C
		public static int DecAt(int len, uint[] z, int zOff, int zPos)
		{
			for (int i = zPos; i < len; i++)
			{
				IntPtr intPtr;
				if ((z[(int)(intPtr = (IntPtr)(zOff + i))] = z[(int)intPtr] - 1U) != 4294967295U)
				{
					return 0;
				}
			}
			return -1;
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x00119458 File Offset: 0x00119458
		public static bool Eq(int len, uint[] x, uint[] y)
		{
			for (int i = len - 1; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x00119488 File Offset: 0x00119488
		public static uint[] FromBigInteger(int bits, BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > bits)
			{
				throw new ArgumentException();
			}
			int len = bits + 31 >> 5;
			uint[] array = Nat.Create(len);
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (uint)x.IntValue;
				x = x.ShiftRight(32);
			}
			return array;
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x001194EC File Offset: 0x001194EC
		public static ulong[] FromBigInteger64(int bits, BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > bits)
			{
				throw new ArgumentException();
			}
			int len = bits + 63 >> 6;
			ulong[] array = Nat.Create64(len);
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (ulong)x.LongValue;
				x = x.ShiftRight(64);
			}
			return array;
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x00119550 File Offset: 0x00119550
		public static uint GetBit(uint[] x, int bit)
		{
			if (bit == 0)
			{
				return x[0] & 1U;
			}
			int num = bit >> 5;
			if (num < 0 || num >= x.Length)
			{
				return 0U;
			}
			int num2 = bit & 31;
			return x[num] >> num2 & 1U;
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x00119594 File Offset: 0x00119594
		public static bool Gte(int len, uint[] x, uint[] y)
		{
			for (int i = len - 1; i >= 0; i--)
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

		// Token: 0x0600357B RID: 13691 RVA: 0x001195D4 File Offset: 0x001195D4
		public static uint Inc(int len, uint[] z)
		{
			for (int i = 0; i < len; i++)
			{
				IntPtr intPtr;
				if ((z[(int)(intPtr = (IntPtr)i)] = z[(int)intPtr] + 1U) != 0U)
				{
					return 0U;
				}
			}
			return 1U;
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x0011960C File Offset: 0x0011960C
		public static uint Inc(int len, uint[] x, uint[] z)
		{
			int i = 0;
			while (i < len)
			{
				uint num = x[i] + 1U;
				z[i] = num;
				i++;
				if (num != 0U)
				{
					while (i < len)
					{
						z[i] = x[i];
						i++;
					}
					return 0U;
				}
			}
			return 1U;
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x00119654 File Offset: 0x00119654
		public static uint IncAt(int len, uint[] z, int zPos)
		{
			for (int i = zPos; i < len; i++)
			{
				IntPtr intPtr;
				if ((z[(int)(intPtr = (IntPtr)i)] = z[(int)intPtr] + 1U) != 0U)
				{
					return 0U;
				}
			}
			return 1U;
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x0011968C File Offset: 0x0011968C
		public static uint IncAt(int len, uint[] z, int zOff, int zPos)
		{
			for (int i = zPos; i < len; i++)
			{
				IntPtr intPtr;
				if ((z[(int)(intPtr = (IntPtr)(zOff + i))] = z[(int)intPtr] + 1U) != 0U)
				{
					return 0U;
				}
			}
			return 1U;
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x001196C8 File Offset: 0x001196C8
		public static bool IsOne(int len, uint[] x)
		{
			if (x[0] != 1U)
			{
				return false;
			}
			for (int i = 1; i < len; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x00119700 File Offset: 0x00119700
		public static bool IsZero(int len, uint[] x)
		{
			if (x[0] != 0U)
			{
				return false;
			}
			for (int i = 1; i < len; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x00119738 File Offset: 0x00119738
		public static void Mul(int len, uint[] x, uint[] y, uint[] zz)
		{
			zz[len] = Nat.MulWord(len, x[0], y, zz);
			for (int i = 1; i < len; i++)
			{
				zz[i + len] = Nat.MulWordAddTo(len, x[i], y, 0, zz, i);
			}
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x00119778 File Offset: 0x00119778
		public static void Mul(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			zz[zzOff + len] = Nat.MulWord(len, x[xOff], y, yOff, zz, zzOff);
			for (int i = 1; i < len; i++)
			{
				zz[zzOff + i + len] = Nat.MulWordAddTo(len, x[xOff + i], y, yOff, zz, zzOff + i);
			}
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x001197CC File Offset: 0x001197CC
		public static void Mul(uint[] x, int xOff, int xLen, uint[] y, int yOff, int yLen, uint[] zz, int zzOff)
		{
			zz[zzOff + yLen] = Nat.MulWord(yLen, x[xOff], y, yOff, zz, zzOff);
			for (int i = 1; i < xLen; i++)
			{
				zz[zzOff + i + yLen] = Nat.MulWordAddTo(yLen, x[xOff + i], y, yOff, zz, zzOff + i);
			}
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x00119824 File Offset: 0x00119824
		public static uint MulAddTo(int len, uint[] x, uint[] y, uint[] zz)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += ((ulong)Nat.MulWordAddTo(len, x[i], y, 0, zz, i) & (ulong)-1);
				num += ((ulong)zz[i + len] & (ulong)-1);
				zz[i + len] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x00119878 File Offset: 0x00119878
		public static uint MulAddTo(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += ((ulong)Nat.MulWordAddTo(len, x[xOff + i], y, yOff, zz, zzOff) & (ulong)-1);
				num += ((ulong)zz[zzOff + len] & (ulong)-1);
				zz[zzOff + len] = (uint)num;
				num >>= 32;
				zzOff++;
			}
			return (uint)num;
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x001198D8 File Offset: 0x001198D8
		public static uint Mul31BothAdd(int len, uint a, uint[] x, uint b, uint[] y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)a;
			ulong num3 = (ulong)b;
			int num4 = 0;
			do
			{
				num += num2 * (ulong)x[num4] + num3 * (ulong)y[num4] + (ulong)z[zOff + num4];
				z[zOff + num4] = (uint)num;
				num >>= 32;
			}
			while (++num4 < len);
			return (uint)num;
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x00119928 File Offset: 0x00119928
		public static uint MulWord(int len, uint x, uint[] y, uint[] z)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			int num3 = 0;
			do
			{
				num += num2 * (ulong)y[num3];
				z[num3] = (uint)num;
				num >>= 32;
			}
			while (++num3 < len);
			return (uint)num;
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x00119960 File Offset: 0x00119960
		public static uint MulWord(int len, uint x, uint[] y, int yOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			int num3 = 0;
			do
			{
				num += num2 * (ulong)y[yOff + num3];
				z[zOff + num3] = (uint)num;
				num >>= 32;
			}
			while (++num3 < len);
			return (uint)num;
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x0011999C File Offset: 0x0011999C
		public static uint MulWordAddTo(int len, uint x, uint[] y, int yOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			int num3 = 0;
			do
			{
				num += num2 * (ulong)y[yOff + num3] + (ulong)z[zOff + num3];
				z[zOff + num3] = (uint)num;
				num >>= 32;
			}
			while (++num3 < len);
			return (uint)num;
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x001199E4 File Offset: 0x001199E4
		public static uint MulWordDwordAddAt(int len, uint x, ulong y, uint[] z, int zPos)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			num += num2 * (ulong)((uint)y) + (ulong)z[zPos];
			z[zPos] = (uint)num;
			num >>= 32;
			num += num2 * (y >> 32) + (ulong)z[zPos + 1];
			z[zPos + 1] = (uint)num;
			num >>= 32;
			num += (ulong)z[zPos + 2];
			z[zPos + 2] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zPos + 3);
			}
			return 0U;
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x00119A60 File Offset: 0x00119A60
		public static uint ShiftDownBit(int len, uint[] z, uint c)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = z[num];
				z[num] = (num2 >> 1 | c << 31);
				c = num2;
			}
			return c << 31;
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x00119A98 File Offset: 0x00119A98
		public static uint ShiftDownBit(int len, uint[] z, int zOff, uint c)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = z[zOff + num];
				z[zOff + num] = (num2 >> 1 | c << 31);
				c = num2;
			}
			return c << 31;
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x00119AD4 File Offset: 0x00119AD4
		public static uint ShiftDownBit(int len, uint[] x, uint c, uint[] z)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = x[num];
				z[num] = (num2 >> 1 | c << 31);
				c = num2;
			}
			return c << 31;
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x00119B0C File Offset: 0x00119B0C
		public static uint ShiftDownBit(int len, uint[] x, int xOff, uint c, uint[] z, int zOff)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = x[xOff + num];
				z[zOff + num] = (num2 >> 1 | c << 31);
				c = num2;
			}
			return c << 31;
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x00119B48 File Offset: 0x00119B48
		public static uint ShiftDownBits(int len, uint[] z, int bits, uint c)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = z[num];
				z[num] = (num2 >> bits | c << -bits);
				c = num2;
			}
			return c << -bits;
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x00119B88 File Offset: 0x00119B88
		public static uint ShiftDownBits(int len, uint[] z, int zOff, int bits, uint c)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = z[zOff + num];
				z[zOff + num] = (num2 >> bits | c << -bits);
				c = num2;
			}
			return c << -bits;
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x00119BD0 File Offset: 0x00119BD0
		public static uint ShiftDownBits(int len, uint[] x, int bits, uint c, uint[] z)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = x[num];
				z[num] = (num2 >> bits | c << -bits);
				c = num2;
			}
			return c << -bits;
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x00119C10 File Offset: 0x00119C10
		public static uint ShiftDownBits(int len, uint[] x, int xOff, int bits, uint c, uint[] z, int zOff)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = x[xOff + num];
				z[zOff + num] = (num2 >> bits | c << -bits);
				c = num2;
			}
			return c << -bits;
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x00119C58 File Offset: 0x00119C58
		public static uint ShiftDownWord(int len, uint[] z, uint c)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = z[num];
				z[num] = c;
				c = num2;
			}
			return c;
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x00119C84 File Offset: 0x00119C84
		public static uint ShiftUpBit(int len, uint[] z, uint c)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = z[i];
				z[i] = (num << 1 | c >> 31);
				c = num;
			}
			return c >> 31;
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x00119CBC File Offset: 0x00119CBC
		public static uint ShiftUpBit(int len, uint[] z, int zOff, uint c)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = z[zOff + i];
				z[zOff + i] = (num << 1 | c >> 31);
				c = num;
			}
			return c >> 31;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x00119CF8 File Offset: 0x00119CF8
		public static uint ShiftUpBit(int len, uint[] x, uint c, uint[] z)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = x[i];
				z[i] = (num << 1 | c >> 31);
				c = num;
			}
			return c >> 31;
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x00119D30 File Offset: 0x00119D30
		public static uint ShiftUpBit(int len, uint[] x, int xOff, uint c, uint[] z, int zOff)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = x[xOff + i];
				z[zOff + i] = (num << 1 | c >> 31);
				c = num;
			}
			return c >> 31;
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x00119D6C File Offset: 0x00119D6C
		public static ulong ShiftUpBit64(int len, ulong[] x, int xOff, ulong c, ulong[] z, int zOff)
		{
			for (int i = 0; i < len; i++)
			{
				ulong num = x[xOff + i];
				z[zOff + i] = (num << 1 | c >> 63);
				c = num;
			}
			return c >> 63;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x00119DA8 File Offset: 0x00119DA8
		public static uint ShiftUpBits(int len, uint[] z, int bits, uint c)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = z[i];
				z[i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x00119DE8 File Offset: 0x00119DE8
		public static uint ShiftUpBits(int len, uint[] z, int zOff, int bits, uint c)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = z[zOff + i];
				z[zOff + i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x00119E30 File Offset: 0x00119E30
		public static ulong ShiftUpBits64(int len, ulong[] z, int zOff, int bits, ulong c)
		{
			for (int i = 0; i < len; i++)
			{
				ulong num = z[zOff + i];
				z[zOff + i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x00119E78 File Offset: 0x00119E78
		public static uint ShiftUpBits(int len, uint[] x, int bits, uint c, uint[] z)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = x[i];
				z[i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x00119EB8 File Offset: 0x00119EB8
		public static uint ShiftUpBits(int len, uint[] x, int xOff, int bits, uint c, uint[] z, int zOff)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = x[xOff + i];
				z[zOff + i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x00119F00 File Offset: 0x00119F00
		public static ulong ShiftUpBits64(int len, ulong[] x, int xOff, int bits, ulong c, ulong[] z, int zOff)
		{
			for (int i = 0; i < len; i++)
			{
				ulong num = x[xOff + i];
				z[zOff + i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x00119F48 File Offset: 0x00119F48
		public static void Square(int len, uint[] x, uint[] zz)
		{
			int num = len << 1;
			uint num2 = 0U;
			int num3 = len;
			int num4 = num;
			do
			{
				ulong num5 = (ulong)x[--num3];
				ulong num6 = num5 * num5;
				zz[--num4] = (num2 << 31 | (uint)(num6 >> 33));
				zz[--num4] = (uint)(num6 >> 1);
				num2 = (uint)num6;
			}
			while (num3 > 0);
			ulong num7 = 0UL;
			int num8 = 2;
			for (int i = 1; i < len; i++)
			{
				num7 += (ulong)Nat.SquareWordAddTo(x, i, zz);
				num7 += (ulong)zz[num8];
				zz[num8++] = (uint)num7;
				num7 >>= 32;
				num7 += (ulong)zz[num8];
				zz[num8++] = (uint)num7;
				num7 >>= 32;
			}
			Nat.ShiftUpBit(num, zz, x[0] << 31);
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x0011A00C File Offset: 0x0011A00C
		public static void Square(int len, uint[] x, int xOff, uint[] zz, int zzOff)
		{
			int num = len << 1;
			uint num2 = 0U;
			int num3 = len;
			int num4 = num;
			do
			{
				ulong num5 = (ulong)x[xOff + --num3];
				ulong num6 = num5 * num5;
				zz[zzOff + --num4] = (num2 << 31 | (uint)(num6 >> 33));
				zz[zzOff + --num4] = (uint)(num6 >> 1);
				num2 = (uint)num6;
			}
			while (num3 > 0);
			ulong num7 = 0UL;
			int num8 = zzOff + 2;
			for (int i = 1; i < len; i++)
			{
				num7 += (ulong)Nat.SquareWordAddTo(x, xOff, i, zz, zzOff);
				num7 += (ulong)zz[num8];
				zz[num8++] = (uint)num7;
				num7 >>= 32;
				num7 += (ulong)zz[num8];
				zz[num8++] = (uint)num7;
				num7 >>= 32;
			}
			Nat.ShiftUpBit(num, zz, zzOff, x[xOff] << 31);
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x0011A0E0 File Offset: 0x0011A0E0
		[Obsolete("Use 'SquareWordAddTo' instead")]
		public static uint SquareWordAdd(uint[] x, int xPos, uint[] z)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x[xPos];
			int num3 = 0;
			do
			{
				num += num2 * (ulong)x[num3] + (ulong)z[xPos + num3];
				z[xPos + num3] = (uint)num;
				num >>= 32;
			}
			while (++num3 < xPos);
			return (uint)num;
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x0011A124 File Offset: 0x0011A124
		[Obsolete("Use 'SquareWordAddTo' instead")]
		public static uint SquareWordAdd(uint[] x, int xOff, int xPos, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x[xOff + xPos];
			int num3 = 0;
			do
			{
				num += num2 * ((ulong)x[xOff + num3] & (ulong)-1) + ((ulong)z[xPos + zOff] & (ulong)-1);
				z[xPos + zOff] = (uint)num;
				num >>= 32;
				zOff++;
			}
			while (++num3 < xPos);
			return (uint)num;
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x0011A178 File Offset: 0x0011A178
		public static uint SquareWordAddTo(uint[] x, int xPos, uint[] z)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x[xPos];
			int num3 = 0;
			do
			{
				num += num2 * (ulong)x[num3] + (ulong)z[xPos + num3];
				z[xPos + num3] = (uint)num;
				num >>= 32;
			}
			while (++num3 < xPos);
			return (uint)num;
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x0011A1BC File Offset: 0x0011A1BC
		public static uint SquareWordAddTo(uint[] x, int xOff, int xPos, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x[xOff + xPos];
			int num3 = 0;
			do
			{
				num += num2 * ((ulong)x[xOff + num3] & (ulong)-1) + ((ulong)z[xPos + zOff] & (ulong)-1);
				z[xPos + zOff] = (uint)num;
				num >>= 32;
				zOff++;
			}
			while (++num3 < xPos);
			return (uint)num;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x0011A210 File Offset: 0x0011A210
		public static int Sub(int len, uint[] x, uint[] y, uint[] z)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)x[i] - (ulong)y[i]);
				z[i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x0011A24C File Offset: 0x0011A24C
		public static int Sub(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)x[xOff + i] - (ulong)y[yOff + i]);
				z[zOff + i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x0011A290 File Offset: 0x0011A290
		public static int Sub33At(int len, uint x, uint[] z, int zPos)
		{
			long num = (long)((ulong)z[zPos] - (ulong)x);
			z[zPos] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zPos + 1] - 1UL);
			z[zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zPos + 2);
			}
			return 0;
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x0011A2E0 File Offset: 0x0011A2E0
		public static int Sub33At(int len, uint x, uint[] z, int zOff, int zPos)
		{
			long num = (long)((ulong)z[zOff + zPos] - (ulong)x);
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + zPos + 1] - 1UL);
			z[zOff + zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, zPos + 2);
			}
			return 0;
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x0011A340 File Offset: 0x0011A340
		public static int Sub33From(int len, uint x, uint[] z)
		{
			long num = (long)((ulong)z[0] - (ulong)x);
			z[0] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[1] - 1UL);
			z[1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, 2);
			}
			return 0;
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x0011A38C File Offset: 0x0011A38C
		public static int Sub33From(int len, uint x, uint[] z, int zOff)
		{
			long num = (long)((ulong)z[zOff] - (ulong)x);
			z[zOff] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 1] - 1UL);
			z[zOff + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, 2);
			}
			return 0;
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x0011A3DC File Offset: 0x0011A3DC
		public static int SubBothFrom(int len, uint[] x, uint[] y, uint[] z)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)z[i] - (ulong)x[i] - (ulong)y[i]);
				z[i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x0011A41C File Offset: 0x0011A41C
		public static int SubBothFrom(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)z[zOff + i] - (ulong)x[xOff + i] - (ulong)y[yOff + i]);
				z[zOff + i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x0011A46C File Offset: 0x0011A46C
		public static int SubDWordAt(int len, ulong x, uint[] z, int zPos)
		{
			long num = (long)((ulong)z[zPos] - (x & (ulong)-1));
			z[zPos] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zPos + 1] - (x >> 32));
			z[zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zPos + 2);
			}
			return 0;
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x0011A4C0 File Offset: 0x0011A4C0
		public static int SubDWordAt(int len, ulong x, uint[] z, int zOff, int zPos)
		{
			long num = (long)((ulong)z[zOff + zPos] - (x & (ulong)-1));
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + zPos + 1] - (x >> 32));
			z[zOff + zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, zPos + 2);
			}
			return 0;
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x0011A524 File Offset: 0x0011A524
		public static int SubDWordFrom(int len, ulong x, uint[] z)
		{
			long num = (long)((ulong)z[0] - (x & (ulong)-1));
			z[0] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[1] - (x >> 32));
			z[1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, 2);
			}
			return 0;
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x0011A574 File Offset: 0x0011A574
		public static int SubDWordFrom(int len, ulong x, uint[] z, int zOff)
		{
			long num = (long)((ulong)z[zOff] - (x & (ulong)-1));
			z[zOff] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 1] - (x >> 32));
			z[zOff + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, 2);
			}
			return 0;
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x0011A5C8 File Offset: 0x0011A5C8
		public static int SubFrom(int len, uint[] x, uint[] z)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)z[i] - (ulong)x[i]);
				z[i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x0011A604 File Offset: 0x0011A604
		public static int SubFrom(int len, uint[] x, int xOff, uint[] z, int zOff)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)z[zOff + i] - (ulong)x[xOff + i]);
				z[zOff + i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x0011A648 File Offset: 0x0011A648
		public static int SubWordAt(int len, uint x, uint[] z, int zPos)
		{
			long num = (long)((ulong)z[zPos] - (ulong)x);
			z[zPos] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zPos + 1);
			}
			return 0;
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x0011A680 File Offset: 0x0011A680
		public static int SubWordAt(int len, uint x, uint[] z, int zOff, int zPos)
		{
			long num = (long)((ulong)z[zOff + zPos] - (ulong)x);
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, zPos + 1);
			}
			return 0;
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x0011A6C0 File Offset: 0x0011A6C0
		public static int SubWordFrom(int len, uint x, uint[] z)
		{
			long num = (long)((ulong)z[0] - (ulong)x);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, 1);
			}
			return 0;
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x0011A6F8 File Offset: 0x0011A6F8
		public static int SubWordFrom(int len, uint x, uint[] z, int zOff)
		{
			long num = (long)((ulong)z[zOff] - (ulong)x);
			z[zOff] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, 1);
			}
			return 0;
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x0011A730 File Offset: 0x0011A730
		public static BigInteger ToBigInteger(int len, uint[] x)
		{
			byte[] array = new byte[len << 2];
			for (int i = 0; i < len; i++)
			{
				uint num = x[i];
				if (num != 0U)
				{
					Pack.UInt32_To_BE(num, array, len - 1 - i << 2);
				}
			}
			return new BigInteger(1, array);
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x0011A778 File Offset: 0x0011A778
		public static void Zero(int len, uint[] z)
		{
			for (int i = 0; i < len; i++)
			{
				z[i] = 0U;
			}
		}

		// Token: 0x04001D21 RID: 7457
		private const ulong M = 4294967295UL;
	}
}
