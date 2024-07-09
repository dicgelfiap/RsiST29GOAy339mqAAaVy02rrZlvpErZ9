using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000501 RID: 1281
	public abstract class ExporterLabel
	{
		// Token: 0x04001975 RID: 6517
		public const string client_finished = "client finished";

		// Token: 0x04001976 RID: 6518
		public const string server_finished = "server finished";

		// Token: 0x04001977 RID: 6519
		public const string master_secret = "master secret";

		// Token: 0x04001978 RID: 6520
		public const string key_expansion = "key expansion";

		// Token: 0x04001979 RID: 6521
		public const string client_EAP_encryption = "client EAP encryption";

		// Token: 0x0400197A RID: 6522
		public const string ttls_keying_material = "ttls keying material";

		// Token: 0x0400197B RID: 6523
		public const string ttls_challenge = "ttls challenge";

		// Token: 0x0400197C RID: 6524
		public const string dtls_srtp = "EXTRACTOR-dtls_srtp";

		// Token: 0x0400197D RID: 6525
		public static readonly string extended_master_secret = "extended master secret";
	}
}
