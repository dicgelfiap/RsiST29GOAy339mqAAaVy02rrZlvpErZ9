using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004C1 RID: 1217
	public interface TlsKeyExchange
	{
		// Token: 0x06002581 RID: 9601
		void Init(TlsContext context);

		// Token: 0x06002582 RID: 9602
		void SkipServerCredentials();

		// Token: 0x06002583 RID: 9603
		void ProcessServerCredentials(TlsCredentials serverCredentials);

		// Token: 0x06002584 RID: 9604
		void ProcessServerCertificate(Certificate serverCertificate);

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002585 RID: 9605
		bool RequiresServerKeyExchange { get; }

		// Token: 0x06002586 RID: 9606
		byte[] GenerateServerKeyExchange();

		// Token: 0x06002587 RID: 9607
		void SkipServerKeyExchange();

		// Token: 0x06002588 RID: 9608
		void ProcessServerKeyExchange(Stream input);

		// Token: 0x06002589 RID: 9609
		void ValidateCertificateRequest(CertificateRequest certificateRequest);

		// Token: 0x0600258A RID: 9610
		void SkipClientCredentials();

		// Token: 0x0600258B RID: 9611
		void ProcessClientCredentials(TlsCredentials clientCredentials);

		// Token: 0x0600258C RID: 9612
		void ProcessClientCertificate(Certificate clientCertificate);

		// Token: 0x0600258D RID: 9613
		void GenerateClientKeyExchange(Stream output);

		// Token: 0x0600258E RID: 9614
		void ProcessClientKeyExchange(Stream input);

		// Token: 0x0600258F RID: 9615
		byte[] GeneratePremasterSecret();
	}
}
