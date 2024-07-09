using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200051D RID: 1309
	public abstract class SignatureAlgorithm
	{
		// Token: 0x04001A4C RID: 6732
		public const byte anonymous = 0;

		// Token: 0x04001A4D RID: 6733
		public const byte rsa = 1;

		// Token: 0x04001A4E RID: 6734
		public const byte dsa = 2;

		// Token: 0x04001A4F RID: 6735
		public const byte ecdsa = 3;
	}
}
