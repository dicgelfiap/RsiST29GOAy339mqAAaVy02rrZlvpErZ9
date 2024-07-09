using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003B7 RID: 951
	public class DHBasicKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001E51 RID: 7761 RVA: 0x000B1734 File Offset: 0x000B1734
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.param = (DHKeyGenerationParameters)parameters;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000B1744 File Offset: 0x000B1744
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DHKeyGeneratorHelper instance = DHKeyGeneratorHelper.Instance;
			DHParameters parameters = this.param.Parameters;
			BigInteger x = instance.CalculatePrivate(parameters, this.param.Random);
			BigInteger y = instance.CalculatePublic(parameters, x);
			return new AsymmetricCipherKeyPair(new DHPublicKeyParameters(y, parameters), new DHPrivateKeyParameters(x, parameters));
		}

		// Token: 0x04001418 RID: 5144
		private DHKeyGenerationParameters param;
	}
}
