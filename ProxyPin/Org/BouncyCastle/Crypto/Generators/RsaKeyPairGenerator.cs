using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003D6 RID: 982
	public class RsaKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001F03 RID: 7939 RVA: 0x000B5F90 File Offset: 0x000B5F90
		public virtual void Init(KeyGenerationParameters parameters)
		{
			if (parameters is RsaKeyGenerationParameters)
			{
				this.parameters = (RsaKeyGenerationParameters)parameters;
				return;
			}
			this.parameters = new RsaKeyGenerationParameters(RsaKeyPairGenerator.DefaultPublicExponent, parameters.Random, parameters.Strength, 100);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000B5FD8 File Offset: 0x000B5FD8
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			int num2;
			BigInteger publicExponent;
			BigInteger bigInteger;
			BigInteger bigInteger2;
			BigInteger bigInteger4;
			BigInteger bigInteger6;
			BigInteger bigInteger7;
			BigInteger bigInteger8;
			do
			{
				int strength = this.parameters.Strength;
				int num = (strength + 1) / 2;
				num2 = strength - num;
				int num3 = strength / 3;
				int num4 = strength >> 2;
				publicExponent = this.parameters.PublicExponent;
				bigInteger = this.ChooseRandomPrime(num, publicExponent);
				for (;;)
				{
					bigInteger2 = this.ChooseRandomPrime(num2, publicExponent);
					BigInteger bigInteger3 = bigInteger2.Subtract(bigInteger).Abs();
					if (bigInteger3.BitLength >= num3)
					{
						bigInteger4 = bigInteger.Multiply(bigInteger2);
						if (bigInteger4.BitLength != strength)
						{
							bigInteger = bigInteger.Max(bigInteger2);
						}
						else
						{
							if (WNafUtilities.GetNafWeight(bigInteger4) >= num4)
							{
								break;
							}
							bigInteger = this.ChooseRandomPrime(num, publicExponent);
						}
					}
				}
				if (bigInteger.CompareTo(bigInteger2) < 0)
				{
					BigInteger bigInteger5 = bigInteger;
					bigInteger = bigInteger2;
					bigInteger2 = bigInteger5;
				}
				bigInteger6 = bigInteger.Subtract(RsaKeyPairGenerator.One);
				bigInteger7 = bigInteger2.Subtract(RsaKeyPairGenerator.One);
				BigInteger val = bigInteger6.Gcd(bigInteger7);
				BigInteger m = bigInteger6.Divide(val).Multiply(bigInteger7);
				bigInteger8 = publicExponent.ModInverse(m);
			}
			while (bigInteger8.BitLength <= num2);
			BigInteger dP = bigInteger8.Remainder(bigInteger6);
			BigInteger dQ = bigInteger8.Remainder(bigInteger7);
			BigInteger qInv = bigInteger2.ModInverse(bigInteger);
			return new AsymmetricCipherKeyPair(new RsaKeyParameters(false, bigInteger4, publicExponent), new RsaPrivateCrtKeyParameters(bigInteger4, publicExponent, bigInteger8, bigInteger, bigInteger2, dP, dQ, qInv));
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000B6138 File Offset: 0x000B6138
		protected virtual BigInteger ChooseRandomPrime(int bitlength, BigInteger e)
		{
			bool flag = e.BitLength <= RsaKeyPairGenerator.SPECIAL_E_BITS && Arrays.Contains(RsaKeyPairGenerator.SPECIAL_E_VALUES, e.IntValue);
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(bitlength, 1, this.parameters.Random);
			}
			while (bigInteger.Mod(e).Equals(RsaKeyPairGenerator.One) || !bigInteger.IsProbablePrime(this.parameters.Certainty, true) || (!flag && !e.Gcd(bigInteger.Subtract(RsaKeyPairGenerator.One)).Equals(RsaKeyPairGenerator.One)));
			return bigInteger;
		}

		// Token: 0x04001478 RID: 5240
		protected const int DefaultTests = 100;

		// Token: 0x04001479 RID: 5241
		private static readonly int[] SPECIAL_E_VALUES = new int[]
		{
			3,
			5,
			17,
			257,
			65537
		};

		// Token: 0x0400147A RID: 5242
		private static readonly int SPECIAL_E_HIGHEST = RsaKeyPairGenerator.SPECIAL_E_VALUES[RsaKeyPairGenerator.SPECIAL_E_VALUES.Length - 1];

		// Token: 0x0400147B RID: 5243
		private static readonly int SPECIAL_E_BITS = BigInteger.ValueOf((long)RsaKeyPairGenerator.SPECIAL_E_HIGHEST).BitLength;

		// Token: 0x0400147C RID: 5244
		protected static readonly BigInteger One = BigInteger.One;

		// Token: 0x0400147D RID: 5245
		protected static readonly BigInteger DefaultPublicExponent = BigInteger.ValueOf(65537L);

		// Token: 0x0400147E RID: 5246
		protected RsaKeyGenerationParameters parameters;
	}
}
