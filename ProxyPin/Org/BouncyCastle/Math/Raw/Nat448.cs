using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Math.Raw
{
	// Token: 0x0200062B RID: 1579
	internal abstract class Nat448
	{
		// Token: 0x060036A5 RID: 13989 RVA: 0x00123A4C File Offset: 0x00123A4C
		public static void Copy64(ulong[] x, ulong[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
			z[4] = x[4];
			z[5] = x[5];
			z[6] = x[6];
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x00123A78 File Offset: 0x00123A78
		public static void Copy64(ulong[] x, int xOff, ulong[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
			z[zOff + 4] = x[xOff + 4];
			z[zOff + 5] = x[xOff + 5];
			z[zOff + 6] = x[xOff + 6];
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x00123ACC File Offset: 0x00123ACC
		public static ulong[] Create64()
		{
			return new ulong[7];
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x00123AD4 File Offset: 0x00123AD4
		public static ulong[] CreateExt64()
		{
			return new ulong[14];
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x00123AE0 File Offset: 0x00123AE0
		public static bool Eq64(ulong[] x, ulong[] y)
		{
			for (int i = 6; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x00123B10 File Offset: 0x00123B10
		public static ulong[] FromBigInteger64(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 448)
			{
				throw new ArgumentException();
			}
			ulong[] array = Nat448.Create64();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (ulong)x.LongValue;
				x = x.ShiftRight(64);
			}
			return array;
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x00123B70 File Offset: 0x00123B70
		public static bool IsOne64(ulong[] x)
		{
			if (x[0] != 1UL)
			{
				return false;
			}
			for (int i = 1; i < 7; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x00123BAC File Offset: 0x00123BAC
		public static bool IsZero64(ulong[] x)
		{
			for (int i = 0; i < 7; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x00123BDC File Offset: 0x00123BDC
		public static BigInteger ToBigInteger64(ulong[] x)
		{
			byte[] array = new byte[56];
			for (int i = 0; i < 7; i++)
			{
				ulong num = x[i];
				if (num != 0UL)
				{
					Pack.UInt64_To_BE(num, array, 6 - i << 3);
				}
			}
			return new BigInteger(1, array);
		}
	}
}
