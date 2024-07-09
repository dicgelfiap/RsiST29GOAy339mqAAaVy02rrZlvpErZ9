using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Math.Raw
{
	// Token: 0x0200062D RID: 1581
	internal abstract class Nat576
	{
		// Token: 0x060036B2 RID: 14002 RVA: 0x00123D70 File Offset: 0x00123D70
		public static void Copy64(ulong[] x, ulong[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
			z[4] = x[4];
			z[5] = x[5];
			z[6] = x[6];
			z[7] = x[7];
			z[8] = x[8];
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x00123DA8 File Offset: 0x00123DA8
		public static void Copy64(ulong[] x, int xOff, ulong[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
			z[zOff + 4] = x[xOff + 4];
			z[zOff + 5] = x[xOff + 5];
			z[zOff + 6] = x[xOff + 6];
			z[zOff + 7] = x[xOff + 7];
			z[zOff + 8] = x[xOff + 8];
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x00123E10 File Offset: 0x00123E10
		public static ulong[] Create64()
		{
			return new ulong[9];
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x00123E1C File Offset: 0x00123E1C
		public static ulong[] CreateExt64()
		{
			return new ulong[18];
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x00123E28 File Offset: 0x00123E28
		public static bool Eq64(ulong[] x, ulong[] y)
		{
			for (int i = 8; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x00123E58 File Offset: 0x00123E58
		public static ulong[] FromBigInteger64(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 576)
			{
				throw new ArgumentException();
			}
			ulong[] array = Nat576.Create64();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (ulong)x.LongValue;
				x = x.ShiftRight(64);
			}
			return array;
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x00123EB8 File Offset: 0x00123EB8
		public static bool IsOne64(ulong[] x)
		{
			if (x[0] != 1UL)
			{
				return false;
			}
			for (int i = 1; i < 9; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x00123EF4 File Offset: 0x00123EF4
		public static bool IsZero64(ulong[] x)
		{
			for (int i = 0; i < 9; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x00123F24 File Offset: 0x00123F24
		public static BigInteger ToBigInteger64(ulong[] x)
		{
			byte[] array = new byte[72];
			for (int i = 0; i < 9; i++)
			{
				ulong num = x[i];
				if (num != 0UL)
				{
					Pack.UInt64_To_BE(num, array, 8 - i << 3);
				}
			}
			return new BigInteger(1, array);
		}
	}
}
