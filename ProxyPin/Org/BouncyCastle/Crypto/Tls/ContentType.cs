using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004E3 RID: 1251
	public abstract class ContentType
	{
		// Token: 0x04001909 RID: 6409
		public const byte change_cipher_spec = 20;

		// Token: 0x0400190A RID: 6410
		public const byte alert = 21;

		// Token: 0x0400190B RID: 6411
		public const byte handshake = 22;

		// Token: 0x0400190C RID: 6412
		public const byte application_data = 23;

		// Token: 0x0400190D RID: 6413
		public const byte heartbeat = 24;
	}
}
