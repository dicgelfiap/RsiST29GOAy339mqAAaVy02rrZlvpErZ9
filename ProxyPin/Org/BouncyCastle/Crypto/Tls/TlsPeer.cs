using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004B9 RID: 1209
	public interface TlsPeer
	{
		// Token: 0x06002524 RID: 9508
		void NotifyCloseHandle(TlsCloseable closehandle);

		// Token: 0x06002525 RID: 9509
		void Cancel();

		// Token: 0x06002526 RID: 9510
		bool RequiresExtendedMasterSecret();

		// Token: 0x06002527 RID: 9511
		bool ShouldUseGmtUnixTime();

		// Token: 0x06002528 RID: 9512
		void NotifySecureRenegotiation(bool secureRenegotiation);

		// Token: 0x06002529 RID: 9513
		TlsCompression GetCompression();

		// Token: 0x0600252A RID: 9514
		TlsCipher GetCipher();

		// Token: 0x0600252B RID: 9515
		void NotifyAlertRaised(byte alertLevel, byte alertDescription, string message, Exception cause);

		// Token: 0x0600252C RID: 9516
		void NotifyAlertReceived(byte alertLevel, byte alertDescription);

		// Token: 0x0600252D RID: 9517
		void NotifyHandshakeComplete();
	}
}
