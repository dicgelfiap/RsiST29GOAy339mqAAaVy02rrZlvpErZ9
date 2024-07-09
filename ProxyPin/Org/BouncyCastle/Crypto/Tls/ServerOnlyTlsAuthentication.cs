using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200051A RID: 1306
	public abstract class ServerOnlyTlsAuthentication : TlsAuthentication
	{
		// Token: 0x060027D1 RID: 10193
		public abstract void NotifyServerCertificate(Certificate serverCertificate);

		// Token: 0x060027D2 RID: 10194 RVA: 0x000D6CB8 File Offset: 0x000D6CB8
		public TlsCredentials GetClientCredentials(CertificateRequest certificateRequest)
		{
			return null;
		}
	}
}
