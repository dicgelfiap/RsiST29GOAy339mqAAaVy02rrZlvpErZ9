using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004BF RID: 1215
	public interface TlsEncryptionCredentials : TlsCredentials
	{
		// Token: 0x0600257E RID: 9598
		byte[] DecryptPreMasterSecret(byte[] encryptedPreMasterSecret);
	}
}
