using System;
using System.Collections;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004C3 RID: 1219
	public interface TlsServer : TlsPeer
	{
		// Token: 0x060025A1 RID: 9633
		void Init(TlsServerContext context);

		// Token: 0x060025A2 RID: 9634
		void NotifyClientVersion(ProtocolVersion clientVersion);

		// Token: 0x060025A3 RID: 9635
		void NotifyFallback(bool isFallback);

		// Token: 0x060025A4 RID: 9636
		void NotifyOfferedCipherSuites(int[] offeredCipherSuites);

		// Token: 0x060025A5 RID: 9637
		void NotifyOfferedCompressionMethods(byte[] offeredCompressionMethods);

		// Token: 0x060025A6 RID: 9638
		void ProcessClientExtensions(IDictionary clientExtensions);

		// Token: 0x060025A7 RID: 9639
		ProtocolVersion GetServerVersion();

		// Token: 0x060025A8 RID: 9640
		int GetSelectedCipherSuite();

		// Token: 0x060025A9 RID: 9641
		byte GetSelectedCompressionMethod();

		// Token: 0x060025AA RID: 9642
		IDictionary GetServerExtensions();

		// Token: 0x060025AB RID: 9643
		IList GetServerSupplementalData();

		// Token: 0x060025AC RID: 9644
		TlsCredentials GetCredentials();

		// Token: 0x060025AD RID: 9645
		CertificateStatus GetCertificateStatus();

		// Token: 0x060025AE RID: 9646
		TlsKeyExchange GetKeyExchange();

		// Token: 0x060025AF RID: 9647
		CertificateRequest GetCertificateRequest();

		// Token: 0x060025B0 RID: 9648
		void ProcessClientSupplementalData(IList clientSupplementalData);

		// Token: 0x060025B1 RID: 9649
		void NotifyClientCertificate(Certificate clientCertificate);

		// Token: 0x060025B2 RID: 9650
		NewSessionTicket GetNewSessionTicket();
	}
}
