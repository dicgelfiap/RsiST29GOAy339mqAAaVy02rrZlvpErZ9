using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020003B6 RID: 950
	public interface IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001E4F RID: 7759
		void Init(KeyGenerationParameters parameters);

		// Token: 0x06001E50 RID: 7760
		AsymmetricCipherKeyPair GenerateKeyPair();
	}
}
