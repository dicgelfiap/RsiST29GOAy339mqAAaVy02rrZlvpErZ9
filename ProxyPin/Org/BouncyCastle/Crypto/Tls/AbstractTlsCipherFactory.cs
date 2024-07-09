using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004B8 RID: 1208
	public class AbstractTlsCipherFactory : TlsCipherFactory
	{
		// Token: 0x06002522 RID: 9506 RVA: 0x000CE988 File Offset: 0x000CE988
		public virtual TlsCipher CreateCipher(TlsContext context, int encryptionAlgorithm, int macAlgorithm)
		{
			throw new TlsFatalAlert(80);
		}
	}
}
