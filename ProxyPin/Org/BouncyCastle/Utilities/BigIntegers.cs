using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x020006FC RID: 1788
	public abstract class BigIntegers
	{
		// Token: 0x06003EA7 RID: 16039 RVA: 0x00159818 File Offset: 0x00159818
		public static byte[] AsUnsignedByteArray(BigInteger n)
		{
			return n.ToByteArrayUnsigned();
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x00159820 File Offset: 0x00159820
		public static byte[] AsUnsignedByteArray(int length, BigInteger n)
		{
			byte[] array = n.ToByteArrayUnsigned();
			if (array.Length > length)
			{
				throw new ArgumentException("standard length exceeded", "n");
			}
			if (array.Length == length)
			{
				return array;
			}
			byte[] array2 = new byte[length];
			Array.Copy(array, 0, array2, array2.Length - array.Length, array.Length);
			return array2;
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x00159878 File Offset: 0x00159878
		public static BigInteger CreateRandomBigInteger(int bitLength, SecureRandom secureRandom)
		{
			return new BigInteger(bitLength, secureRandom);
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x00159884 File Offset: 0x00159884
		public static BigInteger CreateRandomInRange(BigInteger min, BigInteger max, SecureRandom random)
		{
			int num = min.CompareTo(max);
			if (num >= 0)
			{
				if (num > 0)
				{
					throw new ArgumentException("'min' may not be greater than 'max'");
				}
				return min;
			}
			else
			{
				if (min.BitLength > max.BitLength / 2)
				{
					return BigIntegers.CreateRandomInRange(BigInteger.Zero, max.Subtract(min), random).Add(min);
				}
				for (int i = 0; i < 1000; i++)
				{
					BigInteger bigInteger = new BigInteger(max.BitLength, random);
					if (bigInteger.CompareTo(min) >= 0 && bigInteger.CompareTo(max) <= 0)
					{
						return bigInteger;
					}
				}
				return new BigInteger(max.Subtract(min).BitLength - 1, random).Add(min);
			}
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x0015993C File Offset: 0x0015993C
		public static int GetUnsignedByteLength(BigInteger n)
		{
			return (n.BitLength + 7) / 8;
		}

		// Token: 0x0400207A RID: 8314
		private const int MaxIterations = 1000;
	}
}
