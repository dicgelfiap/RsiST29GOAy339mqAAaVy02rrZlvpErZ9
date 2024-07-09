using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003B9 RID: 953
	public class DHKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001E58 RID: 7768 RVA: 0x000B1874 File Offset: 0x000B1874
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.param = (DHKeyGenerationParameters)parameters;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x000B1884 File Offset: 0x000B1884
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DHKeyGeneratorHelper instance = DHKeyGeneratorHelper.Instance;
			DHParameters parameters = this.param.Parameters;
			BigInteger x = instance.CalculatePrivate(parameters, this.param.Random);
			BigInteger y = instance.CalculatePublic(parameters, x);
			return new AsymmetricCipherKeyPair(new DHPublicKeyParameters(y, parameters), new DHPrivateKeyParameters(x, parameters));
		}

		// Token: 0x0400141A RID: 5146
		private DHKeyGenerationParameters param;
	}
}
