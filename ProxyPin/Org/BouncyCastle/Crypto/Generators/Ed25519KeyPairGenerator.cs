using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003BF RID: 959
	public class Ed25519KeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001E7E RID: 7806 RVA: 0x000B2744 File Offset: 0x000B2744
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x000B2754 File Offset: 0x000B2754
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			Ed25519PrivateKeyParameters ed25519PrivateKeyParameters = new Ed25519PrivateKeyParameters(this.random);
			Ed25519PublicKeyParameters publicParameter = ed25519PrivateKeyParameters.GeneratePublicKey();
			return new AsymmetricCipherKeyPair(publicParameter, ed25519PrivateKeyParameters);
		}

		// Token: 0x0400142F RID: 5167
		private SecureRandom random;
	}
}
