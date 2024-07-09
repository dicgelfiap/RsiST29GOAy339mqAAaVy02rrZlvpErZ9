using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003D9 RID: 985
	public class X448KeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001F15 RID: 7957 RVA: 0x000B6698 File Offset: 0x000B6698
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000B66A8 File Offset: 0x000B66A8
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			X448PrivateKeyParameters x448PrivateKeyParameters = new X448PrivateKeyParameters(this.random);
			X448PublicKeyParameters publicParameter = x448PrivateKeyParameters.GeneratePublicKey();
			return new AsymmetricCipherKeyPair(publicParameter, x448PrivateKeyParameters);
		}

		// Token: 0x04001480 RID: 5248
		private SecureRandom random;
	}
}
