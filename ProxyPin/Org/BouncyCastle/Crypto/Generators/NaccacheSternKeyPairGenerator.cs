using System;
using System.Collections;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003CD RID: 973
	public class NaccacheSternKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001EB8 RID: 7864 RVA: 0x000B45FC File Offset: 0x000B45FC
		public void Init(KeyGenerationParameters parameters)
		{
			this.param = (NaccacheSternKeyGenerationParameters)parameters;
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000B460C File Offset: 0x000B460C
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			int strength = this.param.Strength;
			SecureRandom random = this.param.Random;
			int certainty = this.param.Certainty;
			IList list = NaccacheSternKeyPairGenerator.findFirstPrimes(this.param.CountSmallPrimes);
			list = NaccacheSternKeyPairGenerator.permuteList(list, random);
			BigInteger bigInteger = BigInteger.One;
			BigInteger bigInteger2 = BigInteger.One;
			for (int i = 0; i < list.Count / 2; i++)
			{
				bigInteger = bigInteger.Multiply((BigInteger)list[i]);
			}
			for (int j = list.Count / 2; j < list.Count; j++)
			{
				bigInteger2 = bigInteger2.Multiply((BigInteger)list[j]);
			}
			BigInteger bigInteger3 = bigInteger.Multiply(bigInteger2);
			int num = strength - bigInteger3.BitLength - 48;
			BigInteger bigInteger4 = NaccacheSternKeyPairGenerator.generatePrime(num / 2 + 1, certainty, random);
			BigInteger bigInteger5 = NaccacheSternKeyPairGenerator.generatePrime(num / 2 + 1, certainty, random);
			long num2 = 0L;
			BigInteger val = bigInteger4.Multiply(bigInteger).ShiftLeft(1);
			BigInteger val2 = bigInteger5.Multiply(bigInteger2).ShiftLeft(1);
			BigInteger bigInteger6;
			BigInteger bigInteger7;
			BigInteger bigInteger8;
			BigInteger bigInteger9;
			for (;;)
			{
				num2 += 1L;
				bigInteger6 = NaccacheSternKeyPairGenerator.generatePrime(24, certainty, random);
				bigInteger7 = bigInteger6.Multiply(val).Add(BigInteger.One);
				if (bigInteger7.IsProbablePrime(certainty, true))
				{
					for (;;)
					{
						bigInteger8 = NaccacheSternKeyPairGenerator.generatePrime(24, certainty, random);
						if (!bigInteger6.Equals(bigInteger8))
						{
							bigInteger9 = bigInteger8.Multiply(val2).Add(BigInteger.One);
							if (bigInteger9.IsProbablePrime(certainty, true))
							{
								break;
							}
						}
					}
					if (bigInteger3.Gcd(bigInteger6.Multiply(bigInteger8)).Equals(BigInteger.One) && bigInteger7.Multiply(bigInteger9).BitLength >= strength)
					{
						break;
					}
				}
			}
			BigInteger bigInteger10 = bigInteger7.Multiply(bigInteger9);
			BigInteger bigInteger11 = bigInteger7.Subtract(BigInteger.One).Multiply(bigInteger9.Subtract(BigInteger.One));
			num2 = 0L;
			BigInteger bigInteger12;
			bool flag;
			do
			{
				IList list2 = Platform.CreateArrayList();
				for (int num3 = 0; num3 != list.Count; num3++)
				{
					BigInteger val3 = (BigInteger)list[num3];
					BigInteger e = bigInteger11.Divide(val3);
					do
					{
						num2 += 1L;
						bigInteger12 = NaccacheSternKeyPairGenerator.generatePrime(strength, certainty, random);
					}
					while (bigInteger12.ModPow(e, bigInteger10).Equals(BigInteger.One));
					list2.Add(bigInteger12);
				}
				bigInteger12 = BigInteger.One;
				for (int k = 0; k < list.Count; k++)
				{
					BigInteger bigInteger13 = (BigInteger)list2[k];
					BigInteger val4 = (BigInteger)list[k];
					bigInteger12 = bigInteger12.Multiply(bigInteger13.ModPow(bigInteger3.Divide(val4), bigInteger10)).Mod(bigInteger10);
				}
				flag = false;
				for (int l = 0; l < list.Count; l++)
				{
					if (bigInteger12.ModPow(bigInteger11.Divide((BigInteger)list[l]), bigInteger10).Equals(BigInteger.One))
					{
						flag = true;
						break;
					}
				}
			}
			while (flag || bigInteger12.ModPow(bigInteger11.ShiftRight(2), bigInteger10).Equals(BigInteger.One) || bigInteger12.ModPow(bigInteger11.Divide(bigInteger6), bigInteger10).Equals(BigInteger.One) || bigInteger12.ModPow(bigInteger11.Divide(bigInteger8), bigInteger10).Equals(BigInteger.One) || bigInteger12.ModPow(bigInteger11.Divide(bigInteger4), bigInteger10).Equals(BigInteger.One) || bigInteger12.ModPow(bigInteger11.Divide(bigInteger5), bigInteger10).Equals(BigInteger.One));
			return new AsymmetricCipherKeyPair(new NaccacheSternKeyParameters(false, bigInteger12, bigInteger10, bigInteger3.BitLength), new NaccacheSternPrivateKeyParameters(bigInteger12, bigInteger10, bigInteger3.BitLength, list, bigInteger11));
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x000B49E4 File Offset: 0x000B49E4
		private static BigInteger generatePrime(int bitLength, int certainty, SecureRandom rand)
		{
			return new BigInteger(bitLength, certainty, rand);
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000B49F0 File Offset: 0x000B49F0
		private static IList permuteList(IList arr, SecureRandom rand)
		{
			IList list = Platform.CreateArrayList(arr.Count);
			foreach (object value in arr)
			{
				int index = rand.Next(list.Count + 1);
				list.Insert(index, value);
			}
			return list;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000B4A68 File Offset: 0x000B4A68
		private static IList findFirstPrimes(int count)
		{
			IList list = Platform.CreateArrayList(count);
			for (int num = 0; num != count; num++)
			{
				list.Add(BigInteger.ValueOf((long)NaccacheSternKeyPairGenerator.smallPrimes[num]));
			}
			return list;
		}

		// Token: 0x04001461 RID: 5217
		private static readonly int[] smallPrimes = new int[]
		{
			3,
			5,
			7,
			11,
			13,
			17,
			19,
			23,
			29,
			31,
			37,
			41,
			43,
			47,
			53,
			59,
			61,
			67,
			71,
			73,
			79,
			83,
			89,
			97,
			101,
			103,
			107,
			109,
			113,
			127,
			131,
			137,
			139,
			149,
			151,
			157,
			163,
			167,
			173,
			179,
			181,
			191,
			193,
			197,
			199,
			211,
			223,
			227,
			229,
			233,
			239,
			241,
			251,
			257,
			263,
			269,
			271,
			277,
			281,
			283,
			293,
			307,
			311,
			313,
			317,
			331,
			337,
			347,
			349,
			353,
			359,
			367,
			373,
			379,
			383,
			389,
			397,
			401,
			409,
			419,
			421,
			431,
			433,
			439,
			443,
			449,
			457,
			461,
			463,
			467,
			479,
			487,
			491,
			499,
			503,
			509,
			521,
			523,
			541,
			547,
			557
		};

		// Token: 0x04001462 RID: 5218
		private NaccacheSternKeyGenerationParameters param;
	}
}
