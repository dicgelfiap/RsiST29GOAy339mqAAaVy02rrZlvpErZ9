using System;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000191 RID: 401
	public abstract class OcspObjectIdentifiers
	{
		// Token: 0x04000988 RID: 2440
		internal const string PkixOcspId = "1.3.6.1.5.5.7.48.1";

		// Token: 0x04000989 RID: 2441
		public static readonly DerObjectIdentifier PkixOcsp = new DerObjectIdentifier("1.3.6.1.5.5.7.48.1");

		// Token: 0x0400098A RID: 2442
		public static readonly DerObjectIdentifier PkixOcspBasic = new DerObjectIdentifier("1.3.6.1.5.5.7.48.1.1");

		// Token: 0x0400098B RID: 2443
		public static readonly DerObjectIdentifier PkixOcspNonce = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".2");

		// Token: 0x0400098C RID: 2444
		public static readonly DerObjectIdentifier PkixOcspCrl = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".3");

		// Token: 0x0400098D RID: 2445
		public static readonly DerObjectIdentifier PkixOcspResponse = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".4");

		// Token: 0x0400098E RID: 2446
		public static readonly DerObjectIdentifier PkixOcspNocheck = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".5");

		// Token: 0x0400098F RID: 2447
		public static readonly DerObjectIdentifier PkixOcspArchiveCutoff = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".6");

		// Token: 0x04000990 RID: 2448
		public static readonly DerObjectIdentifier PkixOcspServiceLocator = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".7");
	}
}
