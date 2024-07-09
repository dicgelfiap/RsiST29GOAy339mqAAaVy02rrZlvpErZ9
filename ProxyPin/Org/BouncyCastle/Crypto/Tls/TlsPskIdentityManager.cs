using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000543 RID: 1347
	public interface TlsPskIdentityManager
	{
		// Token: 0x0600295C RID: 10588
		byte[] GetHint();

		// Token: 0x0600295D RID: 10589
		byte[] GetPsk(byte[] identity);
	}
}
