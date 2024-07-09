using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004C8 RID: 1224
	public abstract class AbstractTlsSignerCredentials : AbstractTlsCredentials, TlsSignerCredentials, TlsCredentials
	{
		// Token: 0x060025E8 RID: 9704
		public abstract byte[] GenerateCertificateSignature(byte[] hash);

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000CF5AC File Offset: 0x000CF5AC
		public virtual SignatureAndHashAlgorithm SignatureAndHashAlgorithm
		{
			get
			{
				throw new InvalidOperationException("TlsSignerCredentials implementation does not support (D)TLS 1.2+");
			}
		}
	}
}
