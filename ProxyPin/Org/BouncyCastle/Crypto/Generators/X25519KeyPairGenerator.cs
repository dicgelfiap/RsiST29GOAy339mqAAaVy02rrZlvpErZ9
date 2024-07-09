using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003D8 RID: 984
	public class X25519KeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001F12 RID: 7954 RVA: 0x000B6654 File Offset: 0x000B6654
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000B6664 File Offset: 0x000B6664
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			X25519PrivateKeyParameters x25519PrivateKeyParameters = new X25519PrivateKeyParameters(this.random);
			X25519PublicKeyParameters publicParameter = x25519PrivateKeyParameters.GeneratePublicKey();
			return new AsymmetricCipherKeyPair(publicParameter, x25519PrivateKeyParameters);
		}

		// Token: 0x0400147F RID: 5247
		private SecureRandom random;
	}
}
