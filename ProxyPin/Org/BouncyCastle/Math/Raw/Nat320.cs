using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Math.Raw
{
	// Token: 0x02000629 RID: 1577
	internal abstract class Nat320
	{
		// Token: 0x06003698 RID: 13976 RVA: 0x00123758 File Offset: 0x00123758
		public static void Copy64(ulong[] x, ulong[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
			z[4] = x[4];
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x00123778 File Offset: 0x00123778
		public static void Copy64(ulong[] x, int xOff, ulong[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
			z[zOff + 4] = x[xOff + 4];
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x001237A8 File Offset: 0x001237A8
		public static ulong[] Create64()
		{
			return new ulong[5];
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x001237B0 File Offset: 0x001237B0
		public static ulong[] CreateExt64()
		{
			return new ulong[10];
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x001237BC File Offset: 0x001237BC
		public static bool Eq64(ulong[] x, ulong[] y)
		{
			for (int i = 4; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x001237EC File Offset: 0x001237EC
		public static ulong[] FromBigInteger64(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 320)
			{
				throw new ArgumentException();
			}
			ulong[] array = Nat320.Create64();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (ulong)x.LongValue;
				x = x.ShiftRight(64);
			}
			return array;
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x0012384C File Offset: 0x0012384C
		public static bool IsOne64(ulong[] x)
		{
			if (x[0] != 1UL)
			{
				return false;
			}
			for (int i = 1; i < 5; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x00123888 File Offset: 0x00123888
		public static bool IsZero64(ulong[] x)
		{
			for (int i = 0; i < 5; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x001238B8 File Offset: 0x001238B8
		public static BigInteger ToBigInteger64(ulong[] x)
		{
			byte[] array = new byte[40];
			for (int i = 0; i < 5; i++)
			{
				ulong num = x[i];
				if (num != 0UL)
				{
					Pack.UInt64_To_BE(num, array, 4 - i << 3);
				}
			}
			return new BigInteger(1, array);
		}
	}
}
