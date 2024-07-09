using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003D5 RID: 981
	public class RsaBlindingFactorGenerator
	{
		// Token: 0x06001F00 RID: 7936 RVA: 0x000B5E9C File Offset: 0x000B5E9C
		public void Init(ICipherParameters param)
		{
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.key = (RsaKeyParameters)parametersWithRandom.Parameters;
				this.random = parametersWithRandom.Random;
			}
			else
			{
				this.key = (RsaKeyParameters)param;
				this.random = new SecureRandom();
			}
			if (this.key.IsPrivate)
			{
				throw new ArgumentException("generator requires RSA public key");
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000B5F14 File Offset: 0x000B5F14
		public BigInteger GenerateBlindingFactor()
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("generator not initialised");
			}
			BigInteger modulus = this.key.Modulus;
			int sizeInBits = modulus.BitLength - 1;
			BigInteger bigInteger;
			BigInteger bigInteger2;
			do
			{
				bigInteger = new BigInteger(sizeInBits, this.random);
				bigInteger2 = bigInteger.Gcd(modulus);
			}
			while (bigInteger.SignValue == 0 || bigInteger.Equals(BigInteger.One) || !bigInteger2.Equals(BigInteger.One));
			return bigInteger;
		}

		// Token: 0x04001476 RID: 5238
		private RsaKeyParameters key;

		// Token: 0x04001477 RID: 5239
		private SecureRandom random;
	}
}
