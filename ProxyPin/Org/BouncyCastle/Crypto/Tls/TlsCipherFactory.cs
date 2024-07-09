using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004B7 RID: 1207
	public interface TlsCipherFactory
	{
		// Token: 0x06002521 RID: 9505
		TlsCipher CreateCipher(TlsContext context, int encryptionAlgorithm, int macAlgorithm);
	}
}
