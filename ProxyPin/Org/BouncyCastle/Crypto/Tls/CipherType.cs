using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004DC RID: 1244
	public abstract class CipherType
	{
		// Token: 0x040018F2 RID: 6386
		public const int stream = 0;

		// Token: 0x040018F3 RID: 6387
		public const int block = 1;

		// Token: 0x040018F4 RID: 6388
		public const int aead = 2;
	}
}
