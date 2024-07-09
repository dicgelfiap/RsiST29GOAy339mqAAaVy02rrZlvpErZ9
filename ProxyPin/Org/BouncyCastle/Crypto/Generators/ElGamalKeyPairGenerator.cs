using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003C1 RID: 961
	public class ElGamalKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001E84 RID: 7812 RVA: 0x000B27CC File Offset: 0x000B27CC
		public void Init(KeyGenerationParameters parameters)
		{
			this.param = (ElGamalKeyGenerationParameters)parameters;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x000B27DC File Offset: 0x000B27DC
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DHKeyGeneratorHelper instance = DHKeyGeneratorHelper.Instance;
			ElGamalParameters parameters = this.param.Parameters;
			DHParameters dhParams = new DHParameters(parameters.P, parameters.G, null, 0, parameters.L);
			BigInteger x = instance.CalculatePrivate(dhParams, this.param.Random);
			BigInteger y = instance.CalculatePublic(dhParams, x);
			return new AsymmetricCipherKeyPair(new ElGamalPublicKeyParameters(y, parameters), new ElGamalPrivateKeyParameters(x, parameters));
		}

		// Token: 0x04001431 RID: 5169
		private ElGamalKeyGenerationParameters param;
	}
}
