using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003C0 RID: 960
	public class Ed448KeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001E81 RID: 7809 RVA: 0x000B2788 File Offset: 0x000B2788
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x000B2798 File Offset: 0x000B2798
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			Ed448PrivateKeyParameters ed448PrivateKeyParameters = new Ed448PrivateKeyParameters(this.random);
			Ed448PublicKeyParameters publicParameter = ed448PrivateKeyParameters.GeneratePublicKey();
			return new AsymmetricCipherKeyPair(publicParameter, ed448PrivateKeyParameters);
		}

		// Token: 0x04001430 RID: 5168
		private SecureRandom random;
	}
}
