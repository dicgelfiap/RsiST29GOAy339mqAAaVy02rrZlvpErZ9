using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004C7 RID: 1223
	public interface TlsSignerCredentials : TlsCredentials
	{
		// Token: 0x060025E6 RID: 9702
		byte[] GenerateCertificateSignature(byte[] hash);

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060025E7 RID: 9703
		SignatureAndHashAlgorithm SignatureAndHashAlgorithm { get; }
	}
}
