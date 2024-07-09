using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003BB RID: 955
	internal class DHParametersHelper
	{
		// Token: 0x06001E5E RID: 7774 RVA: 0x000B1958 File Offset: 0x000B1958
		private static BigInteger[] ConstructBigPrimeProducts(int[] primeProducts)
		{
			BigInteger[] array = new BigInteger[primeProducts.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = BigInteger.ValueOf((long)primeProducts[i]);
			}
			return array;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x000B1994 File Offset: 0x000B1994
		internal static BigInteger[] GenerateSafePrimes(int size, int certainty, SecureRandom random)
		{
			int num = size - 1;
			int num2 = size >> 2;
			BigInteger bigInteger;
			BigInteger bigInteger2;
			if (size <= 32)
			{
				for (;;)
				{
					bigInteger = new BigInteger(num, 2, random);
					bigInteger2 = bigInteger.ShiftLeft(1).Add(BigInteger.One);
					if (bigInteger2.IsProbablePrime(certainty, true))
					{
						if (certainty <= 2 || bigInteger.IsProbablePrime(certainty, true))
						{
							break;
						}
					}
				}
			}
			else
			{
				for (;;)
				{
					bigInteger = new BigInteger(num, 0, random);
					for (;;)
					{
						IL_54:
						for (int i = 0; i < DHParametersHelper.primeLists.Length; i++)
						{
							int num3 = bigInteger.Remainder(DHParametersHelper.BigPrimeProducts[i]).IntValue;
							if (i == 0)
							{
								int num4 = num3 % 3;
								if (num4 != 2)
								{
									int num5 = 2 * num4 + 2;
									bigInteger = bigInteger.Add(BigInteger.ValueOf((long)num5));
									num3 = (num3 + num5) % DHParametersHelper.primeProducts[i];
								}
							}
							foreach (int num6 in DHParametersHelper.primeLists[i])
							{
								int num7 = num3 % num6;
								if (num7 == 0 || num7 == num6 >> 1)
								{
									bigInteger = bigInteger.Add(DHParametersHelper.Six);
									goto IL_54;
								}
							}
						}
						break;
					}
					if (bigInteger.BitLength == num && bigInteger.RabinMillerTest(2, random, true))
					{
						bigInteger2 = bigInteger.ShiftLeft(1).Add(BigInteger.One);
						if (bigInteger2.RabinMillerTest(certainty, random, true) && (certainty <= 2 || bigInteger.RabinMillerTest(certainty - 2, random, true)) && WNafUtilities.GetNafWeight(bigInteger2) >= num2)
						{
							break;
						}
					}
				}
			}
			return new BigInteger[]
			{
				bigInteger2,
				bigInteger
			};
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000B1B38 File Offset: 0x000B1B38
		internal static BigInteger SelectGenerator(BigInteger p, BigInteger q, SecureRandom random)
		{
			BigInteger max = p.Subtract(BigInteger.Two);
			BigInteger bigInteger2;
			do
			{
				BigInteger bigInteger = BigIntegers.CreateRandomInRange(BigInteger.Two, max, random);
				bigInteger2 = bigInteger.ModPow(BigInteger.Two, p);
			}
			while (bigInteger2.Equals(BigInteger.One));
			return bigInteger2;
		}

		// Token: 0x0400141E RID: 5150
		private static readonly BigInteger Six = BigInteger.ValueOf(6L);

		// Token: 0x0400141F RID: 5151
		private static readonly int[][] primeLists = BigInteger.primeLists;

		// Token: 0x04001420 RID: 5152
		private static readonly int[] primeProducts = BigInteger.primeProducts;

		// Token: 0x04001421 RID: 5153
		private static readonly BigInteger[] BigPrimeProducts = DHParametersHelper.ConstructBigPrimeProducts(DHParametersHelper.primeProducts);
	}
}
