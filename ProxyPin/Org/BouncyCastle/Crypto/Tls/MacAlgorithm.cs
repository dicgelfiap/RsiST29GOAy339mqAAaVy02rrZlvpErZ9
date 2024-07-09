using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200050B RID: 1291
	public abstract class MacAlgorithm
	{
		// Token: 0x040019D8 RID: 6616
		public const int cls_null = 0;

		// Token: 0x040019D9 RID: 6617
		public const int md5 = 1;

		// Token: 0x040019DA RID: 6618
		public const int sha = 2;

		// Token: 0x040019DB RID: 6619
		public const int hmac_md5 = 1;

		// Token: 0x040019DC RID: 6620
		public const int hmac_sha1 = 2;

		// Token: 0x040019DD RID: 6621
		public const int hmac_sha256 = 3;

		// Token: 0x040019DE RID: 6622
		public const int hmac_sha384 = 4;

		// Token: 0x040019DF RID: 6623
		public const int hmac_sha512 = 5;
	}
}
