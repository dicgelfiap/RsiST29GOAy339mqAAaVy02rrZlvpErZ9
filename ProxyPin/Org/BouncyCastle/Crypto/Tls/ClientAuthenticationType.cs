using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004DD RID: 1245
	public abstract class ClientAuthenticationType
	{
		// Token: 0x040018F5 RID: 6389
		public const byte anonymous = 0;

		// Token: 0x040018F6 RID: 6390
		public const byte certificate_based = 1;

		// Token: 0x040018F7 RID: 6391
		public const byte psk = 2;
	}
}
