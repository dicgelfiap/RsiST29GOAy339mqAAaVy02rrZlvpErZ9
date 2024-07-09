using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000226 RID: 550
	public abstract class X509ObjectIdentifiers
	{
		// Token: 0x04000CDF RID: 3295
		internal const string ID = "2.5.4";

		// Token: 0x04000CE0 RID: 3296
		public static readonly DerObjectIdentifier CommonName = new DerObjectIdentifier("2.5.4.3");

		// Token: 0x04000CE1 RID: 3297
		public static readonly DerObjectIdentifier CountryName = new DerObjectIdentifier("2.5.4.6");

		// Token: 0x04000CE2 RID: 3298
		public static readonly DerObjectIdentifier LocalityName = new DerObjectIdentifier("2.5.4.7");

		// Token: 0x04000CE3 RID: 3299
		public static readonly DerObjectIdentifier StateOrProvinceName = new DerObjectIdentifier("2.5.4.8");

		// Token: 0x04000CE4 RID: 3300
		public static readonly DerObjectIdentifier Organization = new DerObjectIdentifier("2.5.4.10");

		// Token: 0x04000CE5 RID: 3301
		public static readonly DerObjectIdentifier OrganizationalUnitName = new DerObjectIdentifier("2.5.4.11");

		// Token: 0x04000CE6 RID: 3302
		public static readonly DerObjectIdentifier id_at_telephoneNumber = new DerObjectIdentifier("2.5.4.20");

		// Token: 0x04000CE7 RID: 3303
		public static readonly DerObjectIdentifier id_at_name = new DerObjectIdentifier("2.5.4.41");

		// Token: 0x04000CE8 RID: 3304
		public static readonly DerObjectIdentifier id_at_organizationIdentifier = new DerObjectIdentifier("2.5.4.97");

		// Token: 0x04000CE9 RID: 3305
		public static readonly DerObjectIdentifier IdSha1 = new DerObjectIdentifier("1.3.14.3.2.26");

		// Token: 0x04000CEA RID: 3306
		public static readonly DerObjectIdentifier RipeMD160 = new DerObjectIdentifier("1.3.36.3.2.1");

		// Token: 0x04000CEB RID: 3307
		public static readonly DerObjectIdentifier RipeMD160WithRsaEncryption = new DerObjectIdentifier("1.3.36.3.3.1.2");

		// Token: 0x04000CEC RID: 3308
		public static readonly DerObjectIdentifier IdEARsa = new DerObjectIdentifier("2.5.8.1.1");

		// Token: 0x04000CED RID: 3309
		public static readonly DerObjectIdentifier IdPkix = new DerObjectIdentifier("1.3.6.1.5.5.7");

		// Token: 0x04000CEE RID: 3310
		public static readonly DerObjectIdentifier IdPE = new DerObjectIdentifier(X509ObjectIdentifiers.IdPkix + ".1");

		// Token: 0x04000CEF RID: 3311
		public static readonly DerObjectIdentifier IdAD = new DerObjectIdentifier(X509ObjectIdentifiers.IdPkix + ".48");

		// Token: 0x04000CF0 RID: 3312
		public static readonly DerObjectIdentifier IdADCAIssuers = new DerObjectIdentifier(X509ObjectIdentifiers.IdAD + ".2");

		// Token: 0x04000CF1 RID: 3313
		public static readonly DerObjectIdentifier IdADOcsp = new DerObjectIdentifier(X509ObjectIdentifiers.IdAD + ".1");

		// Token: 0x04000CF2 RID: 3314
		public static readonly DerObjectIdentifier OcspAccessMethod = X509ObjectIdentifiers.IdADOcsp;

		// Token: 0x04000CF3 RID: 3315
		public static readonly DerObjectIdentifier CrlAccessMethod = X509ObjectIdentifiers.IdADCAIssuers;
	}
}
