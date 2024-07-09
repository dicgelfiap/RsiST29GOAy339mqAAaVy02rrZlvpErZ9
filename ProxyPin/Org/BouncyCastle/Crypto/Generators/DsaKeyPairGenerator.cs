using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003BC RID: 956
	public class DsaKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001E63 RID: 7779 RVA: 0x000B1BBC File Offset: 0x000B1BBC
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.param = (DsaKeyGenerationParameters)parameters;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x000B1BDC File Offset: 0x000B1BDC
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DsaParameters parameters = this.param.Parameters;
			BigInteger x = DsaKeyPairGenerator.GeneratePrivateKey(parameters.Q, this.param.Random);
			BigInteger y = DsaKeyPairGenerator.CalculatePublicKey(parameters.P, parameters.G, x);
			return new AsymmetricCipherKeyPair(new DsaPublicKeyParameters(y, parameters), new DsaPrivateKeyParameters(x, parameters));
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000B1C38 File Offset: 0x000B1C38
		private static BigInteger GeneratePrivateKey(BigInteger q, SecureRandom random)
		{
			int num = q.BitLength >> 2;
			BigInteger bigInteger;
			do
			{
				bigInteger = BigIntegers.CreateRandomInRange(DsaKeyPairGenerator.One, q.Subtract(DsaKeyPairGenerator.One), random);
			}
			while (WNafUtilities.GetNafWeight(bigInteger) < num);
			return bigInteger;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000B1C74 File Offset: 0x000B1C74
		private static BigInteger CalculatePublicKey(BigInteger p, BigInteger g, BigInteger x)
		{
			return g.ModPow(x, p);
		}

		// Token: 0x04001422 RID: 5154
		private static readonly BigInteger One = BigInteger.One;

		// Token: 0x04001423 RID: 5155
		private DsaKeyGenerationParameters param;
	}
}
