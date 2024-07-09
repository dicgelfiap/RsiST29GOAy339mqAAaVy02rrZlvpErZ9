using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000524 RID: 1316
	public abstract class SrtpProtectionProfile
	{
		// Token: 0x04001A5D RID: 6749
		public const int SRTP_AES128_CM_HMAC_SHA1_80 = 1;

		// Token: 0x04001A5E RID: 6750
		public const int SRTP_AES128_CM_HMAC_SHA1_32 = 2;

		// Token: 0x04001A5F RID: 6751
		public const int SRTP_NULL_HMAC_SHA1_80 = 5;

		// Token: 0x04001A60 RID: 6752
		public const int SRTP_NULL_HMAC_SHA1_32 = 6;

		// Token: 0x04001A61 RID: 6753
		public const int SRTP_AEAD_AES_128_GCM = 7;

		// Token: 0x04001A62 RID: 6754
		public const int SRTP_AEAD_AES_256_GCM = 8;
	}
}
