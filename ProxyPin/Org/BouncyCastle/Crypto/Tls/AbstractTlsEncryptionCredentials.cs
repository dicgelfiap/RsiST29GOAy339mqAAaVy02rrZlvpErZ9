using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004C0 RID: 1216
	public abstract class AbstractTlsEncryptionCredentials : AbstractTlsCredentials, TlsEncryptionCredentials, TlsCredentials
	{
		// Token: 0x0600257F RID: 9599
		public abstract byte[] DecryptPreMasterSecret(byte[] encryptedPreMasterSecret);
	}
}
