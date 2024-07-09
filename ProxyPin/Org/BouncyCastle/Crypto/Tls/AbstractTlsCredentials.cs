using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004B4 RID: 1204
	public abstract class AbstractTlsCredentials : TlsCredentials
	{
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600251C RID: 9500
		public abstract Certificate Certificate { get; }
	}
}
