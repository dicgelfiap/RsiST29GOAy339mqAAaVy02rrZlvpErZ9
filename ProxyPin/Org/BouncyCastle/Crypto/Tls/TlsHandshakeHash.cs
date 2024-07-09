using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004DF RID: 1247
	public interface TlsHandshakeHash : IDigest
	{
		// Token: 0x06002657 RID: 9815
		void Init(TlsContext context);

		// Token: 0x06002658 RID: 9816
		TlsHandshakeHash NotifyPrfDetermined();

		// Token: 0x06002659 RID: 9817
		void TrackHashAlgorithm(byte hashAlgorithm);

		// Token: 0x0600265A RID: 9818
		void SealHashAlgorithms();

		// Token: 0x0600265B RID: 9819
		TlsHandshakeHash StopTracking();

		// Token: 0x0600265C RID: 9820
		IDigest ForkPrfHash();

		// Token: 0x0600265D RID: 9821
		byte[] GetFinalHash(byte hashAlgorithm);
	}
}
