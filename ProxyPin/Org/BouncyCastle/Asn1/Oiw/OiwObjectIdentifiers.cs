using System;

namespace Org.BouncyCastle.Asn1.Oiw
{
	// Token: 0x020001A0 RID: 416
	public abstract class OiwObjectIdentifiers
	{
		// Token: 0x040009BE RID: 2494
		public static readonly DerObjectIdentifier MD4WithRsa = new DerObjectIdentifier("1.3.14.3.2.2");

		// Token: 0x040009BF RID: 2495
		public static readonly DerObjectIdentifier MD5WithRsa = new DerObjectIdentifier("1.3.14.3.2.3");

		// Token: 0x040009C0 RID: 2496
		public static readonly DerObjectIdentifier MD4WithRsaEncryption = new DerObjectIdentifier("1.3.14.3.2.4");

		// Token: 0x040009C1 RID: 2497
		public static readonly DerObjectIdentifier DesEcb = new DerObjectIdentifier("1.3.14.3.2.6");

		// Token: 0x040009C2 RID: 2498
		public static readonly DerObjectIdentifier DesCbc = new DerObjectIdentifier("1.3.14.3.2.7");

		// Token: 0x040009C3 RID: 2499
		public static readonly DerObjectIdentifier DesOfb = new DerObjectIdentifier("1.3.14.3.2.8");

		// Token: 0x040009C4 RID: 2500
		public static readonly DerObjectIdentifier DesCfb = new DerObjectIdentifier("1.3.14.3.2.9");

		// Token: 0x040009C5 RID: 2501
		public static readonly DerObjectIdentifier DesEde = new DerObjectIdentifier("1.3.14.3.2.17");

		// Token: 0x040009C6 RID: 2502
		public static readonly DerObjectIdentifier IdSha1 = new DerObjectIdentifier("1.3.14.3.2.26");

		// Token: 0x040009C7 RID: 2503
		public static readonly DerObjectIdentifier DsaWithSha1 = new DerObjectIdentifier("1.3.14.3.2.27");

		// Token: 0x040009C8 RID: 2504
		public static readonly DerObjectIdentifier Sha1WithRsa = new DerObjectIdentifier("1.3.14.3.2.29");

		// Token: 0x040009C9 RID: 2505
		public static readonly DerObjectIdentifier ElGamalAlgorithm = new DerObjectIdentifier("1.3.14.7.2.1.1");
	}
}
