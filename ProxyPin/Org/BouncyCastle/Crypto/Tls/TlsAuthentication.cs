using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000519 RID: 1305
	public interface TlsAuthentication
	{
		// Token: 0x060027CF RID: 10191
		void NotifyServerCertificate(Certificate serverCertificate);

		// Token: 0x060027D0 RID: 10192
		TlsCredentials GetClientCredentials(CertificateRequest certificateRequest);
	}
}
