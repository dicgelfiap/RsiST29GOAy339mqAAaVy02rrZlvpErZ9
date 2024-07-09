using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000511 RID: 1297
	public abstract class PrfAlgorithm
	{
		// Token: 0x04001A07 RID: 6663
		public const int tls_prf_legacy = 0;

		// Token: 0x04001A08 RID: 6664
		public const int tls_prf_sha256 = 1;

		// Token: 0x04001A09 RID: 6665
		public const int tls_prf_sha384 = 2;
	}
}
