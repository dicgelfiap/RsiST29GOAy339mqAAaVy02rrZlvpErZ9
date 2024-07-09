using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200054B RID: 1355
	public interface TlsSession
	{
		// Token: 0x0600299A RID: 10650
		SessionParameters ExportSessionParameters();

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600299B RID: 10651
		byte[] SessionID { get; }

		// Token: 0x0600299C RID: 10652
		void Invalidate();

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600299D RID: 10653
		bool IsResumable { get; }
	}
}
