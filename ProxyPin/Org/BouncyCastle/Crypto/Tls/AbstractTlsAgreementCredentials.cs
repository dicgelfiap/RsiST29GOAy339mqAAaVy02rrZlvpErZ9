using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004B6 RID: 1206
	public abstract class AbstractTlsAgreementCredentials : AbstractTlsCredentials, TlsAgreementCredentials, TlsCredentials
	{
		// Token: 0x0600251F RID: 9503
		public abstract byte[] GenerateAgreement(AsymmetricKeyParameter peerPublicKey);
	}
}
