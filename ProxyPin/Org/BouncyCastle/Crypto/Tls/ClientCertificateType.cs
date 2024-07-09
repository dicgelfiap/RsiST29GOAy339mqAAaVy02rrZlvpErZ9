using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004DE RID: 1246
	public abstract class ClientCertificateType
	{
		// Token: 0x040018F8 RID: 6392
		public const byte rsa_sign = 1;

		// Token: 0x040018F9 RID: 6393
		public const byte dss_sign = 2;

		// Token: 0x040018FA RID: 6394
		public const byte rsa_fixed_dh = 3;

		// Token: 0x040018FB RID: 6395
		public const byte dss_fixed_dh = 4;

		// Token: 0x040018FC RID: 6396
		public const byte rsa_ephemeral_dh_RESERVED = 5;

		// Token: 0x040018FD RID: 6397
		public const byte dss_ephemeral_dh_RESERVED = 6;

		// Token: 0x040018FE RID: 6398
		public const byte fortezza_dms_RESERVED = 20;

		// Token: 0x040018FF RID: 6399
		public const byte ecdsa_sign = 64;

		// Token: 0x04001900 RID: 6400
		public const byte rsa_fixed_ecdh = 65;

		// Token: 0x04001901 RID: 6401
		public const byte ecdsa_fixed_ecdh = 66;
	}
}
