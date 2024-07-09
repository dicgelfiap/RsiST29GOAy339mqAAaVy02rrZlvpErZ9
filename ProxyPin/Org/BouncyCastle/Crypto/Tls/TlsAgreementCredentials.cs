using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004B5 RID: 1205
	public interface TlsAgreementCredentials : TlsCredentials
	{
		// Token: 0x0600251E RID: 9502
		byte[] GenerateAgreement(AsymmetricKeyParameter peerPublicKey);
	}
}
